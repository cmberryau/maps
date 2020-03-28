using System;
using System.Collections.Generic;
using log4net;
using Maps.Unity.Interaction.Response;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Maps.Unity.Interaction.Input
{
    /// <summary>
    /// Handles input and calls appropriate responders
    /// </summary>
    public abstract class InputHandler
    {
        private readonly IList<IPointerClickResponder> _pointerClickResponders;
        private readonly IList<IPointerDragResponder> _pointerDragResponders;
        private readonly IList<IPointerScrollResponder> _pointerScrollResponders;
        private readonly IDictionary<PointerEventData.InputButton, Vector3> _pointerDragLastWorld;
        private readonly IDictionary<PointerEventData.InputButton, Vector3> _pointerDragPressWorldPosition;

        private static readonly ILog Log = LogManager.GetLogger(typeof(InputHandler));

        /// <summary>
        /// Initializes a new instance of InputHandler
        /// </summary>
        protected InputHandler()
        {
            _pointerClickResponders = new List<IPointerClickResponder>();
            _pointerDragResponders = new List<IPointerDragResponder>();
            _pointerScrollResponders = new List<IPointerScrollResponder>();
            _pointerDragLastWorld = new Dictionary<PointerEventData.InputButton, Vector3>();
            _pointerDragPressWorldPosition = new Dictionary<PointerEventData.InputButton, Vector3>();
        }

        /// <summary>
        /// Creates an InputHandler appropritate for the given hardware platform
        /// </summary>
        public static InputHandler Create()
        {
            InputHandler handler;

            if (Application.isMobilePlatform)
            {
                throw new NotImplementedException();
            }
            else
            {
                handler = new DesktopInputHandler();
            }

            return handler;
        }

        /// <summary>
        /// Adds a responder
        /// </summary>
        /// <param name="obj">The responder object to add</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="obj"/> is null</exception>
        public virtual void AddResponder(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (obj is IPointerClickResponder)
            {
                _pointerClickResponders.Add((IPointerClickResponder)obj);
            }

            if (obj is IPointerDragResponder)
            {
                _pointerDragResponders.Add((IPointerDragResponder)obj);
            }

            if (obj is IPointerScrollResponder)
            {
                _pointerScrollResponders.Add((IPointerScrollResponder)obj);
            }
        }

        /// <summary>
        /// Called when the pointer has been pressed down
        /// </summary>
        /// <param name="eventData">The pointer event data</param>
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (eventData == null)
            {
                throw new ArgumentNullException(nameof(eventData));
            }
        }

        /// <summary>
        /// Called when the pointer has been released
        /// </summary>
        /// <param name="eventData">The pointer event data</param>
        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (eventData == null)
            {
                throw new ArgumentNullException(nameof(eventData));
            }
        }

        /// <summary>
        /// Called when the pointer has been clicked
        /// </summary>
        /// <param name="eventData">The pointer event data</param>
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData == null)
            {
                throw new ArgumentNullException(nameof(eventData));
            }

            if (!eventData.dragging)
            {
                for (var i = 0; i < _pointerClickResponders.Count; ++i)
                {
                    _pointerClickResponders[i].RecievedPointerClick(eventData.position,
                        eventData.pointerCurrentRaycast.worldPosition, eventData.button);
                }
            }
        }

        /// <summary>
        /// Called when the pointer has been dragged
        /// </summary>
        /// <param name="eventData">The pointer event data</param>
        public virtual void OnPointerDrag(PointerEventData eventData)
        {
            if (eventData == null)
            {
                throw new ArgumentNullException(nameof(eventData));
            }

            Vector3 lastWorld;
            if (_pointerDragPressWorldPosition.ContainsKey(eventData.button))
            {
                // a continued drag
                if (_pointerDragPressWorldPosition[eventData.button] ==
                    eventData.pointerPressRaycast.worldPosition)
                {
                    lastWorld = _pointerDragLastWorld[eventData.button];
                } // a new drag for a button which has had a drag before
                else
                {
                    _pointerDragPressWorldPosition[eventData.button] =
                        eventData.pointerPressRaycast.worldPosition;
                    lastWorld = eventData.pointerPressRaycast.worldPosition;
                }
            } // a new drag for a button which has never had a drag
            else
            {
                _pointerDragPressWorldPosition[eventData.button] =
                    eventData.pointerPressRaycast.worldPosition;
                lastWorld = eventData.pointerPressRaycast.worldPosition;
            }

            var startScreenPosition = eventData.position;
            var endScreenPosition = startScreenPosition + eventData.delta;

            var startWorldPosition = lastWorld;
            var endWorldPosition = eventData.pointerCurrentRaycast.worldPosition;

            for (var i = 0; i < _pointerDragResponders.Count; ++i)
            {
                _pointerDragResponders[i].RecievedPointerDrag(startScreenPosition, 
                    startWorldPosition, endScreenPosition, endWorldPosition, eventData.button);
            }

            _pointerDragLastWorld[eventData.button] = endWorldPosition;
        }

        /// <summary>
        /// Called when the pointer has scrolled
        /// </summary>
        /// <param name="eventData">The pointer event data</param>
        public virtual void OnPointerScroll(PointerEventData eventData)
        {
            if (eventData == null)
            {
                throw new ArgumentNullException(nameof(eventData));
            }

            for (var i = 0; i < _pointerScrollResponders.Count; ++i)
            {
                _pointerScrollResponders[i].RecievedPointerScroll(eventData.position,
                    eventData.pointerCurrentRaycast.worldPosition, eventData.scrollDelta);
            }
        }

        /// <summary>
        /// Should be called by the holding MonoBehaviour to update the InputHandler
        /// </summary>
        public virtual void Update()
        {
            
        }
    }
}