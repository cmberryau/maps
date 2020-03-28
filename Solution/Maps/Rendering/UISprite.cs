using System;
using System.Collections.Generic;
using System.Drawing;
using Maps.Appearance;
using Maps.Extensions;
using Maps.Geometry;

namespace Maps.Rendering
{
    /// <summary>
    /// Represents a UI renderable sprite
    /// </summary>
    public class UISprite : UIRenderable
    {
        /// <summary>
        /// Delegate for handling sprite index changes
        /// </summary>
        /// <param name="newIndex">The new index</param>
        public delegate void OnImageIndexChangeHandler(int newIndex);

        /// <summary>
        /// Called when the UISprite instance's ImageIndex has changed
        /// </summary>
        public event OnImageIndexChangeHandler ImageIndexChanged;

        /// <summary>
        /// Index accessor for the sprite images
        /// </summary>
        public Bitmap this[int index] => _images[index];

        /// <summary>
        /// The index of the current image
        /// </summary>
        public int ImageIndex
        {
            get => _imageIndex;
            set
            {
                // dont bother if no change
                if (value == _imageIndex)
                {
                    return;
                }

                // clamp
                if (_imageIndex < 0)
                {
                    value = 0;
                }
                else if (_imageIndex >= ImageCount)
                {
                    value = ImageCount - 1;
                }

                _imageIndex = value;

                // call the event
                if (ImageIndexChanged != null)
                {
                    ImageIndexChanged(_imageIndex);
                }
            }
        }

        /// <summary>
        /// The total number of images
        /// </summary>
        public int ImageCount => _images.Count;

        private readonly IReadOnlyList<Bitmap> _images;
        private int _imageIndex;

        /// <summary>
        /// Initializes a new instance of UISprite
        /// </summary>
        /// <param name="bounds">The bounds of the object in world space</param>
        /// <param name="position">The position of the sprite</param>
        /// <param name="appearance">The appearance of the sprite</param>
        /// <param name="text">The text of the sprite</param>
        /// <param name="images">The images of the sprite</param>
        public UISprite(Bounds3d bounds, Vector3d position, UIRenderableAppearance appearance, 
            string text, IReadOnlyList<Bitmap> images) : base(bounds, position, appearance, text)
        {
            if (images == null)
            {
                throw new ArgumentNullException(nameof(images));
            }

            images.AssertNoNullEntries();
            _images = images;
        }
    }
}