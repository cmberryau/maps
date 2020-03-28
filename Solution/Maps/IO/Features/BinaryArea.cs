using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Maps.Geographical;
using Maps.Geographical.Features;
using Maps.IO.Geographical;
using ProtoBuf;

namespace Maps.IO.Features
{
    /// <summary>
    /// Represents an Area for binary storage
    /// </summary>
    [ProtoContract]
    internal class BinaryArea : BinaryFeature
    {
        static BinaryArea()
        {
            Serializer.PrepareSerializer<BinaryArea>();
        }

        private Geodetic2d[] _outerCoordinates;
        private Geodetic2d[][] _innerCoordinates;
        private AreaCategory _category;

        [ProtoMember(1)]
        private readonly BinaryCoordinate[] _binaryOuters;
        [ProtoMember(2)]
        private readonly BinaryCoordinate[] _binaryHoles;
        [ProtoMember(3)]
        private readonly int[] _innerSplits;
        [ProtoMember(4)]
        private readonly BinaryAreaCategory _binaryCategory;
        [ProtoMember(5)]
        private readonly double _originalArea;
        [ProtoMember(6)]
        private readonly long _nameId;

        /// <summary>
        /// Initializes a new instance of BinaryArea
        /// </summary>
        /// <param name="area">The area to store</param>
        /// <param name="sideData">The side data target</param>
        internal BinaryArea(Area area, ISideData sideData = null) : base(area?.Guid ?? Guid.Empty)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            var polygon = area.Polygon;

            // if there are any holes
            if (polygon.HoleCount > 0)
            {
                // step back one for polygon closure
                var totalHoleCount = polygon.Hole(0).Count - 1;

                // if there is more than one hole, we need to setup the splits
                if (polygon.HoleCount > 1)
                {
                    _innerSplits = new int[polygon.HoleCount - 1];

                    // running through each hole
                    for (var i = 1; i < polygon.HoleCount; ++i)
                    {
                        // mark the split at the current count
                        _innerSplits[i - 1] = totalHoleCount;
                        // increment the total count for marking splits and total array generation
                        totalHoleCount += polygon.Hole(i).Count - 1;
                    }
                }

                // generate the flat hole coordinate array
                _binaryHoles = new BinaryCoordinate[totalHoleCount];
                for (int i = 0, k = 0; i < polygon.HoleCount; ++i)
                {
                    var hole = polygon.Hole(i);

                    for (var j = 0; j < hole.Count - 1; ++j)
                    {
                        _binaryHoles[k++] = new BinaryCoordinate(hole[j]);
                    }
                }
            }

            // generate the outer coordinate array, stepping back one to skip closure coordinate
            _binaryOuters = new BinaryCoordinate[polygon.Count - 1];
            for (var i = 0; i < _binaryOuters.Length; ++i)
            {
                _binaryOuters[i] = new BinaryCoordinate(polygon[i]);
            }

            if (sideData != null && sideData.TryGetTable(out ITable<string> strings))
            {
                _nameId = strings.Add(area.Name);
            }

            _binaryCategory = new BinaryAreaCategory(area.Category);
            _originalArea = area.OriginalArea;
        }

        /// <summary>
        /// Converts to the Feature class representation of the BinaryArea
        /// </summary>
        public override Feature ToFeature(ISideData sideData = null)
        {
            var name = string.Empty;

            if (sideData != null && sideData.TryGetTable(out ITable<string> strings))
            {
                strings.TryGet(_nameId, out name);
            }

            if (_innerCoordinates == null)
            {
                return new Area(Guid, name, new GeodeticPolygon2d((IList<Geodetic2d>)_outerCoordinates),
                    _category, _originalArea);
            }

            var holes = new GeodeticPolygon2d[_innerCoordinates.Length];

            for (var i = 0; i < _innerCoordinates.Length; ++i)
            {
                holes[i] = new GeodeticPolygon2d((IList<Geodetic2d>)_innerCoordinates[i]);
            }

            return new Area(Guid, name, new GeodeticPolygon2d(_outerCoordinates, holes), _category, _originalArea);
        }

        private BinaryArea() : base() { }

        [OnDeserialized]
        private void OnDeserialized()
        {
            // there are inner coordinates
            if (_binaryHoles != null)
            {
                // they are split
                if (_innerSplits != null)
                {
                    // create and fill the inner array taking splits into account
                    _innerCoordinates = new Geodetic2d[_innerSplits.Length + 1][];

                    int lastSplit = 0, totalCoordinates = 0;
                    for (var i = 0; i < _innerSplits.Length; i++)
                    {
                        _innerCoordinates[i] = new Geodetic2d[_innerSplits[i] - lastSplit];

                        // fill in the actual coordinates
                        for (var k = 0; k < _innerCoordinates[i].Length; k++)
                        {
                            _innerCoordinates[i][k] =
                                _binaryHoles[totalCoordinates++].Geodetic2dCoordinate();
                        }

                        lastSplit = _innerSplits[i];
                    }

                    _innerCoordinates[_innerCoordinates.Length - 1] =
                        new Geodetic2d[_binaryHoles.Length - lastSplit];

                    // fill last inner array
                    for (var i = 0;
                        i < _innerCoordinates[_innerCoordinates.Length - 1].Length;
                        i++)
                    {
                        _innerCoordinates[_innerCoordinates.Length - 1][i] =
                            _binaryHoles[totalCoordinates++].Geodetic2dCoordinate();
                    }
                }
                else
                {
                    // simply create and fill the inner array
                    _innerCoordinates = new Geodetic2d[1][];
                    _innerCoordinates[0] = new Geodetic2d[_binaryHoles.Length];
                    for (var i = 0; i < _binaryHoles.Length; i++)
                    {
                        _innerCoordinates[0][i] = _binaryHoles[i].Geodetic2dCoordinate();
                    }
                }
            }

            // create and fill the outer coordinates array
            _outerCoordinates = new Geodetic2d[_binaryOuters.Length];
            for (var i = 0; i < _binaryOuters.Length; i++)
            {
                _outerCoordinates[i] = _binaryOuters[i].Geodetic2dCoordinate();
            }

            _category = _binaryCategory.ToAreaCategory();
        }
    }
}