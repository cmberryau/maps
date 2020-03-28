using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Useful extensions for the System.Drawing.Image class in the context of Unity3D
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        /// Creates a Texture2D instance from an Image instance
        /// </summary>
        /// <param name="image">The Image instance to evaluate against</param>
        public static Texture2D Texture2D(this Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            using (var stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Png);
                var texture = new Texture2D(image.Width, image.Height);

                if (!texture.LoadImage(stream.ToArray()))
                {
                    Debug.LogError("Could not load image");
                    return null;
                }

                return texture;
            }
        }
    }
}