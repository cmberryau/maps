using System;
using Maps.Geographical.Features;

namespace Maps.Data.OpenStreetMap.Geographical.Features
{
    /// <summary>
    /// Feature provider for OpenStreetMap
    /// </summary>
    public class OpenStreetMapFeatureProvider : IFeatureProvider
    {
        /// <inheritdoc />
        public OfflineSupportLevel OfflineSupport
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(OpenStreetMapFeatureProvider));
                }

                return OfflineSupportLevel.None;
            }
        }

        /// <inheritdoc />
        public bool ReadOnly
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(OpenStreetMapFeatureProvider));
                }

                return _geoConnection.ReadOnly;
            }
        }

        /// <inheritdoc />
        public bool Tiled
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(OpenStreetMapFeatureProvider));
                }

                return false;
            }
        }

        private readonly IOsmGeoConnection _geoConnection;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of OpenStreetMapFeatureProvider
        /// </summary>
        /// <param name="geoConnection">The feature connection</param>
        internal OpenStreetMapFeatureProvider(IOsmGeoConnection geoConnection)
        {
            if (geoConnection == null)
            {
                throw new ArgumentNullException(nameof(geoConnection));
            }

            _geoConnection = geoConnection;
        }

        /// <inheritdoc />
        public IFeatureSource FeatureSource()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OpenStreetMapFeatureProvider));
            }

            return new OpenStreetMapFeatureSource(_geoConnection.GeoSource);
        }

        /// <inheritdoc />
        public ITiledFeatureSource TiledFeatureSource()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OpenStreetMapFeatureProvider));
            }

            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public IFeatureTarget FeatureTarget()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OpenStreetMapFeatureProvider));
            }

            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public ITiledFeatureTarget TiledFeatureTarget()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OpenStreetMapFeatureProvider));
            }

            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OpenStreetMapFeatureProvider));
            }

            if (_geoConnection != null)
            {
                _geoConnection.Dispose();
            }

            _disposed = true;
        }
    }
}