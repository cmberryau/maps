using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maps.Geographical.Geocoding
{
    /// <summary>
    /// Interface for a source of reverse geocoding
    /// </summary>
    public interface IReverseGeocodingSource : IDisposable
    {
        /// <summary>
        /// Returns the results from the reverse geocoding source given the 
        /// coordinate and the default search parameters
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate</param>
        /// <param name="range">The range from the coordinate to evaluate</param>
        IList<ReverseGeocodingResult> Get(Geodetic2d coordinate, double range);

        /// <summary>
        /// Returns the results from the reverse geocoding source given the 
        /// coordinate and the default search parameters asyncronously
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate</param>
        /// <param name="range">The range from the coordinate to evaluate</param>
        Task<IList<ReverseGeocodingResult>> GetAsync(Geodetic2d coordinate, double range);
    }
}
