using System.Collections.Generic;
using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Unity3d specific extensions for the Maps.Vector4D class
    /// </summary>
    public static class Vector4dExtensions
    {
        /// <summary>
        /// Performs a lossy down-version from Vector4d to Vector4
        /// </summary>
        /// <param name="a">The vector to convert</param>
        public static Vector4 Vector4(this Vector4d a)
        {
            return new Vector4((float)a.x, (float)a.y, (float)a.z, (float)a.w);
        }

        /// <summary>
        /// Performs a lossy down-version from Vector4d to Vector4
        /// </summary>
        /// <param name="vectors">The vectors to convert</param>
        public static List<Vector4> Vector4(this IList<Vector4d> vectors)
        {
            var vecfs = new List<Vector4>();

            for (var i = 0; i < vectors.Count; ++i)
            {
                vecfs.Add(vectors[i].Vector4());
            }

            return vecfs;
        }
    }
}