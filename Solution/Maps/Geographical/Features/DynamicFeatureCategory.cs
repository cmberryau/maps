using System.Reflection;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// Root category for dynamic features
    /// </summary>
    public enum RootDynamicFeatureCategory
    {
#pragma warning disable 1591
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Unknown,
#pragma warning restore 1591
    }

    /// <summary>
    /// Describes the category of a dynamic feature
    /// </summary>
    public class DynamicFeatureCategory
    {
        /// <summary>
        /// The unknown dynamic feature category
        /// </summary>
        public static DynamicFeatureCategory Unknown => new
            DynamicFeatureCategory(RootDynamicFeatureCategory.Unknown);

        /// <summary>
        /// The root category of the dynamic feature
        /// </summary>
        public readonly RootDynamicFeatureCategory Root;

        /// <summary>
        /// Initializes a new instance of DynamicFeatureCategory
        /// </summary>
        /// <param name="root">The root category</param>
        public DynamicFeatureCategory(RootDynamicFeatureCategory root)
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
            if (!(obj is DynamicFeatureCategory))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (DynamicFeatureCategory)obj;
            return other.Root == Root;
        }
    }
}