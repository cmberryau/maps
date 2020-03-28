using System;
using System.Runtime.Serialization;
using Maps.Geographical;
using Maps.Geographical.Features;
using Maps.IO.Geographical;
using ProtoBuf;

namespace Maps.IO.Features
{
    /// <summary>
    /// Represents a Segment for binary storage
    /// </summary>
    [ProtoContract]
    public class BinarySegment : BinaryFeature
    {
        static BinarySegment()
        {
            Serializer.PrepareSerializer<BinarySegment>();
        }

        private Geodetic2d[] _coordinates;
        private SegmentCategory _category;

        [ProtoMember(1)]
        private readonly BinaryCoordinate[] _binaryCoords;
        [ProtoMember(2)]
        private readonly BinarySegmentCategory _binaryCategory;
        [ProtoMember(3)]
        private readonly long _nameId;

        /// <summary>
        /// Creates a contiguous BinarySegment
        /// </summary>
        /// <param name="segment">The segment to store</param>
        /// <param name="sideData">The side data</param>
        public BinarySegment(Segment segment, ISideData sideData = null) : base(segment?.Guid ?? Guid.Empty)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            if (sideData != null && sideData.TryGetTable(out ITable<string> strings))
            {
                _nameId = strings.Add(segment.Name);
            }

            _binaryCoords = new BinaryCoordinate[segment.LineStrip.Count];

            for (var i = 0; i < _binaryCoords.Length; i++)
            {
                _binaryCoords[i] = new BinaryCoordinate(segment.LineStrip[i]);
            }

            _binaryCategory = new BinarySegmentCategory(segment.Category);
        }

        /// <summary>
        /// Empty constructor as required by protobuf-net
        /// </summary>
        protected BinarySegment() : base() { }

        /// <inheritdoc />
        public override Feature ToFeature(ISideData sideData = null)
        {
            var name = string.Empty;

            if (sideData != null && sideData.TryGetTable(out ITable<string> strings))
            {
                strings.TryGet(_nameId, out name);
            }

            return new Segment(Guid, name, _coordinates, _category);
        }

        [OnDeserialized]
        private void OnDeserialized()
        {
            _category = _binaryCategory.ToSegmentCategory();
            _coordinates = new Geodetic2d[_binaryCoords.Length];

            for (var i = 0; i < _binaryCoords.Length; i++)
            {
                _coordinates[i] = _binaryCoords[i].Geodetic2dCoordinate();
            }
        }
    }
}