using System;
using System.Collections.Generic;
using Maps.Appearance.Properties;
using Maps.Geometry.Tessellation;

namespace Maps.Appearance
{
    /// <summary>
    /// Parses properties for the AreaAppearance class
    /// </summary>
    public class AreaPropertyParser : FeaturePropertyParser
    {
        /// <summary>
        /// The main tessellator
        /// </summary>
        public IPolygonTessellator MainTessellator
        {
            get;
            private set;
        }

        /// <summary>
        /// The main appearance
        /// </summary>
        public MeshAppearance MainAppearance
        {
            get;
            private set;
        }

        // main info
        private int _z;
        private Colorf _mainColor;
        private double _height;

        /// <inheritdoc />
        public AreaPropertyParser(IList<Property> properties) : base(properties)
        {

        }

        /// <inheritdoc />
        public override void Visit(ColorProperty property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (property.Key.Equals("main"))
            {
                _mainColor = property.Value;
            }
        }

        /// <inheritdoc />
        public override void Visit(Int32Property property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (property.Key.Equals("z"))
            {
                _z = property.Value;
            }
        }

        /// <inheritdoc />
        public override void Visit(DoubleProperty property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (property.Key.Equals("height"))
            {
                _height = property.Value;
            }
        }

        /// <inheritdoc />
        protected override void OnParseComplete()
        {
            base.OnParseComplete();

            MainAppearance = new MeshAppearance(_z, _mainColor, !(_height > 0));
            MainTessellator = new TrianglePolygonTessellator2d(true, _height);
        }
    }
}