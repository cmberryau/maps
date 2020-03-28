using System;
using System.Data;
using Maps.Geographical;
using Npgsql;

namespace Maps.Data.OpenStreetMap.PostgreSQL
{
    /// <summary>
    /// Commonly required functionality for postgresql
    /// </summary>
    public class PostgreSQLClient : IDisposable, ICloneable
    {
        /// <summary>
        /// The actual connection
        /// </summary>
        public readonly NpgsqlConnection Connection;

        /// <summary>
        /// Is the client disposed?
        /// </summary>
        public bool Disposed
        {
            get;
            private set;
        }

        private const string EnvelopeValues = @"{0},{1},{2},{3}";

        /// <summary>
        /// Initializes a new instance of PostgreSQLClient
        /// </summary>
        /// <param name="connection">The connection to use</param>
        public PostgreSQLClient(NpgsqlConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            if (connection.FullState != ConnectionState.Open)
            {
                connection.Open();
            }

            if (connection.FullState != ConnectionState.Open)
            {
                throw new ArgumentException("Could not connect to db", nameof(connection));
            }

            Connection = connection;
        }

        /// <summary>
        /// Returns a PostGIS envelope string for a given box
        /// </summary>
        /// <param name="box">The box to create an envelope string for</param>
        /// <returns>A string for the given box</returns>
        public static string EnvelopeString(GeodeticBox2d box)
        {
            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            return string.Format(EnvelopeValues, box.MinimumLongitude,
                box.MinimumLatitude, box.MaximumLongitude, box.MaximumLatitude);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (Connection != null)
            {
                Connection.Close();
            }

            Disposed = true;
        }

        /// <inheritdoc />
        public object Clone()
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(nameof(PostgreSQLClient));
            }

            var cloneableConnection = Connection as ICloneable;

            if (cloneableConnection == null)
            {
                throw new InvalidOperationException($"{nameof(NpgsqlConnection)} not castable to {nameof(ICloneable)}");
            }

            var clonedConnection = cloneableConnection.Clone();

            if (clonedConnection == null)
            {
                throw new InvalidOperationException($"{nameof(Connection)} failed to clone");
            }

            var castedCloneConnection = clonedConnection as NpgsqlConnection;

            if (castedCloneConnection == null)
            {
                throw new InvalidOperationException($"Cloned {nameof(NpgsqlConnection)} not castable to {nameof(NpgsqlConnection)}");
            }

            return new PostgreSQLClient(castedCloneConnection);
        }
    }
}