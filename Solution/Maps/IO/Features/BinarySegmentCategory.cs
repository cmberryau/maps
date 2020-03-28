using System;
using Maps.Geographical.Features;
using ProtoBuf;

namespace Maps.IO.Features
{
    /// <summary>
    /// Represents a SegmentCategory for binary storage
    /// </summary>
    [ProtoContract]
    public class BinarySegmentCategory
    {
        static BinarySegmentCategory()
        {
            Serializer.PrepareSerializer<BinarySegmentCategory>();
        }

        [ProtoMember(1)]
        private readonly RootSegmentCategory _root;

        /// <summary>
        /// Initializes a new instance of BinarySegmentCategory
        /// </summary>
        /// <param name="category">The category to use</param>
        /// <exception cref="ArgumentNullException">Thrown if category is null
        /// </exception>
        internal BinarySegmentCategory(SegmentCategory category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _root = category.Root;
        }

        private BinarySegmentCategory() { }

        /// <summary>
        /// Returns the SegmentCategory stored by the BinarySegmentCategory
        /// </summary>
        internal SegmentCategory ToSegmentCategory()
        {
            return new SegmentCategory(_root);
        }
    }
}