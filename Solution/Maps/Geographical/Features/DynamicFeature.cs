using System;
using System.Collections.Generic;
using Maps.Extensions;
using Maps.Geographical.Projection;
using Maps.Rendering;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// A dynamic map feature
    /// </summary>
    public abstract class DynamicFeature : IDynamicFeature
    {
        /// <inheritdoc />
        public event CoordinateChangeHandler CoordinateChanged;

        /// <inheritdoc />
        public event HeadingChangeHandler HeadingChanged;

        /// <inheritdoc />
        public event ActiveChangeHandler ActiveChanged;

        /// <inheritdoc />
        public event DisposedHandler OnDisposed;

        /// <inheritdoc />
        public string Name
        {
            get;
        }

        /// <inheritdoc />
        public bool ShouldHeadingRotate
        {
            get;
            protected set;
        }

        /// <inheritdoc />
        public bool Active
        {
            get
            {
                if (Disposed)
                {
                    throw new ObjectDisposedException(nameof(DynamicFeature));
                }

                return _active;
            }
            set
            {
                if (Disposed)
                {
                    throw new ObjectDisposedException(nameof(DynamicFeature));
                }

                if (_active != value)
                {
                    if (ActiveChanged != null)
                    {
                        ActiveChanged(value);
                    }

                    _active = value;
                }
            }
        }

        /// <inheritdoc />
        public Geodetic3d Coordinate
        {
            get
            {
                if (Disposed)
                {
                    throw new ObjectDisposedException(nameof(DynamicFeature));
                }

                return _coordinate;
            }
            set
            {
                if (Disposed)
                {
                    throw new ObjectDisposedException(nameof(DynamicFeature));
                }

                if (_coordinate != value)
                {
                    if (CoordinateChanged != null)
                    {
                        CoordinateChanged(value);
                    }

                    _coordinate = value;
                    OnCoordinateChanged(value);
                }
            }
        }

        /// <inheritdoc />
        public double Heading
        {
            get
            {
                if (Disposed)
                {
                    throw new ObjectDisposedException(nameof(DynamicFeature));
                }

                return _heading;
            }
            set
            {
                if (Disposed)
                {
                    throw new ObjectDisposedException(nameof(DynamicFeature));
                }

                // clamp within 360d
                value %= 360d;

                if (!Mathd.EpsilonEquals(_heading, value))
                {
                    if (HeadingChanged != null)
                    {
                        HeadingChanged(value);
                    }

                    _heading = value;
                    OnHeadingChanged(value);
                }
            }
        }

        /// <inheritdoc />
        public bool Disposed
        {
            get;
            private set;
        }

        private const string EmptyName = "";
        private Geodetic3d _coordinate;
        private double _heading;
        private bool _active;

        /// <summary>
        /// Initializes a new instance of DynamicFeature
        /// </summary>
        /// <param name="name">The name of the dynamic feature</param>
        public DynamicFeature(string name)
        {
            if(name.IsNullOrWhiteSpace())
            {
                Name = EmptyName;
            }

            Name = name;
            Active = true;
            _coordinate = Geodetic3d.Zero;
            _heading = 0d;
        }

        /// <inheritdoc />
        public abstract IList<Renderable> Renderables(IProjection projection);

        /// <inheritdoc />
        public override string ToString()
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(nameof(DynamicFeature));
            }

            return Name;
        }

        /// <inheritdoc />
        public virtual void Dispose()
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(nameof(DynamicFeature));
            }

            if (OnDisposed != null)
            {
                OnDisposed();
            }

            Disposed = true;
        }

        /// <summary>
        /// Called when the feature's coordinate changes
        /// </summary>
        /// <param name="coordinate">The new coordinate</param>
        protected virtual void OnCoordinateChanged(Geodetic3d coordinate)
        {

        }

        /// <summary>
        /// Called when the feature's heading changes
        /// </summary>
        /// <param name="heading">The new heading</param>
        protected virtual void OnHeadingChanged(double heading)
        {

        }
    }
}