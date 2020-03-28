using System;
using System.Collections.Generic;
using Maps.Extensions;
using Maps.Geographical.Features;
using Maps.Geometry;

namespace Maps.Geographical
{
    /// <summary>
    /// Represents an immutable 2d axis aligned geodetic box
    /// </summary>
    public sealed class GeodeticBox2d
    {
        /// <summary>
        /// Coordinate A of the GeodeticBox2d
        /// </summary>
        public Geodetic2d A => _coordinates[0];

        /// <summary>
        /// Coordinate B of the GeodeticBox2d
        /// </summary>
        public Geodetic2d B => _coordinates[2];

        /// <summary>
        /// The centre coordinate
        /// </summary>
        public readonly Geodetic2d Centre;

        /// <summary>
        /// The geodetic strip representation
        /// </summary>
        public GeodeticLineStrip2d Strip => new GeodeticLineStrip2d(_coordinates);

        /// <summary>
        /// The geodetic polygon representation
        /// </summary>
        public GeodeticPolygon2d Polygon => new GeodeticPolygon2d(_coordinates);

        /// <summary>
        /// The geometric representation
        /// </summary>
        public readonly Box2d Box2d;

        /// <summary>
        /// Index accessor for coordinates, always clockwise
        /// </summary>
        public Geodetic2d this[int index] => _coordinates[index];

        /// <summary>
        /// The minimum longitude point
        /// </summary>
        public double MinimumLongitude => Geodetic2d.Min(A, B).Longitude;

        /// <summary>
        /// The minimum latitude point
        /// </summary>
        public double MinimumLatitude => Geodetic2d.Min(A, B).Latitude;

        /// <summary>
        /// The maximum longitude point
        /// </summary>
        public double MaximumLongitude => Geodetic2d.Max(A, B).Longitude;

        /// <summary>
        /// The maximum latitude point
        /// </summary>
        public double MaximumLatitude => Geodetic2d.Max(A, B).Latitude;

        /// <summary>
        /// Zero sized GeodeticBox2d
        /// </summary>
        public static readonly GeodeticBox2d Zero = new GeodeticBox2d(
            Geodetic3d.Zero, Geodetic3d.Zero);

        /// <summary>
        /// The 2d geodetic box that covers the whole earth
        /// </summary>
        public static readonly GeodeticBox2d World = new GeodeticBox2d(
            new Geodetic2d(-90, -180),
            new Geodetic2d(90, 180));

        private readonly IList<Geodetic2d> _coordinates;

        /// <summary>
        /// Initializes a new instance of GeodeticBox2d
        /// </summary>
        /// <param name="c0">One corner coordinate of the box</param>
        /// <param name="c1">Another corner coordinate</param>
        public GeodeticBox2d(Geodetic2d c0, Geodetic2d c1)
        {
            // create geometric representation
            Box2d = new Box2d(new Vector2d(c0), new Vector2d(c1));

            // beacause we're axis aligned, we juse use the geometric centre
            Centre = new Geodetic2d(Box2d.Centre.y, Box2d.Centre.x);

            // Box2d handles clockwiseness
            _coordinates = new []
            {
                new Geodetic2d(Box2d[0].y, Box2d[0].x),
                new Geodetic2d(Box2d[1].y, Box2d[1].x),
                new Geodetic2d(Box2d[2].y, Box2d[2].x),
                new Geodetic2d(Box2d[3].y, Box2d[3].x)
            };
        }

        /// <summary>
        /// Initializes a new instance of GeodeticBox2d
        /// </summary>
        /// <param name="c0">One corner coordinate of the box</param>
        /// <param name="c1">Another corner coordinate</param>
        public GeodeticBox2d(Geodetic3d c0, Geodetic3d c1) : 
            this(new Geodetic2d(c0.Latitude, c0.Longitude),
                 new Geodetic2d(c1.Latitude, c1.Longitude))
        {

        }

        /// <summary>
        /// Initializes a new instance of GeodeticBox2d
        /// </summary>
        /// <param name="centre">Centre coordinate a of the box</param>
        /// <param name="size">The size of the box along a given side in meters</param>
        public GeodeticBox2d(Geodetic2d centre, double size)
        {
            Centre = centre;

            // create corner coordinates, forcing clockwiseness
            _coordinates = new Geodetic2d[4];

            _coordinates[0] = Geodetic2d.Offset(centre, size * 0.5d, 
                (double)CardinalDirection.NorthWest);
            _coordinates[2] = Geodetic2d.Offset(centre, size * 0.5d, 
                (double)CardinalDirection.SouthEast);

            _coordinates[1] = new Geodetic2d(_coordinates[0].Latitude,
                _coordinates[2].Longitude);
            _coordinates[3] = new Geodetic2d(_coordinates[2].Latitude,
                _coordinates[0].Longitude);

            // create geometric representation
            Box2d = new Box2d(new Vector2d(A), new Vector2d(B));
        }

        /// <summary>
        /// Clips the given features to the tile
        /// </summary>
        /// <param name="features">The features to clip</param>
        public IList<Feature> Clip(IList<Feature> features)
        {
            if (features == null)
            {
                throw new ArgumentNullException(nameof(features));
            }

            features.AssertNoNullEntries();

            var result = new List<Feature>();

            for (var i = 0; i < features.Count; i++)
            {
                if (features[i] == null)
                {
                    throw new ArgumentException("Contains null element at " +
                        $"index {i}", nameof(features));
                }

                // clip each feature, appending each return list to the result list
                var clipped = Clip(features[i]);

                if (clipped.Count > 0)
                {
                    for (var k = 0; k < clipped.Count; k++)
                    {
                        result.Add(clipped[k]);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Clips the given feature to the tile
        /// </summary>
        /// <param name="feature">The feature to clip</param>
        public IList<Feature> Clip(Feature feature)
        {
            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature));
            }

            return feature.ClipTo(this);
        }

        /// <summary>
        /// Clips a linestrip to the box
        /// </summary>
        /// <param name="subject">The subject linestrip to clip</param>
        /// <returns>Clipped linestrips or null when not intersecting</returns>
        /// <exception cref="ArgumentNullException">Thrown if subject is null</exception>
        public IList<GeodeticLineStrip2d> Clip(GeodeticLineStrip2d subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            var clippedStrips = Box2d.Clip(subject.LineStrip);

            if (clippedStrips == null)
            {
                return new GeodeticLineStrip2d[0];
            }

            var result = new GeodeticLineStrip2d[clippedStrips.Count];

            for (var i = 0; i < clippedStrips.Count; ++i)
            {
                result[i] = new GeodeticLineStrip2d(clippedStrips[i]);
            }

            return result;
        }

        /// <summary>
        /// Clips a geodetic polygon to the box
        /// </summary>
        /// <param name="subject">The subject polygon to clip</param>
        /// <exception cref="ArgumentNullException">Thrown if subject is null</exception>
        public IList<GeodeticPolygon2d> Clip(GeodeticPolygon2d subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            var clippedPolygons = Box2d.Clip(subject.Polygon);
            var clippedGeoPolygons = new GeodeticPolygon2d[clippedPolygons.Count];

            for (var i = 0; i < clippedPolygons.Count; ++i)
            {
                clippedGeoPolygons[i] = new GeodeticPolygon2d(clippedPolygons[i]);
            }

            return clippedGeoPolygons;
        }

        /// <summary>
        /// Returns the given coordinate, clamped by the box
        /// </summary>
        /// <param name="a">The coordinate to clamp</param>
        public Geodetic2d Clamp(Geodetic2d a)
        {
            return Geodetic2d.Clamp(a, MaximumLatitude, MinimumLatitude,
                MaximumLongitude, MinimumLongitude);
        }

        /// <summary>
        /// Evaluates if the given box contains the given coordinate
        /// </summary>
        /// <param name="a">The coordinate to evaluate against</param>
        public bool Contains(Geodetic2d a)
        {
            return Box2d.Contains(a.Point);
        }

        /// <summary>
        /// Evaluates if the box contains a given box
        /// </summary>
        /// <param name="box">The box to evaluate against</param>
        /// <returns>True if contained, false if not</returns>
        public bool Contains(GeodeticBox2d box)
        {
            return Box2d.Contains(box.Box2d);
        }

        /// <summary>
        /// Evaluates if the box contains a given box
        /// </summary>
        /// <param name="box">The box to evaluate against</param>
        /// <param name="error">The allowed error, in degrees</param>
        /// <returns>True if contained, false if not</returns>
        public bool Contains(GeodeticBox2d box, double error)
        {
            return Box2d.Contains(box.Box2d, error);
        }

        /// <summary>
        /// Evaluates a diagonally expanded box
        /// </summary>
        /// <param name="meters">The distance in meters to expand</param>
        /// <returns>The expanded box</returns>
        public GeodeticBox2d Expand(double meters)
        {
            return new GeodeticBox2d(
                Geodetic2d.Offset(A, meters * 0.5d, (double)CardinalDirection.NorthWest),
                Geodetic2d.Offset(B, meters * 0.5d, (double)CardinalDirection.SouthEast));
        }

        /// <summary>
        /// Returns the given box with it's values clamped
        /// </summary>
        /// <param name="box">The box to clamp</param>
        /// <param name="maxLatitude">The maximum latitude</param>
        /// <param name="minLatitude">The minimum latitude</param>
        /// <param name="maxLongitude">The maximum longitude</param>
        /// <param name="minLongitude">The minimum longitude</param>
        public static GeodeticBox2d Clamp(GeodeticBox2d box, double maxLatitude,
            double minLatitude, double maxLongitude, double minLongitude)
        {
            var a = Geodetic2d.Clamp(box.A, maxLatitude, minLatitude, 
                maxLongitude, minLongitude);
            var b = Geodetic2d.Clamp(box.B, maxLatitude, minLatitude, 
                maxLongitude, minLongitude);

            return new GeodeticBox2d(a, b);
        }

        /// <summary>
        /// Returns the given box, absolute values clamped to the given values
        /// </summary>
        /// <param name="box">The box to clamp</param>
        /// <param name="maxLatitude">The latitude value to clamp to</param>
        /// <param name="maxLongitude">The longitude value to clamp to</param>
        public static GeodeticBox2d ClampAbs(GeodeticBox2d box, 
            double maxLatitude, double maxLongitude)
        {
            var a = Geodetic2d.ClampAbs(box.A, maxLatitude, maxLongitude);
            var b = Geodetic2d.ClampAbs(box.B, maxLatitude, maxLongitude);

            return new GeodeticBox2d(a, b);
        }

        /// <summary>
        /// Returns a box which encompasses all given coordinates
        /// </summary>
        /// <param name="coordinates">The coordinates to encompass</param>
        public static GeodeticBox2d Encompass(IList<Geodetic2d> coordinates)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            // resolve the min and max values
            var max = Geodetic2d.MinValue;
            var min = Geodetic2d.MaxValue;

            for (var i = 0; i < coordinates.Count; i++)
            {
                max = Geodetic2d.Max(coordinates[i], max);
                min = Geodetic2d.Min(coordinates[i], min);
            }

            return new GeodeticBox2d(min, max);
        }

        /// <summary>
        /// Returns a box which encompasses all given coordinates
        /// </summary>
        /// <param name="coordinates">The coordinates to encompass</param>
        public static GeodeticBox2d Encompass(IList<Geodetic3d> coordinates)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            // resolve the min and max values
            var max = Geodetic3d.MinValue;
            var min = Geodetic3d.MaxValue;

            for (var i = 0; i < coordinates.Count; i++)
            {
                max = Geodetic3d.Max(coordinates[i], max);
                min = Geodetic3d.Min(coordinates[i], min);
            }

            return new GeodeticBox2d(min, max);
        }

        /// <summary>
        /// Evaluates if the two given geodetic boxes are equal
        /// </summary>
        public static bool operator ==(GeodeticBox2d lhs, GeodeticBox2d rhs)
        {
            if (ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            if (((object)lhs == null) || ((object)rhs == null))
            {
                return false;
            }

            // box may be oriented in reverse
            return lhs.A == rhs.A && rhs.A == lhs.B ||
                   lhs.A == rhs.B && lhs.B == rhs.A;
        }

        /// <summary>
        /// Evaluates if the two given geodetic boxes are not equal
        /// </summary>
        public static bool operator !=(GeodeticBox2d lhs, GeodeticBox2d rhs)
        {
            return !(lhs == rhs);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"A{A}, B{B}";
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is GeodeticBox2d))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var box = (GeodeticBox2d) obj;

            return this == box;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Box2d.GetHashCode();
        }
    }
}
