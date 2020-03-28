namespace Maps.Appearance
{
    /// <summary>
    /// Interface for icon appearance
    /// </summary>
    public interface IIconAppearance : IUIRenderableAppearance
    {
        /// <summary>
        /// The color of the icon
        /// </summary>
        Colorf Color
        {
            get;
        }
    }
}