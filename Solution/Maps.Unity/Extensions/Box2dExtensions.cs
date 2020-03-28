using System;
using Maps.Geometry;
using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Unity3d specific extensions for the Maps.Geometry.Box2d class
    /// </summary>
    public static class Box2dExtensions
    {
        /// <summary>
        /// Draws the box using Unity3d's Debug.Draw
        /// </summary>
        /// <param name="box">The box to draw</param>
        /// <param name="color">The color to draw the box</param>
        public static void Draw(this Box2d box, Color color)
        {
            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            var v0 = box.A;
            var v1 = new Vector2d(box.A.x, box.B.y);
            var v2 = box.B;
            var v3 = new Vector2d(box.B.x, box.A.y);

            Debug.DrawLine(v0.Vector2(), v1.Vector2(), color);
            Debug.DrawLine(v1.Vector2(), v2.Vector2(), color);
            Debug.DrawLine(v2.Vector2(), v3.Vector2(), color);
            Debug.DrawLine(v3.Vector2(), v0.Vector2(), color);
        }

        /// <summary>
        /// Draws the box using Unity3d's Debug.Draw
        /// </summary>
        /// <param name="box">The box to draw</param>
        /// <param name="color">The color to draw the box</param>
        /// <param name="duration">The duration to draw for (seconds)</param>
        public static void Draw(this Box2d box, Color color, 
            float duration)
        {
            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            var v0 = box.A;
            var v1 = new Vector2d(box.A.x, box.B.y);
            var v2 = box.B;
            var v3 = new Vector2d(box.B.x, box.A.y);

            Debug.DrawLine(v0.Vector2(), v1.Vector2(), color, duration);
            Debug.DrawLine(v1.Vector2(), v2.Vector2(), color, duration);
            Debug.DrawLine(v2.Vector2(), v3.Vector2(), color, duration);
            Debug.DrawLine(v3.Vector2(), v0.Vector2(), color, duration);
        }
    }
}