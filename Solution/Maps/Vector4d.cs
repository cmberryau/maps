using System;
using System.Runtime.InteropServices;

namespace Maps
{
    /// <summary>
    /// Immutable 4 dimensional vector with double precision
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public struct Vector4d
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
        /// The w component of the vector
        /// </summary>
        [FieldOffset(24)]
        public readonly double w;

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
                    case 3:
                        return w;
                    default:
                        throw new IndexOutOfRangeException("Invalid " +
                            $"{nameof(Vector4d)} index");
                }
            }
        }

        /// <summary>
        /// Returns the high value for a split 32bit representation
        /// </summary>
        public Vector4f High => new Vector4f((float)x, 
                                                 (float)y,
                                                 (float)z,
                                                 (float)w);

        /// <summary>
        /// Returns low high value for a split 32bit representation
        /// </summary>
        public Vector4f Low
        {
            get
            {
                var high = High;
                return new Vector4f((float)(x - high.x),
                                      (float)(y - high.y),
                                      (float)(z - high.z),
                                      (float)(w - high.w));
            }
        }

        /// <summary>
        /// The square magnitude of the vector
        /// </summary>
        public double SqrMagnitude => x * x + y * y + z * z + w * w;

        /// <summary>
        /// Swizzle property for XYZ
        /// </summary>
        public Vector3d xyz => new Vector3d(x, y, z);

        /// <summary>
        /// Initializes a new instance of Vector4D
        /// </summary>
        /// <param name="x">The x component</param>
        /// <param name="y">The y component</param>
        /// <param name="z">The z component</param>
        /// <param name="w">The w component</param>
        public Vector4d(double x, double y, double z, double w)
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
        public Vector4d(Vector3d vector3d, double w)
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
        public Vector4d(double xyzw)
        {
            x = y = z = w = xyzw;
        }

        /// <summary>
        /// Returns the negative of the given vector
        /// </summary>
        public static Vector4d operator -(Vector4d a)
        {
            return new Vector4d(-a.x, -a.y, -a.z, a.w);
        }

        /// <summary>
        /// Returns a new vector that is the difference of the given two
        /// </summary>
        public static Vector4d operator -(Vector4d a, Vector4d b)
        {
            return new Vector4d(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }

        /// <summary>
        /// Returns a new vector that is the sum of the given two
        /// </summary>
        public static Vector4d operator +(Vector4d a, Vector4d b)
        {
            return new Vector4d(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        /// <summary>
        /// Returns a new vector that is the product of the given
        /// vector by the given double value
        /// </summary>
        public static Vector4d operator *(double d, Vector4d a)
        {
            return new Vector4d(a.x * d, a.y * d, a.z * d, a.w * d);
        }

        /// <summary>
        /// Returns a new vector that is the product of the given
        /// vector by the given double value
        /// </summary>
        public static Vector4d operator *(Vector4d a, double d)
        {
            return new Vector4d(a.x * d, a.y * d, a.z * d, a.w * d);
        }

        /// <summary>
        /// Returns a new vector that is the result of divison
        /// of the given vector by the given double value
        /// </summary>
        public static Vector4d operator /(double d, Vector4d a)
        {
            return new Vector4d(d / a.x, d / a.y, d / a.z, d / a.w);
        }

        /// <summary>
        /// Returns a new vector that is the result of divison
        /// of the given vector by the given double value
        /// </summary>
        public static Vector4d operator /(Vector4d a, double d)
        {
            return new Vector4d(a.x / d, a.y / d, a.z / d, a.w / d);
        }

        /// <summary>
        /// Evaluates if the two given vectors are equal
        /// </summary>
        public static bool operator ==(Vector4d lhs, Vector4d rhs)
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
        public static bool operator !=(Vector4d lhs, Vector4d rhs)
        {
            return !(lhs == rhs);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"[{x}]d,[{y}]d,[{z}]d,[{w}]d";
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is Vector4d))
            {
                return false;
            }

            var other = (Vector4d)obj;

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
