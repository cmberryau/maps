using System;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// Responsible for managing dynamic features on a map
    /// </summary>
    public abstract class DynamicFeatureManagerBase : IDynamicFeatureManager
    {
        /// <inheritdoc />
        public Transformd Transform
        {
            get;
        }

        /// <summary>
        /// The map that the feature manager is associated with
        /// </summary>
        protected ITiledMap Map
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of DynamicFeatureManagerBase
        /// </summary>
        protected DynamicFeatureManagerBase()
        {
            Transform = Transformd.Identity;
            Transform.Changed += OnShouldAssumeTransform;
        }

        /// <inheritdoc />
        public void AttachTo(ITiledMap map)
        {
            if (map == null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            Map = map;
        }

        /// <inheritdoc />
        public void Add(IDynamicFeature feature)
        {
            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature));
            }

            OnAdded(feature);
        }

        /// <summary>
        /// Called when a dynamic feature has been added to the manager
        /// </summary>
        /// <param name="feature">The feature that has been added</param>
        protected virtual void OnAdded(IDynamicFeature feature)
        {
            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature));
            }
        }

        /// <summary>
        /// Called when the manager should assume it's transform
        /// </summary>
        protected abstract void OnShouldAssumeTransform();
    }
}