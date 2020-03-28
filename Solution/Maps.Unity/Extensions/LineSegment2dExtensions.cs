using System;
using Maps.Geometry;
using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Unity3d specific extensions for the Maps.Geometry.LineSegment2d class
    /// </summary>
    public static class LineSegment2dExtensions
    {
        /// <summary>
        /// Draws the line segment using Unity3d's Debug.Draw*
        /// </summary>
        /// <param name="segment">The segment to draw</param>
        /// <param name="color">The color to draw the bounds</param>
        public static void Draw(this LineSegment2d segment, Color color)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            Debug.DrawLine(segment.P0.Vector2(), segment.P1.Vector2(), color);
        }

        /// <summary>
        /// Draws the line segment using Unity3d's Debug.Draw*
        /// </summary>
        /// <param name="segment">The segment to draw</param>
        /// <param name="color">The color to draw the bounds</param>
        /// <param name="duration">The duration to draw for (seconds)</param>
        public static void Draw(this LineSegment2d segment, Color color,
            float duration)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            Debug.DrawLine(segment.P0.Vector2(), segment.P1.Vector2(), color, duration);
        }
    }
}