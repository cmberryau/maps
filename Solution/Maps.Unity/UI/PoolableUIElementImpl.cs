using System;
using Maps.Appearance;
using Maps.Unity.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Responsible for the implementation of representing a poolable ui element
    /// </summary>
    internal sealed class PoolableUIElementImpl : IUIElement, IUIRenderableAppearance
    {
        /// <inheritdoc />
        public int ZIndex
        {
            get
            {
                if (_appearance != null)
                {
                    return _appearance.ZIndex;
                }

                return 0;
            }
        }

        /// <inheritdoc />
        public float Padding => _appearance.Padding;

        /// <inheritdoc />
        public bool IgnoreOthers => _appearance.IgnoreOthers;

        /// <inheritdoc />
        public bool RotateWithMap => _appearance.RotateWithMap;

        /// <summary>
        /// Is the UI element shown?
        /// </summary>
        public bool Shown
        {
            get;
            private set;
        }

        /// <summary>
        /// Is the UI element active?
        /// </summary>
        public bool Active
        {
            get;
            private set;
        }

        private bool InCanvas => _canvasRectTransform.rect.Contains(
            _graphic.rectTransform.anchoredPosition);

        private readonly Rigidbody2D _rigidbody;
        private readonly BoxCollider2D _collider;
        private readonly Graphic _graphic;
        private Vector3d _position;
        private Canvas _canvas;
        private RectTransform _canvasRectTransform;
        private Transformd _anchor;
        private Vector3 _worldPosition;
        private Quaternion _localRotation;
        private bool _shownOnCanvas;
        private bool _collisionHiding;
        private bool _anchorChanged;
        private bool _shownChanged;
        private IUIRenderableAppearance _appearance;
        private int _collisionCount;

        /// <summary>
        /// Initializes a new instance of PoolableUIElementImpl
        /// </summary>
        /// <param name="graphic">The graphic being rendered by the element</param>
        /// <param name="collider">The collider for the element</param>
        /// <param name="rigidbody">The rigidbody for the element</param>
        public PoolableUIElementImpl(Graphic graphic, BoxCollider2D collider, 
            Rigidbody2D rigidbody)
        {
            if (graphic == null)
            {
                throw new ArgumentNullException(nameof(graphic));
            }

            if (collider == null)
            {
                throw new ArgumentNullException(nameof(collider));
            }

            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            // set rigidbody up
            _rigidbody = rigidbody;
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
            _rigidbody.simulated = true;
            _rigidbody.interpolation = RigidbodyInterpolation2D.None;

            // set collider up
            _collider = collider;
            _collider.isTrigger = true;
            _graphic = graphic;

            // make sure that the graphic renders above everything
            graphic.materialForRendering.SetInt("unity_GUIZTestMode",
                (int)UnityEngine.Rendering.CompareFunction.Always);
        }

        /// <summary>
        /// Shows the element
        /// </summary>
        /// <param name="canvas">The canvas to show the element on</param>
        /// <param name="position">The world position to show the element</param>
        /// <param name="anchor">The anchor for the ui element</param>
        /// <param name="appearance">The appearance of the ui element</param>
        public void Show(Canvas canvas, Vector3d position, Transformd anchor,
            IUIRenderableAppearance appearance)
        {
            if (canvas == null)
            {
                throw new ArgumentNullException(nameof(canvas));
            }

            if (anchor == null)
            {
                throw new ArgumentNullException(nameof(anchor));
            }

            if (appearance == null)
            {
                throw new ArgumentNullException(nameof(appearance));
            }

            _appearance = appearance;
            _graphic.rectTransform.SetParent(canvas.transform, false);
            _canvas = canvas;
            _position = position;
            _anchor = anchor;
            _anchor.Changed += OnAnchorChanged;
            _canvasRectTransform = canvas.transform as RectTransform;

            Active = true;
            OnAnchorChanged();
            OnShow();
        }

        /// <summary>
        /// Hides the element
        /// </summary>
        public void Hide()
        {
            Active = false;
            _anchor.Changed -= OnAnchorChanged;
            OnHide();
        }

        /// <summary>
        /// Called by the holding MonoBehaviour on LateUpdate
        /// </summary>
        public void LateUpdate()
        {
            if (_anchorChanged)
            {
                _graphic.rectTransform.SetToWorldPosition(_worldPosition, _canvas);

                if (RotateWithMap)
                {
                    _graphic.rectTransform.localRotation = _localRotation;
                }

                _anchorChanged = false;

                // should be shown
                if (Shown)
                {
                    // must be overlapping the canvas to be shown on canvas
                    if (InCanvas && !_collisionHiding)
                    {
                        ShowOnCanvas();
                    }
                    else
                    {
                        HideOnCanvas();
                    }
                }
            }

            // has the shown state changed
            if (_shownChanged)
            {
                // should be shown
                if (Shown)
                {
                    // is it not yet shown on the canvas yet?
                    if (!_shownOnCanvas)
                    {
                        // must be overlapping the canvas to be shown on canvas
                        if (InCanvas)
                        {
                            ShowOnCanvas();
                        }
                    }
                }
                // should not be shown at all
                else
                {
                    HideOnCanvas();
                }

                _shownChanged = false;
            }
        }

        /// <summary>
        /// Called when the collider makes contact with another 2d collider
        /// </summary>
        /// <param name="collider">The collider the collider made contact with</param>
        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider == null)
            {
                throw new ArgumentNullException(nameof(collider));
            }

            if (Shown && Active && !IgnoreOthers)
            {
                var uiElement = collider.gameObject.GetComponent<PoolableUIElement>();

                if (uiElement != null && uiElement.Shown && !uiElement.IgnoreOthers)
                {
                    if (uiElement.ZIndex >= ZIndex)
                    {
                        _collisionHiding = true;
                        Hide();
                    }
                }

                ++_collisionCount;
            }
        }

        /// <summary>
        /// Called when the collider exits contact with another 2d collider
        /// </summary>
        /// <param name="collider">The collider the collider exited contact with</param>
        public void OnTriggerExit2D(Collider2D collider)
        {
            if (collider == null)
            {
                throw new ArgumentNullException(nameof(collider));
            }

            if (Active)
            {
                if (--_collisionCount == 0)
                {
                    if (!Shown)
                    {
                        _collisionHiding = false;
                        OnShow();
                    }
                }
            }
        }

        private void OnAnchorChanged()
        {
            _worldPosition = (_anchor.Position + _position * _anchor.Rotation).Vector3();
            _localRotation = _anchor.LocalRotation.Quaternion();
            _anchorChanged = true;
        }

        private void OnShow()
        {
            Shown = true;
            _shownChanged = true;
        }

        private void OnHide()
        {
            Shown = false;
            _collisionHiding = false;
            _shownChanged = true;
        }

        private void HideOnCanvas()
        {
            _graphic.enabled = false;
            _rigidbody.simulated = false;
        }

        private void ShowOnCanvas()
        {
            _collisionCount = 0;
            _graphic.enabled = true;
            _rigidbody.simulated = true;
            _collider.size = _graphic.rectTransform.sizeDelta * _appearance.Padding;
            _shownOnCanvas = true;
        }
    }
}