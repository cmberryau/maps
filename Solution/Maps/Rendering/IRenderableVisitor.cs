namespace Maps.Rendering
{
    /// <summary>
    /// Interface for concrete Renderable access
    /// </summary>
    public interface IRenderableVisitor
    {
        /// <summary>
        /// Visits the MeshRenderable
        /// </summary>
        /// <param name="renderable">The MeshRenderable to visit</param>
        void Visit(MeshRenderable renderable);

        /// <summary>
        /// Visits the PointRenderable
        /// </summary>
        /// <param name="renderable">The PointRenderable to visit</param>
        void Visit(UIRenderable renderable);
    }
}