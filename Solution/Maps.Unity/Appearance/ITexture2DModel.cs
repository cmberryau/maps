using System.Drawing;
using UnityEngine;

namespace Maps.Unity.Appearance
{
    /// <summary>
    /// Interface for objects respsonsible for storing Texture2D instances
    /// </summary>
    public interface ITexture2DModel
    {
        /// <summary>
        /// Returns the texture for the given image
        /// </summary>
        /// <param name="image">The image to evaluate a texture for</param>
        Texture2D TextureFor(Image image);
    }
}