using System;
using System.Collections.Generic;
using Maps.Geographical.Features;
using Maps.OsmSharp.Collections;
using OsmSharp.Osm;

namespace Maps.OsmSharp.Geographical.Features
{
    /// <summary>
    /// A abstract implementation of a translator from OsmSharp geos
    /// that provides minimum functionality for validation
    /// </summary>
    public abstract class OsmGeoTranslator : IOsmSharpGeoTranslator
    {
        /// <summary>
        /// Returns the features for the given OsmGeoCollection
        /// </summary>
        /// <param name="collection">The collection to evaluate</param>
        public List<Feature> FeaturesFor(ReadOnlyOsmGeoCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection.Nodes == null)
            {
                throw new ArgumentNullException(nameof(collection.Nodes));
            }

            if (collection.Ways == null)
            {
                throw new ArgumentNullException(nameof(collection.Ways));
            }

            if (collection.Relations == null)
            {
                throw new ArgumentNullException(nameof(collection.Relations));
            }

            var result = new List<Feature>();
            Feature feature;

            // iterate through relations
            foreach (var relation in collection.Relations)
            {
                if (FeatureFor(relation.Value, collection, out feature))
                {
                    result.Add(feature);
                }
            }

            // iterate through ways
            foreach (var way in collection.Ways)
            {
                if (FeatureFor(way.Value, collection, out feature))
                {
                    result.Add(feature);
                }
            }

            // iterate through nodes
            foreach (var node in collection.Nodes)
            {
                if (FeatureFor(node.Value, out feature))
                {
                    result.Add(feature);
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the feature for the given node
        /// </summary>
        /// <param name="node">The node to evaluate</param>
        /// <param name="feature">The feature to write to</param>
        protected abstract bool FeatureFor(Node node, out Feature feature);

        /// <summary>
        /// Returns the feature for the given way
        /// </summary>
        /// <param name="way">The way to evaluate</param>
        /// <param name="collection">The OsmGeo collection to read for dependencies</param>
        /// <param name="feature">The feature to write to</param>
        protected abstract bool FeatureFor(Way way, ReadOnlyOsmGeoCollection collection,
            out Feature feature);

        /// <summary>
        /// Returns the feature for the given relation
        /// </summary>
        /// <param name="relation">The relation to evaluate</param>
        /// <param name="collection">The OsmGeo collection to read for dependencies</param>
        /// <param name="feature">The feature to write to</param>
        protected abstract bool FeatureFor(Relation relation, 
            ReadOnlyOsmGeoCollection collection, out Feature feature);
    }
}