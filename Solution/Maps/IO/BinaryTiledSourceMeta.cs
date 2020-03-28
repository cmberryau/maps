using System;
using System.IO;
using ProtoBuf;

namespace Maps.IO
{
    /// <summary>
    /// Represents a TiledSourceMeta instance for binary storage
    /// </summary>
    [ProtoContract]
    internal class BinaryTiledSourceMeta : BinarySourceMeta
    {
        static BinaryTiledSourceMeta()
        {
            Serializer.PrepareSerializer<BinaryTiledSourceMeta>();
        }

        [ProtoMember(1)]
        private readonly int[] _zoomLevels;

        /// <summary>
        /// Initializes a new instance of BinaryTiledSourceMeta
        /// </summary>
        /// <param name="meta">The TiledSourceMeta instance to initialize from</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="meta"/> 
        /// is null</exception>
        internal BinaryTiledSourceMeta(TiledSourceMeta meta) : base(meta)
        {
            if (meta == null)
            {
                throw new ArgumentNullException(nameof(meta));
            }

            _zoomLevels = new int[meta.ZoomLevels.Count];

            for (var i = 0; i < _zoomLevels.Length; ++i)
            {
                _zoomLevels[i] = meta.ZoomLevels[i];
            }
        }

        /// <summary>
        /// Returns the TiledSourceMeta object
        /// </summary>
        internal TiledSourceMeta ToTiledSourceMeta()
        {
            return new TiledSourceMeta(Area, _zoomLevels);
        }

        internal void Serialize(Stream destination)
        {
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            Serializer.Serialize(destination, this);
        }

        internal static BinaryTiledSourceMeta Deserialize(Stream source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var meta = Serializer.Deserialize<BinaryTiledSourceMeta>(source);
            return meta;
        }

        protected BinaryTiledSourceMeta() { }
    }
}