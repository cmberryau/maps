using System;
using System.Collections.Generic;
using Maps.Geographical.Projection;
using Maps.Rendering;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// Delegate for coordinate changes
    /// </summary>
    /// <param name="coordinate">The new coordinate</param>
    public delegate void CoordinateChangeHandler(Geodetic3d coordinate);

    /// <summary>
    /// Delegate for heading changes
    /// </summary>
    /// <param name="heading">The new heading</param>
    public delegate void HeadingChangeHandler(double heading);

    /// <summary>
    /// Delegate for shown changes
    /// </summary>
    /// <param name="shown">Is the feature shown?</param>
    public delegate void ActiveChangeHandler(bool shown);

    /// <summary>
    /// Delegate for disposal
    /// </summary>
    public delegate void DisposedHandler();

    /// <summary>
    /// Interface for dynamic features
    /// </summary>
    public interface IDynamicFeature : IGeodeticallyControllable, IDisposable
    {
        /// <summary>
        /// Called when the dynamic feature's coordinate has changed
        /// </summary>
        event CoordinateChangeHandler CoordinateChanged;

        /// <summary>
        /// Called when the dynamic feature's heading has changed
        /// </summary>
        event HeadingChangeHandler HeadingChanged;

        /// <summary>
        /// Called when the dynamic feature's active state has changed
        /// </summary>
        event ActiveChangeHandler ActiveChanged;

        /// <summary>
        /// Called when the dynamic feature has been disposed
        /// </summary>
        event DisposedHandler OnDisposed;

        /// <summary>
        /// The name of the dynamic feature
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Is the dynamic feature active?
        /// </summary>
        bool Active
        {
            get;
            set;
        }

        /// <summary>
        /// Should the visual representation of the dynamic feature rotate with heading
        /// changes?
        /// </summary>
        bool ShouldHeadingRotate
        {
            get;
        }

        /// <summary>
        /// Is the dynamic feature disposed?
        /// </summary>
        bool Disposed
        {
            get;
        }

        /// <summary>
        /// Evaluates a list of Renderables
        /// </summary>
        IList<Renderable> Renderables(IProjection projection);
    }
}