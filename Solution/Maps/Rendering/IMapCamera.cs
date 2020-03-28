using Maps.Geographical;

namespace Maps.Rendering
{
    /// <summary>
    /// Delegate for visible area changes
    /// </summary>
    /// <param name="area">The new visible area</param>
    /// <param name="lodChange">Should this trigger a lod change?</param>
    public delegate void VisibleAreaChangeHandler(GeodeticBox2d area, bool lodChange);

    /// <summary>
    /// Delegate for clip plane changes
    /// </summary>
    /// <param name="far">The far clip plane</param>
    /// <param name="near">The near clip plane</param>
    public delegate void ClipPlanesChangeHandler(double far, double near);

    /// <summary>
    /// Interface for objects with the responsibility of the Camera and Map 
    /// relationship
    /// </summary>
    public interface IMapCamera
    {
        /// <summary>
        /// The visible area of the camera
        /// </summary>
        GeodeticBox2d VisibleArea
        {
            get;
        }

        /// <summary>
        /// Called when the visible area of the map camera has changed
        /// </summary>
        event VisibleAreaChangeHandler VisibleAreaChanged;

        /// <summary>
        /// Called when the clip planes have been changed
        /// </summary>
        event ClipPlanesChangeHandler ClipPlanesChanged;
    }
}