using System;
using Maps.Geometry;
using NUnit.Framework;

namespace Maps.Tests.Geometry
{
    /// <summary>
    /// Series of tests for the LineSegment2d class
    /// </summary>
    [TestFixture]
    internal sealed class LineSegment2dTests
    {
        /// <summary>
        /// Tests the LineSegment2d constructor
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var p0 = Vector2d.Zero;
            var p1 = Vector2d.One;
            var segment = new LineSegment2d(p0, p1);

            TestUtilities.AssertThatVector2dsAreEqual(p0, segment.P0);
            TestUtilities.AssertThatVector2dsAreEqual(p1, segment.P1);
            TestUtilities.AssertThatVector2dsAreEqual(p1 - p0, segment.Direction);
            TestUtilities.AssertThatDoublesAreEqual(Math.Sqrt(2d), segment.Length);

            p0 = -Vector2d.One;
            p1 = Vector2d.One;
            segment = new LineSegment2d(p0, p1);

            TestUtilities.AssertThatVector2dsAreEqual(p0, segment.P0);
            TestUtilities.AssertThatVector2dsAreEqual(p1, segment.P1);
            TestUtilities.AssertThatVector2dsAreEqual(p1 - p0, segment.Direction);
            TestUtilities.AssertThatDoublesAreEqual(Math.Sqrt(2d) * 2d, segment.Length);
        }

        /// <summary>
        /// Tests various intersection cases between LineSegment2d instances
        /// </summary>
        [Test]
        public void TestIntersection()
        {
            // duplicate line segment, colinear and overlapping
            var p0 = Vector2d.Zero;
            var p1 = Vector2d.Up;
            var a = new LineSegment2d(p0, p1);

            p0 = Vector2d.Zero;
            p1 = Vector2d.Up;
            var b = new LineSegment2d(p0, p1);

            var intersection = a.Intersection(b);
            Assert.True(intersection);
            Assert.AreEqual(LineIntersection.Intersection.Colinear, intersection.Type);
            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, intersection.A);
            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Up, intersection.B);

            intersection = a.Intersection(b);
            Assert.True(intersection);
            Assert.AreEqual(LineIntersection.Intersection.Colinear, intersection.Type);
            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Up, intersection.B);
            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, intersection.A);

            // colinear and disjoint, simple x case
            p0 = new Vector2d(0, 0);
            p1 = new Vector2d(1, 0);
            a = new LineSegment2d(p0, p1);

            p0 = new Vector2d(2, 0);
            p1 = new Vector2d(3, 0);
            b = new LineSegment2d(p0, p1);

            intersection = a.Intersection(b);
            Assert.False(intersection);
            Assert.AreEqual(LineIntersection.Intersection.NoIntersection, intersection.Type);

            intersection = a.Intersection(b);
            Assert.False(intersection);
            Assert.AreEqual(LineIntersection.Intersection.NoIntersection, intersection.Type);

            // colinear and disjoint, simple -x case
            p0 = new Vector2d(0, 0);
            p1 = new Vector2d(-1, 0);
            a = new LineSegment2d(p0, p1);

            p0 = new Vector2d(-2, 0);
            p1 = new Vector2d(-3, 0);
            b = new LineSegment2d(p0, p1);

            intersection = a.Intersection(b);
            Assert.False(intersection);
            Assert.AreEqual(LineIntersection.Intersection.NoIntersection, intersection.Type);

            intersection = a.Intersection(b);
            Assert.False(intersection);
            Assert.AreEqual(LineIntersection.Intersection.NoIntersection, intersection.Type);

            // colinear and disjoint, simple y case
            p0 = Vector2d.Zero;
            p1 = Vector2d.Up;
            a = new LineSegment2d(p0, p1);

            p0 = new Vector2d(0, 2);
            p1 = new Vector2d(0, 3);
            b = new LineSegment2d(p0, p1);

            intersection = a.Intersection(b);
            Assert.False(intersection);
            Assert.AreEqual(LineIntersection.Intersection.NoIntersection, intersection.Type);

            intersection = a.Intersection(b);
            Assert.False(intersection);
            Assert.AreEqual(LineIntersection.Intersection.NoIntersection, intersection.Type);

            // colinear and disjoint, simple -y case
            p0 = Vector2d.Zero;
            p1 = Vector2d.Up;
            a = new LineSegment2d(p0, p1);

            p0 = new Vector2d(0, -2);
            p1 = new Vector2d(0, -3);
            b = new LineSegment2d(p0, p1);

            intersection = a.Intersection(b);
            Assert.False(intersection);
            Assert.AreEqual(LineIntersection.Intersection.NoIntersection, intersection.Type);

            intersection = a.Intersection(b);
            Assert.False(intersection);
            Assert.AreEqual(LineIntersection.Intersection.NoIntersection, intersection.Type);

            // colinear and disjoint, + 45 degree case
            p0 = new Vector2d(0, 0);
            p1 = new Vector2d(1, 1);
            a = new LineSegment2d(p0, p1);

            p0 = new Vector2d(2, 2);
            p1 = new Vector2d(3, 3);
            b = new LineSegment2d(p0, p1);

            intersection = a.Intersection(b);
            Assert.False(intersection);
            Assert.AreEqual(LineIntersection.Intersection.NoIntersection, intersection.Type);

            intersection = a.Intersection(b);
            Assert.False(intersection);
            Assert.AreEqual(LineIntersection.Intersection.NoIntersection, intersection.Type);

            // colinear and disjoint, - 45 degree case
            p0 = new Vector2d(0, 0);
            p1 = new Vector2d(-1, -1);
            a = new LineSegment2d(p0, p1);

            p0 = new Vector2d(-2, -2);
            p1 = new Vector2d(-3, -3);
            b = new LineSegment2d(p0, p1);

            intersection = a.Intersection(b);
            Assert.False(intersection);
            Assert.AreEqual(LineIntersection.Intersection.NoIntersection, intersection.Type);

            intersection = a.Intersection(b);
            Assert.False(intersection);
            Assert.AreEqual(LineIntersection.Intersection.NoIntersection, intersection.Type);

            // colinear and overlapping
            p0 = new Vector2d(0, 0);
            p1 = new Vector2d(1, 0);
            a = new LineSegment2d(p0, p1);

            p0 = new Vector2d(0.5, 0);
            p1 = new Vector2d(1.5, 0);
            b = new LineSegment2d(p0, p1);

            intersection = a.Intersection(b);
            Assert.True(intersection);
            Assert.AreEqual(LineIntersection.Intersection.Colinear, intersection.Type);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5, 0), intersection.A);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0), intersection.B);

            intersection = a.Intersection(b);
            Assert.True(intersection);
            Assert.AreEqual(LineIntersection.Intersection.Colinear, intersection.Type);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5, 0), intersection.A);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0), intersection.B);

            // parallel, no intersection
            p0 = Vector2d.Zero;
            p1 = Vector2d.Up;
            a = new LineSegment2d(p0, p1);

            p0 = Vector2d.Right;
            p1 = Vector2d.One;
            b = new LineSegment2d(p0, p1);

            intersection = a.Intersection(b);
            Assert.False(intersection);
            Assert.AreEqual(LineIntersection.Intersection.NoIntersection, intersection.Type);

            intersection = a.Intersection(b);
            Assert.False(intersection);
            Assert.AreEqual(LineIntersection.Intersection.NoIntersection, intersection.Type);
        }
    }
}