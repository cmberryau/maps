using System;
using Maps.Geographical.Features;
using ProtoBuf;

namespace Maps.IO.Features
{
    /// <summary>
    /// Represents an AreaCategory for binary storage
    /// </summary>
    [ProtoContract]
    public class BinaryAreaCategory
    {
        static BinaryAreaCategory()
        {
            Serializer.PrepareSerializer<BinaryAreaCategory>();
        }

        [ProtoMember(1)]
        private readonly RootAreaCategory _root;

        /// <summary>
        /// Initializes a new instance of BinaryAreaCategory
        /// </summary>
        /// <param name="category">The category to use</param>
        /// <exception cref="ArgumentNullException">Thrown if category is null
        /// </exception>
        internal BinaryAreaCategory(AreaCategory category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _root = category.Root;
        }

        private BinaryAreaCategory() { }

        /// <summary>
        /// Returns the AreaCategory stored by the BinaryAreaCategory
        /// </summary>
        internal AreaCategory ToAreaCategory()
        {
            return new AreaCategory(_root);
        }
    }
}