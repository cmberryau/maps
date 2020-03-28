using Maps.Appearance;
using UnityEngine;
using UnityEngine.UI;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Represents a poolable icon
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class PoolableIcon : PoolableUIElement, IPoolableIcon
    {
        /// <summary>
        /// The sprite of the icon
        /// </summary>
        public UnityEngine.Sprite Sprite
        {
            get => _impl.Sprite;
            set => _impl.Sprite = value;
        }

        /// <inheritdoc />
        public Colorf Color => _impl.Color;

        /// <inheritdoc />
        public IIconAppearance Appearance
        {
            get => _impl.Appearance;
            set => _impl.Appearance = value;
        }

        private PoolableIconImpl _impl;

        /// <inheritdoc />
        public override void OnAddedToPool()
        {
            base.OnAddedToPool();
            _impl = new PoolableIconImpl(gameObject.GetComponent<Image>());
        }
    }
}