using System;
using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Provides Maps specific extensions to the UnityEngine.Transform class
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        /// Performs a conversion from UnityEngine.Transform to Maps.Transformd
        /// </summary>
        /// <param name="transform">The transform to convert</param>
        public static Transformd Transformd(this Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new Transformd(transform.position.Vector3d(),
                transform.rotation.Quaterniond(), transform.localScale.Vector3d());
        }
    }
}