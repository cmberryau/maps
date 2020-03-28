namespace Maps.Data.OpenStreetMap.Collections
{
    /// <summary>
    /// Represents a relation's member
    /// </summary>
    internal struct RelationMember
    {
        /// <summary>
        /// The type of member
        /// </summary>
        public readonly OsmGeoType Type;

        /// <summary>
        /// The id of the member
        /// </summary>
        public readonly long Id;

        /// <summary>
        /// The role of the member
        /// </summary>
        public readonly string Role;

        /// <summary>
        /// The sequence of the member
        /// </summary>
        public readonly int Sequence;

        /// <summary>
        /// Initializes a new instance of RelationMember
        /// </summary>
        /// <param name="type">The type of relation member</param>
        /// <param name="id">The id of the relation member</param>
        /// <param name="role">The role of the relation member</param>
        /// <param name="sequence">The sequence of the relation member</param>
        public RelationMember(OsmGeoType type, long id, string role, int sequence)
        {
            Type = type;
            Id = id;
            Role = role;
            Sequence = sequence;
        }
    }
}