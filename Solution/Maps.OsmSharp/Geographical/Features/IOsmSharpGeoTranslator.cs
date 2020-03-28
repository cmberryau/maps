using System.Collections.Generic;
using Maps.Geographical.Features;
using Maps.OsmSharp.Collections;

namespace Maps.OsmSharp.Geographical.Features
{
    /// <summary>
    /// An interface for converting OsmSharp geo objects
    /// to features
    /// </summary>
    public interface IOsmSharpGeoTranslator
    {
        /// <summary>
        /// Returns the features for the given OsmGeoCollection
        /// </summary>
        /// <param name="collection">The collection to evaluate</param>
        List<Feature> FeaturesFor(ReadOnlyOsmGeoCollection collection);
    }
}