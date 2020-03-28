using Maps.Geographical;
using OsmSharp.Math.Geo;

namespace Maps.OsmSharp.Geographical.Extensions
{
    /// <summary>
    /// Provides extensions for the GeodeticBox2d class
    /// specifically for the context of the OsmSharp provider
    /// </summary>
    public static class GeodeticBox2dExtensions
    {
        /// <summary>
        /// Returns a OsmSharp GeoCoordinateBox from the given
        /// GeodeticBox2d coordinate
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        public static GeoCoordinateBox GeoCoordinateBox(this GeodeticBox2d box)
        {
            return new GeoCoordinateBox(box.A.GeoCoordinate(), box.B.GeoCoordinate());
        }
    }
}