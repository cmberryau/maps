using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Unity3d specific extensions for the Maps.Vector2d class
    /// </summary>
    public static class Vector2dExtensions
    {
        /// <summary>
        /// Performs a lossy down-version from Vector2d to Vector2
        /// </summary>
        /// <param name="a">The vector to convert</param>
        public static Vector2 Vector2(this Vector2d a)
        {
            return new Vector2((float)a.x, (float)a.y);
        }

        /// <summary>
        /// Performs a lossy down-version from Vector2d to Vector2
        /// </summary>
        /// <param name="vectors">The vectors to convert</param>
        public static List<Vector2> Vector2(this IList<Vector2d> vectors)
        {
            var vecfs = new List<Vector2>();

            for (var i = 0; i < vectors.Count; ++i)
            {
                vecfs.Add(vectors[i].Vector2());
            }

            return vecfs;
        }

        /// <summary>
        /// Draws the given array of vertices as lines in world space
        /// using UnityEngine.Debug.Draw*
        /// </summary>
        /// <param name="vertices">The array of points to draw</param>
        /// <param name="color">The color to draw the lines</param>
        /// <param name="closed">Should a line be drawn from the final vertex to the first?</param>
        /// <param name="duration">The duration to draw for(seconds)</param>
        public static void DrawLines(this IEnumerable<Vector2d> vertices, Color color,
            bool closed, float duration)
        {
            if (vertices == null)
            {
                throw new ArgumentNullException(nameof(vertices));
            }

            var vectorArray = vertices.ToArray();

            for (var i = 0; i < vectorArray.Length - 1; i++)
            {
                Debug.DrawLine(vectorArray[i].Vector2(), vectorArray[i+1].Vector2(),
                    color, duration);
            }

            if (closed)
            {
                Debug.DrawLine(vectorArray[vectorArray.Length - 1].Vector2(),
                    vectorArray[0].Vector2(), color, duration);
            }
        }

        /// <summary>
        /// Draws the given array of vertices as lines in world space
        /// using UnityEngine.Debug.Draw*
        /// </summary>
        /// <param name="vertices">The array of points to draw</param>
        /// <param name="color">The color to draw the lines</param>
        /// <param name="closed">Should a line be drawn from the final vertex to the first?</param>
        public static void DrawLines(this IEnumerable<Vector2d> vertices, Color color, bool closed)
        {
            if (vertices == null)
            {
                throw new ArgumentNullException(nameof(vertices));
            }

            var vectorArray = vertices.ToArray();

            for (var i = 0; i < vectorArray.Length - 1; i++)
            {
                Debug.DrawLine(vectorArray[i].Vector2(), vectorArray[i + 1].Vector2(),
                    color);
            }

            if (closed)
            {
                Debug.DrawLine(vectorArray[vectorArray.Length - 1].Vector2(),
                    vectorArray[0].Vector2(), color);
            }
        }
    }
}