using System;
using Maps.Rendering;
using UnityEngine;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Responsible for creating a single type of UI element
    /// </summary>
    public abstract class UIElementCreator
    {
        /// <summary>
        /// The canvas which created UI elements are to be placed on
        /// </summary>
        protected readonly Canvas Canvas;

        /// <summary>
        /// Initializes a new instance of UIElementCreator
        /// </summary>
        /// <param name="canvas">The canvas which created UI elements are to be 
        /// placed on</param>
        protected UIElementCreator(Canvas canvas)
        {
            if (canvas == null)
            {
                throw new ArgumentNullException(nameof(canvas));
            }

            Canvas = canvas;
        }

        /// <summary>
        /// Creates a UI element from a given PointRenderable instance
        /// </summary>
        /// <param name="renderable">The renderable to create from</param>
        public abstract UIElement Create(UIRenderable renderable);
    }
}