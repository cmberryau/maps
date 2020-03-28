using System.Collections.Generic;

namespace Maps.Data.OpenStreetMap.Collections
{
    /// <summary>
    /// An interface for OpenStreetMap geometry id collection implementers
    /// </summary>
    internal interface IOsmGeoIdCollection
    {
        /// <summary>
        /// The number of node ids in the collection
        /// </summary>
        int NodeCount
        {
            get;
        }

        /// <summary>
        /// The number of way ids in the collection
        /// </summary>
        int WayCount
        {
            get;
        }

        /// <summary>
        /// The number of relation ids in the collection
        /// </summary>
        int RelationCount
        {
            get;
        }

        /// <summary>
        /// The node ids in the collection
        /// </summary>
        IList<long> NodeIds
        {
            get;
        }

        /// <summary>
        /// The way ids in the collection
        /// </summary>
        IList<long> WayIds
        {
            get;
        }

        /// <summary>
        /// The relation ids in the collection
        /// </summary>
        IList<long> RelationIds
        {
            get;
        }

        /// <summary>
        /// The node ids set in the collection
        /// </summary>
        ISet<long> NodeIdsSet
        {
            get;
        }

        /// <summary>
        /// The way ids set in the collection
        /// </summary>
        ISet<long> WayIdsSet
        {
            get;
        }

        /// <summary>
        /// The relation set ids in the collection
        /// </summary>
        ISet<long> RelationIdsSet
        {
            get;
        }

        /// <summary>
        /// Adds a geometry id to the collection
        /// </summary>
        /// <param name="id">The id to add</param>
        /// <param name="type">The geometry type</param>
        void AddId(long id, OsmGeoType type);

        /// <summary>
        /// Adds a node id to the collection
        /// </summary>
        /// <param name="id">The id to add</param>
        void AddNodeId(long id);

        /// <summary>
        /// Adds a list of node ids to the collection
        /// </summary>
        /// <param name="ids">The ids to add</param>
        void AddNodeIds(IList<long> ids);

        /// <summary>
        /// Removes a node id from the collection
        /// </summary>
        /// <param name="id">The id to remove</param>
        bool RemoveNodeId(long id);

        /// <summary>
        /// Retruns true if the node id is in the collection
        /// </summary>
        /// <param name="id">The id to evaluate</param>
        bool ContainsNodeId(long id);

        /// <summary>
        /// Adds a way id to the collection
        /// </summary>
        /// <param name="id">The id to add</param>
        void AddWayId(long id);

        /// <summary>
        /// Adds a list of way ids to the collection
        /// </summary>
        /// <param name="ids">The ids to add</param>
        void AddWayIds(IList<long> ids);

        /// <summary>
        /// Removes a way id from the collection
        /// </summary>
        /// <param name="id">The id to remove</param>
        bool RemoveWayId(long id);

        /// <summary>
        /// Retruns true if the way id is in the collection
        /// </summary>
        /// <param name="id">The id to evaluate</param>
        bool ContainsWayId(long id);

        /// <summary>
        /// Adds a relation id to the collection
        /// </summary>
        /// <param name="id">The id to add</param>
        void AddRelationId(long id);

        /// <summary>
        /// Adds a list of relation ids to the collection
        /// </summary>
        /// <param name="ids">The ids to add</param>
        void AddRelationIds(IList<long> ids);

        /// <summary>
        /// Removes a relation id from the collection
        /// </summary>
        /// <param name="id">The id to remove</param>
        bool RemoveRelationId(long id);

        /// <summary>
        /// Retruns true if the relation id is in the collection
        /// </summary>
        /// <param name="id">The id to evaluate</param>
        bool ContainsRelationId(long id);

        /// <summary>
        /// Clears all data from the collection
        /// </summary>
        void Clear();
    }
}