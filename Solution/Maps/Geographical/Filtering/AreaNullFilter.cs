using System;
using Maps.Geographical.Features;

namespace Maps.Geographical.Filtering
{
    /// <summary>
    /// Allows no areas to pass
    /// </summary>
    public class AreaNullFilter : FeatureFilter<Area>
    {
        /// <inheritdoc />
        public override bool Filter(Area area)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            return false;
        }
    }
}