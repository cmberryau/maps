using NUnit.Framework;

namespace Maps.Tests
{
    /// <summary>
    /// Series of tests for the Mathf class
    /// </summary>
    [TestFixture]
    internal sealed class MathfTests
    {
        [Test]
        public void TestPowMethod()
        {
            var a = 2f;
            TestUtilities.AssertThatSinglesAreEqual(4f, Mathf.Pow(a, 2f));

            a = 3f;
            TestUtilities.AssertThatSinglesAreEqual(27f, Mathf.Pow(a, 3f));

            a = 4f;
            TestUtilities.AssertThatSinglesAreEqual(256f, Mathf.Pow(a, 4f));

            a = 5f;
            TestUtilities.AssertThatSinglesAreEqual(3125f, Mathf.Pow(a, 5f));
        }

        [Test]
        public void TestSqrtMethod()
        {
            var a = 4f;
            TestUtilities.AssertThatSinglesAreEqual(2f, Mathf.Sqrt(a));

            a = 9f;
            TestUtilities.AssertThatSinglesAreEqual(3f, Mathf.Sqrt(a));

            a = 16f;
            TestUtilities.AssertThatSinglesAreEqual(4f, Mathf.Sqrt(a));

            a = 25f;
            TestUtilities.AssertThatSinglesAreEqual(5f, Mathf.Sqrt(a));
        }

        [Test]
        public void TestTanMethod()
        {
            // angle of 0 = tangent 0
            var angle = 0f;

            TestUtilities.AssertThatSinglesAreEqual(0f, Mathf.Tan(angle));

            // angle of 45 degrees = tangent 1
            angle = 45f * Mathf.Deg2Rad;

            TestUtilities.AssertThatSinglesAreEqual(1f, Mathf.Tan(angle));
        }

        [Test]
        public void TestAtanMethod()
        {
            // tangent of 0 = 0
            var tangent = 0.0f;

            TestUtilities.AssertThatSinglesAreEqual(0f, Mathf.Atan(tangent));

            // tangent of 1 = 45
            tangent = 1.0f;

            TestUtilities.AssertThatSinglesAreEqual(Mathf.Pi * 0.25f, Mathf.Atan(tangent));
        }

        [Test]
        public void TestCosMethod()
        {
            // angle of 0 = cosine of 1
            TestUtilities.AssertThatSinglesAreEqual(1f, Mathf.Cos(0f * Mathf.Deg2Rad));

            // angle of 90 = cosine of 0
            TestUtilities.AssertThatSinglesAreEqual(0f, Mathf.Cos(90f * Mathf.Deg2Rad), Mathf.EpsilonE7);

            // angle of 180 = cosine of -1
            TestUtilities.AssertThatSinglesAreEqual(-1f, Mathf.Cos(180f * Mathf.Deg2Rad));

            // angle of 270 = cosine of 0
            TestUtilities.AssertThatSinglesAreEqual(0f, Mathf.Cos(270f * Mathf.Deg2Rad), Mathf.EpsilonE7);
        }

        [Test]
        public void TestAcosMethod()
        {
            // cosine of 1 = angle of 0
            TestUtilities.AssertThatSinglesAreEqual(0f * Mathf.Deg2Rad, Mathf.Acos(1f));

            // cosine of 0 = angle of 90
            TestUtilities.AssertThatSinglesAreEqual(90f * Mathf.Deg2Rad, Mathf.Acos(0f));

            // cosine of -1 = angle of 180
            TestUtilities.AssertThatSinglesAreEqual(180f * Mathf.Deg2Rad, Mathf.Acos(-1f));
        }

        [Test]
        public void TestSinMethod()
        {
            // angle of 0 = sine of 0
            TestUtilities.AssertThatSinglesAreEqual(0f, Mathf.Sin(0f));

            // angle of 90 = sine of 1
            TestUtilities.AssertThatSinglesAreEqual(1f, Mathf.Sin(90f * Mathf.Deg2Rad));

            // angle of 180 = sine of 0
            TestUtilities.AssertThatSinglesAreEqual(0f, Mathf.Sin(180f * Mathf.Deg2Rad), Mathf.EpsilonE7);

            // angle of 270 = sine of -1
            TestUtilities.AssertThatSinglesAreEqual(-1f, Mathf.Sin(270f * Mathf.Deg2Rad));
        }

        [Test]
        public void TestAsinMethod()
        {
            // sine of 0 = angle of 0
            TestUtilities.AssertThatSinglesAreEqual(0f * Mathf.Deg2Rad, Mathf.Asin(0f));

            // sine of 1 = angle of 90
            TestUtilities.AssertThatSinglesAreEqual(90f * Mathf.Deg2Rad, Mathf.Asin(1f));

            // sine of -1 = angle of 270
            TestUtilities.AssertThatSinglesAreEqual(-90f * Mathf.Deg2Rad, Mathf.Asin(-1f));
        }

        [Test]
        public void TestClampMethod()
        {
            var a = 0f;

            TestUtilities.AssertThatSinglesAreEqual(0f, Mathf.Clamp(a, -1f, 1f));

            a = 10f;

            TestUtilities.AssertThatSinglesAreEqual(1f, Mathf.Clamp(a, -1f, 1f));

            a = -10f;

            TestUtilities.AssertThatSinglesAreEqual(-1f, Mathf.Clamp(a, -1f, 1f));
        }

        [Test]
        public void TestClamp01Method()
        {
            var a = 0f;

            TestUtilities.AssertThatSinglesAreEqual(0f, Mathf.Clamp01(a));

            a = 10f;

            TestUtilities.AssertThatSinglesAreEqual(1f, Mathf.Clamp01(a));

            a = -10f;

            TestUtilities.AssertThatSinglesAreEqual(0f, Mathf.Clamp01(a));
        }

        [Test]
        public void TestAbsMethod()
        {
            var a = -1;

            TestUtilities.AssertThatSinglesAreEqual(1, Mathf.Abs(a));

            a = -10;

            TestUtilities.AssertThatSinglesAreEqual(10, Mathf.Abs(a));

            a = 10;

            TestUtilities.AssertThatSinglesAreEqual(10, Mathf.Abs(a));
        }

        [Test]
        public void TestEpsilonEqualsMethod()
        {
            var a = 0f + Mathf.Epsilon;

            Assert.IsTrue(Mathf.EpsilonEquals(0f, a));

            a = 0f - Mathf.Epsilon;

            Assert.IsTrue(Mathf.EpsilonEquals(0f, a));

            a = 1f + Mathf.Epsilon;

            Assert.IsTrue(Mathf.EpsilonEquals(1f, a));

            a = 1f - Mathf.Epsilon;

            Assert.IsTrue(Mathf.EpsilonEquals(1f, a));
        }

        [Test]
        public void TestMaxMethod()
        {
            var a = -10f;
            var b = 10f;

            TestUtilities.AssertThatSinglesAreEqual(10f, Mathf.Max(a, b));

            a = 10f;
            b = 20f;

            TestUtilities.AssertThatSinglesAreEqual(20f, Mathf.Max(a, b));

            a = -10f;
            b = -20f;

            TestUtilities.AssertThatSinglesAreEqual(-10f, Mathf.Max(a, b));
        }

        [Test]
        public void TestMinMethod()
        {
            var a = -10f;
            var b = 10f;

            TestUtilities.AssertThatSinglesAreEqual(-10f, Mathf.Min(a, b));

            a = 10f;
            b = 20f;

            TestUtilities.AssertThatSinglesAreEqual(10f, Mathf.Min(a, b));

            a = -10f;
            b = -20f;

            TestUtilities.AssertThatSinglesAreEqual(-20f, Mathf.Min(a, b));
        }
    }
}