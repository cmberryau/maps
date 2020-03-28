using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OsmSharp.Osm;

namespace Maps.OsmSharp.Collections
{
    /// <summary>
    /// A readonly collection of OsmSharp geo objects
    /// </summary>
    public class ReadOnlyOsmGeoCollection
    {
        /// <summary>
        /// The dictionary of nodes contained by this OsmGeoCollection
        /// </summary>
        public readonly ReadOnlyDictionary<long, Node> Nodes;

        /// <summary>
        /// The dictionary of ways contained by this OsmGeoCollection
        /// </summary>
        public readonly ReadOnlyDictionary<long, Way> Ways;

        /// <summary>
        /// The dictionary of relations contained by this OsmGeoCollection
        /// </summary>
        public readonly ReadOnlyDictionary<long, Relation> Relations;

        /// <summary>
        /// Initializes a new instance of OsmGeoCollection
        /// </summary>
        /// <param name="nodes">The nodes dictionary to fill the collection with</param>
        /// <param name="ways">The ways dictionary to fill the collection with</param>
        /// <param name="relations">The relations dictionary to fill the collection with</param>
        public ReadOnlyOsmGeoCollection(Dictionary<long, Node> nodes,
            Dictionary<long, Way> ways,
            Dictionary<long, Relation> relations)
        {
            if (nodes == null)
            {
                throw new ArgumentException(nameof(nodes));
            }

            if (ways == null)
            {
                throw new ArgumentException(nameof(ways));
            }

            if (relations == null)
            {
                throw new ArgumentException(nameof(relations));
            }

            // copy the parameter dictionaries into the collection
            Nodes = new ReadOnlyDictionary<long, Node>(nodes);
            Ways = new ReadOnlyDictionary<long, Way>(ways);
            Relations = new ReadOnlyDictionary<long, Relation>(relations);
        }
    }
}