using System;
using Maps.Geographical.Features;
using Maps.Geographical.Filtering;
using Maps.Geographical.Places;

namespace Maps.Appearance
{
    /// <summary>
    /// Responsible for holding a filter and a place appearance
    /// </summary>
    internal class PlaceAppearanceTarget
    {
        /// <summary>
        /// The appearance to apply
        /// </summary>
        public PlaceAppearance Appearance
        {
            get;
        }

        /// <summary>
        /// The filter to evaluate against
        /// </summary>
        public FeatureFilter<Place> Filter
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of PlaceAppearanceTarget
        /// </summary>
        /// <param name="appearance">The appearance to hold</param>
        /// <param name="filter">The filter for evaluation</param>
        public PlaceAppearanceTarget(PlaceAppearance appearance, 
            FeatureFilter<Place> filter)
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