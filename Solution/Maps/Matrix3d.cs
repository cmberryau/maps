using System;

namespace Maps
{
    /// <summary>
    /// 3 by 3 matrix with double precision
    /// 
    /// a  b  c
    /// d  e  f
    /// g  h  i
    /// </summary>
    public class Matrix3d
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
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// The inverse of the matrix
        /// </summary>
        public Matrix3d Inverse
        {
            get
            {
                var det = Determinant;

                if (det == 0)
                {
                    return NaN;
                }

                return new Matrix3d(
                    e * i - f * h, c * h - b * i, b * f - c * e,
                    f * g - d * i, a * i - c * g, c * d - a * f,
                    d * h - e * g, b * g - a * h, a * e - b * d) * (1d / det);
            }
        }

        /// <summary>
        /// The transpose of the matrix
        /// </summary>
        public Matrix3d Transpose => new Matrix3d(a, d, g,
                                                  b, e, h,
                                                  c, f, i);

        /// <summary>
        /// The determinant of the matrix
        /// </summary>
        public double Determinant => a * e * i + 
                                     b * f * g + 
                                     c * d * h - 
                                     c * e * g - 
                                     b * d * i - 
                                     a * f * h;

        /// <summary>
        /// The a column minor of the matrix
        /// </summary>
        public Matrix2d AMinor => new Matrix2d(e, f, h, i);

        /// <summary>
        /// The b column minor of the matrix
        /// </summary>
        public Matrix2d BMinor => new Matrix2d(d, f, g, i);

        /// <summary>
        /// The c column minor of the matrix
        /// </summary>
        public Matrix2d CMinor => new Matrix2d(d, e, g, h);

        /// <summary>
        /// The identity of the matrix
        /// </summary>
        public static readonly Matrix3d Identity = new Matrix3d(1d, 0d, 0d,
                                                              0d, 1d, 0d,
                                                              0d, 0d, 1d);

        /// <summary>
        /// NaN matrix
        /// </summary>
        public static readonly Matrix3d NaN = new Matrix3d(double.NaN);

        /// <summary>
        /// Initializes a new instance of Matrix3x3
        /// </summary>
        public Matrix3d(double a, double b, double c,
                         double d, double e, double f,
                         double g, double h, double i)
        {
            this.a = a; this.b = b; this.c = c;
            this.d = d; this.e = e; this.f = f;
            this.g = g; this.h = h; this.i = i;
        }

        private Matrix3d(double all)
        {
            a = b = c = d = e = f = g = h = i = all;
        }

        /// <summary>
        /// Addition operator
        /// </summary>
        public static Matrix3d operator +(Matrix3d lhs, double rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            return new Matrix3d(lhs.a + rhs, 
                               lhs.b + rhs, 
                               lhs.c + rhs,
                               lhs.d + rhs, 
                               lhs.e + rhs, 
                               lhs.f + rhs, 
                               lhs.g + rhs, 
                               lhs.h + rhs, 
                               lhs.i + rhs);
        }

        /// <summary>
        /// Addition operator
        /// </summary>
        public static Matrix3d operator +(double lhs, Matrix3d rhs)
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
        public static Matrix3d operator +(Matrix3d lhs, Matrix3d rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return new Matrix3d(lhs.a + rhs.a, 
                               lhs.b + rhs.b, 
                               lhs.c + rhs.c,
                               lhs.d + rhs.d, 
                               lhs.e + rhs.e, 
                               lhs.f + rhs.f, 
                               lhs.g + rhs.g, 
                               lhs.h + rhs.h, 
                               lhs.i + rhs.i);
        }

        /// <summary>
        /// Subtraction operator
        /// </summary>
        public static Matrix3d operator -(Matrix3d lhs, double rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            return new Matrix3d(lhs.a - rhs,
                               lhs.b - rhs,
                               lhs.c - rhs,
                               lhs.d - rhs,
                               lhs.e - rhs,
                               lhs.f - rhs,
                               lhs.g - rhs,
                               lhs.h - rhs,
                               lhs.i - rhs);
        }

        /// <summary>
        /// Subtraction operator
        /// </summary>
        public static Matrix3d operator -(double lhs, Matrix3d rhs)
        {
            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return new Matrix3d(lhs - rhs.a,
                               lhs - rhs.b,
                               lhs - rhs.c,
                               lhs - rhs.d,
                               lhs - rhs.e,
                               lhs - rhs.f,
                               lhs - rhs.g,
                               lhs - rhs.h,
                               lhs - rhs.i);
        }

        /// <summary>
        /// Subtraction operator
        /// </summary>
        public static Matrix3d operator -(Matrix3d lhs, Matrix3d rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return new Matrix3d(lhs.a - rhs.a,
                               lhs.b - rhs.b,
                               lhs.c - rhs.c,
                               lhs.d - rhs.d,
                               lhs.e - rhs.e,
                               lhs.f - rhs.f,
                               lhs.g - rhs.g,
                               lhs.h - rhs.h,
                               lhs.i - rhs.i);
        }

        /// <summary>
        /// Unary minus operator
        /// </summary>
        public static Matrix3d operator -(Matrix3d matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            return new Matrix3d(-matrix.a,
                               -matrix.b,
                               -matrix.c,
                               -matrix.d,
                               -matrix.e,
                               -matrix.f,
                               -matrix.g,
                               -matrix.h,
                               -matrix.i);
        }

        /// <summary>
        /// Multiplication operator
        /// </summary>
        public static Matrix3d operator *(Matrix3d lhs, double rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            return new Matrix3d(lhs.a * rhs,
                               lhs.b * rhs,
                               lhs.c * rhs,
                               lhs.d * rhs,
                               lhs.e * rhs,
                               lhs.f * rhs,
                               lhs.g * rhs,
                               lhs.h * rhs,
                               lhs.i * rhs);
        }

        /// <summary>
        /// Multiplication operator
        /// </summary>
        public static Matrix3d operator *(double lhs, Matrix3d rhs)
        {
            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return rhs * lhs;
        }

        /// <summary>
        /// Multiplication operator
        /// </summary>
        public static Vector3d operator *(Matrix3d lhs, Vector3d rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            return new Vector3d(
                lhs.a * rhs.x + lhs.b * rhs.y + lhs.c * rhs.z,
                lhs.d * rhs.x + lhs.e * rhs.y + lhs.f * rhs.z,
                lhs.g * rhs.x + lhs.h * rhs.y + lhs.i * rhs.z);
        }

        /// <summary>
        /// Multiplication operator
        /// </summary>
        public static Matrix3d operator *(Matrix3d lhs, Matrix3d rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return new Matrix3d(// row 0
                               lhs.a * rhs.a + 
                               lhs.b * rhs.d + 
                               lhs.c * rhs.g,

                               lhs.a * rhs.b +
                               lhs.b * rhs.e +
                               lhs.c * rhs.h,

                               lhs.a * rhs.c +
                               lhs.b * rhs.f +
                               lhs.c * rhs.i,

                               // row 1
                               lhs.d * rhs.a +
                               lhs.e * rhs.d +
                               lhs.f * rhs.g,

                               lhs.d * rhs.b +
                               lhs.e * rhs.e +
                               lhs.f * rhs.h,

                               lhs.d * rhs.c +
                               lhs.e * rhs.f +
                               lhs.f * rhs.i,

                               // row 2
                               lhs.g * rhs.a +
                               lhs.h * rhs.d +
                               lhs.i * rhs.g,

                               lhs.g * rhs.b +
                               lhs.h * rhs.e +
                               lhs.i * rhs.h,

                               lhs.g * rhs.c + 
                               lhs.h * rhs.f + 
                               lhs.i * rhs.i);
        }

        /// <summary>
        /// Evaluates if the two given matrices are equal
        /// </summary>
        public static bool operator ==(Matrix3d lhs, Matrix3d rhs)
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
                   lhs.i == rhs.i;
        }

        /// <summary>
        /// Evaluates if the two given matrices are not equal
        /// </summary>
        public static bool operator !=(Matrix3d lhs, Matrix3d rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Evaluates if given matrix is NaN
        /// </summary>
        public static bool IsNaN(Matrix3d matrix)
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
                   double.IsNaN(matrix.i);
        }

        /// <summary>
        /// Returns a string representation of the matrix
        /// </summary>
        public override string ToString()
        {
            return $"[{a.ToString("+0.000;-0.000")}]" +
                   $"[{b.ToString("+0.000;-0.000")}]" +
                   $"[{c.ToString("+0.000;-0.000")}]\n" +
                   $"[{d.ToString("+0.000;-0.000")}]" +
                   $"[{e.ToString("+0.000;-0.000")}]" +
                   $"[{f.ToString("+0.000;-0.000")}]\n" +
                   $"[{g.ToString("+0.000;-0.000")}]" +
                   $"[{h.ToString("+0.000;-0.000")}]" +
                   $"[{i.ToString("+0.000;-0.000")}]";
        }

        /// <summary>
        /// Evalutes if one matrix is equal to another
        /// </summary>
        public override bool Equals(object obj)
        {
            if (!(obj is Matrix3d))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var matrix = (Matrix3d)obj;
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

                return hash;
            }
        }
    }
}