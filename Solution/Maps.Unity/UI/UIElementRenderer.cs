using UnityEngine;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Responsible for rendering ui elements
    /// </summary>
    public class UIElementRenderer : MonoBehaviour
    {
        private UIElementRendererImpl _impl;

        /// <summary>
        /// Initializes the UIElementRenderer
        /// </summary>
        /// <param name="pool">The prefab pool to obtain ui elements from</param>
        /// <param name="anchor">The anchor for the renderer</param>
        public void Initialize(IPrefabPool pool, Transformd anchor)
        {
            if (_impl == null)
            {
                _impl = new UIElementRendererImpl(gameObject.activeInHierarchy, pool,
                    anchor);
            }
        }

        /// <inheritdoc />
        public void Add(UIElement uiElement)
        {
            if (_impl == null)
            {
                return;
            }

            _impl.Add(uiElement);
        }

        private void OnEnable()
        {
            if (_impl == null)
            {
                return;
            }

            _impl.Enabled = true;
        }

        private void OnDisable()
        {
            if (_impl == null)
            {
                return;
            }

            _impl.Enabled = false;
        }
    }
}