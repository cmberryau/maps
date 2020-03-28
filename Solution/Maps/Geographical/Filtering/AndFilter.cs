using System;
using Maps.Geographical.Features;

namespace Maps.Geographical.Filtering
{
    /// <summary>
    /// Responsible for combining two feature with an AND logic operation
    /// </summary>
    /// <typeparam name="T">The feature type</typeparam>
    public class AndFilter<T> : FeatureFilter<T> where T : Feature
    {
        private readonly FeatureFilter<T> _a;
        private readonly FeatureFilter<T> _b;

        /// <summary>
        /// Initializes a new instance of AndFeatureFilter
        /// </summary>
        /// <param name="a">The first filter</param>
        /// <param name="b">The second filter</param>
        /// <exception cref="ArgumentNullException">Thrown if a or b are null</exception>
        public AndFilter(FeatureFilter<T> a, FeatureFilter<T> b)
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            _a = a;
            _b = b;
        }

        /// <inheritdoc/>
        public override bool Filter(T feature)
        {
            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature));
            }

            return _a.Filter(feature) && _b.Filter(feature);
        }
    }
}