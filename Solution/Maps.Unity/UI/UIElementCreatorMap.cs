using System;
using System.Collections.Generic;
using Maps.Appearance;
using Maps.Unity.Appearance;
using UnityEngine;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Responsible for creating UIElementCreator instances
    /// </summary>
    public class UIElementCreatorMap
    {
        private readonly Canvas _canvas;
        private readonly ITexture2DModel _textureModel;
        private readonly IDictionary<UIRenderableAppearance, UIElementCreator> _map;

        /// <summary>
        /// Initializes a new instance of UIElementCreatorFactory
        /// </summary>
        public UIElementCreatorMap(Canvas canvas, ITexture2DModel textureModel,
            IList<UIRenderableAppearance> appearances)
        {
            if (canvas == null)
            {
                throw new ArgumentNullException(nameof(canvas));
            }

            if (textureModel == null)
            {
                throw new ArgumentNullException(nameof(textureModel));
            }

            if (appearances == null)
            {
                throw new ArgumentNullException(nameof(appearances));
            }

            _canvas = canvas;
            _textureModel = textureModel;
            _map = CreateMap(canvas, textureModel, appearances);
            Debug.Log($"Total of {_map.Count} UI creators generated");
        }

        /// <summary>
        /// Returns the creator for a given ui element appearance
        /// </summary>
        /// <param name="appearance">The appearance to return a creator for</param>
        public UIElementCreator Creator(UIRenderableAppearance appearance)
        {
            if (appearance == null)
            {
                throw new ArgumentNullException(nameof(appearance));
            }

            if (!_map.ContainsKey(appearance))
            {
                var generator = new UIElementCreatorGenerator(_canvas, _textureModel,
                    appearance);
                _map[appearance] = generator.Creator;

                Debug.Log($"Additional UI creator generated, {_map.Count} in total");
            }

            return _map[appearance];
        }

        private static IDictionary<UIRenderableAppearance, UIElementCreator> CreateMap(
            Canvas canvas, ITexture2DModel textureModel, IList<UIRenderableAppearance> appearances)
        {
            var map = new Dictionary<UIRenderableAppearance, UIElementCreator>();

            for (var i = 0; i < appearances.Count; i++)
            {
                if (appearances[i] == null)
                {
                    throw new ArgumentException("Contains null element at index " +
                                                $"{i}");
                }

                var concrete = new UIElementCreatorGenerator(canvas, textureModel,
                    appearances[i]);
                map[appearances[i]] = concrete.Creator;
            }

            return map;
        }

        /// <summary>
        /// Creates the correct UIElementCreator for the concrete UIElementAppearance
        /// </summary>
        private class UIElementCreatorGenerator : IUIElementAppearanceVisitor
        {
            /// <summary>
            /// The resulting creator
            /// </summary>
            public UIElementCreator Creator
            {
                get;
                private set;
            }

            private readonly Canvas _canvas;
            private readonly ITexture2DModel _textureModel;

            /// <summary>
            /// Initializes a new instance of UIElementCreatorGenerator
            /// </summary>
            /// <param name="canvas">The canvas the creator should use</param>
            /// <param name="textureModel">The texture model to use</param>
            /// <param name="appearance">The appearance to create a creator for</param>
            public UIElementCreatorGenerator(Canvas canvas, ITexture2DModel textureModel,
                UIRenderableAppearance appearance)
            {
                if (appearance == null)
                {
                    throw new ArgumentNullException(nameof(appearance));
                }

                _canvas = canvas;
                _textureModel = textureModel;
                appearance.Accept(this);
            }

            /// <inheritdoc />
            public void Visit(LabelAppearance appearance)
            {
                if (appearance == null)
                {
                    throw new ArgumentNullException(nameof(appearance));
                }

                Creator = new LabelCreator(_canvas, appearance);
            }

            /// <inheritdoc />
            public void Visit(IconAppearance appearance)
            {
                if (appearance == null)
                {
                    throw new ArgumentNullException(nameof(appearance));
                }

                Creator = new IconCreator(_canvas, appearance, _textureModel);
            }

            /// <inheritdoc />
            public void Visit(SpriteAppearance appearance)
            {
                if (appearance == null)
                {
                    throw new ArgumentNullException(nameof(appearance));
                }

                Creator = new SpriteCreator(_canvas, appearance, _textureModel);
            }
        }
    }
}