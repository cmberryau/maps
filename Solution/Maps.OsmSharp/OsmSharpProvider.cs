using System;
using Maps.Geographical.Features;
using Maps.Geographical.Places;
using Maps.OsmSharp.Geographical.Features;
using Maps.OsmSharp.Geographical.Places;
using OsmSharp.Osm.Data;

namespace Maps.OsmSharp
{
    /// <summary>
    /// Simplest entry point of the OsmSharp provider assembly
    /// </summary>
    public sealed class OsmSharpProvider : IProvider
    {
        /// <summary>
        /// Can the provider provide a Place source?
        /// </summary>
        public bool PlacesSupported
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(OsmSharpProvider));
                }

                return true;
            }
        }

        /// <summary>
        /// Can the provider provide a Feature source?
        /// </summary>
        public bool FeaturesSupported
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(OsmSharpProvider));
                }

                return true;
            }
        }

        /// <summary>
        /// The place provider
        /// </summary>
        public IPlaceProvider PlaceProvider
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(OsmSharpProvider));
                }

                if (_placeProvider == null)
                {
                    _placeProvider = new OsmSharpPlaceProvider(_source);
                }

                return _placeProvider;
            }
        }

        /// <summary>
        /// The feature provider
        /// </summary>
        public IFeatureProvider FeatureProvider
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(OsmSharpProvider));
                }

                if (_featureProvider == null)
                {
                    _featureProvider = new OsmSharpFeatureProvider(_source);
                }

                return _featureProvider;
            }
        }

        private IPlaceProvider _placeProvider;
        private IFeatureProvider _featureProvider;
        private readonly IDataSourceReadOnly _source;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of OsmSharpProvider
        /// </summary>
        /// <param name="source">The OsmSharp data source to use</param>
        public OsmSharpProvider(IDataSourceReadOnly source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            _source = source;
        }
        
        /// <summary>
        /// Disposes of all resources held by the OsmSharpProvider instance
        /// </summary>
        public void Dispose()
        {
            _disposed = true;
        }
    }
}
