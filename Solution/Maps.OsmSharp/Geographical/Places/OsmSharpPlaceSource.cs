using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using log4net;
using Maps.Extensions;
using Maps.Geographical;
using Maps.Geographical.Places;
using Maps.OsmSharp.Geographical.Extensions;
using OsmSharp.Osm;
using OsmSharp.Osm.Data;
using OsmSharp.Osm.Filters;

namespace Maps.OsmSharp.Geographical.Places
{
    /// <summary>
    /// A source for places using OsmSharp
    /// </summary>
    public sealed class OsmSharpPlaceSource : IPlaceSource
    {
        /// <summary>
        /// Does OsmSharpPlaceSource support Box queries? 
        /// </summary>
        public bool SupportsBoxQueries
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(OsmSharpPlaceSource));
                }

                return true;
            }
        }

        /// <summary>
        /// Does SupportsRangeQueries support Range queries?
        /// </summary>
        public bool SupportsRangeQueries
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(OsmSharpPlaceSource));
                }

                return true;
            }
        }

        private const double DefaultBoxSize = 200d;

        private static readonly ILog Log = LogManager.GetLogger(typeof(OsmSharpPlaceSource));

        private readonly List<PlaceCategory> _availableCategories;
        private readonly CategoriesMap _categoriesMap;
        private readonly IDataSourceReadOnly _source;
        private readonly object _sourceLock;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of OsmSharpPlaceSource
        /// </summary>
        /// <param name="source">The OsmSharp data source to use</param>
        public OsmSharpPlaceSource(IDataSourceReadOnly source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            _sourceLock = new object();
            _source = source;
            _categoriesMap = new CategoriesMap();

            // prefill the available categories list
            var placeCategoryValues = Enum.GetValues(typeof(RootPlaceCategory));
            _availableCategories = new List<PlaceCategory>();

            foreach (var categoryValue in placeCategoryValues)
            {
                _availableCategories.Add(new PlaceCategory((RootPlaceCategory)categoryValue));
            }
        }

        /// <summary>
        /// Returns the results for the supported searchable categories 
        /// of places at the given coordinate
        /// 
        /// Note: does not imply that there are places of this category
        /// available at the location
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate</param>
        /// <param name="range">The optional range parameter (meters) to evaluate within, 
        /// ignored if API does not support range queries</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        public IList<PlaceCategory> GetCategories(Geodetic2d coordinate, 
            double range = DefaultBoxSize)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OsmSharpPlaceSource));
            }

            // there are no category restrictions for OsmSharp really
            return _availableCategories.AsReadOnly();
        }

        /// <summary>
        /// Returns the results for the supported searchable categories 
        /// of places for the given coordinate box
        /// 
        /// Note: does not imply that there are places of this category
        /// available at the location
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        public IList<PlaceCategory> GetCategories(GeodeticBox2d box)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OsmSharpPlaceSource));
            }

            return GetCategories(box.Centre);
        }

        /// <summary>
        /// Returns the results for the supported searchable categories 
        /// of places at the given coordinate asyncronously
        /// 
        /// Note: does not imply that there are places of this category
        /// available at the location
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate</param>
        /// <param name="range">The optional range parameter (meters) to evaluate within, 
        /// ignored if API does not support range queries</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        public Task<IList<PlaceCategory>> GetCategoriesAsync(Geodetic2d coordinate, 
            double range = DefaultBoxSize)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OsmSharpPlaceSource));
            }

            return Task<IList<PlaceCategory>>.Factory.StartNew(
                () => GetCategories(coordinate, range));
        }

        /// <summary>
        /// Returns the results for the supported searchable categories 
        /// of places for the given coordinate box asyncronously
        /// 
        /// Note: does not imply that there are places of this category
        /// available at the location
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        public Task<IList<PlaceCategory>> GetCategoriesAsync(GeodeticBox2d box)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OsmSharpPlaceSource));
            }

            return Task<IList<PlaceCategory>>.Factory.StartNew(
                () => GetCategories(box));
        }

        /// <summary>
        /// Returns a list of place results for the given coordinate and query string
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate</param>
        /// <param name="query">The query string to search with</param>
        /// <param name="range">The optional range parameter (meters) to evaluate within, 
        /// ignored if API does not support range queries</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        /// <exception cref="ArgumentNullException"></exception>
        public IList<Place> Get(Geodetic2d coordinate, string query, 
            double range = DefaultBoxSize)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OsmSharpPlaceSource));
            }

            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException(nameof(query));
            }

            return Get(new GeodeticBox2d(coordinate, range), query);
        }

        /// <summary>
        /// Returns a list of place results for the given coordinate box and query string
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        /// <param name="query">The query string to search with</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        /// <exception cref="ArgumentNullException"></exception>
        public IList<Place> Get(GeodeticBox2d box, string query)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OsmSharpPlaceSource));
            }

            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException(nameof(query));
            }

            // lock the source and get the geos from it
            IList<OsmGeo> geos;
            lock (_sourceLock)
            {
                geos = _source.Get(box.GeoCoordinateBox(), FilterForQueryString(query));
            }

            return PlacesFor(geos);
        }

        /// <summary>
        /// Asyncronously returns a list of place results for the given 
        /// coordinate and query string
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate</param>
        /// <param name="query">The query string to search with</param>
        /// <param name="range">The optional range parameter (meters) to evaluate within, 
        /// ignored if API does not support range queries</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        /// <exception cref="ArgumentNullException"></exception>
        public Task<IList<Place>> GetAsync(Geodetic2d coordinate, string query, 
            double range = DefaultBoxSize)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OsmSharpPlaceSource));
            }

            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException(nameof(query));
            }

            return Task<IList<Place>>.Factory.StartNew(
                () => Get(coordinate, query, range));
        }

        /// <summary>
        /// Asyncronously returns a list of place results for the given 
        /// coordinate box and query string
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        /// <param name="query">The query string to search with</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        /// <exception cref="ArgumentNullException"></exception>
        public Task<IList<Place>> GetAsync(GeodeticBox2d box, string query)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OsmSharpPlaceSource));
            }

            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException(nameof(query));
            }

            return Task<IList<Place>>.Factory.StartNew(
                () => Get(box, query));
        }

        /// <summary>
        /// Returns a list of place results for the given coordinate and category
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate</param>
        /// <param name="category">The category to query for</param>
        /// <param name="range">The optional range parameter (meters) to evaluate within, 
        /// ignored if API does not support range queries</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        public IList<Place> Get(Geodetic2d coordinate, PlaceCategory category, 
            double range = DefaultBoxSize)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OsmSharpPlaceSource));
            }

            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            return Get(coordinate, new List<PlaceCategory> { category }, range);
        }

        /// <summary>
        /// Returns a list of place results for the given coordinate box and category
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        /// <param name="category">The category to query for</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        /// <exception cref="ArgumentNullException"></exception>
        public IList<Place> Get(GeodeticBox2d box, PlaceCategory category)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OsmSharpPlaceSource));
            }

            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            return Get(box, new List<PlaceCategory> { category });
        }

        /// <summary>
        /// Asyncronously returns a list of place results for the 
        /// given coordinate and category
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate</param>
        /// <param name="category">The category to query for</param>
        /// <param name="range">The optional range parameter (meters) to evaluate within, 
        /// ignored if API does not support range queries</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        public Task<IList<Place>> GetAsync(Geodetic2d coordinate, PlaceCategory category,
            double range = DefaultBoxSize)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OsmSharpPlaceSource));
            }

            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            return Task<IList<Place>>.Factory.StartNew(
                () => Get(coordinate, category, range));
        }

        /// <summary>
        /// Asyncronously returns a list of place results for the 
        /// given coordinate box and category
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        /// <param name="category">The category to query for</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        public Task<IList<Place>> GetAsync(GeodeticBox2d box, PlaceCategory category)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OsmSharpPlaceSource));
            }

            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            return Task<IList<Place>>.Factory.StartNew(() => Get(box, category));
        }

        /// <summary>
        /// Returns a list of place results for the given coordinate and categories
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate</param>
        /// <param name="categories">The categories to query for</param>
        /// <param name="range">The optional range parameter (meters) to evaluate within, 
        /// ignored if API does not support range queries</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        /// <exception cref="ArgumentNullException"></exception>
        public IList<Place> Get(Geodetic2d coordinate, IList<PlaceCategory> categories,
            double range = DefaultBoxSize)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OsmSharpPlaceSource));
            }

            if (categories == null)
            {
                throw new ArgumentNullException(nameof(categories));
            }

            return Get(new GeodeticBox2d(coordinate, range), categories);
        }

        /// <summary>
        /// Returns a list of place results for the given coordinate box and categories
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        /// <param name="categories">The categories to query for</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        /// <exception cref="ArgumentNullException"></exception>
        public IList<Place> Get(GeodeticBox2d box, IList<PlaceCategory> categories)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OsmSharpPlaceSource));
            }

            if (categories == null)
            {
                throw new ArgumentNullException(nameof(categories));
            }

            var osmFilter = FilterForCategories(categories);

            if (osmFilter == null)
            {
                Log.Warn($"Unable to create filter for categories: {categories.ItemsToStrings()}");
                return new List<Place>();
            }

            // lock the source and get the geos from it
            IList<OsmGeo> geos;
            lock (_sourceLock)
            {
                try
                {
                    geos = _source.Get(box.GeoCoordinateBox(), osmFilter);
                }
                catch (ArgumentException e)
                {
                    Log.Error($"Could not perform query for {box} with categories: " +
                                $"{categories.ItemsToStrings()}", e);

                    return new List<Place>();
                }
            }

            return PlacesFor(geos);
        }

        /// <summary>
        /// Asyncronously returns a list of place results for the 
        /// given coordinate and categories
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate</param>
        /// <param name="categories">The categories to query for</param>
        /// <param name="range">The optional range parameter (meters) to evaluate within, 
        /// ignored if API does not support range queries</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        /// <exception cref="ArgumentNullException"></exception>
        public Task<IList<Place>> GetAsync(Geodetic2d coordinate, 
            IList<PlaceCategory> categories, double range = DefaultBoxSize)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OsmSharpPlaceSource));
            }

            if (categories == null)
            {
                throw new ArgumentNullException(nameof(categories));
            }

            return Task<IList<Place>>.Factory.StartNew(
                () => Get(coordinate, categories, range));
        }

        /// <summary>
        /// Asyncronously returns a list of place results for the 
        /// given coordinate box and categories
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        /// <param name="categories">The categories to query for</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        /// <exception cref="ArgumentNullException"></exception>
        public Task<IList<Place>> GetAsync(GeodeticBox2d box, IList<PlaceCategory> categories)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OsmSharpPlaceSource));
            }

            if (categories == null)
            {
                throw new ArgumentNullException(nameof(categories));
            }

            return Task<IList<Place>>.Factory.StartNew(
                () => Get(box, categories));
        }

        /// <summary>
        /// Disposes of all resources held by the OsmSharpPlaceSource
        /// </summary>
        public void Dispose()
        {
            _disposed = true;
        }

        private Filter FilterForQueryString(string query)
        {
            // type filter
            var filter = Filter.Type(OsmGeoType.Node) | Filter.Type(OsmGeoType.Way);

            // global wildcard searchcase
            if (query == "*")
            {
                return filter;
            }

            // sanitise the string
            query = query.ToLower();

            filter &= Filter.Match("name", query) | Filter.MatchPartial("amenity", query);

            return filter;
        }

        private Filter FilterForCategories(IList<PlaceCategory> categories)
        {
            Filter tagsFilter = null;

            // run through each category
            foreach (var category in categories)
            {
                // if category is unknown, skip it
                if (category.Root == RootPlaceCategory.Invalid)
                {
                    continue;
                }

                // get the matching tags and values for the category
                var tagsForCategory = category.Tags(_categoriesMap);

                // run through each tag and it's values
                foreach (var tag in tagsForCategory)
                {
                    // match all for this tag key if * is present in first slot
                    if (tag.Value[0] == "*")
                    {
                        if (tagsFilter == null)
                        {
                            tagsFilter = Filter.Exists(tag.Key);
                        }
                        else
                        {
                            tagsFilter |= Filter.Exists(tag.Key);
                        }
                    }
                    // otherwise match the specified values for this key
                    else
                    {
                        if (tagsFilter == null)
                        {
                            tagsFilter = Filter.MatchAny(tag.Key, tag.Value.ToArray());
                        }
                        else
                        {
                            tagsFilter |= Filter.MatchAny(tag.Key, tag.Value.ToArray());
                        }
                    }
                }
            }

            // if there were absolutely no tags matched for categories, return null
            if (tagsFilter == null)
            {
                return null;
            }

            // we only search for nodes or ways
            var typeFilter = Filter.Type(OsmGeoType.Node) | Filter.Type(OsmGeoType.Way);

            return typeFilter & tagsFilter;
        }

        private List<Place> PlacesFor(IList<OsmGeo> geos)
        {
            if (geos == null || geos.Count <= 0)
            {
                return new List<Place>();
            }

            var result = new List<Place>();

            var nodes = new Dictionary<long, Node>();
            var missingNodes = new List<long>();

            var ways = new List<Way>();
            //var relations = new List<Relation>();

            // handle nodes as they have no depedencies, cache nodes against ids
            // to be used for resolving ways and relations
            foreach (var geo in geos)
            {
                switch (geo.Type)
                {
                    case OsmGeoType.Node:
                        var node = geo as Node;

                        if (node != null && node.Id.HasValue)
                        {
                            // cache the node for usage later
                            nodes[node.Id.Value] = node;

                            // add the node while we are here
                            try
                            {
                                var placeResult = PlaceFor(node);

                                if (placeResult.Category.Root != RootPlaceCategory.Invalid)
                                {
                                    result.Add(PlaceFor(node));
                                } // skip place results categorized as unknown
                                else
                                {
                                    Log.Warn($"Node {node.Id} categorized as Unknown");
                                }
                            }
                            catch (Exception e)
                            {
                                Log.Error($"Could not add PlaceResult from Node {node.Id}", e);
                            }
                        }
                        break;
                    case OsmGeoType.Way:
                        var way = geo as Way;

                        if (way != null)
                        {
                            ways.Add(way);

                            for (int i = 0; i < way.Nodes.Count; i++)
                            {
                                var nodeId = way.Nodes[i];
                                if (!nodes.ContainsKey(nodeId))
                                {
                                    missingNodes.Add(nodeId);
                                }
                            }
                        }
                        break;
                    default:
                        throw new NotImplementedException("Relations not yet supported");
                }
                //else
                //{
                //    var relation = geo as Relation;

                //    if (relation != null)
                //    {
                //        relations.Add(relation);
                //    }
                //}
            }

            // fetch any missing nodes and add them to the node map
            if (missingNodes.Count > 0)
            {
                IList<Node> fetchedNodes;

                lock (_sourceLock)
                {
                    fetchedNodes = _source.GetNodes(missingNodes);
                }

                foreach (var node in fetchedNodes)
                {
                    nodes[node.Id.Value] = node;
                }
            }

            // append place results for ways and relations
            AppendPlacesFor(ways, nodes, result);
            //AppendPlaceResultsFor(relations, result);

            return result;
        }

        private void AppendPlacesFor(List<Way> ways, Dictionary<long, Node> nodeMap, List<Place> to)
        {
            foreach (var way in ways)
            {
                try
                {
                    var placeResult = PlaceFor(way, nodeMap);

                    if (placeResult.Category.Root != RootPlaceCategory.Invalid)
                    {
                        to.Add(PlaceFor(way, nodeMap));
                    } // skip place results categorized as unknown
                    else
                    {
                        Log.Warn($"Way {way.Id} categorized as Unknown");
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Could not add PlaceResult from Way {way.Id}", e);
                }
            }
        }

        //private void AppendPlaceResultsFor(List<Relation> relations, List<PlaceResult> to)
        //{
        //    foreach (var relation in relations)
        //    {
        //        try
        //        {
        //            to.Add(PlaceResultFor(relation));
        //        }
        //        catch (Exception e)
        //        {
        //            Log.Error($"Could not add PlaceResult from Relation {relation.Id}", e);
        //        }
        //    }
        //}

        private Place PlaceFor(Node node)
        {
            return new Place(node.Guid(), node.Name(),
                node.Geodetic2d(), node.Category(_categoriesMap));
        }

        private Place PlaceFor(Way way, Dictionary<long, Node> nodeMap)
        {
            return new Place(way.Guid(), way.Name(),
                PlaceCoordinateFor(way, nodeMap), way.Category(_categoriesMap));
        }

        //private PlaceResult PlaceResultFor(Relation relation)
        //{
        //    return new PlaceResult(PlaceCoordinateFor(relation), relation.Category(), 
        //        relation.Name());
        //}

        private Geodetic2d PlaceCoordinateFor(Way way, Dictionary<long, Node> nodeMap)
        {
            // area
            if (way.Nodes[0] == way.Nodes[way.Nodes.Count - 1] ||
                way.Nodes[0] == way.Nodes[way.Nodes.Count - 2])
            {
                // find centre of bounding box of nodes
                var nodes = new List<Node>();

                foreach (var nodeId in way.Nodes)
                {
                    if (nodeMap.ContainsKey(nodeId))
                    {
                        nodes.Add(nodeMap[nodeId]);
                    }
                    else
                    {
                        Log.Warn($"Missing Node {nodeId} from way {way.Id}");
                    }
                }

                return nodes.BoundingBox().Centre;
            }

            // otherwise it's a line, attempt to return the middle point
            if (nodeMap.ContainsKey(way.Nodes[way.Nodes.Count / 2 - 1]))
            {
                return nodeMap[way.Nodes.Count / 2 - 1].Geodetic2d();
            }

            // couldn't find the middle point, just return the first possible point
            foreach (var nodeId in way.Nodes)
            {
                if (nodeMap.ContainsKey(nodeId))
                {
                    return nodeMap[nodeId].Geodetic2d();
                }
            }

            throw new Exception($"Could not find a suitable point for {way.Id}");
        }

        //private Geodetic2d PlaceCoordinateFor(Relation relation)
        //{
        //    throw new NotImplementedException();
        //}
    }
}