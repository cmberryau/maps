using System;
using Maps.Geographical.Features;
using Maps.Geographical.Filtering;

namespace Maps.Appearance
{
    /// <summary>
    /// Responsible for holding a filter and a segment appearance
    /// </summary>
    internal class SegmentAppearanceTarget
    {
        /// <summary>
        /// The appearance to apply
        /// </summary>
        public SegmentAppearance Appearance
        {
            get;
        }

        /// <summary>
        /// The filter to evaluate against
        /// </summary>
        public FeatureFilter<Segment> Filter
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of SegmentAppearanceTarget
        /// </summary>
        /// <param name="appearance">The appearance to hold</param>
        /// <param name="filter">The filter for evaluation</param>
        public SegmentAppearanceTarget(SegmentAppearance appearance, 
            FeatureFilter<Segment> filter)
        {
            if (appearance == null)
            {
                throw new ArgumentNullException(nameof(appearance));
            }

            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            Appearance = appearance;
            Filter = filter;
        }
    }
}