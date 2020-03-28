using System;
using Maps.Geographical;
using Maps.IO.Geographical;
using ProtoBuf;

namespace Maps.IO
{
    /// <summary>
    /// Represents a SourceMeta instance for binary storage
    /// </summary>
    [ProtoContract]
    [ProtoInclude(2, typeof(BinaryTiledSourceMeta))]
    internal class BinarySourceMeta
    {
        static BinarySourceMeta()
        {
            Serializer.PrepareSerializer<BinarySourceMeta>();
        }

        protected GeodeticBox2d Area => _area.ToGeodeticBox2d();

        [ProtoMember(1)]
        private readonly BinaryGeodeticBox2d _area;

        /// <summary>
        /// Initializes a new instance of BinarySourceMeta
        /// </summary>
        /// <param name="meta">The SourceMeta instance to initialize from</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="meta"/> 
        /// is null</exception>
        internal BinarySourceMeta(SourceMeta meta)
        {
            if (meta == null)
            {
                throw new ArgumentNullException(nameof(meta));
            }

            _area = new BinaryGeodeticBox2d(meta.Area);
        }

        protected BinarySourceMeta() { }

        /// <summary>
        /// Returns the stored SourceMeta object
        /// </summary>
        internal SourceMeta ToSourceMeta()
        {
            return new SourceMeta(_area.ToGeodeticBox2d());
        }
    }
}