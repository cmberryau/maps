using System;
using System.Collections.Generic;
using System.Drawing;
using Maps.Appearance;
using Maps.Collections;
using Maps.Geographical.Projection;
using Maps.Geometry;
using Maps.Rendering;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// Represents a car icon as a dynamic feature
    /// </summary>
    public class CarIcon : DynamicFeature
    {
        private static readonly IReadOnlyList<Bitmap> SpriteImages;
        private const string SpriteImageNamePrefix = @"Maps.Resources.DefaultArrow.Arrow_{0}.png";
        private const int SpriteImagesCount = 24;
        private const double DegreesPerChange = 360d / SpriteImagesCount;

        static CarIcon()
        {
            // collect the sprite images
            var spriteImages = new List<Bitmap>();
            SpriteImages = new ReadOnlyList<Bitmap>(spriteImages);
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            for (var i = 0; i < SpriteImagesCount; ++i)
            {
                var name = string.Format(SpriteImageNamePrefix, i);
                using (var stream = assembly.GetManifestResourceStream(name))
                {
                    if (stream != null)
                    {
                        spriteImages.Add(new Bitmap(Image.FromStream(stream)));
                    }
                }
            }
        }

        private readonly SpriteAppearance _appearance;
        private UISprite _sprite;

        /// <inheritdoc />
        public CarIcon(string name) : base(name)
        {
            ShouldHeadingRotate = false;

            _appearance = new SpriteAppearance(RenderableAppearance.DefaultZIndex,
                UIRenderableAppearance.DefaultPadding, true, true,
                SpriteAppearance.DefaultBackgroundColor);
        }

        /// <inheritdoc />
        public override IList<Renderable> Renderables(IProjection projection)
        {
            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            var position = projection.Forward(Coordinate);
            _sprite = new UISprite(new Bounds3d(position, Vector3d.One), position, _appearance, Name, SpriteImages);

            var renderables = new List<Renderable>
            {
                _sprite
            };

            return renderables;
        }

        /// <inheritdoc />
        protected override void OnHeadingChanged(double heading)
        {
            base.OnHeadingChanged(heading);

            if (heading < 0)
            {
                heading += 360d;
            }

            _sprite.ImageIndex = (int) (heading / DegreesPerChange);
        }
    }
}