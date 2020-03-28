using System;
using System.Collections.Generic;

namespace Maps.Data.OpenStreetMap.Translation
{
    /// <summary>
    /// Base functionality for feature translators
    /// </summary>
    internal abstract class FeatureTranslator : IFeatureTranslator
    {
        /// <inheritdoc />
        public IList<Tuple<string, ISet<string>>> Tags
        {
            get;
        }

        /// <inheritdoc />
        public int TagsCount
        {
            get;
        }

        private readonly IList<string> _wildCards;
        private readonly int _wildCardCount;

        /// <summary>
        /// Initializes a new instance of FeatureTranslator
        /// </summary>
        /// <param name="tags"></param>
        protected FeatureTranslator(IDictionary<string, ISet<string>> tags)
        {
            if (tags == null)
            {
                throw new ArgumentNullException(nameof(tags));
            }

            Tags = new List<Tuple<string, ISet<string>>>();
            _wildCards = new List<string>();

            foreach (var tag in tags)
            {
                if (tag.Key == null)
                {
                    throw new ArgumentException("Contains null key", nameof(tags));
                }

                if (tag.Value == null)
                {
                    throw new ArgumentException("Contains null value set", nameof(tags));
                }

                var wildcard = false;
                foreach (var value in tag.Value)
                {
                    if (value == null)
                    {
                        throw new ArgumentException("Contains null tag value");
                    }

                    if (value == "" || value == "*")
                    {
                        wildcard = true;
                    }
                }

                Tags.Add(new Tuple<string, ISet<string>>(tag.Key, tag.Value));

                if (wildcard)
                {
                    _wildCards.Add(tag.Key);
                }
            }

            TagsCount = Tags.Count;
            _wildCardCount = _wildCards.Count;
        }

        /// <summary>
        /// Evaluates if an OpenStreetMap geometry meets the tags requirement
        /// </summary>
        /// <param name="geo">The geometry to evaluate</param>
        /// <returns>True if geometry meets requirements, false otherwise</returns>
        protected bool TagsMatch(OsmGeo geo)
        {
            if (geo == null)
            {
                throw new ArgumentNullException(nameof(geo));
            }

            var result = false;

            for (var i = 0; i < _wildCardCount && !result; ++i)
            {
                var wildCardKey = _wildCards[i];

                if (geo.Tags.ContainsKey(wildCardKey))
                {
                    result = true;
                }
            }

            for (var i = 0; i < TagsCount && !result; ++i)
            {
                var tagKey = Tags[i].Item1;

                if (geo.Tags.ContainsKey(tagKey))
                {
                    var tagValues = Tags[i].Item2;

                    if (tagValues.Contains(geo.Tags[tagKey]))
                    {
                        result = true;
                    }
                }
            }

            return result;
        }
    }
}