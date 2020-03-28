using System;
using Maps.Geographical;
using OsmSharp.Math.Geo;

namespace Maps.OsmSharp.Geographical.Extensions
{
    /// <summary>
    /// Provides extensions for OsmSharp's GeoCoordinate class
    /// </summary>
    public static class GeoCoordinateExtensions
    {
        /// <summary>
        /// Returns a Geodetic2d corodinate from the given OsmSharp
        /// GeoCoordiante
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate</param>
        public static Geodetic2d Geodetic2d(this GeoCoordinate coordinate)
        {
            if (coordinate == null)
            {
                throw new ArgumentNullException(nameof(coordinate));
            }

            return new Geodetic2d(coordinate.Latitude, coordinate.Longitude);
        }
    }
}