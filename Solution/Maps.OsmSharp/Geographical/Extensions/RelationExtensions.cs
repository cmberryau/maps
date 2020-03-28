using System;
using OsmSharp.Osm;

namespace Maps.OsmSharp.Geographical.Extensions
{
    /// <summary>
    /// Provides extensions for OsmSharp's Relation class
    /// </summary>
    public static class RelationExtensions
    {
        /// <summary>
        /// Evaluates if the Relation is a multipolygon relation
        /// </summary>
        /// <param name="relation">The relation to evaluate</param>
        public static bool IsMultipolygon(this Relation relation)
        {
            if (relation == null)
            {
                throw new NullReferenceException(nameof(relation));
            }

            var result = false;

            if (relation.Tags != null)
            {
                if (relation.Tags.ContainsKey("type"))
                {
                    result = relation.Tags["type"].Equals("multipolygon");
                }
            }

            return result;
        }

        /// <summary>
        /// Evaluates if the Relation represents a Waterway
        /// </summary>
        /// <param name="relation">The relation to evaluate</param>
        public static bool IsWaterway(this Relation relation)
        {
            if (relation == null)
            {
                throw new NullReferenceException(nameof(relation));
            }

            var result = false;

            if (relation.Tags != null)
            {
                result = relation.Tags.ContainsKey("waterway") ||
                         relation.Tags.ContainsKeyValue("type", "waterway") ||
                         relation.Tags.ContainsKeyValue("natural", "water");
            }

            return result;
        }
    }
}