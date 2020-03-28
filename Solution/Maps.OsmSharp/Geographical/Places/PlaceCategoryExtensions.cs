using System;
using System.Collections.Generic;
using Maps.Geographical.Places;

namespace Maps.OsmSharp.Geographical.Places
{
    /// <summary>
    /// Provides extensions for the PlaceCategory class specific to
    /// the context of OsmSharp
    /// </summary>
    internal static class PlaceCategoryExtensions
    {
        /// <summary>
        /// Returns a dictionary of tags and values for the given PlaceCategory
        /// and the given RootPlaceCategoriesMap
        /// </summary>
        /// <param name="category">The PlaceCategory to evaluate</param>
        /// <param name="categoriesMap">The RootPlaceCategoriesMap to use in evaluation</param>
        internal static Dictionary<string, List<string>> Tags(this PlaceCategory category,
            CategoriesMap categoriesMap)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            if (categoriesMap == null)
            {
                throw new ArgumentNullException(nameof(categoriesMap));
            }

            return categoriesMap.TagsFor(category.Root);
        }
    }
}