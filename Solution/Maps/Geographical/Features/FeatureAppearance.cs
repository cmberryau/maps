using System;
using System.Collections.Generic;
using Maps.Appearance;
using Maps.Appearance.Properties;
using Maps.Geographical.Projection;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// Responsible for holding feature appearance information
    /// </summary>
    public abstract class FeatureAppearance
    {
        /// <summary>
        /// All unique renderable appearances
        /// </summary>
        public IList<MeshAppearance> RenderableAppearances
        {
            get;
            protected set;
        }

        /// <summary>
        /// All unique ui element appearances
        /// </summary>
        public IList<UIRenderableAppearance> UIElementAppearances
        {
            get;
            protected set;
        }

        /// <summary>
        /// The projection for this appearance
        /// </summary>
        protected readonly IProjection Projection;

        private readonly IList<Property> _properties;
        private readonly int _hashCode;

        /// <summary>
        /// Initializes a new unnamed instance of FeatureAppearance
        /// </summary>
        /// <param name="properties">The properties for the feature</param>
        /// <param name="projection">The projection for the feature</param>
        protected FeatureAppearance(IList<Property> properties, IProjection projection)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            Projection = projection;
            _properties = properties;
            _hashCode = GenerateHashCode();
        }

        /// <inheritdoc />
        public abstract override bool Equals(object obj);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return _hashCode;
        }

        private int GenerateHashCode()
        {
            var propertyCount = _properties.Count;
            var hashCode = _properties[0].GetHashCode();

            for (var i = 1; i < propertyCount; ++i)
            {
                unchecked
                {
                    hashCode = (hashCode * 397) ^ _properties[i].GetHashCode();
                }
            }

            return hashCode;
        }
    }
}