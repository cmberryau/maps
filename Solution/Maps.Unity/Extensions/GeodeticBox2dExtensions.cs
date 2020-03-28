using System;
using Maps.Geographical;
using Maps.Geographical.Projection;
using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Unity3d specific extensions for GeodeticBox2d
    /// </summary>
    public static class GeodeticBox2dExtensions
    {
        /// <summary>
        /// Draws the Geodetic2dBox instance using Unity3d's Debug.Draw
        /// </summary>
        /// <param name="box">The Geodetic2dBox instance to draw</param>
        /// <param name="projection">The projection to use</param>
        /// <param name="color">The color to draw</param>
        /// <param name="duration">The duration to draw for</param>
        public static void Draw(this GeodeticBox2d box, IProjection projection,
            Color color, float duration)
        {
            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            if (projection == null)
            {
                throw new ArgumentException(nameof(projection));
            }

            projection.Forward(box).DrawLines(color, duration, true);
        }
    }
}