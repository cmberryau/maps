using System;
using UnityEngine;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Represents a single ui element
    /// </summary>
    public abstract class UIElement
    {
        /// <summary>
        /// The map canvas to display the ui element on
        /// </summary>
        protected readonly Canvas Canvas;

        /// <summary>
        /// The world position of the ui element
        /// </summary>
        protected readonly Vector3d Position;

        /// <summary>
        /// Initializes a new instance of MapUIElement
        /// </summary>
        /// <param name="canvas">The canvas to display the element on</param>
        /// <param name="position">The world position of the element</param>
        protected UIElement(Canvas canvas, Vector3d position)
        {
            if (canvas == null)
            {
                throw new ArgumentNullException(nameof(canvas));
            }

            Position = position;
            Canvas = canvas;
        }

        /// <summary>
        /// Shows the element
        /// </summary>
        /// <param name="prefabPool">The pool to get an element from</param>
        /// <param name="anchor">The anchor for the element</param>
        public abstract void Show(IPrefabPool prefabPool, Transformd anchor);

        /// <summary>
        /// Hides the element
        /// </summary>
        /// <param name="prefabPool">The pool to return the element to</param>
        public abstract void Hide(IPrefabPool prefabPool);
    }
}