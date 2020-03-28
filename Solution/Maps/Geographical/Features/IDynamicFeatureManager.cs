namespace Maps.Geographical.Features
{
    /// <summary>
    /// An interface for dynamic feature managers
    /// </summary>
    public interface IDynamicFeatureManager
    {
        /// <summary>
        /// The parent transform for the features created by the dynamic feature manager
        /// </summary>
        Transformd Transform
        {
            get;
        }

        /// <summary>
        /// Attaches the DynamicFeatureManagerBase instance to map
        /// </summary>
        /// <param name="map">The map to attach to</param>
        void AttachTo(ITiledMap map);

        /// <summary>
        /// Adds a dynamic feature to be managed
        /// </summary>
        /// <param name="feature">The feature to add</param>
        void Add(IDynamicFeature feature);
    }
}