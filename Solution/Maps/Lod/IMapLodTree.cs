namespace Maps.Lod
{
    /// <summary>
    /// Interface for objects with the responsibility of managing a map's lod tree
    /// </summary>
    public interface IMapLodTree
    {
        /// <summary>
        /// The anchor of the lod tree
        /// </summary>
        Transformd Anchor
        {
            get;
        }

        /// <summary>
        /// The transform of the lod tree
        /// </summary>
        Transformd Transform
        {
            get;
        }

        /// <summary>
        /// Creates a map lod
        /// </summary>
        /// <param name="level">The level to create the lod for</param>
        /// <param name="scale">The scale of the lod</param>
        IMapLod CreateLod(int level, double scale);

        /// <summary>
        /// Notifies the lod tree that there has been an update
        /// </summary>
        void OnUpdate();
    }
}