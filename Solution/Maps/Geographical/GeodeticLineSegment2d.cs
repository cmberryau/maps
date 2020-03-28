namespace Maps.Geographical
{
    /// <summary>
    /// Represents a 2d geodetic line segment
    /// </summary>
    public class GeodeticLineSegment2d
    {
        /// <summary>
        /// First coordinate of the line segment
        /// </summary>
        public readonly Geodetic2d P0;

        /// <summary>
        /// Second coordinate of the line segment
        /// </summary>
        public readonly Geodetic2d P1;

        /// <summary>
        /// Initializes a new instance of GeodeticLineSegment2d
        /// </summary>
        /// <param name="p0">First coordinate of the line</param>
        /// <param name="p1">Second coordinate of the line</param>
        public GeodeticLineSegment2d(Geodetic2d p0, Geodetic2d p1)
        {
            P0 = p0;
            P1 = p1;
        }

        /// <summary>
        /// Evaluates the distance from the segment to the coordinate
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate against</param>
        /// <returns>The distance in meters</returns>
        /// <remarks>I'm not sure of the proof behind this, but it seems to work.
        /// See: https://github.com/DotSpatial/DotSpatial/blob/5ac3daa615b0e1443c1eeaae7d315544b4d9ac36/Source/DotSpatial.Positioning/Segment.cs#L138</remarks>
        public double Distance(Geodetic2d coordinate)
        {
            if (P0 == P1)
            {
                return Geodetic2d.Distance(P0, coordinate);
            }

            var d = P1 - P0;
            var t = ((coordinate.Longitude - P0.Longitude) * d.Longitude + 
                     (coordinate.Latitude - P0.Latitude) * d.Latitude) / 
                     (d.Longitude * d.Longitude + d.Latitude * d.Latitude);

            if (t < 0)
            {
                return Geodetic2d.Distance(P0, coordinate);
            }

            if (t > 1)
            {
                return Geodetic2d.Distance(P1, coordinate);
            }

            var p = new Geodetic2d((1 - t) * P0.Latitude + t * P1.Latitude,
                                   (1 - t) * P0.Longitude + t * P1.Longitude);

            return Geodetic2d.Distance(coordinate, p);
        }
    }
}