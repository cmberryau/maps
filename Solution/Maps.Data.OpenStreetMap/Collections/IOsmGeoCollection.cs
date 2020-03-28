using System.Collections.Generic;

namespace Maps.Data.OpenStreetMap.Collections
{
    /// <summary>
    /// An interface for OpenStreetMap geometry collection implementers
    /// </summary>
    internal interface IOsmGeoCollection
    {
        /// <summary>
        /// The number of nodes held in the collection
        /// </summary>
        int NodeCount
        {
            get;
        }

        /// <summary>
        /// The number of ways held in the collection
        /// </summary>
        int WayCount
        {
            get;
        }

        /// <summary>
        /// The number of relations held in the collection
        /// </summary>
        int RelationCount
        {
            get;
        }

        /// <summary>
        /// Adds a node
        /// </summary>
        void AddNode(Node node);

        /// <summary>
        /// Adds a list of nodes
        /// </summary>
        void AddNodes(IList<Node> nodes);

        /// <summary>
        /// Adds a dictionary of node ids and nodes
        /// </summary>
        void AddNodes(IDictionary<long, Node> nodes);

        /// <summary>
        /// Returns a dictionary of fetched nodes given the ids
        /// </summary>
        /// <param name="ids">The ids of the nodes to fetch</param>
        /// <param name="unfetchedIds">The ids that were unable to be found</param>
        IDictionary<long, Node> GetNodes(IList<long> ids, out IList<long> unfetchedIds);

        /// <summary>
        /// Returns a dictionary of fetched nodes given the ids
        /// </summary>
        /// <param name="ids">The ids of the nodes to fetch</param>
        IDictionary<long, Node> GetNodes(IList<long> ids);

        /// <summary>
        /// Returns a list of fetched nodes given the ids
        /// </summary>
        /// <param name="ids">The ids of the nodes to fetch</param>
        /// <param name="unfetchedIds">The ids that were unable to be found</param>
        IList<Node> GetNodesList(IList<long> ids, out IList<long> unfetchedIds);

        /// <summary>
        /// Returns a list of fetched nodes given the ids
        /// </summary>
        /// <param name="ids">The ids of the nodes to fetch</param>
        IList<Node> GetNodesList(IList<long> ids);

        /// <summary>
        /// Removes the node with the given id
        /// </summary>
        bool RemoveNode(long id);

        /// <summary>
        /// Retruns true if the node exists
        /// </summary>
        bool ContainsNode(long id);

        /// <summary>
        /// Tries to get the node with the given id
        /// </summary>
        bool TryGetNode(long id, out Node node);

        /// <summary>
        /// Adds a way
        /// </summary>
        void AddWay(Way way);

        /// <summary>
        /// Adds a list of ways
        /// </summary>
        void AddWays(IList<Way> ways);

        /// <summary>
        /// Adds a dictionary of ways
        /// </summary>
        void AddWays(IDictionary<long, Way> ways);

        /// <summary>
        /// Returns a dictionary of fetched ways given the ids
        /// </summary>
        /// <param name="ids">The ids of the ways to fetch</param>
        /// <param name="unfetchedIds">The ids that were unable to be found</param>
        IDictionary<long, Way> GetWays(IList<long> ids, out IList<long> unfetchedIds);

        /// <summary>
        /// Returns a dictionary of fetched ways given the ids
        /// </summary>
        /// <param name="ids">The ids of the ways to fetch</param>
        IDictionary<long, Way> GetWays(IList<long> ids);

        /// <summary>
        /// Returns a list of fetched ways given the ids
        /// </summary>
        /// <param name="ids">The ids of the ways to fetch</param>
        /// <param name="unfetchedIds">The ids that were unable to be found</param>
        IList<Way> GetWaysList(IList<long> ids, out IList<long> unfetchedIds);

        /// <summary>
        /// Returns a list of fetched ways given the ids
        /// </summary>
        /// <param name="ids">The ids of the ways to fetch</param>
        IList<Way> GetWaysList(IList<long> ids);

        /// <summary>
        /// Removes the way with the given id
        /// </summary>
        bool RemoveWay(long id);

        /// <summary>
        /// Returns true if the way exists
        /// </summary>
        bool ContainsWay(long id);

        /// <summary>
        /// Tries to get the way with the given id
        /// </summary>
        bool TryGetWay(long id, out Way way);

        /// <summary>
        /// Adds a new relation
        /// </summary>
        void AddRelation(Relation relation);

        /// <summary>
        /// Adds a list of relations
        /// </summary>
        void AddRelations(IList<Relation> relations);

        /// <summary>
        /// Adds a dictionary of relations
        /// </summary>
        void AddRelations(IDictionary<long, Relation> relations);

        /// <summary>
        /// Returns a dictionary of fetched relations given the ids
        /// </summary>
        /// <param name="ids">The ids of the relations to fetch</param>
        /// <param name="unfetchedIds">The ids that were unable to be found</param>
        IDictionary<long, Relation> GetRelations(IList<long> ids, out IList<long> unfetchedIds);

        /// <summary>
        /// Returns a dictionary of fetched relations given the ids
        /// </summary>
        /// <param name="ids">The ids of the relations to fetch</param>
        /// <param name="unfetchedIds">The ids that were unable to be found</param>
        IDictionary<long, Relation> GetRelations(IReadOnlyList<long> ids, out IList<long> unfetchedIds);

        /// <summary>
        /// Returns a dictionary of fetched relations given the ids
        /// </summary>
        /// <param name="ids">The ids of the relations to fetch</param>
        IDictionary<long, Relation> GetRelations(IList<long> ids);

        /// <summary>
        /// Returns a dictionary of fetched relations given the ids
        /// </summary>
        /// <param name="ids">The ids of the relations to fetch</param>
        IDictionary<long, Relation> GetRelations(IReadOnlyList<long> ids);

        /// <summary>
        /// Returns a list of fetched relations given the ids
        /// </summary>
        /// <param name="ids">The ids of the relations to fetch</param>
        /// <param name="unfetchedIds">The ids that were unable to be found</param>
        IList<Relation> GetRelationsList(IList<long> ids, out IList<long> unfetchedIds);

        /// <summary>
        /// Returns a list of fetched relations given the ids
        /// </summary>
        /// <param name="ids">The ids of the relations to fetch</param>
        /// <param name="unfetchedIds">The ids that were unable to be found</param>
        IList<Relation> GetRelationsList(IReadOnlyList<long> ids, out IList<long> unfetchedIds);

        /// <summary>
        /// Returns a list of fetched relations given the ids
        /// </summary>
        /// <param name="ids">The ids of the relations to fetch</param>
        IList<Relation> GetRelationsList(IList<long> ids);

        /// <summary>
        /// Returns a list of fetched relations given the ids
        /// </summary>
        /// <param name="ids">The ids of the relations to fetch</param>
        IList<Relation> GetRelationsList(IReadOnlyList<long> ids);

        /// <summary>
        /// Removes the relation with the given id
        /// </summary>
        bool RemoveRelation(long id);

        /// <summary>
        /// Retruns true if the relation exists
        /// </summary>
        bool ContainsRelation(long id);

        /// <summary>
        /// Tries to get the relation with the given id
        /// </summary>
        bool TryGetRelation(long id, out Relation relation);

        /// <summary>
        /// Clears all data from the collection
        /// </summary>
        void Clear();
    }
}