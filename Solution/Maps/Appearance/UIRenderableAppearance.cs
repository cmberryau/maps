using Maps.Rendering;

namespace Maps.Appearance
{
    /// <summary>
    /// Holds information of how a ui renderable element should appear
    /// </summary>
    public abstract class UIRenderableAppearance : RenderableAppearance, 
        IUIRenderableAppearance
    {
        /// <summary>
        /// The default padding
        /// </summary>
        public const float DefaultPadding = 1f;

        /// <summary>
        /// The default ignore others behaviour
        /// </summary>
        public const bool DefaultIgnoreOthers = false;

        /// <summary>
        /// The default rotate with map behaviour
        /// </summary>
        public const bool DefaultRotateWithMap = false;

        /// <inheritdoc />
        public float Padding
        {
            get;
        }

        /// <inheritdoc />
        public bool IgnoreOthers
        {
            get;
        }

        /// <inheritdoc />
        public bool RotateWithMap
        {
            get;
        }

        /// <inheritdoc />
        protected UIRenderableAppearance(int z, float padding, bool ignoreOthers,
            bool rotateWithMap) : base(z)
        {
            Padding = padding;
            IgnoreOthers = ignoreOthers;
            RotateWithMap = rotateWithMap;
        }

        /// <summary>
        /// Accepts the visitor
        /// </summary>
        /// <param name="visitor">The visitor to accept</param>
        public abstract void Accept(IUIElementAppearanceVisitor visitor);

        /// <inheritdoc />
        public abstract override bool Equals(object obj);

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hash = (base.GetHashCode() * 397) ^ Padding.GetHashCode();
                hash = (hash * 397) ^ IgnoreOthers.GetHashCode();
                return hash;
            }
        }
    }
}