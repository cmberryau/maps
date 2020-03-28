namespace Maps.Geometry.Tessellation
{
    /// <summary>
    /// Interface for tessellators that can accept single points 
    /// </summary>
    public interface IPointTessellator
    {
        /// <summary>
        /// Tessellates a single point into a mesh
        /// </summary>
        /// <param name="point">A point to tessellate</param>
        Mesh Tessellate(Vector3d point);
    }
}