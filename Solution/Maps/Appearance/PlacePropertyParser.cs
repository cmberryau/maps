using System.Collections.Generic;
using Maps.Appearance.Properties;

namespace Maps.Appearance
{
    /// <summary>
    /// Parses properties for the PlaceAppearance class
    /// </summary>
    public class PlacePropertyParser : FeaturePropertyParser
    {
        /// <inheritdoc />
        public PlacePropertyParser(IList<Property> properties) : base(properties)
        {

        }
    }
}