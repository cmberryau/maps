namespace Maps.Geometry
{
    /// <summary>
    /// Describes the topology of a mesh
    /// </summary>
    public enum Topology : byte
    {
        /// <summary>
        /// Mesh is a collection of triangles
        /// </summary>
        Triangles = 0,

        /// <summary>
        /// Mesh is a collection of quads
        /// </summary>
        Quads,

        /// <summary>
        /// Mesh is a collection of lines
        /// </summary>
        Lines,

        /// <summary>
        /// Mesh is a single linestrip
        /// </summary>
        LineStrip,

        /// <summary>
        /// Mesh is a collection of points
        /// </summary>
        Points
    }
}