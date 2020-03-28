using System.Collections.Generic;
using Maps.Appearance;

namespace Maps.Unity.UI
{
    /// <summary>
    /// An interface for poolable sprite control
    /// </summary>
    public interface IPoolableSprite : ISpriteAppearance
    {
        /// <summary>
        /// The individual images
        /// </summary>
        IList<UnityEngine.Sprite> Images
        {
            get;
            set;
        }

        /// <summary>
        /// The index of the current image
        /// </summary>
        int ImageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// The appearance of the sprite
        /// </summary>
        ISpriteAppearance Appearance
        {
            get;
            set;
        }
    }
}