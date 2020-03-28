using System.Collections.Generic;

namespace Maps.Geometry.Tessellation
{
    /// <summary>
    /// Interface for tessellators that can points representing a polygon
    /// </summary>
    public interface IPolygonTessellator
    {
        /// <summary>
        /// Tessellates a list of points
        /// </summary>
        /// <param name="points">The list of outer points</param>
        Mesh Tessellate(IList<Vector3d> points);

        /// <summary>
        /// Tessellates a list of points
        /// </summary>
        /// <param name="points">A list of point lists to tessellate, the first 
        /// being the outer points of polygon, any following being holes</param>
        Mesh Tessellate(IList<IList<Vector3d>> points);

        /// <summary>
        /// Tessellates a list of points
        /// </summary>
        /// <param name="points">The list of outer points</param>
        /// <param name="holes">The list of holes as point lists</param>
        Mesh Tessellate(IList<Vector3d> points, IList<IList<Vector3d>> holes);
    }
}