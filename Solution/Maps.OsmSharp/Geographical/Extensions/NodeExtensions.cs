using System;
using System.Collections.Generic;
using Maps.Geographical;
using OsmSharp.Osm;

namespace Maps.OsmSharp.Geographical.Extensions
{
    /// <summary>
    /// Provides extensions for OsmSharp's Node class
    /// </summary>
    public static class NodeExtensions
    {
        /// <summary>
        /// Evaluates if the Node is a Place
        /// </summary>
        /// <param name="node">The node to evaluate</param>
        public static bool IsPlace(this Node node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var value = false;

            if (node.Tags != null)
            {
                value = node.Tags.ContainsKey("place");
            }

            return value;
        }

        /// <summary>
        /// Returns a Geodetic2d corodinate from the given OsmSharp Node
        /// </summary>
        /// <param name="node">The node to evaluate</param>
        public static Geodetic2d Geodetic2d(this Node node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return new Geodetic2d(node.Latitude.Value, node.Longitude.Value);
        }

        /// <summary>
        /// Returns a bounding box for the given list of nodes
        /// </summary>
        /// <param name="nodes">The collection of nodes to evaluate</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static GeodeticBox2d BoundingBox(this IList<Node> nodes)
        {
            if (nodes == null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            if (nodes.Count < 2)
            {
                throw new ArgumentException(nameof(nodes));
            }

            var maxLat = double.MinValue;
            var maxLon = double.MinValue;

            var minLat = double.MaxValue;
            var minLon = double.MaxValue;

            foreach (var node in nodes)
            {
                if (node.Latitude.Value > maxLat)
                {
                    maxLat = node.Latitude.Value;
                }

                if (node.Latitude.Value < minLat)
                {
                    minLat = node.Latitude.Value;
                }

                if (node.Longitude.Value > maxLon)
                {
                    maxLon = node.Longitude.Value;
                }

                if (node.Longitude.Value < minLon)
                {
                    minLon = node.Longitude.Value;
                }
            }

            return new GeodeticBox2d(new Geodetic2d(maxLat, maxLon), 
                new Geodetic2d(minLat, minLon));
        }
    }
}