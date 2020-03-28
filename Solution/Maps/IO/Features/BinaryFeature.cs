using System;
using System.IO;
using System.Runtime.Serialization;
using Maps.Extensions;
using Maps.Geographical.Features;
using Maps.IO.Places;
using ProtoBuf;
using ProtoBuf.Meta;

namespace Maps.IO.Features
{
    /// <summary>
    /// Represents a Feature for binary storage
    /// </summary>
    [ProtoContract]
    [ProtoInclude(4, typeof(BinarySegment))]
    [ProtoInclude(5, typeof(BinaryArea))]
    [ProtoInclude(6, typeof(BinaryPlace))]
    public abstract class BinaryFeature
    {
        static BinaryFeature()
        {
            Serializer.PrepareSerializer<BinaryFeature>();
        }

        /// <summary>
        /// The guid of the binary feature
        /// </summary>
        protected Guid Guid
        {
            get;
            private set;
        }

        [ProtoMember(1)]
        private readonly long _idPart0;

        [ProtoMember(2)]
        private readonly long _idPart1;

        /// <summary>
        /// Initializes a new instance of BinaryFeature
        /// </summary>
        /// <param name="guid">The guid</param>
        protected BinaryFeature(Guid guid)
        {
            Guid = guid;

            _idPart0 = Guid.ToLong();
            _idPart1 = Guid.ToLong(8);
        }

        /// <summary>
        /// The empty constructor as required by protobuf-net
        /// </summary>
        protected BinaryFeature(){}

        /// <summary>
        /// Serializes the BinaryFeature
        /// </summary>
        /// <param name="destination">The destination stream</param>
        /// <param name="endMarker">Is an end marker required?</param>
        public void Serialize(Stream destination, bool endMarker = false)
        {
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            if (endMarker)
            {
                Serializer.SerializeWithLengthPrefix(destination, this,
                    PrefixStyle.Base128);
            }
            else
            {
                Serializer.Serialize(destination, this);
            }
        }

        /// <summary>
        /// Deserializes a BinaryFeature
        /// </summary>
        /// <param name="source">The source stream</param>
        /// <param name="endMarker">Are end markers used in serialization?</param>
        public static BinaryFeature Deserialize(Stream source, bool endMarker = false)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (endMarker)
            {
                return Serializer.DeserializeWithLengthPrefix<BinaryFeature>(source,
                    PrefixStyle.Base128);
            }

            return Serializer.Deserialize<BinaryFeature>(source);
        }

        /// <summary>
        /// Converts to the Feature class representation of the Feature
        /// </summary>
        /// <param name="sideData">The side data source</param>
        public abstract Feature ToFeature(ISideData sideData = null);

        [OnDeserialized]
        private void OnDeserialized()
        {
            Guid = GuidExtensions.ToGuid(_idPart0, _idPart1);
        }
    }
}