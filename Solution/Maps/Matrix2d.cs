using System;

namespace Maps
{
    /// <summary>
    /// 2 by 2 matrix with double precision
    /// 
    /// a  b
    /// c  d
    /// </summary>
    public class Matrix2d
    {
#pragma warning disable 1591
        public readonly double a;
        public readonly double b;
        public readonly double c;
        public readonly double d;
#pragma warning restore 1591

        /// <summary>
        /// The inverse of the matrix
        /// </summary>
        public Matrix2d Inverse
        {
            get
            {
                var det = Determinant;

                if (det == 0)
                {
                    return NaN;
                }

                return new Matrix2d(d, -b, -c, a) * (1d / det);
            }
        }

        /// <summary>
        /// The transpose of the matrix
        /// </summary>
        public Matrix2d Transpose => new Matrix2d(a, c, b, d);

        /// <summary>
        /// The determinant of the matrix
        /// </summary>
        public double Determinant => a * d - b * c;

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
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// The identity of the matrix
        /// </summary>
        public static readonly Matrix2d Identity = new Matrix2d(1d, 0d,
                                                              0d, 1d);

        /// <summary>
        /// NaN matrix
        /// </summary>
        public static readonly Matrix2d NaN = new Matrix2d(double.NaN);

        /// <summary>
        /// Initializes a new instance of Matrix2x2
        /// </summary>
        public Matrix2d(double a, double b, double c, double d)
        {
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
        }

        private Matrix2d(double all)
        {
            a = b = c = d = all;
        }

        /// <summary>
        /// Addition operator
        /// </summary>
        public static Matrix2d operator +(Matrix2d lhs, double rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            return new Matrix2d(lhs.a + rhs, 
                               lhs.b + rhs, 
                               lhs.c + rhs,
                               lhs.d + rhs);
        }

        /// <summary>
        /// Addition operator
        /// </summary>
        public static Matrix2d operator +(double lhs, Matrix2d rhs)
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
        public static Matrix2d operator +(Matrix2d lhs, Matrix2d rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return new Matrix2d(lhs.a + rhs.a, 
                               lhs.b + rhs.b,
                               lhs.c + rhs.c, 
                               lhs.d + rhs.d);
        }

        /// <summary>
        /// Subtraction operator
        /// </summary>
        public static Matrix2d operator -(Matrix2d lhs, double rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            return new Matrix2d(lhs.a - rhs, 
                               lhs.b - rhs,
                               lhs.c - rhs, 
                               lhs.d - rhs);
        }

        /// <summary>
        /// Subtraction operator
        /// </summary>
        public static Matrix2d operator -(double lhs, Matrix2d rhs)
        {
            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return new Matrix2d(lhs - rhs.a, 
                               lhs - rhs.b, 
                               lhs - rhs.c, 
                               lhs - rhs.d);
        }

        /// <summary>
        /// Subtraction operator
        /// </summary>
        public static Matrix2d operator -(Matrix2d lhs, Matrix2d rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return new Matrix2d(lhs.a - rhs.a, 
                               lhs.b - rhs.b,
                               lhs.c - rhs.c, 
                               lhs.d - rhs.d);
        }

        /// <summary>
        /// Unary minus operator
        /// </summary>
        public static Matrix2d operator -(Matrix2d matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            return new Matrix2d(-matrix.a,
                               -matrix.b,
                               -matrix.c,
                               -matrix.d);
        }

        /// <summary>
        /// Multiplication operator
        /// </summary>
        public static Matrix2d operator *(Matrix2d lhs, double rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            return new Matrix2d(lhs.a * rhs, 
                               lhs.b * rhs, 
                               lhs.c * rhs, 
                               lhs.d * rhs);
        }

        /// <summary>
        /// Multiplication operator
        /// </summary>
        public static Matrix2d operator *(double lhs, Matrix2d rhs)
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
        public static Matrix2d operator *(Matrix2d lhs, Matrix2d rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }

            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }

            return new Matrix2d(// row 0
                               lhs.a * rhs.a +
                               lhs.b * rhs.c,
                               lhs.a * rhs.b +
                               lhs.b * rhs.d,

                               // row 1
                               lhs.c * rhs.a +
                               lhs.d * rhs.c,
                               lhs.c * rhs.b +
                               lhs.d * rhs.d);
        }

        /// <summary>
        /// Evaluates if the two given matrices are equal
        /// </summary>
        public static bool operator ==(Matrix2d lhs, Matrix2d rhs)
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
                   lhs.d == rhs.d;
        }

        /// <summary>
        /// Evaluates if the two given matrices are not equal
        /// </summary>
        public static bool operator !=(Matrix2d lhs, Matrix2d rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Evaluates if given matrix is NaN
        /// </summary>
        public static bool IsNaN(Matrix2d matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            return double.IsNaN(matrix.a) ||
                   double.IsNaN(matrix.b) || 
                   double.IsNaN(matrix.c) ||
                   double.IsNaN(matrix.d);
        }

        /// <summary>
        /// Returns a string representation of the matrix
        /// </summary>
        public override string ToString()
        {
            return $"[{a.ToString("+0.000;-0.000")}]" +
                   $"[{b.ToString("+0.000;-0.000")}]\n" +
                   $"[{c.ToString("+0.000;-0.000")}]" +
                   $"[{d.ToString("+0.000;-0.000")}]";
        }

        /// <summary>
        /// Evalutes if one matrix is equal to another
        /// </summary>
        public override bool Equals(object obj)
        {
            if (!(obj is Matrix2d))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var matrix = (Matrix2d) obj;
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

                return hash;
            }
        }
    }
}