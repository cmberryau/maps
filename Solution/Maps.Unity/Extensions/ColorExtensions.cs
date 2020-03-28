using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Extensions for the Unity3D Color struct
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Converts the Color to a Colorf instance
        /// </summary>
        /// <param name="color">The color to convert</param>
        public static Colorf Colorf(this Color color)
        {
            return new Colorf(color.r, color.g, color.b, color.a);
        }
    }
}