using Maps.Appearance;

namespace Maps.Rendering
{
    /// <summary>
    /// Responsible for holding renderable object appearance information
    /// </summary>
    public abstract class RenderableAppearance : IRenderableAppearance
    {
        /// <summary>
        /// The default z index
        /// </summary>
        public const int DefaultZIndex = 0;

        /// <inheritdoc />
        public int ZIndex
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of RenderableAppearance
        /// </summary>
        protected RenderableAppearance(int z)
        {
            ZIndex = z;
        }

        /// <inheritdoc />
        public abstract override bool Equals(object obj);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return ZIndex.GetHashCode();
        }
    }
}