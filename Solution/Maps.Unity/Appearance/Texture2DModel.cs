using System;
using System.Drawing;
using Maps.IO;
using UnityEngine;

namespace Maps.Unity.Appearance
{
    /// <summary>
    /// Responsible for providing images for rendering maps in Unity3D
    /// </summary>
    public sealed class Texture2DModel : MonoBehaviour, ITexture2DModel
    {
        private ITexture2DModel _impl;

        /// <summary>
        /// Initializes the Texture2DModel
        /// </summary>
        /// <param name="sideData">The feature side data to use</param>
        public void Initialize(ISideData sideData)
        {
            if (sideData == null)
            {
                throw new ArgumentNullException(nameof(sideData));
            }

            _impl = new Texture2DModelImpl(sideData);
        }

        /// <inheritdoc />
        public Texture2D TextureFor(Image image)
        {
            return _impl.TextureFor(image);
        }
    }
}