using System;
using System.Collections.Generic;
using Maps.Collections;

namespace Maps.Extensions
{
    /// <summary>
    /// Useful extensions for ILists
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Returns a copy of the list with no duplicates
        /// </summary>
        /// <param name="list">The list to remove duplicates from</param>
        /// <returns>A list with no duplicates</returns>
        /// <exception cref="ArgumentNullException">Thrown when list is null</exception>
        public static IList<T> NoDuplicates<T>(this IList<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            var count = list.Count;
            var map = new HashSet<T>();
            var cleanedList = new List<T>();

            for (var i = 0; i < count; ++i)
            {
                if (map.Add(list[i]))
                {
                    cleanedList.Add(list[i]);
                }
            }

            return cleanedList;
        }

        /// <summary>
        /// Evaluates if the list any duplicates
        /// </summary>
        /// <param name="list">The list to evaluate</param>
        /// <returns>True if the list has any duplicate elements</returns>
        /// <exception cref="ArgumentNullException">Thrown when list is null</exception>
        public static bool HasDuplicates<T>(this IList<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            var found = false;
            var count = list.Count;
            var map = new HashSet<T>();

            for (var i = 0; !found && i < count; ++i)
            {
                if (!map.Add(list[i]))
                {
                    found = true;
                }
            }

            return found;
        }

        /// <summary>
        /// Validates that no entries of the list are null
        /// </summary>
        /// <param name="list">The list to validate</param>
        /// <typeparam name="T">A nullable class type</typeparam>
        /// <exception cref="ArgumentNullException">Thrown when list is null</exception>
        /// <exception cref="ArgumentException">Thrown when any list element is null</exception>
        public static void AssertNoNullEntries<T>(this IReadOnlyList<T> list) where T : class
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            var count = list.Count;

            for (var i = 0; i < count; ++i)
            {
                if (list[i] == null)
                {
                    throw new ArgumentException($"Contains null entry at index {i}",
                        nameof(list));
                }
            }
        }

        /// <summary>
        /// Validates that no entries of the list are null
        /// </summary>
        /// <param name="list">The list to validate</param>
        /// <typeparam name="T">A nullable class type</typeparam>
        /// <exception cref="ArgumentNullException">Thrown when list is null</exception>
        /// <exception cref="ArgumentException">Thrown when any list element is null</exception>
        public static void AssertNoNullEntries<T>(this IList<T> list) where T : class
        {
            new ReadOnlyList<T>(list).AssertNoNullEntries();
        }
    }
}