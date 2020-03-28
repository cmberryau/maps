using System;
using Maps.Geographical;

namespace Maps
{
    /// <summary>
    /// Responsible for holding meta information for a source
    /// </summary>
    public class SourceMeta
    {
        /// <summary>
        /// The available area on the source
        /// </summary>
        public GeodeticBox2d Area => _area;

        private readonly GeodeticBox2d _area;

        /// <summary>
        /// Initializes a new instance of TiledSourceMeta
        /// </summary>
        /// <param name="area">The <paramref name="area"/> covered by the 
        /// TiledSourceMeta</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SourceMeta(GeodeticBox2d area)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            _area = area;
        }
    }
}