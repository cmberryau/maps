using Maps.Appearance;
using Maps.Geographical;
using Maps.Geographical.Features;
using Maps.Geographical.Projection;
using Maps.Rendering;
using Maps.Unity.Logging;
using UnityEngine;

namespace Maps.Unity
{
    /// <summary>
    /// Responsible for bringing a map to a Unity scene
    /// </summary>
    public sealed class Map : MonoBehaviour, ITiledMap, IGeodeticController
    {
        #region ITiledMap
        /// <inheritdoc />
        public Geodetic3d Coordinate
        {
            get => _impl.Coordinate;
            set => _impl.Coordinate = value;
        }

        /// <inheritdoc />
        public double Heading
        {
            get => _impl.Heading;
            set => _impl.Heading = value;
        }

        /// <inheritdoc />
        public event MapCoordinateChangeHandler CoordinateChanged
        {
            add => _impl.CoordinateChanged += value;
            remove => _impl.CoordinateChanged -= value;
        }

        /// <inheritdoc />
        public event MapHeadingChangeHandler HeadingChanged
        {
            add => _impl.HeadingChanged += value;
            remove => _impl.HeadingChanged -= value;
        }

        /// <inheritdoc />
        public event MapTiltChangeHandler TiltChanged
        {
            add => _impl.TiltChanged += value;
            remove => _impl.TiltChanged -= value;
        }

        /// <inheritdoc />
        public IProjection Projection => _impl.Projection;

        /// <inheritdoc />
        public IMapCamera Camera
        {
            get => _impl.Camera;
            set => _impl.Camera = value;
        }

        /// <inheritdoc />
        public double Tilt
        {
            get => _impl.Tilt;
            set => _impl.Tilt = value;
        }

        /// <inheritdoc />
        public Transformd Transform => _impl.Transform;

        /// <inheritdoc />
        public Transformd GroundTransform => _impl.GroundTransform;

        /// <inheritdoc />
        public void Add(IDynamicFeature feature)
        {
            _impl.Add(feature);
        }

        /// <inheritdoc />
        public TiledMapAppearance Appearance
        {
            get => _impl.Appearance;
            set => _impl.Appearance = value;
        }

        /// <inheritdoc />
        public int Lod => _impl.Lod;

        /// <inheritdoc />
        public event MapLodChangedHandler LodChanged
        {
            add => _impl.LodChanged += value;
            remove => _impl.LodChanged -= value;
        }

        /// <inheritdoc />
        public double Scale => _impl.Scale;

        /// <inheritdoc />
        public double InverseScale => _impl.InverseScale;
        #endregion ITiledMap

        #region IGeodeticController

        /// <inheritdoc />
        public void MoveUp(double meters)
        {
            _geoController.MoveUp(meters);
        }

        /// <inheritdoc />
        public void MoveDown(double meters)
        {
            _geoController.MoveDown(meters);
        }

        /// <inheritdoc />
        public void MoveNorth(double meters)
        {
            _geoController.MoveNorth(meters);
        }

        /// <inheritdoc />
        public void MoveSouth(double meters)
        {
            _geoController.MoveSouth(meters);
        }

        /// <inheritdoc />
        public void MoveEast(double meters)
        {
            _geoController.MoveEast(meters);
        }

        /// <inheritdoc />
        public void MoveWest(double meters)
        {
            _geoController.MoveWest(meters);
        }

        /// <inheritdoc />
        public void MoveForward(double meters)
        {
            _geoController.MoveForward(meters);
        }

        /// <inheritdoc />
        public void MoveBackward(double meters)
        {
            _geoController.MoveBackward(meters);
        }

        /// <inheritdoc />
        public void MoveRight(double meters)
        {
            _geoController.MoveRight(meters);
        }

        /// <inheritdoc />
        public void MoveLeft(double meters)
        {
            _geoController.MoveLeft(meters);
        }

        /// <inheritdoc />
        public void FaceNorth()
        {
            _geoController.FaceNorth();
        }

        /// <inheritdoc />
        public void FaceSouth()
        {
            _geoController.FaceSouth();
        }

        /// <inheritdoc />
        public void FaceEast()
        {
            _geoController.FaceEast();
        }

        /// <inheritdoc />
        public void FaceWest()
        {
            _geoController.FaceWest();
        }

        /// <inheritdoc />
        public void TurnClockwise(double degrees)
        {
            _geoController.TurnClockwise(degrees);
        }

        /// <inheritdoc />
        public void TurnCounterclockwise(double degrees)
        {
            _geoController.TurnCounterclockwise(degrees);
        }
        #endregion IGeodeticController

        /// <summary>
        /// The initial latitude of the map
        /// </summary>
        [Header("Initial Settings")]
        [Range(-90f, 90f)]
        public double InitialLatitude;

        /// <summary>
        /// The initial longitude of the map
        /// </summary>
        [Range(-180f, 180f)]
        public double InitialLongitude;

        /// <summary>
        /// The intial height of the map viewpoint
        /// </summary>
        [Range(1f, 100000f)]
        public double InitialHeight;

        private MapImpl _impl;
        private GeodeticController _geoController;

        /// <summary>
        /// Recenters the map to the Initial latitude, longituide and height
        /// </summary>
        public void Recenter()
        {
            _impl.Coordinate = new Geodetic3d(InitialLatitude, InitialLongitude, InitialHeight);
        }

        /// <summary>
        /// Resets the Map, disposing all resources and re-initializing. Do not use to re-center the map, instead use Center()
        /// </summary>
        public void Reset()
        {
            Dispose();
            Initialize();
        }

        private void Initialize()
        {
            _impl = new MapImpl(InitialLatitude, InitialLongitude, InitialHeight, gameObject);
            _geoController = new GeodeticController(_impl);
        }

        private void Dispose()
        {
            if (_impl != null)
            {
                _impl.Dispose();
                _impl = null;
            }
        }

        private void Start()
        {
            LoggingConfiguration.Initialize();
            Initialize();
        }

        private void Update()
        {
            if (_impl != null)
            {
                _impl.Update();
            }
        }

        private void OnApplicationQuit()
        {
            Dispose();
        }
    }
}