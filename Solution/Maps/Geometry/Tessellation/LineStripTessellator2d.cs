using System;
using System.Collections.Generic;

namespace Maps.Geometry.Tessellation
{
    /// <summary>
    /// Tessellates input points into two dimensional linestrip meshes
    /// </summary>
    internal class LineStripTessellator2d : ILineTessellator
    {
        /// <inheritdoc />
        public Mesh Tessellate(IList<Vector3d> points)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            return DoTessellate(points);
        }

        /// <inheritdoc />
        public Mesh Tessellate(IList<IList<Vector3d>> points)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            var mesh = new Mesh(Topology.LineStrip);

            for (var i = 0; i < points.Count; ++i)
            {
                mesh.Append(DoTessellate(points[i]));
            }

            return mesh;
        }

        private static Mesh DoTessellate(IList<Vector3d> points)
        {
            var vertices = new Vector3d[points.Count];
            var indices = new int[points.Count];

            for (var i = 0; i < points.Count; i++)
            {
                vertices[i] = new Vector3d(points[i].x, points[i].y, 0);
                indices[i] = i;
            }

            var mesh = new Mesh(Topology.LineStrip);

            mesh.SetVertices(vertices);
            mesh.SetIndices(indices);
            return mesh;
        }
    }
}