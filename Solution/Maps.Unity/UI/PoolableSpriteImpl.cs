using System;
using System.Collections.Generic;
using Maps.Appearance;
using UnityEngine.UI;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Responsible for the implementation of a poolable sprite
    /// </summary>
    public class PoolableSpriteImpl : IPoolableSprite
    {
        /// <inheritdoc />
        public int ZIndex
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public float Padding
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public bool IgnoreOthers
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public bool RotateWithMap
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public Colorf Color
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public IList<UnityEngine.Sprite> Images
        {
            get => _sprites;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Images));
                }

                if (value.Count <= 0)
                {
                    throw new ArgumentException("Must have at least one sprite", 
                        nameof(Images));
                }

                _sprites = value;
                ImageIndex = 0;
            }
        }

        /// <inheritdoc />
        public int ImageIndex
        {
            get => _currentIndex;
            set
            {
                // clamp the value
                if (value > _sprites.Count - 1)
                {
                    value = _sprites.Count - 1;
                }
                else if (value < 0)
                {
                    value = 0;
                }

                // set the image's sprite
                if (_sprites != null)
                {
                    _image.sprite = _sprites[value];
                    _image.SetNativeSize();
                }

                _currentIndex = value;
            }
        }

        /// <inheritdoc />
        public ISpriteAppearance Appearance
        {
            get => _appearance;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Appearance));
                }

                _appearance = value;
                ZIndex = _appearance.ZIndex;
                Padding = _appearance.Padding;
                IgnoreOthers = _appearance.IgnoreOthers;
                Color = _appearance.Color;
                RotateWithMap = _appearance.RotateWithMap;
            }
        }

        private readonly Image _image;
        private int _currentIndex;
        private IList<UnityEngine.Sprite> _sprites;
        private ISpriteAppearance _appearance;

        /// <summary>
        /// Initializes a new instance of PoolableSpriteImpl
        /// </summary>
        /// <param name="image">The image for the sprite</param>
        public PoolableSpriteImpl(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            _image = image;
        }
    }
}