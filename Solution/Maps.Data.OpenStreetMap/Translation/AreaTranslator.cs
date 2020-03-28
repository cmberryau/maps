using System;
using System.Collections.Generic;
using log4net;
using Maps.Geographical;
using Maps.Geographical.Features;

namespace Maps.Data.OpenStreetMap.Translation
{
    /// <summary>
    /// Responsible for translating ways into areas
    /// </summary>
    internal class AreaTranslator : FeatureTranslator, IWayTranslator
    {
        /// <summary>
        /// The default area translator
        /// </summary>
        public static AreaTranslator Default
        {
            get
            {
                var tags = new Dictionary<string, ISet<string>>();

                // natural tags
                var natural = tags["natural"] = new HashSet<string>();
                
                // vegetation or surface related
                natural.Add("wood");
                natural.Add("scrub");
                natural.Add("heath");
                natural.Add("grassland");

                // water related
                natural.Add("water");
                natural.Add("wetland");
                natural.Add("glacier");
                natural.Add("bay");
                natural.Add("beach");
                natural.Add("hot_spring");

                // leisure tags
                var leisure = tags["leisure"] = new HashSet<string>();
                leisure.Add("common");
                leisure.Add("dog_park");
                leisure.Add("garden");
                leisure.Add("golf_course");
                leisure.Add("nature_reserve");
                leisure.Add("park");
                leisure.Add("pitch");
                leisure.Add("playground");
                leisure.Add("wildlife_hide");

                // landuse tags
                var landuse = tags["landuse"] = new HashSet<string>();
                landuse.Add("residential");
                landuse.Add("commercial");
                landuse.Add("retail");
                landuse.Add("industrial");
                landuse.Add("forest");
                landuse.Add("farmland");
                landuse.Add("grass");
                landuse.Add("landfill");
                landuse.Add("meadow");
                landuse.Add("military");
                landuse.Add("orchard");
                landuse.Add("port");
                landuse.Add("quarry");
                landuse.Add("reservoir");

                // building tags
                var building = tags["building"] = new HashSet<string>();
                building.Add("");

                return new AreaTranslator(tags);
            }
        }

        private static readonly ILog Log = LogManager.GetLogger(typeof(AreaTranslator));
        private readonly AreaCategoryMap _categoryMap;

        /// <summary>
        /// Initializes an instance of AreaTranslator
        /// </summary>
        /// <param name="tags">Tags that will result in an area</param>
        public AreaTranslator(IDictionary<string, ISet<string>> tags) : base(tags)
        {
            _categoryMap = new AreaCategoryMap();
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

                // not closed at all, even untypically closed
                if (!closed && !(way.Coordinates[0] == way.Coordinates[coordinateCount - 2]))
                {
                    Log.Info($"Geometric failure (not closed) for: {way}, rejecting");
                } // ensure that the way has sufficient coordinates
                else if (closed && coordinateCount > 3 || !closed && coordinateCount > 2)
                {
                    var rootCategory = _categoryMap.Map(way.Tags);
                    if (rootCategory != RootAreaCategory.Invalid)
                    {
                        var category = new AreaCategory(rootCategory);
                        var polygon = new GeodeticPolygon2d(way.Coordinates);
                        var area = new Area(way.Guid, way.Name, polygon, category, polygon.Area);
                        feature = area;
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