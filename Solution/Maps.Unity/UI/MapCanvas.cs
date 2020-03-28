using System;
using UnityEngine;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Responsible for holding a Canvas instance for use with Maps.Unity.Map
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public class MapCanvas : MonoBehaviour
    {
        /// <summary>
        /// The canvas used by the MapCanvas
        /// </summary>
        public Canvas Canvas => _impl.Canvas;

        private MapCanvasImpl _impl;

        /// <summary>
        /// Initializes the MapCanvas
        /// </summary>
        /// <param name="camera">The unity camera to view the map</param>
        public void Initialize(Camera camera)
        {
            var canvas = GetComponent<Canvas>();

            if (canvas == null)
            {
                throw new NullReferenceException($"Could not find a {nameof(Canvas)}, {nameof(MapCanvas)} requires an attached {nameof(Canvas)}");
            }

            _impl = new MapCanvasImpl(canvas, camera);
        }
    }
}