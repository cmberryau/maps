using System;
using System.Collections.Generic;
using System.Drawing;
using Maps.IO;
using Maps.Unity.Extensions;
using UnityEngine;

namespace Maps.Unity.Appearance
{
    /// <summary>
    /// Responsible for providing images for rendering maps in Unity3D
    /// </summary>
    internal sealed class Texture2DModelImpl : ITexture2DModel
    {
        private readonly IDictionary<Image, Texture2D> _map;

        /// <summary>
        /// Initializes a new instance of ImageModel
        /// </summary>
        /// <param name="sideData">The feature side data to use</param>
        public Texture2DModelImpl(ISideData sideData)
        {
            if (sideData == null)
            {
                throw new ArgumentNullException(nameof(sideData));
            }

            _map = new Dictionary<Image, Texture2D>();

            if (sideData.TryGetTable<Bitmap>(out var table))
            {
                var count = table.Count;
                for (var i = 1; i < count; ++i)
                {
                    if (table.TryGet(i, out var image))
                    {
                        _map[image] = image.Texture2D();
                    }
                }
            }

            Debug.Log($"Total of {_map.Count} textures generated");
        }

        /// <inheritdoc />
        public Texture2D TextureFor(Image image)
        {
            if (!_map.ContainsKey(image))
            {
                Debug.Log($"Additional texture generated, {_map.Count} in total");
                _map[image] = image.Texture2D();
            }

            return _map[image];
        }
    }
}