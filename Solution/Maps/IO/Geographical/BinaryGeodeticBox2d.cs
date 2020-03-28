using System;
using Maps.Geographical;
using ProtoBuf;

namespace Maps.IO.Geographical
{
    /// <summary>
    /// Represents a GeodeticBox2d instance for binary storage
    /// </summary>
    [ProtoContract]
    internal class BinaryGeodeticBox2d
    {
        static BinaryGeodeticBox2d()
        {
            Serializer.PrepareSerializer<BinaryGeodeticBox2d>();
        }

        [ProtoMember(1)]
        private readonly BinaryCoordinate _a;
        [ProtoMember(2)]
        private readonly BinaryCoordinate _b;

        /// <summary>
        /// Initializes a new instance of BinaryGeodeticBox2d
        /// </summary>
        /// <param name="box">The box to initialize from</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="box"/> is
        /// null</exception>
        internal BinaryGeodeticBox2d(GeodeticBox2d box)
        {
            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            _a = new BinaryCoordinate(box.A);
            _b = new BinaryCoordinate(box.B);
        }

        private BinaryGeodeticBox2d(){}

        /// <summary>
        /// Returns the stored GeodeticBox2d object
        /// </summary>
        internal GeodeticBox2d ToGeodeticBox2d()
        {
            return new GeodeticBox2d(_a.Geodetic2dCoordinate(), 
                _b.Geodetic2dCoordinate());
        }
    }
}