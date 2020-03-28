using System;
using System.Collections.Generic;
using Maps.Appearance;
using Maps.Geographical;
using Maps.Geographical.Features;
using Maps.Geographical.Projection;
using Maps.IO;
using Maps.Rendering;
using Maps.Unity.Appearance;
using Maps.Unity.Extensions;
using Maps.Unity.Features;
using Maps.Unity.Interaction.Input;
using Maps.Unity.Interaction.Response;
using Maps.Unity.Lod;
using Maps.Unity.Rendering;
using Maps.Unity.Threading;
using Maps.Unity.UI;
using log4net;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Maps.Unity
{
    /// <summary>
    /// Responsible for the implementation of bringing a map into a Unity scene
    /// </summary>
    public sealed class MapImpl : ITiledMap, IDisposable, IPointerDragResponder, 
        IPointerScrollResponder, IKeyboardPressResponder
    {
        #region TiledMap
        /// <inheritdoc />
        public Geodetic3d Coordinate
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapImpl));
                }

                return _impl.Coordinate;
            }
            set
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapImpl));
                }

                _impl.Coordinate = value;
            }
        }

        /// <inheritdoc />
        public double Heading
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapImpl));
                }

                return _impl.Heading;
            }
            set
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapImpl));
                }

                _impl.Heading = value;
            }
        }

        /// <inheritdoc />
        public event MapCoordinateChangeHandler CoordinateChanged
        {
            add
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapImpl));
                }

                _impl.CoordinateChanged += value;
            }
            remove => _impl.CoordinateChanged -= value;
        }

        /// <inheritdoc />
        public event MapHeadingChangeHandler HeadingChanged
        {
            add
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapImpl));
                }

                _impl.HeadingChanged += value;
            }
            remove => _impl.HeadingChanged -= value;
        }

        /// <inheritdoc />
        public event MapTiltChangeHandler TiltChanged
        {
            add
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapImpl));
                }

                _impl.TiltChanged += value;
            }
            remove => _impl.TiltChanged -= value;
        }

        /// <inheritdoc />
        public IProjection Projection
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapImpl));
                }

                return _impl.Projection;
            }
        }

        /// <inheritdoc />
        public IMapCamera Camera
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapImpl));
                }

                return _impl.Camera;
            }
            set
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapImpl));
                }

                _impl.Camera = value;
            }
        }

        /// <inheritdoc />
        public double Tilt
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapImpl));
                }

                return _impl.Tilt;
            }
            set
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapImpl));
                }

                _impl.Tilt = value;
            }
        }

        /// <inheritdoc />
        public TiledMapAppearance Appearance
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapImpl));
                }

                return _impl.Appearance;
            }
            set
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapImpl));
                }

                _impl.Appearance = value;
            }
        }

        /// <inheritdoc />
        public int Lod => _impl.Lod;

        /// <inheritdoc />
        public event MapLodChangedHandler LodChanged
        {
            add
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapImpl));
                }

                _impl.LodChanged += value;
            }
            remove
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapImpl));
                }

                _impl.LodChanged -= value;
            }
        }

        /// <inheritdoc />
        public double Scale => _impl.Scale;

        /// <inheritdoc />
        public double InverseScale => _impl.InverseScale;

        /// <inheritdoc />
        public Transformd Transform => _impl.Transform;

        /// <inheritdoc />
        public Transformd GroundTransform => _impl.GroundTransform;

        /// <inheritdoc />
        public void Add(IDynamicFeature feature)
        {
            _impl.Add(feature);
        }

        #endregion TiledMap

        private static readonly ILog Log = LogManager.GetLogger(typeof(MapImpl));

        private readonly Coroutines _coroutines;
        private readonly ITiledFeatureSource _source;
        private readonly MapLodTree _lodTree;
        private readonly PrefabPool _prefabPool;
        private readonly TiledMap _impl;
        private readonly DynamicFeatureManager _dynamicManager;
        private readonly InputHandler _inputHandler;
        private bool _disposed;

        /// <summary>
        /// Creates a new instance of MapImpl
        /// </summary>
        /// <param name="latitude">The latitude of the map</param>
        /// <param name="longitude">The longitude of the map</param>
        /// <param name="height">The height of the map</param>
        /// <param name="gameObject">The game object of the map</param>
        public MapImpl(double latitude, double longitude, double height, 
            GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Keys = new HashSet<KeyCode>
            {
                KeyCode.K
            };

            _coroutines = EnsureCoroutines(gameObject);
            _source = EnsureSource();

            var camera = EnsureCamera(gameObject);
            var eventSystem = EnsureEventSystem(gameObject);
            var inputHandler = InputHandler.Create();
            _inputHandler = inputHandler;

            inputHandler.AddResponder(this);
            var mapCanvas = EnsureMapCanvas(gameObject, camera);
            _prefabPool = EnsurePrefabPool(gameObject, EnsurePrefabModel(gameObject));

            var day = TiledMapAppearance.DefaultDay;
            var appearances = new List<TiledMapAppearance>
            {
                day,
            };

            var textureModel = EnsureTexturesModel(gameObject, _source.SideData);
            var uiFactory = EnsureUIElementFactory(appearances, mapCanvas, textureModel);
            var materialsModel = EnsureMaterialsModel(gameObject, appearances);
            var translator = EnsureTranslator(materialsModel, uiFactory, _prefabPool);

            _dynamicManager = EnsureDynamicFeatureManager(gameObject, _impl, translator);
            _lodTree = EnsureLodTree(gameObject, translator, inputHandler);

            var coordinate = new Geodetic3d(latitude, longitude, height);
            _impl = new TiledMap(day, coordinate, _source, _lodTree, _dynamicManager);
            _impl.Camera = EnsureMapCamera(gameObject, _impl, inputHandler);
        }

        /// <summary>
        /// Adds a dynamic feature to the map
        /// </summary>
        /// <param name="feature">The dynamic feature to add</param>
        public void AddDynamicFeature(IDynamicFeature feature)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapImpl));
            }

            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature));
            }

            _dynamicManager.Add(feature);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapImpl));
            }

            _disposed = true;

            _dynamicManager?.Dispose();
            _prefabPool?.Dispose();
            _lodTree?.Dispose();
            _source?.Dispose();
            _coroutines?.Dispose();
        }

        private static Coroutines EnsureCoroutines(GameObject gameObject)
        {
            // search for a coroutines instance in the children
            var coroutines = gameObject.GetComponentInChildren<Coroutines>();

            // if it's null, create one
            if (coroutines == null)
            {
                var coroutinesObject = new GameObject($"{nameof(Coroutines)}_");
                coroutinesObject.layer = gameObject.layer;
                coroutinesObject.transform.SetParent(gameObject.transform, false);
                coroutines = coroutinesObject.AddComponent<Coroutines>();

                if (coroutines == null)
                {
                    throw new Exception($"Could not create {nameof(Coroutines)} instance");
                }
            }

            // initialize coroutines
            coroutines.Initialize();

            return coroutines;
        }

        private static ITiledFeatureSource EnsureSource()
        {
            var source = Configuration.CreateTiledFeatureSource();

            if (source == null)
            {
                throw new Exception($"Could not create {nameof(ITiledFeatureSource)} instance");
            }

            return source;
        }

        private static MapLodTree EnsureLodTree(GameObject gameObject, 
            TranslatorFactory factory, InputHandler inputHandler)
        {
            // search for a lod tree in the children
            var lodTree = gameObject.GetComponentInChildren<MapLodTree>();

            if (lodTree == null)
            {
                var lodTreeObject = new GameObject($"{nameof(MapLodTree)}_");
                lodTreeObject.layer = gameObject.layer;
                lodTreeObject.transform.SetParent(gameObject.transform, false);
                lodTree = lodTreeObject.AddComponent<MapLodTree>();

                if (lodTree == null)
                {
                    throw new Exception($"Could not create {nameof(MapLodTree)} instance");
                }
            }

            // initialize the lodtree
            lodTree.Initialize(factory, inputHandler);

            return lodTree;
        }

        private PrefabModel EnsurePrefabModel(GameObject gameObject)
        {
            var prefabModel = gameObject.GetComponent<PrefabModel>();

            // if it's still null, create one
            if (prefabModel == null)
            {
                prefabModel = gameObject.AddComponent<PrefabModel>();

                if (prefabModel == null)
                {
                    throw new Exception($"Could not create {nameof(PrefabModel)} instance");
                }
            }

            // initialize the ui prefab model
            prefabModel.Initialize();

            return prefabModel;
        }

        private PrefabPool EnsurePrefabPool(GameObject gameObject, PrefabModel model)
        {
            var prefabPool = gameObject.GetComponentInChildren<PrefabPool>();

            if (prefabPool == null)
            {
                var uiPrefabPoolObject = new GameObject($"{nameof(PrefabPool)}_");
                uiPrefabPoolObject.layer = gameObject.layer;
                uiPrefabPoolObject.transform.SetParent(gameObject.transform, false);
                prefabPool = uiPrefabPoolObject.AddComponent<PrefabPool>();

                if (prefabPool == null)
                {
                    throw new Exception($"Could not create {nameof(PrefabPool)} instance");
                }
            }

            // intialize the prefab pool
            prefabPool.Initialize(model);

            return prefabPool;
        }

        private static Camera EnsureCamera(GameObject gameObject)
        {
            // first search for a map camera
            var mapCamera = gameObject.GetComponentInChildren<MapCamera>();
            if (mapCamera == null)
            {
                var mapCameraObject = new GameObject($"{nameof(MapCamera)}_");
                mapCameraObject.layer = gameObject.layer;
                mapCameraObject.transform.SetParent(gameObject.transform, false);
                mapCamera = mapCameraObject.AddComponent<MapCamera>();

                if (mapCamera == null)
                {
                    throw new Exception($"Could not create {nameof(MapCamera)} instance");
                }
            }

            // once we've got a map camera, return it's camera
            var camera = mapCamera.gameObject.GetComponent<Camera>();
            if (camera == null)
            {
                mapCamera.gameObject.AddComponent<Camera>();

                if (camera == null)
                {
                    throw new Exception($"Could not create {nameof(Camera)} instance");
                }
            }

            return camera;
        }

        private static MapCamera EnsureMapCamera(GameObject gameObject, IMap map, InputHandler inputHandler)
        {
            var mapCamera = gameObject.GetComponentInChildren<MapCamera>();

            // if it's still null, create one
            if (mapCamera == null)
            {
                var mapCameraObject = new GameObject($"{nameof(MapCamera)}_");
                mapCameraObject.layer = gameObject.layer;
                mapCameraObject.transform.SetParent(gameObject.transform, false);
                mapCamera = mapCameraObject.AddComponent<MapCamera>();

                if (mapCamera == null)
                {
                    throw new Exception($"Could not create {nameof(MapCamera)} instance");
                }
            }

            // initialize the map camera
            mapCamera.Initialize(map, inputHandler, gameObject.layer);

            return mapCamera;
        }

        private MapCanvas EnsureMapCanvas(GameObject gameObject, Camera camera)
        {
            var mapCanvas = gameObject.GetComponentInChildren<MapCanvas>();

            // if it's still null, create one
            if (mapCanvas == null)
            {
                var mapCanvasObject = new GameObject($"{nameof(MapCanvas)}_");
                mapCanvasObject.layer = gameObject.layer;
                mapCanvasObject.transform.SetParent(gameObject.transform);
                mapCanvas = mapCanvasObject.AddComponent<MapCanvas>();
            }

            if (mapCanvas == null)
            {
                throw new Exception($"Could not create {nameof(MapCanvas)} instance");
            }

            // initialize the map canvas
            mapCanvas.Initialize(camera);

            return mapCanvas;
        }

        private static UIElementFactory EnsureUIElementFactory(IList<TiledMapAppearance> appearances, MapCanvas canvas, ITexture2DModel textureModel)
        {
            var uiAppearances = new List<UIRenderableAppearance>();

            foreach (var appearance in appearances)
            {
                foreach (var uiAppearance in appearance.UIElementAppearances)
                {
                    uiAppearances.Add(uiAppearance);
                }
            }

            return new UIElementFactory(uiAppearances, canvas, textureModel);
        }

        private static TranslatorFactory EnsureTranslator(IMaterialsModel model, UIElementFactory uiFactory, IPrefabPool pool)
        {
            return new TranslatorFactory(model, uiFactory, pool);
        }

        private static ITexture2DModel EnsureTexturesModel(GameObject gameObject, ISideData sideData)
        {
            var model = gameObject.GetComponent<Texture2DModel>();

            // if it's still null, create one
            if (model == null)
            {
                model = gameObject.AddComponent<Texture2DModel>();

                if (model == null)
                {
                    throw new Exception($"Could not create {nameof(Texture2DModel)} instance");
                }
            }

            // initialize the materials model
            model.Initialize(sideData);

            return model;
        }

        private static IMaterialsModel EnsureMaterialsModel(GameObject gameObject, IList<TiledMapAppearance> appearances)
        {
            var model = gameObject.GetComponent<MaterialsModel>();

            // if it's still null, create one
            if (model == null)
            {
                model = gameObject.AddComponent<MaterialsModel>();

                if (model == null)
                {
                    throw new Exception($"Could not create {nameof(MaterialsModel)} instance");
                }
            }

            var meshAppearances = new List<MeshAppearance>();

            foreach (var appearance in appearances)
            {
                foreach (var meshAppearance in appearance.MeshAppearances)
                {
                    meshAppearances.Add(meshAppearance);
                }
            }

            // initialize the materials model
            model.Initialize(meshAppearances);

            return model;
        }

        private static DynamicFeatureManager EnsureDynamicFeatureManager(
            GameObject gameObject, ITiledMap map, TranslatorFactory translatorFactory)
        {
            var manager = gameObject.GetComponentInChildren<DynamicFeatureManager>();

            if (manager == null)
            {
                var dynamicFeatureManagerObject = new GameObject($"{nameof(DynamicFeatureManager)}_");
                dynamicFeatureManagerObject.layer = gameObject.layer;
                dynamicFeatureManagerObject.transform.SetParent(gameObject.transform);
                manager = dynamicFeatureManagerObject.AddComponent<DynamicFeatureManager>();

                if (manager == null)
                {
                    throw new Exception($"Could not create {nameof(DynamicFeatureManager)} instance");
                }
            }

            manager.Initialize(translatorFactory);

            return manager;
        }

        private static EventSystem EnsureEventSystem(GameObject gameObject)
        {
            var eventSystem = EventSystem.current;

            if (eventSystem == null)
            {
                eventSystem = gameObject.GetComponentInChildren<EventSystem>();
                if (eventSystem == null)
                {
                    var eventSystemObject = new GameObject($"{nameof(EventSystem)}_");
                    eventSystemObject.layer = gameObject.layer;
                    eventSystemObject.transform.SetParent(gameObject.transform);
                    eventSystem = eventSystemObject.AddComponent<EventSystem>();

                    if (eventSystem == null)
                    {
                        throw new Exception($"Could not create {nameof(EventSystem)} instance");
                    }

                    // we have to also ensure that the event system has a standalone input module
                    var standaloneInputModule = eventSystemObject.GetComponent<StandaloneInputModule>();
                    if (standaloneInputModule == null)
                    {
                        standaloneInputModule = eventSystemObject.AddComponent<StandaloneInputModule>();

                        if (standaloneInputModule == null)
                        {
                            throw new Exception($"Could not add {nameof(StandaloneInputModule)} component");
                        }
                    }
                }
            }

            return eventSystem;
        }

        /// <inheritdoc />
        public void RecievedPointerDrag(Vector2 startScreenPosition, Vector3 startWorldPosition, Vector2 endScreenPosition, Vector3 endWorldPosition, PointerEventData.InputButton button)
        {
            var start = _impl.Reverse(startWorldPosition.Vector3d()).Geodetic2d;
            var end = _impl.Reverse(endWorldPosition.Vector3d()).Geodetic2d;

            var distance = Geodetic2d.Distance(start, end);
            var heading = (Geodetic2d.Course(start, end) + 180 + Heading) % 360;

            Coordinate = new Geodetic3d(Geodetic2d.Offset(Coordinate.Geodetic2d, 
                distance, heading), Coordinate.Height);
        }

        /// <inheritdoc />
        public void RecievedPointerScroll(Vector2 screenPosition, Vector3 worldPosition, Vector2 scrollDelta)
        {
            Coordinate = new Geodetic3d(Coordinate.Geodetic2d, Coordinate.Height + 
                scrollDelta.y * (Coordinate.Height / 25d));
        }

        /// <inheritdoc />
        public HashSet<KeyCode> Keys
        {
            get;
        }

        /// <inheritdoc />
        public void RecievedKeyboardPress(KeyCode key)
        {
            
        }

        /// <summary>
        /// Should be called to update the map
        /// </summary>
        public void Update()
        {
            _inputHandler.Update();
        }
    }
}