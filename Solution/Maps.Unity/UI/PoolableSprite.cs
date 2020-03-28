using System.Collections.Generic;
using Maps.Appearance;
using UnityEngine;
using UnityEngine.UI;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Represents a poolable sprite
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class PoolableSprite : PoolableUIElement, IPoolableSprite
    {
        /// <inheritdoc />
        public Colorf Color => _impl.Color;

        /// <inheritdoc />
        public IList<UnityEngine.Sprite> Images
        {
            get => _impl.Images;
            set => _impl.Images = value;
        }

        /// <inheritdoc />
        public int ImageIndex
        {
            get => _impl.ImageIndex;
            set => _impl.ImageIndex = value;
        }

        /// <inheritdoc />
        public ISpriteAppearance Appearance
        {
            get => _impl.Appearance;
            set => _impl.Appearance = value;
        }

        private PoolableSpriteImpl _impl;

        /// <inheritdoc />
        public override void OnAddedToPool()
        {
            base.OnAddedToPool();
            _impl = new PoolableSpriteImpl(gameObject.GetComponent<Image>());
        }
    }
}