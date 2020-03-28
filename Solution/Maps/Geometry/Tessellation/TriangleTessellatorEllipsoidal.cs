using System;
using System.Collections.Generic;

namespace Maps.Geometry.Tessellation
{
    /// <summary>
    /// Tessellates a set of input points into three dimensional triangles
    /// that lay on an ellipsoidal surface
    /// </summary>
    internal class TriangleTessellatorEllipsoidal : ILineTessellator, IPolygonTessellator
    {
        private readonly Ellipsoid _ellipsoid;

        /// <summary>
        /// Initializes a instance of TriangleTessellatorEllipsoidal
        /// </summary>
        /// <param name="ellipsoid">The ellipsoid to tessellate for</param>
        public TriangleTessellatorEllipsoidal(Ellipsoid ellipsoid)
        {
            if (ellipsoid == null)
            {
                throw new ArgumentNullException(nameof(ellipsoid));
            }

            _ellipsoid = ellipsoid;
        }

        /// <inheritdoc />
        Mesh ILineTessellator.Tessellate(IList<Vector3d> points)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        Mesh IPolygonTessellator.Tessellate(IList<Vector3d> points)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        Mesh IPolygonTessellator.Tessellate(IList<Vector3d> points,
            IList<IList<Vector3d>> holes)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        Mesh IPolygonTessellator.Tessellate(IList<IList<Vector3d>> points)
        {
            throw new NotImplementedException();
        }
    }
}