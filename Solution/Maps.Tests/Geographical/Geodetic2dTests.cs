using Maps.Geographical;
using NUnit.Framework;

namespace Maps.Tests.Geographical
{
    /// <summary>
    /// Series of tests for the Geodetic2d class
    /// </summary>
    [TestFixture]
    internal sealed class Geodetic2dTests
    {
        //[Test]
        //public void TestOffsetMethod()
        //{
        //    // no offset should not change the coordinate
        //    var offset = Geodetic2d.Offset(Ingolstadt, 0d, 0d);
        //    TestUtilities.AssertThatGeodetic2dsAreEqual(Ingolstadt, offset);

        //    throw new NotImplementedException("Requires more real-world data for validation");
        //}

        //[Test]
        //public void TestDistanceMethod()
        //{
        //    // currently using haversine, which can't be more than %0.5 accurate due
        //    // to earth's radius approximation, Vicenty's formulae could fix this
        //    TestUtilities.AssertThatDoublesAreEqual(16170103.5936457d, 
        //        Geodetic2d.Distance(Ingolstadt, Cranbourne), Mathd.EpsilonE7);

        //    throw new NotImplementedException("Requires more real-world data for validation");
        //}

        [Test]
        public void TestCourseMethod()
        {
            // initial offsets by .1 degree - significant deviation along 
            // longitudinal axis is expected

            // offset to the north
            var offset = new Geodetic2d(48.86450d, 11.4209873d);
            TestUtilities.AssertThatDoublesAreEqual(0d, 
                Geodetic2d.Course(TestUtilities.Ingolstadt, offset));

            // offset to the east
            offset = new Geodetic2d(48.76450d, 11.5209873d);
            TestUtilities.AssertThatDoublesAreEqual(90d, 
                Geodetic2d.Course(TestUtilities.Ingolstadt, offset), 1d);

            // offset to the south
            offset = new Geodetic2d(48.66450d, 11.4209873d);
            TestUtilities.AssertThatDoublesAreEqual(180d, 
                Geodetic2d.Course(TestUtilities.Ingolstadt, offset));

            // offset to the west
            offset = new Geodetic2d(48.76450d, 11.3209873d);
            TestUtilities.AssertThatDoublesAreEqual(270d, 
                Geodetic2d.Course(TestUtilities.Ingolstadt, offset), 1d);

            // southern hemisphere testing

            // offset to the north
            offset = new Geodetic2d(-38.01074d, 145.25929d);
            TestUtilities.AssertThatDoublesAreEqual(0d,
                Geodetic2d.Course(TestUtilities.Cranbourne, offset));

            // offset to the east
            offset = new Geodetic2d(-38.11074d, 145.35929d);
            TestUtilities.AssertThatDoublesAreEqual(90d,
                Geodetic2d.Course(TestUtilities.Cranbourne, offset), Mathd.EpsilonE1);

            // offset to the south
            offset = new Geodetic2d(-38.21074d, 145.25929d);
            TestUtilities.AssertThatDoublesAreEqual(180d,
                Geodetic2d.Course(TestUtilities.Cranbourne, offset));

            // offset to the west
            offset = new Geodetic2d(-38.11074d, 145.15929d);
            TestUtilities.AssertThatDoublesAreEqual(270d,
                Geodetic2d.Course(TestUtilities.Cranbourne, offset), Mathd.EpsilonE1);
        }

        //[Test]
        //public void TestMidpointMethod()
        //{
        //    var midpoint = Geodetic2d.Midpoint(Ingolstadt, Cranbourne);

        //    throw new NotImplementedException("Requires more real-world data for validation");
        //}

        //[Test]
        //public void TestLerpMethod()
        //{
        //    var lerpedPoint = Geodetic2d.Lerp(Ingolstadt, Cranbourne, 0.999d);

        //    throw new NotImplementedException("Requires more real-world data for validation");
        //}
    }
}