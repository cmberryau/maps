using System.Reflection;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// Root category for areas
    /// </summary>
    public enum RootAreaCategory
    {
#pragma warning disable 1591
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Invalid,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Park,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Water,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Forest,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Farmland,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Grassland,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        ResidentialZone,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        CommercialZone,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        RetailZone,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        IndustrialZone,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        MilitaryZone,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Building,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Tile,
#pragma warning restore 1591
    }

    /// <summary>
    /// Describes the category of an area
    /// </summary>
    public class AreaCategory
    {
        /// <summary>
        /// The unknown area category
        /// </summary>
        public static AreaCategory Unknown => new AreaCategory(RootAreaCategory.Invalid);

        /// <summary>
        /// The root category of the area
        /// </summary>
        public readonly RootAreaCategory Root;

        /// <summary>
        /// Initializes a new instance of AreaCategory
        /// </summary>
        /// <param name="root">The root category</param>
        public AreaCategory(RootAreaCategory root)
        {
            Root = root;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Root.ToString();
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Root.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is AreaCategory))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (AreaCategory)obj;
            return other.Root == Root;
        }
    }
}