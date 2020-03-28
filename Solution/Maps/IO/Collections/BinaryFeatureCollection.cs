using System;
using System.IO;
using Maps.IO.Features;
using ProtoBuf;

namespace Maps.IO.Collections
{
    /// <summary>
    /// A serializable collection of binary map features
    /// </summary>
    [ProtoContract]
    public class BinaryFeatureCollection
    {
        static BinaryFeatureCollection()
        {
            Serializer.PrepareSerializer<BinaryFeatureCollection>();
        }

        /// <summary>
        /// The binary features held by this BinaryFeatureCollection
        /// </summary>
        public BinaryFeature this[int index] => _features[index];

        /// <summary>
        /// The number of features held by this BinaryFeatureCollection
        /// </summary>
        public int Count => _features?.Length ?? 0;

        /// <summary>
        /// The array of features
        /// </summary>
        [ProtoMember(1)]
        private readonly BinaryFeature[] _features;

        /// <summary>
        /// Creates a populated instance of BinaryFeatureCollection
        /// </summary>
        /// <param name="features">The features to add to the BinaryFeatureCollection</param>
        public BinaryFeatureCollection(BinaryFeature[] features)
        {
            if (features == null)
            {
                throw new ArgumentNullException(nameof(features));
            }

            _features = features;
        }

        /// <summary>
        /// Serializes the BinaryFeatureCollection
        /// </summary>
        /// <param name="destination">The destination stream</param>
        public void Serialize(Stream destination)
        {
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            Serializer.Serialize(destination, this);
        }

        /// <summary>
        /// Deserializes a BinaryFeatureCollection
        /// </summary>
        /// <param name="source">The source stream to deserialize from</param>
        public static BinaryFeatureCollection Deserialize(Stream source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var collection = Serializer.Deserialize<BinaryFeatureCollection>(source);

            return collection.Count > 0 ? collection : null;
        }

        private BinaryFeatureCollection() { }
    }
}