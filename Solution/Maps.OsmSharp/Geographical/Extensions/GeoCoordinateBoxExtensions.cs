using System;
using Maps.Geographical;
using OsmSharp.Math.Geo;

namespace Maps.OsmSharp.Geographical.Extensions
{
    /// <summary>
    /// Provides extensions for OsmSharp's GeoCoordinateBox class
    /// </summary>
    public static class GeoCoordinateBoxExtensions
    {
        /// <summary>
        /// Returns a GeodeticBox2d from an OsmSharp GeocoordinateBox
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        public static GeodeticBox2d GodeticBox2d(this GeoCoordinateBox box)
        {
            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            return new GeodeticBox2d(box.BottomLeft.Geodetic2d(), box.TopRight.Geodetic2d());
        }
    }
}