namespace Maps.Collections
{
    /// <summary>
    /// An interface for a dictionary that automatically creates keys for values
    /// </summary>
    /// <typeparam name="TKey">The key type</typeparam>
    /// <typeparam name="TValue">The value type</typeparam>
    public interface IAutoDictionary<TKey, TValue>
    {
        /// <summary>
        /// The number of entries
        /// </summary>
        long Count
        {
            get;
        }

        /// <summary>
        /// Adds a value to the dictionary
        /// </summary>
        /// <param name="value">The value to add</param>
        /// <returns>The key for the added value</returns>
        TKey Add(TValue value);

        /// <summary>
        /// Tries to get a value from the dictionary
        /// </summary>
        /// <param name="key">The key to search for</param>
        /// <param name="value">The value to write to</param>
        /// <returns>True if the value was found, false otherwise</returns>
        bool TryGetValue(TKey key, out TValue value);
    }
}