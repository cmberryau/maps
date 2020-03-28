using Maps.Geographical;
using OsmSharp.Math.Geo;

namespace Maps.OsmSharp.Geographical.Extensions
{
    /// <summary>
    /// Provides extensions for the GeodeticBox2d class
    /// specifically for the context of the OsmSharp provider
    /// </summary>
    public static class Geodetic2dExtensions
    {
        /// <summary>
        /// Returns a OsmSharp GeoCoordinate from the given
        /// Geodetic2d coordinate
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate</param>
        public static GeoCoordinate GeoCoordinate(this Geodetic2d coordinate)
        {
            return new GeoCoordinate(coordinate.Latitude, coordinate.Longitude);
        }
    }
}