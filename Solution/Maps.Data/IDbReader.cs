using System;
using System.Collections.Generic;

namespace Maps.Data
{
    /// <summary>
    /// Interface for db readers
    /// </summary>
    /// <typeparam name="TKey">The default key type</typeparam>
    /// <typeparam name="TValue">The value type</typeparam>
    public interface IDbReader<TKey, TValue> : IDisposable
    {
        /// <summary>
        /// Reads an instance from the db
        /// </summary>
        /// <param name="key">The key to read from</param>
        /// <returns>The fetched value</returns>
        TValue Read(TKey key);

        /// <summary>
        /// Reads instances from the db
        /// </summary>
        /// <param name="keys">The keys to read</param>
        /// <returns>The fetched values</returns>
        IList<TValue> Read(IList<TKey> keys);

        /// <summary>
        /// Reads instances from the db
        /// </summary>
        /// <param name="commandString">The raw command string</param>
        /// <returns>The fetched values</returns>
        IList<TValue> ReadRawCommand(string commandString);
        
        /// <summary>
        /// Reads a key from the db
        /// </summary>
        /// <param name="instance">The instance to get the key for</param>
        /// <returns>The fetched value</returns>
        TKey Read(TValue instance);
    }
}