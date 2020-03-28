using System;
using Maps.Geographical.Places;
using ProtoBuf;

namespace Maps.IO.Places
{
    /// <summary>
    /// Represents a PlaceCategory for binary storage
    /// </summary>
    [ProtoContract]
    public class BinaryPlaceCategory
    {
        static BinaryPlaceCategory()
        {
            Serializer.PrepareSerializer<BinaryPlaceCategory>();
        }

        [ProtoMember(1)]
        private readonly RootPlaceCategory _root;

        /// <summary>
        /// Initializes a new instance of BinaryPlaceCategory
        /// </summary>
        /// <param name="category">The category to use</param>
        /// <exception cref="ArgumentNullException">Thrown if category is null
        /// </exception>
        internal BinaryPlaceCategory(PlaceCategory category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _root = category.Root;
        }

        private BinaryPlaceCategory() { }

        /// <summary>
        /// Returns the PlaceCategory stored by the BinaryPlaceCategory
        /// </summary>
        internal PlaceCategory ToPlaceCategory()
        {
            return new PlaceCategory(_root);
        }
    }
}