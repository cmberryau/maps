using Maps.Geometry;
using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Unity3d specific extensions for the Maps.Geometry.Bounds2d class
    /// </summary>
    public static class Bounds2dExtensions
    {
        /// <summary>
        /// Draws the bounds using Unity3d's Debug.Draw*
        /// </summary>
        /// <param name="bounds">The bounds to draw</param>
        /// <param name="color">The color to draw the bounds</param>
        public static void Draw(this Bounds2d bounds, Color color)
        {
            var v0 = bounds.Max;
            var v1 = new Vector2d(bounds.Max.x, bounds.Min.y);
            var v2 = bounds.Min;
            var v3 = new Vector2d(bounds.Min.x, bounds.Max.y);

            Debug.DrawLine(v0.Vector2(), v1.Vector2(), color);
            Debug.DrawLine(v1.Vector2(), v2.Vector2(), color);
            Debug.DrawLine(v2.Vector2(), v3.Vector2(), color);
            Debug.DrawLine(v3.Vector2(), v0.Vector2(), color);
        }

        /// <summary>
        /// Draws the bounds using Unity3d's Debug.Draw*
        /// </summary>
        /// <param name="bounds">The bounds to draw</param>
        /// <param name="color">The color to draw the bounds</param>
        /// <param name="duration">The duration to draw for (seconds)</param>
        public static void Draw(this Bounds2d bounds, Color color, 
            float duration)
        {
            var v0 = bounds.Max;
            var v1 = new Vector2d(bounds.Max.x, bounds.Min.y);
            var v2 = bounds.Min;
            var v3 = new Vector2d(bounds.Min.x, bounds.Max.y);

            Debug.DrawLine(v0.Vector2(), v1.Vector2(), color, duration);
            Debug.DrawLine(v1.Vector2(), v2.Vector2(), color, duration);
            Debug.DrawLine(v2.Vector2(), v3.Vector2(), color, duration);
            Debug.DrawLine(v3.Vector2(), v0.Vector2(), color, duration);
        }
    }
}