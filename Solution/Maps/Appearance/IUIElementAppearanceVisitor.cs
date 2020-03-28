namespace Maps.Appearance
{
    /// <summary>
    /// An interface to gain access to concrete UIElementAppearance classes
    /// </summary>
    public interface IUIElementAppearanceVisitor
    {
        /// <summary>
        /// Visits a LabelAppearance instance
        /// </summary>
        /// <param name="appearance">The appearance to visit</param>
        void Visit(LabelAppearance appearance);

        /// <summary>
        /// Visits an IconAppearance instance
        /// </summary>
        /// <param name="appearance">The appearance to visit</param>
        void Visit(IconAppearance appearance);

        /// <summary>
        /// Visits a SpriteAppearance instance
        /// </summary>
        /// <param name="appearance">The appearance to visit</param>
        void Visit(SpriteAppearance appearance);
    }
}