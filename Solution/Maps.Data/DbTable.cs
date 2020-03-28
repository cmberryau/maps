using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using log4net;
using Maps.IO;

namespace Maps.Data
{
    /// <summary>
    /// Stores values in a table, with an in-memory cache
    /// </summary>
    public class DbTable<TValue> : ITable<TValue>
    {
        /// <inheritdoc />
        public Type Type => typeof(TValue);

        /// <inheritdoc />
        public long Count => _connection.Count;

        private readonly IDbConnection<long, TValue> _connection;

        private readonly IDbWriter<long, TValue> _writer;
        private readonly IDbReader<long, TValue> _reader;

        private readonly IDictionary<long, TValue> _forwardCache;
        private readonly IDictionary<TValue, long> _reverseCache;

        private readonly object _writeLock;
        private long _nextWriteId;
        private bool _disposed;

        private static readonly ILog Log = LogManager.GetLogger(typeof(DbTable<TValue>));

        /// <summary>
        /// Initializes a new instance of Table
        /// </summary>
        /// <param name="connection">The indexed data connection to use</param>
        public DbTable(IDbConnection<long, TValue> connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            _forwardCache = new ConcurrentDictionary<long, TValue>();
            _reverseCache = new ConcurrentDictionary<TValue, long>();
            _writeLock = new object();

            _connection = connection;

            _writer = connection.Writer();
            _reader = connection.Reader();

            _nextWriteId = 0;
        }

        /// <inheritdoc />
        public void Add(TValue instance, long key)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(DbTable<TValue>));
            }

            // check the forward cache
            if (_forwardCache.TryGetValue(key, out var cachedInstance))
            {
                // ensure the cached one is the same as the one we have been provided
                if (!cachedInstance.Equals(instance))
                {
                    throw new InvalidOperationException("Mismatch between provided instance and cached instance");
                }

                // ensure the reverse cache contains the instance
                if (!_reverseCache.TryGetValue(instance, out var cachedKey))
                {
                    _reverseCache.Add(instance, key);
                }
                else
                {
                    // check for a key mismatch
                    if (cachedKey != key)
                    {
                        Log.Warn("Mismatch between cached key in reverse reader");
                    }
                }
            }
            else
            {
                // check if the instance exists on the medium at the key
                var readInstance = _reader.Read(key);

                // theres no instance on the medium
                if (readInstance == null)
                {
                    // gain write access and write
                    lock (_writeLock)
                    {
                        _writer.Write(key, instance);
                    }
                }
                else // there is an instance on the medium
                {
                    // check for instance mismatch
                    if (!readInstance.Equals(instance))
                    {
                        throw new InvalidOperationException("Mismatch between provided instance and read instance");
                    }

                    _forwardCache.Add(key, instance);
                    _reverseCache.Add(instance, key);
                }
            }
        }

        /// <inheritdoc />
        public long Add(TValue instance)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(DbTable<TValue>));
            }

            if (instance == null)
            {
                return 0;
            }

            // check the reverse cache first
            if (_reverseCache.TryGetValue(instance, out var id))
            {
                return id;
            }

            // check the reader
            id = _reader.Read(instance);

            // if we didn't find it anywhere, write it
            if (id == 0)
            {
                // gain write access and write
                lock (_writeLock)
                {
                    _writer.Write(++_nextWriteId, instance);

                    // add it to the caches
                    if (!_reverseCache.ContainsKey(instance))
                    {
                        _reverseCache.Add(instance, _nextWriteId);
                    }
                    else
                    {
                        Log.Warn($"Potential double entry in Table for: value {instance}, key: {_nextWriteId}");
                    }

                    if (!_forwardCache.ContainsKey(_nextWriteId))
                    {
                        _forwardCache.Add(_nextWriteId, instance);
                    }
                    else
                    {
                        Log.Warn($"Potential double entry in Table for: value {instance}, key: {_nextWriteId}");
                    }
                }

                id = _nextWriteId;
            }

            return id;
        }

        /// <inheritdoc />
        public bool TryGet(long key, out TValue instance)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(DbTable<TValue>));
            }

            // key 0 is always 'empty'
            if (key == 0)
            {
                instance = default(TValue);
                return false;
            }

            var result = false;
            // check the forward cache first
            if (_forwardCache.TryGetValue(key, out instance))
            {
                result = true;
            }
            else
            {
                // check the reader
                instance = _reader.Read(key);

                // if the reader had it, add it to the caches
                if (instance != null)
                {
                    lock (_writeLock)
                    {
                        if (!_forwardCache.ContainsKey(key))
                        {
                            _forwardCache.Add(key, instance);
                        }
                        else
                        {
                            Log.Info($"Potential cache miss for in Table for: value {instance}, key: {key}");
                        }

                        if (!_reverseCache.ContainsKey(instance))
                        {
                            _reverseCache.Add(instance, key);
                        }
                        else
                        {
                            Log.Info($"Potential cache miss for in Table for: value {instance}, key: {key}");
                        }
                    }

                    result = true;
                }
            }

            return result;
        }

        /// <inheritdoc />
        public IList<TValue> ReadRawCommand(string commandString)
        {
            return _reader.ReadRawCommand(commandString);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(DbTable<TValue>));
            }

            Flush();

            lock (_writeLock)
            {
                _writer.Dispose();
            }

            _reader.Dispose();

            _disposed = true;
        }

        /// <summary>
        /// Flushes the string table
        /// </summary>
        public void Flush()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(DbTable<TValue>));
            }

            if (_writer != null)
            {
                lock (_writeLock)
                {
                    _writer.Flush();
                }
            }
        }
    }
}