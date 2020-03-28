using System;
using Maps.Appearance;
using UnityEngine;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Represents a single icon element
    /// </summary>
    public sealed class Icon : UIElement
    {
        private PoolableIcon _poolableIcon;
        private readonly IconAppearance _appearance;
        private readonly UnityEngine.Sprite _sprite;

        /// <summary>
        /// Initializes a new instance of Icon
        /// </summary>
        /// <param name="canvas">The canvas to display the icon on</param>
        /// <param name="position">The world position of the icon</param>
        /// <param name="appearance">The appearance of the icon</param>
        /// <param name="texture">The actual texture for the icon</param>
        public Icon(Canvas canvas, Vector3d position, IconAppearance appearance,
            Texture2D texture) : base(canvas, position)
        {
            if (appearance == null)
            {
                throw new ArgumentNullException(nameof(appearance));
            }

            if (texture == null)
            {
                throw new ArgumentNullException(nameof(texture));
            }

            // todo : probably dont create sprites here, do it back at the texture model
            var size = new Vector2(texture.width, texture.height);
            _sprite = UnityEngine.Sprite.Create(texture, new Rect(Vector2.zero, size), 
                size * 0.5f);
            _appearance = appearance;
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

            _poolableIcon = prefabPool.IconPool.Borrow();
            _poolableIcon.Sprite = _sprite;
            _poolableIcon.Appearance = _appearance;
            _poolableIcon.Show(Canvas, Position, anchor, _appearance);
        }

        /// <inheritdoc />
        public override void Hide(IPrefabPool prefabPool)
        {
            if (prefabPool == null)
            {
                throw new ArgumentNullException(nameof(prefabPool));
            }

            if (_poolableIcon != null)
            {
                _poolableIcon.Hide();
                prefabPool.IconPool.Return(_poolableIcon);
                _poolableIcon = null;
            }
        }
    }
}