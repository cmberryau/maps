using System;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;

namespace Maps.Data.Sqlite
{
    /// <summary>
    /// Reads from a sqlite db
    /// </summary>
    /// <typeparam name="TKey">The default key type</typeparam>
    /// <typeparam name="TValue">The value type</typeparam>
    public class SqliteDbReader<TKey, TValue> : IDbReader<TKey, TValue>
    {
        private readonly SqliteConnection _connection;
        private readonly SqliteCommand _selectSingleRowCommand;
        private readonly SqliteCommand _selectSingleKeyCommand;
        private readonly SqliteDbTypeParser<TKey, TValue> _parser;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of SqliteDbReader
        /// </summary>
        /// <param name="connection">The sqlite connection to use</param>
        /// <param name="parser">The type parser to use</param>
        public SqliteDbReader(SqliteConnection connection, SqliteDbTypeParser<TKey, TValue> parser)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            if (parser == null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            _selectSingleRowCommand = parser.SelectRowCommand(connection);
            _selectSingleKeyCommand = parser.SelectKeyCommand(connection);
            _connection = connection;
            _parser = parser;
        }

        /// <inheritdoc />
        public TValue Read(TKey key)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SqliteDbWriter<TKey, TValue>));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            // set the parameters for the command
            _parser.SetSelectRowParameters(_selectSingleRowCommand, key);
            var value = default(TValue);

            using (var reader = _selectSingleRowCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    value = _parser.ParseSelectRowReaderValue(reader);
                }
            }

            return value;
        }

        /// <inheritdoc />
        public IList<TValue> Read(IList<TKey> keys)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SqliteDbWriter<TKey, TValue>));
            }

            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            var command = _parser.SelectRowCommand(_connection, keys.Count);
            _parser.SetSelectRowParameters(command, keys);

            var valuesDict = new Dictionary<TKey, TValue>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var key = _parser.ParseSelectRowReaderKey(reader);
                    var value = _parser.ParseSelectRowReaderValue(reader);
                    if (!valuesDict.ContainsKey(key))
                    {
                        valuesDict.Add(key, value);
                    }
                }
            }

            // create the return array
            var values = new TValue[keys.Count];
            for (var i = 0; i < keys.Count; ++i)
            {
                // fill in values when they were found
                if(valuesDict.TryGetValue(keys[i], out var value))
                {
                    values[i] = value;
                }
            }

            return values;
        }

        /// <inheritdoc />
        public IList<TValue> ReadRawCommand(string commandString)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SqliteDbWriter<TKey, TValue>));
            }

            var command = new SqliteCommand(commandString, _connection);

            var values = new List<TValue>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    values.Add(_parser.ParseSelectRowReaderValue(reader));
                }
            }

            return values;
        }

        /// <inheritdoc />
        public TKey Read(TValue instance)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SqliteDbWriter<TKey, TValue>));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            _parser.SetSelectKeyParameters(_selectSingleKeyCommand, instance);
            var key = default(TKey);

            using (var reader = _selectSingleKeyCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    key = _parser.ParseSelectKeyReader(reader);
                }
            }

            return key;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SqliteDbWriter<TKey, TValue>));
            }

            _selectSingleRowCommand.Dispose();
            _connection.Dispose();
            _disposed = true;
        }
    }
}