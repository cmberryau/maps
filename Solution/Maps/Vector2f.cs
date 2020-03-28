using System;
using System.Runtime.InteropServices;

namespace Maps
{
    /// <summary>
    /// Immutable 2 dimensional vector with single precision
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct Vector2f
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
        /// Indexer access to components
        /// </summary>
        public float this[int index]
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
                        $"{nameof(Vector2f)} index");
                }

                return y;
            }
        }

        /// <summary>
        /// Returns the smallest component of the vector
        /// </summary>
        public float MinComponent => Mathf.Min(x, y);

        /// <summary>
        /// Returns the smallest component of the vector
        /// </summary>
        public float MaxComponent => Mathf.Max(x, y);

        /// <summary>
        /// The magnitude of the vector
        /// </summary>
        public float Magnitude => Mathf.Sqrt(SqrMagnitude);

        /// <summary>
        /// The square magnitude of the vector
        /// </summary>
        public float SqrMagnitude => x * x + y * y;

        /// <summary>
        /// The vector, normalised
        /// </summary>
        public Vector2f Normalised => Normalise(this);

        /// <summary>
        /// The vector perpendicular to this vector
        /// </summary>
        public Vector2f Perpendicular => new Vector2f(-y, x);

        /// <summary>
        /// Returns a vector with 1, 1
        /// </summary>
        public static readonly Vector2f One = new Vector2f(1f);

        /// <summary>
        /// Returns a vector with 0, 0
        /// </summary>
        public static readonly Vector2f Zero = new Vector2f(0f);

        /// <summary>
        /// Returns a vector with 0, 1
        /// </summary>
        public static readonly Vector2f Up = new Vector2f(0f, 1f);

        /// <summary>
        /// Returns a vector with 0, -1
        /// </summary>
        public static readonly Vector2f Down = new Vector2f(0f, -1f);

        /// <summary>
        /// Returns a vector with 1, 0
        /// </summary>
        public static readonly Vector2f Right = new Vector2f(1f, 0f);

        /// <summary>
        /// Returns a vector with -1, 0
        /// </summary>
        public static readonly Vector2f Left = new Vector2f(-1f, 0f);

        /// <summary>
        /// Returns a vector with MaxValue, MaxValue
        /// </summary>
        public static readonly Vector2f MaxValue = new Vector2f(float.MaxValue);

        /// <summary>
        /// Returns a vector with MaxValue, MaxValue
        /// </summary>
        public static readonly Vector2f MinValue = new Vector2f(float.MinValue);

        /// <summary>
        /// Returns a vector with PositiveInfinity, PositiveInfinity
        /// </summary>
        public static readonly Vector2f PositiveInfinity = new Vector2f(float.PositiveInfinity);

        /// <summary>
        /// Returns a vector with NegativeInfinity, NegativeInfinity
        /// </summary>
        public static readonly Vector2f NegativeInfinity = new Vector2f(float.NegativeInfinity);

        /// <summary>
        /// Returns a vector with NaN, NaN
        /// </summary>
        public static readonly Vector2f NaN = new Vector2f(float.NaN);

        /// <summary>
        /// Swizzle property for XY
        /// </summary>
        public Vector2f xy => this;

        /// <summary>
        /// Swizzle property for YX
        /// </summary>
        public Vector2f yx => new Vector2f(y, x);

        /// <summary>
        /// Constructs a new vector with the given x, y components
        /// </summary>
        /// <param name="xy">The value for both x and y components</param>
        public Vector2f(float xy)
        {
            this.x = this.y = xy;
        }

        /// <summary>
        /// Constructs a new vector with the given x, y components
        /// </summary>
        /// <param name="x">The x component</param>
        /// <param name="y">The y component</param>
        public Vector2f(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Returns the negative of the given vector
        /// </summary>
        public static Vector2f operator -(Vector2f a)
        {
            return new Vector2f(-a.x, -a.y);
        }

        /// <summary>
        /// Returns a new vector that is the difference of the given two
        /// </summary>
        public static Vector2f operator -(Vector2f a, Vector2f b)
        {
            return new Vector2f(a.x - b.x, a.y - b.y);
        }

        /// <summary>
        /// Returns a new vector that is the sum of the given two
        /// </summary>
        public static Vector2f operator +(Vector2f a, Vector2f b)
        {
            return new Vector2f(a.x + b.x, a.y + b.y);
        }

        /// <summary>
        /// Returns a new vector that is the product of the given
        /// vector by the given float value
        /// </summary>
        public static Vector2f operator *(float d, Vector2f a)
        {
            return new Vector2f(a.x * d, a.y * d);
        }

        /// <summary>
        /// Returns a new vector that is the product of the given
        /// vector by the given float value
        /// </summary>
        public static Vector2f operator *(Vector2f a, float f)
        {
            return new Vector2f(a.x * f, a.y * f);
        }

        /// <summary>
        /// Returns a new vector that is the result of divison
        /// of the given vector by the given float value
        /// </summary>
        public static Vector2f operator /(float d, Vector2f a)
        {
            return new Vector2f(d / a.x, d / a.y);
        }

        /// <summary>
        /// Returns a new vector that is the result of divison
        /// of the given vector by the given float value
        /// </summary>
        public static Vector2f operator /(Vector2f a, float d)
        {
            return new Vector2f(a.x / d, a.y / d);
        }

        /// <summary>
        /// Evaluates if the two given vectors are equal
        /// </summary>
        public static bool operator ==(Vector2f lhs, Vector2f rhs)
        {
            if (lhs.x == rhs.x && lhs.y == rhs.y)
            {
                return true;
            }

            return (lhs - rhs).SqrMagnitude < Mathf.Epsilon;
        }

        /// <summary>
        /// Evaluates if the two given vectors are not equal
        /// </summary>
        public static bool operator !=(Vector2f lhs, Vector2f rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Returns a value indicating whether the specified vector or any of it's 
        /// members evaluates to not a number
        /// </summary>
        public static bool IsNaN(Vector2f a)
        {
            if (float.IsNaN(a.x) || float.IsNaN(a.y))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a new vector that is the component wise product
        /// of the two vectors
        /// </summary>
        public static Vector2f ComponentMultiply(Vector2f a, Vector2f b)
        {
            return new Vector2f(a.x * b.x, a.y * b.y);
        }

        /// <summary>
        /// Returns the distance between the two given vectors
        /// </summary>
        public static float Distance(Vector2f a, Vector2f b)
        {
            return (a - b).Magnitude;
        }

        /// <summary>
        /// Returns the absolute vector of the given vector
        /// </summary>
        public static Vector2f Abs(Vector2f a)
        {
            return new Vector2f(Mathf.Abs(a.x), Mathf.Abs(a.y));
        }

        /// <summary>
        /// Returns a vector as the distance between components of two given vectors
        /// </summary>
        public static Vector2f ComponentDistance(Vector2f a, Vector2f b)
        {
            return Abs(a - b);
        }

        /// <summary>
        /// Returns the midpoint between two vectors
        /// </summary>
        public static Vector2f Midpoint(Vector2f a, Vector2f b)
        {
            return (a + b) * 0.5f;
        }

        /// <summary>
        /// Returns a new vector, the result of component wise scaling
        /// </summary>
        public static Vector2f Scale(Vector2f a, Vector2f b)
        {
            return new Vector2f(a.x * b.x, a.y * b.y);
        }

        /// <summary>
        /// Returns the component wise maximum of the two vectors
        /// </summary>
        public static Vector2f Max(Vector2f a, Vector2f b)
        {
            return new Vector2f(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y));
        }

        /// <summary>
        /// Returns the component wise minimum of the two given vectors
        /// </summary>
        public static Vector2f Min(Vector2f a, Vector2f b)
        {
            return new Vector2f(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y));
        }

        /// <summary>
        /// Returns the vector with all components raised to the power
        /// </summary>
        /// <param name="a">A Vector2d32 to be raised to a power</param>
        /// <param name="power">The power to raise the Vector2d32 to</param>
        public static Vector2f Pow(Vector2f a, float power)
        {
            return new Vector2f(Mathf.Pow(a.x, power), Mathf.Pow(a.y, power));
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[{x}]f,[{y}]f";
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is Vector2f))
            {
                return false;
            }

            var other = (Vector2f) obj;

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
        public static float Dot(Vector2f a, Vector2f b)
        {
            return a.x * b.x + a.y * b.y;
        }

        /// <summary>
        /// Returns the cross product for the given vectors
        /// cross = a.x * b.y - a.y * b.x
        /// </summary>
        public static float Cross(Vector2f a, Vector2f b)
        {
            return a.x * b.y - a.y * b.x;
        }

        /// <summary>
        /// Returns the shortest angle between the two given vectors in degrees
        /// </summary>
        /// <remarks>Order is not important</remarks>
        public static float Angle(Vector2f a, Vector2f b)
        {
            return Mathf.Acos(Mathf.Clamp(Dot(a.Normalised, b.Normalised), -1f, 1f)) * Mathf.Rad2deg;
        }

        /// <summary>
        /// Returns the polar angle of the vector
        /// </summary>
        public float PolarAngle()
        {
            var value = 0f;

            if (Mathf.EpsilonEquals(x, 0f) && Mathf.EpsilonEquals(y, 0f))
            {
                return value;
            }

            value = Mathf.Atan2(y, x) * Mathf.Rad2deg;

            return value;
        }

        /// <summary>
        /// Returns a vector rotated counter clockwise by the given amount
        /// </summary>
        public Vector2f Rotate(float angle)
        {
            angle *= Mathf.Deg2Rad;

            var sin = Mathf.Sin(angle);
            var cos = Mathf.Cos(angle);

            return new Vector2f(cos * x - sin * y, cos * y + sin * x);
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
        public static Vector2f Normalise(Vector2f v)
        {
            if (v.Magnitude > Mathf.EpsilonE5)
            {
                return v/v.Magnitude;
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
        public static Vector2f Lerp(Vector2f a, Vector2f b, float t)
        {
            t = Mathf.Clamp01(t);
            return new Vector2f(a.x + (b.x - a.x)*t, a.y + (b.y - a.y)*t);
        }

        /// <summary>
        /// Returns true if the given point p is lies between
        /// or on either of the two given points a and b
        /// </summary>
        /// <param name="p">The point to test for</param>
        /// <param name="a">The possible starting vector</param>
        /// <param name="b">The possible end vector</param>
        public static bool LiesBetween(Vector2f p, Vector2f a, Vector2f b)
        {
            // check if p is at an endpoint
            if (p == a || p == b)
            {
                return true;
            }

            // between on the x axis
            if (p.x >= a.x && p.x <= b.x)
            {
                var xTime = p.x - a.x / (b.x - a.x);

                // between on the y axis
                if (p.y >= a.y && p.y <= b.y)
                {
                    var yTime = p.y - a.y / (b.y - a.y);

                    return Mathf.EpsilonEquals(xTime, yTime);
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
        /// <exception cref="Exception"></exception>
        public static float TimeBetween(Vector2f p, Vector2f a, Vector2f b)
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

            // between on the x axis
            if (p.x >= a.x && p.x <= b.x)
            {
                var xTime = p.x - a.x / (b.x - a.x);

                // between on the y axis
                if (p.y >= a.y && p.y <= b.y)
                {
                    var yTime = p.y - a.y / (b.y - a.y);

                    if (Mathf.EpsilonEquals(xTime, yTime))
                    {
                        return xTime;
                    }
                }
            }

            throw new ArithmeticException("Point p does not lie on line a->b");
        }
    }
}