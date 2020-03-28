using Maps.Geographical.Features;

namespace Maps.Geographical.Filtering
{
    /// <summary>
    /// Performs no filtering
    /// </summary>
    public class AreaPassthroughFilter : FeatureFilter<Area>
    {
        /// <inheritdoc />
        public override bool Filter(Area area)
        {
            return true;
        }
    }
}