namespace Maps
{
    /// <summary>
    /// Interface for keyed types
    /// </summary>
    public interface IKeyed<out TKey>
    {
        /// <summary>
        /// The key for the object
        /// </summary>
        TKey Key
        {
            get;
        }
    }
}