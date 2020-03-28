using System;
using NUnit.Framework;

namespace Maps.Tests
{
    /// <summary>
    /// Series of tests for the Matrix4 class
    /// </summary>
    [TestFixture]
    internal sealed class Matrix4Tests
    {
        private static readonly Matrix4d A = new Matrix4d( 8d,  2d,  3d,  4d,
                                                             5d,  6d,  -7d,  8d,
                                                             9d, -10d, 11d, 12d,
                                                            13d, 14d, 15d, 16d);

        private static readonly Matrix4d B = new Matrix4d(-16d, 15d, 14d, -13d,
                                                            12d, 11d, 10d,  9d,
                                                             8d,  -7d,  6d,  5d,
                                                             -4d,  3d,  2d,  8d);

        /// <summary>
        /// Tests the constructor
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var matrix = new Matrix4d(0d, 0d, 0d, 0d,
                                       0d, 0d, 0d, 0d,
                                       0d, 0d, 0d, 0d,
                                       0d, 0d, 0d, 0d);

            TestUtilities.AssertThatDoublesAreEqual(0d, matrix.a);
            TestUtilities.AssertThatDoublesAreEqual(0d, matrix.b);
            TestUtilities.AssertThatDoublesAreEqual(0d, matrix.c);
            TestUtilities.AssertThatDoublesAreEqual(0d, matrix.d);
            TestUtilities.AssertThatDoublesAreEqual(0d, matrix.e);
            TestUtilities.AssertThatDoublesAreEqual(0d, matrix.f);
            TestUtilities.AssertThatDoublesAreEqual(0d, matrix.g);
            TestUtilities.AssertThatDoublesAreEqual(0d, matrix.h);
            TestUtilities.AssertThatDoublesAreEqual(0d, matrix.i);
            TestUtilities.AssertThatDoublesAreEqual(0d, matrix.j);
            TestUtilities.AssertThatDoublesAreEqual(0d, matrix.k);
            TestUtilities.AssertThatDoublesAreEqual(0d, matrix.l);
            TestUtilities.AssertThatDoublesAreEqual(0d, matrix.m);
            TestUtilities.AssertThatDoublesAreEqual(0d, matrix.n);
            TestUtilities.AssertThatDoublesAreEqual(0d, matrix.o);
            TestUtilities.AssertThatDoublesAreEqual(0d, matrix.p);

            matrix = new Matrix4d(1d, 1d, 1d, 1d,
                                   1d, 1d, 1d, 1d,
                                   1d, 1d, 1d, 1d,
                                   1d, 1d, 1d, 1d);

            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.a);
            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.b);
            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.c);
            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.d);
            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.e);
            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.f);
            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.g);
            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.h);
            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.i);
            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.j);
            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.k);
            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.l);
            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.m);
            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.n);
            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.o);
            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.p);

            matrix = new Matrix4d(-1d, -1d, -1d, -1d,
                                   -1d, -1d, -1d, -1d,
                                   -1d, -1d, -1d, -1d,
                                   -1d, -1d, -1d, -1d);

            TestUtilities.AssertThatDoublesAreEqual(-1d, matrix.a);
            TestUtilities.AssertThatDoublesAreEqual(-1d, matrix.b);
            TestUtilities.AssertThatDoublesAreEqual(-1d, matrix.c);
            TestUtilities.AssertThatDoublesAreEqual(-1d, matrix.d);
            TestUtilities.AssertThatDoublesAreEqual(-1d, matrix.e);
            TestUtilities.AssertThatDoublesAreEqual(-1d, matrix.f);
            TestUtilities.AssertThatDoublesAreEqual(-1d, matrix.g);
            TestUtilities.AssertThatDoublesAreEqual(-1d, matrix.h);
            TestUtilities.AssertThatDoublesAreEqual(-1d, matrix.i);
            TestUtilities.AssertThatDoublesAreEqual(-1d, matrix.j);
            TestUtilities.AssertThatDoublesAreEqual(-1d, matrix.k);
            TestUtilities.AssertThatDoublesAreEqual(-1d, matrix.l);
            TestUtilities.AssertThatDoublesAreEqual(-1d, matrix.m);
            TestUtilities.AssertThatDoublesAreEqual(-1d, matrix.n);
            TestUtilities.AssertThatDoublesAreEqual(-1d, matrix.o);
            TestUtilities.AssertThatDoublesAreEqual(-1d, matrix.p);

            matrix = new Matrix4d( 1d,  2d,  3d,  4d,
                                    5d,  6d,  7d,  8d,
                                    9d, 10d, 11d, 12d,
                                   13d, 14d, 15d, 16d);

            TestUtilities.AssertThatDoublesAreEqual(1d, matrix.a);
            TestUtilities.AssertThatDoublesAreEqual(2d, matrix.b);
            TestUtilities.AssertThatDoublesAreEqual(3d, matrix.c);
            TestUtilities.AssertThatDoublesAreEqual(4d, matrix.d);
            TestUtilities.AssertThatDoublesAreEqual(5d, matrix.e);
            TestUtilities.AssertThatDoublesAreEqual(6d, matrix.f);
            TestUtilities.AssertThatDoublesAreEqual(7d, matrix.g);
            TestUtilities.AssertThatDoublesAreEqual(8d, matrix.h);
            TestUtilities.AssertThatDoublesAreEqual(9d, matrix.i);
            TestUtilities.AssertThatDoublesAreEqual(10d, matrix.j);
            TestUtilities.AssertThatDoublesAreEqual(11d, matrix.k);
            TestUtilities.AssertThatDoublesAreEqual(12d, matrix.l);
            TestUtilities.AssertThatDoublesAreEqual(13d, matrix.m);
            TestUtilities.AssertThatDoublesAreEqual(14d, matrix.n);
            TestUtilities.AssertThatDoublesAreEqual(15d, matrix.o);
            TestUtilities.AssertThatDoublesAreEqual(16d, matrix.p);
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
            TestUtilities.AssertThatDoublesAreEqual(A.e, A[4]);
            TestUtilities.AssertThatDoublesAreEqual(A.f, A[5]);
            TestUtilities.AssertThatDoublesAreEqual(A.g, A[6]);
            TestUtilities.AssertThatDoublesAreEqual(A.h, A[7]);
            TestUtilities.AssertThatDoublesAreEqual(A.i, A[8]);
            TestUtilities.AssertThatDoublesAreEqual(A.j, A[9]);
            TestUtilities.AssertThatDoublesAreEqual(A.k, A[10]);
            TestUtilities.AssertThatDoublesAreEqual(A.l, A[11]);
            TestUtilities.AssertThatDoublesAreEqual(A.m, A[12]);
            TestUtilities.AssertThatDoublesAreEqual(A.n, A[13]);
            TestUtilities.AssertThatDoublesAreEqual(A.o, A[14]);
            TestUtilities.AssertThatDoublesAreEqual(A.p, A[15]);
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
                    var a = A[16];
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
            Assert.IsFalse(Matrix4d.IsNaN(aInv));
            Assert.IsFalse(Matrix4d.IsNaN(bInv));

            // prove some properties of an inverted matrix

            // A(A^-1) = B(A^-1) = I
            TestUtilities.AssertThatMatricesAreEqual(Matrix4d.Identity, 
                A * aInv, Mathd.EpsilonE15);
            TestUtilities.AssertThatMatricesAreEqual(Matrix4d.Identity,
                aInv * A, Mathd.EpsilonE15);

            // (A^-1)^-1 = A
            TestUtilities.AssertThatMatricesAreEqual(A, aInv.Inverse, 
                Mathd.EpsilonE14);

            // (At)^-1 = (A^-1)t
            TestUtilities.AssertThatMatricesAreEqual(A.Transpose.Inverse, 
                aInv.Transpose);

            // (AB)^-1 = B^-1A^-1
            TestUtilities.AssertThatMatricesAreEqual((A * B).Inverse, 
                bInv * aInv, Mathd.EpsilonE17);

            // det(A^-1) = det(A)^-1
            TestUtilities.AssertThatDoublesAreEqual(aInv.Determinant, 
                1 / A.Determinant, Mathd.EpsilonE20);
        }

        /// <summary>
        /// Tests the inverse property on a singular matrix
        /// </summary>
        [Test]
        public void TestInversePropertySingularMatrix()
        {
            // singular matrix
            var singular = new Matrix4d(1d, 2d, 3d, 4d,
                                       5d, 6d, 7d, 8d,
                                       9d, 10d, 11d, 12d,
                                       13d, 14d, 15d, 16d);

            Assert.IsTrue(Matrix4d.IsNaN(singular.Inverse));
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
            // prove some properties of a determinant

            // det(I) = 1 where I is the Identity matrix
            Assert.AreEqual(1d, Matrix4d.Identity.Determinant);

            // det(At) = det(A)
            Assert.AreEqual(A.Transpose.Determinant, A.Determinant);

            // det(AB) = det(A)det(b) where A, B are equal size and square
            Assert.AreEqual((A * B).Determinant, A.Determinant * B.Determinant);

            // det(cA) = c^n det(A) for a nxn matrix
            Assert.AreEqual((A * 6).Determinant, System.Math.Pow(6, 4) * A.Determinant);
        }

        /// <summary> 
        /// Tests the addition operator with a scalar parameter
        /// </summary>
        [Test]
        public void TestAdditionOperatorScalar()
        {
            var addend = 1d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a + addend, A.b + addend, A.c + addend,
                              A.d + addend, A.e + addend, A.f + addend,
                              A.g + addend, A.h + addend, A.i + addend,
                              A.j + addend, A.k + addend, A.l + addend,
                              A.m + addend, A.n + addend, A.o + addend,
                              A.p + addend), A + addend);

            addend = 0d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a + addend, A.b + addend, A.c + addend,
                              A.d + addend, A.e + addend, A.f + addend,
                              A.g + addend, A.h + addend, A.i + addend,
                              A.j + addend, A.k + addend, A.l + addend,
                              A.m + addend, A.n + addend, A.o + addend,
                              A.p + addend), A + addend);

            addend = -1d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a + addend, A.b + addend, A.c + addend,
                              A.d + addend, A.e + addend, A.f + addend,
                              A.g + addend, A.h + addend, A.i + addend,
                              A.j + addend, A.k + addend, A.l + addend,
                              A.m + addend, A.n + addend, A.o + addend,
                              A.p + addend), A + addend);

            addend = 0.5d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a + addend, A.b + addend, A.c + addend,
                              A.d + addend, A.e + addend, A.f + addend,
                              A.g + addend, A.h + addend, A.i + addend,
                              A.j + addend, A.k + addend, A.l + addend,
                              A.m + addend, A.n + addend, A.o + addend,
                              A.p + addend), A + addend);

            addend = -0.5d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a + addend, A.b + addend, A.c + addend,
                              A.d + addend, A.e + addend, A.f + addend,
                              A.g + addend, A.h + addend, A.i + addend,
                              A.j + addend, A.k + addend, A.l + addend,
                              A.m + addend, A.n + addend, A.o + addend,
                              A.p + addend), A + addend);
        }

        /// <summary>
        /// Tests the addition operator with two matrix parameters
        /// </summary>
        [Test]
        public void TestAdditionOperatorMatrices()
        {
            var addend = B;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a + addend.a, A.b + addend.b, A.c + addend.c,
                              A.d + addend.d, A.e + addend.e, A.f + addend.f,
                              A.g + addend.g, A.h + addend.h, A.i + addend.i,
                              A.j + addend.j, A.k + addend.k, A.l + addend.l,
                              A.m + addend.m, A.n + addend.n, A.o + addend.o,
                              A.p + addend.p), A + addend);

            addend = -B;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a + addend.a, A.b + addend.b, A.c + addend.c,
                              A.d + addend.d, A.e + addend.e, A.f + addend.f,
                              A.g + addend.g, A.h + addend.h, A.i + addend.i,
                              A.j + addend.j, A.k + addend.k, A.l + addend.l,
                              A.m + addend.m, A.n + addend.n, A.o + addend.o,
                              A.p + addend.p), A + addend);

            addend = B * 2;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a + addend.a, A.b + addend.b, A.c + addend.c,
                              A.d + addend.d, A.e + addend.e, A.f + addend.f,
                              A.g + addend.g, A.h + addend.h, A.i + addend.i,
                              A.j + addend.j, A.k + addend.k, A.l + addend.l,
                              A.m + addend.m, A.n + addend.n, A.o + addend.o,
                              A.p + addend.p), A + addend);

            addend = B * 0.5;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a + addend.a, A.b + addend.b, A.c + addend.c,
                              A.d + addend.d, A.e + addend.e, A.f + addend.f,
                              A.g + addend.g, A.h + addend.h, A.i + addend.i,
                              A.j + addend.j, A.k + addend.k, A.l + addend.l,
                              A.m + addend.m, A.n + addend.n, A.o + addend.o,
                              A.p + addend.p), A + addend);
        }

        /// <summary>
        /// Tests the subtraction operator with a scalar parameter
        /// </summary>
        [Test]
        public void TestSubtractionOperatorScalar()
        {
            var subtrahend = 1d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a - subtrahend, A.b - subtrahend, A.c - subtrahend,
                              A.d - subtrahend, A.e - subtrahend, A.f - subtrahend,
                              A.g - subtrahend, A.h - subtrahend, A.i - subtrahend,
                              A.j - subtrahend, A.k - subtrahend, A.l - subtrahend,
                              A.m - subtrahend, A.n - subtrahend, A.o - subtrahend,
                              A.p - subtrahend), A - subtrahend);

            subtrahend = 0d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a - subtrahend, A.b - subtrahend, A.c - subtrahend,
                              A.d - subtrahend, A.e - subtrahend, A.f - subtrahend,
                              A.g - subtrahend, A.h - subtrahend, A.i - subtrahend,
                              A.j - subtrahend, A.k - subtrahend, A.l - subtrahend,
                              A.m - subtrahend, A.n - subtrahend, A.o - subtrahend,
                              A.p - subtrahend), A - subtrahend);

            subtrahend = -1d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a - subtrahend, A.b - subtrahend, A.c - subtrahend,
                              A.d - subtrahend, A.e - subtrahend, A.f - subtrahend,
                              A.g - subtrahend, A.h - subtrahend, A.i - subtrahend,
                              A.j - subtrahend, A.k - subtrahend, A.l - subtrahend,
                              A.m - subtrahend, A.n - subtrahend, A.o - subtrahend,
                              A.p - subtrahend), A - subtrahend);

            subtrahend = 0.5d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a - subtrahend, A.b - subtrahend, A.c - subtrahend,
                              A.d - subtrahend, A.e - subtrahend, A.f - subtrahend,
                              A.g - subtrahend, A.h - subtrahend, A.i - subtrahend,
                              A.j - subtrahend, A.k - subtrahend, A.l - subtrahend,
                              A.m - subtrahend, A.n - subtrahend, A.o - subtrahend,
                              A.p - subtrahend), A - subtrahend);

            subtrahend = -0.5d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a - subtrahend, A.b - subtrahend, A.c - subtrahend,
                              A.d - subtrahend, A.e - subtrahend, A.f - subtrahend,
                              A.g - subtrahend, A.h - subtrahend, A.i - subtrahend,
                              A.j - subtrahend, A.k - subtrahend, A.l - subtrahend,
                              A.m - subtrahend, A.n - subtrahend, A.o - subtrahend,
                              A.p - subtrahend), A - subtrahend);
        }

        /// <summary>
        /// Tests the subtraction operator with two matrix parameters
        /// </summary>
        [Test]
        public void TestSubtractionOperatorMatrices()
        {
            var subtrahend = B;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a - subtrahend.a, A.b - subtrahend.b, A.c - subtrahend.c,
                              A.d - subtrahend.d, A.e - subtrahend.e, A.f - subtrahend.f,
                              A.g - subtrahend.g, A.h - subtrahend.h, A.i - subtrahend.i,
                              A.j - subtrahend.j, A.k - subtrahend.k, A.l - subtrahend.l,
                              A.m - subtrahend.m, A.n - subtrahend.n, A.o - subtrahend.o,
                              A.p - subtrahend.p), A - subtrahend);

            subtrahend = -B;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a - subtrahend.a, A.b - subtrahend.b, A.c - subtrahend.c,
                              A.d - subtrahend.d, A.e - subtrahend.e, A.f - subtrahend.f,
                              A.g - subtrahend.g, A.h - subtrahend.h, A.i - subtrahend.i,
                              A.j - subtrahend.j, A.k - subtrahend.k, A.l - subtrahend.l,
                              A.m - subtrahend.m, A.n - subtrahend.n, A.o - subtrahend.o,
                              A.p - subtrahend.p), A - subtrahend);

            subtrahend = B * 2;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a - subtrahend.a, A.b - subtrahend.b, A.c - subtrahend.c,
                              A.d - subtrahend.d, A.e - subtrahend.e, A.f - subtrahend.f,
                              A.g - subtrahend.g, A.h - subtrahend.h, A.i - subtrahend.i,
                              A.j - subtrahend.j, A.k - subtrahend.k, A.l - subtrahend.l,
                              A.m - subtrahend.m, A.n - subtrahend.n, A.o - subtrahend.o,
                              A.p - subtrahend.p), A - subtrahend);

            subtrahend = B * 0.5;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a - subtrahend.a, A.b - subtrahend.b, A.c - subtrahend.c,
                              A.d - subtrahend.d, A.e - subtrahend.e, A.f - subtrahend.f,
                              A.g - subtrahend.g, A.h - subtrahend.h, A.i - subtrahend.i,
                              A.j - subtrahend.j, A.k - subtrahend.k, A.l - subtrahend.l,
                              A.m - subtrahend.m, A.n - subtrahend.n, A.o - subtrahend.o,
                              A.p - subtrahend.p), A - subtrahend);
        }

        /// <summary>
        /// Tests the unary minus operator
        /// </summary>
        [Test]
        public void TestUnaryMinusOperator()
        {
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(-A.a, -A.b, -A.c,
                              -A.d, -A.e, -A.f,
                              -A.g, -A.h, -A.i,
                              -A.j, -A.k, -A.l,
                              -A.m, -A.n, -A.o,
                              -A.p), -A);

            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(-B.a, -B.b, -B.c,
                              -B.d, -B.e, -B.f,
                              -B.g, -B.h, -B.i,
                              -B.j, -B.k, -B.l,
                              -B.m, -B.n, -B.o,
                              -B.p), -B);
        }

        /// <summary>
        /// Tests the multiplication operator with a scalar parameter
        /// </summary>
        [Test]
        public void TestMultiplicationOperatorScalar()
        {
            var factor = 1d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a * factor, A.b * factor, A.c * factor,
                              A.d * factor, A.e * factor, A.f * factor,
                              A.g * factor, A.h * factor, A.i * factor,
                              A.j * factor, A.k * factor, A.l * factor,
                              A.m * factor, A.n * factor, A.o * factor,
                              A.p * factor), A * factor);

            factor = 0d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a * factor, A.b * factor, A.c * factor,
                              A.d * factor, A.e * factor, A.f * factor,
                              A.g * factor, A.h * factor, A.i * factor,
                              A.j * factor, A.k * factor, A.l * factor,
                              A.m * factor, A.n * factor, A.o * factor,
                              A.p * factor), A * factor);

            factor = -1d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a * factor, A.b * factor, A.c * factor,
                              A.d * factor, A.e * factor, A.f * factor,
                              A.g * factor, A.h * factor, A.i * factor,
                              A.j * factor, A.k * factor, A.l * factor,
                              A.m * factor, A.n * factor, A.o * factor,
                              A.p * factor), A * factor);

            factor = 0.5d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a * factor, A.b * factor, A.c * factor,
                              A.d * factor, A.e * factor, A.f * factor,
                              A.g * factor, A.h * factor, A.i * factor,
                              A.j * factor, A.k * factor, A.l * factor,
                              A.m * factor, A.n * factor, A.o * factor,
                              A.p * factor), A * factor);

            factor = -0.5d;
            TestUtilities.AssertThatMatricesAreEqual(
                new Matrix4d(A.a * factor, A.b * factor, A.c * factor,
                              A.d * factor, A.e * factor, A.f * factor,
                              A.g * factor, A.h * factor, A.i * factor,
                              A.j * factor, A.k * factor, A.l * factor,
                              A.m * factor, A.n * factor, A.o * factor,
                              A.p * factor), A * factor);
        }

        /// <summary>
        /// Tests the multiplication operator with two matrix parameters
        /// </summary>
        [Test]
        public void TestMultiplicationOperatorMatrices()
        {
            TestUtilities.AssertThatMatricesAreEqual(new Matrix4d(
                 -96d,  133d, 158d, -39d,
                 -96d,  214d, 104d,  18d,
                 -224d, -16d, 116d, -56d,
                 16d,   292d, 444d, 160d), A * B);

            TestUtilities.AssertThatMatricesAreEqual(new Matrix4d(
                 96d, -133d, -158d, 39d,
                 96d, -214d, -104d, -18d,
                 224d, 16d, -116d, 56d,
                 -16d, -292d, -444d, -160d), A * -B);

            TestUtilities.AssertThatMatricesAreEqual(new Matrix4d(
                 96d, -133d, -158d, 39d,
                 96d, -214d, -104d, -18d,
                 224d, 16d, -116d, 56d,
                 -16d, -292d, -444d, -160d), -A * B);

            TestUtilities.AssertThatMatricesAreEqual(new Matrix4d(
                 -96d, 133d, 158d, -39d,
                 -96d, 214d, 104d, 18d,
                 -224d, -16d, 116d, -56d,
                 16d, 292d, 444d, 160d), -A * -B);

            // prove that A x B != B x A
            TestUtilities.AssertThatMatricesAreEqual(new Matrix4d(
                 -96d, -264d, -194d, 16d,
                 358d, 116d, 204d, 400d,
                 148d, -16d, 214d, 128d,
                 105d, 102d, 109d, 160d), B * A);
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
            Assert.IsFalse(Matrix4d.IsNaN(A));
            Assert.IsFalse(Matrix4d.IsNaN(B));

            Assert.IsTrue(Matrix4d.IsNaN(Matrix4d.NaN));
        }

        /// <summary>
        /// Tests the ToString method
        /// </summary>
        [Test]
        public void TestToStringMethod()
        {
            Assert.AreEqual($"[{A.a.ToString("+0.000;-0.000")}]" +
                            $"[{A.b.ToString("+0.000;-0.000")}]" +
                            $"[{A.c.ToString("+0.000;-0.000")}]" +
                            $"[{A.d.ToString("+0.000;-0.000")}]\n" +
                            $"[{A.e.ToString("+0.000;-0.000")}]" +
                            $"[{A.f.ToString("+0.000;-0.000")}]" +
                            $"[{A.g.ToString("+0.000;-0.000")}]" +
                            $"[{A.h.ToString("+0.000;-0.000")}]\n" +
                            $"[{A.i.ToString("+0.000;-0.000")}]" +
                            $"[{A.j.ToString("+0.000;-0.000")}]" +
                            $"[{A.k.ToString("+0.000;-0.000")}]" +
                            $"[{A.l.ToString("+0.000;-0.000")}]\n" +
                            $"[{A.m.ToString("+0.000;-0.000")}]" +
                            $"[{A.n.ToString("+0.000;-0.000")}]" +
                            $"[{A.o.ToString("+0.000;-0.000")}]" +
                            $"[{A.p.ToString("+0.000;-0.000")}]", 
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
                       (A.b.GetHashCode() << 2) ^
                       (A.c.GetHashCode() << 4) ^
                       (A.d.GetHashCode() << 6) ^
                       (A.e.GetHashCode() << 8) ^
                       (A.f.GetHashCode() << 10) ^
                       (A.g.GetHashCode() << 12) ^
                       (A.h.GetHashCode() << 14) ^
                       (A.i.GetHashCode() << 16) ^
                       (A.j.GetHashCode() << 18) ^
                       (A.k.GetHashCode() << 20) ^
                       (A.l.GetHashCode() << 22) ^
                       (A.m.GetHashCode() << 24) ^
                       (A.n.GetHashCode() << 26) ^
                       (A.o.GetHashCode() << 28) ^
                       (A.p.GetHashCode() << 30);

            Assert.AreEqual(AHash, A.GetHashCode());
        }
    }
}