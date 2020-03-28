using System;
using System.Collections.Generic;

namespace Maps.Data.OpenStreetMap.Translation
{
    /// <summary>
    /// Base functionality for mapping tags to objects
    /// </summary>
    internal abstract class TagsMap<T> : ITagsMap<T>
    {
        private readonly IDictionary<T, Dictionary<string, List<string>>> _objectMap;
        private readonly IDictionary<string, Dictionary<string, T>> _keysMap;
        private readonly T _invalid;

        private readonly IDictionary<T, string> _wildcardObjectMap;
        private readonly IDictionary<string, T> _wildcardKeysMap;

        /// <summary>
        /// Initializes a new instance of TagsMap
        /// </summary>
        /// <param name="invalid">The invalid object</param>
        protected TagsMap(T invalid)
        {
            _objectMap = new Dictionary<T, Dictionary<string, List<string>>>();
            _keysMap = new Dictionary<string, Dictionary<string, T>>();

            _wildcardObjectMap = new Dictionary<T, string>();
            _wildcardKeysMap = new Dictionary<string, T>();

            _invalid = invalid;
        }

        /// <inheritdoc />
        public T Map(IReadOnlyDictionary<string, string> tags)
        {
            if (tags == null)
            {
                throw new ArgumentNullException(nameof(tags));
            }

            foreach (var tag in tags)
            {
                if (_wildcardKeysMap.ContainsKey(tag.Key))
                {
                    return _wildcardKeysMap[tag.Key];
                }

                if (_keysMap.ContainsKey(tag.Key))
                {
                    if (_keysMap[tag.Key].ContainsKey(tag.Value))
                    {
                        return _keysMap[tag.Key][tag.Value];
                    }
                }
            }

            return _invalid;
        }

        protected void AddTagForCategory(T obj, string key, string value)
        {
            // add the key value pair to the categories map
            if (!_objectMap.ContainsKey(obj))
            {
                _objectMap[obj] = new Dictionary<string, List<string>>();
            }

            if (!_objectMap[obj].ContainsKey(key))
            {
                _objectMap[obj][key] = new List<string>();
            }

            _objectMap[obj][key].Add(value);

            // add the key value pair to the keys map
            if (!_keysMap.ContainsKey(key))
            {
                _keysMap[key] = new Dictionary<string, T>();
            }

            _keysMap[key][value] = obj;
        }

        protected void AddTagWildcardForCategory(T obj, string key)
        {
            if (!_wildcardObjectMap.ContainsKey(obj))
            {
                _wildcardObjectMap[obj] = key;
            }

            if (!_wildcardKeysMap.ContainsKey(key))
            {
                _wildcardKeysMap[key] = obj;
            }
        }
    }
}