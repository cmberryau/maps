using System;
using System.Collections.Generic;

namespace Maps.Data
{
    /// <summary>
    /// Interface for db writers
    /// </summary>
    /// <typeparam name="TKey">The default key type</typeparam>
    /// <typeparam name="TValue">The value type</typeparam>
    public interface IDbWriter<TKey, TValue> : IDisposable
    {
        /// <summary>
        /// Writes an instance to the db
        /// </summary>
        /// <param name="key">The key to write</param>
        /// <param name="instance">The instance to write</param>
        void Write(TKey key, TValue instance);

        /// <summary>
        /// Writes instances to the db
        /// </summary>
        /// <param name="keys">The keys to write</param>
        /// <param name="instances">The instances to write</param>
        void Write(IList<TKey> keys, IList<TValue> instances);

        /// <summary>
        /// Flushes pending write operations to the db
        /// </summary>
        void Flush();
    }
}