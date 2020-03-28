using System;
using Maps.Appearance;
using Maps.Rendering;
using Maps.Unity.Appearance;
using UnityEngine;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Responsible for creating Icon instances
    /// </summary>
    public class IconCreator : UIElementCreator
    {
        private readonly IconAppearance _appearance;
        private readonly ITexture2DModel _textureModel;

        /// <inheritdoc />
        public IconCreator(Canvas canvas, IconAppearance appearance,
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

            if (renderable.Image != null)
            {
                var texture = _textureModel.TextureFor(renderable.Image);
                return new Icon(Canvas, renderable.Position, _appearance, texture);
            }

            throw new NotImplementedException();
        }
    }
}