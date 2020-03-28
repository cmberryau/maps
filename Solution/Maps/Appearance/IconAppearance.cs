using System;

namespace Maps.Appearance
{
    /// <summary>
    /// Holds information of how an icon should appear
    /// </summary>
    public class IconAppearance : UIRenderableAppearance, IIconAppearance
    {
        /// <summary>
        /// The default background color
        /// </summary>
        public static readonly Colorf DefaultBackgroundColor = Colorf.White;

        /// <inheritdoc />
        public Colorf Color
        {
            get;
        }

        private readonly int _hashCode;

        /// <inheritdoc />
        public IconAppearance(int z, float padding, bool ignoreOthers, bool rotateWithMap,
            Colorf background) : base(z, padding, ignoreOthers, rotateWithMap)
        {
            Color = background;
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
            if (!(obj is IconAppearance))
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
                hash = (hash * 397) ^ Color.GetHashCode();
                return hash;
            }
        }
    }
}