using System;
using Maps.Geographical.Features;
using Maps.Geographical.Filtering;

namespace Maps.Appearance
{
    /// <summary>
    /// Responsible for holding a filter and an area appearance
    /// </summary>
    internal class AreaAppearanceTarget
    {
        /// <summary>
        /// The appearance to apply
        /// </summary>
        public AreaAppearance Appearance
        {
            get;
        }

        /// <summary>
        /// The filter to evaluate against
        /// </summary>
        public FeatureFilter<Area> Filter
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of AreaAppearanceTarget
        /// </summary>
        /// <param name="appearance">The appearance to hold</param>
        /// <param name="filter">The filter for evaluation</param>
        public AreaAppearanceTarget(AreaAppearance appearance,
            FeatureFilter<Area> filter)
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