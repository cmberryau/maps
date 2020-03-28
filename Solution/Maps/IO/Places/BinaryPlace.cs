using System;
using System.Drawing;
using System.Runtime.Serialization;
using Maps.Geographical;
using Maps.Geographical.Features;
using Maps.Geographical.Places;
using Maps.IO.Features;
using Maps.IO.Geographical;
using ProtoBuf;

namespace Maps.IO.Places
{
    /// <summary>
    /// Represents a Place for binary storage
    /// </summary>
    [ProtoContract]
    internal class BinaryPlace : BinaryFeature
    {
        static BinaryPlace()
        {
            Serializer.PrepareSerializer<BinaryPlace>();
        }

        private Geodetic2d _coordinate;
        private PlaceCategory _category;

        [ProtoMember(1)]
        private readonly BinaryCoordinate _binaryCoordinate;
        [ProtoMember(2)]
        private readonly BinaryPlaceCategory _binaryCategory;
        [ProtoMember(3)]
        private readonly long _nameId;
        [ProtoMember(4)]
        private readonly long _iconId;

        /// <summary>
        /// Initializes a new instance of BinaryPlace
        /// </summary>
        /// <param name="place">The place to store</param>
        /// <param name="sideData">The side data target</param>
        internal BinaryPlace(Place place, ISideData sideData = null) : base(place?.Guid ?? Guid.Empty)
        {
            if (place == null)
            {
                throw new ArgumentNullException(nameof(place));
            }

            if (sideData != null)
            {
                if (sideData.TryGetTable<string>(out var strings))
                {
                    _nameId = strings.Add(place.Name);
                }

                if (sideData.TryGetTable<Bitmap>(out var bitmaps))
                {
                    _iconId = bitmaps.Add(place.Icon);
                }
            }

            _binaryCoordinate = new BinaryCoordinate(place.Coordinate);
            _binaryCategory = new BinaryPlaceCategory(place.Category);
        }

        /// <inheritdoc />
        public override Feature ToFeature(ISideData sideData = null)
        {
            var name = string.Empty;
            Bitmap icon = null;

            if (sideData != null)
            {
                if (sideData.TryGetTable<string>(out var strings))
                {
                    strings.TryGet(_nameId, out name);
                }

                if (sideData.TryGetTable<Bitmap>(out var bitmaps))
                {
                    bitmaps.TryGet(_iconId, out icon);
                }
            }

            return new Place(Guid, name, _coordinate, _category, icon);
        }

        private BinaryPlace() : base() { }

        [OnDeserialized]
        private void OnDeserialized()
        {
            _coordinate = _binaryCoordinate.Geodetic2dCoordinate();
            _category = _binaryCategory.ToPlaceCategory();
        }
    }
}