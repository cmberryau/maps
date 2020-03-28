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
    public class Table<TValue> : ITable<TValue>
    {
        /// <inheritdoc />
        public Type Type => typeof(TValue);

        /// <inheritdoc />
        public long Count => _forwardCache.Count;

        private readonly IDictionary<long, TValue> _forwardCache;
        private readonly IDictionary<TValue, long> _reverseCache;

        private readonly object _writeLock;
        private long _nextWriteId;
        private bool _disposed;

        private static readonly ILog Log = LogManager.GetLogger(typeof(DbTable<TValue>));

        /// <summary>
        /// Initializes a new instance of Table
        /// </summary>
        public Table()
        {
            _forwardCache = new ConcurrentDictionary<long, TValue>();
            _reverseCache = new ConcurrentDictionary<TValue, long>();
            _writeLock = new object();

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
                _forwardCache.Add(key, instance);
                _reverseCache.Add(instance, key);
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

            // gain write access and write
            lock (_writeLock)
            {
                ++_nextWriteId;
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
            }

            return result;
        }

        /// <inheritdoc />
        public IList<TValue> ReadRawCommand(string commandString)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(DbTable<TValue>));
            }

            Flush();

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
        }
    }
}