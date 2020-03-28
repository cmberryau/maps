using System;
using System.Drawing;
using Maps.Appearance;
using Maps.Geometry;

namespace Maps.Rendering
{
    /// <summary>
    /// Represents content that is renderable in the user interface
    /// </summary>
    public class UIRenderable : Renderable
    {
        /// <summary>
        /// The position in world space
        /// </summary>
        public Vector3d Position
        {
            get;
            private set;
        }

        /// <summary>
        /// The appearance
        /// </summary>
        public UIRenderableAppearance Appearance
        {
            get;
        }

        /// <summary>
        /// The text content
        /// </summary>
        public string Text
        {
            get;
        }

        /// <summary>
        /// The image content
        /// </summary>
        public Bitmap Image
        {
            get;
        }

        /// <summary>
        /// Initializses a new instance of UIRenderable
        /// </summary>
        /// <param name="bounds">The bounds of the object in world space</param>
        /// <param name="position">The position of the object in world space</param>
        /// <param name="appearance">The appearance to apply to the renderable</param>
        /// <param name="text">The text content of the object</param>
        /// <param name="image">The image content of the object</param>
        public UIRenderable(Bounds3d bounds, Vector3d position, 
            UIRenderableAppearance appearance, string text = null, Bitmap image = null) 
            : base(bounds)
        {
            if (appearance == null)
            {
                throw new ArgumentNullException(nameof(appearance));
            }

            Appearance = appearance;
            Position = position;
            Image = image;
            Text = text ?? string.Empty;
        }

        /// <inheritdoc />
        public override Renderable Relative(Vector3d anchor, double scale)
        {
            base.Relative(anchor, scale);
            Position = Vector3d.Relative(anchor, Position, scale);

            return this;
        }

        /// <inheritdoc />
        public override void Accept(IRenderableVisitor visitor)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException(nameof(visitor));
            }

            visitor.Visit(this);
        }
    }
}