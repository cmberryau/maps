using System;
using System.Drawing;
using Maps.Data.Geographical.Features;
using Maps.Data.Sqlite;
using Maps.Extensions;
using Maps.Geographical.Features;
using Maps.Geographical.Places;
using Maps.IO;

namespace Maps.Data
{
    /// <summary>
    /// The default data provider, using Sqlite for offline storage
    /// </summary>
    public class MapsProvider : IProvider
    {
        /// <inheritdoc />
        public bool PlacesSupported
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapsProvider));
                }

                return false;
            }
        }

        /// <inheritdoc />
        public bool FeaturesSupported
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapsProvider));
                }

                return true;
            }
        }

        /// <inheritdoc />
        public IPlaceProvider PlaceProvider
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapsProvider));
                }

                throw new NotSupportedException();
            }
        }

        /// <inheritdoc />
        public IFeatureProvider FeatureProvider
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapsProvider));
                }

                return _featureProvider;
            }
        }

        private readonly IFeatureProvider _featureProvider;
        private bool _disposed;

        /// <summary>
        /// Initializes new MapsProvider instance
        /// </summary>
        public MapsProvider(IDbConnection<long, byte[]> features, ISideData sideData)
        {
            _featureProvider = new MapsFeatureProvider(features, sideData);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapsProvider));
            }

            _disposed = true;
        }
    }
}