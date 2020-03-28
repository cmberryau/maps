using System;
using System.Collections.Generic;
using Maps.Appearance;
using Maps.Rendering;
using Maps.Unity.Appearance;
using UnityEngine;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Responsible for creating Sprite instances
    /// </summary>
    public class SpriteCreator : UIElementCreator
    {
        private readonly SpriteAppearance _appearance;
        private readonly ITexture2DModel _textureModel;

        /// <inheritdoc />
        public SpriteCreator(Canvas canvas, SpriteAppearance appearance, 
            ITexture2DModel textureModel) : base(canvas)
        {
            if (appearance == null)
            {
                throw new ArgumentNullException(nameof(appearance));
            }

            if (textureModel == null)
            {
                throw new ArgumentNullException(nameof(textureModel));
            }

            _appearance = appearance;
            _textureModel = textureModel;
        }

        /// <inheritdoc />
        public override UIElement Create(UIRenderable renderable)
        {
            if (renderable == null)
            {
                throw new ArgumentNullException(nameof(renderable));
            }

            // todo: maybe think about a UIRenderable visitor pattern here
            var uiSprite = renderable as UISprite;
            if (uiSprite != null)
            {
                // fetch the textures from the texture model
                var textures = new List<Texture2D>();
                for(var i = 0; i < uiSprite.ImageCount; ++i)
                {
                    textures.Add(_textureModel.TextureFor(uiSprite[i]));
                }

                return new Sprite(Canvas, renderable.Position, _appearance, textures, uiSprite);
            }

            throw new NotImplementedException();
        }
    }
}