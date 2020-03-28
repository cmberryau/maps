using System.Collections.Generic;
using log4net;
using Maps.Geographical;
using Maps.Geographical.Features;

namespace Maps.Data.OpenStreetMap.Translation
{
    /// <summary>
    /// Responsible for translating relations into areas
    /// </summary>
    internal class CompoundAreaTranslator : FeatureTranslator, IRelationTranslator
    {
        /// <summary>
        /// The default compount area translator
        /// </summary>
        public static CompoundAreaTranslator Default
        {
            get
            {
                var tags = new Dictionary<string, ISet<string>>();

                // natural tags
                var natural = tags["natural"] = new HashSet<string>();
                natural.Add("waterway");

                // waterway tags
                var waterway = tags["waterway"] = new HashSet<string>();
                waterway.Add("riverbank");

                return new CompoundAreaTranslator(tags);
            }
        }

        private const string OuterSnippet = "outer";
        private const string InnerSnippet = "inner";
        private static readonly ILog Log = LogManager.GetLogger(typeof(CompoundAreaTranslator));
        private readonly AreaCategoryMap _categoryMap;

        /// <summary>
        /// Initializes an instance of CompoundAreaTranslator
        /// </summary>
        /// <param name="tags">Tags that will result in a compound area</param>
        public CompoundAreaTranslator(IDictionary<string, ISet<string>> tags) : base(tags)
        {
            _categoryMap = new AreaCategoryMap();
        }

        /// <inheritdoc />
        public bool TryTranslate(Relation relation, IList<Feature> features)
        {
            if (features == null)
            {
                features = new List<Feature>();
            }

            // ensure it's a multipolygon relation
            if (!(relation.Tags.ContainsKey("type") && relation.Tags["type"] == "multipolygon"))
            {
                Log.Info($"Non multipolygon relation: {relation}, rejecting");
                return false;
            }

            // first check the tags
            if (!TagsMatch(relation))
            {
                Log.Info($"Tags did not match for: {relation}, rejecting");
                return false;
            }

            // determine the root category
            var rootCategory = _categoryMap.Map(relation.Tags);
            if (rootCategory == RootAreaCategory.Invalid)
            {
                Log.Info($"Root category is invalid for: {relation}, rejecting");
                return false;
            }

            // create the complete category
            var category = new AreaCategory(rootCategory);

            var memberCount = relation.Members.Count;
            var closedInners = new List<GeodeticPolygon2d>();
            var closedOuters = new List<GeodeticPolygon2d>();
            var openInners = new List<GeodeticLineStrip2d>();
            var openOuters = new List<GeodeticLineStrip2d>();

            // resolve all members first
            for (var i = 0; i < memberCount; ++i)
            {
                var way = relation.Members[i] as Way;

                if (way != null)
                {
                    var coordinateCount = way.Coordinates.Count;
                    var closed = way.Coordinates[0] == way.Coordinates[coordinateCount - 1];

                    // not closed
                    if (!closed)
                    {
                        if (relation.MemberRoles[i] == InnerSnippet)
                        {
                            openInners.Add(new GeodeticLineStrip2d(way.Coordinates));
                        }
                        else if (relation.MemberRoles[i] == OuterSnippet)
                        {
                            openOuters.Add(new GeodeticLineStrip2d(way.Coordinates));
                        }
                        else
                        {
                            Log.Info($"Non inner or outer member ({way}) supplied for: {relation}");
                        }
                    }
                    // ensure that the way has sufficient coordinates when closed
                    else if (coordinateCount > 3)
                    {
                        if (relation.MemberRoles[i] == InnerSnippet)
                        {
                            closedInners.Add(new GeodeticPolygon2d(way.Coordinates));
                        }
                        else if (relation.MemberRoles[i] == OuterSnippet)
                        {
                            closedOuters.Add(new GeodeticPolygon2d(way.Coordinates));
                        }
                        else
                        {
                            Log.Info($"Non inner or outer member ({way}) supplied for: {relation}");
                        }
                    }
                    else
                    {
                        Log.Info($"Geometric failure (not enough coordinates: {coordinateCount}) for: {way} supplied for: {relation}, rejecting");
                    }
                }
            }

            // combine the open outer members
            var closeFailure = false;
            var connectedOuters = GeodeticLineStrip2d.Combine(openOuters);
            for (var i = 0; i < connectedOuters.Count; ++i)
            {
                if (connectedOuters[i].Closed)
                {
                    closedOuters.Add(new GeodeticPolygon2d(connectedOuters[i]));
                }
                else
                {
                    closeFailure = true;
                }
            }

            // on closure failure, log
            if (closeFailure)
            {
                Log.Info($"Could not close outer members supplied for: {relation}");
            }

            // combine the open inner members
            closeFailure = false;
            var connectedInners = GeodeticLineStrip2d.Combine(openInners);
            for (var i = 0; i < connectedInners.Count; ++i)
            {
                if (connectedInners[i].Closed)
                {
                    closedInners.Add(new GeodeticPolygon2d(connectedInners[i]));
                }
                else
                {
                    closeFailure = true;
                }
            }

            // on closure failure, log
            if (closeFailure)
            {
                Log.Info($"Could not close inner members supplied for: {relation}");
            }

            // handle the closed members
            var closedInnersCount = closedInners.Count;
            var closedOutersCount = closedOuters.Count;
            for (var i = 0; i < closedOutersCount; ++i)
            {
                // initially a simple geodetic polygon
                var poly = closedOuters[i];

                // any inners previously found, could be a complex geodetic polygon
                if (closedInnersCount > 0)
                {
                    // run through inners
                    var holes = new List<GeodeticPolygon2d>();
                    for (var j = 0; j < closedInnersCount; ++j)
                    {
                        var inner = closedInners[j];

                        // if inner intersects outer, it's added as a hole
                        if (poly.Polygon.Intersects(inner.Polygon))
                        {
                            holes.Add(inner);
                        }
                    }

                    // going to be a complex geodetic polygon if true
                    if (holes.Count > 0)
                    {
                        // create new poly with all holes
                        var finalPoly = new GeodeticPolygon2d(poly, holes);
                        poly = finalPoly;
                    }
                }

                // create the final feature and add to the list
                features.Add(new Area(relation.Guid, relation.Name, poly, category, poly.Area));
            }

            return true;
        }
    }
}