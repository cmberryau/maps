using Maps.Geometry;
using NUnit.Framework;

namespace Maps.Tests.Geometry
{
    /// <summary>
    /// Series of tests for the Line2d class
    /// </summary>
    [TestFixture]
    internal sealed class Line2dTests
    {
        /// <summary>
        /// Tests the constructor of the Line2d class
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var p0 = Vector2d.Zero;
            var p1 = Vector2d.One;

            var line = new Line2d(p0, p1 - p0);

            TestUtilities.AssertThatVector2dsAreEqual(p0, line.P0);
            TestUtilities.AssertThatVector2dsAreEqual(p1 - p0, line.Direction);

            p0 = -Vector2d.One;
            p1 = Vector2d.One;

            line = new Line2d(p0, p1 - p0);

            TestUtilities.AssertThatVector2dsAreEqual(p0, line.P0);
            TestUtilities.AssertThatVector2dsAreEqual(p1 - p0, line.Direction);
        }

        /// <summary>
        /// Tests the evaluation of the side of a line points lie on
        /// </summary>
        [Test]
        public void TestSideMethod()
        {
            // simple right case
            var p0 = Vector2d.Zero;
            var p1 = Vector2d.Up;

            var line = new Line2d(p0, p1 - p0);
            var point = Vector2d.Right;

            Assert.AreEqual(RelativePosition.Right, line.Side(point));

            // simple left case
            point = Vector2d.Left;
            Assert.AreEqual(RelativePosition.Left, line.Side(point));

            // simple colinear case
            point = Vector2d.Up;
            Assert.AreEqual(RelativePosition.Centre, line.Side(point));
        }
        
        /// <summary>
        /// Tests various intersection cases between Line2d and LineSegment2d instances
        /// </summary>
        [Test]
        public void TestLineSegmentIntersectionMethod()
        {
            // simple colinear full coverage case
            var p0 = Vector2d.Zero;
            var p1 = Vector2d.Up;

            var line = new Line2d(p0, p1-p0);

            p0 = Vector2d.Zero;
            p1 = Vector2d.Up;

            var segment = new LineSegment2d(p0, p1);

            var intersection = line.Intersection(segment);

            Assert.IsTrue(intersection);
            Assert.AreEqual(LineIntersection.Intersection.Colinear, intersection.Type);
            TestUtilities.AssertThatVector2dsAreEqual(intersection.A, Vector2d.Zero);
            TestUtilities.AssertThatVector2dsAreEqual(intersection.B, Vector2d.Up);

            // simple perpendicular case
            p0 = Vector2d.Up;
            p1 = Vector2d.Down;

            line = new Line2d(p0, p1 - p0);

            p0 = Vector2d.Left;
            p1 = Vector2d.Right;

            segment = new LineSegment2d(p0, p1);

            intersection = line.Intersection(segment);

            Assert.IsTrue(intersection);
            Assert.AreEqual(LineIntersection.Intersection.SinglePoint, intersection.Type);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0), intersection.A);

            // reverse segment case
            segment = new LineSegment2d(p1, p0);

            intersection = line.Intersection(segment);

            Assert.IsTrue(intersection);
            Assert.AreEqual(LineIntersection.Intersection.SinglePoint, intersection.Type);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0), intersection.A);

            // simple 45 degree case
            p0 = Vector2d.Up;
            p1 = Vector2d.Down;

            line = new Line2d(p0, p1 - p0);

            p0 = -Vector2d.One;
            p1 = Vector2d.One;

            segment = new LineSegment2d(p0, p1);

            intersection = line.Intersection(segment);

            Assert.IsTrue(intersection);
            Assert.AreEqual(LineIntersection.Intersection.SinglePoint, intersection.Type);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0), intersection.A);

            // reverse segment case
            segment = new LineSegment2d(p1, p0);

            intersection = line.Intersection(segment);

            Assert.IsTrue(intersection);
            Assert.AreEqual(LineIntersection.Intersection.SinglePoint, intersection.Type);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0), intersection.A);

            // 45 degree line 45 degree segment case
            p0 = new Vector2d(1, -1);
            p1 = new Vector2d(-1, 1);

            line = new Line2d(p0, p1 - p0);

            p0 = -Vector2d.One;
            p1 = Vector2d.One;

            segment = new LineSegment2d(p0, p1);

            intersection = line.Intersection(segment);

            Assert.IsTrue(intersection);
            Assert.AreEqual(LineIntersection.Intersection.SinglePoint, intersection.Type);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0), intersection.A);

            // reverse segment case
            segment = new LineSegment2d(p1, p0);

            intersection = line.Intersection(segment);

            Assert.IsTrue(intersection);
            Assert.AreEqual(LineIntersection.Intersection.SinglePoint, intersection.Type);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0), intersection.A);

            // reverse line case
            segment = new LineSegment2d(p0, p1);

            p0 = new Vector2d(1, -1);
            p1 = new Vector2d(-1, 1);

            line = new Line2d(p0, p0 - p1);

            intersection = line.Intersection(segment);

            Assert.IsTrue(intersection);
            Assert.AreEqual(LineIntersection.Intersection.SinglePoint, intersection.Type);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0), intersection.A);

            // non origin centred case
            p0 = new Vector2d(1, 1);
            p1 = new Vector2d(2, 5);

            line = new Line2d(p0, p1 - p0);

            p0 = new Vector2d(-2, 3);
            p1 = new Vector2d(5, 3);

            segment = new LineSegment2d(p0, p1);

            intersection = line.Intersection(segment);

            Assert.IsTrue(intersection);
            Assert.AreEqual(LineIntersection.Intersection.SinglePoint, intersection.Type);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1.5, 3), intersection.A);

            // reverse segment case
            segment = new LineSegment2d(p1, p0);

            intersection = line.Intersection(segment);

            Assert.IsTrue(intersection);
            Assert.AreEqual(LineIntersection.Intersection.SinglePoint, intersection.Type);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1.5, 3), intersection.A);
        }

        /// <summary>
        /// Tests various intersection cases between Line2d and Line2d instances
        /// </summary>
        [Test]
        public void TestLineIntersectionMethod()
        {
            // simple cross over case
            var p0 = Vector2d.Down;
            var p1 = Vector2d.Up;

            var a = new Line2d(p0, p1 - p0);

            p0 = Vector2d.Left;
            p1 = Vector2d.Right;

            var b = new Line2d(p0, p1 - p0);
            var intersection = a.Intersection(b, out double t, out double k);

            TestUtilities.AssertThatDoublesAreEqual(0.5d, t);
            TestUtilities.AssertThatDoublesAreEqual(0.5d, k);
            Assert.AreEqual(LineIntersection.Intersection.SinglePoint, intersection.Type);

            // simple same (colinear) case
            a = new Line2d(p0, p1 - p0);
            intersection = a.Intersection(b, out t, out k);

            TestUtilities.AssertThatDoublesAreEqual(0d, t);
            TestUtilities.AssertThatDoublesAreEqual(0d, k);
            Assert.AreEqual(LineIntersection.Intersection.Colinear, intersection.Type);

            // simple cross over case at base of a
            p0 = Vector2d.Zero;
            p1 = Vector2d.Up;

            a = new Line2d(p0, p1 - p0);

            p0 = Vector2d.Left;
            p1 = Vector2d.Right;

            b = new Line2d(p0, p1 - p0);

            intersection = a.Intersection(b, out t, out k);

            TestUtilities.AssertThatDoublesAreEqual(0d, t);
            TestUtilities.AssertThatDoublesAreEqual(0.5d, k);
            Assert.AreEqual(LineIntersection.Intersection.SinglePoint, intersection.Type);

            // simple same origin case
            p0 = Vector2d.Zero;
            p1 = Vector2d.Up;

            a = new Line2d(p0, p1 - p0);

            p0 = Vector2d.Zero;
            p1 = Vector2d.Right;

            b = new Line2d(p0, p1 - p0);

            intersection = a.Intersection(b, out t, out k);

            TestUtilities.AssertThatDoublesAreEqual(0d, t);
            TestUtilities.AssertThatDoublesAreEqual(0d, k);
            Assert.AreEqual(LineIntersection.Intersection.SinglePoint, intersection.Type);

            // simple extended case
            p0 = Vector2d.Up;
            p1 = Vector2d.Up * 2d;

            a = new Line2d(p0, p1 - p0);

            p0 = Vector2d.Right;
            p1 = Vector2d.Right * 2d;

            b = new Line2d(p0, p1 - p0);

            intersection = a.Intersection(b, out t, out k);

            TestUtilities.AssertThatDoublesAreEqual(-1d, t);
            TestUtilities.AssertThatDoublesAreEqual(-1d, k);
            Assert.AreEqual(LineIntersection.Intersection.SinglePoint, intersection.Type);

            // simple 45 degree case
            p0 = Vector2d.One;
            p1 = -Vector2d.One;

            a = new Line2d(p0, p1 - p0);

            p0 = new Vector2d(1, -1);
            p1 = new Vector2d(-1, 1);

            b = new Line2d(p0, p1 - p0);

            intersection = a.Intersection(b, out t, out k);

            TestUtilities.AssertThatDoublesAreEqual(0.5d, t);
            TestUtilities.AssertThatDoublesAreEqual(0.5d, k);
            Assert.AreEqual(LineIntersection.Intersection.SinglePoint, intersection.Type);

            // simple 45 degree case, scaled
            p0 = Vector2d.One * 2d;
            p1 = -Vector2d.One;

            a = new Line2d(p0, p1 - p0);

            p0 = new Vector2d(1, -1);
            p1 = new Vector2d(-1, 1);

            b = new Line2d(p0, p1 - p0);

            intersection = a.Intersection(b, out t, out k);

            TestUtilities.AssertThatDoublesAreEqual(2d / 3d, t);
            TestUtilities.AssertThatDoublesAreEqual(0.5d, k);
            Assert.AreEqual(LineIntersection.Intersection.SinglePoint, intersection.Type);

            // simple 45 degree case, scaled
            p0 = new Vector2d(1, -1) * 2d;
            p1 = new Vector2d(-1, 1);

            a = new Line2d(p0, p1 - p0);

            p0 = Vector2d.One;
            p1 = -Vector2d.One;

            b = new Line2d(p0, p1 - p0);

            intersection = a.Intersection(b, out t, out k);

            TestUtilities.AssertThatDoublesAreEqual(2d / 3d, t);
            TestUtilities.AssertThatDoublesAreEqual(0.5d, k);
            Assert.AreEqual(LineIntersection.Intersection.SinglePoint, intersection.Type);

            // parallel case
            p0 = Vector2d.Zero;
            p1 = Vector2d.Up;

            a = new Line2d(p0, p1 - p0);

            p0 = new Vector2d(1d, 0d);
            p1 = new Vector2d(1d, 1d);

            b = new Line2d(p0, p1 - p0);

            intersection = a.Intersection(b, out t, out k);

            Assert.IsNaN(t);
            Assert.IsNaN(k);
            Assert.AreEqual(LineIntersection.Intersection.NoIntersection, intersection.Type);

            // parallel case
            p0 = Vector2d.Zero;
            p1 = Vector2d.Right;

            a = new Line2d(p0, p1 - p0);

            p0 = new Vector2d(0d, 1d);
            p1 = new Vector2d(1d, 1d);

            b = new Line2d(p0, p1 - p0);

            intersection = a.Intersection(b, out t, out k);

            Assert.IsNaN(t);
            Assert.IsNaN(k);
            Assert.AreEqual(LineIntersection.Intersection.NoIntersection, intersection.Type);
        }
    }
}