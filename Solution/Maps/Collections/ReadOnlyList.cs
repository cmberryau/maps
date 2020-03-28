using System;
using System.Collections;
using System.Collections.Generic;

namespace Maps.Collections
{
    /// <summary>
    /// Wraps a normal IList up to become read-only
    /// </summary>
    public class ReadOnlyList<T> : IReadOnlyList<T>
    {
        /// <inheritdoc />
        public T this[int index] => _list[index];

        /// <inheritdoc />
        public int Count => _list.Count;

        private readonly IList<T> _list;

        /// <summary>
        /// Initializes a new ReadOnlyList
        /// </summary>
        /// <param name="list">The list to wrap</param>
        public ReadOnlyList(IList<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            _list = list;
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < _list.Count; ++i)
            {
                yield return _list[i];
            }
        }
    }
}