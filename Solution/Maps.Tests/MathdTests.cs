using NUnit.Framework;

namespace Maps.Tests
{
    /// <summary>
    /// Series of tests for the Mathd class
    /// </summary>
    [TestFixture]
    internal sealed class MathdTests
    {
        [Test]
        public void TestClampMethod()
        {
            var a = 0d;

            TestUtilities.AssertThatDoublesAreEqual(0d, Mathd.Clamp(a, -1d, 1d));

            a = 10d;

            TestUtilities.AssertThatDoublesAreEqual(1d, Mathd.Clamp(a, -1d, 1d));

            a = -10d;

            TestUtilities.AssertThatDoublesAreEqual(-1d, Mathd.Clamp(a, -1d, 1d));
        }

        [Test]
        public void TestClamp01Method()
        {
            var a = 0d;

            TestUtilities.AssertThatDoublesAreEqual(0d, Mathd.Clamp01(a));

            a = 10d;

            TestUtilities.AssertThatDoublesAreEqual(1d, Mathd.Clamp01(a));

            a = -10d;

            TestUtilities.AssertThatDoublesAreEqual(0d, Mathd.Clamp01(a));
        }

        [Test]
        public void TestEpsilonEqualsMethod()
        {
            var a = 0d + Mathd.Epsilon;

            Assert.IsTrue(Mathd.EpsilonEquals(0d, a));

            a = 0d - Mathd.Epsilon;

            Assert.IsTrue(Mathd.EpsilonEquals(0d, a));

            a = 1d + Mathd.Epsilon;

            Assert.IsTrue(Mathd.EpsilonEquals(1d, a));

            a = 1d - Mathd.Epsilon;

            Assert.IsTrue(Mathd.EpsilonEquals(1d, a));
        }
    }
}