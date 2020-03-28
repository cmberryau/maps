using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Set of extensions for the UnityEngine.Matrix4x4 class specific for Unity3d
    /// </summary>
    public static class Matrix4x4Extensions
    {
        /// <summary>
        /// Creates a Maps.Matrix4 instance from the given Matrix4x4
        /// </summary>
        /// <param name="matrix">The matrix to copy from</param>
        public static Matrix4d Matrix4(this Matrix4x4 matrix)
        {
            return new Matrix4d(matrix.m00, matrix.m01, matrix.m02, matrix.m03,
                               matrix.m10, matrix.m11, matrix.m12, matrix.m13,
                               matrix.m20, matrix.m21, matrix.m22, matrix.m23,
                               matrix.m30, matrix.m31, matrix.m32, matrix.m33);
        }
    }
}