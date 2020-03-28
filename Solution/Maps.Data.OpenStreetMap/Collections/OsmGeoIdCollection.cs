using System;
using System.Collections.Generic;

namespace Maps.Data.OpenStreetMap.Collections
{
    /// <summary>
    /// An in-memory, thread safe collection for OpenStreetMap geometry ids
    /// </summary>
    internal class OsmGeoIdCollection : IOsmGeoIdCollection
    {
        /// <inheritdoc />
        public int NodeCount => NodeIdsSet.Count;

        /// <inheritdoc />
        public int WayCount => WayIdsSet.Count;

        /// <inheritdoc />
        public int RelationCount => RelationIdsSet.Count;

        /// <inheritdoc />
        public IList<long> NodeIds
        {
            get;
        }

        /// <inheritdoc />
        public IList<long> WayIds
        {
            get;
        }

        /// <inheritdoc />
        public IList<long> RelationIds
        {
            get;
        }

        /// <inheritdoc />
        public ISet<long> NodeIdsSet
        {
            get;
        }

        /// <inheritdoc />
        public ISet<long> WayIdsSet
        {
            get;
        }

        /// <inheritdoc />
        public ISet<long> RelationIdsSet
        {
            get;
        }

        private readonly object _nodesLock;
        private readonly object _waysLock;
        private readonly object _relationsLock;

        /// <summary>
        /// Initializes a new instance of OsmGeoIdCollection
        /// </summary>
        public OsmGeoIdCollection()
        {
            NodeIdsSet = new HashSet<long>();
            WayIdsSet = new HashSet<long>();
            RelationIdsSet = new HashSet<long>();

            NodeIds = new List<long>();
            WayIds = new List<long>();
            RelationIds = new List<long>();

            _nodesLock = new object();
            _waysLock = new object();
            _relationsLock = new object();
        }

        /// <inheritdoc />
        public void AddId(long id, OsmGeoType type)
        {
            switch(type)
            {
                case OsmGeoType.Node:
                    AddNodeId(id);
                    break;
                case OsmGeoType.Way:
                    AddWayId(id);
                    break;
                case OsmGeoType.Relation:
                    AddRelationId(id);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        /// <inheritdoc />
        public void AddNodeId(long id)
        {
            lock (_nodesLock)
            {
                AddNodeIdImpl(id);
            }
        }

        /// <inheritdoc />
        public void AddNodeIds(IList<long> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var idsCount = ids.Count;

            lock (_nodesLock)
            {
                for (var i = 0; i < idsCount; ++i)
                {
                    AddNodeIdImpl(ids[i]);
                }
            }
        }

        /// <inheritdoc />
        public bool RemoveNodeId(long id)
        {
            lock (_nodesLock)
            {
                if (NodeIdsSet.Remove(id))
                {
                    NodeIds.Remove(id);
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public bool ContainsNodeId(long id)
        {
            lock (_nodesLock)
            {
                return NodeIdsSet.Contains(id);
            }
        }

        /// <inheritdoc />
        public void AddWayId(long id)
        {
            lock (_waysLock)
            {
                AddWayIdImpl(id);
            }
        }

        /// <inheritdoc />
        public void AddWayIds(IList<long> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var idsCount = ids.Count;

            lock (_waysLock)
            {
                for (var i = 0; i < idsCount; i++)
                {
                    AddWayIdImpl(ids[i]);
                }
            }
        }

        /// <inheritdoc />
        public bool RemoveWayId(long id)
        {
            lock (_waysLock)
            {
                if (WayIdsSet.Remove(id))
                {
                    WayIds.Remove(id);
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public bool ContainsWayId(long id)
        {
            lock (_waysLock)
            {
                return WayIdsSet.Contains(id);
            }
        }

        /// <inheritdoc />
        public void AddRelationId(long id)
        {
            lock (_relationsLock)
            {
                AddRelationIdImpl(id);
            }
        }

        /// <inheritdoc />
        public void AddRelationIds(IList<long> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            lock (_relationsLock)
            {
                var idsCount = ids.Count;
                for (var i = 0; i < idsCount; i++)
                {
                    AddRelationIdImpl(ids[i]);
                }
            }
        }

        /// <inheritdoc />
        public bool RemoveRelationId(long id)
        {
            lock (_relationsLock)
            {
                if (RelationIdsSet.Remove(id))
                {
                    RelationIds.Remove(id);
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public bool ContainsRelationId(long id)
        {
            lock (_relationsLock)
            {
                return RelationIdsSet.Contains(id);
            }
        }

        /// <inheritdoc />
        public void Clear()
        {
            lock (_nodesLock)
            {
                NodeIdsSet.Clear();
            }

            lock (_waysLock)
            {
                WayIdsSet.Clear();
            }

            lock (_relationsLock)
            {
                RelationIdsSet.Clear();
            }
        }

        private void AddNodeIdImpl(long id)
        {
            if (NodeIdsSet.Add(id))
            {
                NodeIds.Add(id);
            }
        }

        private void AddWayIdImpl(long id)
        {
            if (WayIdsSet.Add(id))
            {
                WayIds.Add(id);
            }
        }

        private void AddRelationIdImpl(long id)
        {
            if (RelationIdsSet.Add(id))
            {
                RelationIds.Add(id);
            }
        }
    }
}