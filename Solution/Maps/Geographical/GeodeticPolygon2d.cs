using System;
using System.Collections;
using System.Collections.Generic;
using Maps.Extensions;
using Maps.Geographical.Projection;
using Maps.Geometry;

namespace Maps.Geographical
{
    /// <summary>
    /// Represents an immutable 2 dimensional geodetic polygon
    /// </summary>
    public sealed class GeodeticPolygon2d : IReadOnlyList<Geodetic2d>
    {
        /// <inheritdoc />
        public Geodetic2d this[int index] => _coordinates[index];

        /// <inheritdoc />
        public int Count => _coordinates.Length;

        /// <summary>
        /// The number of holes the polygon has
        /// </summary>
        public int HoleCount => _holes.Length;

        /// <summary>
        /// The area of the polygon in square meters
        /// </summary>
        public double Area
        {
            get
            {
                var projection = new LambertAzimuthalEqualAreaProjection(Bounds.Centre);
                var projected = new Vector2d[Count];

                // total area = area of outer - total area of holes
                for (var i = 0; i < Count; ++i)
                {
                    projected[i] = projection.Forward(_coordinates[i]).xy;
                }

                // resolve area of holes
                var area = new Polygon2d(projected).Area;
                for (var i = 0; i < HoleCount; ++i)
                {
                    area -= _holes[i].Area;
                }

                return area;
            }
        }

        /// <summary>
        /// The geodetic bounding box of the polygon
        /// </summary>
        public readonly GeodeticBox2d Bounds;

        /// <summary>
        /// The geometric polygon
        /// </summary>
        public readonly Polygon2d Polygon;

        private readonly Geodetic2d[] _coordinates;
        private readonly GeodeticPolygon2d[] _holes;

        /// <summary>
        /// Initializes a new instance of GeodeticPolygon2d
        /// </summary>
        /// <param name="coordinates">The coordinates of the polygon</param>
        /// <exception cref="ArgumentNullException">Thrown if coordinates is 
        /// null</exception>
        /// <exception cref="ArgumentException">Thrown when insufficient coordinates 
        /// given</exception>
        public GeodeticPolygon2d(IReadOnlyList<Geodetic2d> coordinates)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            if (coordinates.Count < 3)
            {
                throw new ArgumentException("Requires at least 3 coordinates",
                    nameof(coordinates));
            }

            Polygon = new Polygon2d(coordinates);

            var max = Geodetic2d.MinValue;
            var min = Geodetic2d.MaxValue;

            // copy over the coordinates, closing if needed
            var close = coordinates[0] != coordinates[coordinates.Count - 1];
            _coordinates = new Geodetic2d[close ? coordinates.Count + 1
                : coordinates.Count];
            for (var i = 0; i < coordinates.Count; ++i)
            {
                var coordinate = coordinates[i];
                _coordinates[i] = coordinate;

                max = Geodetic2d.Max(coordinate, max);
                min = Geodetic2d.Min(coordinate, min);
            }
            if (close)
            {
                var coordinate = coordinates[0];
                _coordinates[_coordinates.Length - 1] = coordinate;
            }

            Bounds = new GeodeticBox2d(min, max);
            _holes = new GeodeticPolygon2d[0];
        }

        /// <summary>
        /// Initializes a new instance of GeodeticPolygon2d
        /// </summary>
        /// <param name="coordinates">The coordinates of the polygon</param>
        /// <exception cref="ArgumentNullException">Thrown if coordinates is 
        /// null</exception>
        /// <exception cref="ArgumentException">Thrown when insufficient coordinates 
        /// given</exception>
        public GeodeticPolygon2d(IList<Geodetic2d> coordinates)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            if (coordinates.Count < 3)
            {
                throw new ArgumentException("Requires at least 3 coordinates",
                    nameof(coordinates));
            }

            Polygon = new Polygon2d(coordinates);

            var max = Geodetic2d.MinValue;
            var min = Geodetic2d.MaxValue;

            // copy over the coordinates, closing if needed
            var close = coordinates[0] != coordinates[coordinates.Count - 1];
            _coordinates = new Geodetic2d[close ? coordinates.Count + 1 
                : coordinates.Count];
            for (var i = 0; i < coordinates.Count; ++i)
            {
                var coordinate = coordinates[i];
                _coordinates[i] = coordinate;

                max = Geodetic2d.Max(coordinate, max);
                min = Geodetic2d.Min(coordinate, min);
            }
            if (close)
            {
                var coordinate = coordinates[0];
                _coordinates[_coordinates.Length - 1] = coordinate;
            }

            Bounds = new GeodeticBox2d(min, max);
            _holes = new GeodeticPolygon2d[0];
        }

        /// <summary>
        /// Initializes a new instance of GeodeticPolygon2d
        /// </summary>
        /// <param name="coordinates">The coordinates of the polygon</param>
        /// <param name="holes"></param>
        /// <exception cref="ArgumentNullException">Thrown if coordinates or holes is 
        /// null</exception>
        /// <exception cref="ArgumentException">Thrown when insufficient coordinates 
        /// given or when holes contains a null element</exception>
        public GeodeticPolygon2d(IList<Geodetic2d> coordinates, IList<GeodeticPolygon2d> holes)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            if (holes == null)
            {
                throw new ArgumentNullException(nameof(holes));
            }

            if (coordinates.Count < 3)
            {
                throw new ArgumentException("Requires at least 3 coordiante", nameof(coordinates));
            }

            var holesCount = holes.Count;
            _holes = new GeodeticPolygon2d[holesCount];
            var geometricHoles = new Polygon2d[holesCount];

            for (var i = 0; i < holesCount; ++i)
            {
                if (holes[i] == null)
                {
                    throw new ArgumentException($"Contains null element at index {i}", nameof(holes));
                }

                _holes[i] = holes[i];
                geometricHoles[i] = holes[i].Polygon;
            }

            Polygon = new Polygon2d(coordinates, geometricHoles);

            var max = Geodetic2d.MinValue;
            var min = Geodetic2d.MaxValue;

            // copy over the coordinates, closing if needed
            var close = coordinates[0] != coordinates[coordinates.Count - 1];
            _coordinates = new Geodetic2d[close ? coordinates.Count + 1
                : coordinates.Count];
            for (var i = 0; i < coordinates.Count; ++i)
            {
                var coordinate = coordinates[i];
                _coordinates[i] = coordinate;

                max = Geodetic2d.Max(coordinate, max);
                min = Geodetic2d.Min(coordinate, min);
            }
            if (close)
            {
                _coordinates[_coordinates.Length - 1] = coordinates[0];
            }

            Bounds = new GeodeticBox2d(min, max);
        }

        /// <summary>
        /// Initializes a new instance of GeodeticPolygon2d
        /// </summary>
        /// <param name="outer">The outer polygon</param>
        /// <param name="holes">Any additional holes to add</param>
        public GeodeticPolygon2d(GeodeticPolygon2d outer, IList<GeodeticPolygon2d> holes)
        {
            if (outer == null)
            {
                throw new ArgumentNullException(nameof(outer));
            }

            if (holes == null)
            {
                throw new ArgumentNullException(nameof(holes));
            }

            var holesCount = holes.Count;
            _holes = new GeodeticPolygon2d[holesCount];
            var geometricHoles = new Polygon2d[holesCount];

            for (var i = 0; i < holesCount; ++i)
            {
                if (holes[i] == null)
                {
                    throw new ArgumentException($"Contains null element at index {i}", nameof(holes));
                }

                _holes[i] = holes[i];
                geometricHoles[i] = holes[i].Polygon;
            }

            _coordinates = outer._coordinates;

            Polygon = new Polygon2d(outer.Polygon, geometricHoles);
            Bounds = outer.Bounds;
        }

        /// <summary>
        /// Initializes a new instance of GeodeticPolygon2d
        /// </summary>
        /// <param name="polygon">The geometric polygon to create from</param>
        /// <exception cref="ArgumentNullException">Thrown if polygon is null</exception>
        internal GeodeticPolygon2d(Polygon2d polygon)
        {
            if (polygon == null)
            {
                throw new ArgumentNullException(nameof(polygon));
            }

            var max = Geodetic2d.MinValue;
            var min = Geodetic2d.MaxValue;

            // create coordinates
            _coordinates = new Geodetic2d[polygon.Count];
            for (var i = 0; i < _coordinates.Length; ++i)
            {
                var coordinate = new Geodetic2d(polygon[i]);
                _coordinates[i] = coordinate;

                max = Geodetic2d.Max(coordinate, max);
                min = Geodetic2d.Min(coordinate, min);

                _coordinates[i] = coordinate;
            }

            // copy over holes
            _holes = new GeodeticPolygon2d[polygon.HoleCount];
            for (var i = 0; i < polygon.HoleCount; ++i)
            {
                _holes[i] = new GeodeticPolygon2d(polygon.Hole(i));
            }

            Polygon = polygon;
            Bounds = new GeodeticBox2d(min, max);
        }

        /// <summary>
        /// Returns the hole at the given index
        /// </summary>
        /// <param name="index">The index of the hole to return</param>
        /// <returns></returns>
        public GeodeticPolygon2d Hole(int index)
        {
            return _holes[index];
        }

        /// <summary>
        /// Attempts to combine the given polygons
        /// </summary>
        /// <param name="polygons">The polygons to combine</param>
        /// <param name="indices">The optional list of indices to track where the
        /// original polygons end up</param>
        /// <returns>A list of potentially combined polygons</returns>
        /// <exception cref="ArgumentNullException">Thrown if 
        /// <paramref name="polygons"/> is null</exception>
        /// <exception cref="ArgumentException">Thrown if any element of 
        /// <paramref name="polygons"/> is null </exception>
        public static IList<GeodeticPolygon2d> Combine(IList<GeodeticPolygon2d> polygons,
            IList<int> indices = null)
        {
            if (polygons == null)
            {
                throw new ArgumentNullException(nameof(polygons));
            }

            polygons.AssertNoNullEntries();
            var polygonCount = polygons.Count;

            if (polygonCount < 2)
            {
                return polygons;
            }

            // fill the indices array
            if (indices != null)
            {
                for (var i = 0; i < polygonCount; ++i)
                {
                    indices[i] = i;
                }
            }

            // extract the geometric polygons
            var geometric = new Polygon2d[polygonCount];
            for (var i = 0; i < polygonCount; ++i)
            {
                geometric[i] = polygons[i].Polygon;
            }

            var combined = Polygon2d.Combine(geometric, indices);

            // create the geodetic polygons from the combined geometric polygons
            var combinedCount = combined.Count;
            if (combinedCount != polygonCount)
            {
                var result = new GeodeticPolygon2d[combinedCount];
                for (var i = 0; i < combinedCount; ++i)
                {
                    result[i] = new GeodeticPolygon2d(combined[i]);
                }

                return result;
            }

            return polygons;
        }

        /// <summary>
        /// Returns the polygon, relative to the given coordinate
        /// </summary>
        /// <param name="coordinate">The coordinate to become relative to</param>
        /// <returns>A polygon relative to the given coordinate</returns>
        public GeodeticPolygon2d Relative(Geodetic2d coordinate)
        {
            return new GeodeticPolygon2d(Polygon.Relative(coordinate.Point));
        }

        /// <summary>
        /// Returns the polygon, reversing relativity to a given coordinate
        /// </summary>
        /// <param name="coordinate">The coordinate to reverse relativity to</param>
        /// <returns>A polygon relative to the given coordinate</returns>
        public GeodeticPolygon2d Absolute(Geodetic2d coordinate)
        {
            return new GeodeticPolygon2d(Polygon.Absolute(coordinate.Point));
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is GeodeticPolygon2d))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (GeodeticPolygon2d) obj;
            return Polygon.Equals(other.Polygon);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Polygon.ToString();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public IEnumerator<Geodetic2d> GetEnumerator()
        {
            for (var i = 0; i < _coordinates.Length; ++i)
            {
                yield return _coordinates[i];
            }
        }
    }
}