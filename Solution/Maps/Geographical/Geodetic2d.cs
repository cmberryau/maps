using System;
using System.Collections.Generic;

namespace Maps.Geographical
{
    /// <summary>
    /// Represents a 2d geodetic coordinate
    /// </summary>
    public struct Geodetic2d
    {
        /// <summary>
        /// The latitude of this coordinate
        /// </summary>
        public double Latitude => Point.y;

        /// <summary>
        /// The longitude of this coordinate
        /// </summary>
        public double Longitude => Point.x;

        /// <summary>
        /// The geometric point representation of the coordinate
        /// </summary>
        public readonly Vector2d Point;

        /// <summary>
        /// Returns a Geodetic2d coordinate with (double.MaxValue,
        /// double.MaxValue)
        /// </summary>
        public static readonly Geodetic2d MaxValue = new Geodetic2d(
            double.MaxValue, double.MaxValue);

        /// <summary>
        /// Returns a Geodetic2d coordinate with (double.MinValue, 
        /// double.MinValue)
        /// </summary>
        public static readonly Geodetic2d MinValue = new Geodetic2d(double.MinValue, 
            double.MinValue);

        /// <summary>
        /// Returns a Geodetic2d with (double.NaN, double.NaN)
        /// </summary>
        public static readonly Geodetic2d NaN = new Geodetic2d(double.NaN, double.NaN);

        /// <summary>
        /// Returns a Geodetic2d coordinate with (90, 0)
        /// </summary>
        public static readonly Geodetic2d NorthPole = new Geodetic2d(90d, 0d);

        /// <summary>
        /// Returns a Geodetic2d coordinate with (-90, 0)
        /// </summary>
        public static readonly Geodetic2d SouthPole = new Geodetic2d(-90d, 0d);

        /// <summary>
        /// Returns a Geodetic2d coordinate with (0, 0)
        /// </summary>
        public static readonly Geodetic2d Meridian = new Geodetic2d(0d, 0d);

        /// <summary>
        /// Returns a Geodetic2d coordinate with (0, 90)
        /// </summary>
        public static readonly Geodetic2d Meridian90 = new Geodetic2d(0d, 90d);

        /// <summary>
        /// Returns a Geodetic2d coordinate with (0, -90)
        /// </summary>
        public static readonly Geodetic2d MeridianNegative90 = new Geodetic2d(0d, -90d);

        /// <summary>
        /// Returns a Geodetic2d coordinate with (0, 180)
        /// </summary>
        public static readonly Geodetic2d Meridian180 = new Geodetic2d(0d, 180d);

        /// <summary>
        /// Returns a Geodetic2d coordinate with (0, -180)
        /// </summary>
        public static readonly Geodetic2d MeridianNegative180 = new Geodetic2d(0d, 
            -180d);

        /// <summary>
        /// Returns a Geodetic2d coordinate with (90, 180)
        /// </summary>
        public static readonly Geodetic2d PositiveExtent = new Geodetic2d(90d, 180d);

        /// <summary>
        /// Returns a Geodetic2d coordinate with (-90, -180)
        /// </summary>
        public static readonly Geodetic2d NegativeExtent = new Geodetic2d(-90d, -180d);

        /// <summary>
        /// Initializes a new instance of Geodetic2d
        /// </summary>
        /// <param name="longitude">The longitude of the new coordinate</param>
        /// <param name="latitude">The latitude of the new coordinate</param>
        public Geodetic2d(double latitude, double longitude)
        {
            Point = new Vector2d(longitude, latitude);
        }

        /// <summary>
        /// Initializes a new instance of Geodetic2d
        /// </summary>
        /// <param name="point">The vector to create the coordinate from</param>
        public Geodetic2d(Vector2d point)
        {
            Point = point;
        }

        /// <summary>
        /// Initializes a new instance of Geodetic2d in the order latitude, longitude
        /// </summary>
        /// <param name="coordinate">The array to create the coordinate from</param>
        public Geodetic2d(double[] coordinate)
        {
            if (coordinate == null)
            {
                throw new ArgumentNullException(nameof(coordinate));
            }

            if (coordinate.Length != 2)
            {
                throw new ArgumentException("Must have exactly 2 elements", nameof(coordinate));
            }

            Point = new Vector2d(coordinate[1], coordinate[0]);
        }

        /// <summary>
        /// Evaluates if the two given 2d geodetic coordinates are equal
        /// </summary>
        /// <param name="lhs">The left hand coordinate</param>
        /// <param name="rhs">The right hand coordinate</param>
        public static bool operator ==(Geodetic2d lhs, Geodetic2d rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Evaluates if the two given 2d geodetic coordinates are not equal
        /// </summary>
        /// <param name="lhs">The left hand coordinate</param>
        /// <param name="rhs">The right hand coordinate</param>
        public static bool operator !=(Geodetic2d lhs, Geodetic2d rhs)
        {
            return !lhs.Equals(rhs);
        }

        /// <summary>
        /// Returns the sum of the two coordinates
        /// </summary>
        /// <param name="lhs">The left hand coordinate</param>
        /// <param name="rhs">The right hand coordinate</param>
        public static Geodetic2d operator +(Geodetic2d lhs, Geodetic2d rhs)
        {
            return new Geodetic2d(lhs.Latitude + rhs.Latitude,
                lhs.Longitude + rhs.Longitude);
        }

        /// <summary>
        /// Returns the difference of the two coordinates
        /// </summary>
        /// <param name="lhs">The left hand coordinate</param>
        /// <param name="rhs">The right hand coordinate</param>
        public static Geodetic2d operator -(Geodetic2d lhs, Geodetic2d rhs)
        {
            return new Geodetic2d(lhs.Latitude - rhs.Latitude, 
                lhs.Longitude - rhs.Longitude);
        }

        /// <summary>
        /// Returns the coordinate offset by the given distance in the given heading
        /// See http://williams.best.vwh.net/avform.htm#LL
        /// </summary>
        /// <param name="a">The coordinate to offset</param>
        /// <param name="distance">The distance to offset by in meters</param>
        /// <param name="heading">The angle (from north)</param>
        /// <param name="radius">The optional radius to provide, defaults to earth's average
        /// radius</param>
        public static Geodetic2d Offset(Geodetic2d a, double distance, 
            double heading,  double radius = Mathd.RAverage)
        {
            if (distance < Mathd.Epsilon)
            {
                return a;
            }

            var d = distance / radius;
            var sinLat = Math.Sin(a.Latitude * Mathd.Deg2Rad);
            var cosLat = Math.Cos(a.Latitude * Mathd.Deg2Rad);
            var sinD = Math.Sin(d);
            var cosD = Math.Cos(d);
            var cosHeading = Math.Cos(heading * Mathd.Deg2Rad);
            var sinHeading = Math.Sin(heading * Mathd.Deg2Rad);

            var newLat = Math.Asin(sinLat * cosD + cosLat * sinD * cosHeading) * Mathd.Rad2Deg;
            var deltaLongitude = Math.Atan2(sinHeading * sinD * cosLat, 
                cosD - sinLat * Math.Sin(newLat * Mathd.Deg2Rad)) * Mathd.Rad2Deg;

            var newLon = a.Longitude + deltaLongitude + 180d % 360d - 180d;

            return new Geodetic2d(newLat, newLon);
        }

        /// <summary>
        /// Returns the distance between two coordinates in meters, uses haversine formula
        /// See http://williams.best.vwh.net/avform.htm#Dist,
        ///     https://en.wikipedia.org/wiki/Haversine_formula
        /// </summary>
        /// <param name="a">Coordinate a to evaluate</param>
        /// <param name="b">Coordinate b to evaluate</param>
        /// <param name="radius">The optional radius to provide, defaults to earth's average
        /// radius</param>
        public static double Distance(Geodetic2d a, Geodetic2d b, double radius = Mathd.RAverage)
        {
            if (a == b)
            {
                return 0d;
            }

            var latDeltaRads = (b.Latitude - a.Latitude) * Mathd.Deg2Rad;
            var lonDeltaRads = (b.Longitude - a.Longitude) * Mathd.Deg2Rad;

            var compa = Math.Pow(Math.Sin(latDeltaRads * 0.5d), 2d);
            var compb = Math.Pow(Math.Sin(lonDeltaRads * 0.5d), 2d);

            return 2 * radius * Math.Asin(Math.Sqrt(compa + Math.Cos(a.Latitude * Mathd.Deg2Rad) *
                Math.Cos(b.Latitude * Mathd.Deg2Rad) * compb));
        }

        /// <summary>
        /// Returns the Course between two coordinates (angle clockwise from true North)
        /// See http://williams.best.vwh.net/avform.htm#Crs
        /// </summary>
        /// <param name="a">The starting coordinate</param>
        /// <param name="b">The final coordinate</param>
        public static double Course(Geodetic2d a, Geodetic2d b, bool overflow = false)
        {
            if (a == b)
            {
                return 0d;
            }

            var alatCos = Math.Cos(a.Latitude * Mathd.Deg2Rad);

            if (alatCos < Mathd.Epsilon)
            {
                if (a.Latitude > 0)
                {
                    return 180d;
                }

                return 360d;
            }

            var cosBLat = Math.Cos(b.Latitude * Mathd.Deg2Rad);
            var compa = Math.Sin((a.Longitude - b.Longitude) * Mathd.Deg2Rad) * cosBLat;
            var compb = Math.Cos(a.Latitude * Mathd.Deg2Rad) * Math.Sin(b.Latitude * Mathd.Deg2Rad)
                - Math.Sin(a.Latitude * Mathd.Deg2Rad) * cosBLat * 
                Math.Cos((a.Longitude - b.Longitude) * Mathd.Deg2Rad);

            var result = Math.Abs(Math.Atan2(compa, compb) * Mathd.Rad2Deg - 360d);

            if (!overflow)
            {
                result %= 360d;
            }
            
            return result;
        }

        /// <summary>
        /// Returns the midpoint on the great circle between two coordinates
        /// </summary>
        /// <param name="a">The starting coordinate</param>
        /// <param name="b">The final coordinate</param>
        public static Geodetic2d Midpoint(Geodetic2d a, Geodetic2d b)
        {
            return Lerp(a, b, 0.5d);
        }

        /// <summary>
        /// Returns a coordinate on the great circle between two coordinates
        /// at the time value given
        /// See http://williams.best.vwh.net/avform.htm#Intermediate
        /// </summary>
        /// <param name="a">The starting coordinate</param>
        /// <param name="b">The final coordinate</param>
        /// <param name="t">The time value</param>
        public static Geodetic2d Lerp(Geodetic2d a, Geodetic2d b, double t)
        {
            if (a == b)
            {
                return a;
            }

            t = Mathd.Clamp01(t);
            var d = Distance(a, b, 1d);

            var A = Math.Sin((1 - t) * d) / Math.Sin(d);
            var B = Math.Sin(t * d) / Math.Sin(d);

            var aLatCos = Math.Cos(a.Latitude * Mathd.Deg2Rad);
            var aLonCos = Math.Cos(a.Longitude * Mathd.Deg2Rad);
            var aLatSin = Math.Sin(a.Latitude * Mathd.Deg2Rad);
            var aLonSin = Math.Sin(a.Longitude * Mathd.Deg2Rad);

            var bLatCos = Math.Cos(b.Latitude * Mathd.Deg2Rad);
            var bLonCos = Math.Cos(b.Longitude * Mathd.Deg2Rad);
            var bLatSin = Math.Sin(b.Latitude * Mathd.Deg2Rad);
            var bLonSin = Math.Sin(b.Longitude * Mathd.Deg2Rad);

            var x = A * aLatCos * aLonCos + B * bLatCos * bLonCos;
            var y = A * aLatCos * aLonSin + B * bLatCos * bLonSin;
            var z = A * aLatSin + B * bLatSin;

            return new Geodetic2d(Math.Atan2(z, Math.Sqrt(Math.Pow(x, 2d) + Math.Pow(y, 2d))) 
                                  * Mathd.Rad2Deg, Math.Atan2(y, x) * Mathd.Rad2Deg);
        }

        /// <summary>
        /// Returns the component wise maximum of the two coordinates
        /// </summary>
        /// <param name="a">The first coordinate</param>
        /// <param name="b">The second coordinate</param>
        public static Geodetic2d Max(Geodetic2d a, Geodetic2d b)
        {
            return new Geodetic2d(Vector2d.Max(a.Point, b.Point));
        }

        /// <summary>
        /// Returns the component wise minimum of the two coordinates
        /// </summary>
        /// <param name="a">The first coordinate</param>
        /// <param name="b">The second coordinate</param>
        public static Geodetic2d Min(Geodetic2d a, Geodetic2d b)
        {
            return new Geodetic2d(Vector2d.Min(a.Point, b.Point));
        }

        /// <summary>
        /// Returns the given coordinate, clamped to the given values
        /// </summary>
        /// <param name="a">The coordinate to clamp</param>
        /// <param name="maxLatitude">The maximum latitude</param>
        /// <param name="minLatitude">The minimum latitude</param>
        /// <param name="maxLongitude">The maximum longitude</param>
        /// <param name="minLongitude">The minimum longitude</param>
        public static Geodetic2d Clamp(Geodetic2d a, double maxLatitude, double minLatitude,
            double maxLongitude, double minLongitude)
        {
            return new Geodetic2d(Mathd.Clamp(a.Latitude, minLatitude, maxLatitude),
                                  Mathd.Clamp(a.Longitude, minLongitude, maxLongitude));
        }

        /// <summary>
        /// Returns the given coordinate, clamped to the given values
        /// </summary>
        /// <param name="a">The coordinate to clamp</param>
        /// <param name="maxAbsLatitude">The maximum absolute latitude</param>
        /// <param name="maxAbsLongitude">The maximum absolute latitude</param>
        public static Geodetic2d ClampAbs(Geodetic2d a, double maxAbsLatitude,
            double maxAbsLongitude)
        {
            return new Geodetic2d(Mathd.Clamp(a.Latitude, -maxAbsLatitude, maxAbsLatitude),
                                  Mathd.Clamp(a.Longitude, -maxAbsLongitude, maxAbsLongitude));
        }

        /// <summary>
        /// Evaluates if the given coordinates are convex
        /// Based on this method: http://math.stackexchange.com/questions/51310/equation-to-find-if-a-set-of-vertices-form-a-concave-shape
        /// </summary>
        /// <param name="coordinates">The coordinates to evaluate</param>
        public static bool Convex(IList<Geodetic2d> coordinates)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            if (coordinates.Count < 3)
            {
                throw new ArgumentException("Cannot evaluate less than 3 " +
                    "coordinates", nameof(coordinates));
            }

            // cannot be concave
            if (coordinates.Count == 3)
            {
                return true;
            }

            var sign = Math.Sign(Vector2d.Cross(coordinates[0].Point - 
                                                coordinates[1].Point,
                                                coordinates[2].Point - 
                                                coordinates[1].Point));

            for (var i = 2; i < coordinates.Count - 1; ++i)
            {
                if (Math.Sign(Vector2d.Cross(coordinates[i - 1].Point -
                                             coordinates[i].Point,
                                             coordinates[i + 1].Point -
                                             coordinates[i].Point)) != sign)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Evaluates if the Geodetic2d is equal to the given Geodetic2d
        /// </summary>
        /// <param name="other">The Geodetic2d to evaluate</param>
        public bool Equals(Geodetic2d other)
        {
            return Point.Equals(other.Point);
        }

        /// <summary>
        /// Evaluates if the Geodetic2d is equal to the given object
        /// </summary>
        /// <param name="obj">The object to evaluate against</param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Geodetic2d && Equals((Geodetic2d)obj);
        }

        /// <summary>
        /// Returns the hash code of the Geodetic2d
        /// </summary>
        public override int GetHashCode()
        {
            return Point.GetHashCode();
        }

        /// <summary>
        /// Returns a string representation of the 2d geodetic coordinate
        /// </summary>
        public override string ToString()
        {
            return $"ϕ[{Latitude}]d,θ[{Longitude}]d";
        }
    }
}