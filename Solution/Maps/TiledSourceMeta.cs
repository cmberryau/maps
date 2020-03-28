using System;
using System.Collections.Generic;
using System.IO;
using Maps.Collections;
using Maps.Geographical;
using Maps.IO;

namespace Maps
{
    /// <summary>
    /// Responsible for holding meta information for a tiled source
    /// </summary>
    public class TiledSourceMeta : SourceMeta
    {
        /// <summary>
        /// The available zoom levels on the source
        /// </summary>
        public IReadOnlyList<int> ZoomLevels => _zoomLevels;

        private readonly IReadOnlyList<int> _zoomLevels;

        /// <summary>
        /// Initializes a new instance of TiledSourceMeta
        /// </summary>
        /// <param name="area">The area covered by the TiledSourceMeta</param>
        /// <param name="zoomLevels">The available zoom levels</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="area"/> or
        /// <paramref name="zoomLevels"/> are null</exception>
        public TiledSourceMeta(GeodeticBox2d area, IList<int> zoomLevels) : base(area)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            if (zoomLevels == null)
            {
                throw new ArgumentNullException(nameof(zoomLevels));
            }

            _zoomLevels = new ReadOnlyList<int>(zoomLevels);
        }

        /// <summary>
        /// Serializes the TiledSourceMeta instance
        /// </summary>
        /// <param name="destination">The destination stream to serialize to</param>
        /// <exception cref="ArgumentNullException">Thrown if destination is null
        /// </exception>
        public void Serialize(Stream destination)
        {
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            var binary = new BinaryTiledSourceMeta(this);
            binary.Serialize(destination);
        }

        /// <summary>
        /// Deserializes a TiledSourceMeta instance
        /// </summary>
        /// <param name="source">The source stream to deserialize from</param>
        /// <param name="meta">The target TiledSourceMeta object</param>
        /// <returns>A deserialized instance of TiledSourceMeta</returns>
        /// <exception cref="ArgumentNullException">Thrown if source is null</exception>
        public static bool TryDeserialize(Stream source, out TiledSourceMeta meta)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var binary = BinaryTiledSourceMeta.Deserialize(source);
            meta = binary.ToTiledSourceMeta();
            return meta != null;
        }
    }
}