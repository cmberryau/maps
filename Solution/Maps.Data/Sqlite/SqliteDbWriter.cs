using System;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;

namespace Maps.Data.Sqlite
{
    /// <summary>
    /// Writes to an sqlite db
    /// </summary>
    /// <typeparam name="TKey">The default key type</typeparam>
    /// <typeparam name="TValue">The value type</typeparam>
    public class SqliteDbWriter<TKey, TValue> : IDbWriter<TKey, TValue>
    {
        private const int DefaultMaxBatchSize = 64;

        private readonly SqliteConnection _connection;
        private readonly SqliteDbTypeParser<TKey, TValue> _parser;
        private readonly SqliteCommand _replaceCommand;
        private bool _disposed;

        private readonly TKey[] _keyCache;
        private readonly TValue[] _instanceCache;
        private int _maxCacheCount = DefaultMaxBatchSize;
        private int _cachedCount;

        /// <summary>
        /// Initializes a new instance of SqliteDbWriter
        /// </summary>
        /// <param name="connection">The sqlite connection to use</param>
        /// <param name="parser">The type parser to use</param>
        public SqliteDbWriter(SqliteConnection connection, SqliteDbTypeParser<TKey, TValue> parser)
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

            _replaceCommand = parser.BatchReplaceCommand(connection, DefaultMaxBatchSize);

            _connection = connection;
            _parser = parser;

            _keyCache = new TKey[_maxCacheCount];
            _instanceCache = new TValue[_maxCacheCount];
        }

        /// <inheritdoc />
        public void Write(TKey key, TValue instance)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SqliteDbWriter<TKey, TValue>));
            }

            // cache items
            _keyCache[_cachedCount] = key;
            _instanceCache[_cachedCount] = instance;

            if (++_cachedCount == _maxCacheCount)
            {
                // batch out
                ExecuteBatchCommandFromCache(_replaceCommand, _maxCacheCount);
                _cachedCount = 0;
            }
        }

        /// <inheritdoc />
        public void Write(IList<TKey> keys, IList<TValue> instances)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SqliteDbWriter<TKey, TValue>));
            }

            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            if (instances == null)
            {
                throw new ArgumentNullException(nameof(instances));
            }

            for (var i = 0; i < keys.Count; i++)
            {
                Write(keys[i], instances[i]);
            }
        }

        /// <inheritdoc />
        public void Flush()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SqliteDbWriter<TKey, TValue>));
            }

            if (_cachedCount > 0)
            {
                var command = _parser.BatchReplaceCommand(_connection, _cachedCount);
                ExecuteBatchCommandFromCache(command, _cachedCount);
                _cachedCount = 0;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SqliteDbWriter<TKey, TValue>));
            }

            Flush();
            _replaceCommand.Dispose();
            _connection.Dispose();
            _disposed = true;
        }

        private void ExecuteBatchCommandFromCache(SqliteCommand command, int count)
        {
            _parser.SetReplaceCommandParameters(command, _keyCache, _instanceCache, count);
            command.ExecuteNonQuery();
        }
    }
}