using System;
using System.Collections.Generic;
using Maps.Unity.Interaction.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Maps.Unity.Interaction
{
    /// <summary>
    /// Responsible for repeating input from a target image
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class TargetImageInputForwarder : MonoBehaviour, IPointerDownHandler,
        IPointerUpHandler, IPointerClickHandler, IDragHandler, IScrollHandler
    {
        private RectTransform _rectTransform;
        private InputHandler _inputHandler;
        private Camera _inputHandlerCamera;

        private PhysicsRaycaster _rayCaster;
        private RaycastResult _pressRaycastResult;

        /// <summary>
        /// Initializes the TargetImageInputRepeater instance
        /// </summary>
        /// <param name="inputHandler">The input handler to send repeated input to</param>
        /// <param name="inputHandlerCamera">The camera of the input handler</param>
        public void Initialize(InputHandler inputHandler, Camera inputHandlerCamera)
        {
            if (inputHandler == null)
            {
                throw new ArgumentNullException(nameof(inputHandler));
            }

            if (inputHandlerCamera == null)
            {
                throw new ArgumentNullException(nameof(inputHandlerCamera));
            }

            _inputHandler = inputHandler;
            _inputHandlerCamera = inputHandlerCamera;
            _rayCaster = _inputHandlerCamera.gameObject.GetComponent<PhysicsRaycaster>();
        }

        /// <inheritdoc />
        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject == gameObject)
            {
                eventData = Project(eventData);
                _inputHandler.OnPointerDown(eventData);
            }
        }

        /// <inheritdoc />
        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject == gameObject)
            {
                eventData = Project(eventData);
                _inputHandler.OnPointerUp(eventData);
            }
        }

        /// <inheritdoc />
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject == gameObject)
            {
                eventData = Project(eventData);
                _inputHandler.OnPointerDrag(eventData);
            }
        }

        /// <inheritdoc />
        public void OnDrag(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject == gameObject)
            {
                eventData = Project(eventData);
                _inputHandler.OnPointerDrag(eventData);
            }
        }

        /// <inheritdoc />
        public void OnScroll(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject == gameObject)
            {
                eventData = Project(eventData);
                _inputHandler.OnPointerScroll(eventData);
            }
        }

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private PointerEventData Project(PointerEventData eventData)
        {
            var halfSizeDelta = _rectTransform.sizeDelta / 2;

            // determine the two positions on the rect
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform,
                eventData.position, eventData.pressEventCamera, 
                out Vector2 rectPosition);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform,
                eventData.pressPosition, eventData.pressEventCamera, 
                out Vector2 rectPressPosition);

            // determine the two positions on the render texture
            var position = new Vector3(rectPosition.x + halfSizeDelta.x,
                rectPosition.y + halfSizeDelta.y, 0);
            var pressPosition = new Vector3(rectPressPosition.x + halfSizeDelta.x,
                rectPressPosition.y + halfSizeDelta.y, 0);

            var newEventData = new PointerEventData(EventSystem.current)
            {
                button = eventData.button,
                dragging = eventData.dragging,
                useDragThreshold = eventData.useDragThreshold,
                scrollDelta = eventData.scrollDelta,
                clickCount = eventData.clickCount,
                clickTime = eventData.clickTime,
                pressPosition = pressPosition,
                delta = eventData.delta,
                position = position,
                pointerId = eventData.pointerId,
                eligibleForClick = eventData.eligibleForClick
            };

            var results = new List<RaycastResult>();
            _rayCaster.Raycast(newEventData, results);

            if (results.Count > 0)
            {
                // if this is the press event, save it for later use
                if (position == pressPosition)
                {
                    _pressRaycastResult = results[0];
                }

                newEventData.pointerPress = _pressRaycastResult.gameObject;

                newEventData.pointerCurrentRaycast = results[0];
                newEventData.pointerPressRaycast = _pressRaycastResult;
            }

            return newEventData;
        }
    }
}