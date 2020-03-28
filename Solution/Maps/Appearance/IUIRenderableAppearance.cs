namespace Maps.Appearance
{
    /// <summary>
    /// Intrerface for ui renderable appearance
    /// </summary>
    public interface IUIRenderableAppearance : IRenderableAppearance
    {
        /// <summary>
        /// The padding around the element
        /// </summary>
        float Padding
        {
            get;
        }

        /// <summary>
        /// Does the element ignore other ui elements?
        /// </summary>
        bool IgnoreOthers
        {
            get;
        }

        /// <summary>
        /// Does the element rotate with the map?
        /// </summary>
        bool RotateWithMap
        {
            get;
        }
    }
}