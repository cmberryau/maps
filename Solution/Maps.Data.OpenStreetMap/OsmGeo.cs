using System;
using System.Collections.Generic;
using Maps.Extensions;

namespace Maps.Data.OpenStreetMap
{
    /// <summary>
    /// Enum to describe the geometry type
    /// </summary>
    public enum OsmGeoType
    {
        Node = 0,
        Way = 1,
        Relation = 2,
    }

    /// <summary>
    /// Base class for OpenStreetMap geometries
    /// </summary>
    public abstract class OsmGeo
    {
        /// <summary>
        /// The id for the geometry
        /// </summary>
        public readonly long Id;

        /// <summary>
        /// The tags for the geometry
        /// </summary>
        public readonly IReadOnlyDictionary<string, string> Tags;

        /// <summary>
        /// The globally unique id for the geometry
        /// </summary>
        public Guid Guid => Id.ToGuid((long)Type);

        /// <summary>
        /// The name of the geometry
        /// </summary>
        public string Name => Tags.ContainsKey("name") ? Tags["name"] : null;

        /// <summary>
        /// The enum matching this geometry
        /// </summary>
        protected abstract OsmGeoType Type
        {
            get;
        }

        private static readonly IReadOnlyDictionary<string, string> EmptyTags =
            new Dictionary<string, string>();

        /// <summary>
        /// Initializes a new instance of OsmGeo
        /// </summary>
        /// <param name="id">The id of the geometry</param>
        protected OsmGeo(long id) : this(id, null)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of OsmGeo
        /// </summary>
        /// <param name="id">The id of the geometry</param>
        /// <param name="tags">The tags for the geometry</param>
        protected OsmGeo(long id, IDictionary<string, string> tags)
        {
            if (tags == null || tags.Count <= 0)
            {
                Tags = EmptyTags;
            }
            else
            {
                foreach (var tag in tags)
                {
                    if (tag.Key == null)
                    {
                        throw new ArgumentException("Contains a null tag " +
                                                    "key", nameof(tags));
                    }
                }

                Tags = new Dictionary<string, string>(tags);
            }

            Id = id;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Type} : {Id}";
        }
    }
}