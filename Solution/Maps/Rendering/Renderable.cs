using Maps.Geometry;

namespace Maps.Rendering
{
    /// <summary>
    /// Represents a renderable object
    /// </summary>
    public abstract class Renderable
    {
        /// <summary>
        /// The bounds
        /// </summary>
        public Bounds3d Bounds
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new Renderable instance
        /// </summary>
        /// <param name="bounds">The bounds of the object in world space</param>
        protected Renderable(Bounds3d bounds)
        {
            Bounds = bounds;
        }

        /// <summary>
        /// Makes the renderable relative, returning itself
        /// </summary>
        /// <param name="anchor">The point to evaluate against</param>
        /// <param name="scale">The scale to apply during the evaluation</param>
        public virtual Renderable Relative(Vector3d anchor, double scale)
        {
            Bounds = new Bounds3d(Bounds.Centre - anchor, Bounds.Extents * 2);

            return this;
        }

        /// <summary>
        /// Accepts the IRenderableVisitor
        /// </summary>
        /// <param name="visitor">The visitor</param>
        public abstract void Accept(IRenderableVisitor visitor);
    }
}