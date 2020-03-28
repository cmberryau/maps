using System;
using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Provides extensions for the Texture2D class
    /// </summary>
    public static class Texture2DExtensions
    {
        /// <summary>
        /// Crops out a portion of a given source texture
        /// </summary>
        /// <param name="source">The source texture to crop from</param>
        /// <param name="rect">The rect to crop</param>
        /// <returns>A cropped portion from the given texture in RGBA32 format</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/>
        /// is null</exception>
        public static Texture2D Crop(this Texture2D source, Rect rect)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Crop((int) rect.xMin, (int) rect.yMin, (int) rect.width,
                (int) rect.height);
        }

        /// <summary>
        /// Crops out a portion of a given source texture
        /// </summary>
        /// <param name="source">The source texture to crop from</param>
        /// <param name="left">The left offset</param>
        /// <param name="top">The top offset</param>
        /// <param name="width">The width of the cropped area</param>
        /// <param name="height">The height of the cropped area</param>
        /// <returns>A cropped portion from the given texture in RGBA32 format</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/>
        /// is null</exception>
        public static Texture2D Crop(this Texture2D source, int left, int top, int width,
            int height)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (left < 0)
            {
                width += left;
                left = 0;
            }

            if (top < 0)
            {
                height += top;
                top = 0;
            }

            if (left + width > source.width)
            {
                width = source.width - left;
            }

            if (top + height > source.height)
            {
                height = source.height - top;
            }

            if (width <= 0 || height <= 0)
            {
                return null;
            }

            var aSourceColor = source.GetPixels(0);
            var texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            var xLength = width * height;
            var aColor = new Color[xLength];

            for (int y = 0, i = -1; y < height; ++y)
            {
                var sourceIndex = (y + top) * source.width + left - 1;
                for (var x = 0; x < width; ++x)
                {
                    aColor[++i] = aSourceColor[++sourceIndex];
                }
            }

            texture.SetPixels(aColor);
            texture.Apply();

            return texture;
        }
    }
}