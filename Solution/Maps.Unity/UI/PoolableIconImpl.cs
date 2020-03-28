using System;
using Maps.Appearance;
using Maps.Unity.Extensions;
using UnityEngine.UI;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Responsible for the implementation of a poolable icon
    /// </summary>
    public class PoolableIconImpl : IPoolableIcon
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

        /// <summary>
        /// The sprite of the icon
        /// </summary>
        public UnityEngine.Sprite Sprite
        {
            get => _image.sprite;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Sprite));
                }

                _image.sprite = value;
                _image.SetNativeSize();
            }
        }

        /// <inheritdoc />
        public Colorf Color
        {
            get => _image.color.Colorf();
            private set => _image.color = value.Color();
        }

        /// <inheritdoc />
        public IIconAppearance Appearance
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
        private IIconAppearance _appearance;

        /// <summary>
        /// Initializes a new instance of PoolableIconImpl
        /// </summary>
        /// <param name="image">The image of the icon</param>
        public PoolableIconImpl(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            _image = image;
        }
    }
}