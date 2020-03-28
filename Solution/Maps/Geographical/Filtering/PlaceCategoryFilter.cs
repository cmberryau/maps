using System;
using System.Collections.Generic;
using Maps.Geographical.Places;

namespace Maps.Geographical.Filtering
{
    /// <summary>
    /// Responsible for filtering Places matching any of the given categories
    /// </summary>
    public class PlaceCategoryFilter : FeatureFilter<Place>
    {
        private readonly HashSet<PlaceCategory> _categories;

        /// <summary>
        /// Initializes a new instance of PlaceCategoryFilter
        /// </summary>
        /// <param name="categories">The categories to evaulate against</param>
        public PlaceCategoryFilter(IList<PlaceCategory> categories)
        {
            if (categories == null)
            {
                throw new ArgumentNullException(nameof(categories));
            }

            _categories = new HashSet<PlaceCategory>();

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
        public override bool Filter(Place place)
        {
            if (place == null)
            {
                throw new ArgumentNullException(nameof(place));
            }

            var result = _categories.Contains(place.Category);
            return result;
        }
    }
}