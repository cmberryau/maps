using System;
using System.Collections.Generic;
using Maps.Appearance.Properties;
using Maps.Geometry.Tessellation;

namespace Maps.Appearance
{
    /// <summary>
    /// Parses properties for the SegmentAppearance class
    /// </summary>
    public class SegmentPropertyParser : FeaturePropertyParser
    {
        /// <summary>
        /// The main tessellator
        /// </summary>
        public ILineTessellator MainTessellator
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

        /// <summary>
        /// Is there an outline?
        /// </summary>
        public bool Outline
        {
            get;
            private set;
        }

        /// <summary>
        /// The outline appearance
        /// </summary>
        public MeshAppearance OutlineAppearance
        {
            get;
            private set;
        }

        /// <summary>
        /// The outline tessellator
        /// </summary>
        public ILineTessellator OutlineTessellator
        {
            get;
            private set;
        }

        // main info
        private int _z;
        private Colorf _mainColor;
        private double _width;

        // outline info
        private Colorf _outlineColor;
        private double _outlineWidth;

        /// <summary>
        /// Initializes a new instance of PropertyParser
        /// </summary>
        /// <param name="properties">The properties to parse</param>
        public SegmentPropertyParser(IList<Property> properties) : base(properties)
        {

        }

        /// <inheritdoc />
        public override void Visit(DoubleProperty property)
        {
            base.Visit(property);

            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (property.Key.Equals("width"))
            {
                _width = property.Value;
            }
            else if (property.Key.Equals("outline_width"))
            {
                _outlineWidth = property.Value;
            }
        }

        /// <inheritdoc />
        public override void Visit(ColorProperty property)
        {
            base.Visit(property);

            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (property.Key.Equals("main"))
            {
                _mainColor = property.Value;
            }
            else if (property.Key.Equals("outline_color"))
            {
                _outlineColor = property.Value;
            }
        }

        /// <inheritdoc />
        public override void Visit(Int32Property property)
        {
            base.Visit(property);

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
        public override void Visit(BoolProperty property)
        {
            base.Visit(property);

            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (property.Key.Equals("outline") && property.Value)
            {
                Outline = property.Value;
            }
        }

        /// <inheritdoc />
        protected override void OnParseComplete()
        {
            base.OnParseComplete();

            if (Outline)
            {
                OutlineAppearance = new MeshAppearance(_z - 1, _outlineColor, true);
                OutlineTessellator = new TriangleLineTessellator2d(_outlineWidth);
            }

            MainAppearance = new MeshAppearance(_z, _mainColor, true);
            MainTessellator = new TriangleLineTessellator2d(_width);
        }
    }
}