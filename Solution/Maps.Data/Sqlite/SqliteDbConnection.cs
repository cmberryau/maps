using System;
using System.Data;
using System.Text;
using Mono.Data.Sqlite;

namespace Maps.Data.Sqlite
{
    /// <summary>
    /// Sqlite db connection
    /// </summary>
    public abstract class SqliteDbConnection : IDbConnection
    {
        /// <summary>
        /// Returns the sqlite version string
        /// </summary>
        public static string SqliteVersion
        {
            get
            {
                // create an empty in-memory db
                var connectionString = ConnectionString(true, false);
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    if (connection.State == ConnectionState.Open)
                    {
                        using (var command = new SqliteCommand(@"SELECT sqlite_version();", connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var fetched = reader.GetString(0);

                                    if (!String.IsNullOrEmpty(fetched))
                                    {
                                        return fetched;
                                    }
                                }
                            }
                        }
                    }
                }

                throw new InvalidOperationException();
            }
        }

        /// <inheritdoc />
        public bool ReadOnly
        {
            get;
        }

        /// <summary>
        /// The meta suffix for tables
        /// </summary>
        public const string MetaSuffixSnippet = "_meta";

        /// <summary>
        /// The uri of the sqlite db
        /// </summary>
        protected readonly string Uri;

        private const string TableParamSnippet = @"@tableName0";

        private const string DetectTableSnippet = @"SELECT COUNT(1) FROM " +
                                                      @"sqlite_master " +
                                                  @"WHERE " +
                                                      @"type = 'table' " +
                                                  @"AND " +
                                                      @"name = {0};";

        private const string DropTableSnippet = @"DROP TABLE {0};";

        private const string VacuumSnippet = @"VACUUM;";

        private readonly string _table;

        /// <summary>
        /// Initializes a new instance of SqliteDbConnection
        /// </summary>
        /// <param name="uri">The uri of the sqlite DB</param>
        /// <param name="table">The table to operate on</param>
        public SqliteDbConnection(string uri, string table)
        {
            if (String.IsNullOrEmpty(table))
            {
                throw new ArgumentNullException(nameof(table));
            }

            Uri = uri ?? String.Empty;
            _table = table;
        }

        /// <summary>
        /// Creates a sqlite connection string
        /// </summary>
        /// <param name="memory">Is the db in-memory?</param>
        /// <param name="compressed">Is the db compressed?</param>
        /// <param name="uri">The uri of the db (optional)</param>
        /// <param name="password">The password for the db (optional)</param>
        /// <returns>The complete sqlite connection string</returns>
        public static string ConnectionString(bool memory, bool compressed, string uri = null, string password = null)
        {
            var connectionStringSb = new StringBuilder();

            if (memory || string.IsNullOrEmpty(uri))
            {
                connectionStringSb.Append(@"URI=file::memory:,");
            }
            else
            {
                connectionStringSb.Append(@"Data Source=");
                connectionStringSb.Append(uri);
                connectionStringSb.Append(@";");
            }

            connectionStringSb.Append("version=3;");

            if (password != null)
            {
                connectionStringSb.Append(@"Password=");
                connectionStringSb.Append(password);
                connectionStringSb.Append(@";");
            }

            if (compressed)
            {
                connectionStringSb.Append(@"Compress=True;");
            }

            return connectionStringSb.ToString();
        }

        /// <inheritdoc />
        public virtual void Clear()
        {
            using (var connection = new SqliteConnection(ConnectionString(false, false, Uri)))
            {
                connection.Open();

                if (TableExists(connection))
                {
                    DropTable(connection);
                    Vacuum(connection);
                    CreateTable(connection);
                }
            }
        }

        /// <inheritdoc />
        public void Flush()
        {
            using (var connection = new SqliteConnection(ConnectionString(false, false, Uri)))
            {
                connection.Open();
                Vacuum(connection);
            }
        }

        /// <summary>
        /// Creates the table
        /// </summary>
        /// <param name="connection">The connection to create the table on</param>
        protected abstract void CreateTable(SqliteConnection connection);

        /// <summary>
        /// Ensures that the table exists
        /// </summary>
        protected void EnsureTables()
        {
            using (var connection = new SqliteConnection(ConnectionString(false, false, Uri)))
            {
                connection.Open();

                if (!TableExists(connection))
                {
                    CreateTable(connection);
                }
            }
        }

        /// <summary>
        /// Evaluates if the table exists
        /// </summary>
        /// <param name="connection">The connection to evaluate on</param>
        /// <returns>True if the table exists, false otherwise</returns>
        protected bool TableExists(SqliteConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            using (var command = new SqliteCommand(string.Format(DetectTableSnippet, TableParamSnippet), connection))
            {
                command.Parameters.Add(TableParamSnippet, DbType.String);
                command.Parameters[0].Value = _table;
                return (long)command.ExecuteScalar() > 0;
            }
        }

        /// <summary>
        /// Drops the table
        /// </summary>
        /// <param name="connection">The connection to drop the table from</param>
        protected void DropTable(SqliteConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            using (var command = new SqliteCommand(string.Format(DropTableSnippet, _table), connection))
            {
                // this command doesn't seem to like proper parameters
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Vacuums the db
        /// </summary>
        /// <param name="connection">The connection to vacuum</param>
        protected void Vacuum(SqliteConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            using (var command = new SqliteCommand(VacuumSnippet, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Sqlite db connection
    /// </summary>
    /// <typeparam name="TKey">The default key type</typeparam>
    /// <typeparam name="TValue">The value type</typeparam>
    public class SqliteDbConnection<TKey, TValue> : SqliteDbConnection, IDbConnection<TKey, TValue>
    {
        /// <inheritdoc />
        public long Count
        {
            get
            {
                EnsureTables();

                using (var connection = new SqliteConnection(ConnectionString(false, false, Uri)))
                {
                    connection.Open();

                    using (var command = _parser.SelectCountCommand(connection))
                    {
                        return (long) command.ExecuteScalar();
                    }
                }
            }
        }

        private readonly SqliteDbTypeParser<TKey, TValue> _parser;
        private readonly SqliteDbConnection<string, byte[]> _metaConnection;

        /// <summary>
        /// Initlaizes a new instance of SqliteDbConnection
        /// </summary>
        /// <param name="uri">The uri of the Sqlite DB</param>
        public SqliteDbConnection(string uri) : base(uri, typeof(TValue).Name.ToLower())
        {
            var valueName = typeof(TValue).Name.ToLower();
            _parser = new SqliteDbTypeParser<TKey, TValue>(valueName);
            _parser.Parse();

            _metaConnection = new SqliteDbConnection<string, byte[]>(uri, valueName + MetaSuffixSnippet, false);
        }

        /// <summary>
        /// Initlaizes a new instance of SqliteDbConnection
        /// </summary>
        /// <param name="uri">The uri of the Sqlite DB</param>
        /// <param name="table">The table to operate on</param>
        public SqliteDbConnection(string uri, string table) : base(uri, table)
        {
            _parser = new SqliteDbTypeParser<TKey, TValue>(table);
            _parser.Parse();

            _metaConnection = new SqliteDbConnection<string, byte[]>(uri, table + MetaSuffixSnippet, false);
        }

        private SqliteDbConnection(string uri, string table, bool createMeta) : base(uri, table)
        {
            _parser = new SqliteDbTypeParser<TKey, TValue>(table);
            _parser.Parse();
        }

        /// <inheritdoc />
        public IDbReader<TKey, TValue> Reader()
        {
            EnsureTables();
            return new SqliteDbReader<TKey, TValue>(new SqliteConnection(ConnectionString(false, false, Uri)), _parser);
        }

        /// <inheritdoc />
        public IDbReader<string, byte[]> MetaReader()
        {
            return _metaConnection.Reader();
        }

        /// <inheritdoc />
        public IDbWriter<TKey, TValue> Writer()
        {
            EnsureTables();
            return new SqliteDbWriter<TKey, TValue>(new SqliteConnection(ConnectionString(false, false, Uri)), _parser);
        }

        /// <inheritdoc />
        public IDbWriter<string, byte[]> MetaWriter()
        {
            return _metaConnection.Writer();
        }

        /// <inheritdoc />
        public override void Clear()
        {
            base.Clear();

            if (_metaConnection != null)
            {
                _metaConnection.Clear();
            }
        }

        /// <inheritdoc />
        protected override void CreateTable(SqliteConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            using (var command = new SqliteCommand(_parser.CreateTable, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}