using Maps.Appearance;
using UnityEngine;
using UnityEngine.UI;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Responsible for representing a poolable ui element
    /// </summary>
    [RequireComponent(typeof(RectTransform), typeof(Graphic))]
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
    public abstract class PoolableUIElement : PoolableGameObject, IRenderableAppearance, 
        IUIElement
    {
        /// <inheritdoc />
        public int ZIndex => _impl.ZIndex;

        /// <inheritdoc />
        public bool IgnoreOthers => _impl.IgnoreOthers;

        /// <inheritdoc />
        public float Padding => _impl.Padding;

        /// <inheritdoc />
        public bool RotateWithMap => _impl.RotateWithMap;

        /// <inheritdoc />
        public bool Shown => _impl.Shown;

        /// <inheritdoc />
        public bool Active => _impl.Active;

        private PoolableUIElementImpl _impl;

        /// <summary>
        /// Shows the ui element
        /// </summary>
        /// <param name="canvas">The canvas on which to show the ui element</param>
        /// <param name="position">The world position to attach to</param>
        /// <param name="anchor">The anchor for the ui element</param>
        /// <param name="appearance">The appearance of the ui element</param>
        public void Show(Canvas canvas, Vector3d position, Transformd anchor,
            IUIRenderableAppearance appearance)
        {
            _impl.Show(canvas, position, anchor, appearance);
        }

        /// <summary>
        /// Hides the ui element
        /// </summary>
        public void Hide()
        {
            _impl.Hide();
        }

        /// <inheritdoc />
        public override void OnAddedToPool()
        {
            base.OnAddedToPool();

            _graphic = gameObject.GetComponent<Graphic>();

            _impl = new PoolableUIElementImpl(gameObject.GetComponent<Graphic>(), 
                gameObject.GetComponent<BoxCollider2D>(), gameObject.GetComponent<Rigidbody2D>());
        }

        private Graphic _graphic;

        private void LateUpdate()
        {
            _impl.LateUpdate();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            _impl.OnTriggerEnter2D(collider);
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            _impl.OnTriggerExit2D(collider);
        }
    }
}