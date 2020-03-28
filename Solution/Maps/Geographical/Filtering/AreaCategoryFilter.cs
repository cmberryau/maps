using System;
using System.Collections.Generic;
using Maps.Geographical.Features;

namespace Maps.Geographical.Filtering
{
    /// <summary>
    /// Responsible for filtering Areas matching any of the given categories
    /// </summary>
    public class AreaCategoryFilter : FeatureFilter<Area>
    {
        private readonly HashSet<AreaCategory> _categories;

        /// <summary>
        /// Initializes a new instance of AreaCategoryFilter
        /// </summary>
        /// <param name="categories">The categories to evaulate against</param>
        public AreaCategoryFilter(IList<AreaCategory> categories)
        {
            if (categories == null)
            {
                throw new ArgumentNullException(nameof(categories));
            }

            _categories = new HashSet<AreaCategory>();

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
        public override bool Filter(Area area)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            var result = _categories.Contains(area.Category);
            return result;
        }
    }
}