using System;
using System.Runtime.InteropServices;

namespace Maps
{
    /// <summary>
    /// Immutable 4 dimensional vector with single precision
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public struct Vector4f
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
        /// The w component of the vector
        /// </summary>
        [FieldOffset(12)]
        public readonly float w;

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
                    case 3:
                        return w;
                    default:
                        throw new IndexOutOfRangeException("Invalid " +
                            $"{nameof(Vector4f)} index");
                }
            }
        }

        /// <summary>
        /// The square magnitude of the vector
        /// </summary>
        public float SqrMagnitude => x * x + y * y + z * z + w * w;

        /// <summary>
        /// Initializes a new instance of Vector4D32
        /// </summary>
        /// <param name="x">The x component</param>
        /// <param name="y">The y component</param>
        /// <param name="z">The z component</param>
        /// <param name="w">The w component</param>
        public Vector4f(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Initializes a new instance of Vector4D
        /// </summary>
        /// <param name="vector3d">The 3d vector to construct xyz from</param>
        /// <param name="w">The w component</param>
        public Vector4f(Vector3f vector3d, float w)
        {
            x = vector3d.x;
            y = vector3d.y;
            z = vector3d.z;
            this.w = w;
        }

        /// <summary>
        /// Initializes a new instance of Vector4D
        /// </summary>
        /// <param name="xyzw">The value for the x, y, z and w components</param>
        public Vector4f(float xyzw)
        {
            x = y = z = w = xyzw;
        }

        /// <summary>
        /// Returns the negative of the given vector
        /// </summary>
        public static Vector4f operator -(Vector4f a)
        {
            return new Vector4f(-a.x, -a.y, -a.z, a.w);
        }

        /// <summary>
        /// Returns a new vector that is the difference of the given two
        /// </summary>
        public static Vector4f operator -(Vector4f a, Vector4f b)
        {
            return new Vector4f(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }

        /// <summary>
        /// Returns a new vector that is the sum of the given two
        /// </summary>
        public static Vector4f operator +(Vector4f a, Vector4f b)
        {
            return new Vector4f(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        /// <summary>
        /// Returns a new vector that is the product of the given
        /// vector by the given double value
        /// </summary>
        public static Vector4f operator *(float f, Vector4f a)
        {
            return new Vector4f(a.x * f, a.y * f, a.z * f, a.w * f);
        }

        /// <summary>
        /// Returns a new vector that is the product of the given
        /// vector by the given double value
        /// </summary>
        public static Vector4f operator *(Vector4f a, float f)
        {
            return new Vector4f(a.x * f, a.y * f, a.z * f, a.w * f);
        }

        /// <summary>
        /// Returns a new vector that is the result of divison
        /// of the given vector by the given double value
        /// </summary>
        public static Vector4f operator /(float f, Vector4f a)
        {
            return new Vector4f(f / a.x, f / a.y, f / a.z, f / a.w);
        }

        /// <summary>
        /// Returns a new vector that is the result of divison
        /// of the given vector by the given double value
        /// </summary>
        public static Vector4f operator /(Vector4f a, float f)
        {
            return new Vector4f(a.x / f, a.y / f, a.z / f, a.w / f);
        }

        /// <summary>
        /// Evaluates if the two given vectors are equal
        /// </summary>
        public static bool operator ==(Vector4f lhs, Vector4f rhs)
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
        public static bool operator !=(Vector4f lhs, Vector4f rhs)
        {
            return !(lhs == rhs);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[{x}]f,[{y}]f,[{z}]f,[{w}]f";
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is Vector4f))
            {
                return false;
            }

            var other = (Vector4f)obj;

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
                hash = (hash * 397) ^ w.GetHashCode();
                return hash;
            }
        }
    }
}