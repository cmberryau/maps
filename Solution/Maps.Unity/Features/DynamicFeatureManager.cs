using System;
using Maps.Geographical.Features;
using Maps.Unity.Rendering;
using UnityEngine;

namespace Maps.Unity.Features
{
    /// <summary>
    /// Responsible for managing dynamic features on a map
    /// </summary>
    public class DynamicFeatureManager : MonoBehaviour, IDynamicFeatureManager, IDisposable
    {
        /// <inheritdoc />
        public Transformd Transform => _impl.Transform;

        private DynamicFeatureManagerImpl _impl;
        private bool _disposed;

        /// <inheritdoc />
        public void Initialize(TranslatorFactory translatorFactory)
        {
            if (translatorFactory == null)
            {
                throw new ArgumentNullException(nameof(translatorFactory));
            }

            _impl = new DynamicFeatureManagerImpl(translatorFactory, gameObject);
        }

        /// <inheritdoc />
        public void AttachTo(ITiledMap map)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(DynamicFeatureManager));
            }

            if (map == null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            _impl.AttachTo(map);
        }

        /// <inheritdoc />
        public void Add(IDynamicFeature feature)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(DynamicFeatureManager));
            }

            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature));
            }

            _impl.Add(feature);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(DynamicFeatureManager));
            }

            if (_impl != null)
            {
                _impl.Dispose();
                _impl = null;
                _disposed = true;
            }
        }
    }
}