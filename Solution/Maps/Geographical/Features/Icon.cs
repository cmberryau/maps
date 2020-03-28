using System;
using System.Collections.Generic;
using System.Drawing;
using Maps.Appearance;
using Maps.Geographical.Projection;
using Maps.Geometry;
using Maps.Rendering;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// The base icon
    /// </summary>
    public class Icon : DynamicFeature
    {
        private const string SpriteImageName = @"Maps.Resources.DefaultIcon.png";
        private static readonly Bitmap SpriteBitmap;

        static Icon()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(SpriteImageName))
            {
                if (stream != null)
                {
                    SpriteBitmap = new Bitmap(Image.FromStream(stream));
                }
            }
        }

        private readonly SpriteAppearance _appearance;
        private UISprite _sprite;

        /// <inheritdoc />
        public Icon(string name) : base(name)
        {
            _appearance = new SpriteAppearance(RenderableAppearance.DefaultZIndex, UIRenderableAppearance.DefaultPadding, true, true, SpriteAppearance.DefaultBackgroundColor);
        }

        /// <inheritdoc />
        public override IList<Renderable> Renderables(IProjection projection)
        {
            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            var position = projection.Forward(Coordinate);
            _sprite = new UISprite(new Bounds3d(position, Vector3d.One), position, _appearance, Name, new[] { SpriteBitmap });

            var renderables = new List<Renderable>
            {
                _sprite
            };

            return renderables;
        }
    }
}