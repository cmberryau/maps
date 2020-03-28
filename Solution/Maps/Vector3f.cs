using System;
using System.Runtime.InteropServices;

namespace Maps
{
    /// <summary>
    /// Immutable 3 dimensional vector with single precision
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public struct Vector3f
    {
        /// <summary>
        /// The x component of the vector
        /// </summary>
        [FieldOffset(0)]
        public readonly float x;

        /// <summary>
        /// The y component of the vector
        /// </summary>
        [FieldOffset(4)]
        public readonly float y;

        /// <summary>
        /// The z component of the vector
        /// </summary>
        [FieldOffset(8)]
        public readonly float z;

        /// <summary>
        /// Indexer access to components
        /// </summary>
        public float this[int index]
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
                            $"{nameof(Vector3f)} index");
                }
            }
        }

        /// <summary>
        /// Returns the smallest component of the vector
        /// </summary>
        public float MinComponent => Mathf.Min(x, Mathf.Min(y, z));

        /// <summary>
        /// Returns the smallest component of the vector
        /// </summary>
        public float MaxComponent => Mathf.Max(x, Mathf.Max(y, z));

        /// <summary>
        /// The magnitude of the vector
        /// </summary>
        public float Magnitude => Mathf.Sqrt(SqrMagnitude);

        /// <summary>
        /// The square magnitude of the vector
        /// </summary>
        public float SqrMagnitude => x * x + y * y + z * z;

        /// <summary>
        /// The vector, normalised
        /// </summary>
        public Vector3f Normalised => Normalise(this);

        /// <summary>
        /// Returns a vector with 1, 1, 1
        /// </summary>
        public static readonly Vector3f One = new Vector3f(1f);

        /// <summary>
        /// Returns a vector with 0, 0, 0
        /// </summary>
        public static readonly Vector3f Zero = new Vector3f(0f);

        /// <summary>
        /// Returns a vector with 0, 1, 0
        /// </summary>
        public static readonly Vector3f Up = new Vector3f(0f, 1f, 0f);

        /// <summary>
        /// Returns a vector with 0, -1, 0
        /// </summary>
        public static readonly Vector3f Down = new Vector3f(0f, -1f, 0f);

        /// <summary>
        /// Returns a vector with 1, 0, 0
        /// </summary>
        public static readonly Vector3f Right = new Vector3f(1f, 0f, 0f);

        /// <summary>
        /// Returns a vector with -1, 0, 0
        /// </summary>
        public static readonly Vector3f Left = new Vector3f(-1f, 0f, 0f);

        /// <summary>
        /// Returns a vector with 0, 0, 1
        /// </summary>
        public static readonly Vector3f Forward = new Vector3f(0f, 0f, 1f);

        /// <summary>
        /// Returns a vector with 0, 0, -1
        /// </summary>
        public static readonly Vector3f Back = new Vector3f(0f, 0f, -1f);

        /// <summary>
        /// Returns a vector with MaxValue, MaxValue, MaxValue
        /// </summary>
        public static readonly Vector3f MaxValue = new Vector3f(float.MaxValue);

        /// <summary>
        /// Returns a vector with MinValue, MinValue
        /// </summary>
        public static readonly Vector3f MinValue = new Vector3f(float.MinValue);

        /// <summary>
        /// Returns a vector with PositiveInfinity, PositiveInfinity, PositiveInfinity
        /// </summary>
        public static readonly Vector3f PositiveInfinity = new Vector3f(float.PositiveInfinity);

        /// <summary>
        /// Returns a vector with NegativeInfinity, NegativeInfinity, NegativeInfinity
        /// </summary>
        public static readonly Vector3f NegativeInfinity = new Vector3f(float.NegativeInfinity);

        /// <summary>
        /// Returns a vector with NaN, NaN, NaN
        /// </summary>
        public static readonly Vector3f NaN = new Vector3f(float.NaN);

        /// <summary>
        /// Swizzle property for XY
        /// </summary>
        public Vector2f xy => new Vector2f(x, y);

        /// <summary>
        /// Swizzle property for XZ
        /// </summary>
        public Vector2f xz => new Vector2f(x, z);

        /// <summary>
        /// Swizzle property for YX
        /// </summary>
        public Vector2f yx => new Vector2f(y, x);

        /// <summary>
        /// Swizzle property for YZ
        /// </summary>
        public Vector2f yz => new Vector2f(y, z);

        /// <summary>
        /// Swizzle property for ZX
        /// </summary>
        public Vector2f zx => new Vector2f(z, x);

        /// <summary>
        /// Swizzle property for ZY
        /// </summary>
        public Vector2f zy => new Vector2f(z, y);

        /// <summary>
        /// Swizzle property for XYZ
        /// </summary>
        public Vector3f xyz => this;

        /// <summary>
        /// Swizzle property for XZY
        /// </summary>
        public Vector3f xzy => new Vector3f(x, z, y);

        /// <summary>
        /// Swizzle property for YXZ
        /// </summary>
        public Vector3f yxz => new Vector3f(y, x, z);

        /// <summary>
        /// Swizzle property for YZX
        /// </summary>
        public Vector3f yzx => new Vector3f(y, z, x);

        /// <summary>
        /// Swizzle property for ZYX
        /// </summary>
        public Vector3f zyx => new Vector3f(z, y, x);

        /// <summary>
        /// Swizzle property for ZXY
        /// </summary>
        public Vector3f zxy => new Vector3f(z, x, y);

        /// <summary>
        /// Constructs a new vector with the given x, y and z components
        /// </summary>
        /// <param name="xyz">The value for the x, y and z components</param>
        public Vector3f(float xyz)
        {
            this.x = this.y = this.z = xyz;
        }

        /// <summary>
        /// Constructs a new vector with the given x, y and z components
        /// </summary>
        /// <param name="x">The x component</param>
        /// <param name="y">The y component</param>
        /// <param name="z">The z component</param>
        public Vector3f(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// Returns the negative of the given vector
        /// </summary>
        public static Vector3f operator -(Vector3f a)
        {
            return new Vector3f(-a.x, -a.y, -a.z);
        }

        /// <summary>
        /// Returns a new vector that is the difference of the given two
        /// </summary>
        public static Vector3f operator -(Vector3f a, Vector3f b)
        {
            return new Vector3f(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        /// <summary>
        /// Returns a new vector that is the sum of the given two
        /// </summary>
        public static Vector3f operator +(Vector3f a, Vector3f b)
        {
            return new Vector3f(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        /// <summary>
        /// Returns a new vector that is the product of the given
        /// vector by the given float value
        /// </summary>
        public static Vector3f operator *(float d, Vector3f a)
        {
            return new Vector3f(a.x * d, a.y * d, a.z * d);
        }

        /// <summary>
        /// Returns a new vector that is the product of the given
        /// vector by the given float value
        /// </summary>
        public static Vector3f operator *(Vector3f a, float d)
        {
            return new Vector3f(a.x * d, a.y * d, a.z * d);
        }

        /// <summary>
        /// Returns a new vector that is the result of divison
        /// of the given vector by the given float value
        /// </summary>
        public static Vector3f operator /(float d, Vector3f a)
        {
            return new Vector3f(d / a.x, d / a.y, d / a.z);
        }

        /// <summary>
        /// Returns a new vector that is the result of divison
        /// of the given vector by the given float value
        /// </summary>
        public static Vector3f operator /(Vector3f a, float d)
        {
            return new Vector3f(a.x / d, a.y / d, a.z / d);
        }

        /// <summary>
        /// Evaluates if the two given vectors are equal
        /// </summary>
        public static bool operator ==(Vector3f lhs, Vector3f rhs)
        {
            if (lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z)
            {
                return true;
            }

            return (lhs - rhs).SqrMagnitude < Mathf.Epsilon;
        }

        /// <summary>
        /// Evaluates if the two given vectors are not equal
        /// </summary>
        public static bool operator !=(Vector3f lhs, Vector3f rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Returns a value indicating whether the specified vector or any of it's 
        /// members evaluates to not a number
        /// </summary>
        public static bool IsNaN(Vector3f a)
        {
            if (float.IsNaN(a.x) || float.IsNaN(a.y) || float.IsNaN(a.z))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a new vector that is the component wise product
        /// of the two vectors
        /// </summary>
        public static Vector3f ComponentMultiply(Vector3f a, Vector3f b)
        {
            return new Vector3f(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        /// <summary>
        /// Returns the distance between the two given vectors
        /// </summary>
        public static float Distance(Vector3f a, Vector3f b)
        {
            return (a - b).Magnitude;
        }

        /// <summary>
        /// Returns the absolute vector of the given vector
        /// </summary>
        public static Vector3f Abs(Vector3f a)
        {
            return new Vector3f(Mathf.Abs(a.x), Mathf.Abs(a.y), Mathf.Abs(a.z));
        }

        /// <summary>
        /// Returns a vector as the distance between components of two given vectors
        /// </summary>
        public static Vector3f ComponentDistance(Vector3f a, Vector3f b)
        {
            return Abs(a - b);
        }

        /// <summary>
        /// Returns the midpoint between two vectors
        /// </summary>
        public static Vector3f Midpoint(Vector3f a, Vector3f b)
        {
            return (a + b) * 0.5f;
        }

        /// <summary>
        /// Returns a new vector, the result of component wise scaling
        /// </summary>
        public static Vector3f Scale(Vector3f a, Vector3f b)
        {
            return new Vector3f(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        /// <summary>
        /// Returns the component wise maximum of the two vectors
        /// </summary>
        public static Vector3f Max(Vector3f a, Vector3f b)
        {
            return new Vector3f(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y), Mathf.Max(a.z, b.z));
        }

        /// <summary>
        /// Returns the component wise minimum of the two given vectors
        /// </summary>
        public static Vector3f Min(Vector3f a, Vector3f b)
        {
            return new Vector3f(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y), Mathf.Min(a.z, b.z));
        }

        /// <summary>
        /// Returns the vector with all components raised to the power
        /// </summary>
        /// <param name="a">A Vector3d32 to be raised to a power</param>
        /// <param name="power">The power to raise the Vector3d32 to</param>
        public static Vector3f Pow(Vector3f a, float power)
        {
            return new Vector3f(Mathf.Pow(a.x, power), Mathf.Pow(a.y, power), Mathf.Pow(a.z, power));
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[{x}]f,[{y}]f,[{z}]f";
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is Vector3f))
            {
                return false;
            }

            var other = (Vector3f)obj;

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
        /// dot = a.x * a.y + b.x * b.y
        /// </summary>
        public static float Dot(Vector3f a, Vector3f b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        /// <summary>
        /// Returns the cross product for the given vectors
        /// cross = a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x
        /// </summary>
        public static Vector3f Cross(Vector3f a, Vector3f b)
        {
            return new Vector3f(a.y * b.z - a.z * b.y,
                                a.z * b.x - a.x * b.z,
                                a.x * b.y - a.y * b.x);
        }

        /// <summary>
        /// Returns the shortest angle between the two given vectors
        /// </summary>
        /// <remarks>Order is not important</remarks>
        public static float Angle(Vector3f a, Vector3f b)
        {
            return Mathf.Acos(Mathf.Clamp(Dot(a.Normalised, b.Normalised), -1f, 1f)) * Mathf.Rad2deg;
        }

        /// <summary>
        /// Returns the vector rotated around the given axis
        /// </summary>
        /// <param name="axis">The axis to rotate the vector around</param>
        /// <param name="angle">The angle to rotate the vector by</param>
        public Vector3f Rotate(Vector3f axis, float angle)
        {
            var u = axis.x;
            var v = axis.y;
            var w = axis.z;

            var cosAngle = Mathf.Cos(angle * Mathf.Deg2Rad);
            var sinAngle = Mathf.Sin(angle * Mathf.Deg2Rad);

            var ms = axis.SqrMagnitude;
            var m = Mathf.Sqrt(ms);

            var xNew = (u * (u * x + v * y + w * z) +
                        (x * (v * v + w * w) - u * (v * y + w * z)) * cosAngle +
                        m * (-w * y + v * z) * sinAngle) / ms;

            var yNew = (v * (u * x + v * y + w * z) +
                       (y * (u * u + w * w) - v * (u * x + w * z)) * cosAngle +
                       m * (w * x - u * z) * sinAngle) / ms;

            var zNew = (w * (u * x + v * y + w * z) +
                       (z * (u * u + v * v) - w * (u * x + v * y)) * cosAngle +
                       m * (-(v * x) + u * y) * sinAngle) / ms;

            return new Vector3f(xNew, yNew, zNew);
        }

        /// <summary>
        /// Normalises the vector
        /// </summary>
        public void Normalise()
        {
            if (Magnitude > Mathf.EpsilonE5)
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
        public static Vector3f Normalise(Vector3f v)
        {
            if (v.Magnitude > Mathf.EpsilonE5)
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
        public static Vector3f Lerp(Vector3f a, Vector3f b, float t)
        {
            t = Mathf.Clamp01(t);
            return new Vector3f(a.x + (b.x - a.x) * t,
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
        public static bool LiesBetween(Vector3f p, Vector3f a, Vector3f b)
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
                        return Mathf.EpsilonEquals(xTime, yTime) &&
                               Mathf.EpsilonEquals(yTime, zTime);
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
        public static float TimeBetween(Vector3f p, Vector3f a, Vector3f b)
        {
            // endpoint checks
            if (p == a)
            {
                return 0f;
            }

            if (p == b)
            {
                return 1f;
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
                        if (Mathf.EpsilonEquals(xTime, yTime) &&
                           Mathf.EpsilonEquals(yTime, zTime))
                        {
                            return xTime;
                        }
                    }
                }
            }

            throw new ArithmeticException("Point p does not lie on line ab");
        }
    }
}
