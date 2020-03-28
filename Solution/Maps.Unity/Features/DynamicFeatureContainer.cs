using System;
using Maps.Geographical.Features;
using Maps.Unity.Rendering;
using UnityEngine;

namespace Maps.Unity.Features
{
    /// <summary>
    /// Responsible for holding a dynamic feature within Unity
    /// </summary>
    public class DynamicFeatureContainer : MonoBehaviour, IDisposable
    {
        /// <summary>
        /// Has the container been disposed?
        /// </summary>
        public bool Disposed => _impl.Disposed;

        private DynamicFeatureContainerImpl _impl;

        /// <summary>
        /// Initializes the DynamicFeature instance
        /// </summary>
        /// <param name="feature">The feature to capture</param>
        /// <param name="map">The map the feature belongs to</param>
        /// <param name="anchor">The transform for the feature</param>
        /// <param name="translatorFactory">The translator factory</param>
        public void Initialize(IDynamicFeature feature, ITiledMap map, Transformd anchor,
            TranslatorFactory translatorFactory)
        {
            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature));
            }

            if (map == null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            if (anchor == null)
            {
                throw new ArgumentNullException(nameof(anchor));
            }

            _impl = new DynamicFeatureContainerImpl(feature, map, anchor, gameObject,
                translatorFactory);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _impl.Dispose();
        }
    }
}