using System;

namespace Maps.Geographical
{
    /// <summary>
    /// Represents a 3d geodetic coordinate
    /// </summary>
    public struct Geodetic3d
    {
        /// <summary>
        /// The height of this coordinate
        /// </summary>
        public readonly double Height;

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
        /// 2D representation, same minus the height
        /// </summary>
        public Geodetic2d Geodetic2d => new Geodetic2d(Point.y, Point.x);

        /// <summary>
        /// Returns a Geodetic3d coordinate with (0d, 0d, 0d)
        /// </summary>
        public static readonly Geodetic3d Zero = new Geodetic3d(0d, 0d, 0d);

        /// <summary>
        /// Returns a Geodetic3d coordinate with (double.MaxValue,
        /// double.MaxValue)
        /// </summary>
        public static readonly Geodetic3d MaxValue = new Geodetic3d(double.MaxValue, 
            double.MaxValue, double.MaxValue);

        /// <summary>
        /// Returns a Geodetic3d coordinate with (double.MinValue, 
        /// double.MinValue)
        /// </summary>
        public static readonly Geodetic3d MinValue = new Geodetic3d(double.MinValue, 
            double.MinValue, double.MinValue);

        /// <summary>
        /// Returns a Geodetic2d with (double.NaN, double.NaN, double.NaN)
        /// </summary>
        public static readonly Geodetic3d NaN = new Geodetic3d(double.NaN, double.NaN, 
            double.NaN);

        /// <summary>
        /// Initializes a new instance of Geodetic3d
        /// </summary>
        /// <param name="latitude">The latitude of the new coordinate</param>
        /// <param name="longitude">The longitude of the new coordinate</param>
        /// <param name="height">The height of the new coordinate</param>
        public Geodetic3d(double latitude, double longitude, double height) 
        {
            Point = new Vector2d(longitude, latitude);
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of Geodetic3d
        /// </summary>
        /// <param name="point">The vector to create the coordinate from</param>
        /// <param name="height">The height of the new coordinate</param>
        public Geodetic3d(Vector2d point, double height) 
        {
            Point = point;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of Geodetic3d
        /// </summary>
        /// <param name="coordinate"></param>
        /// <param name="height"></param>
        public Geodetic3d(Geodetic2d coordinate, double height)
        {
            Point = new Vector2d(coordinate.Longitude, coordinate.Latitude);
            Height = height;
        }

        /// <summary>
        /// Evaluates if the two given 3d geodetic coordinates are equal
        /// </summary>
        public static bool operator ==(Geodetic3d lhs, Geodetic3d rhs)
        {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Evaluates if the two given 3d geodetic coordinates are not equal
        /// </summary>
        public static bool operator !=(Geodetic3d lhs, Geodetic3d rhs)
        {
            return !lhs.Equals(rhs);
        }

        /// <summary>
        /// Returns the component wise maximum of the two coordinates
        /// </summary>
        /// <param name="a">The first coordinate</param>
        /// <param name="b">The second coordinate</param>
        public static Geodetic3d Max(Geodetic3d a, Geodetic3d b)
        {
            return new Geodetic3d(Vector2d.Max(a.Point, b.Point), 
                Math.Max(a.Height, b.Height));
        }

        /// <summary>
        /// Returns the component wise minimum of the two coordinates
        /// </summary>
        /// <param name="a">The first coordinate</param>
        /// <param name="b">The second coordinate</param>
        public static Geodetic3d Min(Geodetic3d a, Geodetic3d b)
        {
            return new Geodetic3d(Vector2d.Min(a.Point, b.Point),
                Math.Min(a.Height, b.Height));
        }

        /// <summary>
        /// Evaluates if the Geodetic3d is equal to the given Geodetic3d
        /// </summary>
        /// <param name="other">The Geodetic3d to evaluate</param>
        public bool Equals(Geodetic3d other)
        {
            return Height.Equals(other.Height) && Point.Equals(other.Point);
        }

        /// <summary>
        /// Evaluates if the Geodetic3d is equal to the given object
        /// </summary>
        /// <param name="obj">The object to evaluate against</param>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Geodetic3d && Equals((Geodetic3d)obj);
        }

        /// <summary>
        /// Returns the hash code of the Geodetic2d
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Height.GetHashCode() * 397) ^ Point.GetHashCode();
            }
        }

        /// <summary>
        /// Returns a string representation of the 2d geodetic coordinate
        /// </summary>
        public override string ToString()
        {
            return $"ϕ[{Latitude}]d,θ[{Longitude}]d,r[{Height}]d";
        }
    }
}