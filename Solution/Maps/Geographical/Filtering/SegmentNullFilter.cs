using System;
using Maps.Geographical.Features;

namespace Maps.Geographical.Filtering
{
    /// <summary>
    /// Allows no segments to pass
    /// </summary>
    public class SegmentNullFilter : FeatureFilter<Segment>
    {
        /// <inheritdoc />
        public override bool Filter(Segment segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            return false;
        }
    }
}