using Maps.Geometry;
using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Unity3d specific extensions for the Maps.Geometry.Bounds2d class
    /// </summary>
    public static class Bounds3dExtensions
    {
        /// <summary>
        /// Returns a Unity3D Bounds instance
        /// </summary>
        /// <param name="bounds">The Bounds3d instance to create from</param>
        public static Bounds Bounds(this Bounds3d bounds)
        {
            return new Bounds(bounds.Centre.Vector3(), bounds.Extents.Vector3() * 2f);
        }

        /// <summary>
        /// Draws the bounds using Unity3d's Debug.Draw*
        /// </summary>
        /// <param name="bounds">The bounds to draw</param>
        /// <param name="color">The color to draw the bounds</param>
        public static void Draw(this Bounds3d bounds, Color color)
        {
            var v0 = bounds.Min;
            var v1 = new Vector3d(bounds.Max.x, bounds.Min.y, bounds.Min.z);
            var v2 = new Vector3d(bounds.Max.x, bounds.Min.y, bounds.Max.z);
            var v3 = new Vector3d(bounds.Min.x, bounds.Min.y, bounds.Max.z);
            var v4 = new Vector3d(bounds.Min.x, bounds.Max.y, bounds.Min.z);
            var v5 = new Vector3d(bounds.Max.x, bounds.Max.y, bounds.Min.z);
            var v6 = bounds.Max;
            var v7 = new Vector3d(bounds.Min.x, bounds.Max.y, bounds.Max.z);

            // bottom ring
            Debug.DrawLine(v0.Vector3(), v1.Vector3(), color);
            Debug.DrawLine(v1.Vector3(), v2.Vector3(), color);
            Debug.DrawLine(v2.Vector3(), v3.Vector3(), color);
            Debug.DrawLine(v3.Vector3(), v0.Vector3(), color);

            // columns
            Debug.DrawLine(v0.Vector3(), v4.Vector3(), color);
            Debug.DrawLine(v1.Vector3(), v5.Vector3(), color);
            Debug.DrawLine(v2.Vector3(), v6.Vector3(), color);
            Debug.DrawLine(v3.Vector3(), v7.Vector3(), color);

            // top ring
            Debug.DrawLine(v4.Vector3(), v5.Vector3(), color);
            Debug.DrawLine(v5.Vector3(), v6.Vector3(), color);
            Debug.DrawLine(v6.Vector3(), v7.Vector3(), color);
            Debug.DrawLine(v7.Vector3(), v4.Vector3(), color);
        }

        /// <summary>
        /// Draws the bounds using Unity3d's Debug.Draw*
        /// </summary>
        /// <param name="bounds">The bounds to draw</param>
        /// <param name="color">The color to draw the bounds</param>
        /// <param name="duration">The duration to draw for (seconds)</param>
        public static void Draw(this Bounds3d bounds, Color color, float duration)
        {
            var v0 = bounds.Min;
            var v1 = new Vector3d(bounds.Max.x, bounds.Min.y, bounds.Min.z);
            var v2 = new Vector3d(bounds.Max.x, bounds.Min.y, bounds.Max.z);
            var v3 = new Vector3d(bounds.Min.x, bounds.Min.y, bounds.Max.z);
            var v4 = new Vector3d(bounds.Min.x, bounds.Max.y, bounds.Min.z);
            var v5 = new Vector3d(bounds.Max.x, bounds.Max.y, bounds.Min.z);
            var v6 = bounds.Max;
            var v7 = new Vector3d(bounds.Min.x, bounds.Max.y, bounds.Max.z);

            // bottom ring
            Debug.DrawLine(v0.Vector3(), v1.Vector3(), color, duration);
            Debug.DrawLine(v1.Vector3(), v2.Vector3(), color, duration);
            Debug.DrawLine(v2.Vector3(), v3.Vector3(), color, duration);
            Debug.DrawLine(v3.Vector3(), v0.Vector3(), color, duration);

            // columns
            Debug.DrawLine(v0.Vector3(), v4.Vector3(), color, duration);
            Debug.DrawLine(v1.Vector3(), v5.Vector3(), color, duration);
            Debug.DrawLine(v2.Vector3(), v6.Vector3(), color, duration);
            Debug.DrawLine(v3.Vector3(), v7.Vector3(), color, duration);

            // top ring
            Debug.DrawLine(v4.Vector3(), v5.Vector3(), color, duration);
            Debug.DrawLine(v5.Vector3(), v6.Vector3(), color, duration);
            Debug.DrawLine(v6.Vector3(), v7.Vector3(), color, duration);
            Debug.DrawLine(v7.Vector3(), v4.Vector3(), color, duration);
        }
    }
}