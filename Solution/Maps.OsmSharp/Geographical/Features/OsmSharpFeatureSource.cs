using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maps.Geographical;
using Maps.Geographical.Features;
using Maps.OsmSharp.Collections;
using Maps.OsmSharp.Geographical.Extensions;
using OsmSharp.Osm;
using OsmSharp.Osm.Data;
using OsmSharp.Osm.Filters;

namespace Maps.OsmSharp.Geographical.Features
{
    /// <summary>
    /// A source for features using OsmSharp
    /// </summary>
    public class OsmSharpFeatureSource : IFeatureSource
    {
        /// <inheritdoc />
        public SourceMeta Meta
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Container class for shifting geos around along
        /// with support for tracking any missing geos
        /// </summary>
        private class IntermediateGeoContainer
        {
            /// <summary>
            /// Are any geos missing?
            /// </summary>
            public bool MissingAny => MissingRelationsMap.Count > 0 || 
                                      MissingWaysMap.Count > 0 ||
                                      MissingNodesMap.Count > 0;

            public readonly List<long> MissingNodesList = new List<long>();
            public readonly List<long> MissingWaysList = new List<long>();
            public readonly List<long> MissingRelationsList = new List<long>();

            public readonly HashSet<long> MissingNodesMap = new HashSet<long>();
            public readonly HashSet<long> MissingWaysMap = new HashSet<long>();
            public readonly HashSet<long> MissingRelationsMap = new HashSet<long>();

            public readonly Dictionary<long, Node> Nodes = new Dictionary<long, Node>();
            public readonly Dictionary<long, Way> Ways = new Dictionary<long, Way>();
            public readonly Dictionary<long, Relation> Relations = new Dictionary<long, Relation>();
        }

        private static Filter FeatureFilter => Filter.Any();

        private readonly IDataSourceReadOnly _source;
        private readonly IOsmSharpGeoTranslator _translator;

        private readonly object _sourceLock;
        private const int MaxRelationSearchDepth = 5;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of OsmSharpFeatureSource
        /// </summary>
        /// <param name="source">The OsmSharp data source to use</param>
        public OsmSharpFeatureSource(IDataSourceReadOnly source)
            : this(source, new DefaultOsmGeoTranslator())
        {

        }

        /// <summary>
        /// Initializes a new instance of OsmSharpFeatureSource
        /// </summary>
        /// <param name="source">The OsmSharp data source to use</param>
        /// <param name="translator">The geo translator to use</param>
        public OsmSharpFeatureSource(IDataSourceReadOnly source, 
            IOsmSharpGeoTranslator translator)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (translator == null)
            {
                throw new ArgumentNullException(nameof(translator));
            }

            _sourceLock = new object();
            _source = source;
            _translator = translator;
        }

        /// <summary>
        /// Returns features for the given coordinate box
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        public IList<Feature> Get(GeodeticBox2d box)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OsmSharpFeatureSource));
            }

            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            // lock the source and get the geos from it
            IList<OsmGeo> geos;
            lock (_sourceLock)
            {
                geos = _source.Get(box.GeoCoordinateBox(), FeatureFilter);
            }

            IList<Feature> result = new List<Feature>();

            if (geos != null && geos.Count > 0)
            {
                result = FeaturesFor(geos, _source);
            }

            return result;
        }

        /// <summary>
        /// Returns features for the given coordinate box asyncronously
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        public Task<IList<Feature>> GetAsync(GeodeticBox2d box)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OsmSharpFeatureSource));
            }

            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            return Task<IList<Feature>>.Factory.StartNew(() => Get(box));
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _disposed = true;
        }

        private IList<Feature> FeaturesFor(IList<OsmGeo> geos, 
            IDataSourceReadOnly source)
        {
            // fetch geos as complete as possible including depedencies
            var completeGeos = CompleteGeos(geos, source);

            // post process complete geos to remove any broken elements
            var pristineGeos = PristineGeos(completeGeos);

            return _translator.FeaturesFor(pristineGeos);
        }

        private static IntermediateGeoContainer CompleteGeos(IList<OsmGeo> geos,
            IDataSourceReadOnly source)
        {
            // intermediate container
            var container = new IntermediateGeoContainer();

            // add all geos to the container
            var geosCount = geos.Count;
            for (var i = 0; i < geosCount; i++)
            {
                var geo = geos[i];

                switch (geo.Type)
                {
                    case OsmGeoType.Node:
                        var node = geo as Node;
                        Add(node, container);
                        break;
                    case OsmGeoType.Way:
                        var way = geo as Way;
                        Add(way, container);
                        break;
                    case OsmGeoType.Relation:
                        var relation = geo as Relation;
                        Add(relation, container);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(geo.Type));
                }
            }

            // resolve all missing geos for relations
            foreach (var relation in container.Relations)
            {
                ResolveDepedencies(relation.Value, container);
            }

            // attempt to get missing relations
            var searchDepth = 0;
            while (container.MissingRelationsMap.Count > 0 && 
                searchDepth < MaxRelationSearchDepth)
            {
                // fetch missing relations
                var fetchedRelations = source.GetRelations(
                    container.MissingRelationsList);
                var fetchedRelationsCount = fetchedRelations.Count;

                // when no missing relations were found, we're getting nowhere
                if (fetchedRelationsCount < 1)
                {
                    break;
                }

                for (var i = 0; i < fetchedRelationsCount; i++)
                {
                    // add relation to container
                    Add(fetchedRelations[i], container);
                    ResolveDepedencies(fetchedRelations[i], container);
                }

                searchDepth++;
            }

            // throw error when searched max search depth
            if (searchDepth >= MaxRelationSearchDepth)
            {
                throw new InvalidOperationException("Reached max relation search depth");
            }

            // attempt to get missing ways
            var fetchedWays = source.GetWays(container.MissingWaysList);
            var fetchedWaysCount = fetchedWays.Count;
            for (var i = 0; i < fetchedWaysCount; i++)
            {
                // add way to container
                Add(fetchedWays[i], container);
            }

            // resolve all missing nodes
            foreach (var way in container.Ways)
            {
                ResolveDepedencies(way.Value, container);
            }

            // attempt to get missing nodes
            var fetchedNodes = source.GetNodes(container.MissingNodesList);
            var fetchedNodesCount = fetchedNodes.Count;
            for (var i = 0; i < fetchedNodesCount; i++)
            {
                // simply add the node to the container
                Add(fetchedNodes[i], container);
            }

            return container;
        }

        private static void Add(Node node, IntermediateGeoContainer container)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (!node.Id.HasValue)
            {
                throw new ArgumentException("Id has no value", nameof(node));
            }

            if (!node.Latitude.HasValue)
            {
                throw new ArgumentException("Latitude has no value", nameof(node));
            }

            if (!node.Longitude.HasValue)
            {
                throw new ArgumentException("Longitude has no value", nameof(node));
            }

            var nodeId = node.Id.Value;

            // check for existence in node dictionary
            if (container.Nodes.ContainsKey(nodeId))
            {
                throw new InvalidOperationException($"{nodeId} already added");
            }

            container.Nodes[nodeId] = node;

            // remove it from the missing node set if it exists
            if (container.MissingNodesMap.Remove(nodeId))
            {
                // if it's in the set, remove it from the list
                container.MissingNodesList.Remove(nodeId);
            }
        }

        private static void Add(Way way, IntermediateGeoContainer container)
        {
            // validate required resources
            if (way == null)
            {
                throw new ArgumentNullException(nameof(way));
            }

            if (!way.Id.HasValue)
            {
                throw new ArgumentException("Id has no value", nameof(way));
            }

            if (way.Nodes == null)
            {
                throw new ArgumentException("Nodes is null", nameof(way));
            }

            var wayId = way.Id.Value;

            // check for existence in way dictionary
            if (container.Ways.ContainsKey(wayId))
            {
                throw new InvalidOperationException($"{wayId} already added");
            }

            // add to way dictionary
            container.Ways[wayId] = way;

            // remove from missing way set if exists
            if (container.MissingWaysMap.Remove(wayId))
            {
                // if it's in the set, remove it from the list
                container.MissingWaysList.Remove(wayId);
            }
        }

        private static void ResolveDepedencies(Way way, IntermediateGeoContainer container)
        {
            var nodesCount = way.Nodes.Count;
            for (var j = 0; j < nodesCount; j++)
            {
                var nodeId = way.Nodes[j];
                if (!container.Nodes.ContainsKey(nodeId))
                {
                    if (container.MissingNodesMap.Add(nodeId))
                    {
                        container.MissingNodesList.Add(nodeId);
                    }
                }
            }
        }

        private static void Add(Relation relation, IntermediateGeoContainer container)
        {
            // validate required resources
            if (relation == null)
            {
                throw new ArgumentNullException(nameof(relation));
            }

            if (!relation.Id.HasValue)
            {
                throw new ArgumentException("Id has no value", nameof(relation));
            }

            if (relation.Members == null)
            {
                throw new ArgumentException("Members is null", nameof(relation));
            }

            var relationId = relation.Id.Value;

            // check for existence in relation dictionary
            if (!container.Relations.ContainsKey(relationId))
            {
                // add to relation dictionary
                container.Relations[relation.Id.Value] = relation;
            }

            // remove from missing relation set if exists
            if (container.MissingRelationsMap.Remove(relationId))
            {
                // if it's in the set, remove it from the list
                container.MissingRelationsList.Remove(relationId);
            }
        }

        private static void ResolveDepedencies(Relation relation, IntermediateGeoContainer container)
        {
            // either way its a new relation, resolve its dependencies
            var relationMembersCount = relation.Members.Count;
            for (var i = 0; i < relationMembersCount; i++)
            {
                var member = relation.Members[i];

                if (!member.MemberType.HasValue)
                {
                    throw new ArgumentException($"{relation.Id.Value} has a missing MemberType on a member");
                }

                if (!member.MemberId.HasValue)
                {
                    throw new ArgumentException($"{relation.Id.Value} has a missing MemberId on a member");
                }

                var memberId = member.MemberId.Value;
                switch (member.MemberType.Value)
                {
                    case OsmGeoType.Node:
                        if (!container.Nodes.ContainsKey(memberId))
                        {
                            if (container.MissingNodesMap.Add(memberId))
                            {
                                container.MissingNodesList.Add(memberId);
                            }
                        }
                        break;
                    case OsmGeoType.Way:
                        if (!container.Ways.ContainsKey(memberId))
                        {
                            if (container.MissingWaysMap.Add(memberId))
                            {
                                container.MissingWaysList.Add(memberId);
                            }
                        }
                        break;
                    case OsmGeoType.Relation:
                        if (!container.Relations.ContainsKey(memberId))
                        {
                            if (container.MissingRelationsMap.Add(memberId))
                            {
                                container.MissingRelationsList.Add(memberId);
                            }
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(member.MemberType));
                }
            }
        }

        private ReadOnlyOsmGeoCollection PristineGeos(IntermediateGeoContainer completeGeos)
        {
            // nodes are already completely validated, copy them over
            var pristineNodes = new Dictionary<long, Node>(completeGeos.Nodes);

            // ways next
            var pristineWays = new Dictionary<long, Way>();
            var unpristineWays = new Dictionary<long, Way>();

            foreach (var way in completeGeos.Ways)
            {
                var pristine = true;

                // validate that every node exists in the pristine state
                var nodesCount = way.Value.Nodes.Count;
                for (var i = 0; i < nodesCount; i++)
                {
                    var node = way.Value.Nodes[i];
                    if (!pristineNodes.ContainsKey(node))
                    {
                        // exit early if not pristine
                        pristine = false;
                        break;
                    }
                }

                if (pristine)
                {
                    pristineWays.Add(way.Key, way.Value);
                }
                else
                {
                    unpristineWays.Add(way.Key, way.Value);
                }
            }

            // relations last
            var pristineRelations = PristineRelations(completeGeos, 
                pristineNodes, pristineWays);

            return new ReadOnlyOsmGeoCollection(pristineNodes, pristineWays, 
                pristineRelations);
        }

        private static Dictionary<long, Relation> PristineRelations(
            IntermediateGeoContainer completeGeos, 
            IDictionary<long, Node> pristineNodes,
            IDictionary<long, Way> pristineWays)
        {
            var pristineRelations = PristineRelationsFirstPass(completeGeos,
                pristineNodes, pristineWays);
            pristineRelations = PristineRelationsSecondPass(pristineRelations);
            return pristineRelations;
        }

        private static Dictionary<long, Relation> PristineRelationsFirstPass(
            IntermediateGeoContainer geos, IDictionary<long, Node> nodes, 
            IDictionary<long, Way> ways)
        {
            var pristineRelations = new Dictionary<long, Relation>();

            // check existence of each relation's members
            foreach (var relation in geos.Relations)
            {
                var pristine = true;
                var memberCount = relation.Value.Members.Count;
                for (var i = 0; i < memberCount; i++)
                {
                    var member = relation.Value.Members[i];
                    switch (member.MemberType.Value)
                    {
                        case OsmGeoType.Node:
                            if (!nodes.ContainsKey(member.MemberId.Value))
                            {
                                pristine = false;
                            }
                            break;
                        case OsmGeoType.Way:
                            if (!ways.ContainsKey(member.MemberId.Value))
                            {
                                pristine = false;
                            }
                            break;
                        case OsmGeoType.Relation:
                            if (!geos.Relations.ContainsKey(member.MemberId.Value))
                            {
                                pristine = false;
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(
                                nameof(member.MemberType.Value));
                    }

                    // exit early if not pristine
                    if (!pristine)
                    {
                        break;
                    }
                }

                if (pristine)
                {
                    pristineRelations.Add(relation.Key, relation.Value);
                }

            }

            return pristineRelations;
        }

        private static Dictionary<long, Relation> PristineRelationsSecondPass(
            Dictionary<long, Relation> relations)
        {
            var unpristineRelations = UnpristineRelations(relations);
            var searchDepth = 0;

            while (unpristineRelations.Count > 0 &&
                searchDepth < MaxRelationSearchDepth)
            {
                // remove unpristine relations from the pristine map
                foreach (var relation in unpristineRelations)
                {
                    relations.Remove(relation.Key);
                }

                // remove the relations depending on the unpristine ones
                RemoveDependents(relations, unpristineRelations);

                // resolve the next lot of unpristine relations
                unpristineRelations = UnpristineRelations(relations);
                searchDepth++;
            }

            // throw error when searched max search depth
            if (searchDepth >= MaxRelationSearchDepth)
            {
                throw new InvalidOperationException("Reached max relation search depth");
            }

            return relations;
        }

        private static Dictionary<long, Relation> UnpristineRelations(
            IDictionary<long, Relation> relations)
        {
            var unpristineRelations = new Dictionary<long, Relation>();

            // run through relations and cache unpristine relations
            foreach (var relation in relations)
            {
                var pristine = true;
                var memberCount = relation.Value.Members.Count;
                for (var i = 0; i < memberCount; i++)
                {
                    // if a member is a relation, check its pristine state
                    var member = relation.Value.Members[i];
                    if (member.MemberType.Value == OsmGeoType.Relation)
                    {
                        var memberId = member.MemberId.Value;

                        if (!relations.ContainsKey(memberId) ||
                            unpristineRelations.ContainsKey(memberId))
                        {
                            // exit early if not pristine
                            pristine = false;
                            break;
                        }
                    }
                }

                if (!pristine)
                {
                    // add the relation to the unpristine map
                    unpristineRelations.Add(relation.Key, relation.Value);
                }
            }

            return unpristineRelations;
        }

        private static void RemoveDependents(Dictionary<long, Relation> relations,
            Dictionary<long, Relation> unpristineRelations)
        {
            var dependencyMap = DependentRelations(relations);

            // run through the unpristine relations
            foreach (var relation in unpristineRelations)
            {                
                if (dependencyMap.ContainsKey(relation.Key))
                {
                    // any relations depending on it are now unpristine
                    var dependents = dependencyMap[relation.Key];

                    // make the dependents unpristine
                    var dependentCount = dependents.Count;
                    for (var i = 0; i < dependentCount; i++)
                    {
                        var dependent = dependents[i];
                        relations.Remove(dependent);
                    }
                }
            }
        }

        private static Dictionary<long, List<long>> DependentRelations(
            Dictionary<long, Relation> relations)
        {
            var dependantRelations = new Dictionary<long, List<long>>();

            // build map of relation depdendencies
            foreach (var relation in relations)
            {
                var membersCount = relation.Value.Members.Count;
                for (var i = 0; i < membersCount; i++)
                {
                    var member = relation.Value.Members[i];
                    if (member.MemberType.Value == OsmGeoType.Relation)
                    {
                        var memberId = member.MemberId.Value;

                        if (!dependantRelations.ContainsKey(memberId))
                        {
                            dependantRelations[memberId] = new List<long>();
                        }

                        dependantRelations[memberId].Add(relation.Key);
                    }
                }
            }
            return dependantRelations;
        }

        /// <inheritdoc />
        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}