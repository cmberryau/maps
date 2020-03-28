using Maps.Geographical;
using NUnit.Framework;

namespace Maps.Tests.Geographical
{
    /// <summary>
    /// A series of tests for the GeodeticLineSegment2d class
    /// </summary>
    [TestFixture]
    internal sealed class GeodeticLineSegment2dTests
    {
        /// <summary>
        /// Tests the Distance method
        /// </summary>
        [Test]
        public void TestDistanceMethod()
        {
            var a = Geodetic2d.Meridian;
            var b = Geodetic2d.Meridian;
            var c = Geodetic2d.Meridian;
            var segment = new GeodeticLineSegment2d(a, b);

            Assert.AreEqual(0d, segment.Distance(c));

            var offset = 50d;
            a = TestUtilities.Ingolstadt;
            b = Geodetic2d.Offset(a, 1000d, (double)CardinalDirection.East);
            c = Geodetic2d.Offset(a, 500d, (double)CardinalDirection.East);
            c = Geodetic2d.Offset(c, offset, (double)CardinalDirection.North);
            segment = new GeodeticLineSegment2d(a, b);

            TestUtilities.AssertThatDoublesAreEqual(offset, segment.Distance(c), 
                Mathd.EpsilonE1);
        }
    }
}