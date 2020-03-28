using System;

namespace Maps.Appearance
{
    /// <summary>
    /// Holds information of how a label should appear
    /// </summary>
    public class LabelAppearance : UIRenderableAppearance, ILabelAppearance
    {
        /// <summary>
        /// The default font color
        /// </summary>
        public static readonly Colorf DefaultFontColor = Colorf.White;

        /// <summary>
        /// The default font bold state
        /// </summary>
        public const bool DefaultFontBold = false;

        /// <summary>
        /// The default font size
        /// </summary>
        public const float DefaultFontSize = 14;

        /// <summary>
        /// The default font outline state
        /// </summary>
        public const bool DefaultFontOutline = true;

        /// <summary>
        /// The default font outline color
        /// </summary>
        public static readonly Colorf DefaultFontOutlineColor = Colorf.Black;

        /// <summary>
        /// The default minimum segment length state
        /// </summary>
        public const bool DefaultMinimumSegmentLength = false;

        /// <summary>
        /// The default minimum segment length value
        /// </summary>
        public const float DefaultMinimumSegmentLengthValue = 10f;

        /// <summary>
        /// The main color of the font
        /// </summary>
        public Colorf FontColor
        {
            get;
        }

        /// <summary>
        /// The bold state of the font
        /// </summary>
        public bool FontBold
        {
            get;
        }

        /// <summary>
        /// The size of the font
        /// </summary>
        public float FontSize
        {
            get;
        }

        /// <summary>
        /// The outline state of the font
        /// </summary>
        public bool FontOutline
        {
            get;
        }

        /// <summary>
        /// The font outline color
        /// </summary>
        public Colorf FontOutlineColor
        {
            get;
        }

        /// <summary>
        /// The minimum segment length state
        /// </summary>
        public bool MinimumSegmentLength
        {
            get;
        }

        /// <summary>
        /// The minimum segment length
        /// </summary>
        public float MinimumSegmentLengthValue
        {
            get;
        }

        private readonly int _hashCode;

        /// <inheritdoc />
        public LabelAppearance(int z, float padding, bool ignoreOthers, 
            bool rotateWithMap, Colorf fontColor,bool fontBold, float fontSize, 
            bool fontOutline,Colorf fontOutlineColor, bool minimumSegmentLength, 
            float minimumSegmentLengthValue) : base(z, padding, ignoreOthers, rotateWithMap)
        {
            FontColor = fontColor;
            FontBold = fontBold;
            FontSize = fontSize;
            FontOutline = fontOutline;
            FontOutlineColor = fontOutlineColor;
            MinimumSegmentLength = minimumSegmentLength;
            MinimumSegmentLengthValue = minimumSegmentLengthValue;

            _hashCode = GenerateHashCode();
        }

        /// <inheritdoc />
        public override void Accept(IUIElementAppearanceVisitor visitor)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException(nameof(visitor));
            }

            visitor.Visit(this);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is LabelAppearance))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetHashCode().Equals(GetHashCode());
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return _hashCode;
        }

        private int GenerateHashCode()
        {
            var hash = base.GetHashCode();

            unchecked
            {
                hash = (hash * 397) ^ FontColor.GetHashCode();
                hash = (hash * 397) ^ FontBold.GetHashCode();
                hash = (hash * 397) ^ FontSize.GetHashCode();
                hash = (hash * 397) ^ FontOutline.GetHashCode();
                hash = (hash * 397) ^ FontOutlineColor.GetHashCode();
                hash = (hash * 397) ^ MinimumSegmentLength.GetHashCode();
                hash = (hash * 397) ^ MinimumSegmentLengthValue.GetHashCode();

                return hash;
            }
        }
    }
}