using System;
using System.Collections.Generic;
using Maps.Geographical.Features;

namespace Maps.Geographical.Filtering
{
    /// <summary>
    /// Responsible for filtering features
    /// </summary>
    /// <typeparam name="T">The feature type</typeparam>
    public abstract class FeatureFilter<T> where T : Feature
    {
        /// <summary>
        /// Evaluates if the feature passes the filter
        /// </summary>
        /// <param name="feature">The feature to filter</param>
        /// <returns>True if the feature passes the filter, false otherwise</returns>
        /// <exception cref="ArgumentNullException">Thrown if feature is null</exception>
        public abstract bool Filter(T feature);

        /// <summary>
        /// Filters the given features
        /// </summary>
        /// <param name="features">The features to filter</param>
        /// <returns>A filtered list of features</returns>
        /// <exception cref="ArgumentNullException">Thrown if any argument or argument
        /// list element is null</exception>
        public IList<Feature> Filter(IList<T> features)
        {
            if (features == null)
            {
                throw new ArgumentNullException(nameof(features));
            }

            var featureCount = features.Count;
            var filteredFeatures = new List<Feature>();

            for (var i = 0; i < featureCount; ++i)
            {
                if (features[i] == null)
                {
                    throw new ArgumentException($"Contains null element at index {i}",
                        nameof(features));
                }

                if (Filter(features[i]))
                {
                    filteredFeatures.Add(features[i]);
                }
            }

            return filteredFeatures;
        }

        /// <summary>
        /// Returns a combined filter with a logical OR operation
        /// </summary>
        /// <returns>A combined OR filter</returns>
        /// <exception cref="ArgumentNullException">Thrown if lhs or rhs is null
        /// </exception>
        public static FeatureFilter<T> operator |(FeatureFilter<T> lhs, 
            FeatureFilter<T> rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return new OrFilter<T>(rhs, lhs);
        }

        /// <summary>
        /// Returns a combined filter with a logical AND operation
        /// </summary>
        /// <returns>A combined AND filter</returns>
        /// <exception cref="ArgumentNullException">Thrown if lhs or rhs is null
        /// </exception>
        public static FeatureFilter<T> operator &(FeatureFilter<T> lhs,
            FeatureFilter<T> rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return new AndFilter<T>(rhs, lhs);
        }

        /// <summary>
        /// Returns a filter with a logical NOT operation
        /// </summary>
        /// <returns>A NOT filter</returns>
        /// <exception cref="ArgumentNullException">Thrown if rhs is null</exception>
        public static FeatureFilter<T> operator !(FeatureFilter<T> rhs)
        {
            return new NotFilter<T>(rhs);
        }
    }
}