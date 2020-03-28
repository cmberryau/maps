using System;
using System.Collections.Generic;
using Maps.Geographical.Features;

namespace Maps.Geographical.Filtering
{
    /// <summary>
    /// Responsible for filtering Segments matching any of the given categories
    /// </summary>
    public class SegmentCategoryFilter : FeatureFilter<Segment>
    {
        private readonly HashSet<SegmentCategory> _categories;

        /// <summary>
        /// Initializes a new instance of SegmentCategoryFilter
        /// </summary>
        /// <param name="categories">The categories to evaulate against</param>
        public SegmentCategoryFilter(IList<SegmentCategory> categories)
        {
            if (categories == null)
            {
                throw new ArgumentNullException(nameof(categories));
            }

            _categories = new HashSet<SegmentCategory>();

            for (var i = 0; i < categories.Count; ++i)
            {
                if (categories[i] == null)
                {
                    throw new ArgumentException($"Contains null element at index {i}",
                        nameof(categories));
                }

                _categories.Add(categories[i]);
            }
        }

        /// <inheritdoc />
        public override bool Filter(Segment segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            var result = _categories.Contains(segment.Category);
            return result;
        }
    }
}