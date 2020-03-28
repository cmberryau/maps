using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Maps specific extensions for the UnityEngine.Quaternion class
    /// </summary>
    public static class QuaternionExtensions
    {
        /// <summary>
        /// Performs a conversion from UnityEngine.Quaternion to Maps.Quaterniond
        /// </summary>
        /// <param name="q">The quaternion to convert</param>
        public static Quaterniond Quaterniond(this Quaternion q)
        {
            return new Quaterniond(q.x, q.y, q.z, q.w);
        }
    }
}