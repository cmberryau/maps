using System.Collections.Generic;

namespace Maps.Geometry.Tessellation
{
    /// <summary>
    /// Interface for tessellators that can accept points representing a line
    /// </summary>
    public interface ILineTessellator
    {
        /// <summary>
        /// Tessellates a list of points
        /// </summary>
        /// <param name="points">A list of points to tessellate</param>
        Mesh Tessellate(IList<Vector3d> points);
    }
}