using Maps.Appearance;
using UnityEngine;
using UnityEngine.UI;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Represents a poolable label
    /// </summary>
    [RequireComponent(typeof(Text), typeof(Outline))]
    public sealed class PoolableLabel : PoolableUIElement, IPoolableLabel
    {        
        /// <summary>
        /// The text of the label
        /// </summary>
        public string Text
        {
            get => _impl.Text;
            set => _impl.Text = value;
        }

        /// <inheritdoc />
        public ILabelAppearance Appearance
        {
            get => _impl.Appearance;
            set => _impl.Appearance = value;
        }

        /// <inheritdoc />
        public bool FontBold => _impl.FontBold;

        /// <inheritdoc />
        public Colorf FontColor => _impl.FontColor;

        /// <inheritdoc />
        public float FontSize => _impl.FontSize;

        /// <inheritdoc />
        public bool FontOutline => _impl.FontOutline;

        /// <inheritdoc />
        public Colorf FontOutlineColor => _impl.FontOutlineColor;

        private PoolableLabelImpl _impl;

        /// <inheritdoc />
        public override void OnAddedToPool()
        {
            base.OnAddedToPool();
            _impl = new PoolableLabelImpl(gameObject.GetComponent<Text>(),
                gameObject.GetComponent<Outline>());
        }
    }
}