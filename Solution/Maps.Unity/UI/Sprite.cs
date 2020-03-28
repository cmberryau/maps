using System;
using System.Collections.Generic;
using Maps.Appearance;
using Maps.Extensions;
using Maps.Rendering;
using UnityEngine;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Represents a single sprite element
    /// </summary>
    public class Sprite : UIElement
    {
        private PoolableSprite _poolableSprite;
        private readonly SpriteAppearance _appearance;
        private readonly IList<UnityEngine.Sprite> _sprites;
        private readonly UISprite _uiSprite;
        private int _imageIndex;

        /// <summary>
        /// Initializes a new instance of Sprite
        /// </summary>
        /// <param name="canvas">The canvas the sprite should be on</param>
        /// <param name="position">The world position of the sprite</param>
        /// <param name="appearance">The appearance of the sprite</param>
        /// <param name="textures">The textures of the sprite</param>
        /// <param name="uiSprite">The originating ui sprite</param>
        public Sprite(Canvas canvas, Vector3d position, SpriteAppearance appearance, 
            IList<Texture2D> textures, UISprite uiSprite) : base(canvas, position)
        {
            if (appearance == null)
            {
                throw new ArgumentNullException(nameof(appearance));
            }

            if (textures == null)
            {
                throw new ArgumentNullException(nameof(textures));
            }

            if (uiSprite == null)
            {
                throw new ArgumentNullException(nameof(uiSprite));
            }

            // todo : probably dont create sprites here, do it back at the texture model
            textures.AssertNoNullEntries();
            _sprites = new List<UnityEngine.Sprite>();

            for (var i = 0; i < textures.Count; i++)
            {
                var size = new Vector2(textures[i].width, textures[i].height);
                _sprites.Add(UnityEngine.Sprite.Create(textures[i], 
                    new Rect(Vector2.zero, size), size * 0.5f));
            }

            _appearance = appearance;
            _uiSprite = uiSprite;
        }

        /// <inheritdoc />
        public override void Show(IPrefabPool prefabPool, Transformd anchor)
        {
            if (prefabPool == null)
            {
                throw new ArgumentNullException(nameof(prefabPool));
            }

            if (anchor == null)
            {
                throw new ArgumentNullException(nameof(anchor));
            }

            _poolableSprite = prefabPool.SpritePool.Borrow();
            _poolableSprite.Images = _sprites;
            _poolableSprite.Appearance = _appearance;
            _poolableSprite.Show(Canvas, Position, anchor, _appearance);
            _poolableSprite.ImageIndex = _imageIndex;
            _uiSprite.ImageIndexChanged += OnUISpriteImageIndexChanged;
        }

        /// <inheritdoc />
        public override void Hide(IPrefabPool prefabPool)
        {
            if (prefabPool == null)
            {
                throw new ArgumentNullException(nameof(prefabPool));
            }

            _uiSprite.ImageIndexChanged -= OnUISpriteImageIndexChanged;

            if (_poolableSprite != null)
            {
                _poolableSprite.Hide();
                prefabPool.SpritePool.Return(_poolableSprite);
                _poolableSprite = null;
            }
        }

        private void OnUISpriteImageIndexChanged(int newIndex)
        {
            _imageIndex = newIndex;

            if (_poolableSprite != null)
            {
                _poolableSprite.ImageIndex = _imageIndex;
            }
        }
    }
}