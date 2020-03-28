using System;
using System.Collections.Generic;

namespace Maps.IO
{
    /// <summary>
    /// Interface for storing objects
    /// </summary>
    public interface ITable : IDisposable
    {
        /// <summary>
        /// The type the table stores
        /// </summary>
        Type Type
        {
            get;
        }

        /// <summary>
        /// The number of entries
        /// </summary>
        long Count
        {
            get;
        }

        /// <summary>
        /// Flushes the table to the storage medium
        /// </summary>
        void Flush();
    }

    /// <summary>
    /// Interface for storing objects of a known type
    /// </summary>
    public interface ITable<TValue> : ITable
    {
        /// <summary>
        /// Adds an instance to the table with a known key
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <param name="key">The key for the instance</param>
        void Add(TValue instance, long key);

        /// <summary>
        /// Adds an instance to the table
        /// </summary>
        /// <param name="instance">The instance</param>
        /// <returns>The key for the instance</returns>
        long Add(TValue instance);

        /// <summary>
        /// Gets an instance from the table
        /// </summary>
        /// <param name="key">The key for the instance</param>
        /// <param name="instance">The instance</param>
        /// <returns>True if an instance is found, false otherwise</returns>
        bool TryGet(long key, out TValue instance);

        /// <summary>
        /// Gets instances from the table
        /// </summary>
        /// <param name="commandString">The raw command string</param>
        /// <returns>The fetched values</returns>
        IList<TValue> ReadRawCommand(string commandString);
    }
}