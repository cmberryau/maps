using System.Reflection;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// The root category of a segment
    /// </summary>
    public enum RootSegmentCategory
    {
#pragma warning disable 1591
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Invalid,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        UnknownStreet,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Freeway,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        FreewayLink,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        MajorHighway,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        MajorHighwayLink,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        MinorHighway,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        MinorHighwayLink,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        PrimaryStreet,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        PrimaryStreetLink,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        SecondaryStreet,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        SecondaryStreetLink,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        ResidentialStreet,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        UnclassifiedStreet,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        PedestrianSharedStreet,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        ServiceStreet,
#pragma warning restore 1591
    }

    /// <summary>
    /// Describes the category of a segment
    /// </summary>
    public class SegmentCategory
    {
        /// <summary>
        /// The unknown segment category
        /// </summary>
        public static SegmentCategory Unknown => new SegmentCategory(
            RootSegmentCategory.UnknownStreet);

        /// <summary>
        /// Is the segment category referring to a link?
        /// </summary>
        public bool IsLink => Root == RootSegmentCategory.MajorHighwayLink ||
                              Root == RootSegmentCategory.MinorHighwayLink ||
                              Root == RootSegmentCategory.PrimaryStreetLink ||
                              Root == RootSegmentCategory.SecondaryStreetLink;

        /// <summary>
        /// The root category of a segment
        /// </summary>
        public readonly RootSegmentCategory Root;

        /// <summary>
        /// Initializes a new instance of SegmentCategory
        /// </summary>
        /// <param name="root">The root category</param>
        public SegmentCategory(RootSegmentCategory root)
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
            if (!(obj is SegmentCategory))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (SegmentCategory)obj;
            return other.Root == Root;
        }
    }
}