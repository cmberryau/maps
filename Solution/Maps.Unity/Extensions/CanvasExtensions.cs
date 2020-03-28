using System;
using UnityEngine;
using UnityEngine.UI;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Provides extensions for the UnityEngine.Canvas class
    /// </summary>
    public static class CanvasExtensions
    {
        /// <summary>
        /// Evaluates a canvas point for a world space point
        /// </summary>
        /// <param name="canvas">The canvas on which to resolve the point</param>
        /// <param name="point">The point to evaluate</param>
        /// <param name="camera">The camera viewing the point</param>
        public static Vector2 WorldToCanvas(this Canvas canvas, Vector3 point,
            Camera camera = null)
        {
            if (canvas == null)
            {
                throw new ArgumentNullException(nameof(canvas));
            }

            if (camera == null)
            {
                camera = canvas.worldCamera;

                if (camera == null)
                {
                    camera = Camera.main;
                }
            }

            var viewportPosition = camera.WorldToViewportPoint(point);
            var rect = canvas.GetComponent<RectTransform>();

            return new Vector2(viewportPosition.x * rect.sizeDelta.x -
                               rect.sizeDelta.x * 0.5f,
                               viewportPosition.y * rect.sizeDelta.y -
                               rect.sizeDelta.y * 0.5f);
        }

        /// <summary>
        /// Sets a Graphic's canvas point to the world space point
        /// </summary>
        /// <param name="graphic">The graphic which to set point for</param>
        /// <param name="point">The point to evaluate</param>
        /// <param name="camera">The camera viewing the point</param>
        public static void SetToWorldPosition(this Graphic graphic, Vector3 point,
            Camera camera = null)
        {
            if (graphic == null)
            {
                throw new ArgumentNullException(nameof(graphic));
            }

            graphic.rectTransform.SetToWorldPosition(point, graphic.canvas, camera);
        }

        /// <summary>
        /// Sets a RectTransform to a world space point
        /// </summary>
        /// <param name="transform">The transform which to set point for</param>
        /// <param name="point">The point to evaluate</param>
        /// <param name="canvas">The canvas the transform belongs to</param>
        /// <param name="camera">The camera viewing the point</param>
        public static void SetToWorldPosition(this RectTransform transform, 
            Vector3 point, Canvas canvas, Camera camera = null)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            if (canvas == null)
            {
                throw new ArgumentNullException(nameof(canvas));
            }

            transform.anchoredPosition = canvas.WorldToCanvas(point, camera);
        }
    }
}