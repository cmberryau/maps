using System;
using System.Collections.Generic;

namespace Maps.Data.OpenStreetMap.Collections
{
    /// <summary>
    /// Contains relation member ids, types, roles and sequences
    /// </summary>
    internal class RelationMemberCollection : IRelationMemberCollection
    {
        /// <inheritdoc />
        public IOsmGeoIdCollection Ids
        {
            get;
        }

        /// <inheritdoc />
        public int RelationCount => RelationIds.Count;

        /// <inheritdoc />
        public IList<long> RelationIds
        {
            get;
        }

        private readonly IDictionary<long, IList<RelationMember>> _members;

        /// <summary>
        /// Initializes a new instance of RelationMemberCollection
        /// </summary>
        public RelationMemberCollection()
        {
            Ids = new OsmGeoIdCollection();
            RelationIds = new List<long>();
            _members = new Dictionary<long, IList<RelationMember>>();
        }

        /// <inheritdoc />
        public void Add(long relationId, RelationMember member)
        {
            if (!_members.ContainsKey(relationId))
            {
                _members.Add(relationId, new List<RelationMember>());
                RelationIds.Add(relationId);
            }

            _members[relationId].Add(member);
            Ids.AddId(member.Id, member.Type);
        }

        /// <inheritdoc />
        public void Add(long relationId, IList<RelationMember> members)
        {
            if (members == null)
            {
                throw new ArgumentNullException(nameof(members));
            }

            var membersCount = members.Count;
            for (var i = 0; i < membersCount; ++i)
            {
                Add(relationId, members[i]);
            }
        }

        /// <inheritdoc />
        public bool HasRelation(long relationId)
        {
            return _members.ContainsKey(relationId);
        }

        /// <inheritdoc />
        public IList<RelationMember> MembersFor(long relationId)
        {
            return _members[relationId];
        }

        /// <inheritdoc />
        public void Append(IRelationMemberCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            var relationCount = collection.RelationCount;
            for (var i = 0; i < relationCount; ++i)
            {
                var relationId = collection.RelationIds[i];
                Add(relationId, collection.MembersFor(relationId));
            }
        }
    }
}