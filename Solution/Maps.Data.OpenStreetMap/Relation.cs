using System;
using System.Collections.Generic;

namespace Maps.Data.OpenStreetMap
{
    /// <summary>
    /// Relation feature from OpenStreetMap
    /// </summary>
    public class Relation : OsmGeo
    {
        /// <summary>
        /// The members that make up the relation, in order
        /// </summary>
        public IReadOnlyList<OsmGeo> Members
        {
            get;
        }

        /// <summary>
        /// The role of each member
        /// </summary>
        public IReadOnlyList<string> MemberRoles
        {
            get;
        }

        /// <inheritdoc />
        protected override OsmGeoType Type => OsmGeoType.Relation;

        private readonly IList<OsmGeo> _members;

        /// <summary>
        /// Initializes a new instance of Relation
        /// </summary>
        /// <param name="id">The id of the relation</param>
        /// <param name="tags">The tags for the relation</param>
        /// <param name="members">The members of the relation</param>
        /// <param name="roles">The member roles of the relation</param>
        public Relation(long id, IDictionary<string, string> tags, 
            IList<OsmGeo> members, IList<string> roles) : base(id, tags)
        {
            if (members == null)
            {
                throw new ArgumentNullException(nameof(members));
            }

            if (roles == null)
            {
                throw new ArgumentNullException(nameof(roles));
            }

            if (members.Count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(members));
            }

            if (roles.Count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(roles));
            }

            if (members.Count != roles.Count)
            {
                throw new ArgumentException($"{nameof(members)} count does not " +
                                            $"match {nameof(roles)} count");
            }

            var memberCount = members.Count;
            for (var i = 0; i < memberCount; ++i)
            {
                if (roles[i] == null)
                {
                    throw new ArgumentException($"Contains null entry at {i}",
                        nameof(roles));
                }
            }

            _members = new List<OsmGeo>(members);
            Members = (IReadOnlyList<OsmGeo>) _members;
            MemberRoles = new List<string>(roles);
        }
        
        /// <summary>
        /// Sets a member for a given sequence
        /// </summary>
        /// <param name="sequence">The sequence to set</param>
        /// <param name="geo">The member geo to set</param>
        internal void SetMember(int sequence, OsmGeo geo)
        {
            if (geo == null)
            {
                throw new ArgumentNullException(nameof(geo));
            }

            if (_members[sequence] != null)
            {
                throw new InvalidOperationException("Trying to set already set member");
            }

            _members[sequence] = geo;
        }
    }
}