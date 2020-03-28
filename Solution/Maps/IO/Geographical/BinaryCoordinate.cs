using Maps.Geographical;
using ProtoBuf;

namespace Maps.IO.Geographical
{
    /// <summary>
    /// Represents a coordinate for binary storage
    /// </summary>
    [ProtoContract]
    public struct BinaryCoordinate
    {
        static BinaryCoordinate()
        {
            Serializer.PrepareSerializer<BinaryCoordinate>();
        }

        /// <summary>
        /// The floating point Latitude component
        /// </summary>
        [ProtoMember(1)]
        internal readonly double Latitude;

        /// <summary>
        /// The floating point longitude component
        /// </summary>
        [ProtoMember(2)]
        internal readonly double Longitude;

        /// <summary>
        /// The maximum internal representation value
        /// </summary>
        internal static double MaxValue => double.MaxValue;

        /// <summary>
        /// The minimum internal representation value
        /// </summary>
        internal static double MinValue => double.MinValue;

        /// <summary>
        /// Initializes a new instance of BinaryCoordinate
        /// </summary>
        /// <param name="latitude">The latitude of the Coordinate</param>
        /// <param name="longitude">The longitude of the Coordinate</param>
        public BinaryCoordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Initializes a new instance of BinaryCoordinate
        /// </summary>
        /// <param name="coordinate">The coordinate</param>
        public BinaryCoordinate(Geodetic2d coordinate)
        {
            Latitude = coordinate.Latitude;
            Longitude = coordinate.Longitude;
        }

        /// <summary>
        /// Returns a Geodetic2d instance from the BinaryCoordinate
        /// </summary>
        public Geodetic2d Geodetic2dCoordinate()
        {
            return new Geodetic2d(Latitude, Longitude);
        }
    }
}