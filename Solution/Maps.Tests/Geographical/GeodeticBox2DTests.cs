using Maps.Geographical;
using Maps.Tests.Geometry;
using NUnit.Framework;

namespace Maps.Tests.Geographical
{
    /// <summary>
    /// A series of tests for the GeodeticBox2d class
    /// </summary>
    [TestFixture]
    internal sealed class GeodeticBox2dTests
    {
        /// <summary>
        /// Creates the default sized geodetic box
        /// </summary>
        /// <param name="centre">The centre of the box</param>
        public GeodeticBox2d CreateDefaultBox(Geodetic2d centre)
        {
            return new GeodeticBox2d(centre, 100);
        }

        /// <summary>
        /// Creates a diamond shaped geodetic linestrip
        /// </summary>
        /// <param name="centre">The centre of the linestrip</param>
        public static GeodeticLineStrip2d CreateDiamondLineStrip(
            Geodetic2d centre)
        {
            var translation = new Vector2d(centre);
            return new GeodeticLineStrip2d(Box2dTests.CreateDiamondLineStrip(
                translation));
        }

        /// <summary>
        /// Creates a spiral shaped geodetic linestrip
        /// </summary>
        /// <param name="centre">The centre of the linestrip</param>
        /// <param name="res">The number of points to generate</param>
        /// <param name="cycles">The number of full circle movements</param>
        /// <param name="rate">The rate of expansion from the centre</param>
        public static GeodeticLineStrip2d CreateSpiralLineStrip(
            Geodetic2d centre, int res, int cycles, double rate)
        {
            var translation = new Vector2d(centre);

            return new GeodeticLineStrip2d(Box2dTests.CreateSpiralLineStrip(
                translation, res, cycles, rate));
        }

        /// <summary>
        /// Tests the constructor that uses two coordinates for params
        /// </summary>
        [Test]
        public void TestConstructorAB()
        {
            var a = TestUtilities.Ingolstadt;
            var b = Geodetic2d.Offset(a, 100, 
                (double)CardinalDirection.SouthEast);

            var box = new GeodeticBox2d(a, b);

            TestUtilities.AssertThatGeodetic2dsAreEqual(a, box.A);
            TestUtilities.AssertThatGeodetic2dsAreEqual(b, box.B);
        }

        /// <summary>
        /// Tests the constructor that uses one coordinate and a size
        /// for params
        /// </summary>
        [Test]
        public void TestConstructorCentreSize()
        {
            var a = TestUtilities.Ingolstadt;
            var size = 100;
            var box = new GeodeticBox2d(a, size);

            var offseta = Geodetic2d.Offset(a, size * 0.5d,
                (double)CardinalDirection.NorthWest);
            var offsetb = Geodetic2d.Offset(a, size * 0.5d,
                (double)CardinalDirection.SouthEast);

            TestUtilities.AssertThatGeodetic2dsAreEqual(offseta, box.A);
            TestUtilities.AssertThatGeodetic2dsAreEqual(offsetb, box.B);
        }

        /// <summary>
        /// Tests clipping geodetic linestrips with some simple cases
        /// </summary>
        [Test]
        public void TestGeodeticLineStrip2dClipSimpleCases()
        {
            
        }
    }
}