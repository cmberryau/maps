using System;
using System.Collections.Generic;

namespace Maps.Data.OpenStreetMap.Collections
{
    /// <summary>
    /// A in-memory, thread safe collection for OpenStreetMap geometries
    /// </summary>
    internal class OsmGeoCollection : IOsmGeoCollection
    {
        /// <inheritdoc />
        public int NodeCount => _nodes.Count;

        /// <inheritdoc />
        public int WayCount => _ways.Count;

        /// <inheritdoc />
        public int RelationCount => _relations.Count;

        private const int MaxNodes = (int) 10e4;
        private const int MaxWays = (int) 10e4;
        private const int MaxRelations = (int) 10e4;

        private const int MaxNodeQueries = (int) 10e5;
        private const int MaxWayQueries = (int) 10e5;
        private const int MaxRelationQueries = (int) 10e5;

        private const int DefaultNodeFlush = (int) 10e3;
        private const int DefaultWayFlush = (int) 10e3;
        private const int DefaultRelationFlush = (int) 10e3;

        private readonly IDictionary<long, Node> _nodes;
        private readonly IDictionary<long, Way> _ways;
        private readonly IDictionary<long, Relation> _relations;

        private readonly Queue<long> _nodeQueries;
        private readonly Queue<long> _wayQueries;
        private readonly Queue<long> _relationQueries;

        private readonly object _nodesLock;
        private readonly object _waysLock;
        private readonly object _relationsLock;

        /// <summary>
        /// Initializes a new instance of OsmGeoCollection
        /// </summary>
        public OsmGeoCollection()
        {
            _nodes = new Dictionary<long, Node>();
            _ways = new Dictionary<long, Way>();
            _relations = new Dictionary<long, Relation>();

            _nodeQueries = new Queue<long>();
            _wayQueries = new Queue<long>();
            _relationQueries = new Queue<long>();

            _nodesLock = new object();
            _waysLock = new object();
            _relationsLock = new object();
        }

        /// <inheritdoc />
        public void AddNode(Node node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            lock (_nodesLock)
            {
                AddNodeImpl(node);
            }
        }

        /// <inheritdoc />
        public void AddNodes(IList<Node> nodes)
        {
            if (nodes == null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            lock (_nodesLock)
            {
                var nodeCount = nodes.Count;
                for (var i = 0; i < nodeCount; i++)
                {
                    AddNodeImpl(nodes[i]);
                }
            }
        }

        /// <inheritdoc />
        public void AddNodes(IDictionary<long, Node> nodes)
        {
            if (nodes == null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            lock (_nodesLock)
            {
                foreach (var node in nodes)
                {
                    AddNodeImpl(node.Value);
                }
            }
        }

        /// <inheritdoc />
        public IDictionary<long, Node> GetNodes(IList<long> ids, 
            out IList<long> unfetchedIds)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var nodes = new Dictionary<long, Node>();
            unfetchedIds = new List<long>();
            var idsCount = ids.Count;

            lock (_nodesLock)
            {
                for (var i = 0; i < idsCount; ++i)
                {
                    var id = ids[i];
                    if (_nodes.TryGetValue(id, out Node node))
                    {
                        // use map to prevent query doubleups
                        if (!nodes.ContainsKey(id))
                        {
                            OnNodeQuery(id);
                        }

                        nodes[id] = node;
                    }
                    else
                    {
                        unfetchedIds.Add(id);
                    }
                }
            }

            return nodes;
        }

        /// <inheritdoc />
        public IDictionary<long, Node> GetNodes(IList<long> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var nodes = new Dictionary<long, Node>();
            var idsCount = ids.Count;

            lock (_nodesLock)
            {
                for (var i = 0; i < idsCount; ++i)
                {
                    var id = ids[i];
                    if (_nodes.TryGetValue(id, out Node node))
                    {
                        // use map to prevent query doubleups
                        if (!nodes.ContainsKey(id))
                        {
                            OnNodeQuery(id);
                        }

                        nodes[id] = node;
                    }
                }
            }

            return nodes;
        }

        /// <inheritdoc />
        public IList<Node> GetNodesList(IList<long> ids, out IList<long> unfetchedIds)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var nodesList = new List<Node>();
            var nodesSet = new HashSet<Node>();
            unfetchedIds = new List<long>();
            var idsCount = ids.Count;

            lock (_nodesLock)
            {
                for (var i = 0; i < idsCount; ++i)
                {
                    var id = ids[i];
                    if (_nodes.TryGetValue(id, out Node node))
                    {
                        // use map to prevent query doubleups
                        if (nodesSet.Add(node))
                        {
                            OnNodeQuery(id);
                        }

                        nodesList.Add(node);
                    }
                    else
                    {
                        unfetchedIds.Add(id);
                    }
                }
            }

            return nodesList;
        }

        /// <inheritdoc />
        public IList<Node> GetNodesList(IList<long> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var nodesList = new List<Node>();
            var nodesSet = new HashSet<Node>();
            var idsCount = ids.Count;

            lock (_nodesLock)
            {
                for (var i = 0; i < idsCount; ++i)
                {
                    var id = ids[i];
                    if (_nodes.TryGetValue(id, out Node node))
                    {
                        // use map to prevent query doubleups
                        if (nodesSet.Add(node))
                        {
                            OnNodeQuery(id);
                        }

                        nodesList.Add(node);
                    }
                }
            }

            return nodesList;
        }

        /// <inheritdoc />
        public bool RemoveNode(long id)
        {
            lock (_nodesLock)
            {
                return _nodes.Remove(id);
            }
        }

        /// <inheritdoc />
        public bool ContainsNode(long id)
        {
            lock (_nodesLock)
            {
                return _nodes.ContainsKey(id);
            }
        }

        /// <inheritdoc />
        public bool TryGetNode(long id, out Node node)
        {
            lock (_nodesLock)
            {
                if (_nodes.TryGetValue(id, out node))
                {
                    OnNodeQuery(id);
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public void AddWay(Way way)
        {
            if (way == null)
            {
                throw new ArgumentNullException(nameof(way));
            }

            lock (_waysLock)
            {
                AddWayImpl(way);
            }
        }

        /// <inheritdoc />
        public void AddWays(IList<Way> ways)
        {
            if (ways == null)
            {
                throw new ArgumentNullException(nameof(ways));
            }

            lock (_waysLock)
            {
                var waysCount = ways.Count;
                for (var i = 0; i < waysCount; i++)
                {
                    AddWayImpl(ways[i]);
                }
            }
        }

        /// <inheritdoc />
        public void AddWays(IDictionary<long, Way> ways)
        {
            if (ways == null)
            {
                throw new ArgumentNullException(nameof(ways));
            }

            lock (_waysLock)
            {
                foreach (var way in ways)
                {
                    AddWayImpl(way.Value);
                }
            }
        }

        /// <inheritdoc />
        public IDictionary<long, Way> GetWays(IList<long> ids, 
            out IList<long> unfetchedIds)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var ways = new Dictionary<long, Way>();
            unfetchedIds = new List<long>();
            var idsCount = ids.Count;

            lock (_waysLock)
            {
                for (var i = 0; i < idsCount; ++i)
                {
                    var id = ids[i];
                    if (_ways.TryGetValue(id, out Way way))
                    {
                        // use map to prevent query doubleups
                        if (!ways.ContainsKey(id))
                        {
                            OnWayQuery(id);
                        }

                        ways[id] = way;
                    }
                    else
                    {
                        unfetchedIds.Add(id);
                    }
                }
            }

            return ways;
        }

        /// <inheritdoc />
        public IDictionary<long, Way> GetWays(IList<long> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var ways = new Dictionary<long, Way>();
            var idsCount = ids.Count;

            lock (_waysLock)
            {
                for (var i = 0; i < idsCount; ++i)
                {
                    var id = ids[i];
                    if (_ways.TryGetValue(id, out Way way))
                    {
                        // use map to prevent query doubleups
                        if (!ways.ContainsKey(id))
                        {
                            OnWayQuery(id);
                        }

                        ways[id] = way;
                    }
                }
            }

            return ways;
        }

        /// <inheritdoc />
        public IList<Way> GetWaysList(IList<long> ids, out IList<long> unfetchedIds)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var waysList = new List<Way>();
            var waysSet = new HashSet<Way>();
            unfetchedIds = new List<long>();
            var idsCount = ids.Count;

            lock (_waysLock)
            {
                for (var i = 0; i < idsCount; ++i)
                {
                    var id = ids[i];
                    if (_ways.TryGetValue(id, out Way way))
                    {
                        // use map to prevent query doubleups
                        if (waysSet.Add(way))
                        {
                            OnWayQuery(id);
                        }

                        waysList.Add(way);
                    }
                    else
                    {
                        unfetchedIds.Add(id);
                    }
                }
            }

            return waysList;
        }

        /// <inheritdoc />
        public IList<Way> GetWaysList(IList<long> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var waysList = new List<Way>();
            var waysSet = new HashSet<Way>();
            var idsCount = ids.Count;

            lock (_waysLock)
            {
                for (var i = 0; i < idsCount; ++i)
                {
                    var id = ids[i];
                    if (_ways.TryGetValue(id, out Way way))
                    {
                        // use map to prevent query doubleups
                        if (waysSet.Add(way))
                        {
                            OnWayQuery(id);
                        }

                        waysList.Add(way);
                    }
                }
            }

            return waysList;
        }

        /// <inheritdoc />
        public bool RemoveWay(long id)
        {
            lock (_waysLock)
            {
                return _ways.Remove(id);
            }
        }

        /// <inheritdoc />
        public bool ContainsWay(long id)
        {
            lock (_waysLock)
            {
                return _ways.ContainsKey(id);
            }
        }

        /// <inheritdoc />
        public bool TryGetWay(long id, out Way way)
        {
            lock (_waysLock)
            {
                if (_ways.TryGetValue(id, out way))
                {
                    OnWayQuery(id);
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public void AddRelation(Relation relation)
        {
            if (relation == null)
            {
                throw new ArgumentNullException(nameof(relation));
            }

            lock (_relationsLock)
            {
                AddRelationImpl(relation);
            }
        }

        /// <inheritdoc />
        public void AddRelations(IList<Relation> relations)
        {
            if (relations == null)
            {
                throw new ArgumentNullException(nameof(relations));
            }

            var relationsCount = relations.Count;

            lock (_relationsLock)
            {
                for (var i = 0; i < relationsCount; i++)
                {
                    AddRelationImpl(relations[i]);
                }
            }
        }

        /// <inheritdoc />
        public void AddRelations(IDictionary<long, Relation> relations)
        {
            if (relations == null)
            {
                throw new ArgumentNullException(nameof(relations));
            }

            lock (_relationsLock)
            {
                foreach (var relation in relations)
                {
                    AddRelationImpl(relation.Value);
                }
            }
        }

        /// <inheritdoc />
        public IDictionary<long, Relation> GetRelations(IList<long> ids, 
            out IList<long> unfetchedIds)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            return GetRelations(ids as IReadOnlyList<long>, out unfetchedIds);
        }

        /// <inheritdoc />
        public IDictionary<long, Relation> GetRelations(IReadOnlyList<long> ids, 
            out IList<long> unfetchedIds)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var relations = new Dictionary<long, Relation>();
            unfetchedIds = new List<long>();
            var idsCount = ids.Count;

            lock (_relationsLock)
            {
                for (var i = 0; i < idsCount; ++i)
                {
                    var id = ids[i];
                    if (_relations.TryGetValue(id, out Relation relation))
                    {
                        // use map to prevent query doubleups
                        if (!relations.ContainsKey(id))
                        {
                            OnRelationQuery(id);
                        }

                        relations[id] = relation;
                    }
                    else
                    {
                        unfetchedIds.Add(id);
                    }
                }
            }

            return relations;
        }

        /// <inheritdoc />
        public IDictionary<long, Relation> GetRelations(IList<long> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            return GetRelations(ids as IReadOnlyList<long>);
        }

        /// <inheritdoc />
        public IDictionary<long, Relation> GetRelations(IReadOnlyList<long> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var relations = new Dictionary<long, Relation>();
            var idsCount = ids.Count;

            lock (_relationsLock)
            {
                for (var i = 0; i < idsCount; ++i)
                {
                    var id = ids[i];
                    if (_relations.TryGetValue(id, out Relation relation))
                    {
                        // use map to prevent query doubleups
                        if (!relations.ContainsKey(id))
                        {
                            OnRelationQuery(id);
                        }

                        relations[id] = relation;
                    }
                }
            }

            return relations;
        }

        /// <inheritdoc />
        public IList<Relation> GetRelationsList(IList<long> ids, 
            out IList<long> unfetchedIds)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var relationsList = new List<Relation>();
            var relationsSet = new HashSet<Relation>();
            unfetchedIds = new List<long>();
            var idsCount = ids.Count;

            lock (_relationsLock)
            {
                for (var i = 0; i < idsCount; ++i)
                {
                    var id = ids[i];
                    if (_relations.TryGetValue(id, out Relation relation))
                    {
                        // use map to prevent query doubleups
                        if (relationsSet.Add(relation))
                        {
                            OnRelationQuery(id);
                        }

                        relationsList.Add(relation);
                    }
                    else
                    {
                        unfetchedIds.Add(id);
                    }
                }
            }

            return relationsList;
        }

        /// <inheritdoc />
        public IList<Relation> GetRelationsList(IReadOnlyList<long> ids, 
            out IList<long> unfetchedIds)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IList<Relation> GetRelationsList(IList<long> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var relationsList = new List<Relation>();
            var relationsSet = new HashSet<Relation>();
            var idsCount = ids.Count;

            lock (_relationsLock)
            {
                for (var i = 0; i < idsCount; ++i)
                {
                    var id = ids[i];
                    if (_relations.TryGetValue(id, out Relation relation))
                    {
                        if (relationsSet.Add(relation))
                        {
                            // use map to prevent query doubleups
                            OnRelationQuery(id);
                        }

                        relationsList.Add(relation);
                    }
                }
            }

            return relationsList;
        }

        /// <inheritdoc />
        public IList<Relation> GetRelationsList(IReadOnlyList<long> ids)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool RemoveRelation(long id)
        {
            lock (_relationsLock)
            {
                return _relations.Remove(id);
            }
        }

        /// <inheritdoc />
        public bool ContainsRelation(long id)
        {
            lock (_relationsLock)
            {
                return _relations.ContainsKey(id);
            }
        }

        /// <inheritdoc />
        public bool TryGetRelation(long id, out Relation relation)
        {
            lock (_relationsLock)
            {
                if (_relations.TryGetValue(id, out relation))
                {
                    OnRelationQuery(id);
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public void Clear()
        {
            lock (_nodesLock)
            {
                _nodes.Clear();
            }

            lock (_waysLock)
            {
                _ways.Clear();
            }

            lock (_relationsLock)
            {
                _relations.Clear();
            }
        }

        private void AddNodeImpl(Node node)
        {
            var id = node.Id;
            if (!_nodes.ContainsKey(id))
            {
                _nodes[id] = node;
                OnNodeQuery(id);

                if (_nodes.Count >= MaxNodes)
                {
                    var remove = _nodeQueries.Dequeue();

                    while (!_nodes.Remove(remove))
                    {
                        remove = _nodeQueries.Dequeue();
                    }
                }
            }
        }

        private void AddWayImpl(Way way)
        {
            var id = way.Id;
            if (!_ways.ContainsKey(id))
            {
                _ways[id] = way;
                OnWayQuery(id);

                if (_ways.Count >= MaxWays)
                {
                    var remove = _wayQueries.Dequeue();

                    while (!_ways.Remove(remove))
                    {
                        remove = _wayQueries.Dequeue();
                    }
                }
            }
        }

        private void AddRelationImpl(Relation relation)
        {
            var id = relation.Id;
            if (!_relations.ContainsKey(id))
            {
                _relations[id] = relation;
                OnRelationQuery(id);

                if (_relations.Count >= MaxRelations)
                {
                    var remove = _relationQueries.Dequeue();

                    while (!_relations.Remove(remove))
                    {
                        remove = _relationQueries.Dequeue();
                    }
                }
            }
        }

        private void OnNodeQuery(long id)
        {
            _nodeQueries.Enqueue(id);
            FlushNodes();
        }

        private void FlushNodes()
        {
            if (_nodeQueries.Count >= MaxNodeQueries || _nodes.Count >= MaxNodes)
            {
                var flushCount = Math.Min(_nodeQueries.Count, DefaultNodeFlush);

                for (var i = 0; i < flushCount; ++i)
                {
                    var id = _nodeQueries.Dequeue();

                    if (_nodes.Count > 0)
                    {
                        _nodes.Remove(id);
                    }
                }
            }
        }

        private void OnWayQuery(long id)
        {
            _wayQueries.Enqueue(id);
            FlushWays();
        }

        private void FlushWays()
        {
            if (_wayQueries.Count >= MaxWayQueries || _nodes.Count >= MaxWays)
            {
                var flushCount = Math.Min(_wayQueries.Count, DefaultWayFlush);

                for (var i = 0; i < flushCount; ++i)
                {
                    var id = _wayQueries.Dequeue();

                    if (_ways.Count > 0)
                    {
                        _ways.Remove(id);
                    }
                }
            }
        }

        private void OnRelationQuery(long id)
        {
            _relationQueries.Enqueue(id);
            FlushRelations();
        }

        private void FlushRelations()
        {
            if (_relationQueries.Count >= MaxRelationQueries ||
                _relations.Count >= MaxRelations)
            {
                var flushCount = Math.Min(_relationQueries.Count, DefaultRelationFlush);

                for (var i = 0; i < flushCount; ++i)
                {
                    var id = _relationQueries.Dequeue();

                    if (_relations.Count > 0)
                    {
                        _relations.Remove(id);
                    }
                }
            }
        }
    }
}