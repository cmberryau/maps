using System;
using System.Collections.Generic;
using Maps.Geographical;

namespace Maps.Data.OpenStreetMap
{
    /// <summary>
    /// Way feature from OpenStreetMap
    /// </summary>
    public class Way : OsmGeo
    {
        /// <summary>
        /// The coordinates of the way
        /// </summary>
        public IList<Geodetic2d> Coordinates
        {
            get
            {
                var nodeCount = Nodes.Count;
                var coords = new Geodetic2d[nodeCount];

                for (var i = 0; i < nodeCount; ++i)
                {
                    coords[i] = Nodes[i].Coordinate;
                }

                return coords;
            }
        }

        /// <summary>
        /// The ids of the coordinates
        /// </summary>
        public IList<long> Ids
        {
            get
            {
                var nodeCount = Nodes.Count;
                var ids = new long[nodeCount];

                for (var i = 0; i < nodeCount; ++i)
                {
                    ids[i] = Nodes[i].Id;
                }

                return ids;
            }
        }

        /// <summary>
        /// The nodes that make up the way
        /// </summary>
        public readonly IReadOnlyList<Node> Nodes;

        /// <inheritdoc />
        protected override OsmGeoType Type => OsmGeoType.Way;

        /// <summary>
        /// Initializes a new instance of Way
        /// </summary>
        /// <param name="id">The id of the way</param>
        /// <param name="tags">The tags for the way</param>
        /// <param name="nodes">The nodes that make up the way</param>
        public Way(long id, IDictionary<string, string> tags, IList<Node> nodes) 
            : base(id, tags)
        {
            if (nodes == null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            var nodesCount = nodes.Count;
            if (nodesCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(nodes));
            }

            for (var i = 0; i < nodesCount; ++i)
            {
                if (nodes[i] == null)
                {
                    throw new ArgumentException($"Contains null element at {i}",
                        nameof(nodes));
                }
            }

            Nodes = new List<Node>(nodes);
        }
    }
}