using System;
using Npgsql;

namespace Maps.Data.OpenStreetMap.PostgreSQL
{
    /// <summary>
    /// The root connection for a PostgreSQL Db 
    /// </summary>
    internal class PostgreSQLRootConnection : IRootOsmConnection
    {
        /// <inheritdoc />
        public IOsmGeoConnection GeosConnection => new PostgreSQLOsmGeoConnection(_connection);

        private readonly NpgsqlConnection _connection;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of PostgreSQLRootConnection
        /// </summary>
        /// <param name="connection">The postgresql connection to use</param>
        public PostgreSQLRootConnection(NpgsqlConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            _connection = connection;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(PostgreSQLRootConnection));
            }

            _connection.Dispose();
            _disposed = true;
        }
    }
}