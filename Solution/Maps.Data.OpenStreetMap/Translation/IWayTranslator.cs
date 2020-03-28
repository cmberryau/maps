using Maps.Geographical.Features;

namespace Maps.Data.OpenStreetMap.Translation
{
    /// <summary>
    /// Responsible for translation of Ways into map features
    /// </summary>
    public interface IWayTranslator : IFeatureTranslator
    {
        /// <summary>
        /// Attempts to translate a way into a map feature
        /// </summary>
        /// <param name="way">The way to translate</param>
        /// <param name="feature">The feature that will be written to</param>
        /// <returns>True on sucessful translation, false otherwise</returns>
        bool TryTranslate(Way way, out Feature feature);
    }
}