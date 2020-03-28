using System;

namespace Maps.Geographical.Projection
{
    /// <summary>
    /// Provides an equal area projection
    /// </summary>
    public sealed class LambertAzimuthalEqualAreaProjection : Projection2d
    {
        /// <inheritdoc />
        public override Vector3d Extents
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private readonly Geodetic2d _centre;
        private readonly double _scale;
        private readonly double _sinCentreLat;
        private readonly double _cosCentreLat;

        /// <summary>
        /// Initializes a new instance of LambertAzimuthalEqualAreaProjection
        /// http://desktop.arcgis.com/en/arcmap/10.3/guide-books/map-projections/lambert-azimuthal-equal-area.htm
        /// </summary>
        /// <param name="centre">The centre of the projection</param>
        public LambertAzimuthalEqualAreaProjection(Geodetic2d centre)
        {
            _centre = centre;
            _sinCentreLat = Math.Sin(_centre.Latitude * Mathd.Deg2Rad);
            _cosCentreLat = Math.Cos(_centre.Latitude * Mathd.Deg2Rad);

            _scale = 1d;
            var projectedCentre = Forward(centre);
            var projectedOffset = Forward(Geodetic2d.Offset(centre, 1d,
                (double)CardinalDirection.East));
            _scale = 1d / Vector3d.Distance(projectedCentre, projectedOffset);
        }

        /// <inheritdoc/>
        public override Vector3d Forward(Geodetic2d coordinate)
        {
            var sinLat = Math.Sin(coordinate.Latitude * Mathd.Deg2Rad);
            var cosLat = Math.Cos(coordinate.Latitude * Mathd.Deg2Rad);

            var dLon = (coordinate.Longitude - _centre.Longitude) * Mathd.Deg2Rad;
            var cosDLon = Math.Cos(dLon);
            var sinDLon = Math.Sin(dLon);

            var k = Math.Sqrt(2 / (1 + _sinCentreLat * sinLat + _cosCentreLat *
                cosLat * cosDLon));
            var x = k * cosLat * sinDLon;
            var y = k * (_cosCentreLat * sinLat - _sinCentreLat * cosLat *
                cosDLon);

            return new Vector3d(x, y, 0d) * _scale;
        }

        /// <inheritdoc />
        public override Vector3d Forward(Geodetic3d coordinate)
        {
            var projected2d = Forward(coordinate.Geodetic2d);
            return new Vector3d(projected2d.x, projected2d.y, 0d);
        }

        /// <inheritdoc/>
        public override Geodetic3d Reverse(Vector3d point)
        {
            var p = point.Magnitude;
            var c = 2 * Math.Asin(p * 0.5);

            var cosC = Math.Cos(c);
            var sinC = Math.Sin(c);

            var lat = Math.Asin(cosC * _sinCentreLat + point.y * sinC * _cosCentreLat / 
                p) * Mathd.Rad2Deg;
            var lon = _centre.Longitude + Math.Atan(point.x * sinC / (p * _cosCentreLat 
                * cosC - point.y * _sinCentreLat * sinC)) * Mathd.Rad2Deg;

            return new Geodetic3d(lat, lon, 0d);
        }
    }
}