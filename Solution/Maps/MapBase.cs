using System;
using Maps.Geographical;
using Maps.Geographical.Features;
using Maps.Geographical.Projection;
using Maps.Rendering;

namespace Maps 
{
    /// <summary>
    /// Responsible for the base map functionality
    /// </summary>
    public abstract class MapBase : IMap
    {
        #region IMap

        /// <inheritdoc />
        public event MapCoordinateChangeHandler CoordinateChanged;

        /// <inheritdoc />
        public event MapHeadingChangeHandler HeadingChanged;

        /// <inheritdoc />
        public event MapTiltChangeHandler TiltChanged;

        /// <inheritdoc />
        public Geodetic3d Coordinate
        {
            get => _coordinate;
            set
            {
                // don't update if the coordinate has not changed
                if (_coordinate == value)
                {
                    return;
                }

                // clamp height
                if (value.Height < MinimumHeight)
                {
                    value = new Geodetic3d(value.Geodetic2d, MinimumHeight);

                    // check again
                    if (_coordinate == value)
                    {
                        return;
                    }
                }
                else if (value.Height > MaximumHeight)
                {
                    value = new Geodetic3d(value.Geodetic2d, MaximumHeight);

                    // check again
                    if (_coordinate == value)
                    {
                        return;
                    }
                }

                _coordinate = value;
                Transform.LocalPosition = Projection.Forward(_coordinate);
                GroundTransform.LocalPosition = Projection.Forward(_coordinate.Geodetic2d);

                // fire the coordinate changed event
                if (CoordinateChanged != null)
                {
                    CoordinateChanged(_coordinate);
                }

                OnCoordinateChanged(_coordinate);
            }
        }

        /// <inheritdoc />
        public double Heading
        {
            get => _heading;
            set
            {
                // clamp within 360
                value %= 360d;

                if (Mathd.EpsilonEquals(_heading, value))
                {
                    return;
                }

                var rel = value - _heading;
                Transform.Rotate(Vector3d.Forward, rel);
                GroundTransform.Rotate(Vector3d.Forward, rel);
                _heading = value;

                // fire the heading changed event
                if (HeadingChanged != null)
                {
                    HeadingChanged(rel);
                }

                OnHeadingChanged(rel);
            }
        }

        /// <summary>
        /// The tilt of the map
        /// </summary>
        public double Tilt
        {
            get => _tilt;
            set
            {
                // clamp within 360, then 0-45
                value = Mathd.Clamp(value % 360d, 0d, MaximumTilt);

                if (Mathd.EpsilonEquals(_tilt, value))
                {
                    return;
                }

                var rel = value - _tilt;
                Transform.Rotate(Vector3d.Right, rel, true);
                GroundTransform.Rotate(Vector3d.Right, rel, true);
                _tilt = value;

                // fire the heading changed event
                if (TiltChanged != null)
                {
                    TiltChanged(rel);
                }

                OnTiltChanged(rel);
            }
        }

        /// <inheritdoc />
        public abstract IProjection Projection
        {
            get;
        }

        /// <inheritdoc />
        public IMapCamera Camera
        {
            get => _camera;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Camera));
                }

                _camera = value;

                // grab the initial visible area with a forced lod change
                OnCameraVisibleAreaChanged(value.VisibleArea, true);
                // subscribe to camera visible area updates
                value.VisibleAreaChanged += OnCameraVisibleAreaChanged;
            }
        }

        /// <inheritdoc />
        public abstract double Scale
        {
            get;
        }

        /// <inheritdoc />
        public abstract double InverseScale
        {
            get;
        }

        /// <inheritdoc />
        public Transformd Transform
        {
            get;
        }

        /// <inheritdoc />
        public Transformd GroundTransform
        {
            get;
        }

        #endregion IMap

        private const double MinimumHeight = 50d;
        private const double MaximumHeight = 10000d;
        private const double MaximumTilt = 45d;
        private IMapCamera _camera;
        private Geodetic3d _coordinate;
        private GeodeticBox2d _visibleArea;
        private double _heading;
        private double _tilt;

        /// <summary>
        /// Initializes a new instance of DefaultMapImplementation
        /// </summary>
        /// <param name="coordinate">The initial coordinate</param>
        protected MapBase(Geodetic3d coordinate)
        {
            Transform = Transformd.Identity;
            GroundTransform = Transformd.Identity;
            _coordinate = coordinate;
        }

        /// <summary>
        /// Called when the visible area changes
        /// </summary>
        /// <param name="area">The newly visible area</param>
        /// <param name="lodChange">Can a lod change be triggered?</param>
        protected virtual void OnVisibleAreaChanged(GeodeticBox2d area, bool lodChange)
        {

        }

        /// <summary>
        /// Called when the coordinate changes
        /// </summary>
        /// <param name="coordinate">The new coordinate</param>
        protected virtual void OnCoordinateChanged(Geodetic3d coordinate)
        {

        }

        /// <summary>
        /// Called when the heading changes
        /// </summary>
        /// <param name="delta">The change of heading in degrees</param>
        protected virtual void OnHeadingChanged(double delta)
        {

        }

        /// <summary>
        /// Called when the tilt changes
        /// </summary>
        /// <param name="delta">The change of tilt in degrees</param>
        protected virtual void OnTiltChanged(double delta)
        {

        }

        /// <inheritdoc />
        public abstract void Add(IDynamicFeature feature);

        private void OnCameraVisibleAreaChanged(GeodeticBox2d area, bool lodChange)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            // don't update if the area has not changed
            if (area == _visibleArea || area == GeodeticBox2d.Zero)
            {
                return;
            }

            _visibleArea = area;
            OnVisibleAreaChanged(area, lodChange);
        }
    }
}