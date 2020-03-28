using System;
using Maps.Geographical;
using Maps.Geographical.Projection;
using Maps.Geometry;

namespace Maps.Rendering
{
    /// <summary>
    /// Responsible for the Camera and Map relationship
    /// </summary>
    public abstract class MapCameraBase : IMapCamera
    {
        /// <inheritdoc />
        public GeodeticBox2d VisibleArea => _visibleArea;

        /// <inheritdoc />
        public event VisibleAreaChangeHandler VisibleAreaChanged;

        /// <inheritdoc />
        public event ClipPlanesChangeHandler ClipPlanesChanged;

        /// <summary>
        /// The far clip of the camera
        /// </summary>
        protected double Far;

        /// <summary>
        /// The near clip of the camera
        /// </summary>
        protected double Near;

        private const double NearDefault = 0.3;
        private const int ExpectedIntersectionCount = 4;
        private static readonly Vector3d NdcTopLeft = new Vector3d(-1d, 1d, 0d);
        private static readonly Vector3d NdcTopRight = new Vector3d(1d, 1d, 0d);
        private static readonly Vector3d NdcBottomRight = new Vector3d(1d, -1d, 0d);
        private static readonly Vector3d NdcBottomLeft = new Vector3d(-1d, -1d, 0d);

        // camera info
        private Matrix4d _inverseProjection;
        private readonly Transformd _transform;

        // geographical info
        private readonly IMap _map;
        private Geodetic3d _coordinate;
        private GeodeticBox2d _visibleArea;

        /// <summary>
        /// Initializes a new instance of MapCameraBase
        /// </summary>
        /// <param name="map">The map the camera is viewing</param>
        /// <param name="transform">The camera transform</param>
        /// <param name="projection">The camera projection matrix</param>
        protected MapCameraBase(IMap map, Transformd transform, Matrix4d projection)
        {
            if (map == null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            // get the coordinate from the map
            _coordinate = map.Coordinate;

            // create the start transform
            _transform = new Transformd(map.Projection.Forward(_coordinate),
                transform.LocalRotation, transform.LocalScale);

            // set the starting matrices
            _inverseProjection = projection.Inverse;

            _map = map;
            _lastMapPosition = _map.Transform.Position;
            _map.CoordinateChanged += OnMapCoordinateChanged;
            _map.HeadingChanged += OnMapHeadingChanged;
            _map.TiltChanged += OnTiltChanged;

            Far = 50d;
            //Far = map.Projection.Extents.Magnitude;
            Near = NearDefault;

            // update the virtual view with a forced lod change
            UpdateVirtualView(true);
        }

        /// <summary>
        /// Called when the camera transform should change
        /// </summary>
        /// <param name="transform">The new camera transform</param>
        protected abstract void OnShouldAssumeTransform(Transformd transform);

        /// <summary>
        /// Called to draw a debug ray
        /// </summary>
        /// <param name="ray">The ray to draw</param>
        protected abstract void DrawDebugRay(Ray3d ray);

        /// <summary>
        /// Should be called when the camera projection matrix has changed
        /// </summary>
        /// <param name="cameraProjection">The new camera projection matrix</param>
        protected void OnProjectionChanged(Matrix4d cameraProjection)
        {
            if (cameraProjection == null)
            {
                throw new ArgumentNullException(nameof(cameraProjection));
            }

            _inverseProjection = cameraProjection.Inverse;
        }

        /// <summary>
        /// Should be called when the camera transform has been changed externally
        /// </summary>
        /// <param name="transform">The new camera transform</param>
        protected void OnTransformChange(Transformd transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }
            
            // if in relative mode, camera should reset back to origin
            OnShouldAssumeTransform(Transformd.Identity);
        }

        private Vector3d _lastMapPosition;

        private void OnMapCoordinateChanged(Geodetic3d coordinate)
        {
            _transform.Translate(_map.Transform.LocalPosition - 
                _lastMapPosition, true);
            // lod change possibility should be evaluated
            UpdateVirtualView(CanLodChange(coordinate));

            _lastMapPosition = _map.Transform.Position;
            _coordinate = coordinate;
        }

        private void OnMapHeadingChanged(double delta)
        {
            _transform.RotateAround(_map.GroundTransform.LocalPosition, 
                Vector3d.Forward, -delta);
            // do not perform lod changes on a heading change
            UpdateVirtualView(false);
        }

        private void OnTiltChanged(double delta)
        {
            _transform.RotateAround(_map.GroundTransform.LocalPosition, 
                Vector3d.Right, -delta);
            // do not perform lod changes on a tilt change
            UpdateVirtualView(false);
        }

        private void UpdateVirtualView(bool lodChange)
        {
            var intersections = ResolveIntersections(_map.Projection, _transform, 
                _inverseProjection);

            if (intersections != null)
            {
                _visibleArea = ResolveVisibleArea(_map.Projection, intersections);

                if (VisibleAreaChanged != null)
                {
                    VisibleAreaChanged(_visibleArea, lodChange);
                }
            }
        }

        private Vector3d[] ResolveIntersections(IProjection projection, 
            Transformd transform, Matrix4d inverseCamProjection)
        {
            // get the view position of the extents of the normalized device coords
            var viewTopLeft = inverseCamProjection * NdcTopLeft;
            var viewTopRight = inverseCamProjection * NdcTopRight;
            var viewBottomRight = inverseCamProjection * NdcBottomRight;
            var viewBottomLeft = inverseCamProjection * NdcBottomLeft;

            // remove any z component
            viewTopLeft = new Vector3d(viewTopLeft.x, viewTopLeft.y, 1d);
            viewTopRight = new Vector3d(viewTopRight.x, viewTopRight.y, 1d);
            viewBottomRight = new Vector3d(viewBottomRight.x, viewBottomRight.y, 1d);
            viewBottomLeft = new Vector3d(viewBottomLeft.x, viewBottomLeft.y, 1d);

            // create the rays for the viewing volume
            var rays = new []
            {
                new Ray3d(transform.LocalPosition, transform.Matrix * viewTopLeft - 
                    transform.LocalPosition),
                new Ray3d(transform.LocalPosition, transform.Matrix * viewTopRight - 
                    transform.LocalPosition),
                new Ray3d(transform.LocalPosition, transform.Matrix * viewBottomRight - 
                    transform.LocalPosition),
                new Ray3d(transform.LocalPosition, transform.Matrix * viewBottomLeft - 
                    transform.LocalPosition)
            };

            //DrawDebugRay(rays[0]);
            //DrawDebugRay(rays[1]);
            //DrawDebugRay(rays[2]);
            //DrawDebugRay(rays[3]);

            // resolve the world intersections
            var intersections = new Vector3d[ExpectedIntersectionCount];
            var allNaN = true;
            for (var i = 0; i < ExpectedIntersectionCount; ++i)
            {
                if (!projection.Intersection(rays[i], out intersections[i]))
                {
                    intersections[i] = Vector3d.NaN;
                }
                else
                {
                    allNaN = false;
                }
            }

            return allNaN ? null : intersections;
        }

        private static GeodeticBox2d ResolveVisibleArea(IProjection projection,
            Vector3d[] intersections)
        {
            return GeodeticBox2d.Encompass(projection.Reverse(intersections));
        }

        private bool CanLodChange(Geodetic3d coordinate)
        {
            return Math.Abs(coordinate.Height - _coordinate.Height) > Mathd.Epsilon;
        }
    }
}