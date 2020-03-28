using System;
using System.Collections.Generic;
using Maps.Geographical.Features;
using log4net;

namespace Maps.Data.OpenStreetMap.Translation
{
    /// <summary>
    /// Responsible for translating ways into segments
    /// </summary>
    internal class SegmentTranslator : FeatureTranslator, IWayTranslator
    {
        /// <summary>
        /// The default segment translator
        /// </summary>
        public static SegmentTranslator Default
        {
            get
            {
                var tags = new Dictionary<string, ISet<string>>();

                // highway tags
                var highway = tags["highway"] = new HashSet<string>();

                // standard Segment types
                highway.Add("motorway");
                highway.Add("trunk");
                highway.Add("primary");
                highway.Add("secondary");
                highway.Add("tertiary");
                highway.Add("residential");
                highway.Add("unclassified"); // not unknown, see http://wiki.openstreetmap.org/wiki/Tag:highway%3Dunclassified
                highway.Add("service");

                // link Segments
                highway.Add("motorway_link");
                highway.Add("trunk_link");
                highway.Add("primary_link");
                highway.Add("secondary_link");
                highway.Add("tertiary_link");

                // special types
                highway.Add("living_street");

                // unknown types
                highway.Add("unknown");
                highway.Add("road");

                return new SegmentTranslator(tags);
            }
        }

        private static readonly ILog Log = LogManager.GetLogger(typeof(SegmentTranslator));
        private readonly SegmentCategoryMap _categoryMap;

        /// <summary>
        /// Creates a new instance of SegmentTranslator
        /// </summary>
        /// <param name="tags">Tags that will result in a Segment</param>
        public SegmentTranslator(IDictionary<string, ISet<string>> tags) : base(tags)
        {
            _categoryMap = new SegmentCategoryMap();
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

            if (TagsMatch(way))
            {
                var coordinateCount = way.Coordinates.Count;
                var closed = way.Coordinates[0] == way.Coordinates[coordinateCount - 1];
                // ensure that the way has sufficient coordinates
                if (closed && coordinateCount > 3 || !closed && coordinateCount > 1)
                {
                    var rootCategory = _categoryMap.Map(way.Tags);

                    if (rootCategory != RootSegmentCategory.Invalid)
                    {
                        var category = new SegmentCategory(rootCategory);
                        var segment = new Segment(way.Guid, way.Name, way.Coordinates, category);
                        feature = segment;
                        result = true;
                    }
                    else
                    {
                        Log.Info($"Root category is invalid for: {way}, rejecting");
                    }
                }
                else
                {
                    Log.Info($"Geometric failure (not enough coordinates: {coordinateCount}) for: {way}, rejecting");
                }
            }

            return result;
        }
    }
}