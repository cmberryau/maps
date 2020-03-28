using System;
using Maps.Appearance;
using Maps.Rendering;
using UnityEngine;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Represents a single label element
    /// </summary>
    public sealed class Label : UIElement
    {
        private PoolableLabel _poolableLabel;
        private readonly string _text;
        private readonly LabelAppearance _appearance;

        /// <summary>
        /// Initializes a new instance of Label
        /// </summary>
        /// <param name="canvas">The canvas to display the label on</param>
        /// <param name="position">The world position of the label</param>
        /// <param name="renderable">The ui renderable element</param>
        /// <param name="appearance">The appearance of the label</param>
        public Label(Canvas canvas, Vector3d position, UIRenderable renderable, 
            LabelAppearance appearance) : base(canvas, position)
        {
            _text = renderable.Text;
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

            _poolableLabel = prefabPool.LabelPool.Borrow();
            _poolableLabel.Text = _text;
            _poolableLabel.Appearance = _appearance;
            _poolableLabel.Show(Canvas, Position, anchor, _appearance);
        }

        /// <inheritdoc />
        public override void Hide(IPrefabPool prefabPool)
        {
            if (prefabPool == null)
            {
                throw new ArgumentNullException(nameof(prefabPool));
            }

            if (_poolableLabel != null)
            {
                _poolableLabel.Hide();
                prefabPool.LabelPool.Return(_poolableLabel);
                _poolableLabel = null;
            }
        }
    }
}