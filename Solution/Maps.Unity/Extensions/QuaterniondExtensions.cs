using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Provides Unity3D specific extensions for the Maps.Quaterniond class
    /// </summary>
    public static class QuaterniondExtensions
    {
        /// <summary>
        /// Performs a lossy down conversion to UnityEngine.Quaternion
        /// </summary>
        /// <param name="q">The quaternion to convert</param>
        public static Quaternion Quaternion(this Quaterniond q)
        {
            return new Quaternion((float)q.x, (float)q.y, (float)q.z, (float)q.w);
        }
    }
}