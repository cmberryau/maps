using System;
using System.Collections.Generic;
using Maps.Extensions;
using Maps.Geographical.Features;

namespace Maps.Data.OpenStreetMap.Translation
{
    /// <summary>
    /// Performs translation of OpenStreetMap geometries into map features
    /// </summary>
    internal class OsmGeoTranslator : IOsmGeoTranslator
    {
        /// <inheritdoc />
        public IList<Tuple<string, IList<string>>> NodeTags
        {
            get;
        }

        /// <inheritdoc />
        public IList<Tuple<string, IList<string>>> WayTags
        {
            get;
        }

        /// <inheritdoc />
        public IList<Tuple<string, IList<string>>> RelationTags
        {
            get;
        }

        /// <summary>
        /// The default OsmGeo translator
        /// </summary>
        public static IOsmGeoTranslator Default
        {
            get
            {
                var nodeTranslators = new INodeTranslator[]
                {
                    PlaceTranslator.Default,
                };

                var wayTranslators = new IWayTranslator[]
                {
                    SegmentTranslator.Default,
                    AreaTranslator.Default
                };

                var relationTranslators = new IRelationTranslator[]
                {
                    CompoundAreaTranslator.Default
                };

                return new OsmGeoTranslator(nodeTranslators, wayTranslators, 
                    relationTranslators);
            }
        }

        private readonly IList<INodeTranslator> _nodeTranslators;
        private readonly IList<IWayTranslator> _wayTranslators;
        private readonly IList<IRelationTranslator> _relationTranslators;

        private readonly int _nodeTranslatorsCount;
        private readonly int _wayTranslatorsCount;
        private readonly int _relationTranslatorsCount;

        /// <summary>
        /// Initializes a new instance of OsmGeoTranslator
        /// </summary>
        /// <param name="nodeTranslators">The node translators</param>
        /// <param name="wayTranslators">The way translators</param>
        /// <param name="relationTranslators">The relation translators</param>
        public OsmGeoTranslator(IList<INodeTranslator> nodeTranslators, 
            IList<IWayTranslator> wayTranslators, 
            IList<IRelationTranslator> relationTranslators)
        {
            if (nodeTranslators == null)
            {
                throw new ArgumentNullException(nameof(nodeTranslators));
            }

            if (wayTranslators == null)
            {
                throw new ArgumentNullException(nameof(wayTranslators));
            }

            if (relationTranslators == null)
            {
                throw new ArgumentNullException(nameof(relationTranslators));
            }

            nodeTranslators.AssertNoNullEntries();
            wayTranslators.AssertNoNullEntries();
            relationTranslators.AssertNoNullEntries();

            _nodeTranslators = nodeTranslators;
            _wayTranslators = wayTranslators;
            _relationTranslators = relationTranslators;

            _nodeTranslatorsCount = nodeTranslators.Count;
            _wayTranslatorsCount = wayTranslators.Count;
            _relationTranslatorsCount = relationTranslators.Count;

            NodeTags = new List<Tuple<string, IList<string>>>();
            WayTags = new List<Tuple<string, IList<string>>>();
            RelationTags = new List<Tuple<string, IList<string>>>();

            // populate the inclusion tags lists
            for (var i = 0; i < _nodeTranslatorsCount; ++i)
            {
                var translator = _nodeTranslators[i];
                for (var j = 0; j < translator.TagsCount; ++j)
                {
                    var key = translator.Tags[j].Item1;

                    NodeTags.Add(new Tuple<string, IList<string>>(key, new List<string>()));

                    foreach (var value in translator.Tags[j].Item2)
                    {
                        NodeTags[NodeTags.Count - 1].Item2.Add(value);
                    }
                }
            }

            for (var i = 0; i < _wayTranslatorsCount; ++i)
            {
                var translator = _wayTranslators[i];
                for (var j = 0; j < translator.TagsCount; ++j)
                {
                    var key = translator.Tags[j].Item1;

                    WayTags.Add(new Tuple<string, IList<string>>(key, new List<string>()));

                    foreach (var value in translator.Tags[j].Item2)
                    {
                        WayTags[WayTags.Count - 1].Item2.Add(value);
                    }
                }
            }

            for (var i = 0; i < _relationTranslatorsCount; ++i)
            {
                var translator = _relationTranslators[i];
                for (var j = 0; j < translator.TagsCount; ++j)
                {
                    var key = translator.Tags[j].Item1;

                    RelationTags.Add(new Tuple<string, IList<string>>(key, new List<string>()));

                    foreach (var value in translator.Tags[j].Item2)
                    {
                        RelationTags[RelationTags.Count - 1].Item2.Add(value);
                    }
                }
            }
        }

        /// <inheritdoc />
        public bool TryTranslate(Node node, out Feature feature)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var result = false;
            feature = null;

            for (var i = 0; !result && i < _nodeTranslatorsCount; ++i)
            {
                result = _nodeTranslators[i].TryTranslate(node, out feature);
            }

            return result;
        }

        /// <inheritdoc />
        public bool TryTranslate(Way way, out Feature feature)
        {
            if (way == null)
            {
                throw new ArgumentNullException(nameof(way));
            }

            var result = false;
            feature = null;

            for (var i = 0; !result && i < _wayTranslatorsCount; ++i)
            {
                result = _wayTranslators[i].TryTranslate(way, out feature);
            }

            return result;
        }

        /// <inheritdoc />
        public bool TryTranslate(Relation relation, IList<Feature> features)
        {
            if (relation == null)
            {
                throw new ArgumentNullException(nameof(relation));
            }

            var result = false;

            for (var i = 0; !result && i < _relationTranslatorsCount; ++i)
            {
                result = _relationTranslators[i].TryTranslate(relation, features);
            }

            return result;
        }
    }
}