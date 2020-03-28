namespace Maps.Lod
{
    /// <summary>
    /// Represents the lod tree for a map, responsible for creating LOD's
    /// </summary>
    public abstract class MapLodTreeBase : IMapLodTree
    {
        /// <inheritdoc />
        public Transformd Anchor
        {
            get;
        }

        /// <inheritdoc />
        public Transformd Transform
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of MapLodTreeBase
        /// </summary>
        protected MapLodTreeBase()
        {
            Anchor = Transformd.Identity;
            Transform = Transformd.Identity;
        }

        /// <inheritdoc />
        public abstract IMapLod CreateLod(int level, double scale);

        /// <inheritdoc />
        public void OnUpdate()
        {
            OnShouldAssumeTransform();
        }

        /// <summary>
        /// Called when the transform of the lod tree should change
        /// </summary>
        protected abstract void OnShouldAssumeTransform();
    }
}