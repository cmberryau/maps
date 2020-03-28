using System.Collections.Generic;

namespace Maps.Data.OpenStreetMap.Translation
{
    /// <summary>
    /// Provides mapping to and from tags
    /// </summary>
    /// <typeparam name="T">The type to map against</typeparam>
    internal interface ITagsMap<T>
    {
        /// <summary>
        /// Returns the mapped object for the tags
        /// </summary>
        /// <param name="tags">The tags to evaluate against</param>
        T Map(IReadOnlyDictionary<string, string> tags);
    }
}