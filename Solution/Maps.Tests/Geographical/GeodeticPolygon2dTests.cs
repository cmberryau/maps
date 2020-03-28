using System.Collections.Generic;
using Maps.Geographical;
using NUnit.Framework;

namespace Maps.Tests.Geographical
{
    /// <summary>
    /// Series of tests for the GeodeticPolygon2d class
    /// </summary>
    [TestFixture]
    internal sealed class GeodeticPolygon2dTests
    {
        /// <summary>
        /// Tests the constructor
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var a = Geodetic2d.Meridian;
            var b = Geodetic2d.Offset(a, 10d, (double)CardinalDirection.North);
            var c = Geodetic2d.Offset(b, 10d, (double)CardinalDirection.East);
            var d = Geodetic2d.Offset(c, 10d, (double)CardinalDirection.South);

            var coordinates = new[]
            {
                a,
                b,
                c,
                d,
            };

            var polygon = new GeodeticPolygon2d((IList<Geodetic2d>)coordinates);
        }

        /// <summary>
        /// Tests the area property in a very simple case
        /// </summary>
        [Test]
        public void TestAreaPropertySimpleCase()
        {
            var a = Geodetic2d.Meridian;
            var b = Geodetic2d.Offset(a, 10d, (double)CardinalDirection.North);
            var c = Geodetic2d.Offset(b, 10d, (double)CardinalDirection.East);
            var d = Geodetic2d.Offset(c, 10d, (double)CardinalDirection.South);

            var coordinates = new[]
            {
                a,
                b,
                c,
                d,
            };

            var polygon = new GeodeticPolygon2d((IList<Geodetic2d>)coordinates);
            Assert.AreEqual(100d, polygon.Area, Mathd.EpsilonE6);

            a = Geodetic2d.Meridian;
            b = Geodetic2d.Offset(a, 100d, (double)CardinalDirection.North);
            c = Geodetic2d.Offset(b, 100d, (double)CardinalDirection.East);
            d = Geodetic2d.Offset(c, 100d, (double)CardinalDirection.South);

            coordinates = new[]
            {
                a,
                b,
                c,
                d,
            };

            polygon = new GeodeticPolygon2d((IList<Geodetic2d>)coordinates);
            Assert.AreEqual(10000d, polygon.Area, Mathd.EpsilonE5);
        }
    }
}