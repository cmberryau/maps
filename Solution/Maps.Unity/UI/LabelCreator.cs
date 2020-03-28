using System;
using Maps.Appearance;
using Maps.Rendering;
using UnityEngine;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Responsible for creating Label instances
    /// </summary>
    public class LabelCreator : UIElementCreator
    {
        private readonly LabelAppearance _appearance;

        /// <summary>
        /// Initializes a new instance of UIElementCreator
        /// </summary>
        /// <param name="canvas">The canvas to which ui elements will reside on</param>
        /// <param name="appearance">The appearance to create elements for</param>
        public LabelCreator(Canvas canvas, LabelAppearance appearance) : base(canvas)
        {
            if (appearance == null)
            {
                throw new ArgumentNullException(nameof(appearance));
            }

            _appearance = appearance;
        }

        /// <summary>
        /// Creates a new UI element
        /// </summary>
        /// <param name="renderable">The renderable to create an element from</param>
        public override UIElement Create(UIRenderable renderable)
        {
            if (renderable == null)
            {
                throw new ArgumentNullException(nameof(renderable));
            }

            return new Label(Canvas, renderable.Position, renderable, _appearance);
        }
    }
}