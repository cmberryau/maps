using System;
using System.Collections.Generic;

namespace Maps.Data.OpenStreetMap.Extensions
{
    /// <summary>
    /// Provides extensions for the IList interface
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Removes all entries in the given list that are present in the given set
        /// </summary>
        /// <param name="list">The list to remove entries from</param>
        /// <param name="set">The set of entries to remove from the list</param>
        /// <returns>A cleaned list with no entries that are in the given set</returns>
        /// <exception cref="ArgumentNullException">Thrown when any argument is null</exception>
        public static IList<T> Remove<T>(this IList<T> list, ISet<T> set)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            var count = list.Count;
            var cleanedList = new List<T>();
            for (var i = 0; i < count; ++i)
            {
                if (!set.Contains(list[i]))
                {
                    cleanedList.Add(list[i]);
                }
            }

            return cleanedList;
        }
    }
}