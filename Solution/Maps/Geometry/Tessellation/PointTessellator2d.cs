using System;
using System.Collections.Generic;

namespace Maps.Geometry.Tessellation
{
    /// <summary>
    /// Tessellates input points into 2d point meshes
    /// </summary>
    internal class PointTessellator2d : IPointTessellator, ILineTessellator, 
        IPolygonTessellator
    {
        /// <inheritdoc />
        Mesh IPointTessellator.Tessellate(Vector3d point)
        {
            throw new NotImplementedException();
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
        public Mesh Tessellate(IList<IList<Vector3d>> points)
        {
            throw new NotImplementedException();
        }
    }
}