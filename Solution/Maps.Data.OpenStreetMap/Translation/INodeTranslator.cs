using Maps.Geographical.Features;

namespace Maps.Data.OpenStreetMap.Translation
{
    /// <summary>
    /// Responsible for translation of Nodes into map features
    /// </summary>
    internal interface INodeTranslator : IFeatureTranslator
    {
        /// <summary>
        /// Attempts to translate a node into a map feature
        /// </summary>
        /// <param name="node">The node to translate</param>
        /// <param name="feature">The feature that will be written to</param>
        /// <returns>True on sucessful translation, false otherwise</returns>
        bool TryTranslate(Node node, out Feature feature);
    }
}