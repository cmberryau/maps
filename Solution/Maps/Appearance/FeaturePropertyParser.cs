using System;
using System.Collections.Generic;
using Maps.Appearance.Properties;
using Maps.Rendering;

namespace Maps.Appearance
{
    /// <summary>
    /// Parses common feature properties
    /// </summary>
    public abstract class FeaturePropertyParser : PropertyParserBase
    {
        /// <summary>
        /// Is there a label?
        /// </summary>
        public bool Label
        {
            get;
            private set;
        }

        /// <summary>
        /// Is there an icon?
        /// </summary>
        public bool Icon
        {
            get;
            private set;
        }

        /// <summary>
        /// The label appearance
        /// </summary>
        public LabelAppearance LabelAppearance
        {
            get;
            private set;
        }

        /// <summary>
        /// The icon appearance
        /// </summary>
        public IconAppearance IconApppearance
        {
            get;
            private set;
        }

        // label info
        private int _labelZ = RenderableAppearance.DefaultZIndex;
        private float _labelPadding = UIRenderableAppearance.DefaultPadding;
        private bool _labelIgnoreOthers = UIRenderableAppearance.DefaultIgnoreOthers;
        private bool _labelRotateWithMap = UIRenderableAppearance.DefaultRotateWithMap;
        private Colorf _labelFontColor = LabelAppearance.DefaultFontColor;
        private bool _labelBold = LabelAppearance.DefaultFontBold;
        private float _labelFontSize = LabelAppearance.DefaultFontSize;
        private bool _labelFontOutline = LabelAppearance.DefaultFontOutline;
        private Colorf _labelFontOutlineColor = LabelAppearance.DefaultFontOutlineColor;
        private bool _labelRequiredSegmentLength = LabelAppearance.DefaultMinimumSegmentLength;
        private float _labelRequiredSegmentLengthValue = LabelAppearance.DefaultMinimumSegmentLengthValue;

        // icon info
        private int _iconZ = RenderableAppearance.DefaultZIndex;
        private float _iconPadding = UIRenderableAppearance.DefaultPadding;
        private bool _iconIgnoreOthers = UIRenderableAppearance.DefaultIgnoreOthers;
        private bool _iconRotateWithMap = UIRenderableAppearance.DefaultRotateWithMap;
        private Colorf _iconBackgroundColor = IconAppearance.DefaultBackgroundColor;

        /// <inheritdoc />
        protected FeaturePropertyParser(IList<Property> properties) : base(properties)
        {

        }

        /// <inheritdoc />
        public override void Visit(BoolProperty property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (property.Key.Equals("label"))
            {
                Label = property.Value;
            }
            else if (property.Key.Equals("label_font_bold"))
            {
                _labelBold = property.Value;
            }
            else if (property.Key.Equals("label_font_outline"))
            {
                _labelFontOutline = property.Value;
            }
            else if (property.Key.Equals("icon"))
            {
                Icon = property.Value;
            }
        }

        /// <inheritdoc />
        public override void Visit(ColorProperty property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (property.Key.Equals("label_font_color"))
            {
                _labelFontColor = property.Value;
            }
            else if (property.Key.Equals("label_font_outline_color"))
            {
                _labelFontOutlineColor = property.Value;
            }
            else if (property.Key.Equals("icon_background_color"))
            {
                _iconBackgroundColor = property.Value;
            }
        }

        /// <inheritdoc />
        public override void Visit(SingleProperty property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (property.Key.Equals("label_font_size"))
            {
                _labelFontSize = property.Value;
            }
            else if (property.Key.Equals("label_minimum_segment_length"))
            {
                _labelRequiredSegmentLength = true;
                _labelRequiredSegmentLengthValue = property.Value;
            }
            else if (property.Key.Equals("label_padding"))
            {
                _labelPadding = property.Value;
            }
            else if (property.Key.Equals("icon_padding"))
            {
                _iconPadding = property.Value;
            }
        }

        /// <inheritdoc />
        public override void Visit(Int32Property property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (property.Key.Equals("label_z"))
            {
                _labelZ = property.Value;
            }
            else if (property.Key.Equals("icon_z"))
            {
                _iconZ = property.Value;
            }
        }

        /// <inheritdoc />
        protected override void OnParseComplete()
        {
            if (Label)
            {
                LabelAppearance = new LabelAppearance(_labelZ, _labelPadding,
                    _labelIgnoreOthers, _labelRotateWithMap, _labelFontColor, _labelBold, 
                    _labelFontSize, _labelFontOutline, _labelFontOutlineColor,
                    _labelRequiredSegmentLength, _labelRequiredSegmentLengthValue);
            }

            if (Icon)
            {
                IconApppearance = new IconAppearance(_iconZ, _iconPadding,
                    _iconIgnoreOthers, _iconRotateWithMap, _iconBackgroundColor);
            }
        }
    }
}