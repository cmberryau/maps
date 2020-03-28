using Maps.Rendering;

namespace Maps.Appearance
{
    /// <summary>
    /// Holds information of how a mesh element should appear
    /// </summary>
    public class MeshAppearance : RenderableAppearance, IMeshAppearance
    {
        /// <inheritdoc />
        public bool Flat
        {
            get;
        }

        /// <inheritdoc />
        public Colorf MainColor
        {
            get;
        }

        /// <inheritdoc />
        public MeshAppearance(int z, Colorf mainColor, bool flat) : base(z)
        {
            MainColor = mainColor;
            Flat = flat;
            _hashCode = GenerateHashCode();
        }

        private readonly int _hashCode;

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is MeshAppearance))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return GetHashCode().Equals(obj.GetHashCode());
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
                hash = (hash * 397) ^ Flat.GetHashCode();
                hash = (hash * 397) ^ MainColor.GetHashCode();
                return hash;
            }
        }
    }
}