using System.Collections.Generic;

namespace Maps.Collections
{
    /// <summary>
    /// An auto dictionary, kept in memory
    /// </summary>
    public class AutoDictionary<TValue> : IAutoDictionary<long, TValue>
    {
        /// <inheritdoc />
        public long Count => _forward.Count;

        private readonly IDictionary<long, TValue> _forward;
        private readonly IDictionary<TValue, long> _reverse;
        private int _nextKey = -1;

        /// <summary>
        /// Initializes a new instance of AutoDictionary
        /// </summary>
        public AutoDictionary()
        {
            _forward = new Dictionary<long, TValue>();
            _reverse = new Dictionary<TValue, long>();
        }

        /// <inheritdoc />
        public long Add(TValue value)
        {
            if (_reverse.ContainsKey(value))
            {
                return _reverse[value];
            }

            _forward.Add(++_nextKey, value);
            _reverse.Add(value, _nextKey);

            return _nextKey;
        }

        /// <inheritdoc />
        public bool TryGetValue(long key, out TValue value)
        {
            return _forward.TryGetValue(key, out value);
        }
    }
}