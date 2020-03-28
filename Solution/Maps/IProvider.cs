using System;
using Maps.Geographical.Features;
using Maps.Geographical.Places;

namespace Maps
{
    /// <summary>
    /// Indicates levels of support for offline (no internet connection) usage
    /// </summary>
    public enum OfflineSupportLevel
    {
        /// <summary>
        /// No offline support is provided at all
        /// </summary>
        None,

        /// <summary>
        /// Partial offline support is provided, some features or data may
        /// be missing
        /// </summary>
        Partial,

        /// <summary>
        /// Complete offline support is provided, all features and data are
        /// present regardless of being offline or online
        /// </summary>
        Full
    }

    /// <summary>
    /// Entry point to client-specific Maps interfaces
    /// </summary>
    public interface IProvider : IDisposable
    {
        /// <summary>
        /// Does the provider have support for places?
        /// </summary>
        bool PlacesSupported
        {
            get;
        }

        /// <summary>
        /// Does the provider have support for map features?
        /// </summary>
        bool FeaturesSupported
        {
            get;
        }

        /// <summary>
        /// The place provider
        /// </summary>
        /// <exception cref="System.NotSupportedException">Thrown when places
        /// are not supported by the provider</exception>
        IPlaceProvider PlaceProvider
        {
            get;
        }

        /// <summary>
        /// The feature provider
        /// </summary>
        /// <exception cref="System.NotSupportedException">Thrown when map 
        /// features are not supported by the provider</exception>
        IFeatureProvider FeatureProvider
        {
            get;
        }
    }
}