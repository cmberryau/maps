namespace Maps.Data
{
    /// <summary>
    /// Interface for db connections
    /// </summary>
    public interface IDbConnection
    {
        /// <summary>
        /// Is the db connection readonly?
        /// </summary>
        bool ReadOnly
        {
            get;
        }

        /// <summary>
        /// Clears the db connection
        /// </summary>
        void Clear();

        /// <summary>
        /// Flushes the db connection
        /// </summary>
        void Flush();
    }

    /// <summary>
    /// Interface for a known-type db connections
    /// </summary>
    /// <typeparam name="TKey">The default key type</typeparam>
    /// <typeparam name="TValue">The value type</typeparam>
    public interface IDbConnection<TKey, TValue> : IDbConnection
    {
        /// <summary>
        /// The number of entries
        /// </summary>
        long Count
        {
            get;
        }

        /// <summary>
        /// Creates a reader
        /// </summary>
        IDbReader<TKey, TValue> Reader();

        /// <summary>
        /// Creates a meta reader
        /// </summary>
        IDbReader<string, byte[]> MetaReader();

        /// <summary>
        /// Creates a writer
        /// </summary>
        IDbWriter<TKey, TValue> Writer();

        /// <summary>
        /// Creates a meta writer
        /// </summary>
        IDbWriter<string, byte[]> MetaWriter();
    }
}