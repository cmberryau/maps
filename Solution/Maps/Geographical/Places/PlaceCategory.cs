using System.Reflection;

namespace Maps.Geographical.Places
{
    /// <summary>
    /// Root category enums for places
    /// </summary>
    public enum RootPlaceCategory
    {
#pragma warning disable 1591
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Invalid,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        FoodAndDrink,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Entertainment,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Nature,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Shopping,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Transport,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Accomodation,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Services,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Parking,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Petrol,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Emergency,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        City,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Borough,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Suburb,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Quarter,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Neighbourhood,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Town,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Village,
        [Obfuscation(Feature = "renaming", Exclude = true)]
        Hamlet,
#pragma warning restore 1591
    }

    /// <summary>
    /// Describes the category of a place
    /// </summary>
    public class PlaceCategory
    {
        /// <summary>
        /// The unknown place category
        /// </summary>
        public static PlaceCategory Unknown => new PlaceCategory(
            RootPlaceCategory.Invalid);

        /// <summary>
        /// The root category that the PlaceCategory belongs to
        /// </summary>
        public readonly RootPlaceCategory Root;

        /// <summary>
        /// Initializes a new instance of PlaceCategory with the given
        /// root level category
        /// </summary>
        /// <param name="root">The root category</param>
        public PlaceCategory(RootPlaceCategory root)
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
            if (!(obj is PlaceCategory))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (PlaceCategory) obj;

            return other.Root == Root;
        }
    }
}