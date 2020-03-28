using System;
using System.Collections.Generic;
using Maps.Appearance.Properties;

namespace Maps.Appearance
{
    /// <summary>
    /// Holds information about how a dynamic feature should appear
    /// </summary>
    public class DynamicFeatureAppearance
    {
        /// <inheritdoc />
        public IList<Property> Properties
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of DynamicFeatureAppearance
        /// </summary>
        /// <param name="properties">The properties of the appearance</param>
        public DynamicFeatureAppearance(IList<Property> properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            Properties = properties;
        }
    }
}