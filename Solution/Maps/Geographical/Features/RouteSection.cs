using System;
using System.Collections.Generic;
using Maps.Appearance;
using Maps.Geographical.Projection;
using Maps.Geometry.Tessellation;
using Maps.Rendering;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// Represents a route section
    /// </summary>
    public class RouteSection : DynamicFeature
    {
        private readonly IReadOnlyList<Geodetic2d> _points;

        /// <inheritdoc />
        public RouteSection(string name, IReadOnlyList<Geodetic2d> points) : base(name)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            Coordinate = new Geodetic3d(points[0], 0d);
            _points = points;
        }

        /// <inheritdoc />
        public override IList<Renderable> Renderables(IProjection projection)
        {
            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            var projected = projection.Forward(_points);

            var outerTessellator = new TriangleLineTessellator2d(Mathd.EpsilonE6 * 2.5d);
            var outerMesh = outerTessellator.Tessellate(projected);
            var outerAppearance = new MeshAppearance(99, Colorf.Red, true);

            var innerTessellator = new TriangleLineTessellator2d(Mathd.EpsilonE6 * 1.75d);
            var innerMesh = innerTessellator.Tessellate(projected);
            var innerAppearance = new MeshAppearance(100, Colorf.Red * 1.5d, true);

            return new List<Renderable>
            {
                new MeshRenderable(outerMesh.Bounds, outerMesh, outerAppearance),
                new MeshRenderable(innerMesh.Bounds, innerMesh, innerAppearance)
            };
        }
    }
}