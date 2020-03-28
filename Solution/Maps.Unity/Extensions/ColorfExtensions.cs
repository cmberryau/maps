using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Unity3d specific extensions for the Maps.Color32 struct
    /// </summary>
    public static class ColorfExtensions
    {
        /// <summary>
        /// Performs a conversion from Maps.Color32 to UnityEngine.Color
        /// </summary>
        /// <param name="color">The Color32 instance to convert</param>
        public static Color Color(this Colorf color)
        {
            return new Color(color.r, color.g, color.b, color.a);
        }
    }
}