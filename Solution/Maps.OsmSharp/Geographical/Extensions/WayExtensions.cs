using System;
using Maps.Geographical;
using System.Collections.ObjectModel;
using OsmSharp.Osm;

namespace Maps.OsmSharp.Geographical.Extensions
{
    /// <summary>
    /// Provides extensions for OsmSharp's Way class
    /// </summary>
    internal static class WayExtensions
    {
        /// <summary>
        /// Evaluates if the way represents a segment, or part of a segment
        /// </summary>
        /// <param name="way">The way to evaluate</param>
        public static bool IsSegment(this Way way)
        {
            if (way == null)
            {
                throw new NullReferenceException(nameof(way));
            }

            var result = false;

            if (way.Tags != null)
            {
                result = way.Tags.ContainsKey("highway");
            }

            return result;
        }

        /// <summary>
        /// Evaluates if the way represents a piece of land
        /// </summary>
        /// <param name="way">The way to evaluate</param>
        public static bool IsArea(this Way way)
        {
            if (way == null)
            {
                throw new NullReferenceException(nameof(way));
            }

            var result = false;

            if (way.Tags != null)
            {
                result = way.Tags.ContainsKey("landuse") ||
                         way.Tags.ContainsKey("natural") ||
                         way.Tags.ContainsKey("leisure") ||
                         way.Tags.ContainsKey("waterway");
            }

            return result;
        }

        /// <summary>
        /// Returns the coordinates for a given way
        /// </summary>
        /// <param name="way">The way to return the coordinates for</param>
        /// <param name="nodes">The nodes map to fetch the way's nodes coordinates</param>
        public static Geodetic2d[] Coordinates(this Way way, 
            ReadOnlyDictionary<long, Node> nodes)
        {
            if (way == null)
            {
                throw new ArgumentNullException(nameof(way));
            }

            if (nodes == null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            var nodesCount = way.Nodes.Count;
            var result = new Geodetic2d[nodesCount];

            for (var i = 0; i < nodesCount; i++)
            {
                result[i] = nodes[way.Nodes[i]].Geodetic2d();
            }

            return result;
        }
    }
}