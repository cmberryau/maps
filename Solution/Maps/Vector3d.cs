using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Maps
{
    public static class Vector3dExtensions
    {
        public static IList<Vector2d> xy(this IList<Vector3d> points)
        {
            var pointCount = points.Count;
            var xyPoints = new Vector2d[pointCount];
            for (var i = 0; i < pointCount; ++i)
            {
                xyPoints[i] = points[i].xy;
            }

            return xyPoints;
        }

        public static IList<Vector2d> xz(this IList<Vector3d> points)
        {
            var pointCount = points.Count;
            var xzPoints = new Vector2d[pointCount];
            for (var i = 0; i < pointCount; ++i)
            {
                xzPoints[i] = points[i].xz;
            }

            return xzPoints;
        }

        public static IList<Vector2d> yx(this IList<Vector3d> points)
        {
            var pointCount = points.Count;
            var yxPoints = new Vector2d[pointCount];
            for (var i = 0; i < pointCount; ++i)
            {
                yxPoints[i] = points[i].yx;
            }

            return yxPoints;
        }

        public static IList<Vector2d> yz(this IList<Vector3d> points)
        {
            var pointCount = points.Count;
            var yzPoints = new Vector2d[pointCount];
            for (var i = 0; i < pointCount; ++i)
            {
                yzPoints[i] = points[i].yz;
            }

            return yzPoints;
        }

        public static IList<Vector2d> zx(this IList<Vector3d> points)
        {
            var pointCount = points.Count;
            var zxPoints = new Vector2d[pointCount];
            for (var i = 0; i < pointCount; ++i)
            {
                zxPoints[i] = points[i].zx;
            }

            return zxPoints;
        }

        public static IList<Vector2d> zy(this IList<Vector3d> points)
        {
            var pointCount = points.Count;
            var zyPoints = new Vector2d[pointCount];
            for (var i = 0; i < pointCount; ++i)
            {
                zyPoints[i] = points[i].zy;
            }

            return zyPoints;
        }
    }

    /// <summary>
    /// Immutable 3 dimensional vector with double precision
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public struct Vector3d
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
        /// The z component of the vector
        /// </summary>
        [FieldOffset(16)]
        public readonly double z;

        /// <summary>
        /// Indexer access to components
        /// </summary>
        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    case 2:
                        return z;
                    default:
                        throw new IndexOutOfRangeException("Invalid " +
                            $"{nameof(Vector3d)} index");
                }
            }
        }

        /// <summary>
        /// Returns the high value for a split 32bit representation
        /// </summary>
        public Vector3f High => new Vector3f((float)x, (float)y, (float)z);

        /// <summary>
        /// Returns low high value for a split 32bit representation
        /// </summary>
        public Vector3f Low
        {
            get
            {
                var high = High;
                return new Vector3f((float)(x - high.x),
                                      (float)(y - high.y),
                                      (float)(z - high.z));
            }
        }

        /// <summary>
        /// Returns the smallest component of the vector
        /// </summary>
        public double MinComponent => Math.Min(x, Math.Min(y, z));

        /// <summary>
        /// Returns the smallest component of the vector
        /// </summary>
        public double MaxComponent => Math.Max(x, Math.Max(y, z));

        /// <summary>
        /// The magnitude of the vector
        /// </summary>
        public double Magnitude => Math.Sqrt(SqrMagnitude);

        /// <summary>
        /// The square magnitude of the vector
        /// </summary>
        public double SqrMagnitude => x * x + y * y + z * z;

        /// <summary>
        /// The vector, normalised
        /// </summary>
        public Vector3d Normalised => Normalise(this);

        /// <summary>
        /// Returns a vector with 1, 1, 1
        /// </summary>
        public static readonly Vector3d One = new Vector3d(1d);

        /// <summary>
        /// Returns a vector with 0, 0, 0
        /// </summary>
        public static readonly Vector3d Zero = new Vector3d(0d);

        /// <summary>
        /// Returns a vector with 0, 1, 0
        /// </summary>
        public static readonly Vector3d Up = new Vector3d(0d, 1d, 0d);

        /// <summary>
        /// Returns a vector with 0, -1, 0
        /// </summary>
        public static readonly Vector3d Down = new Vector3d(0d, -1d, 0d);

        /// <summary>
        /// Returns a vector with 1, 0, 0
        /// </summary>
        public static readonly Vector3d Right = new Vector3d(1d, 0d, 0d);

        /// <summary>
        /// Returns a vector with -1, 0, 0
        /// </summary>
        public static readonly Vector3d Left = new Vector3d(-1d, 0d, 0d);

        /// <summary>
        /// Returns a vector with 0, 0, 1
        /// </summary>
        public static readonly Vector3d Forward = new Vector3d(0d, 0d, 1d);

        /// <summary>
        /// Returns a vector with 0, 0, -1
        /// </summary>
        public static readonly Vector3d Back = new Vector3d(0d, 0d, -1d);

        /// <summary>
        /// Returns a vector with MaxValue, MaxValue, MaxValue
        /// </summary>
        public static readonly Vector3d MaxValue = new Vector3d(double.MaxValue);

        /// <summary>
        /// Returns a vector with MinValue, MinValue
        /// </summary>
        public static readonly Vector3d MinValue = new Vector3d(double.MinValue);

        /// <summary>
        /// Returns a vector with PositiveInfinity, PositiveInfinity, PositiveInfinity
        /// </summary>
        public static readonly Vector3d PositiveInfinity = new Vector3d(double.PositiveInfinity);

        /// <summary>
        /// Returns a vector with NegativeInfinity, NegativeInfinity, NegativeInfinity
        /// </summary>
        public static readonly Vector3d NegativeInfinity = new Vector3d(double.NegativeInfinity);

        /// <summary>
        /// Returns a vector with NaN, NaN, NaN
        /// </summary>
        public static readonly Vector3d NaN = new Vector3d(double.NaN);

        /// <summary>
        /// Swizzle property for XY
        /// </summary>
        public Vector2d xy => new Vector2d(x, y);

        /// <summary>
        /// Swizzle property for XZ
        /// </summary>
        public Vector2d xz => new Vector2d(x, z);

        /// <summary>
        /// Swizzle property for YX
        /// </summary>
        public Vector2d yx => new Vector2d(y, x);

        /// <summary>
        /// Swizzle property for YZ
        /// </summary>
        public Vector2d yz => new Vector2d(y, z);

        /// <summary>
        /// Swizzle property for ZX
        /// </summary>
        public Vector2d zx => new Vector2d(z, x);

        /// <summary>
        /// Swizzle property for ZY
        /// </summary>
        public Vector2d zy => new Vector2d(z, y);

        /// <summary>
        /// Swizzle property for XYZ
        /// </summary>
        public Vector3d xyz => this;

        /// <summary>
        /// Swizzle property for XZY
        /// </summary>
        public Vector3d xzy => new Vector3d(x, z, y);

        /// <summary>
        /// Swizzle property for YXZ
        /// </summary>
        public Vector3d yxz => new Vector3d(y, x, z);

        /// <summary>
        /// Swizzle property for YZX
        /// </summary>
        public Vector3d yzx => new Vector3d(y, z, x);

        /// <summary>
        /// Swizzle property for ZYX
        /// </summary>
        public Vector3d zyx => new Vector3d(z, y, x);

        /// <summary>
        /// Swizzle property for ZXY
        /// </summary>
        public Vector3d zxy => new Vector3d(z, x, y);

        /// <summary>
        /// Constructs a new vector with the given x, y and z components
        /// </summary>
        /// <param name="xyz">The value for the x, y and z components</param>
        public Vector3d(double xyz)
        {
            this.x = this.y = this.z = xyz;
        }

        /// <summary>
        /// Constructs a new vector with the given x, y and z components
        /// </summary>
        /// <param name="x">The x component</param>
        /// <param name="y">The y component</param>
        /// <param name="z">The z component</param>
        public Vector3d(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Constructs a new vector with the given 2d vector x and y components
        /// </summary>
        /// <param name="vector">The 2d vector to create from</param>
        public Vector3d(Vector2d vector)
        {
            this.x = vector.x;
            this.y = vector.y;
            this.z = 0d;
        }

        /// <summary>
        /// Returns the negative of the given vector
        /// </summary>
        public static Vector3d operator -(Vector3d a)
        {
            return new Vector3d(-a.x, -a.y, -a.z);
        }

        /// <summary>
        /// Returns a new vector that is the difference of the given two
        /// </summary>
        public static Vector3d operator -(Vector3d a, Vector3d b)
        {
            return new Vector3d(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        /// <summary>
        /// Returns a new vector that is the sum of the given two
        /// </summary>
        public static Vector3d operator +(Vector3d a, Vector3d b)
        {
            return new Vector3d(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        /// <summary>
        /// Returns a new vector that is the product of the given
        /// vector by the given double value
        /// </summary>
        public static Vector3d operator *(double d, Vector3d a)
        {
            return new Vector3d(a.x * d, a.y * d, a.z * d);
        }

        /// <summary>
        /// Returns a new vector that is the product of the given
        /// vector by the given double value
        /// </summary>
        public static Vector3d operator *(Vector3d a, double d)
        {
            return new Vector3d(a.x * d, a.y * d, a.z * d);
        }

        /// <summary>
        /// Returns a new vector that is the result of divison
        /// of the given vector by the given double value
        /// </summary>
        public static Vector3d operator /(double d, Vector3d a)
        {
            return new Vector3d(d / a.x, d / a.y, d / a.z);
        }

        /// <summary>
        /// Returns a new vector that is the result of divison
        /// of the given vector by the given double value
        /// </summary>
        public static Vector3d operator /(Vector3d a, double d)
        {
            return new Vector3d(a.x / d, a.y / d, a.z / d);
        }

        /// <summary>
        /// Evaluates if the two given vectors are equal
        /// </summary>
        public static bool operator ==(Vector3d lhs, Vector3d rhs)
        {
            if (lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z)
            {
                return true;
            }

            return (lhs - rhs).SqrMagnitude < Mathd.Epsilon;
        }

        /// <summary>
        /// Evaluates if the two given vectors are not equal
        /// </summary>
        public static bool operator !=(Vector3d lhs, Vector3d rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Returns a value indicating whether the specified vector or any of it's 
        /// members evaluates to not a number
        /// </summary>
        public static bool IsNaN(Vector3d a)
        {
            if (double.IsNaN(a.x) || double.IsNaN(a.y) || double.IsNaN(a.z))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a new vector that is the component wise product
        /// of the two vectors
        /// </summary>
        public static Vector3d ComponentMultiply(Vector3d a, Vector3d b)
        {
            return new Vector3d(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        /// <summary>
        /// Returns the distance between the two given vectors
        /// </summary>
        public static double Distance(Vector3d a, Vector3d b)
        {
            return (a - b).Magnitude;
        }

        /// <summary>
        /// Returns the absolute vector of the given vector
        /// </summary>
        public static Vector3d Abs(Vector3d a)
        {
            return new Vector3d(Math.Abs(a.x), Math.Abs(a.y), Math.Abs(a.z));
        }

        /// <summary>
        /// Returns a vector as the distance between components of two given vectors
        /// </summary>
        public static Vector3d ComponentDistance(Vector3d a, Vector3d b)
        {
            return Abs(a - b);
        }

        /// <summary>
        /// Returns the midpoint between two vectors
        /// </summary>
        public static Vector3d Midpoint(Vector3d a, Vector3d b)
        {
            return (a + b) * 0.5d;
        }

        /// <summary>
        /// Returns a new vector, the result of component wise scaling
        /// </summary>
        public static Vector3d Scale(Vector3d a, Vector3d b)
        {
            return new Vector3d(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        /// <summary>
        /// Returns the component wise maximum of the two vectors
        /// </summary>
        public static Vector3d Max(Vector3d a, Vector3d b)
        {
            return new Vector3d(Math.Max(a.x, b.x), Math.Max(a.y, b.y), Math.Max(a.z, b.z));
        }

        /// <summary>
        /// Returns the component wise minimum of the two given vectors
        /// </summary>
        public static Vector3d Min(Vector3d a, Vector3d b)
        {
            return new Vector3d(Math.Min(a.x, b.x), Math.Min(a.y, b.y), Math.Min(a.z, b.z));
        }

        /// <summary>
        /// Returns the vector with all components raised to the power
        /// </summary>
        /// <param name="a">A Vector3d to be raised to a power</param>
        /// <param name="power">The power to raise the Vector3d to</param>
        public static Vector3d Pow(Vector3d a, double power)
        {
            return new Vector3d(Math.Pow(a.x, power), Math.Pow(a.y, power), Math.Pow(a.z, power));
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[{x}]d,[{y}]d,[{z}]d";
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is Vector3d))
            {
                return false;
            }

            var other = (Vector3d)obj;

            return this == other;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hash = x.GetHashCode();
                hash = (hash * 397) ^ y.GetHashCode();
                hash = (hash * 397) ^ z.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        /// Returns the dot product for the given vectors
        /// dot = a.x * a.y + b.x * b.y + b.z * b.z
        /// </summary>
        public static double Dot(Vector3d a, Vector3d b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        /// <summary>
        /// Returns the cross product for the given vectors
        /// cross = a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x
        /// </summary>
        public static Vector3d Cross(Vector3d a, Vector3d b)
        {
            return new Vector3d(a.y * b.z - a.z * b.y, 
                                a.z * b.x - a.x * b.z, 
                                a.x * b.y - a.y * b.x);
        }

        /// <summary>
        /// Returns the shortest angle between the two given vectors
        /// </summary>
        /// <remarks>Order is not important</remarks>
        public static double Angle(Vector3d a, Vector3d b)
        {
            return Math.Acos(Mathd.Clamp(Dot(a.Normalised, b.Normalised), -1d, 1d)) * Mathd.Rad2Deg;
        }

        /// <summary>
        /// Returns the vector rotated around the given axis
        /// </summary>
        /// <param name="axis">The axis to rotate the vector around</param>
        /// <param name="angle">The angle to rotate the vector by</param>
        public Vector3d Rotate(Vector3d axis, double angle)
        {
            if (axis == Zero)
            {
                throw new ArithmeticException(nameof(axis) + " is Vector3d.Zero " +
                                              "and will result in NaN");
            }

            var u = axis.x;
            var v = axis.y;
            var w = axis.z;

            var cosAngle = Math.Cos(angle * Mathd.Deg2Rad);
            var sinAngle = Math.Sin(angle * Mathd.Deg2Rad);

            var ms = axis.SqrMagnitude;
            var m = Math.Sqrt(ms);

            var xNew = (u * (u * x + v * y + w * z) +
                        (x * (v * v + w * w) - u * (v * y + w * z)) * cosAngle +
                        m * (-w * y + v * z) * sinAngle) / ms;

            var yNew = (v * (u * x + v * y + w * z) +
                       (y * (u * u + w * w) - v * (u * x + w * z)) * cosAngle +
                       m * (w * x - u * z) * sinAngle) / ms;

            var zNew = (w * (u * x + v * y + w * z) +
                       (z * (u * u + v * v) - w * (u * x + v * y)) * cosAngle +
                       m * (-(v * x) + u * y) * sinAngle) / ms;

            return new Vector3d(xNew, yNew, zNew);
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
        public static Vector3d Normalise(Vector3d v)
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
        public static Vector3d Lerp(Vector3d a, Vector3d b, double t)
        {
            t = Mathd.Clamp01(t);
            return new Vector3d(a.x + (b.x - a.x) * t, 
                                a.y + (b.y - a.y) * t,
                                a.z + (b.z - a.z) * t);
        }

        /// <summary>
        /// Returns true if the given point p is lies between
        /// or on either of the two given points a and b
        /// </summary>
        /// <param name="p">The point to test for</param>
        /// <param name="a">The possible starting vector</param>
        /// <param name="b">The possible end vector</param>
        public static bool LiesBetween(Vector3d p, Vector3d a, Vector3d b)
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
                    var zTime = p.z - a.z / (b.z - a.z);

                    if (zTime > 0 && zTime < 1)
                    {
                        return Mathd.EpsilonEquals(xTime, yTime) && 
                               Mathd.EpsilonEquals(yTime, zTime);
                    }
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
        public static double TimeBetween(Vector3d p, Vector3d a, Vector3d b)
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
                    var zTime = p.z - a.z / (b.z - a.z);

                    if (zTime > 0 && zTime < 1)
                    {
                        if(Mathd.EpsilonEquals(xTime, yTime) &&
                           Mathd.EpsilonEquals(yTime, zTime))
                        {
                            return xTime;
                        }
                    }
                }
            }

            throw new ArithmeticException("Point p does not lie on line ab");
        }

        /// <summary>
        /// Evaluates if the given points are coplanar, can fail on duplicate points
        /// </summary>
        /// <param name="points">The points to evaluate</param>
        /// <returns>True if the points are coplanar</returns>
        /// <exception cref="ArgumentNullException">Thrown when points is null</exception>
        public static bool Coplanar(IList<Vector3d> points)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            var pointsCount = points.Count;
            // 3 or less points are always coplanar
            if (pointsCount < 4)
            {
                return true;
            }

            // form the plane from the first 3 points
            var ab = points[1] - points[0];
            var ac = points[2] - points[0];
            var abxac = Vector3d.Cross(ab, ac);

            for (var i = 3; i < pointsCount; i++)
            {
                // form a vector from the point to the first point
                var ad = points[i] - points[0];

                // the vector must be perpendicular to abxac
                if (Vector3d.Dot(ad, abxac) > Mathf.Epsilon)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Evaluates a point's relative position given an anchor and an optional scale
        /// </summary>
        /// <param name="anchor">The anchor</param>
        /// <param name="point">The point</param>
        /// <param name="scale">The optional scale</param>
        public static Vector3d Relative(Vector3d anchor, Vector3d point,
            double scale = 1d)
        {
            return (point - anchor) * scale;
        }
    }
}