using System;
using System.Collections.Generic;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Responsible for the implementation of rendering ui elements
    /// </summary>
    internal sealed class UIElementRendererImpl
    {
        /// <summary>
        /// Is the renderer enabled?
        /// </summary>
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                if (value && !_enabled)
                {
                    _enabled = true;
                    OnBecameEnabled();
                }
                else if(!value && _enabled)
                {
                    _enabled = false;
                    OnBecameDisabled();
                }
            }
        }

        private readonly IList<UIElement> _uiElements;
        private readonly IPrefabPool _prefabPool;
        private readonly Transformd _anchor;
        private bool _enabled;

        /// <summary>
        /// Initializes a new instance of UIElementRendererImpl
        /// </summary>
        /// <param name="enabled">Should the renderer become active?</param>
        /// <param name="prefabPool">The prefab pool for ui elements</param>
        /// <param name="anchor">The anchor for the ui element</param>
        public UIElementRendererImpl(bool enabled, IPrefabPool prefabPool, 
            Transformd anchor)
        {
            if (prefabPool == null)
            {
                throw new ArgumentNullException(nameof(prefabPool));
            }

            if (anchor == null)
            {
                throw new ArgumentNullException(nameof(anchor));
            }

            _uiElements = new List<UIElement>();
            _prefabPool = prefabPool;
            _enabled = enabled;
            _anchor = anchor;
        }

        /// <inheritdoc />
        public void Add(UIElement uiElement)
        {
            if (uiElement == null)
            {
                throw new ArgumentNullException(nameof(uiElement));
            }

            _uiElements.Add(uiElement);

            if (_enabled)
            {
                uiElement.Show(_prefabPool, _anchor);
            }
        }

        private void OnBecameEnabled()
        {
            ShowAllElements();
        }

        private void OnBecameDisabled()
        {
            HideAllElements();
        }

        private void ShowAllElements()
        {
            var elementsCount = _uiElements.Count;
            for (var i = 0; i < elementsCount; ++i)
            {
                _uiElements[i].Show(_prefabPool, _anchor);
            }
        }

        private void HideAllElements()
        {
            var elementsCount = _uiElements.Count;
            for (var i = 0; i < elementsCount; ++i)
            {
                _uiElements[i].Hide(_prefabPool);
            }
        }
    }
}