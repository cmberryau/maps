using System;
using NUnit.Framework;

namespace Maps.Tests
{
    /// <summary>
    /// Series of tests for the Matrix2 class
    /// </summary>
    [TestFixture]
    internal sealed class Matrix2Tests
    {
        private static readonly Matrix2d A = new Matrix2d(1d, 2d, 3d, 4d);
        private static readonly Matrix2d B = new Matrix2d(4d, 3d, 2d, 1d);

        /// <summary>
        /// Tests the constructor
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var matrix = new Matrix2d(0d, 0d, 
                                       0d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(0d, matrix.a);
            TestUtilities.AssertThatDoublesAreEqual(0d, matrix.b);
            TestUtilities.AssertThatDoublesAreEqual(0d, matrix.c);
            TestUtilities.AssertThatDoublesAreEqual(0d, matrix.d);

            matrix = new Matrix2d(1d, 1d, 
                                   1d, 1d);

            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.a);
            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.b);
            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.c);
            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.d);

            matrix = new Matrix2d(-1d, -1d, 
                                   -1d, -1d);

            TestUtilities.AssertThatDoublesAreEqual(-1d, matrix.a);
            TestUtilities.AssertThatDoublesAreEqual(-1d, matrix.b);
            TestUtilities.AssertThatDoublesAreEqual(-1d, matrix.c);
            TestUtilities.AssertThatDoublesAreEqual(-1d, matrix.d);

            matrix = new Matrix2d(1d, 2d, 
                                   3d, 4d);

            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.a);
            TestUtilities.AssertThatDoublesAreEqual(2d, matrix.b);
            TestUtilities.AssertThatDoublesAreEqual(3d, matrix.c);
            TestUtilities.AssertThatDoublesAreEqual(4d, matrix.d);
        }

        /// <summary>
        /// Tests the index accessor
        /// </summary>
        [Test]
        public void TestIndexAccess()
        {
            TestUtilities.AssertThatDoublesAreEqual(A.a, A[0]);
            TestUtilities.AssertThatDoublesAreEqual(A.b, A[1]);
            TestUtilities.AssertThatDoublesAreEqual(A.c, A[2]);
            TestUtilities.AssertThatDoublesAreEqual(A.d, A[3]);
        }

        /// <summary>
        /// Tests the index accessor when out of bounds access is attempted
        /// </summary>
        [Test]
        public void TestIndexAccessOutOfBounds()
        {
            Assert.Throws<IndexOutOfRangeException>(
                () =>
                {
                    var a = A[-1];
                });

            Assert.Throws<IndexOutOfRangeException>(
                () =>
                {
                    var a = A[4];
                });
        }

        /// <summary>
        /// Tests the inverse property
        /// </summary>
        [Test]
        public void TestInverseProperty()
        {
            var aInv = A.Inverse;
            var bInv = B.Inverse;

            // determine that both matrices are invertible
            Assert.IsFalse(Matrix2d.IsNaN(aInv));
            Assert.IsFalse(Matrix2d.IsNaN(bInv));

            // prove some properties of an inverted matrix

            // A(A^-1) = (A^-1)A = I
            Assert.AreEqual(Matrix2d.Identity, A * aInv);
            Assert.AreEqual(Matrix2d.Identity, aInv * A);

            // (A^-1)^-1 = A
            Assert.AreEqual(A, aInv.Inverse);

            // (At)^-1 = (A^-1)t
            Assert.AreEqual(A.Transpose.Inverse, aInv.Transpose);

            // (AB)^-1 = B^-1A^-1
            Assert.AreEqual((A * B).Inverse, bInv * aInv);

            // det(A^-1) = det(A)^-1
            Assert.AreEqual(aInv.Determinant, 1 / A.Determinant);
        }

        /// <summary>
        /// Tests the inverse property on a singular matrix
        /// </summary>
        [Test]
        public void TestInversePropertySingularMatrix()
        {
            // singular matrix
            var singular = new Matrix2d(2, 3, 2, 3);

            Assert.IsTrue(Matrix2d.IsNaN(singular.Inverse));
        }

        /// <summary>
        /// Tests the transpose property
        /// </summary>
        [Test]
        public void TestTransposeProperty()
        {
            var aTranspose = A.Transpose;
            var bTranspose = B.Transpose;

            // prove some properties of a transposed matrix

            // (At)t = A
            TestUtilities.AssertThatMatricesAreEqual(A, aTranspose.Transpose);

            // (A + B)t = At + Bt
            TestUtilities.AssertThatMatricesAreEqual((A + B).Transpose, 
                aTranspose + bTranspose);

            // (AB)t = BtAt
            TestUtilities.AssertThatMatricesAreEqual((A * B).Transpose, 
                bTranspose * aTranspose);

            // (cA)t = cAt where A is square
            TestUtilities.AssertThatMatricesAreEqual((2 * A).Transpose, 
                2 * A.Transpose);

            // det(At) = det(A)
            TestUtilities.AssertThatDoublesAreEqual(aTranspose.Determinant, 
                A.Determinant);

            // (At)^-1 = (A^-1)t
            TestUtilities.AssertThatMatricesAreEqual(aTranspose.Inverse, 
                A.Inverse.Transpose);
        }

        /// <summary>
        /// Tests the determinant property
        /// </summary>
        [Test]
        public void TestDeterminantProperty()
        {
            // check a simple case determinant
            Assert.AreEqual(-2d, A.Determinant);

            // prove some properties of a determinant

            // det(I) = 1 where I is the Identity matrix
            Assert.AreEqual(1d, Matrix2d.Identity.Determinant);

            // det(At) = det(A)
            Assert.AreEqual(A.Transpose.Determinant, A.Determinant);

            // det(AB) = det(A)det(b) where A, B are equal size and square
            Assert.AreEqual((A*B).Determinant, A.Determinant * B.Determinant);

            // det(cA) = c^n det(A) for a nxn matrix
            Assert.AreEqual((A * 4).Determinant, System.Math.Pow(4, 2) * A.Determinant);
        }

        /// <summary> 
        /// Tests the addition operator with a scalar parameter
        /// </summary>
        [Test]
        public void TestAdditionOperatorScalar()
        {
            var addend = 1d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a + addend, A.b + addend, A.c + addend,
                              A.d + addend), A + addend);

            addend = 0d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a + addend, A.b + addend, A.c + addend,
                              A.d + addend), A + addend);

            addend = -1d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a + addend, A.b + addend, A.c + addend,
                              A.d + addend), A + addend);

            addend = 0.5d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a + addend, A.b + addend, A.c + addend,
                              A.d + addend), A + addend);

            addend = -0.5d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a + addend, A.b + addend, A.c + addend,
                              A.d + addend), A + addend);
        }

        /// <summary>
        /// Tests the addition operator with two matrix parameters
        /// </summary>
        [Test]
        public void TestAdditionOperatorMatrices()
        {
            var addend = B;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a + addend.a, A.b + addend.b, A.c + addend.c,
                              A.d + addend.d), A + addend);

            addend = -B;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a + addend.a, A.b + addend.b, A.c + addend.c,
                              A.d + addend.d), A + addend);

            addend = B * 2;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a + addend.a, A.b + addend.b, A.c + addend.c,
                              A.d + addend.d), A + addend);

            addend = B * 0.5;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a + addend.a, A.b + addend.b, A.c + addend.c,
                              A.d + addend.d), A + addend);
        }

        /// <summary>
        /// Tests the subtraction operator with a scalar parameter
        /// </summary>
        [Test]
        public void TestSubtractionOperatorScalar()
        {
            var subtrahend = 1d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a - subtrahend, A.b - subtrahend, A.c - subtrahend,
                              A.d - subtrahend), A - subtrahend);

            subtrahend = 0d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a - subtrahend, A.b - subtrahend, A.c - subtrahend,
                              A.d - subtrahend), A - subtrahend);

            subtrahend = -1d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a - subtrahend, A.b - subtrahend, A.c - subtrahend,
                              A.d - subtrahend), A - subtrahend);

            subtrahend = 0.5d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a - subtrahend, A.b - subtrahend, A.c - subtrahend,
                              A.d - subtrahend), A - subtrahend);

            subtrahend = -0.5d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a - subtrahend, A.b - subtrahend, A.c - subtrahend,
                              A.d - subtrahend), A - subtrahend);
        }

        /// <summary>
        /// Tests the subtraction operator with two matrix parameters
        /// </summary>
        [Test]
        public void TestSubtractionOperatorMatrices()
        {
            var subtrahend = B;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a - subtrahend.a, A.b - subtrahend.b, A.c - subtrahend.c,
                              A.d - subtrahend.d), A - subtrahend);

            subtrahend = -B;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a - subtrahend.a, A.b - subtrahend.b, A.c - subtrahend.c,
                              A.d - subtrahend.d), A - subtrahend);

            subtrahend = B * 2;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a - subtrahend.a, A.b - subtrahend.b, A.c - subtrahend.c,
                              A.d - subtrahend.d), A - subtrahend);

            subtrahend = B * 0.5;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a - subtrahend.a, A.b - subtrahend.b, A.c - subtrahend.c,
                              A.d - subtrahend.d), A - subtrahend);
        }

        /// <summary>
        /// Tests the unary minus operator
        /// </summary>
        [Test]
        public void TestUnaryMinusOperator()
        {
            TestUtilities.AssertThatMatricesAreEqual(new Matrix2d(-A.a, -A.b,
                -A.c, -A.d), -A);

            TestUtilities.AssertThatMatricesAreEqual(new Matrix2d(-B.a, -B.b,
                -B.c, -B.d), -B);
        }

        /// <summary>
        /// Tests the multiplication operator with a scalar parameter
        /// </summary>
        [Test]
        public void TestMultiplicationOperatorScalar()
        {
            var factor = 1d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a * factor, A.b * factor, A.c * factor,
                              A.d * factor), A * factor);

            factor = 0d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a * factor, A.b * factor, A.c * factor,
                              A.d * factor), A * factor);

            factor = -1d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a * factor, A.b * factor, A.c * factor,
                              A.d * factor), A * factor);

            factor = 0.5d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a * factor, A.b * factor, A.c * factor,
                              A.d * factor), A * factor);

            factor = -0.5d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix2d(A.a * factor, A.b * factor, A.c * factor,
                              A.d * factor), A * factor);
        }

        /// <summary>
        /// Tests the multiplication operator with two matrix parameters
        /// </summary>
        [Test]
        public void TestMultiplicationOperatorMatrices()
        {
            TestUtilities.AssertThatMatricesAreEqual(new Matrix2d(8d, 5d, 
                20d, 13d), A * B);

            TestUtilities.AssertThatMatricesAreEqual(new Matrix2d(-8d, -5d,
                -20d, -13d), A * -B);

            TestUtilities.AssertThatMatricesAreEqual(new Matrix2d(-8d, -5d,
                -20d, -13d), -A * B);

            TestUtilities.AssertThatMatricesAreEqual(new Matrix2d(8d, 5d,
                20d, 13d), -A * -B);

            // prove that A x B != B x A
            TestUtilities.AssertThatMatricesAreEqual(new Matrix2d(13d, 20d,
                5d, 8d), B * A);
        }

        /// <summary>
        /// Tests the equality operator
        /// </summary>
        [Test]
        public void TestEqualityOperator()
        {
            Assert.IsFalse(A == B);
            Assert.IsFalse(B == A);

#pragma warning disable 1718
            Assert.IsTrue(A == A);
            Assert.IsTrue(B == B);
#pragma warning restore 1718
        }

        /// <summary>
        /// Tests the inequality operator
        /// </summary>
        [Test]
        public void TestInequalityOperator()
        {
            Assert.IsTrue(A != B);
            Assert.IsTrue(B != A);

#pragma warning disable 1718
            Assert.IsFalse(A != A);
            Assert.IsFalse(B != B);
#pragma warning restore 1718
        }

        /// <summary>
        /// Tests the IsNaN method
        /// </summary>
        [Test]
        public void TestIsNaNMethod()
        {
            Assert.IsFalse(Matrix2d.IsNaN(A));
            Assert.IsFalse(Matrix2d.IsNaN(B));

            Assert.IsTrue(Matrix2d.IsNaN(Matrix2d.NaN));
        }

        /// <summary>
        /// Tests the ToString method
        /// </summary>
        [Test]
        public void TestToStringMethod()
        {
            Assert.AreEqual($"[{A.a.ToString("+0.000;-0.000")}]" +
                            $"[{A.b.ToString("+0.000;-0.000")}]\n" +
                            $"[{A.c.ToString("+0.000;-0.000")}]" +
                            $"[{A.d.ToString("+0.000;-0.000")}]",
                            A.ToString());
        }

        /// <summary>
        /// Tests the Equals method
        /// </summary>
        [Test]
        public void TestEqualsMethod()
        {
            Assert.IsTrue(A.Equals(A));
            Assert.IsTrue(B.Equals(B));

            Assert.IsFalse(A.Equals(B));
            Assert.IsFalse(B.Equals(A));
        }

        /// <summary>
        /// Tests the GetHashCode method
        /// </summary>
        [Test]
        public void TestGetHashCodeMethod()
        {
            var AHash = A.a.GetHashCode() ^
                       (A.b.GetHashCode() << 8) ^
                       (A.c.GetHashCode() << 16) ^
                       (A.d.GetHashCode() << 24);

            Assert.AreEqual(AHash, A.GetHashCode());
        }
    }
}