using Maps.Geographical;
using Maps.Geographical.Projection;
using NUnit.Framework;

namespace Maps.Tests.Geographical.Projection
{
    /// <summary>
    /// Series of tests for the LambertAzimuthalEqualAreaProjection class
    /// </summary>
    [TestFixture]
    internal sealed class LambertAzimuthalEqualAreaProjectionTests
    {
        /// <summary>
        /// Tests the project method
        /// </summary>
        [Test]
        public void TestProjectMethod()
        {
            var proj = new LambertAzimuthalEqualAreaProjection(Geodetic2d.Meridian);
            var coord = Geodetic2d.Meridian;
            var projectedCoord = proj.Forward(coord);

            TestUtilities.AssertThatVector2dsAreEqual(projectedCoord.xy, Vector2d.Zero);

            proj = new LambertAzimuthalEqualAreaProjection(TestUtilities.Ingolstadt);
            coord = Geodetic2d.Offset(TestUtilities.Ingolstadt, 100d, 0d);
            projectedCoord = proj.Forward(coord);

            TestUtilities.AssertThatGeodetic2dsAreEqual(
                proj.Reverse(projectedCoord).Geodetic2d, coord);
        }
    }
}