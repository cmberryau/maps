using System;

namespace Maps.IO
{
    /// <summary>
    /// An interface for sidedata
    /// </summary>
    public interface ISideData : IDisposable
    {
        /// <summary>
        /// Tries to get a table
        /// </summary>
        /// <typeparam name="TValue">The table type</typeparam>
        /// <param name="table">The output table if found</param>
        /// <returns>True if the table exists, false if not</returns>
        bool TryGetTable<TValue>(out ITable<TValue> table);

        /// <summary>
        /// Flushes the target tables
        /// </summary>
        void Flush();
    }
}