using System;
using Maps.Data.OpenStreetMap.Geographical.Features;
using Maps.Data.OpenStreetMap.PostgreSQL;
using Maps.Geographical.Features;
using Maps.Geographical.Places;
using Npgsql;

namespace Maps.Data.OpenStreetMap
{
    /// <summary>
    /// A provider for OpenStreetMap data
    /// </summary>
    public class OpenStreetMapProvider : IProvider
    {
        /// <inheritdoc />
        public bool PlacesSupported
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(OpenStreetMapProvider));
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
                    throw new ObjectDisposedException(nameof(OpenStreetMapProvider));
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
                    throw new ObjectDisposedException(nameof(OpenStreetMapProvider));
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
                    throw new ObjectDisposedException(nameof(OpenStreetMapProvider));
                }

                return new OpenStreetMapFeatureProvider(
                    _rootOsmConnection.GeosConnection);
            }
        }

        private readonly IRootOsmConnection _rootOsmConnection;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of OpenStreetMapProvider
        /// </summary>
        /// <param name="connection">The connection to use</param>
        public OpenStreetMapProvider(NpgsqlConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            _rootOsmConnection = new PostgreSQLRootConnection(connection);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OpenStreetMapProvider));
            }

            if (_rootOsmConnection != null)
            {
                _rootOsmConnection.Dispose();
            }

            _disposed = true;
        }
    }
}
