namespace Maps.Geographical.Geocoding
{
    /// <summary>
    /// Contains results from a reverse geocoding query
    /// </summary>
    public sealed class ReverseGeocodingResult
    {
        /// <summary>
        /// The coordinate of the ReverseGeocodingResult
        /// </summary>
        public readonly Geodetic3d Coordinate;

        /// <summary>
        /// Creates a new ReverseGeocodingResult
        /// </summary>
        /// <param name="coordinate"></param>
        public ReverseGeocodingResult(Geodetic3d coordinate)
        {
            Coordinate = coordinate;
        }
    }
}