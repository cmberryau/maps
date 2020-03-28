using Maps.Geometry;
using NUnit.Framework;

namespace Maps.Tests.Geometry
{
    /// <summary>
    /// Series of tests for the Bounds2d class
    /// </summary>
    [TestFixture]
    internal sealed class Bounds2dTests
    {
        /// <summary>
        /// Tests the Bounds2d & LineSegment2d intersection against
        /// some very simple cases
        /// </summary>
        [Test]
        public void TestLineSegmentIntersectionSimpleCases()
        {
            // simple pass through case
            var a = Vector2d.Zero;
            var b = Vector2d.One;
            var bounds = new Bounds2d(Vector2d.Midpoint(a, b), Vector2d.Abs(b - a));

            var p0 = new Vector2d(-1, 0.5);
            var p1 = new Vector2d(2, 0.5);
            var segment = new LineSegment2d(p0, p1);
            var intersectedSegment = Bounds2d.Intersection(bounds, segment);

            Assert.IsNotNull(intersectedSegment);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.5), intersectedSegment.P0);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.5), intersectedSegment.P1);

            // reverse case
            segment = new LineSegment2d(p1, p0);
            intersectedSegment = Bounds2d.Intersection(bounds, segment);

            Assert.IsNotNull(intersectedSegment);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.5), intersectedSegment.P0);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.5), intersectedSegment.P1);
        }

        /// <summary>
        /// Tests the Bounds2d & LineSegment2d intersection against
        /// some more complex cases
        /// </summary>
        [Test]
        public void TestLineSegmentIntersectionComplexCases()
        {
            // simple pass through case
            var a = Vector2d.Zero;
            var b = Vector2d.One;
            var bounds = new Bounds2d(Vector2d.Midpoint(a, b), Vector2d.Abs(b - a));

            var p0 = new Vector2d(-2, -2);
            var p1 = new Vector2d(2, 2);
            var segment = new LineSegment2d(p0, p1);
            var intersectedSegment = Bounds2d.Intersection(bounds, segment);

            Assert.IsNotNull(intersectedSegment);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0), intersectedSegment.P0);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 1), intersectedSegment.P1);

            // reverse case
            segment = new LineSegment2d(p1, p0);
            intersectedSegment = Bounds2d.Intersection(bounds, segment);

            Assert.IsNotNull(intersectedSegment);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 1), intersectedSegment.P0);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0), intersectedSegment.P1);
        }

        /// <summary>
        /// Tests the Bounds2d & LineSegment2d intersection against cases where
        /// segments disjoint of the bounding box
        /// </summary>
        [Test]
        public void TestLineSegmentIntersectionDisjointCases()
        {
            // simple disjoint case, disjoint in y
            var a = Vector2d.Zero;
            var b = Vector2d.One;

            var bounds = new Bounds2d(Vector2d.Midpoint(a, b), Vector2d.Abs(b - a));

            var p0 = new Vector2d(-1, 2);
            var p1 = new Vector2d(2, 2);
            var segment = new LineSegment2d(p0, p1);
            var intersectedSegment = Bounds2d.Intersection(bounds, segment);

            Assert.IsNull(intersectedSegment);

            // reverse case
            segment = new LineSegment2d(p1, p0);
            intersectedSegment = Bounds2d.Intersection(bounds, segment);

            Assert.IsNull(intersectedSegment);

            // disjoint in x
            p0 = new Vector2d(-1, 0.5);
            p1 = new Vector2d(-2, 0.5);
            segment = new LineSegment2d(p0, p1);
            intersectedSegment = Bounds2d.Intersection(bounds, segment);

            Assert.IsNull(intersectedSegment);

            // reverse case
            segment = new LineSegment2d(p1, p0);
            intersectedSegment = Bounds2d.Intersection(bounds, segment);

            Assert.IsNull(intersectedSegment);
        }

        /// <summary>
        /// Tests the Bounds2d & LineSegment2d intersection against cases where
        /// segments are already contained in the bounding box
        /// </summary>
        [Test]
        public void TestLineSegmentIntersectionContainedCases()
        {
            // simple contained case
            var a = Vector2d.Zero;
            var b = Vector2d.One;

            var bounds = new Bounds2d(Vector2d.Midpoint(a, b), Vector2d.Abs(b - a));

            var p0 = new Vector2d(0.25, 0.25);
            var p1 = new Vector2d(0.75, 0.75);
            var segment = new LineSegment2d(p0, p1);

            var intersectedSegment = Bounds2d.Intersection(bounds, segment);

            Assert.AreSame(segment, intersectedSegment);

            // reverse case
            segment = new LineSegment2d(p1, p0);
            intersectedSegment = Bounds2d.Intersection(bounds, segment);

            Assert.AreSame(segment, intersectedSegment);
        }
    }
}