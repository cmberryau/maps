using System;
using System.Runtime.InteropServices;

namespace Maps
{
    /// <summary>
    /// Double precision quaternion
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public struct Quaterniond
    {
        /// <summary>
        /// The x component of the quaternion
        /// </summary>
        [FieldOffset(0)]
        public readonly Vector3d v;

        /// <summary>
        /// The w component of the quaternion
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
                        return v[index];
                    case 1:
                        return v[index];
                    case 2:
                        return v[index];
                    case 3:
                        return w;
                    default:
                        throw new IndexOutOfRangeException("Invalid " +
                            $"{nameof(Quaterniond)} index");
                }
            }
        }

        /// <summary>
        /// The x component of the quaternion
        /// </summary>
        public double x => v.x;

        /// <summary>
        /// The y component of the quaternion
        /// </summary>
        public double y => v.y;

        /// <summary>
        /// The z component of the quaternion
        /// </summary>
        public double z => v.z;

        /// <summary>
        /// The magnitude of the quaternion
        /// </summary>
        public double Magnitude => Math.Sqrt(SqrMagnitude);

        /// <summary>
        /// The squared magnitude of the quaternion
        /// </summary>
        public double SqrMagnitude => w * w + v.SqrMagnitude;

        /// <summary>
        /// The conjugate of the quaternion
        /// </summary>
        public Quaterniond Conjugate => ConjugateIt(this);

        /// <summary>
        /// The quaternion, with a unit magnitude - not a unit vector
        /// </summary>
        public Quaterniond Normalised => Normalise(this);

        /// <summary>
        /// The quaternion, inverted
        /// </summary>
        public Quaterniond Inverse => Invert(this);

        /// <summary>
        /// The 4x4 matrix to rotate a point in space
        /// </summary>
        public Matrix4d RotationMatrix4
        {
            get
            {
                var xy = v.x * v.y;
                var xz = v.x * v.z;
                var yz = v.y * v.z;

                var wx = w * v.x;
                var wy = w * v.y;
                var wz = w * v.z;

                var x2 = v.x * v.x;
                var y2 = v.y * v.y;
                var z2 = v.z * v.z;

                return new Matrix4d(1 - 2 * (y2 + z2), 2 * (xy - wz), 2 * (xz + wy), 0,
                                   2 * (xy + wz), 1 - 2 * (x2 + z2), 2 * (yz - wx), 0,
                                   2 * (xz - wy), 2 * (yz + wx), 1 - 2 * (x2 + y2), 0,
                                   0, 0, 0, 1);
            }
        }

        /// <summary>
        /// The 3x3 matrix to rotate a point in space
        /// </summary>
        public Matrix3d RotationMatrix3
        {
            get
            {
                var xy = v.x * v.y;
                var xz = v.x * v.z;
                var yz = v.y * v.z;

                var wx = w * v.x;
                var wy = w * v.y;
                var wz = w * v.z;

                var x2 = v.x * v.x;
                var y2 = v.y * v.y;
                var z2 = v.z * v.z;

                return new Matrix3d(1 - 2 * (y2 + z2), 2 * (xy - wz), 2 * (xz + wy),
                                   2 * (xy + wz), 1 - 2 * (x2 + z2), 2 * (yz - wx),
                                   2 * (xz - wy), 2 * (yz + wx), 1 - 2 * (x2 + y2));
            }
        }

        /// <summary>
        /// The zero quaternion
        /// </summary>
        public static readonly Quaterniond Zero = new Quaterniond(0, 0, 0, 0);

        /// <summary>
        /// The identity quaternion
        /// </summary>
        public static readonly Quaterniond Identity = new Quaterniond(0, 0, 0, 1);

        /// <summary>
        /// The quaternion represented by euler angles
        /// </summary>
        public Vector3d EulerAngles
        {
            get
            {
                var x = EulerX;
                var y = EulerY;
                var z = EulerZ;

                return new Vector3d(x, y, z);
            }
        }

        /// <summary>
        /// The x axis euler rotation
        /// </summary>
        public double EulerX
        {
            get
            {
                var qy = 2d * (y * z + w * x);
                var qx = w * w - x * x - y * y + z * z;

                if (Mathd.EpsilonEquals(qy, 0d) && Mathd.EpsilonEquals(qx, 0d))
                {
                    return 2 * Math.Atan2(x, w) * Mathd.Rad2Deg;
                }

                return Math.Atan2(qy, qx) * Mathd.Rad2Deg;
            }
        }

        /// <summary>
        /// The y axis euler rotation
        /// </summary>
        public double EulerY
        {
            get
            {
                return Math.Asin(Mathd.Clamp(-2d * (x * z - w * y), 
                    -1d, 1d)) * Mathd.Rad2Deg;
            }
        }

        /// <summary>
        /// The z axis euler rotation
        /// </summary>
        public double EulerZ
        {
            get
            {
                return Math.Atan2(2d * (x * y + w * z), 
                    w * w + x * x - y * y - z * z) * Mathd.Rad2Deg;
            }
        }

        /// <summary>
        /// Initializes a new instance of Quaterniond
        /// </summary>
        /// <param name="v">The vector component</param>
        /// <param name="w">The w component</param>
        public Quaterniond(Vector3d v, double w)
        {
            this.v = v;
            this.w = w;
        }

        /// <summary>
        /// Initializes a new instance of Quaterniond
        /// </summary>
        /// <param name="x">The x component</param>
        /// <param name="y">The y component</param>
        /// <param name="z">The z component</param>
        /// <param name="w">The w component</param>
        public Quaterniond(double x, double y, double z, double w)
        {
            v = new Vector3d(x, y, z);
            this.w = w;
        }

        /// <summary>
        /// Initializes a new instance of Quaterniond
        /// 
        /// Reference: http://www.euclideanspace.com/maths/geometry/rotations/conversions/matrixToQuaternion/
        /// </summary>
        /// <param name="m">The rotation matrix to create a quaternion from</param>
        public Quaterniond(Matrix3d m)
        {
            if (m == null)
            {
                throw new ArgumentNullException(nameof(m));
            }

            if (Math.Abs(m.Determinant - 1) > Mathd.EpsilonE15)
            {
                throw new ArgumentException("Determinant is not +1, will produce " +
                                            "degenerate quaternion", nameof(m));
            }

            var tr = m.a + m.e + m.i;
            double qx, qy, qz;

            if (tr > 0)
            {
                var s = Math.Sqrt(tr + 1) * 2;

                w = 0.25 * s;
                qx = (m.h - m.f) / s;
                qy = (m.c - m.g) / s;
                qz = (m.d - m.b) / s;
            }
            else if (m.a > m.e && m.a > m.i)
            {
                var s = Math.Sqrt(1 + m.a - m.e - m.i) * 2;

                w = (m.h - m.f) / s;
                qx = 0.25 * s;
                qy = (m.b + m.d) / s;
                qz = (m.c + m.g) / s;
            }
            else if (m.e > m.i)
            {
                var s = Math.Sqrt(1 + m.e - m.a - m.i) * 2;

                w = (m.c - m.g) / s;
                qx = (m.b + m.d) / s;
                qy = 0.25 * s;
                qz = (m.f + m.h) / s;
            }
            else
            {
                var s = Math.Sqrt(1 + m.i - m.a - m.e) * 2;

                w = (m.d - m.b) / s;
                qx = (m.c + m.g) / s;
                qy = (m.f + m.h) / s;
                qz = 0.25 * s;
            }

            v = new Vector3d(qx, qy, qz);
        }

        /// <summary>
        /// Evaluates the axis and angle for the given quaternion
        /// </summary>
        /// <param name="axis">The output axis of the quaternion</param>
        /// <param name="angle">The output angle of the quaternion</param>
        public void ToAxisAngle(out Vector3d axis, out double angle)
        {
            angle = Math.Acos(w) * 2 * Mathd.Rad2Deg;

            if (w == 1)
            {
                axis = Vector3d.One;
            }
            else
            {
                axis = v / Math.Sqrt(1 - w * w);
            }
        }

        /// <summary>
        /// Creates a quaternion from a vector with euler angles
        /// </summary>
        /// <param name="angles">The vector with euler angles</param>
        /// <returns>A quaternion representing the euler angles</returns>
        public static Quaterniond Euler(Vector3d angles)
        {
            return Euler(angles.x, angles.y, angles.z);
        }

        /// <summary>
        /// Creates a quaternion from euler angles
        /// </summary>
        /// <param name="x">The rotation on the x axis</param>
        /// <param name="y">The rotation on the y axis</param>
        /// <param name="z">The rotation on the z axis</param>
        /// <returns>A quaternion representing the euler angles</returns>
        public static Quaterniond Euler(double x, double y, double z)
        {
            var halfx = x * Mathd.HalfDeg2Rad;
            var halfy = y * Mathd.HalfDeg2Rad;
            var halfz = z * Mathd.HalfDeg2Rad;

            var cy = Math.Cos(halfy);
            var sy = Math.Sin(halfy);
            var cx = Math.Cos(halfx);
            var sx = Math.Sin(halfx);
            var cz = Math.Cos(halfz);
            var sz = Math.Sin(halfz);

            var qw = cx * cy * cz + sx * sy * sz;
            var qx = sx * cy * cz - cx * sy * sz;
            var qy = cx * sy * cz + sx * cy * sz;
            var qz = cx * cy * sz - sx * sy * cz;

            return new Quaterniond(qx, qy, qz, qw);
        }

        /// <summary>
        /// Evaluates the inverse of a quaternion
        /// </summary>
        public static Quaterniond operator -(Quaterniond q)
        {
            return q.Inverse;
        }

        /// <summary>
        /// Evaluates the difference of two quaternions
        /// </summary>
        public static Quaterniond operator -(Quaterniond a, Quaterniond b)
        {
            return new Quaterniond(a.v - b.v, a.w - b.w);
        }

        /// <summary>
        /// Evaluates the sum of two quaternions
        /// </summary>
        public static Quaterniond operator +(Quaterniond a, Quaterniond b)
        {
            return new Quaterniond(a.v + b.v, a.w + b.w);
        }

        /// <summary>
        /// Evaluates the product of a quaternion and a scalar
        /// </summary>
        public static Quaterniond operator *(Quaterniond q, double s)
        {
            return new Quaterniond(q.v * s, q.w * s);
        }

        /// <summary>
        /// Evaluates the product of a scalar and a quaternion
        /// </summary>
        public static Quaterniond operator *(double s, Quaterniond q)
        {
            return new Quaterniond(q.v * s, q.w * s);
        }

        /// <summary>
        /// Evaluates the product of two quaternions
        /// </summary>
        public static Quaterniond operator *(Quaterniond a, Quaterniond b)
        {
            return new Quaterniond(a.w * b.v + b.w * a.v + Vector3d.Cross(a.v,
                b.v), a.w * b.w - Vector3d.Dot(a.v, b.v));
        }

        /// <summary>
        /// Evaluates the product of a quaternion and a vector
        /// </summary>
        public static Vector3d operator *(Quaterniond q, Vector3d v)
        {
            return q.RotationMatrix4 * v;
        }

        /// <summary>
        /// Evaluates the product of a vector and a quaternion
        /// </summary>
        public static Vector3d operator *(Vector3d v, Quaterniond q)
        {
            return q.RotationMatrix4 * v;
        }

        /// <summary>
        /// Evaluates the division of a quaternion by a scalar
        /// </summary>
        public static Quaterniond operator /(Quaterniond q, double s)
        {
            return new Quaterniond(q.v / s, q.w / s);
        }

        /// <summary>
        /// Evaluates equality between two quaternions
        /// </summary>
        public static bool operator ==(Quaterniond a, Quaterniond b)
        {
            return a.v == b.v && Math.Abs(a.w - b.w) < Mathd.Epsilon;
        }

        /// <summary>
        /// Evaluates inequality between two quaternions
        /// </summary>
        public static bool operator !=(Quaterniond a, Quaterniond b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Forms a quaternion given the rotation axis and angle
        /// </summary>
        /// <param name="axis">The axis to rotate on</param>
        /// <param name="angle">The angle in degrees</param>
        public static Quaterniond AxisAngle(Vector3d axis, double angle)
        {
            if (axis.SqrMagnitude < Mathd.Epsilon)
            {
                return Identity;
            }

            angle *= 0.5 * Mathd.Deg2Rad;
            var v = axis.Normalised * Math.Sin(angle);
            var w = Math.Cos(angle);

            return new Quaterniond(v, w).Normalised;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{v},[{w}]d";
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is Quaterniond))
            {
                return false;
            }

            var other = (Quaterniond)obj;

            return this == other;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return v.GetHashCode() ^ w.GetHashCode();
        }

        private static Quaterniond ConjugateIt(Quaterniond q)
        {
            return new Quaterniond(-q.v, q.w);
        }

        private static Quaterniond Invert(Quaterniond q)
        {
            var sqrMagnitude = q.SqrMagnitude;
            return sqrMagnitude > Mathd.Epsilon ? q.Conjugate / sqrMagnitude : q;
        }

        private static Quaterniond Normalise(Quaterniond q)
        {
            var s = 1 / q.Magnitude;
            return new Quaterniond(q.v * s, q.w * s);
        }
    }
}