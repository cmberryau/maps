using System.Collections.Generic;

namespace Maps.Data.OpenStreetMap.Collections
{
    /// <summary>
    /// Interface for objects storing relation members during queries
    /// </summary>
    internal interface IRelationMemberCollection
    {
        /// <summary>
        /// The id collection of all members
        /// </summary>
        IOsmGeoIdCollection Ids
        {
            get;
        }

        /// <summary>
        /// The number of relations the collection has members for
        /// </summary>
        int RelationCount
        {
            get;
        }

        /// <summary>
        /// The ids of the relations the collection has members for
        /// </summary>
        IList<long> RelationIds
        {
            get;
        }

        /// <summary>
        /// Adds a relation member to the member collection
        /// </summary>
        /// <param name="relationId">The id of the relation</param>
        /// <param name="member">The relation member</param>
        void Add(long relationId, RelationMember member);

        /// <summary>
        /// Adds a relation members to the member collection
        /// </summary>
        /// <param name="relationId">The id of the relation</param>
        /// <param name="members">The relations member</param>
        void Add(long relationId, IList<RelationMember> members);

        /// <summary>
        /// Evaluates if the relation member contains the relation
        /// </summary>
        /// <param name="relationId">The relation to look up</param>
        /// <returns>True if so, false otherwise</returns>
        bool HasRelation(long relationId);

        /// <summary>
        /// Returns the members for the given relation
        /// </summary>
        /// <param name="relationId">The relation id to return members for</param>
        /// <returns>The list of members for the given relation</returns>
        IList<RelationMember> MembersFor(long relationId);

        /// <summary>
        /// Appends the given collection
        /// </summary>
        /// <param name="collection">The collection to append</param>
        void Append(IRelationMemberCollection collection);
    }
}