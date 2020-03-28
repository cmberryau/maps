using Maps.Geographical.Places;

namespace Maps.Geographical.Filtering
{
    /// <summary>
    /// Performs no filtering
    /// </summary>
    public class PlacePassthroughFilter : FeatureFilter<Place>
    {
        /// <inheritdoc />
        public override bool Filter(Place place)
        {
            return true;
        }
    }
}