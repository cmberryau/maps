using System;
using Maps.Geographical.Features;

namespace Maps.Geographical.Filtering
{
    /// <summary>
    /// Responsible for filtering Areas with a geodetic area equal or larger the given
    /// minimum geodetic area
    /// </summary>
    public class AreaGeodeticAreaFilter : FeatureFilter<Area>
    {
        private readonly double _minimumArea;

        /// <summary>
        /// Initializes a new instance of AreaGeodeticAreaFilter
        /// </summary>
        /// <param name="min">The minimum area in square meters to pass</param>
        public AreaGeodeticAreaFilter(double min)
        {
            _minimumArea = min;
        }

        /// <inheritdoc />
        public override bool Filter(Area area)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            return area.OriginalArea >= _minimumArea;
        }
    }
}