using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Unity3d specific extensions for the Maps.Vector3d struct
    /// </summary>
    public static class Vector3dExtensions
    {
        /// <summary>
        /// Performs a lossy down-version from Vector3d to Vector3
        /// </summary>
        /// <param name="a">The Vector3d to convert</param>
        public static Vector3 Vector3(this Vector3d a)
        {
            return new Vector3((float)a.x, (float)a.y, (float)a.z);
        }

        /// <summary>
        /// Performs a lossy down-version from Vector3d to Vector3
        /// </summary>
        /// <param name="vectors">The vectors to convert</param>
        public static List<Vector3> Vector3(this IList<Vector3d> vectors)
        {
            var vecfs = new List<Vector3>();

            for (var i = 0; i < vectors.Count; ++i)
            {
                vecfs.Add(vectors[i].Vector3());
            }

            return vecfs;
        }

        /// <summary>
        /// Performs a conversion from a Vector3d to a pair of
        /// UnityEngine.Vector3
        /// </summary>
        /// <param name="a">The Vector3d to convert</param>
        /// <param name="high">The high result</param>
        /// <param name="low">The low result</param>
        public static void HighLowPair(this Vector3d a, out Vector3 high, out Vector3 low)
        {
            high = a.High.Vector3();
            low = a.Low.Vector3();
        }

        /// <summary>
        /// Draws the given array of vertices as lines in world space
        /// using UnityEngine.Debug.Draw*
        /// </summary>
        /// <param name="vertices">The array of points to draw</param>
        /// <param name="color">The color to draw the lines</param>
        /// <param name="duration">The duration to draw for (seconds)</param>
        /// <param name="closed">Should a line be drawn from the final vertex to the first?</param>
        public static void DrawLines(this IEnumerable<Vector3d> vertices, Color color,
            float duration, bool closed)
        {
            if (vertices == null)
            {
                throw new ArgumentNullException(nameof(vertices));
            }

            var vectorArray = vertices.ToArray();

            for (var i = 0; i < vectorArray.Length - 1; i++)
            {
                Debug.DrawLine(vectorArray[i].Vector3(), vectorArray[i + 1].Vector3(),
                    color, duration);
            }

            if (closed)
            {
                Debug.DrawLine(vectorArray[vectorArray.Length - 1].Vector3(),
                    vectorArray[0].Vector3(), color, duration);
            }
        }
    }
}