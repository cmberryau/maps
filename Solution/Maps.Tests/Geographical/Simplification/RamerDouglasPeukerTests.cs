using System.Collections.Generic;
using Maps.Geographical;
using Maps.Geographical.Simplification;
using NUnit.Framework;

namespace Maps.Tests.Geographical.Simplification
{
    /// <summary>
    /// Tests the Ramer-Douglas-Peuker simplifier
    /// </summary>
    [TestFixture]
    internal sealed class RamerDouglasPeukerTests
    {
        /// <summary>
        /// Tests the constructor
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var simplifier = new RamerDouglasPeukerSimplifier();
        }

        /// <summary>
        /// Tests the simplify method with coordinates in a very simple case
        /// </summary>
        [Test]
        public void TestSimplifyMethodCoordinatesSimpleCase()
        {
            var a = Geodetic2d.Meridian;
            var b = Geodetic2d.Offset(a, 10d, (double) CardinalDirection.East);
            var c = Geodetic2d.Offset(b, 10d, (double) CardinalDirection.SouthEast);

            var coordinates = new []
            {
                a,
                b,
                c
            };

            var simplifier = new RamerDouglasPeukerSimplifier();
            var simplifiedCoordinates = simplifier.Simplify(coordinates);

            Assert.IsNotNull(simplifiedCoordinates);
            Assert.IsNotEmpty(simplifiedCoordinates);
            Assert.AreEqual(3, simplifiedCoordinates.Count);

            TestUtilities.AssertThatGeodetic2dsAreEqual(a, simplifiedCoordinates[0]);
            TestUtilities.AssertThatGeodetic2dsAreEqual(b, simplifiedCoordinates[1]);
            TestUtilities.AssertThatGeodetic2dsAreEqual(c, simplifiedCoordinates[2]);

            simplifier = new RamerDouglasPeukerSimplifier(5d);
            simplifiedCoordinates = simplifier.Simplify(coordinates);

            Assert.IsNotNull(simplifiedCoordinates);
            Assert.IsNotEmpty(simplifiedCoordinates);
            Assert.AreEqual(2, simplifiedCoordinates.Count);

            TestUtilities.AssertThatGeodetic2dsAreEqual(a, simplifiedCoordinates[0]);
            TestUtilities.AssertThatGeodetic2dsAreEqual(c, simplifiedCoordinates[1]);
        }

        /// <summary>
        /// Tests the simplify method with a linestrip in a very simple case
        /// </summary>
        [Test]
        public void TestSimplifyMethodLineStripSimpleCase()
        {
            var a = Geodetic2d.Meridian;
            var b = Geodetic2d.Offset(a, 10d, (double)CardinalDirection.East);
            var c = Geodetic2d.Offset(b, 10d, (double)CardinalDirection.SouthEast);

            var coordinates = new[]
            {
                a,
                b,
                c
            };

            var linestrip = new GeodeticLineStrip2d(coordinates);
            var simplifier = new RamerDouglasPeukerSimplifier();
            var simplifiedLinestrip = simplifier.Simplify(linestrip);

            Assert.IsNotNull(simplifiedLinestrip);
            Assert.AreEqual(3, simplifiedLinestrip.Count);

            TestUtilities.AssertThatGeodetic2dsAreEqual(a, simplifiedLinestrip[0]);
            TestUtilities.AssertThatGeodetic2dsAreEqual(b, simplifiedLinestrip[1]);
            TestUtilities.AssertThatGeodetic2dsAreEqual(c, simplifiedLinestrip[2]);

            simplifier = new RamerDouglasPeukerSimplifier(5d);
            simplifiedLinestrip = simplifier.Simplify(linestrip);

            Assert.IsNotNull(simplifiedLinestrip);
            Assert.AreEqual(2, simplifiedLinestrip.Count);

            TestUtilities.AssertThatGeodetic2dsAreEqual(a, simplifiedLinestrip[0]);
            TestUtilities.AssertThatGeodetic2dsAreEqual(c, simplifiedLinestrip[1]);
        }

        /// <summary>
        /// Tests the simplify method with a linestrip in a very simple case
        /// </summary>
        [Test]
        public void TestSimplifyMethodPolygonSimpleCase()
        {
            var a = Geodetic2d.Meridian;
            var b = Geodetic2d.Offset(a, 10d, (double)CardinalDirection.East);
            var c = Geodetic2d.Offset(b, 10d, (double)CardinalDirection.SouthEast);
            var d = Geodetic2d.Offset(c, 10d, (double)CardinalDirection.SouthWest);
            var e = Geodetic2d.Offset(d, 1d, (double)CardinalDirection.NorthWest);

            var coordinates = new[]
            {
                a,
                b,
                c,
                d,
                e,
            };

            var polygon = new GeodeticPolygon2d((IList<Geodetic2d>)coordinates);
            var simplifier = new RamerDouglasPeukerSimplifier();
            var simplifiedPolygon = simplifier.Simplify(polygon);

            Assert.IsNotNull(simplifiedPolygon);
            Assert.AreEqual(6, simplifiedPolygon.Count);

            TestUtilities.AssertThatGeodetic2dsAreEqual(a, simplifiedPolygon[0]);
            TestUtilities.AssertThatGeodetic2dsAreEqual(b, simplifiedPolygon[1]);
            TestUtilities.AssertThatGeodetic2dsAreEqual(c, simplifiedPolygon[2]);
            TestUtilities.AssertThatGeodetic2dsAreEqual(d, simplifiedPolygon[3]);
            TestUtilities.AssertThatGeodetic2dsAreEqual(e, simplifiedPolygon[4]);
            TestUtilities.AssertThatGeodetic2dsAreEqual(a, simplifiedPolygon[5]);

            simplifier = new RamerDouglasPeukerSimplifier(1d);
            simplifiedPolygon = simplifier.Simplify(polygon);

            Assert.IsNotNull(simplifiedPolygon);
            Assert.AreEqual(5, simplifiedPolygon.Count);

            TestUtilities.AssertThatGeodetic2dsAreEqual(a, simplifiedPolygon[0]);
            TestUtilities.AssertThatGeodetic2dsAreEqual(b, simplifiedPolygon[1]);
            TestUtilities.AssertThatGeodetic2dsAreEqual(c, simplifiedPolygon[2]);
            TestUtilities.AssertThatGeodetic2dsAreEqual(d, simplifiedPolygon[3]);
            TestUtilities.AssertThatGeodetic2dsAreEqual(a, simplifiedPolygon[4]);
        }
    }
}