using System;
using Maps.Geographical.Places;

namespace Maps.Geographical.Filtering
{
    /// <summary>
    /// Allows no places to pass
    /// </summary>
    public class PlaceNullFilter : FeatureFilter<Place>
    {
        /// <inheritdoc />
        public override bool Filter(Place place)
        {
            if (place == null)
            {
                throw new ArgumentNullException(nameof(place));
            }

            return false;
        }
    }
}