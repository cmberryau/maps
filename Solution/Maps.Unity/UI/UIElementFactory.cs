using System;
using System.Collections.Generic;
using Maps.Appearance;
using Maps.Rendering;
using Maps.Unity.Appearance;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Responsible for creating ui elements
    /// </summary>
    public class UIElementFactory
    {
        private readonly UIElementCreatorMap _map;

        /// <summary>
        /// Initializes a new instance of UIElementFactory
        /// </summary>
        /// <param name="appearances">The ui element appearances for the map</param>
        /// <param name="mapCanvas">The map canvas elements will attach to</param>
        /// <param name="textureModel">The texture model</param>
        public UIElementFactory(IList<UIRenderableAppearance> appearances, 
            MapCanvas mapCanvas, ITexture2DModel textureModel)
        {
            if (mapCanvas == null)
            {
                throw new ArgumentNullException(nameof(mapCanvas));
            }

            if (appearances == null)
            {
                throw new ArgumentNullException(nameof(appearances));
            }

            if (textureModel == null)
            {
                throw new ArgumentNullException(nameof(textureModel));
            }

            _map = new UIElementCreatorMap(mapCanvas.Canvas, textureModel, appearances);
        }

        /// <summary>
        /// Creates an ui element
        /// </summary>
        /// <param name="renderable">The ui renderable to create from</param>
        public UIElement Create(UIRenderable renderable)
        {
            if (renderable == null)
            {
                throw new ArgumentNullException(nameof(renderable));
            }

            return _map.Creator(renderable.Appearance).Create(renderable);
        }
    }
}