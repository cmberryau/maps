using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Maps.Geographical;

namespace Maps
{
    /// <summary>
    /// Immutable 2 dimensional vector with single precision
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public struct Vector2d
    {
        /// <summary>
        /// The x component of the vector
        /// </summary>
        [FieldOffset(0)]
        public readonly double x;

        /// <summary>
        /// The y component of the vector
        /// </summary>
        [FieldOffset(8)]
        public readonly double y;

        /// <summary>
        /// Indexer access to components
        /// </summary>
        public double this[int index]
        {
            get
            {
                if (index == 0)
                {
                    return x;
                }

                if (index != 1)
                {
                    throw new IndexOutOfRangeException("Invalid " +
                        $"{nameof(Vector2d)} index");
                }

                return y;
            }
        }

        /// <summary>
        /// Returns the high value for a split 32bit representation
        /// </summary>
        public Vector2f High => new Vector2f((float)x, (float)y);

        /// <summary>
        /// Returns the low value for a split 32bit representation
        /// </summary>
        public Vector2f Low
        {
            get
            {
                var high = High;
                return new Vector2f((float)(x - high.x),
                                      (float)(y - high.y));
            }
        }

        /// <summary>
        /// Returns the smallest component of the vector
        /// </summary>
        public double MinComponent => Math.Min(x, y);

        /// <summary>
        /// Returns the smallest component of the vector
        /// </summary>
        public double MaxComponent => Math.Max(x, y);

        /// <summary>
        /// The magnitude of the vector
        /// </summary>
        public double Magnitude => Math.Sqrt(SqrMagnitude);

        /// <summary>
        /// The square magnitude of the vector
        /// </summary>
        public double SqrMagnitude => x * x + y * y;

        /// <summary>
        /// The vector, normalised
        /// </summary>
        public Vector2d Normalised => Normalise(this);

        /// <summary>
        /// The vector perpendicular to this vector
        /// </summary>
        public Vector2d Perpendicular => new Vector2d(-y, x);

        /// <summary>
        /// Returns a vector with 1, 1
        /// </summary>
        public static readonly Vector2d One = new Vector2d(1d, 1d);

        /// <summary>
        /// Returns a vector with 0, 0
        /// </summary>
        public static readonly Vector2d Zero = new Vector2d(0d, 0d);

        /// <summary>
        /// Returns a vector with 0, 1
        /// </summary>
        public static readonly Vector2d Up = new Vector2d(0d, 1d);

        /// <summary>
        /// Returns a vector with 0, -1
        /// </summary>
        public static readonly Vector2d Down = new Vector2d(0d, -1d);

        /// <summary>
        /// Returns a vector with 1, 0
        /// </summary>
        public static readonly Vector2d Right = new Vector2d(1d, 0d);

        /// <summary>
        /// Returns a vector with -1, 0
        /// </summary>
        public static readonly Vector2d Left = new Vector2d(-1d, 0d);

        /// <summary>
        /// Returns a vector with MaxValue, MaxValue
        /// </summary>
        public static readonly Vector2d MaxValue = new Vector2d(double.MaxValue);

        /// <summary>
        /// Returns a vector with MaxValue, MaxValue
        /// </summary>
        public static readonly Vector2d MinValue = new Vector2d(double.MinValue);

        /// <summary>
        /// Returns a vector with PositiveInfinity, PositiveInfinity
        /// </summary>
        public static readonly Vector2d PositiveInfinity = new Vector2d(double.PositiveInfinity);

        /// <summary>
        /// Returns a vector with NegativeInfinity, NegativeInfinity
        /// </summary>
        public static readonly Vector2d NegativeInfinity = new Vector2d(double.NegativeInfinity);

        /// <summary>
        /// Returns a vector with NaN, NaN
        /// </summary>
        public static readonly Vector2d NaN = new Vector2d(double.NaN);

        /// <summary>
        /// Swizzle property for XY
        /// </summary>
        public Vector2d xy => this;

        /// <summary>
        /// Swizzle property for YX
        /// </summary>
        public Vector2d yx => new Vector2d(y, x);

        /// <summary>
        /// Constructs a new vector with the given x, y components
        /// </summary>
        /// <param name="xy">The value for both x and y components</param>
        public Vector2d(double xy)
        {
            this.x = this.y = xy;
        }

        /// <summary>
        /// Constructs a new vector with the given x, y components
        /// </summary>
        /// <param name="x">The x component</param>
        /// <param name="y">The y component</param>
        public Vector2d(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Constructs a new vector from the given coordinate
        /// </summary>
        /// <param name="geodetic2d">The coordinate to construct the vector from</param>
        public Vector2d(Geodetic2d geodetic2d)
        {
            this.x = geodetic2d.Longitude;
            this.y = geodetic2d.Latitude;
        }

        /// <summary>
        /// Returns the negative of the given vector
        /// </summary>
        public static Vector2d operator -(Vector2d a)
        {
            return new Vector2d(-a.x, -a.y);
        }

        /// <summary>
        /// Returns a new vector that is the difference of the given two
        /// </summary>
        public static Vector2d operator -(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.x - b.x, a.y - b.y);
        }

        /// <summary>
        /// Returns a new vector that is the sum of the given two
        /// </summary>
        public static Vector2d operator +(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.x + b.x, a.y + b.y);
        }

        /// <summary>
        /// Returns a new vector that is the product of the given
        /// vector by the given double value
        /// </summary>
        public static Vector2d operator *(double d, Vector2d a)
        {
            return new Vector2d(a.x * d, a.y * d);
        }

        /// <summary>
        /// Returns a new vector that is the product of the given
        /// vector by the given double value
        /// </summary>
        public static Vector2d operator *(Vector2d a, double f)
        {
            return new Vector2d(a.x * f, a.y * f);
        }

        /// <summary>
        /// Returns a new vector that is the result of divison
        /// of the given vector by the given double value
        /// </summary>
        public static Vector2d operator /(double d, Vector2d a)
        {
            return new Vector2d(d / a.x, d / a.y);
        }

        /// <summary>
        /// Returns a new vector that is the result of divison
        /// of the given vector by the given double value
        /// </summary>
        public static Vector2d operator /(Vector2d a, double d)
        {
            return new Vector2d(a.x / d, a.y / d);
        }

        /// <summary>
        /// Evaluates if the two given vectors are equal
        /// </summary>
        public static bool operator ==(Vector2d lhs, Vector2d rhs)
        {
            if (lhs.x == rhs.x && lhs.y == rhs.y)
            {
                return true;
            }

            return (lhs - rhs).SqrMagnitude < Mathd.Epsilon;
        }

        /// <summary>
        /// Evaluates if the two given vectors are not equal
        /// </summary>
        public static bool operator !=(Vector2d lhs, Vector2d rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Returns a value indicating whether the specified vector or any of it's 
        /// members evaluates to not a number
        /// </summary>
        public static bool IsNaN(Vector2d a)
        {
            if (double.IsNaN(a.x) || double.IsNaN(a.y))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a new vector that is the component wise product
        /// of the two vectors
        /// </summary>
        public static Vector2d ComponentMultiply(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.x * b.x, a.y * b.y);
        }

        /// <summary>
        /// Returns the distance between the two given vectors
        /// </summary>
        public static double Distance(Vector2d a, Vector2d b)
        {
            return (a - b).Magnitude;
        }

        /// <summary>
        /// Returns the absolute vector of the given vector
        /// </summary>
        public static Vector2d Abs(Vector2d a)
        {
            return new Vector2d(Math.Abs(a.x), Math.Abs(a.y));
        }

        /// <summary>
        /// Returns a vector as the distance between components of two given vectors
        /// </summary>
        public static Vector2d ComponentDistance(Vector2d a, Vector2d b)
        {
            return Abs(a - b);
        }

        /// <summary>
        /// Returns the midpoint between two vectors
        /// </summary>
        public static Vector2d Midpoint(Vector2d a, Vector2d b)
        {
            return (a + b) * 0.5d;
        }

        /// <summary>
        /// Returns a new vector, the result of component wise scaling
        /// </summary>
        public static Vector2d Scale(Vector2d a, Vector2d b)
        {
            return new Vector2d(a.x * b.x, a.y * b.y);
        }

        /// <summary>
        /// Returns the component wise maximum of the two vectors
        /// </summary>
        public static Vector2d Max(Vector2d a, Vector2d b)
        {
            return new Vector2d(Math.Max(a.x, b.x), Math.Max(a.y, b.y));
        }

        /// <summary>
        /// Returns the component wise minimum of the two given vectors
        /// </summary>
        public static Vector2d Min(Vector2d a, Vector2d b)
        {
            return new Vector2d(Math.Min(a.x, b.x), Math.Min(a.y, b.y));
        }

        /// <summary>
        /// Returns the vector with all components raised to the power
        /// </summary>
        /// <param name="a">A Vector2d to be raised to a power</param>
        /// <param name="power">The power to raise the Vector2d to</param>
        public static Vector2d Pow(Vector2d a, double power)
        {
            return new Vector2d(Math.Pow(a.x, power), Math.Pow(a.y, power));
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[{x}]d,[{y}]d";
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is Vector2d))
            {
                return false;
            }

            var other = (Vector2d)obj;

            return this == other;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hash = x.GetHashCode();
                hash = (hash * 397) ^ y.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Returns the dot product for the given vectors
        /// dot = a.x * a.y + b.x * b.y
        /// </summary>
        public static double Dot(Vector2d a, Vector2d b)
        {
            return a.x * b.x + a.y * b.y;
        }

        /// <summary>
        /// Returns the cross product for the given vectors
        /// cross = a.x * b.y - a.y * b.x
        /// </summary>
        public static double Cross(Vector2d a, Vector2d b)
        {
            return a.x * b.y - a.y * b.x;
        }

        /// <summary>
        /// Returns the shortest angle between the two given vectors
        /// </summary>
        /// <remarks>Order is not important</remarks>
        public static double Angle(Vector2d a, Vector2d b)
        {
            return Math.Acos(Mathd.Clamp(Dot(a.Normalised, b.Normalised), -1d, 1d)) 
                * Mathd.Rad2Deg;
        }

        /// <summary>
        /// Returns the polar angle of the vector
        /// </summary>
        public double PolarAngle()
        {
            var value = 0d;

            if (Mathd.EpsilonEquals(x, 0d) && Mathd.EpsilonEquals(y, 0d))
            {
                return value;
            }

            value = Math.Atan2(y, x) * Mathd.Rad2Deg;

            return value;
        }

        /// <summary>
        /// Returns the cardinal heading of the vector
        /// </summary>
        public double CardinalHeading()
        {
            if (SqrMagnitude < Mathd.Epsilon)
            {
                return 0d;
            }

            return (90d - PolarAngle() + 360d) % 360d;
        }

        /// <summary>
        /// Returns a vector rotated counter clockwise by the given amount
        /// </summary>
        public Vector2d Rotate(double angle)
        {
            angle *= Mathd.Deg2Rad;

            var sin = Math.Sin(angle);
            var cos = Math.Cos(angle);

            return new Vector2d(cos * x - sin * y, cos * y + sin * x);
        }

        /// <summary>
        /// Normalises the vector
        /// </summary>
        public void Normalise()
        {
            if (Magnitude > Mathd.EpsilonE16)
            {
                this /= Magnitude;
            }
            else
            {
                this = Zero;
            }
        }

        /// <summary>
        /// Normalizes the vector, American style - YEEHAWWWW
        /// </summary>
        public void Normalize() => Normalise();

        /// <summary>
        /// Returns a new normalised vector from the given vector
        /// </summary>
        /// <param name="v">The vector to normalise</param>
        public static Vector2d Normalise(Vector2d v)
        {
            if (v.Magnitude > Mathd.EpsilonE16)
            {
                return v / v.Magnitude;
            }

            return Zero;
        }

        /// <summary>
        /// Returns a vector interpolated between the two given vectors
        /// at the time value given
        /// </summary>
        /// <param name="a">The starting vector</param>
        /// <param name="b">The final vector</param>
        /// <param name="t">The time value</param>
        public static Vector2d Lerp(Vector2d a, Vector2d b, double t)
        {
            t = Mathd.Clamp01(t);
            return new Vector2d(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
        }

        /// <summary>
        /// Returns true if the given point p is lies between
        /// or on either of the two given points a and b
        /// </summary>
        /// <param name="p">The point to test for</param>
        /// <param name="a">The possible starting vector</param>
        /// <param name="b">The possible end vector</param>
        public static bool LiesBetween(Vector2d p, Vector2d a, Vector2d b)
        {
            // check if p is at an endpoint
            if (p == a || p == b)
            {
                return true;
            }

            // only continue if it can be on the line ab
            var xTime = p.x - a.x / (b.x - a.x);

            if (xTime > 0 && xTime < 1)
            {
                var yTime = p.y - a.y / (b.y - a.y);

                if (yTime > 0 && yTime < 1)
                {
                    return Mathd.EpsilonEquals(xTime, yTime);
                }
            }

            return false;
        }

        /// <summary>
        /// Return the time that the given point p lies on between
        /// a and b, otherwise throw exception
        /// </summary>
        /// <param name="p">The point to test for</param>
        /// <param name="a">The possible starting vector</param>
        /// <param name="b">The possible end vector</param>
        public static double TimeBetween(Vector2d p, Vector2d a, Vector2d b)
        {
            // endpoint checks
            if (p == a)
            {
                return 0d;
            }

            if (p == b)
            {
                return 1d;
            }

            // only continue if it can be on the line ab
            var xTime = p.x - a.x / (b.x - a.x);

            if (xTime > 0 && xTime < 1)
            {
                var yTime = p.y - a.y / (b.y - a.y);

                if (yTime > 0 && yTime < 1)
                {
                    if (Mathd.EpsilonEquals(xTime, yTime))
                    {
                        return xTime;
                    }
                }
            }

            throw new ArithmeticException("Point p does not lie on line ab");
        }

        /// <summary>
        /// Evaluates if the given array of points is clockwise
        /// </summary>
        /// <param name="points">The array of points to evaluate</param>
        public static bool Clockwise(IList<Vector2d> points)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            if (points.Count < 3)
            {
                throw new ArgumentException("Less than 3 in size", nameof(points));
            }

            double result = 0;

            Vector2d v0;
            Vector2d v1;

            for (var i = 1; i < points.Count; i++)
            {                
                v1 = points[i];
                v0 = points[i - 1];

                result += (v1.x - v0.x) * (v1.y + v0.y);
            }
            
            v1 = points[0];
            v0 = points[points.Count - 1];

            // if the last point doesnt overlap the first, evaluate them also
            if (v0 != v1)
            {
                result += (v1.x - v0.x) * (v1.y + v0.y);
            }

            return result > 0;
        }

        /// <summary>
        /// Evaluates if the given array of 3d points is clockwise on the xy plane
        /// </summary>
        /// <param name="points">The array of points to evaluate</param>
        public static bool Clockwise(IList<Vector3d> points)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            if (points.Count < 3)
            {
                throw new ArgumentException("Less than 3 in size", nameof(points));
            }

            double result = 0;

            Vector3d v0;
            Vector3d v1;

            for (var i = 1; i < points.Count; i++)
            {
                v1 = points[i];
                v0 = points[i - 1];

                result += (v1.x - v0.x) * (v1.y + v0.y);
            }

            v1 = points[0];
            v0 = points[points.Count - 1];

            // if the last point doesnt overlap the first, evaluate them also
            if (v0 != v1)
            {
                result += (v1.x - v0.x) * (v1.y + v0.y);
            }

            return result > 0;
        }

        /// <summary>
        /// Evaluates if the given points are convex on the xy plane
        /// Based on this method: http://math.stackexchange.com/questions/51310/equation-to-find-if-a-set-of-vertices-form-a-concave-shape
        /// </summary>
        /// <param name="points">The points to evaluate</param>
        public static bool Convex(IList<Vector2d> points)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            if (points.Count < 3)
            {
                throw new ArgumentException("Cannot evaluate less than 3 " +
                    "points", nameof(points));
            }

            // cannot be concave
            if (points.Count == 3)
            {
                return true;
            }

            var sign = Math.Sign(Vector2d.Cross(points[0] -
                                                points[1],
                                                points[2] -
                                                points[1]));

            for (var i = 2; i < points.Count - 1; i++)
            {
                if (Math.Sign(Vector2d.Cross(points[i - 1] -
                                             points[i],
                                             points[i + 1] -
                                             points[i])) != sign)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Evaluates if the given 3d points are convex when projected onto
        /// the XY plane
        /// Based on this method: http://math.stackexchange.com/questions/51310/equation-to-find-if-a-set-of-vertices-form-a-concave-shape
        /// </summary>
        /// <param name="points">The points to evaluate</param>
        public static bool Convex(IList<Vector3d> points)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            if (points.Count < 3)
            {
                throw new ArgumentException("Cannot evaluate less than 3 " +
                    "points", nameof(points));
            }

            // cannot be concave
            if (points.Count == 3)
            {
                return true;
            }

            var sign = Math.Sign(Vector3d.Cross(points[0] -
                                                points[1],
                                                points[2] -
                                                points[1]).z);

            for (var i = 2; i < points.Count - 1; i++)
            {
                if (Math.Sign(Vector3d.Cross(points[i - 1] -
                                             points[i],
                                             points[i + 1] -
                                             points[i]).z) != sign)
                {
                    return false;
                }
            }

            return true;
        }
    }
}