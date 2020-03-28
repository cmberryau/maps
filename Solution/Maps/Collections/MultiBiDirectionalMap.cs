using System;
using System.Collections.Generic;

namespace Maps.Collections
{
    /// <summary>
    /// A map that allows multi bidirectional referencing
    /// </summary>
    public sealed class MultiBiDirectionalMap<TSingleKey, TManyValue>
    {
        private readonly Dictionary<TSingleKey, IList<TManyValue>> _forwardDictionary;
        private readonly Dictionary<TManyValue, TSingleKey> _reverseDictionary;

        /// <summary>
        /// Initializes a new instance of MultiBiDirectionalMap
        /// </summary>
        public MultiBiDirectionalMap()
        {
            _forwardDictionary = new Dictionary<TSingleKey, IList<TManyValue>>();
            _reverseDictionary = new Dictionary<TManyValue, TSingleKey>();
        }

        /// <summary>
        /// Adds a key and single value to the MultiBiDirectionalMap
        /// </summary>
        /// <param name="key">The key to add</param>
        /// <param name="value">The value to add</param>
        public void Add(TSingleKey key, TManyValue value)
        {
            if (!_forwardDictionary.ContainsKey(key))
            {
                _forwardDictionary[key] = new List<TManyValue>();
            }

            _forwardDictionary[key].Add(value);
            _reverseDictionary[value] = key;
        }

        /// <summary>
        /// Adds a key and multiple value to the MultiBiDirectionalMap
        /// </summary>
        /// <param name="key">The key to add</param>
        /// <param name="values">The values to add</param>
        public void Add(TSingleKey key, List<TManyValue> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (!_forwardDictionary.ContainsKey(key))
            {
                _forwardDictionary[key] = new List<TManyValue>();
            }

            foreach (var value in values)
            {
                _forwardDictionary[key].Add(value);
                _reverseDictionary[value] = key;
            }
        }

        /// <summary>
        /// Returns a List of values for a given key
        /// </summary>
        public IList<TManyValue> this[TSingleKey key] => _forwardDictionary[key];

        /// <summary>
        /// Returns a key for a value
        /// </summary>
        public TSingleKey this[TManyValue valueKey] => _reverseDictionary[valueKey];
    }
}