using System;
using Maps.Appearance;
using Maps.Geometry;

namespace Maps.Rendering
{
    /// <summary>
    /// Represents a mesh object that should be statically rendered in 3d
    /// </summary>
    public class MeshRenderable : Renderable
    {
        /// <summary>
        /// The mesh in world space
        /// </summary>
        public Mesh Mesh
        {
            get;
            private set;
        }

        /// <summary>
        /// The appearance
        /// </summary>
        public MeshAppearance Appearance
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of MeshRenderable
        /// </summary>
        /// <param name="bounds">The bounds of the object in world space</param>
        /// <param name="mesh">The mesh of the object in world space</param>
        /// <param name="appearance">The appearance</param>
        public MeshRenderable(Bounds3d bounds, Mesh mesh, MeshAppearance appearance) :
            base(bounds)
        {
            if (mesh == null)
            {
                throw new ArgumentNullException(nameof(mesh));
            }

            if (appearance == null)
            {
                throw new ArgumentNullException(nameof(appearance));
            }

            Mesh = mesh;
            Appearance = appearance;
        }

        /// <inheritdoc />
        public override Renderable Relative(Vector3d anchor, double scale)
        {
            base.Relative(anchor, scale);
            Mesh = Mesh.Relative(anchor, scale);

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