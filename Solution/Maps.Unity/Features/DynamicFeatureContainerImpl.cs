using System;
using System.Collections.Generic;
using Maps.Geographical;
using Maps.Geographical.Features;
using Maps.Rendering;
using Maps.Unity.Extensions;
using Maps.Unity.Rendering;
using Maps.Unity.Threading;
using UnityEngine;

namespace Maps.Unity.Features
{
    /// <summary>
    /// Responsible for the implementation of holding a dynamic feature within Unity
    /// </summary>
    internal sealed class DynamicFeatureContainerImpl : IDisposable
    {
        /// <summary>
        /// Has the container been disposed?
        /// </summary>
        public bool Disposed
        {
            get;
            private set;
        }

        private readonly IDynamicFeature _feature;
        private readonly Transformd _transform;
        private readonly ITiledMap _map;
        private readonly TranslatorFactory _factory;

        private readonly GameObject _gameObject;
        private readonly IDictionary<int, GameObject> _lodGameObjects;

        private Vector3d _position;
        private Quaterniond _rotation;

        /// <summary>
        /// Initializes a new instance of DynamicFeatureImpl
        /// </summary>
        /// <param name="feature">The feature to capture</param>
        /// <param name="map">The map the feature belongs to</param>
        /// <param name="transform">The transform for the feature</param>
        /// <param name="gameObject">The game object containing the feature</param>
        /// <param name="factory">The translator factory</param>
        public DynamicFeatureContainerImpl(IDynamicFeature feature, ITiledMap map, Transformd transform, GameObject gameObject, TranslatorFactory factory)
        {
            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature));
            }

            if (map == null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            _lodGameObjects = new Dictionary<int, GameObject>();
            _position = map.Projection.Forward(feature.Coordinate);
            _feature = feature;
            _transform = transform;
            _map = map;
            _gameObject = gameObject;
            _factory = factory;

            _feature.OnDisposed += Dispose;
            _feature.CoordinateChanged += OnFeatureCoordinateChanged;
            _feature.HeadingChanged += OnFeatureHeadingChanged;
            _feature.ActiveChanged += OnActiveChanged;
            _map.CoordinateChanged += OnMapCoordinateChanged;
            _map.HeadingChanged += OnMapHeadingChanged;
            _map.TiltChanged += OnMapTiltChanged;
            _map.LodChanged += OnMapLodChanged;

            OnMapLodChanged(0, _map.Lod);

            UpdateTransform();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(nameof(DynamicFeatureContainer));
            }

            _feature.OnDisposed -= Dispose;
            _feature.CoordinateChanged -= OnFeatureCoordinateChanged;
            _feature.HeadingChanged -= OnFeatureHeadingChanged;
            _feature.ActiveChanged -= OnActiveChanged;
            _map.CoordinateChanged -= OnMapCoordinateChanged;
            _map.HeadingChanged -= OnMapHeadingChanged;
            _map.TiltChanged -= OnMapTiltChanged;
            _map.LodChanged -= OnMapLodChanged;

            Disposed = true;
            _gameObject.SafeDestroy();
        }

        private void OnFeatureCoordinateChanged(Geodetic3d coordinate)
        {
            _position = _map.Projection.Forward(coordinate);
            UpdateTransform();
        }

        private void OnActiveChanged(bool active)
        {
            _gameObject.SetActive(active);
        }

        private void OnFeatureHeadingChanged(double heading)
        {
            if (_feature.ShouldHeadingRotate)
            {
                _transform.LocalRotation = Quaterniond.AxisAngle(Vector3d.Back, heading);
            }
        }

        private void OnMapCoordinateChanged(Geodetic3d coordinate)
        {
            UpdateTransform();
        }

        private void OnMapHeadingChanged(double heading)
        {
            UpdateTransform();
        }

        private void OnMapTiltChanged(double tilt)
        {
            UpdateTransform();
        }

        private void OnMapLodChanged(int oldLod, int newLod)
        {
            if (_lodGameObjects.TryGetValue(oldLod, out GameObject gameObject))
            {
                gameObject.SetActive(false);
            }

            if (!_lodGameObjects.ContainsKey(newLod))
            {
                var lodGameObject = new GameObject($"LOD_{newLod}");

                lodGameObject.layer = _gameObject.layer;
                lodGameObject.transform.SetParent(_gameObject.transform, false);
                lodGameObject.SetActive(true);

                _lodGameObjects[newLod] = lodGameObject;

                // create our translator
                var translator = _factory.Create(_transform);

                // create the renderables for the feature
                var renderables = _feature.Renderables(_map.Projection);

                // finally, make the renderables relative
                var relativeRenderables = new List<Renderable>();
                foreach (var renderable in renderables)
                {
                    relativeRenderables.Add(renderable.Relative(
                        _map.Projection.Forward(_feature.Coordinate), _map.InverseScale));
                }

                // submit the renderables to be translated and attach results to the game object
                translator.Submit(relativeRenderables);
                translator.Translate(lodGameObject);
            }
            else
            {
                _lodGameObjects[newLod].SetActive(true);
            }
        }

        private void UpdateTransform()
        {
            if (Coroutines.IsUnityThread)
            {
                UpdateTransformImpl();
            }
            else
            {
                Coroutines.Queue(UpdateTransformImpl);
            }
        }

        private void UpdateTransformImpl()
        {
            _transform.LocalPosition = Vector3d.Relative(_map.GroundTransform.LocalPosition, _position, _map.InverseScale);
            _gameObject.transform.localPosition = _transform.LocalPosition.Vector3();
            _gameObject.transform.localRotation = _transform.LocalRotation.Quaternion();
            _gameObject.transform.localScale = _transform.LocalScale.Vector3();
        }
    }
}