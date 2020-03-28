using System;
using Npgsql;

namespace Maps.Data.OpenStreetMap.PostgreSQL
{
    /// <summary>
    /// The OpenStreetMap geometry connection for a PostgreSQL db
    /// </summary>
    internal class PostgreSQLOsmGeoConnection : IOsmGeoConnection
    {
        /// <inheritdoc />
        public bool ReadOnly => true;

        /// <inheritdoc />
        public bool Exists
        {
            get;
        }

        /// <inheritdoc />
        public IOsmGeoSource GeoSource => new PostgreSQLOsmGeoSource(_client);

        private readonly PostgreSQLClient _client;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of PostgreSQLOsmGeoConnection
        /// </summary>
        /// <param name="connection">The postgresql connection</param>
        public PostgreSQLOsmGeoConnection(NpgsqlConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            _client = new PostgreSQLClient(connection);
        }

        /// <inheritdoc />
        public void Connect()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(PostgreSQLOsmGeoConnection));
            }

            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void Flush()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(PostgreSQLOsmGeoConnection));
            }

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Clear()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(PostgreSQLOsmGeoConnection));
            }

            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(PostgreSQLOsmGeoConnection));
            }

            GeoSource.Dispose();
            _disposed = true;
        }
    }
}