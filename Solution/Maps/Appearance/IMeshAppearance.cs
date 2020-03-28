namespace Maps.Appearance
{
    /// <summary>
    /// Interface for mesh renderable appearance
    /// </summary>
    public interface IMeshAppearance : IRenderableAppearance
    {
        /// <summary>
        /// Is the mesh flat?
        /// </summary>
        bool Flat
        {
            get;
        }

        /// <summary>
        /// The main color
        /// </summary>
        Colorf MainColor
        {
            get;
        }
    }
}