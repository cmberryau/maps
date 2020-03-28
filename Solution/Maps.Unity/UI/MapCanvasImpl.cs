using System;
using UnityEngine;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Responsible for the implementation of holding a Canvas instance for use 
    /// with Maps.Unity.Map
    /// </summary>
    internal sealed class MapCanvasImpl
    {
        private const float DefaultPlaneDistance = 10f;

        /// <summary>
        /// The canvas used by the MapCanvasImpl
        /// </summary>
        public readonly Canvas Canvas;

        /// <summary>
        /// Initializes a new instance of MapCanvasImpl
        /// </summary>
        /// <param name="canvas">The unity canvas to work with</param>
        /// <param name="camera">The unity camera to work with</param>
        public MapCanvasImpl(Canvas canvas, Camera camera)
        {
            if (canvas == null)
            {
                throw new ArgumentNullException(nameof(canvas));
            }

            if (camera == null)
            {
                throw new ArgumentNullException(nameof(camera));
            }

            // setup required canvas properties
            canvas.worldCamera = camera;
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.planeDistance = DefaultPlaneDistance;
            Canvas = canvas;
        }
    }
}