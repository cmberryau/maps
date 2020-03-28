using System;
using System.Collections.Generic;
using System.Text;

namespace Maps.Extensions
{
    /// <summary>
    /// Provides useful extensions for ICollection implementers
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Returns a string of all items in the collection
        /// </summary>
        /// <param name="collection">The collection to evaluate</param>
        public static string ItemsToStrings<T>(this ICollection<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection.Count <= 0)
            {
                return "";
            }

            var itemSb = new StringBuilder();
            var enumerator = collection.GetEnumerator();
            enumerator.MoveNext();

            if (collection.Count == 1)
            {
                if (enumerator.Current != null)
                {
                    return enumerator.Current.ToString();
                }

                return "null";
            }

            itemSb.Append("{ ");

            for (var i = 0; i < collection.Count - 1; ++i)
            {
                var current = enumerator.Current;

                if (current != null)
                {
                    itemSb.Append(enumerator.Current + ", ");
                }
                else
                {
                    itemSb.Append("null, ");
                }

                if (!enumerator.MoveNext())
                {
                    break;
                }
            }

            if (enumerator.MoveNext())
            {
                if (enumerator.Current != null)
                {
                    itemSb.Append(enumerator.Current + " }");
                }
                else
                {
                    itemSb.Append("null }");
                }
            }

            return itemSb.ToString();
        }
    }
}