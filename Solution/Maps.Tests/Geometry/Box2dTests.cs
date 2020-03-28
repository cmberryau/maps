using System;
using Maps.Geometry;
using NUnit.Framework;

namespace Maps.Tests.Geometry
{
    /// <summary>
    /// A series of tests for the Box2d class
    /// </summary>
    [TestFixture]
    internal sealed class Box2dTests
    {
        /// <summary>
        /// Creates a default unit sized Box2d
        /// </summary>
        /// <param name="translation">Translation to apply</param>
        public static Box2d CreateDefaultBox(Vector2d translation)
        {
            var a = Vector2d.Zero + translation;
            var b = Vector2d.One + translation;
            return new Box2d(a, b);
        }

        /// <summary>
        /// Creates a diamond shaped linestrip
        /// </summary>
        /// <param name="translation">Translation to apply</param>
        public static LineStrip2d CreateDiamondLineStrip(Vector2d translation)
        {
            var points = new[]
            {
                new Vector2d(0.5, 1.1) + translation,
                new Vector2d(1.1, 0.5) + translation,
                new Vector2d(0.5, -0.1) + translation,
                new Vector2d(-0.1, 0.5) + translation,
                new Vector2d(0.5, 1.1) + translation,
            };

            return new LineStrip2d(points);
        }

        /// <summary>
        /// Creates a spiral shaped linestrip
        /// </summary>
        /// <param name="translation">Translation to apply</param>
        /// <param name="res">The number of points to generate</param>
        /// <param name="cycles">The number of full circle movements</param>
        /// <param name="rate">The rate of expansion from the centre</param>
        public static LineStrip2d CreateSpiralLineStrip(Vector2d translation,
            int res, int cycles, double rate)
        {
            var points = new Vector2d[res];

            for (var i = 0; i < res; i++)
            {
                var t = (double)i / res;
                var u = t * rate;

                var x = Math.Cos(t * Math.PI * cycles * 2) * u;
                var y = Math.Sin(t * Math.PI * cycles * 2) * u;

                points[i] = new Vector2d(x, y);
            }

            return new LineStrip2d(points);
        }

        [Test]
        public void TestConstructor()
        {
            // simple case
            var a = Vector2d.Zero;
            var b = Vector2d.One;
            var box = new Box2d(a, b);

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, box.A);
            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, box.B);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5d, 0.5d), box.Centre);

            var vertices = new [] {box[0], box[1], box[2], box[3]};
            Assert.IsTrue(Vector2d.Clockwise(vertices));

            // reverse simple
            box = new Box2d(b, a);

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.One, box.A);
            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, box.B);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5d, 0.5d), box.Centre);

            vertices = new[] { box[0], box[1], box[2], box[3] };
            Assert.IsTrue(Vector2d.Clockwise(vertices));

            // flipped simple
            a = Vector2d.Zero;
            b = new Vector2d(-1, 1);
            box = new Box2d(a, b);

            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, box.A);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-1, 1), box.B);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-0.5d, 0.5d), box.Centre);

            vertices = new[] { box[0], box[1], box[2], box[3] };
            Assert.IsTrue(Vector2d.Clockwise(vertices));

            // reverse flipped simple
            box = new Box2d(b, a);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-1, 1), box.A);
            TestUtilities.AssertThatVector2dsAreEqual(Vector2d.Zero, box.B);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-0.5d, 0.5d), box.Centre);

            vertices = new[] { box[0], box[1], box[2], box[3] };
            Assert.IsTrue(Vector2d.Clockwise(vertices));
        }

        [Test]
        public void TestLineStripClampSimpleCases()
        {
            // simple side entry case
            var translation = Vector2d.Zero;
            var box = CreateDefaultBox(translation);

            var points = new[]
            {
                new Vector2d(-2, 0.5) + translation,
                new Vector2d(0.5, 0.5) + translation,
            };

            var linestrip = new LineStrip2d(points);
            var clampedLineStrip = box.Clamp(linestrip);

            Assert.IsNotNull(clampedLineStrip);
            Assert.AreEqual(2, clampedLineStrip.Count);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.5) + translation,
                clampedLineStrip[0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5, 0.5) + translation,
                clampedLineStrip[1]);

            // simple side exit case
            points = new[]
            {
                new Vector2d(0.5, 0.5) + translation,
                new Vector2d(2, 0.5) + translation,
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clamp(linestrip);

            Assert.IsNotNull(clampedLineStrip);
            Assert.AreEqual(2, clampedLineStrip.Count);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5, 0.5) + translation,
                clampedLineStrip[0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.5) + translation,
                clampedLineStrip[1]);

            // simple x axis skip case
            points = new[]
            {
                new Vector2d(-2, 0.5) + translation,
                new Vector2d(2, 0.5) + translation,
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clamp(linestrip);

            Assert.IsNotNull(clampedLineStrip);
            Assert.AreEqual(2, clampedLineStrip.Count);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.5) + translation,
                clampedLineStrip[0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.5) + translation,
                clampedLineStrip[1]);

            // simple x axis skip case - reverse
            points = new[]
            {
                new Vector2d(2, 0.5),
                new Vector2d(-2, 0.5),
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clamp(linestrip);

            Assert.IsNotNull(clampedLineStrip);
            Assert.AreEqual(2, clampedLineStrip.Count);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.5) + translation,
                clampedLineStrip[0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.5) + translation,
                clampedLineStrip[1]);

            // simple y axis skip case
            points = new[]
            {
                new Vector2d(0.5, -2),
                new Vector2d(0.5, 2),
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clamp(linestrip);

            Assert.IsNotNull(clampedLineStrip);
            Assert.AreEqual(2, clampedLineStrip.Count);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5, 0) + translation,
                clampedLineStrip[0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5, 1) + translation,
                clampedLineStrip[1]);

            // simple y axis skip case - reverse
            points = new[]
            {
                new Vector2d(0.5, 2),
                new Vector2d(0.5, -2),
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clamp(linestrip);

            Assert.IsNotNull(clampedLineStrip);
            Assert.AreEqual(2, clampedLineStrip.Count);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5, 1) + translation,
                clampedLineStrip[0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5, 0) + translation,
                clampedLineStrip[1]);
        }

        [Test]
        public void TestLineStripClampComplexCases()
        {
            // diamond layered over box case
            var translation = Vector2d.Zero;
            var box = CreateDefaultBox(translation);
            var clampedLineStrip = box.Clamp(CreateDiamondLineStrip(translation));

            Assert.IsNotNull(clampedLineStrip);
            Assert.AreEqual(9, clampedLineStrip.Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.4) + translation, clampedLineStrip[0], Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.6, 0) + translation, clampedLineStrip[1]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.4, 0) + translation, clampedLineStrip[2]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.4) + translation, clampedLineStrip[3]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.6) + translation, clampedLineStrip[4]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.4, 1) + translation, clampedLineStrip[5], Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.6, 1) + translation, clampedLineStrip[6], Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.6) + translation, clampedLineStrip[7], Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.4) + translation, clampedLineStrip[8], Mathd.EpsilonE15);

            // translated to lie over origin
            translation = new Vector2d(-0.5, -0.5);
            box = CreateDefaultBox(translation);
            clampedLineStrip = box.Clamp(CreateDiamondLineStrip(translation));

            Assert.IsNotNull(clampedLineStrip);
            Assert.AreEqual(9, clampedLineStrip.Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.4) + translation, clampedLineStrip[0], Mathd.EpsilonE16);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.6, 0) + translation, clampedLineStrip[1]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.4, 0) + translation, clampedLineStrip[2]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.4) + translation, clampedLineStrip[3]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.6) + translation, clampedLineStrip[4], Mathd.EpsilonE16);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.4, 1) + translation, clampedLineStrip[5], Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.6, 1) + translation, clampedLineStrip[6], Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.6) + translation, clampedLineStrip[7], Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.4) + translation, clampedLineStrip[8], Mathd.EpsilonE16);

            // spiral case, 256 iterations
            translation = Vector2d.Zero;
            box = CreateDefaultBox(translation);
            clampedLineStrip = box.Clamp(CreateSpiralLineStrip(Vector2d.Zero, 256, 10, 1));
            Assert.IsNotNull(clampedLineStrip);
        }

        [Test]
        public void TestLineStripClampComplexPerformanceCase()
        {
            // spiral case, 2048 resolution, 8192 repetitions
            var translation = Vector2d.Zero;
            var box = CreateDefaultBox(translation);

            var linestrip = CreateSpiralLineStrip(translation, 2048, 10, 1);

            for (var i = 0; i < 8192; i++)
            {
                var clampedLineStrip = box.Clamp(linestrip);
                Assert.IsNotNull(clampedLineStrip);
            }
        }

        [Test]
        public void TestLineStripClampNullCase()
        {
            // null linestrip case
            var translation = Vector2d.Zero;
            var box = CreateDefaultBox(translation);

            LineStrip2d linestrip = null;

            Assert.Throws<ArgumentNullException>(() => box.Clamp(linestrip));
        }

        [Test]
        public void TestLineStripClampContainedCase()
        {
            // linestrip contained case
            var translation = Vector2d.Zero;
            var box = CreateDefaultBox(translation);

            var points = new[]
            {
                new Vector2d(0.25, 0.25) + translation,
                new Vector2d(0.75, 0.75) + translation,
            };

            var linestrip = new LineStrip2d(points);
            var clampedLineStrip = box.Clamp(linestrip);

            Assert.AreSame(linestrip, clampedLineStrip);
        }

        [Test]
        public void TestLineStripClampNoIntersectionCase()
        {
            // no intersection case
            var translation = Vector2d.Zero;
            var box = CreateDefaultBox(translation);

            var points = new[]
            {
                new Vector2d(2, 2) + translation,
                new Vector2d(3, 3) + translation,
            };

            var linestrip = new LineStrip2d(points);
            var clampedLineStrip = box.Clamp(linestrip);

            Assert.IsNull(clampedLineStrip);
        }

        [Test]
        public void TestLineStripClampVertexInCornerCases()
        {
            // single vertex in the top right corner case
            var translation = Vector2d.Zero;
            var box = CreateDefaultBox(translation);
            var points = new[]
            {
                new Vector2d(1, 1) + translation,
                new Vector2d(2, 2) + translation,
            };

            var linestrip = new LineStrip2d(points);
            var clampedLineStrip = box.Clamp(linestrip);

            Assert.IsNull(clampedLineStrip);

            // single vertex on the +x edge halfway on y
            points = new[]
            {
                new Vector2d(1, 0.5) + translation,
                new Vector2d(2, 2) + translation,
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clamp(linestrip);

            Assert.IsNull(clampedLineStrip);

            // single vertex in the bottom right corner case
            points = new[]
            {
                new Vector2d(1, 0) + translation,
                new Vector2d(2, 2) + translation,
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clamp(linestrip);

            Assert.IsNull(clampedLineStrip);

            // single vertex on the -y edge halfway on x
            points = new[]
            {
                new Vector2d(0.5, 0) + translation,
                new Vector2d(2, -2) + translation,
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clamp(linestrip);

            Assert.IsNull(clampedLineStrip);

            // single vertex in the bottom left corner case
            points = new[]
            {
                new Vector2d(0, 0) + translation,
                new Vector2d(-2, -2) + translation,
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clamp(linestrip);

            Assert.IsNull(clampedLineStrip);

            // single vertex on the -x edge halfway on y
            points = new[]
            {
                new Vector2d(0, 0.5) + translation,
                new Vector2d(-2, -2) + translation,
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clamp(linestrip);

            Assert.IsNull(clampedLineStrip);

            // single vertex in top left corner case
            points = new[]
            {
                new Vector2d(0, 1) + translation,
                new Vector2d(2, 2) + translation,
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clamp(linestrip);

            Assert.IsNull(clampedLineStrip);

            // single vertex on +y edge halfway across
            points = new[]
            {
                new Vector2d(0.5, 1) + translation,
                new Vector2d(2, 2) + translation,
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clamp(linestrip);

            Assert.IsNull(clampedLineStrip);
        }

        [Test]
        public void TestLineStripClampVertexOnEdgeCases()
        {
            // strip starting on -x edge and finishing on +x edge, mid y axis
            var translation = Vector2d.Zero;
            var box = CreateDefaultBox(translation);

            var points = new[]
            {
                new Vector2d(0, 0.5),
                new Vector2d(1, 0.5),
            };

            var linestrip = new LineStrip2d(points);
            var clippedLineStrips = box.Clamp(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(2, clippedLineStrips.Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.5) + translation,
                clippedLineStrips[0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.5) + translation,
                clippedLineStrips[1]);

            // strip starting on -x edge and finishing on +x edge, +y
            points = new[]
            {
                new Vector2d(0, 1),
                new Vector2d(1, 1),
            };

            linestrip = new LineStrip2d(points);
            clippedLineStrips = box.Clamp(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(2, clippedLineStrips.Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 1) + translation,
                clippedLineStrips[0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 1) + translation,
                clippedLineStrips[1]);

            // strip starting on -x edge and finishing on +x edge, -y
            points = new[]
            {
                new Vector2d(0, 0),
                new Vector2d(1, 0),
            };

            linestrip = new LineStrip2d(points);
            clippedLineStrips = box.Clamp(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(2, clippedLineStrips.Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0) + translation,
                clippedLineStrips[0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0) + translation,
                clippedLineStrips[1]);

            // strip starting on -y edge and finishing on +y edge, mid x axis
            points = new[]
            {
                new Vector2d(0.5, 0),
                new Vector2d(0.5, 1),
            };

            linestrip = new LineStrip2d(points);
            clippedLineStrips = box.Clamp(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(2, clippedLineStrips.Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5, 0) + translation,
                clippedLineStrips[0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5, 1) + translation,
                clippedLineStrips[1]);

            // strip starting on -y edge and finishing on +y edge, +x
            points = new[]
            {
                new Vector2d(1, 0),
                new Vector2d(1, 1),
            };

            linestrip = new LineStrip2d(points);
            clippedLineStrips = box.Clamp(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(2, clippedLineStrips.Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0) + translation,
                clippedLineStrips[0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 1) + translation,
                clippedLineStrips[1]);

            // strip starting on -y edge and finishing on +y edge, -x
            points = new[]
            {
                new Vector2d(0, 0),
                new Vector2d(0, 1),
            };

            linestrip = new LineStrip2d(points);
            clippedLineStrips = box.Clamp(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(2, clippedLineStrips.Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0) + translation,
                clippedLineStrips[0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 1) + translation,
                clippedLineStrips[1]);
        }

        [Test]
        public void TestLineStripClampColinearEdgeCases()
        {
            // colinear at +y
            var translation = Vector2d.Zero;
            var box = CreateDefaultBox(translation);

            var points = new[]
            {
                new Vector2d(-1, 1),
                new Vector2d(2, 1),
            };

            var linestrip = new LineStrip2d(points);
            var clippedLineStrips = box.Clamp(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(2, clippedLineStrips.Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 1) + translation,
                clippedLineStrips[0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 1) + translation,
                clippedLineStrips[1]);

            // colinear at -y
            points = new[]
            {
                new Vector2d(-1, 0),
                new Vector2d(2, 0),
            };

            linestrip = new LineStrip2d(points);
            clippedLineStrips = box.Clamp(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(2, clippedLineStrips.Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0) + translation,
                clippedLineStrips[0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0) + translation,
                clippedLineStrips[1]);

            // colinear at +x
            points = new[]
            {
                new Vector2d(1, -1),
                new Vector2d(1, 2),
            };

            linestrip = new LineStrip2d(points);
            clippedLineStrips = box.Clamp(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(2, clippedLineStrips.Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0) + translation,
                clippedLineStrips[0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 1) + translation,
                clippedLineStrips[1]);

            // colinear at -x
            points = new[]
{
                new Vector2d(0, -1),
                new Vector2d(0, 2),
            };

            linestrip = new LineStrip2d(points);
            clippedLineStrips = box.Clamp(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(2, clippedLineStrips.Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0) + translation,
                clippedLineStrips[0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 1) + translation,
                clippedLineStrips[1]);
        }

        [Test]
        public void TestLineStripClampObscuredCases()
        {
            // 1 point completely obscured case
            var translation = Vector2d.Zero;
            var box = CreateDefaultBox(translation);

            var points = new[]
            {
                new Vector2d(-3, 0.5) + translation,
                new Vector2d(-2, 0.5) + translation,
                new Vector2d(0.5, 0.5) + translation,
            };

            var linestrip = new LineStrip2d(points);
            var clampedLineStrip = box.Clamp(linestrip);

            Assert.IsNotNull(clampedLineStrip);
            Assert.AreEqual(2, clampedLineStrip.Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.5) + translation, 
                clampedLineStrip[0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5, 0.5) + translation, 
                clampedLineStrip[1]);

            // 1 point completely obscured case with outlier
            points = new[]
            {
                new Vector2d(-3, 0.5) + translation,
                new Vector2d(-2, 0.5) + translation,
                new Vector2d(2, 0.5) + translation,
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clamp(linestrip);

            Assert.IsNotNull(clampedLineStrip);
            Assert.AreEqual(2, clampedLineStrip.Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.5) + translation, 
                clampedLineStrip[0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.5) + translation, 
                clampedLineStrip[1]);

            // 2 points completely obscured
            points = new[]
            {
                new Vector2d(-3, 0.5) + translation,
                new Vector2d(-2, 0.5) + translation,
                new Vector2d(2, 0.5) + translation,
                new Vector2d(3, 0.5) + translation,
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clamp(linestrip);

            Assert.IsNotNull(clampedLineStrip);
            Assert.AreEqual(2, clampedLineStrip.Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.5) + translation, 
                clampedLineStrip[0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.5) + translation, 
                clampedLineStrip[1]);
        }

        [Test]
        public void TestLineStripClampBoxCase()
        {
            var translation = Vector2d.Zero;
            var box = CreateDefaultBox(translation);

            var points = new[]
            {
                new Vector2d(0.25, 0.25),
                new Vector2d(0.25, 0.75),
                new Vector2d(0.75, 0.75),
                new Vector2d(0.75, 0.25),
                new Vector2d(0.25, 0.25),
            };

            var linestrip = new LineStrip2d(points);
            var clampedLineStrip = box.Clamp(linestrip);

            Assert.IsNotNull(clampedLineStrip);
            Assert.AreEqual(5, clampedLineStrip.Count);

            // -x side
            points = new[]
            {
                new Vector2d(-0.25, 0.25),
                new Vector2d(-0.25, 0.75),
                new Vector2d(0.25, 0.75),
                new Vector2d(0.25, 0.25),
                new Vector2d(-0.25, 0.25),
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clamp(linestrip);

            Assert.IsNotNull(clampedLineStrip);
            Assert.AreEqual(5, clampedLineStrip.Count);

            // +x side
            points = new[]
            {
                new Vector2d(0.75, 0.25),
                new Vector2d(0.75, 0.75),
                new Vector2d(1.25, 0.75),
                new Vector2d(1.25, 0.25),
                new Vector2d(0.75, 0.25),
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clamp(linestrip);

            Assert.IsNotNull(clampedLineStrip);
            Assert.AreEqual(5, clampedLineStrip.Count);
        }

        [Test]
        public void TestLineStripClipSimpleCases()
        {
            // simple side entry case
            var translation = Vector2d.Zero;
            var box = CreateDefaultBox(translation);

            var points = new[]
            {
                new Vector2d(-2, 0.5) + translation,
                new Vector2d(0.5, 0.5) + translation,
            };

            var linestrip = new LineStrip2d(points);
            var clippedLineStrips = box.Clip(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(1, clippedLineStrips.Count);

            foreach (var strip in clippedLineStrips)
            {
                Assert.IsNotNull(strip);
            }

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.5) + translation,
                clippedLineStrips[0][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5, 0.5) + translation,
                clippedLineStrips[0][1]);

            // simple side exit case
            points = new[]
            {
                new Vector2d(0.5, 0.5) + translation,
                new Vector2d(2, 0.5) + translation,
            };

            linestrip = new LineStrip2d(points);
            clippedLineStrips = box.Clip(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(1, clippedLineStrips.Count);

            foreach (var strip in clippedLineStrips)
            {
                Assert.IsNotNull(strip);
            }

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5, 0.5) + translation,
                clippedLineStrips[0][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.5) + translation,
                clippedLineStrips[0][1]);

            // simple x axis skip case
            points = new[]
            {
                new Vector2d(-2, 0.5) + translation,
                new Vector2d(2, 0.5) + translation,
            };

            linestrip = new LineStrip2d(points);
            clippedLineStrips = box.Clip(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(1, clippedLineStrips.Count);

            foreach (var strip in clippedLineStrips)
            {
                Assert.IsNotNull(strip);
            }

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.5) + translation, clippedLineStrips[0][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.5), clippedLineStrips[0][1]);

            // simple x axis skip case - reverse
            points = new[]
            {
                new Vector2d(2, 0.5),
                new Vector2d(-2, 0.5),
            };

            linestrip = new LineStrip2d(points);
            clippedLineStrips = box.Clip(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(1, clippedLineStrips.Count);

            foreach (var strip in clippedLineStrips)
            {
                Assert.IsNotNull(strip);
            }

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.5), clippedLineStrips[0][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.5), clippedLineStrips[0][1]);

            // simple y axis skip case
            points = new[]
            {
                new Vector2d(0.5, -2),
                new Vector2d(0.5, 2),
            };

            linestrip = new LineStrip2d(points);
            clippedLineStrips = box.Clip(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(1, clippedLineStrips.Count);

            foreach (var strip in clippedLineStrips)
            {
                Assert.IsNotNull(strip);
            }

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5, 0), clippedLineStrips[0][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5, 1), clippedLineStrips[0][1]);

            // simple y axis skip case - reverse
            points = new[]
            {
                new Vector2d(0.5, 2),
                new Vector2d(0.5, -2),
            };

            linestrip = new LineStrip2d(points);
            clippedLineStrips = box.Clip(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(1, clippedLineStrips.Count);

            foreach (var strip in clippedLineStrips)
            {
                Assert.IsNotNull(strip);
            }

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5, 1), clippedLineStrips[0][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5, 0), clippedLineStrips[0][1]);
        }

        [Test]
        public void TestLineStripClipComplexCases()
        {
            // diamond layered over box case
            var translation = Vector2d.Zero;
            var box = CreateDefaultBox(translation);
            var linestrip = CreateDiamondLineStrip(translation);
            var clippedLineStrips = box.Clip(linestrip);

            Assert.AreEqual(4, clippedLineStrips.Count);

            foreach (var strip in clippedLineStrips)
            {
                Assert.IsNotNull(strip);
            }

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.6, 1) + translation, clippedLineStrips[0][0], Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.6) + translation, clippedLineStrips[0][1], Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.4) + translation, clippedLineStrips[1][0], Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.6, 0) + translation, clippedLineStrips[1][1]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.4, 0) + translation, clippedLineStrips[2][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.4) + translation, clippedLineStrips[2][1]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.6) + translation, clippedLineStrips[3][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.4, 1) + translation, clippedLineStrips[3][1], Mathd.EpsilonE15);

            // translated to lie over origin
            translation = new Vector2d(-0.5, -0.5);
            box = CreateDefaultBox(translation);
            linestrip = CreateDiamondLineStrip(translation);
            clippedLineStrips = box.Clip(linestrip);

            Assert.AreEqual(4, clippedLineStrips.Count);

            foreach (var strip in clippedLineStrips)
            {
                Assert.IsNotNull(strip);
            }

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.6, 1) + translation, clippedLineStrips[0][0], Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.6) + translation, clippedLineStrips[0][1], Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.4) + translation, clippedLineStrips[1][0], Mathd.EpsilonE16);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.6, 0) + translation, clippedLineStrips[1][1]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.4, 0) + translation, clippedLineStrips[2][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.4) + translation, clippedLineStrips[2][1]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.6) + translation, clippedLineStrips[3][0], Mathd.EpsilonE16);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.4, 1) + translation, clippedLineStrips[3][1], Mathd.EpsilonE16);

            // spiral case, 256 iterations
            translation = Vector2d.Zero;
            box = CreateDefaultBox(translation);
            linestrip = CreateSpiralLineStrip(translation, 256, 10, 1);
            clippedLineStrips = box.Clip(linestrip);

            Assert.AreEqual(10, clippedLineStrips.Count);

            foreach (var strip in clippedLineStrips)
            {
                Assert.IsNotNull(strip);
            }
        }

        [Test]
        public void TestLineStripClipComplexPerformanceCase()
        {
            // spiral case, 2048 resolution, 8192 repetitions
            var translation = Vector2d.Zero;
            var box = CreateDefaultBox(translation);

            var linestrip = CreateSpiralLineStrip(translation, 2048, 10, 1);

            for (var i = 0; i < 8192; i++)
            {
                var clippedLineStrips = box.Clip(linestrip);
                Assert.AreEqual(10, clippedLineStrips.Count);

                foreach (var strip in clippedLineStrips)
                {
                    Assert.IsNotNull(strip);
                }
            }
        }

        [Test]
        public void TestLineStripClipNullCase()
        {
            // null linestrip case
            var translation = Vector2d.Zero;
            var box = CreateDefaultBox(translation);

            LineStrip2d linestrip = null;

            Assert.Throws<ArgumentNullException>(() => box.Clip(linestrip));
        }

        [Test]
        public void TestLineStripClipObscuredCases()
        {
            // 1 point completely obscured case
            var translation = Vector2d.Zero;
            var box = CreateDefaultBox(translation);

            var points = new[]
            {
                new Vector2d(-3, 0.5) + translation,
                new Vector2d(-2, 0.5) + translation,
                new Vector2d(0.5, 0.5) + translation,
            };

            var linestrip = new LineStrip2d(points);
            var clampedLineStrip = box.Clip(linestrip);

            Assert.IsNotNull(clampedLineStrip);
            Assert.AreEqual(1, clampedLineStrip.Count);
            Assert.AreEqual(2, clampedLineStrip[0].Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.5) + translation,
                clampedLineStrip[0][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5, 0.5) + translation,
                clampedLineStrip[0][1]);

            // 1 point completely obscured case with outlier
            points = new[]
            {
                new Vector2d(-3, 0.5) + translation,
                new Vector2d(-2, 0.5) + translation,
                new Vector2d(2, 0.5) + translation,
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clip(linestrip);

            Assert.IsNotNull(clampedLineStrip);
            Assert.AreEqual(1, clampedLineStrip.Count);
            Assert.AreEqual(2, clampedLineStrip[0].Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.5) + translation,
                clampedLineStrip[0][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.5) + translation,
                clampedLineStrip[0][1]);

            // 2 points completely obscured
            points = new[]
            {
                new Vector2d(-3, 0.5) + translation,
                new Vector2d(-2, 0.5) + translation,
                new Vector2d(2, 0.5) + translation,
                new Vector2d(3, 0.5) + translation,
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clip(linestrip);

            Assert.IsNotNull(clampedLineStrip);
            Assert.AreEqual(1, clampedLineStrip.Count);
            Assert.AreEqual(2, clampedLineStrip[0].Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.5) + translation,
                clampedLineStrip[0][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.5) + translation,
                clampedLineStrip[0][1]);
        }

        [Test]
        public void TestLineStripClipContainedCase()
        {
            // linestrip contained case
            var translation = Vector2d.Zero;
            var box = CreateDefaultBox(translation);

            var points = new[]
            {
                new Vector2d(0.25, 0.25) + translation,
                new Vector2d(0.75, 0.75) + translation,
            };

            var linestrip = new LineStrip2d(points);
            var clippedLineStrips = box.Clip(linestrip);

            Assert.AreSame(linestrip, clippedLineStrips[0]);
        }

        [Test]
        public void TestLineStripClipNoIntersectionCase()
        {
            // no intersection case
            var translation = Vector2d.Zero;
            var box = CreateDefaultBox(translation);

            var points = new[]
            {
                new Vector2d(2, 2) + translation,
                new Vector2d(3, 3) + translation,
            };

            var linestrip = new LineStrip2d(points);
            var clippedLineStrips = box.Clip(linestrip);

            Assert.IsEmpty(clippedLineStrips);
        }

        [Test]
        public void TestLineStripClipVertexOnEdgeCases()
        {
            // strip starting on -x edge and finishing on +x edge, mid y axis
            var translation = Vector2d.Zero;
            var box = CreateDefaultBox(translation);

            var points = new[]
            {
                new Vector2d(0, 0.5),
                new Vector2d(1, 0.5),
            };

            var linestrip = new LineStrip2d(points);
            var clippedLineStrips = box.Clip(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(1, clippedLineStrips.Count);

            foreach (var strip in clippedLineStrips)
            {
                Assert.IsNotNull(strip);
            }

            Assert.AreEqual(2, clippedLineStrips[0].Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0.5) + translation,
                clippedLineStrips[0][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0.5) + translation,
                clippedLineStrips[0][1]);

            // strip starting on -x edge and finishing on +x edge, +y
            points = new[]
            {
                new Vector2d(0, 1),
                new Vector2d(1, 1),
            };

            linestrip = new LineStrip2d(points);
            clippedLineStrips = box.Clip(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(1, clippedLineStrips.Count);

            foreach (var strip in clippedLineStrips)
            {
                Assert.IsNotNull(strip);
            }

            Assert.AreEqual(2, clippedLineStrips[0].Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 1) + translation,
                clippedLineStrips[0][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 1) + translation,
                clippedLineStrips[0][1]);

            // strip starting on -x edge and finishing on +x edge, -y
            points = new[]
            {
                new Vector2d(0, 0),
                new Vector2d(1, 0),
            };

            linestrip = new LineStrip2d(points);
            clippedLineStrips = box.Clip(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(1, clippedLineStrips.Count);

            foreach (var strip in clippedLineStrips)
            {
                Assert.IsNotNull(strip);
            }

            Assert.AreEqual(2, clippedLineStrips[0].Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0) + translation,
                clippedLineStrips[0][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0) + translation,
                clippedLineStrips[0][1]);

            // strip starting on -y edge and finishing on +y edge, mid x axis
            points = new[]
            {
                new Vector2d(0.5, 0),
                new Vector2d(0.5, 1),
            };

            linestrip = new LineStrip2d(points);
            clippedLineStrips = box.Clip(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(1, clippedLineStrips.Count);

            foreach (var strip in clippedLineStrips)
            {
                Assert.IsNotNull(strip);
            }

            Assert.AreEqual(2, clippedLineStrips[0].Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5, 0) + translation,
                clippedLineStrips[0][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5, 1) + translation,
                clippedLineStrips[0][1]);

            // strip starting on -y edge and finishing on +y edge, +x
            points = new[]
            {
                new Vector2d(1, 0),
                new Vector2d(1, 1),
            };

            linestrip = new LineStrip2d(points);
            clippedLineStrips = box.Clip(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(1, clippedLineStrips.Count);

            foreach (var strip in clippedLineStrips)
            {
                Assert.IsNotNull(strip);
            }

            Assert.AreEqual(2, clippedLineStrips[0].Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0) + translation,
                clippedLineStrips[0][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 1) + translation,
                clippedLineStrips[0][1]);

            // strip starting on -y edge and finishing on +y edge, -x
            points = new[]
            {
                new Vector2d(0, 0),
                new Vector2d(0, 1),
            };

            linestrip = new LineStrip2d(points);
            clippedLineStrips = box.Clip(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(1, clippedLineStrips.Count);

            foreach (var strip in clippedLineStrips)
            {
                Assert.IsNotNull(strip);
            }

            Assert.AreEqual(2, clippedLineStrips[0].Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0) + translation,
                clippedLineStrips[0][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 1) + translation,
                clippedLineStrips[0][1]);
        }

        [Test]
        public void TestLineStripClipColinearEdgeCases()
        {
            // colinear at +y
            var translation = Vector2d.Zero;
            var box = CreateDefaultBox(translation);

            var points = new[]
            {
                new Vector2d(-1, 1),
                new Vector2d(2, 1),
            };

            var linestrip = new LineStrip2d(points);
            var clippedLineStrips = box.Clip(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(1, clippedLineStrips.Count);

            foreach (var strip in clippedLineStrips)
            {
                Assert.IsNotNull(strip);
            }

            Assert.AreEqual(2, clippedLineStrips[0].Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 1) + translation,
                clippedLineStrips[0][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 1) + translation,
                clippedLineStrips[0][1]);

            // colinear at -y
            points = new[]
            {
                new Vector2d(-1, 0),
                new Vector2d(2, 0),
            };

            linestrip = new LineStrip2d(points);
            clippedLineStrips = box.Clip(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(1, clippedLineStrips.Count);

            foreach (var strip in clippedLineStrips)
            {
                Assert.IsNotNull(strip);
            }

            Assert.AreEqual(2, clippedLineStrips[0].Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0) + translation,
                clippedLineStrips[0][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0) + translation,
                clippedLineStrips[0][1]);

            // colinear at +x
            points = new[]
            {
                new Vector2d(1, -1),
                new Vector2d(1, 2),
            };

            linestrip = new LineStrip2d(points);
            clippedLineStrips = box.Clip(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(1, clippedLineStrips.Count);

            foreach (var strip in clippedLineStrips)
            {
                Assert.IsNotNull(strip);
            }

            Assert.AreEqual(2, clippedLineStrips[0].Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 0) + translation,
                clippedLineStrips[0][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1, 1) + translation,
                clippedLineStrips[0][1]);

            // colinear at -x
            points = new[]
{
                new Vector2d(0, -1),
                new Vector2d(0, 2),
            };

            linestrip = new LineStrip2d(points);
            clippedLineStrips = box.Clip(linestrip);

            Assert.IsNotNull(clippedLineStrips);
            Assert.AreEqual(1, clippedLineStrips.Count);

            foreach (var strip in clippedLineStrips)
            {
                Assert.IsNotNull(strip);
            }

            Assert.AreEqual(2, clippedLineStrips[0].Count);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 0) + translation,
                clippedLineStrips[0][0]);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0, 1) + translation,
                clippedLineStrips[0][1]);
        }

        [Test]
        public void TestLineStripClipVertexInCornerCases()
        {
            // single vertex in the top right corner case
            var translation = Vector2d.Zero;
            var box = CreateDefaultBox(translation);
            var points = new[]
            {
                new Vector2d(1, 1) + translation,
                new Vector2d(2, 2) + translation,
            };

            var linestrip = new LineStrip2d(points);
            var clampedLineStrip = box.Clip(linestrip);

            Assert.IsEmpty(clampedLineStrip);

            // single vertex on the +x edge halfway on y
            points = new[]
            {
                new Vector2d(1, 0.5) + translation,
                new Vector2d(2, 2) + translation,
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clip(linestrip);

            Assert.IsEmpty(clampedLineStrip);

            // single vertex in the bottom right corner case
            points = new[]
            {
                new Vector2d(1, 0) + translation,
                new Vector2d(2, 2) + translation,
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clip(linestrip);

            Assert.IsEmpty(clampedLineStrip);

            // single vertex on the -y edge halfway on x
            points = new[]
            {
                new Vector2d(0.5, 0) + translation,
                new Vector2d(2, -2) + translation,
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clip(linestrip);

            Assert.IsEmpty(clampedLineStrip);

            // single vertex in the bottom left corner case
            points = new[]
            {
                new Vector2d(0, 0) + translation,
                new Vector2d(-2, -2) + translation,
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clip(linestrip);

            Assert.IsEmpty(clampedLineStrip);

            // single vertex on the -x edge halfway on y
            points = new[]
            {
                new Vector2d(0, 0.5) + translation,
                new Vector2d(-2, -2) + translation,
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clip(linestrip);

            Assert.IsEmpty(clampedLineStrip);

            // single vertex in top left corner case
            points = new[]
            {
                new Vector2d(0, 1) + translation,
                new Vector2d(2, 2) + translation,
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clip(linestrip);

            Assert.IsEmpty(clampedLineStrip);

            // single vertex on +y edge halfway across
            points = new[]
            {
                new Vector2d(0.5, 1) + translation,
                new Vector2d(2, 2) + translation,
            };

            linestrip = new LineStrip2d(points);
            clampedLineStrip = box.Clip(linestrip);

            Assert.IsEmpty(clampedLineStrip);
        }
    }
}