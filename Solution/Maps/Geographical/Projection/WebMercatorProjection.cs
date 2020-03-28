using System;

namespace Maps.Geographical.Projection
{
    /// <summary>
    /// Provides a WebMercator projection, an approximation of Mercator projection
    /// </summary>
    /// <remarks>
    /// https://en.wikipedia.org/wiki/Web_Mercator
    /// http://wiki.openstreetmap.org/wiki/Mercator
    /// http://wiki.openstreetmap.org/wiki/Slippy_map_tilenames#Derivation_of_tile_names
    /// </remarks>
    public class WebMercatorProjection : Projection2d
    {
        /// <inheritdoc />
        public override Vector3d Extents => new Vector3d(_scale, _scale, _scale);

        private const double LongitudeLimit = 180;
        private const double LatitudeLimit = 85.0511287798066; // atan(sinh(pi))
        private const double DefaultScale = 1d;
        private const double PiOver4 = Math.PI / 4d;
        private readonly double _scale;

        /// <summary>
        /// Initializes a new instance of WebMercatorProjection
        /// </summary>
        /// <param name="scale">The scaling factor</param>
        public WebMercatorProjection(double scale = DefaultScale)
        {
            _scale = scale;
        }

        /// <inheritdoc />
        public override Vector3d Forward(Geodetic2d coordinate)
        {
            coordinate = Geodetic2d.ClampAbs(coordinate, LatitudeLimit,
                LongitudeLimit);

            var x = coordinate.Longitude / 360d;
            var latRads = coordinate.Latitude * Mathd.Deg2Rad;
            var y = Math.Log(Math.Tan(PiOver4 + latRads / 2d)) / Math.PI * 0.5d;

            return new Vector3d(x * _scale, y * _scale, 0d);
        }

        /// <inheritdoc />
        public override Vector3d Forward(Geodetic3d coordinate)
        {
            var projected = Forward(coordinate.Geodetic2d);

            return new Vector3d(projected.x, projected.y,
                coordinate.Height / Mathd.CEquatorial * -_scale);
        }

        /// <inheritdoc />
        public override Geodetic3d Reverse(Vector3d point)
        {
            var longitude = point.x / _scale * 360d;
            var latRads = Math.Atan(Math.Exp(point.y / _scale * 2d * Math.PI)) * 2d - 
                PiOver4 * 2d;

            return new Geodetic3d(latRads * Mathd.Rad2Deg, longitude,
                 Mathd.CEquatorial * (point.z / -_scale));
        }
    }
}