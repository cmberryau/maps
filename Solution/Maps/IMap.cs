using Maps.Geographical;
using Maps.Geographical.Features;
using Maps.Geographical.Projection;
using Maps.Rendering;

namespace Maps
{
    /// <summary>
    /// Delegate for coordinate changes
    /// </summary>
    /// <param name="coordinate">The new coordinate</param>
    public delegate void MapCoordinateChangeHandler(Geodetic3d coordinate);

    /// <summary>
    /// Delegate for heading changes
    /// </summary>
    /// <param name="delta">The change of heading in degrees</param>
    public delegate void MapHeadingChangeHandler(double delta);

    /// <summary>
    /// Delegate for tilt changes
    /// </summary>
    /// <param name="delta">The change of tilt in degrees</param>
    public delegate void MapTiltChangeHandler(double delta);

    /// <summary>
    /// Interface for maps
    /// </summary>
    public interface IMap : IGeodeticallyControllable
    {
        /// <summary>
        /// Called when the coordinate of the map has changed
        /// </summary>
        event MapCoordinateChangeHandler CoordinateChanged;

        /// <summary>
        /// Called when the heading of the map has changed
        /// </summary>
        event MapHeadingChangeHandler HeadingChanged;

        /// <summary>
        /// Called when the tilt of the map has changed
        /// </summary>
        event MapTiltChangeHandler TiltChanged;

        /// <summary>
        /// The tilt of the map
        /// </summary>
        double Tilt
        {
            get;
            set;
        }

        /// <summary>
        /// The projection of the map
        /// </summary>
        IProjection Projection
        {
            get;
        }

        /// <summary>
        /// The current scale of the map relative to it's projection
        /// </summary>
        double Scale
        {
            get;
        }

        /// <summary>
        /// The current inverse scale of the map relative to it's projection
        /// </summary>
        double InverseScale
        {
            get;
        }

        /// <summary>
        /// The camera for the map
        /// </summary>
        IMapCamera Camera
        {
            set;
            get;
        }

        /// <summary>
        /// The transform of the map (projected coordinate and orientation)
        /// </summary>
        Transformd Transform
        {
            get;
        }

        /// <summary>
        /// The flattened transform of the map (projected coordinate at ground height and orientation)
        /// </summary>
        Transformd GroundTransform
        {
            get;
        }

        /// <summary>
        /// Adds a dynamic feature to the map
        /// </summary>
        /// <param name="feature">The dynamic feature to add</param>
        void Add(IDynamicFeature feature);
    }
}