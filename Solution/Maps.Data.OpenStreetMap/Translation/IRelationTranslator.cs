using System.Collections.Generic;
using Maps.Geographical.Features;

namespace Maps.Data.OpenStreetMap.Translation
{
    /// <summary>
    /// Responsible for translation of Relations into map features
    /// </summary>
    internal interface IRelationTranslator : IFeatureTranslator
    {
        /// <summary>
        /// Attempts to translate a relation into a map feature
        /// </summary>
        /// <param name="relation">The relation to translate</param>
        /// <param name="features">The features list that will be written to</param>
        /// <returns>True on sucessful translation, false otherwise</returns>
        bool TryTranslate(Relation relation, IList<Feature> features);
    }
}