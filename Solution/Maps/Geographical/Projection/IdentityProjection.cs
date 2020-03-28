namespace Maps.Geographical.Projection
{
    /// <summary>
    /// A three dimensional Identity projection
    /// </summary>
    public class IdentityProjection : Projection2d
    {
        /// <inheritdoc />
        public override Vector3d Extents => new Vector3d(_scale, _scale, _scale);

        private const double DefaultScale = 1;
        private readonly double _scale;

        /// <summary>
        /// Initializes a new instance of IdentityProjection
        /// </summary>
        /// <param name="scale">The scaling factor</param>
        public IdentityProjection(double scale = DefaultScale)
        {
            _scale = scale;
        }

        /// <inheritdoc />
        public override Vector3d Forward(Geodetic2d coordinate)
        {
            return new Vector3d(coordinate.Longitude / 180 * _scale,
                coordinate.Latitude / 180 * _scale, 0);
        }

        /// <inheritdoc />
        public override Vector3d Forward(Geodetic3d coordinate)
        {
            return new Vector3d(coordinate.Longitude / 180 * _scale,
                coordinate.Latitude / 180 * _scale, coordinate.Height / 
                Mathd.CEquatorial * -_scale);
        }

        /// <inheritdoc />
        public override Geodetic3d Reverse(Vector3d point)
        {
            return new Geodetic3d(point.y * 180 / _scale, 
                point.x * 180 / _scale, Mathd.CEquatorial * (point.z / -_scale));
        }
    }
}