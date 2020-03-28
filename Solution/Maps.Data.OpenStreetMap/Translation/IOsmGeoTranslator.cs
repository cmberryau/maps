using System;
using System.Collections.Generic;
using Maps.Geographical.Features;

namespace Maps.Data.OpenStreetMap.Translation
{
    /// <summary>
    /// Functionality of translating OpenStreetMap geometries into map features
    /// </summary>
    internal interface IOsmGeoTranslator
    {
        /// <summary>
        /// The tags for node inclusion
        /// </summary>
        IList<Tuple<string, IList<string>>> NodeTags
        {
            get;
        }

        /// <summary>
        /// The tags for way inclusion
        /// </summary>
        IList<Tuple<string, IList<string>>> WayTags
        {
            get;
        }

        /// <summary>
        /// The tags for relation inclusion
        /// </summary>
        IList<Tuple<string, IList<string>>> RelationTags
        {
            get;
        }

        /// <summary>
        /// Attempts to translate a node to a map feature
        /// </summary>
        /// <param name="node">The node to evaluate</param>
        /// <param name="feature">The feature to write to</param>
        /// <returns>True if a feature was successfully evaluated</returns>
        bool TryTranslate(Node node, out Feature feature);

        /// <summary>
        /// Attempts to translate a way to a map feature
        /// </summary>
        /// <param name="way">The way to evaluate</param>
        /// <param name="feature">The feature to write to</param>
        /// <returns>True if a feature was successfully evaluated</returns>
        bool TryTranslate(Way way, out Feature feature);

        /// <summary>
        /// Attempts to translate a relation to a map feature
        /// </summary>
        /// <param name="relation">The relation to evaluate</param>
        /// <param name="features">The features list that will be written to</param>
        /// <returns>True if a feature was successfully evaluated</returns>
        bool TryTranslate(Relation relation, IList<Feature> features);
    }
}