using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Unity3d specific extensions for the Maps.Vector3d32 struct
    /// </summary>
    public static class Vector3d32Extensions
    {
        /// <summary>
        /// Performs a lossless conversion from Vector3d32 to UnityEngine.Vector3
        /// </summary>
        public static Vector3 Vector3(this Vector3f a)
        {
            return new Vector3(a.x, a.y, a.z);
        }
    }
}