namespace Maps.Appearance
{
    /// <summary>
    /// Interface for label appearance
    /// </summary>
    public interface ILabelAppearance : IUIRenderableAppearance
    {
        /// <summary>
        /// The bold state of the font
        /// </summary>
        bool FontBold
        {
            get;
        }

        /// <summary>
        /// The main color of the font
        /// </summary>
        Colorf FontColor
        {
            get;
        }

        /// <summary>
        /// The size of the font
        /// </summary>
        float FontSize
        {
            get;
        }

        /// <summary>
        /// The outline state of the font
        /// </summary>
        bool FontOutline
        {
            get;
        }

        /// <summary>
        /// The font outline color
        /// </summary>
        Colorf FontOutlineColor
        {
            get;
        }
    }
}