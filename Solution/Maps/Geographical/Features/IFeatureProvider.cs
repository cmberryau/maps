using System;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// Entry point to feature sources and targets
    /// </summary>
    public interface IFeatureProvider : IDisposable
    {
        /// <summary>
        /// The level of offline support provided
        /// </summary>
        OfflineSupportLevel OfflineSupport
        {
            get;
        }

        /// <summary>
        /// Is the feature provider readonly?
        /// </summary>
        bool ReadOnly
        {
            get;
        }

        /// <summary>
        /// Is the feature provider tiled?
        /// </summary>
        bool Tiled
        {
            get;
        }

        /// <summary>
        /// The feature source, if available
        /// </summary>
        /// <exception cref="System.NotSupportedException">Thrown when
        /// the IFeatureProvider is marked as Tiled</exception>
        IFeatureSource FeatureSource();

        /// <summary>
        /// The tiled feature source, if available
        /// </summary>
        /// <exception cref="System.NotSupportedException">Thrown when
        /// the IFeatureProvider is marked as non Tiled</exception>
        ITiledFeatureSource TiledFeatureSource();

        /// <summary>
        /// The feature target, if available
        /// </summary>
        /// <exception cref="System.NotSupportedException">Thrown when
        /// the IFeatureProvider is marked as ReadOnly</exception>
        /// <exception cref="System.NotSupportedException">Thrown when
        /// the IFeatureProvider is marked as Tiled</exception>
        IFeatureTarget FeatureTarget();

        /// <summary>
        /// The tiled feature target, if available
        /// </summary>
        /// <exception cref="System.NotSupportedException">Thrown when
        /// the IFeatureProvider is marked as ReadOnly</exception>
        /// <exception cref="System.NotSupportedException">Thrown when
        /// the IFeatureProvider is marked as non Tiled</exception>
        ITiledFeatureTarget TiledFeatureTarget();
    }
}