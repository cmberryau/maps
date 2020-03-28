using System;

namespace Maps
{
    /// <summary>
    /// 4 by 4 matrix with double precision
    /// 
    /// a  b  c  d
    /// e  f  g  h
    /// i  j  k  l
    /// m  n  o  p
    /// </summary>
    public class Matrix4d
    {
#pragma warning disable 1591
        public readonly double a;
        public readonly double b;
        public readonly double c;
        public readonly double d;
        public readonly double e;
        public readonly double f;
        public readonly double g;
        public readonly double h;
        public readonly double i;
        public readonly double j;
        public readonly double k;
        public readonly double l;
        public readonly double m;
        public readonly double n;
        public readonly double o;
        public readonly double p;
#pragma warning restore 1591

        /// <summary>
        /// Index accessor to the matrix
        /// </summary>
        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return a;
                    case 1:
                        return b;
                    case 2:
                        return c;
                    case 3:
                        return d;
                    case 4:
                        return e;
                    case 5:
                        return f;
                    case 6:
                        return g;
                    case 7:
                        return h;
                    case 8:
                        return i;
                    case 9:
                        return j;
                    case 10:
                        return k;
                    case 11:
                        return l;
                    case 12:
                        return m;
                    case 13:
                        return n;
                    case 14:
                        return o;
                    case 15:
                        return p;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// The inverse of the matrix
        /// </summary>
        public Matrix4d Inverse
        {
            get
            {
                var det = Determinant;

                if (det == 0)
                {
                    return NaN;
                }

                var af = a * f;
                var ag = a * g;
                var ah = a * h;
                var aj = a * j;
                var ak = a * k;
                var al = a * l;

                var be = b * e;
                var bg = b * g;
                var bh = b * h;
                var bi = b * i;
                var bk = b * k;
                var bl = b * l;

                var ce = c * e;
                var cf = c * f;
                var ch = c * h;
                var ci = c * i;
                var cj = c * j;
                var cl = c * l;

                var de = d * e;
                var df = d * f;
                var dg = d * g;
                var di = d * i;
                var dj = d * j;
                var dk = d * k;

                var ej = e * j;
                var ek = e * k;
                var el = e * l;

                var fi = f * i;
                var fk = f * k;
                var fl = f * l;

                var gi = g * i;
                var gj = g * j;
                var gl = g * l;

                var hi = h * i;
                var hj = h * j;
                var hk = h * k;

                return new Matrix4d(
                    fk * p + gl * n + hj * o - fl * o - gj * p - hk * n,
                    bl * o + cj * p + dk * n - bk * p - cl * n - dj * o,
                    bg * p + ch * n + df * o - bh * o - cf * p - dg * n,
                    bh * k + cf * l + dg * j - bg * l - ch * j - df * k,
                    el * o + gi * p + hk * m - ek * p - gl * m - hi * o,
                    ak * p + cl * m + di * o - al * o - ci * p - dk * m,
                    ah * o + ce * p + dg * m - ag * p - ch * m - de * o,
                    ag * l + ch * i + de * k - ah * k - ce * l - dg * i,
                    ej * p + fl * m + hi * n - el * n - fi * p - hj * m,
                    al * n + bi * p + dj * m - aj * p - bl * m - di * n,
                    af * p + bh * m + de * n - ah * n - be * p - df * m,
                    ah * j + be * l + df * i - af * l - bh * i - de * j,
                    ek * n + fi * o + gj * m - ej * o - fk * m - gi * n,
                    aj * o + bk * m + ci * n - ak * n - bi * o - cj * m,
                    ag * n + be * o + cf * m - af * o - bg * m - ce * n,
                    af * k + bg * i + ce * j - ag * j - be * k - cf * i)
                    * (1d / det);
            }
        }

        /// <summary>
        /// The transpose of the matrix
        /// </summary>
        public Matrix4d Transpose => new Matrix4d(a, e, i, m,
                                                  b, f, j, n,
                                                  c, g, k, o,
                                                  d, h, l, p);

        /// <summary>
        /// The determinant of the matrix
        /// </summary>
        public double Determinant => a * f * k * p + a * g * l * n + a * h * j * o
                                     + b * e * l * o + b * g * i * p + b * h * k * m
                                     + c * e * j * p + c * f * l * m + c * h * i * n
                                     + d * e * k * n + d * f * i * o + d * g * j * m
                                     - a * f * l * o - a * g * j * p - a * h * k * n
                                     - b * e * k * p - b * g * l * m - b * h * i * o
                                     - c * e * l * n - c * f * i * p - c * h * j * m
                                     - d * e * j * o - d * f * k * m - d * g * i * n;

        /// <summary>
        /// The a minor of the matrix
        /// </summary>
        public Matrix3d AMinor => new Matrix3d(f, g, h, j, k, l, n, o, p);

        /// <summary>
        /// The b minor of the matrix
        /// </summary>
        public Matrix3d BMinor => new Matrix3d(e, g, h, i, k, l, m, o, p);

        /// <summary>
        /// The c minor of the matrix
        /// </summary>
        public Matrix3d CMinor => new Matrix3d(e, f, h, i, j, l, m, n, p);

        /// <summary>
        /// The d minor of the matrix
        /// </summary>
        public Matrix3d DMinor => new Matrix3d(e, f, g, i, j, k, m, n, o);

        /// <summary>
        /// The p minor of the matrix
        /// </summary>
        public Matrix3d PMinor => new Matrix3d(a, b, c, e, f, g, i, j, k);

        /// <summary>
        /// The identity of the matrix
        /// </summary>
        public static readonly Matrix4d Identity = new Matrix4d(1d, 0d, 0d, 0d,
                                                              0d, 1d, 0d, 0d,
                                                              0d, 0d, 1d, 0d,
                                                              0d, 0d, 0d, 1d);

        /// <summary>
        /// NaN matrix
        /// </summary>
        public static readonly Matrix4d NaN = new Matrix4d(double.NaN);

        /// <summary>
        /// Initializes a new instance of Matrix4x4
        /// </summary>
        public Matrix4d(double a, double b, double c, double d,
                         double e, double f, double g, double h,
                         double i, double j, double k, double l,
                         double m, double n, double o, double p)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.e = e;
            this.f = f;
            this.g = g;
            this.h = h;
            this.i = i;
            this.j = j;
            this.k = k;
            this.l = l;
            this.m = m;
            this.n = n;
            this.o = o;
            this.p = p;
        }

        private Matrix4d(double all)
        {
            a = b = c = d = e = f = g = h = i = j = k = l = m = n = o = p = all;
        }

        /// <summary>
        /// Creates a Frustrum matrix
        /// </summary>
        /// <param name="left">The left edge of the frustrum</param>
        /// <param name="right">The right edge of the frustrum</param>
        /// <param name="bottom">The bottom edge of the frustrum</param>
        /// <param name="top">The top of the frustrum</param>
        /// <param name="znear">The near plane of the frustrum</param>
        /// <param name="zfar">The far plane of the frustrum</param>
        public static Matrix4d Frustrum(double left, double right,
            double bottom, double top, double znear, double zfar)
        {
            return new Matrix4d((2 * znear) / (right - left), 0d, 0d, 0d,
                               0d, (2 * znear) / (top - bottom), 0d, 0d,
                               0d, 0d, (zfar + znear) / (zfar - znear),
                               -(2 * zfar * znear) / (zfar - znear), 0d, 
                               0d, 1d, 0d);
        }

        /// <summary>
        /// Creates a Scale matrix
        /// </summary>
        /// <param name="scale">The scale vector</param>
        public static Matrix4d Scale(Vector3d scale)
        {
            return new Matrix4d(scale.x, 0d,      0d, 0d,
                                0d, scale.y,      0d, 0d,
                                0d,      0d, scale.z, 0d,
                                0d,      0d,      0d, 1d);
        }

        /// <summary>
        /// Creates a Translation matrix
        /// </summary>
        /// <param name="translation">The translation vector</param>
        public static Matrix4d Translate(Vector3d translation)
        {
            return new Matrix4d(1d, 0d, 0d, translation.x,
                                0d, 1d, 0d, translation.y,
                                0d, 0d, 1d, translation.z,
                                0d, 0d, 0d,            1d);
        }

        /// <summary>
        /// Creates a rotation matrix
        /// </summary>
        /// <param name="rotation">The rotation quaternion</param>
        public static Matrix4d Rotate(Quaterniond rotation)
        {
            return rotation.RotationMatrix4;
        }

        /// <summary>
        /// Addition operator
        /// </summary>
        public static Matrix4d operator +(Matrix4d lhs, double rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            return new Matrix4d(lhs.a + rhs, 
                               lhs.b + rhs, 
                               lhs.c + rhs, 
                               lhs.d + rhs,
                               lhs.e + rhs, 
                               lhs.f + rhs, 
                               lhs.g + rhs, 
                               lhs.h + rhs,
                               lhs.i + rhs,
                               lhs.j + rhs,
                               lhs.k + rhs,
                               lhs.l + rhs,
                               lhs.m + rhs,
                               lhs.n + rhs,
                               lhs.o + rhs,
                               lhs.p + rhs);
        }

        /// <summary>
        /// Addition operator
        /// </summary>
        public static Matrix4d operator +(double lhs, Matrix4d rhs)
        {
            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return rhs + lhs;
        }

        /// <summary>
        /// Addition operator
        /// </summary>
        public static Matrix4d operator +(Matrix4d lhs, Matrix4d rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return new Matrix4d(lhs.a + rhs.a,
                               lhs.b + rhs.b,
                               lhs.c + rhs.c,
                               lhs.d + rhs.d,
                               lhs.e + rhs.e,
                               lhs.f + rhs.f,
                               lhs.g + rhs.g,
                               lhs.h + rhs.h,
                               lhs.i + rhs.i,
                               lhs.j + rhs.j,
                               lhs.k + rhs.k,
                               lhs.l + rhs.l,
                               lhs.m + rhs.m,
                               lhs.n + rhs.n,
                               lhs.o + rhs.o,
                               lhs.p + rhs.p);
        }

        /// <summary>
        /// Subtraction operator
        /// </summary>
        public static Matrix4d operator -(Matrix4d lhs, double rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            return new Matrix4d(lhs.a - rhs, 
                               lhs.b - rhs,
                               lhs.c - rhs,
                               lhs.d - rhs,
                               lhs.e - rhs,
                               lhs.f - rhs,
                               lhs.g - rhs,
                               lhs.h - rhs,
                               lhs.i - rhs,
                               lhs.j - rhs,
                               lhs.k - rhs,
                               lhs.l - rhs,
                               lhs.m - rhs,
                               lhs.n - rhs,
                               lhs.o - rhs,
                               lhs.p - rhs);
        }

        /// <summary>
        /// Subtraction operator
        /// </summary>
        public static Matrix4d operator -(double lhs, Matrix4d rhs)
        {
            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return new Matrix4d(lhs - rhs.a,
                               lhs - rhs.b,
                               lhs - rhs.c,
                               lhs - rhs.d,
                               lhs - rhs.e,
                               lhs - rhs.f,
                               lhs - rhs.g,
                               lhs - rhs.h,
                               lhs - rhs.i,
                               lhs - rhs.j,
                               lhs - rhs.k,
                               lhs - rhs.l,
                               lhs - rhs.m,
                               lhs - rhs.n,
                               lhs - rhs.o,
                               lhs - rhs.p);
        }

        /// <summary>
        /// Subtraction operator
        /// </summary>
        public static Matrix4d operator -(Matrix4d lhs, Matrix4d rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return new Matrix4d(lhs.a - rhs.a,
                               lhs.b - rhs.b,
                               lhs.c - rhs.c,
                               lhs.d - rhs.d,
                               lhs.e - rhs.e,
                               lhs.f - rhs.f,
                               lhs.g - rhs.g,
                               lhs.h - rhs.h,
                               lhs.i - rhs.i,
                               lhs.j - rhs.j,
                               lhs.k - rhs.k,
                               lhs.l - rhs.l,
                               lhs.m - rhs.m,
                               lhs.n - rhs.n,
                               lhs.o - rhs.o,
                               lhs.p - rhs.p);
        }

        /// <summary>
        /// Unary minus operator
        /// </summary>
        public static Matrix4d operator -(Matrix4d matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            return new Matrix4d(-matrix.a,
                               -matrix.b,
                               -matrix.c,
                               -matrix.d,
                               -matrix.e,
                               -matrix.f,
                               -matrix.g,
                               -matrix.h,
                               -matrix.i,
                               -matrix.j,
                               -matrix.k,
                               -matrix.l,
                               -matrix.m,
                               -matrix.n,
                               -matrix.o,
                               -matrix.p);
        }

        /// <summary>
        /// Multiplication operator
        /// </summary>
        public static Matrix4d operator *(Matrix4d lhs, double factor)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            return new Matrix4d(lhs.a * factor,
                               lhs.b * factor,
                               lhs.c * factor,
                               lhs.d * factor,
                               lhs.e * factor,
                               lhs.f * factor,
                               lhs.g * factor,
                               lhs.h * factor,
                               lhs.i * factor,
                               lhs.j * factor,
                               lhs.k * factor,
                               lhs.l * factor,
                               lhs.m * factor,
                               lhs.n * factor,
                               lhs.o * factor,
                               lhs.p * factor);
        }

        /// <summary>
        /// Multiplication operator
        /// </summary>
        public static Matrix4d operator *(double factor, Matrix4d rhs)
        {
            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return rhs * factor;
        }

        /// <summary>
        /// Multiplication operator
        /// </summary>
        public static Matrix4d operator *(Matrix4d lhs, Matrix4d rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return new Matrix4d(// row 0
                               lhs.a * rhs.a +
                               lhs.b * rhs.e +
                               lhs.c * rhs.i +
                               lhs.d * rhs.m,

                               lhs.a * rhs.b +
                               lhs.b * rhs.f +
                               lhs.c * rhs.j +
                               lhs.d * rhs.n,

                               lhs.a * rhs.c +
                               lhs.b * rhs.g +
                               lhs.c * rhs.k +
                               lhs.d * rhs.o,

                               lhs.a * rhs.d +
                               lhs.b * rhs.h +
                               lhs.c * rhs.l +
                               lhs.d * rhs.p,

                               // row 1
                               lhs.e * rhs.a +
                               lhs.f * rhs.e +
                               lhs.g * rhs.i +
                               lhs.h * rhs.m,

                               lhs.e * rhs.b +
                               lhs.f * rhs.f +
                               lhs.g * rhs.j +
                               lhs.h * rhs.n,

                               lhs.e * rhs.c +
                               lhs.f * rhs.g +
                               lhs.g * rhs.k +
                               lhs.h * rhs.o,

                               lhs.e * rhs.d +
                               lhs.f * rhs.h +
                               lhs.g * rhs.l +
                               lhs.h * rhs.p,

                               // row 2
                               lhs.i * rhs.a +
                               lhs.j * rhs.e +
                               lhs.k * rhs.i +
                               lhs.l * rhs.m,

                               lhs.i * rhs.b +
                               lhs.j * rhs.f +
                               lhs.k * rhs.j +
                               lhs.l * rhs.n,
                               
                               lhs.i * rhs.c +
                               lhs.j * rhs.g +
                               lhs.k * rhs.k +
                               lhs.l * rhs.o,

                               lhs.i * rhs.d +
                               lhs.j * rhs.h +
                               lhs.k * rhs.l +
                               lhs.l * rhs.p,

                               // row 3
                               lhs.m * rhs.a +
                               lhs.n * rhs.e +
                               lhs.o * rhs.i +
                               lhs.p * rhs.m,

                               lhs.m * rhs.b +
                               lhs.n * rhs.f +
                               lhs.o * rhs.j +
                               lhs.p * rhs.n,

                               lhs.m * rhs.c +
                               lhs.n * rhs.g +
                               lhs.o * rhs.k +
                               lhs.p * rhs.o,

                               lhs.m * rhs.d +
                               lhs.n * rhs.h +
                               lhs.o * rhs.l +
                               lhs.p * rhs.p);
        }

        /// <summary>
        /// Multiplication operator
        /// </summary>
        public static Vector3d operator *(Matrix4d lhs, Vector3d rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            return new Vector3d(
                lhs.a * rhs.x + lhs.b * rhs.y + lhs.c * rhs.z + lhs.d * 1,
                lhs.e * rhs.x + lhs.f * rhs.y + lhs.g * rhs.z + lhs.h * 1,
                lhs.i * rhs.x + lhs.j * rhs.y + lhs.k * rhs.z + lhs.l * 1);
        }

        /// <summary>
        /// Multiplication operator
        /// </summary>
        public static Vector4d operator *(Matrix4d lhs, Vector4d rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            return new Vector4d(
                lhs.a * rhs.x + lhs.b * rhs.y + lhs.c * rhs.z + lhs.d * rhs.w,
                lhs.e * rhs.x + lhs.f * rhs.y + lhs.g * rhs.z + lhs.h * rhs.w,
                lhs.i * rhs.x + lhs.j * rhs.y + lhs.k * rhs.z + lhs.l * rhs.w,
                lhs.m * rhs.x + lhs.n * rhs.y + lhs.o * rhs.z + lhs.p * rhs.w);
        }

        /// <summary>
        /// Evaluates if the two given matrices are equal
        /// </summary>
        public static bool operator ==(Matrix4d lhs, Matrix4d rhs)
        {
            if (ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            if (((object)lhs == null) || ((object)rhs == null))
            {
                return false;
            }

            return lhs.a == rhs.a &&
                   lhs.b == rhs.b &&
                   lhs.c == rhs.c &&
                   lhs.d == rhs.d &&
                   lhs.e == rhs.e &&
                   lhs.f == rhs.f &&
                   lhs.g == rhs.g &&
                   lhs.h == rhs.h &&
                   lhs.i == rhs.i &&
                   lhs.j == rhs.j &&
                   lhs.k == rhs.k &&
                   lhs.l == rhs.l &&
                   lhs.m == rhs.m &&
                   lhs.n == rhs.n &&
                   lhs.o == rhs.o &&
                   lhs.p == rhs.p;
        }

        /// <summary>
        /// Evaluates if the two given matrices are not equal
        /// </summary>
        public static bool operator !=(Matrix4d lhs, Matrix4d rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Evaluates if given matrix is NaN
        /// </summary>
        public static bool IsNaN(Matrix4d matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            return double.IsNaN(matrix.a) ||
                   double.IsNaN(matrix.b) ||
                   double.IsNaN(matrix.c) ||
                   double.IsNaN(matrix.d) ||
                   double.IsNaN(matrix.e) ||
                   double.IsNaN(matrix.f) ||
                   double.IsNaN(matrix.g) ||
                   double.IsNaN(matrix.h) ||
                   double.IsNaN(matrix.i) ||
                   double.IsNaN(matrix.j) ||
                   double.IsNaN(matrix.k) ||
                   double.IsNaN(matrix.l) ||
                   double.IsNaN(matrix.m) ||
                   double.IsNaN(matrix.n) ||
                   double.IsNaN(matrix.o) ||
                   double.IsNaN(matrix.p);
        }

        /// <summary>
        /// Returns a string representation of the matrix
        /// </summary>
        public override string ToString()
        {
            return $"[{a.ToString("+0.000;-0.000")}]" +
                   $"[{b.ToString("+0.000;-0.000")}]" +
                   $"[{c.ToString("+0.000;-0.000")}]" +
                   $"[{d.ToString("+0.000;-0.000")}]\n" +
                   $"[{e.ToString("+0.000;-0.000")}]" +
                   $"[{f.ToString("+0.000;-0.000")}]" +
                   $"[{g.ToString("+0.000;-0.000")}]" +
                   $"[{h.ToString("+0.000;-0.000")}]\n" +
                   $"[{i.ToString("+0.000;-0.000")}]" +
                   $"[{j.ToString("+0.000;-0.000")}]" +
                   $"[{k.ToString("+0.000;-0.000")}]" +
                   $"[{l.ToString("+0.000;-0.000")}]\n" +
                   $"[{m.ToString("+0.000;-0.000")}]" +
                   $"[{n.ToString("+0.000;-0.000")}]" +
                   $"[{o.ToString("+0.000;-0.000")}]" +
                   $"[{p.ToString("+0.000;-0.000")}]";
        }

        /// <summary>
        /// Evalutes if the matrix is equal to another object
        /// </summary>
        public override bool Equals(object obj)
        {
            if (!(obj is Matrix4d))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var matrix = (Matrix4d) obj;

            return this == matrix;
        }

        /// <summary>
        /// Returns the hash code for the vector
        /// </summary>
        public override int GetHashCode()
        {
            var hash = a.GetHashCode();

            unchecked
            {
                hash = (hash * 397) ^ b.GetHashCode();
                hash = (hash * 397) ^ c.GetHashCode();
                hash = (hash * 397) ^ d.GetHashCode();
                hash = (hash * 397) ^ e.GetHashCode();
                hash = (hash * 397) ^ f.GetHashCode();
                hash = (hash * 397) ^ g.GetHashCode();
                hash = (hash * 397) ^ h.GetHashCode();
                hash = (hash * 397) ^ i.GetHashCode();
                hash = (hash * 397) ^ j.GetHashCode();
                hash = (hash * 397) ^ k.GetHashCode();
                hash = (hash * 397) ^ l.GetHashCode();
                hash = (hash * 397) ^ m.GetHashCode();
                hash = (hash * 397) ^ n.GetHashCode();
                hash = (hash * 397) ^ o.GetHashCode();
                hash = (hash * 397) ^ p.GetHashCode();

                return hash;
            }
        }
    }
}
