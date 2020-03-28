using System;
using Maps.Geographical.Features;

namespace Maps.Geographical.Filtering
{
    /// <summary>
    /// Responsible for filtering with not logic
    /// </summary>
    /// <typeparam name="T">The feature type</typeparam>
    public class NotFilter<T> : FeatureFilter<T> where T : Feature
    {
        private readonly FeatureFilter<T> _filter;

        /// <summary>
        /// Initializes a new instance of NotFeatureFilter
        /// </summary>
        /// <param name="filter">The filter to use</param>
        /// <exception cref="ArgumentNullException">Thrown if filter is null</exception>
        public NotFilter(FeatureFilter<T> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            _filter = filter;
        }

        /// <inheritdoc/>
        public override bool Filter(T feature)
        {
            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature));
            }

            return !_filter.Filter(feature);
        }
    }
}