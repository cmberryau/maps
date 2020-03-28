using System.IO;
using Maps.Unity.Extensions;
using UnityEngine;

namespace Maps.Unity.Utility
{
    /// <summary>
    /// Utility class for extracting sprites
    /// </summary>
    public class SpriteExtractor : MonoBehaviour
    {
        /// <summary>
        /// The sprites to extract
        /// </summary>
        public Sprite[] Sprites;

        private void Start()
        {
            if (Sprites == null)
            {
                return;
            }

            foreach (var sprite in Sprites)
            {
                var cropped = sprite.texture.Crop(sprite.textureRect);
                var bytes = cropped.EncodeToPNG();

                File.WriteAllBytes(Application.streamingAssetsPath +
                    Path.DirectorySeparatorChar + sprite.name + ".png", bytes);
            }
        }
    }
}