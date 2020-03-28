using Maps.Geographical.Features;

namespace Maps.Geographical.Filtering
{
    /// <summary>
    /// Performs no filtering
    /// </summary>
    public class SegmentPassthroughFilter : FeatureFilter<Segment>
    {
        /// <inheritdoc />
        public override bool Filter(Segment segment)
        {
            return true;
        }
    }
}