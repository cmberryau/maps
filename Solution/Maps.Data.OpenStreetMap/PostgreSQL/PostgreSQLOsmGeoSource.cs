using System;
using System.Collections.Generic;
using System.Text;
using Maps.Data.OpenStreetMap.Collections;
using Maps.Data.OpenStreetMap.Extensions;
using Maps.Extensions;
using Maps.Geographical;
using log4net;
using Npgsql;

namespace Maps.Data.OpenStreetMap.PostgreSQL
{
    /// <summary>
    /// OpenStreetMap geometry source using PostgreSQL
    /// </summary>
    public class PostgreSQLOsmGeoSource : IOsmGeoSource
    {
        #region command strings

        private const string SelectNodesPrefix = @"SELECT " +
                                                     @"nodes.id, " +
                                                     @"nodes.tags, " +
                                                     @"ST_Y(nodes.geom), " +
                                                     @"ST_X(nodes.geom) " +
                                                 @"FROM " +
                                                     @"nodes ";

        private const string SelectNodesGeomSuffix = @"WHERE " +
                                                         @"nodes.geom && " +
                                                         @"ST_MakeEnvelope({0})";

        private const string SelectWaysPrefix = @"SELECT " +
                                                    @"ways.id, " + 
                                                    @"ways.tags, " +
                                                    @"ways.nodes " + 
                                                @"FROM " + 
                                                    @"ways ";

        private const string SelectWaysBboxSuffix = @"WHERE " +
                                                        @"ways.bbox && " +
                                                        @"ST_MakeEnvelope({0})";

        private const string SelectRelationsPrefix = @"SELECT " +
                                                         @"relations.id, " +
                                                         @"relations.tags " +
                                                     @"FROM " +
                                                         @"relations ";

        private const string SelectRelationsBboxSuffix = @"WHERE " +
                                                            @"relations.bbox && " +
                                                            @"ST_MakeEnvelope({0})";

        private const string SelectRelationMembers = @"SELECT " +
                                                         @"relation_members.relation_id, " +
                                                         @"relation_members.member_id, " +
                                                         @"relation_members.member_type, " +
                                                         @"relation_members.member_role, " +
                                                         @"relation_members.sequence_id " +
                                                     @"FROM " +
                                                         @"relation_members " + 
                                                     @"WHERE " +
                                                         @"relation_members.relation_id " +
                                                     @"IN ({0})";

        private const string NodesSnippet = @"nodes";
        private const string WaysSnippet = @"ways";
        private const string RelationsSnippet = @"relations";

        private const string NodeMemberSnippet = @"N";
        private const string WayMemberSnippet = @"W";
        private const string RelationMemberSnippet = @"R";

        private const string SelectIdsSuffix = @"WHERE " +
                                                   @"{0}.id " +
                                               @"IN ({1})";

        private const string SelectTagsSuffix = @"{0}.tags -> '{1}' = '{2}'";
        private const string SelectTagsExistSuffix = @"exist({0}.tags, '{1}')";

        private const string OrSnippet = @" OR ";
        private const string AndSnippet = @" AND ";

        #endregion command strings

        private static readonly ILog Log = LogManager.GetLogger(typeof(PostgreSQLOsmGeoSource));

        private const int NodeReadBatchSize = 1024;
        private const int WayReadBatchSize = 1024;
        private const int RelationReadBatchSize = 1024;
        private const int RelationMemberReadBatchSize = 512;

        private readonly PostgreSQLClient _client;
        private readonly IOsmGeoCollection _cache;
        private readonly IOsmGeoIdCollection _failedIdCache;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of PostgreSQLOsmFeatureSource
        /// </summary>
        /// <param name="client">The postgreSQL client to use</param>
        public PostgreSQLOsmGeoSource(PostgreSQLClient client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            _client = client;
            _cache = new OsmGeoCollection();
            _failedIdCache = new OsmGeoIdCollection();
        }

        private PostgreSQLOsmGeoSource(PostgreSQLClient client, IOsmGeoCollection cache,
            IOsmGeoIdCollection failedIdCache)
        {
            _client = client;
            _cache = cache;
            _failedIdCache = failedIdCache;
        }

        /// <inheritdoc />
        public IList<Node> GetNodes(IList<long> ids)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(PostgreSQLOsmGeoSource));
            }

            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var idsCount = ids.Count;
            if (idsCount <= 0)
            {
                return new Node[0];
            }

            var nodesDictionary = GetNodesDictionary(ids.NoDuplicates());

            // create the return list, ordered the same as incoming ids
            var nodes = new List<Node>();
            if (nodesDictionary.Count > 0)
            {
                for (var i = 0; i < idsCount; ++i)
                {
                    if (nodesDictionary.TryGetValue(ids[i], out Node node))
                    {
                        nodes.Add(node);
                    }
                    else
                    {
                        _failedIdCache.AddNodeId(ids[i]);
                        Log.Info($"Failed to fetch node: {ids[i]}");
                    }
                }
            }

            return nodes;
        }

        /// <inheritdoc />
        public IList<Node> GetNodes(GeodeticBox2d box, IList<Tuple<string, 
            IList<string>>> tags = null)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(PostgreSQLOsmGeoSource));
            }

            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            var sqlSb = new StringBuilder(SelectNodesPrefix);
            sqlSb.Append(string.Format(SelectNodesGeomSuffix, 
                PostgreSQLClient.EnvelopeString(box)));

            if (tags != null)
            {
                sqlSb.Append(AndSnippet);
                sqlSb.Append(TagsString(NodesSnippet, tags));
            }

            var nodes = new List<Node>();

            using (var command = new NpgsqlCommand(sqlSb.ToString(), _client.Connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    var idsSet = new HashSet<long>();

                    while (reader.Read())
                    {
                        var id = reader.GetInt64(0);

                        if (idsSet.Add(id))
                        {
                            if (_cache.TryGetNode(id, out Node node))
                            {
                                nodes.Add(node);
                            }
                            else
                            {
                                var nodeTags = reader.GetFieldValue<Dictionary<string, 
                                    string>>(1);
                                var latitude = reader.GetDouble(2);
                                var longitude = reader.GetDouble(3);
                                var coordinate = new Geodetic2d(latitude, longitude);

                                node = new Node(id, nodeTags, coordinate);
                                _cache.AddNode(node);
                                nodes.Add(node);
                            }
                        }
                    }
                }
            }

            return nodes;
        }

        /// <inheritdoc />
        public IList<Way> GetWays(IList<long> ids)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(PostgreSQLOsmGeoSource));
            }

            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var idsCount = ids.Count;
            if (idsCount <= 0)
            {
                return new List<Way>();
            }

            var waysDictionary = GetWaysDictionary(ids.NoDuplicates());

            // create the return list, ordered the same as incoming ids
            var orderedWays = new List<Way>();
            if (waysDictionary.Count > 0)
            {
                for (var i = 0; i < idsCount; ++i)
                {
                    if (waysDictionary.TryGetValue(ids[i], out Way way))
                    {
                        orderedWays.Add(way);
                    }
                }
            }

            return orderedWays;
        }

        /// <inheritdoc />
        public IList<Way> GetWays(GeodeticBox2d box, IList<Tuple<string, 
            IList<string>>> tags = null)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(PostgreSQLOsmGeoSource));
            }

            var sqlSb = new StringBuilder(SelectWaysPrefix);
            sqlSb.Append(string.Format(SelectWaysBboxSuffix, 
                PostgreSQLClient.EnvelopeString(box)));

            // only include tags in the query if we're given them
            if (tags != null)
            {
                sqlSb.Append(AndSnippet);
                sqlSb.Append(TagsString(WaysSnippet, tags));
            }

            var ways = new List<Way>();
            var wayIds = new List<long>();
            var wayTags = new List<IDictionary<string, string>>();
            var wayNodeIds = new List<long[]>();

            using (var command = new NpgsqlCommand(sqlSb.ToString(), _client.Connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    var idsSet = new HashSet<long>();

                    while (reader.Read())
                    {
                        var id = reader.GetInt64(0);

                        if (idsSet.Add(id))
                        {
                            // always attempt to get from the cache
                            if (_cache.TryGetWay(id, out Way way))
                            {
                                ways.Add(way);
                            }
                            else
                            {
                                var thisWayTags = reader.GetFieldValue<Dictionary<string,
                                    string>>(1);
                                var nodeIds = reader.GetFieldValue<long[]>(2);

                                wayIds.Add(id);
                                wayTags.Add(thisWayTags);
                                wayNodeIds.Add(nodeIds);
                            }
                        }
                    }
                }
            }

            // get the nodes for the ways
            var idCount = wayIds.Count;
            for (var i = 0; i < idCount; ++i)
            {
                var nodes = GetNodes(wayNodeIds[i]);

                // validate the number of nodes returned is what the way wants
                if (nodes.Count == wayNodeIds[i].Length)
                {
                    var way = new Way(wayIds[i], wayTags[i], nodes);
                    _cache.AddWay(way);
                    ways.Add(way);
                }
                else
                {
                    _failedIdCache.AddWayId(wayIds[i]);
                    Log.Info($"Failed to fetch way: {wayIds[i]}");
                }
            }

            return ways;
        }

        /// <inheritdoc />
        public IList<Relation> GetRelations(IList<long> ids)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(PostgreSQLOsmGeoSource));
            }

            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var idsCount = ids.Count;
            if (idsCount <= 0)
            {
                return new List<Relation>();
            }

            // exclude previously failed relations
            var requestedIds = ids.Remove(_failedIdCache.RelationIdsSet);
            // check the cache for relations we've already got
            var relations = _cache.GetRelations(requestedIds, 
                out IList<long> remainingIds);
            var relationIds = remainingIds.NoDuplicates();

            // get relation members in a deep search, relationIds will be updated
            var members = GetRelationMembers(relationIds);

            // get any member relations from the cache
            var memberRelations = _cache.GetRelationsList(members.Ids.RelationIds);

            // add the member relations found in the to the relations dict
            var memberRelationsCount = memberRelations.Count;
            for (var i = 0; i < memberRelationsCount; i++)
            {
                var memberRelation = memberRelations[i];
                var memberRelationId = memberRelation.Id;

                if (!relations.ContainsKey(memberRelationId))
                {
                    relations.Add(memberRelationId, memberRelation);
                }
            }

            // get the relation tags
            var tags = new Dictionary<long, IDictionary<string, string>>();
            CompleteRelationTags(relationIds, tags);

            // complete the relations
            CompleteRelationsMap(members, relationIds, tags, relations);

            // return the originally requested relations, in the requested order
            var relationCount = requestedIds.Count;
            var finalRelations = new List<Relation>();
            for (var i = 0; i < relationCount; ++i)
            {
                if (relations.TryGetValue(requestedIds[i], out Relation relation))
                {
                    finalRelations.Add(relation);
                }
            }

            return finalRelations;
        }

        /// <inheritdoc />
        public IList<Relation> GetRelations(GeodeticBox2d box, IList<Tuple<string, 
            IList<string>>> tags = null)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(PostgreSQLOsmGeoSource));
            }

            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            var sqlSb = new StringBuilder(SelectRelationsPrefix);
            sqlSb.Append(string.Format(SelectRelationsBboxSuffix, 
                PostgreSQLClient.EnvelopeString(box)));

            // only include tags in the query if we're given them
            if (tags != null)
            {
                sqlSb.Append(AndSnippet);
                sqlSb.Append(TagsString(RelationsSnippet, tags));
            }

            var requestedIds = new List<long>();
            var idsToFetch = new List<long>();
            var relations = new Dictionary<long, Relation>();
            var relationTags = new Dictionary<long, IDictionary<string, string>>();

            using (var command = new NpgsqlCommand(sqlSb.ToString(), _client.Connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    var idsSet = new HashSet<long>();

                    while (reader.Read())
                    {
                        var id = reader.GetInt64(0);

                        if (idsSet.Add(id) && !_failedIdCache.ContainsRelationId(id))
                        {
                            // add to the list of requested ids
                            requestedIds.Add(id);

                            // always attempt to get from the cache
                            if (_cache.TryGetRelation(id, out Relation relation))
                            {
                                relations[id] = relation;
                            }
                            // do not attempt to get previously failed relations
                            else
                            {
                                var thisRelationTags = reader.GetFieldValue<
                                    Dictionary<string, string>>(1);
                                relationTags[id] = thisRelationTags;

                                // add to the list of ids to fetch
                                idsToFetch.Add(id);
                            }
                        }
                    }
                }
            }

            if (idsToFetch.Count > 0)
            {
                // complete the relations map
                CompleteRelationsMap(idsToFetch, relationTags, relations);
            }

            // return only the relations found in the original search
            var idsCount = requestedIds.Count;
            var finalRelations = new List<Relation>();
            for (var i = 0; i < idsCount; ++i)
            {
                if (relations.TryGetValue(requestedIds[i], out Relation relation))
                {
                    finalRelations.Add(relation);
                }
            }

            return finalRelations;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(PostgreSQLOsmGeoSource));
            }

            if (_client != null)
            {
                _client.Dispose();
            }

            _disposed = true;
        }

        /// <inheritdoc />
        public object Clone()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(PostgreSQLOsmGeoSource));
            }

            var clonedClient = _client.Clone();

            if (clonedClient == null)
            {
                throw new InvalidOperationException($"{nameof(PostgreSQLClient)} failed to clone");
            }

            var castedClonedClient = clonedClient as PostgreSQLClient;

            if (castedClonedClient == null)
            {
                throw new InvalidOperationException($"Cloned {nameof(PostgreSQLClient)} not castable to {nameof(PostgreSQLClient)}");
            }

            return new PostgreSQLOsmGeoSource(castedClonedClient, _cache, _failedIdCache);
        }

        private IDictionary<long, Node> GetNodesDictionary(IList<long> ids)
        {
            IList<long> remainingIds;
            var nodesDict = _cache.GetNodes(ids, out remainingIds);
            var uniqueIds = remainingIds.NoDuplicates();
            var uniqueIdsCount = uniqueIds.Count;

            for (var i = 0; i < uniqueIdsCount; i += NodeReadBatchSize)
            {
                var idsListCount = Math.Min(uniqueIdsCount, i + NodeReadBatchSize);

                if (idsListCount > 0)
                {
                    var idsList = DbTools.ConstructIdList(uniqueIds, i, idsListCount);
                    var sql = SelectNodesPrefix + string.Format(SelectIdsSuffix,
                                  NodesSnippet, idsList);

                    using (var command = new NpgsqlCommand(sql, _client.Connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var id = reader.GetInt64(0);

                                if (!nodesDict.ContainsKey(id))
                                {
                                    var tags = reader.GetFieldValue<Dictionary<string, 
                                        string>>(1);
                                    var coordinate = new Geodetic2d(reader.GetDouble(2), 
                                        reader.GetDouble(3));
                                    var node = new Node(id, tags, coordinate);

                                    nodesDict.Add(id, node);
                                    _cache.AddNode(node);
                                }
                            }
                        }
                    }
                }
            }

            return nodesDict;
        }

        private IDictionary<long, Way> GetWaysDictionary(IList<long> ids)
        {
            IList<long> remainingIds;
            var ways = _cache.GetWays(ids, out remainingIds);

            var uniqueIds = remainingIds.NoDuplicates();
            var uniqueIdsCount = uniqueIds.Count;

            var idsSet = new HashSet<long>();
            var wayIds = new List<long>();
            var wayTags = new List<IDictionary<string, string>>();
            var wayNodeIds = new List<long[]>();

            for (var i = 0; i < uniqueIdsCount; i += WayReadBatchSize)
            {
                var idsListCount = Math.Min(uniqueIdsCount, i + WayReadBatchSize);

                if (idsListCount > 0)
                {
                    var idsList = DbTools.ConstructIdList(uniqueIds, i, idsListCount);
                    var sql = SelectWaysPrefix + string.Format(SelectIdsSuffix,
                                  WaysSnippet, idsList);

                    using (var command = new NpgsqlCommand(sql, _client.Connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var id = reader.GetInt64(0);

                                if (idsSet.Add(id))
                                {
                                    var tags = reader.GetFieldValue<Dictionary<string,
                                        string>>(1);
                                    var nodeIds = reader.GetFieldValue<long[]>(2);

                                    wayIds.Add(id);
                                    wayTags.Add(tags);
                                    wayNodeIds.Add(nodeIds);
                                }
                            }
                        }
                    }
                }
            }

            // complete the ways
            CompleteWaysMap(wayIds, wayNodeIds, wayTags, ways);

            return ways;
        }

        private void CompleteWaysMap(IList<long> wayIds, IList<long[]> wayNodeIds, 
            IList<IDictionary<string, string>> wayTags, IDictionary<long, Way> destination)
        {
            var wayCount = wayIds.Count;

            for (var i = 0; i < wayCount; ++i)
            {
                // get the nodes for the way
                var nodes = GetNodes(wayNodeIds[i]);

                if (nodes.Count == wayNodeIds[i].Length)
                {
                    var id = wayIds[i];
                    var way = new Way(id, wayTags[i], nodes);
                    destination.Add(id, way);
                    _cache.AddWay(way);
                }
            }
        }

        private void CompleteRelationTags(IList<long> ids,
            IDictionary<long, IDictionary<string, string>> destination)
        {
            var idsCount = ids.Count;

            for (var i = 0; i < idsCount; i += RelationReadBatchSize)
            {
                var idsListCount = Math.Min(idsCount, i + RelationReadBatchSize);

                if (idsListCount > 0)
                {
                    var idsList = DbTools.ConstructIdList(ids, i, idsListCount);
                    var sql = SelectRelationsPrefix + string.Format(SelectIdsSuffix,
                                  RelationsSnippet, idsList);

                    using (var command = new NpgsqlCommand(sql, _client.Connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var id = reader.GetInt64(0);

                                // we don't want to fetch a relation twice
                                if (!destination.ContainsKey(id))
                                {
                                    destination.Add(id, reader.GetFieldValue<
                                        Dictionary<string, string>>(1));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CompleteRelationsMap(IList<long> ids, IDictionary<long,
            IDictionary<string, string>> tags, IDictionary<long, Relation> destination)
        {
            // get relation members
            var members = GetRelationMembers(ids);

            // get any member relations from the cache
            var memberRelations = _cache.GetRelationsList(members.Ids.RelationIds,
                out IList<long> remainingRelationIds);

            // add the member relations found in the to the relations dict
            var memberRelationsCount = memberRelations.Count;
            for (var i = 0; i < memberRelationsCount; ++i)
            {
                var memberRelation = memberRelations[i];
                var memberRelationId = memberRelation.Id;

                if (!destination.ContainsKey(memberRelationId))
                {
                    destination.Add(memberRelationId, memberRelation);
                }
            }

            // get the tags for remaining member relations
            CompleteRelationTags(remainingRelationIds, tags);

            // complete the relations map with members
            CompleteRelationsMap(members, ids, tags, destination);
        }

        private void CompleteRelationsMap(IRelationMemberCollection members, 
            IList<long> ids, IDictionary<long, IDictionary<string, string>> tags,
            IDictionary<long, Relation> destination)
        {
            // get the node members
            var nodes = GetNodesDictionary(members.Ids.NodeIds);
            // get the way members
            var ways = GetWaysDictionary(members.Ids.WayIds);

            // the dictionary of relations waiting on completion
            var waiting = new Dictionary<long, IList<RelationMember>>();
            // the dictionary of relations depending on relations
            var dependencies = new Dictionary<long, IList<long>>();
            // the set of failed relations
            var failed = new HashSet<long>();

            // create relations
            var idsCount = ids.Count;
            for (var i = 0; i < idsCount; ++i)
            {
                var id = ids[i];
                if (destination.ContainsKey(id))
                {
                    continue;
                }

                // the tags, members and roles
                IDictionary<string, string> createTags;

                // get tags
                if (tags.ContainsKey(id))
                {
                    createTags = tags[id];
                }
                else // no tags, weird but okay
                {
                    createTags = new Dictionary<string, string>();
                }

                // get members
                if (members.HasRelation(id))
                {
                    var membersToGet = members.MembersFor(id);
                    var membersToGetCount = membersToGet.Count;

                    var createMembers = new OsmGeo[membersToGetCount];
                    var createRoles = new string[membersToGetCount];

                    // go through members
                    var fail = false;
                    for (var j = 0; j < membersToGetCount && !fail; ++j)
                    {
                        // determine the member type
                        var member = membersToGet[j];
                        switch (member.Type)
                        {
                            case OsmGeoType.Node:
                                if (nodes.ContainsKey(member.Id))
                                {
                                    createMembers[member.Sequence] = nodes[member.Id];
                                }
                                else
                                {
                                    fail = true;
                                }
                                break;
                            case OsmGeoType.Way:
                                if (ways.ContainsKey(member.Id))
                                {
                                    createMembers[member.Sequence] = ways[member.Id];
                                }
                                else
                                {
                                    fail = true;
                                }
                                break;
                            case OsmGeoType.Relation:

                                // update the relation dependency map
                                if (!dependencies.ContainsKey(member.Id))
                                {
                                    dependencies.Add(member.Id, new List<long>());
                                }

                                dependencies[member.Id].Add(id);

                                // if we've already got the relation
                                if (destination.ContainsKey(member.Id))
                                {
                                    createMembers[member.Sequence] = destination[member.Id];
                                }
                                else
                                {
                                    // this relation is now waiting to be completed
                                    if (!waiting.ContainsKey(id))
                                    {
                                        waiting.Add(id, new List<RelationMember>());
                                    }

                                    // mark what it's waiting on
                                    waiting[id].Add(member);
                                }
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        // set the roles
                        createRoles[j] = member.Role;
                    }

                    // if failed, mark it as so
                    if (fail)
                    {
                        failed.Add(id);
                        // remove from waiting dict
                        waiting.Remove(id);
                    }
                    else
                    {
                        // create the relation and add to the dict
                        var relation = new Relation(id, createTags, createMembers, createRoles);
                        destination.Add(id, relation);
                    }
                }
                // relation has no members, invalid
                else
                {
                    failed.Add(id);
                }
            }

            // check if which relations are waiting on completion
            if (waiting.Count > 0)
            {
                foreach (var waiter in waiting)
                {
                    var fail = false;

                    // go through every this relation is waiting on
                    var waitingCount = waiter.Value.Count;
                    for (var i = 0; i < waitingCount && !fail; ++i)
                    {
                        var waited = waiter.Value[i];

                        // we can only be waiting on other relations
                        if (destination.ContainsKey(waited.Id))
                        {
                            // set the member on the relation
                            destination[waiter.Key].SetMember(waited.Sequence,
                                destination[waited.Id]);
                        }
                        else
                        {
                            // if the waited relation does not exist, mark as failed 
                            failed.Add(waiter.Key);
                            fail = true;
                        }
                    }
                }
            }

            // for failed relations, we mark their dependents as failed
            if (failed.Count > 0)
            {
                var nextFailed = new HashSet<long>();

                foreach (var id in failed)
                {
                    // there are any dependents for this relation
                    if (dependencies.ContainsKey(id))
                    {
                        var dependentsCount = dependencies[id].Count;
                        for (var i = 0; i < dependentsCount; ++i)
                        {
                            // add to the next failed set
                            nextFailed.Add(dependencies[id][i]);
                        }
                    }
                }

                // move up the dependency tree
                while (nextFailed.Count > 0)
                {
                    var nextNextFailed = new HashSet<long>();

                    foreach (var id in nextFailed)
                    {
                        // there are any dependents for this relation
                        if (dependencies.ContainsKey(id))
                        {
                            var dependentsCount = dependencies[id].Count;
                            for (var i = 0; i < dependentsCount; ++i)
                            {
                                var failedId = dependencies[id][i];

                                // add to the next failed set
                                nextNextFailed.Add(failedId);
                                // add to the overall failed set
                                failed.Add(failedId);
                            }
                        }
                    }

                    nextFailed = nextNextFailed;
                }
            }

            // filter out failed relations
            for (var i = 0; i < idsCount; ++i)
            {
                var id = ids[i];
                if (failed.Contains(id))
                {
                    // add to the failed relation cache
                    _failedIdCache.AddRelationId(id);
                    Log.Info($"Failed to fetch relation: {id}");
                    // remove from the returned dictionary
                    destination.Remove(id);
                }
                else
                {
                    // successful relations are added to the cache
                    _cache.AddRelation(destination[id]);
                }
            }
        }

        private IRelationMemberCollection GetRelationMembers(IList<long> ids)
        {
            // get members for the relations
            var members = GetRelationMembersShallow(ids);

            // if there are any relation members
            if (members.Ids.RelationCount > 0)
            {
                // mark relations we've already tried to get members for
                var searchedMap = new HashSet<long>();
                for (var i = 0; i < ids.Count; ++i)
                {
                    searchedMap.Add(ids[i]);
                }

                /* search for members of relations returned in the member search
                 * excluding relations that we've already tried to get members for */
                var nextSearchIds = new List<long>();
                for (var i = 0; i < members.Ids.RelationCount; ++i)
                {
                    var relationId = members.Ids.RelationIds[i];
                    if (!searchedMap.Contains(relationId))
                    {
                        // add the relation id to the next relation search
                        nextSearchIds.Add(relationId);
                        // add the relation id to the overall relation search
                        ids.Add(relationId);
                        // mark it as searched
                        searchedMap.Add(relationId);
                    }
                }

                // while we need to keep getting relation members
                while (nextSearchIds.Count > 0)
                {
                    // get the new members
                    var nextMembers = GetRelationMembersShallow(nextSearchIds);

                    /* search for members of relations returned in the member search
                     * excluding relations that we've already tried to get members for */
                    nextSearchIds = new List<long>();
                    for (var i = 0; i < nextMembers.Ids.RelationCount; ++i)
                    {
                        var relationId = nextMembers.Ids.RelationIds[i];
                        if (!searchedMap.Contains(relationId))
                        {
                            // add the relation id to the search
                            nextSearchIds.Add(relationId);
                            // add the relation id to the overall relation search
                            ids.Add(relationId);
                            // mark it as searched
                            searchedMap.Add(relationId);
                        }
                    }

                    // append the next members to the overall members
                    members.Append(nextMembers);
                }
            }

            return members;
        }

        private IRelationMemberCollection GetRelationMembersShallow(IList<long> ids)
        {
            IRelationMemberCollection members = new RelationMemberCollection();

            var idsCount = ids.Count;
            for (var i = 0; i < idsCount; i += RelationMemberReadBatchSize)
            {
                var idsListCount = Math.Min(idsCount, i + RelationMemberReadBatchSize);
                if (idsListCount > 0)
                {
                    var idsList = DbTools.ConstructIdList(ids, i, idsListCount);
                    var sql = string.Format(SelectRelationMembers, idsList);

                    using (var command = new NpgsqlCommand(sql, _client.Connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OsmGeoType type;

                                switch (reader.GetString(2))
                                {
                                    // member is a node
                                    case NodeMemberSnippet:
                                        type = OsmGeoType.Node;
                                        break;
                                    // member is a way
                                    case WayMemberSnippet:
                                        type = OsmGeoType.Way;
                                        break;
                                    // member is a relation
                                    case RelationMemberSnippet:
                                        type = OsmGeoType.Relation;
                                        break;
                                    default:
                                        throw new InvalidOperationException();
                                }

                                members.Add(reader.GetInt64(0), 
                                    new RelationMember(type, reader.GetInt64(1),
                                        reader.GetString(3), reader.GetInt32(4)));
                            }
                        }
                    }
                }
            }

            return members;
        }

        private string TagsString(string table, IList<Tuple<string, IList<string>>> tags)
        {
            var sb = new StringBuilder(@"(");

            var tagsCount = tags.Count;
            for (var i = 0; i < tagsCount; i++)
            {
                var tag = tags[i];
                var tagKey = tag.Item1;
                var values = tag.Item2;
                var valueCount = values.Count;

                // account for no provided values equaling a wildcard
                if (valueCount == 0)
                {
                    sb.Append(string.Format(SelectTagsExistSuffix, table, tagKey));
                }
                else
                {
                    for (var j = 0; j < valueCount; j++)
                    {
                        var tagValue = values[j];

                        // empty values equal a wildcard
                        if (string.IsNullOrWhiteSpace(tagValue))
                        {
                            sb.Append(string.Format(SelectTagsExistSuffix, table, tagKey));
                        }
                        else
                        {
                            sb.Append(string.Format(SelectTagsSuffix, table, tagKey, tagValue));
                        }

                        if (valueCount > 1 && j < valueCount - 1)
                        {
                            sb.Append(OrSnippet);
                        }
                    }
                }

                if (tagsCount > 1 && i < tagsCount - 1)
                {
                    sb.Append(OrSnippet);
                }
            }

            sb.Append(@")");

            return sb.ToString();
        }
    }
}