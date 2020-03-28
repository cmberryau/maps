namespace Maps.Appearance
{
    /// <summary>
    /// Interface for sprite appearance
    /// </summary>
    public interface ISpriteAppearance : IUIRenderableAppearance
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