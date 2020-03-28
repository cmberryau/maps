using System;
using System.Collections.Generic;
using Maps.Geographical.Features;
using Maps.Unity.Extensions;
using Maps.Unity.Rendering;
using Maps.Unity.Threading;
using UnityEngine;

namespace Maps.Unity.Features
{
    /// <summary>
    /// Responsible for the implementation of managing dynamic features on a map
    /// </summary>
    internal sealed class DynamicFeatureManagerImpl : DynamicFeatureManagerBase, IDisposable
    {
        private readonly TranslatorFactory _factory;
        private readonly GameObject _gameObject;
        private readonly IList<DynamicFeatureContainer> _containers;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of DynamicFeatureManagerImpl
        /// </summary>
        /// <param name="factory">The translator factory to use</param>
        /// <param name="gameObject">The game object to parent objects to</param>
        public DynamicFeatureManagerImpl(TranslatorFactory factory, GameObject gameObject)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            _containers = new List<DynamicFeatureContainer>();
            _gameObject = gameObject;
            _factory = factory;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(DynamicFeatureManagerImpl));
            }

            foreach (var container in _containers)
            {
                if (!container.Disposed)
                {
                    container.Dispose();
                }
            }

            _disposed = true;
            _gameObject.SafeDestroy();
        }

        /// <inheritdoc />
        protected override void OnAdded(IDynamicFeature feature)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(DynamicFeatureManagerImpl));
            }

            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature));
            }

            // create the renderables
            Coroutines.Queue(() => CreateDynamicFeatureObject(feature));
        }

        /// <inheritdoc />
        protected override void OnShouldAssumeTransform()
        {
            _gameObject.transform.localPosition = Transform.LocalPosition.Vector3();
            _gameObject.transform.localRotation = Transform.LocalRotation.Quaternion();
            _gameObject.transform.localScale = Transform.LocalScale.Vector3();
        }

        private void CreateDynamicFeatureObject(IDynamicFeature feature)
        {
            // must be attached to a map
            if (Map == null)
            {
                return;
            }

            // create the game object for the feature
            var gameObject = new GameObject(feature.Name);
            gameObject.transform.SetParent(_gameObject.transform);
            gameObject.layer = _gameObject.layer;

            // create and initialize the container for the feature
            var container = gameObject.AddComponent<DynamicFeatureContainer>();
            if (container == null)
            {
                throw new InvalidOperationException($"Could not add {nameof(DynamicFeatureContainer)}");
            }
            _containers.Add(container);

            // the feature's transform takes on the managers transform as a parent
            var transform = Transformd.Identity;
            transform.SetParent(Transform);
            container.Initialize(feature, Map, transform, _factory);
        }
    }
}