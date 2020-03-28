namespace Maps.Appearance
{
    /// <summary>
    /// Interface for all UI elemnents
    /// </summary>
    public interface IUIElement
    {
        /// <summary>
        /// Is the UI element shown?
        /// </summary>
        bool Shown
        {
            get;
        }

        /// <summary>
        /// Is the UI element active?
        /// </summary>
        bool Active
        {
            get;
        }
    }
}