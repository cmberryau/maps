using System;
using System.Collections;
using System.Collections.Generic;
using Maps.Geometry;
using NUnit.Framework;

namespace Maps.Tests.Geometry
{
    [TestFixture]
    internal sealed class LineStrip2dTests
    {
        [Test]
        public void TestConstructor()
        {
            // simple 1d case
            var vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, 0d)
            };

            var segments = new LineSegment2d[vertices.Count - 1];

            for (var i = 1; i < vertices.Count; i++)
            {
                segments[i - 1] = new LineSegment2d(vertices[i - 1], vertices[i]);
            }

            Assert.DoesNotThrow(() => { var linestrip = new LineStrip2d(vertices); });
            Assert.DoesNotThrow(() => { var linestrip = new LineStrip2d(vertices.ToArray()); });
            Assert.DoesNotThrow(() => { var linestrip = new LineStrip2d(segments); });

            vertices = null;
            Assert.Throws<ArgumentNullException>(() => { var linestrip = new LineStrip2d(vertices); });

            vertices = new List<Vector2d> { new Vector2d(0d, 0d) };

            Assert.Throws<ArgumentException>(() => { var linestrip = new LineStrip2d(vertices); });
            Assert.Throws<ArgumentException>(() => { var linestrip = new LineStrip2d(vertices.ToArray()); });

            // simple 2d case
            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, 1d)
            };

            segments = new LineSegment2d[vertices.Count - 1];

            for (var i = 1; i < vertices.Count; i++)
            {
                segments[i - 1] = new LineSegment2d(vertices[i - 1], vertices[i]);
            }

            Assert.DoesNotThrow(() => { var linestrip = new LineStrip2d(vertices); });
            Assert.DoesNotThrow(() => { var linestrip = new LineStrip2d(vertices.ToArray()); });

            vertices = null;
            Assert.Throws<ArgumentNullException>(() => { var linestrip = new LineStrip2d(vertices); });

            vertices = new List<Vector2d> { new Vector2d(0d, 0d) };

            Assert.Throws<ArgumentException>(() => { var linestrip = new LineStrip2d(vertices); });
            Assert.Throws<ArgumentException>(() => { var linestrip = new LineStrip2d(vertices.ToArray()); });

            // complex 2d case
            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, -1d),
                new Vector2d(2d, -2d),
                new Vector2d(0d, -2d),
                new Vector2d(-1d, -2d),
                new Vector2d(-1d, -1d)
            };

            segments = new LineSegment2d[vertices.Count - 1];

            for (var i = 1; i < vertices.Count; i++)
            {
                segments[i - 1] = new LineSegment2d(vertices[i - 1], vertices[i]);
            }

            Assert.DoesNotThrow(() => { var linestrip = new LineStrip2d(vertices); });
            Assert.DoesNotThrow(() => { var linestrip = new LineStrip2d(vertices.ToArray()); });

            vertices = null;
            Assert.Throws<ArgumentNullException>(() => { var linestrip = new LineStrip2d(vertices); });

            vertices = new List<Vector2d> { new Vector2d(0d, 0d) };

            Assert.Throws<ArgumentException>(() => { var linestrip = new LineStrip2d(vertices); });
            Assert.Throws<ArgumentException>(() => { var linestrip = new LineStrip2d(vertices.ToArray()); });
        }

        [Test]
        public void TestIndexAccess()
        {
            // simple 1d case
            var vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, 0d)
            };

            var linestrip = new LineStrip2d(vertices);

            for (var i = 0; i < vertices.Count; ++i)
            {
                TestUtilities.AssertThatVector2dsAreEqual(vertices[i], linestrip[i]);
            }

            var segments = new LineSegment2d[vertices.Count - 1];

            for (var i = 1; i < vertices.Count; i++)
            {
                segments[i - 1] = new LineSegment2d(vertices[i - 1], vertices[i]);
            }

            linestrip = new LineStrip2d(segments);

            for (var i = 0; i < vertices.Count; ++i)
            {
                TestUtilities.AssertThatVector2dsAreEqual(vertices[i], linestrip[i]);
            }

            // complex 2d case
            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, -1d),
                new Vector2d(2d, -2d),
                new Vector2d(0d, -2d),
                new Vector2d(-1d, -2d),
                new Vector2d(-1d, -1d)
            };

            linestrip = new LineStrip2d(vertices);

            for (var i = 0; i < vertices.Count; ++i)
            {
                TestUtilities.AssertThatVector2dsAreEqual(vertices[i], linestrip[i]);
            }

            segments = new LineSegment2d[vertices.Count - 1];

            for (var i = 1; i < vertices.Count; i++)
            {
                segments[i - 1] = new LineSegment2d(vertices[i - 1], vertices[i]);
            }

            linestrip = new LineStrip2d(segments);

            for (var i = 0; i < vertices.Count; ++i)
            {
                TestUtilities.AssertThatVector2dsAreEqual(vertices[i], linestrip[i]);
            }
        }

        [Test]
        public void TestCountProperty()
        {
            // simple 1d case
            var vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, 0d)
            };

            var linestrip = new LineStrip2d(vertices);

            Assert.AreEqual(4, linestrip.Count);

            // complex 2d case
            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, -1d),
                new Vector2d(2d, -2d),
                new Vector2d(0d, -2d),
                new Vector2d(-1d, -2d),
                new Vector2d(-1d, -1d)
            };

            linestrip = new LineStrip2d(vertices);

            Assert.AreEqual(8, linestrip.Count);
        }

        [Test]
        public void TestDistanceProperty()
        {
            // simple 1d case
            var vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, 0d)
            };

            var linestrip = new LineStrip2d(vertices);

            TestUtilities.AssertThatDoublesAreEqual(3, linestrip.Distance);

            // simple 2d case
            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, 1d)
            };

            linestrip = new LineStrip2d(vertices);

            TestUtilities.AssertThatDoublesAreEqual(3 * Math.Sqrt(2), linestrip.Distance);

            // complex 2d case
            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, -1d),
                new Vector2d(2d, -2d),
                new Vector2d(0d, -2d),
                new Vector2d(-1d, -2d),
                new Vector2d(-1d, -1d)
            };

            linestrip = new LineStrip2d(vertices);

            TestUtilities.AssertThatDoublesAreEqual(4 * Math.Sqrt(2) + 4, linestrip.Distance);
        }

        [Test]
        public void TestBoundsProperty()
        {
            var vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(0d, 1d)
            };

            var linestrip = new LineStrip2d(vertices);

            TestUtilities.AssertThatVector2dsAreEqual(linestrip.Bounds.Centre, 
                new Vector2d(0.5d, 0.5d));

            TestUtilities.AssertThatVector2dsAreEqual(linestrip.Bounds.Extents,
                new Vector2d(0.5d, 0.5d));
        }

        [Test]
        public void TestToClockwiseProperty()
        {
            var vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(0d, 1d)
            };

            var linestrip = new LineStrip2d(vertices);

            Assert.IsFalse(linestrip.Clockwise);

            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 1d),
                new Vector2d(1d, 1d),
                new Vector2d(1d, 0d),
                new Vector2d(0d, 0d),
            };

            linestrip = new LineStrip2d(vertices);

            Assert.IsTrue(linestrip.Clockwise);
        }

        [Test]
        public void TestConvexProperty()
        {
            var vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(0d, 1d)
            };

            var linestrip = new LineStrip2d(vertices);

            Assert.IsTrue(linestrip.Convex);

            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(0.25d, 0.25d),
                new Vector2d(0d, 1d)
            };

            linestrip = new LineStrip2d(vertices);

            Assert.IsFalse(linestrip.Convex);
        }

        [Test]
        public void TestVectorEnumeration()
        {
            // simple 1d case
            var vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, 0d)
            };

            var linestrip = new LineStrip2d(vertices);

            var i = 0;
            foreach (var vertex in linestrip)
            {
                TestUtilities.AssertThatVector2dsAreEqual(vertices[i], vertex);
                ++i;
            }

            var castedLinestrip = linestrip as IEnumerable;
            var enumerator = castedLinestrip.GetEnumerator();
            Assert.IsNotNull(enumerator);

            i = 0;
            foreach (var vertex in linestrip)
            {
                TestUtilities.AssertThatVector2dsAreEqual(vertices[i], vertex);
                ++i;
            }

            // complex 2d case
            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, -1d),
                new Vector2d(2d, -2d),
                new Vector2d(0d, -2d),
                new Vector2d(-1d, -2d),
                new Vector2d(-1d, -1d)
            };

            linestrip = new LineStrip2d(vertices);
            castedLinestrip = linestrip;
            castedLinestrip.GetEnumerator();
            Assert.IsNotNull(enumerator);

            i = 0;
            foreach (var vertex in linestrip)
            {
                TestUtilities.AssertThatVector2dsAreEqual(vertices[i], vertex);
                ++i;
            }
        }

        [Test]
        public void TestPointAtMethod()
        {
            // simple 1d case
            var vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, 0d)
            };

            var linestrip = new LineStrip2d(vertices);

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0d), linestrip.PointAlongAt(0d));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5d, 0d), linestrip.PointAlongAt(1d / 6d));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1d, 0d), linestrip.PointAlongAt(1d / 3d));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(2d, 0d), linestrip.PointAlongAt(1d / 1.5d));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(2.5d, 0d), linestrip.PointAlongAt(1d / 1.2d));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(3d, 0d), linestrip.PointAlongAt(1d));

            // complex 2d case
            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, -1d),
                new Vector2d(2d, -2d),
                new Vector2d(0d, -2d),
                new Vector2d(-1d, -2d),
                new Vector2d(-1d, -1d)
            };

            linestrip = new LineStrip2d(vertices);

            var totalDistance = Math.Sqrt(2) * 4d + 4d;
            var angledSectionT = Math.Sqrt(2) / totalDistance;
            var straightSectionT = 1f / totalDistance;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0d), linestrip.PointAlongAt(0d));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1d), linestrip.PointAlongAt(angledSectionT), Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(2d, 0d), linestrip.PointAlongAt(angledSectionT * 2d), Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(3d, -1d), linestrip.PointAlongAt(angledSectionT * 3d));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(2d, -2d), linestrip.PointAlongAt(angledSectionT * 4d), Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0d, -2d), linestrip.PointAlongAt(angledSectionT * 4d + straightSectionT * 2d), Mathd.EpsilonE15);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-1d, -2d), linestrip.PointAlongAt(angledSectionT * 4d + straightSectionT * 3d), Mathd.EpsilonE14);
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-1d), linestrip.PointAlongAt(angledSectionT * 4d + straightSectionT * 4d));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-1d), linestrip.PointAlongAt(1d));
        }

        [Test]
        public void TestVertexOrNextVertexAtMethod()
        {
            // simple 1d case
            var vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, 0d)
            };

            var linestrip = new LineStrip2d(vertices);

            TestUtilities.AssertThatVector2dsAreEqual(vertices[0], linestrip.NextPointAt(0d));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[1], linestrip.NextPointAt(1d / 6d));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[2], linestrip.NextPointAt(1d / 3d));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[3], linestrip.NextPointAt(1d / 1.5d));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[3], linestrip.NextPointAt(1d / 1.2d));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[3], linestrip.NextPointAt(1d));

            // complex 2d case
            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, -1d),
                new Vector2d(2d, -2d),
                new Vector2d(0d, -2d),
                new Vector2d(-1d, -2d),
                new Vector2d(-1d, -1d)
            };

            linestrip = new LineStrip2d(vertices);

            var totalDistance = Math.Sqrt(2) * 4d + 4d;
            var angledSectionT = Math.Sqrt(2) / totalDistance;
            var straightSectionT = 1f / totalDistance;
            const double stepBack = 0.01d;

            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0d), linestrip.NextPointAt(0d - stepBack));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1d), linestrip.NextPointAt(angledSectionT - stepBack));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(2d, 0d), linestrip.NextPointAt(angledSectionT * 2d - stepBack));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(3d, -1d), linestrip.NextPointAt(angledSectionT * 3d - stepBack));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(2d, -2d), linestrip.NextPointAt(angledSectionT * 4d - stepBack));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0d, -2d), linestrip.NextPointAt(angledSectionT * 4d + straightSectionT * 2d - stepBack));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-1d, -2d), linestrip.NextPointAt(angledSectionT * 4d + straightSectionT * 3d - stepBack));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-1d), linestrip.NextPointAt(angledSectionT * 4d + straightSectionT * 4d - stepBack));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(-1d), linestrip.NextPointAt(1d - stepBack));
        }

        [Test]
        public void TestDistanceAtMethod()
        {
            // simple 1d case
            var vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, 0d)
            };

            var linestrip = new LineStrip2d(vertices);

            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.DistanceAt(0));
            TestUtilities.AssertThatDoublesAreEqual(1d, linestrip.DistanceAt(1));
            TestUtilities.AssertThatDoublesAreEqual(2d, linestrip.DistanceAt(2));
            TestUtilities.AssertThatDoublesAreEqual(3d, linestrip.DistanceAt(3));

            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(-1d, 0d),
                new Vector2d(-2d, 0d),
                new Vector2d(-3d, 0d)
            };

            linestrip = new LineStrip2d(vertices);

            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.DistanceAt(0));
            TestUtilities.AssertThatDoublesAreEqual(1d, linestrip.DistanceAt(1));
            TestUtilities.AssertThatDoublesAreEqual(2d, linestrip.DistanceAt(2));
            TestUtilities.AssertThatDoublesAreEqual(3d, linestrip.DistanceAt(3));

            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(0d, 1d),
                new Vector2d(0d, 2d),
                new Vector2d(0d, 3d)
            };

            linestrip = new LineStrip2d(vertices);

            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.DistanceAt(0));
            TestUtilities.AssertThatDoublesAreEqual(1d, linestrip.DistanceAt(1));
            TestUtilities.AssertThatDoublesAreEqual(2d, linestrip.DistanceAt(2));
            TestUtilities.AssertThatDoublesAreEqual(3d, linestrip.DistanceAt(3));

            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(0d, -1d),
                new Vector2d(0d, -2d),
                new Vector2d(0d, -3d)
            };

            linestrip = new LineStrip2d(vertices);

            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.DistanceAt(0));
            TestUtilities.AssertThatDoublesAreEqual(1d, linestrip.DistanceAt(1));
            TestUtilities.AssertThatDoublesAreEqual(2d, linestrip.DistanceAt(2));
            TestUtilities.AssertThatDoublesAreEqual(3d, linestrip.DistanceAt(3));

            vertices = new List<Vector2d>
            {
                new Vector2d(10d, 0d),
                new Vector2d(11d, 0d),
                new Vector2d(12d, 0d),
                new Vector2d(13d, 0d)
            };

            linestrip = new LineStrip2d(vertices);

            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.DistanceAt(0));
            TestUtilities.AssertThatDoublesAreEqual(1d, linestrip.DistanceAt(1));
            TestUtilities.AssertThatDoublesAreEqual(2d, linestrip.DistanceAt(2));
            TestUtilities.AssertThatDoublesAreEqual(3d, linestrip.DistanceAt(3));

            Assert.Throws<ArgumentOutOfRangeException>(() => { linestrip.DistanceAt(-1); });
            Assert.Throws<ArgumentOutOfRangeException>(() => { linestrip.DistanceAt(4); });

            // complex 2d case
            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, -1d),
                new Vector2d(2d, -2d),
                new Vector2d(0d, -2d),
                new Vector2d(-1d, -2d),
                new Vector2d(-1d, -1d)
            };

            linestrip = new LineStrip2d(vertices);

            var angledSectionT = Math.Sqrt(2);
            var straightSectionT = 1f;

            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.DistanceAt(0));
            TestUtilities.AssertThatDoublesAreEqual(angledSectionT, linestrip.DistanceAt(1));
            TestUtilities.AssertThatDoublesAreEqual(angledSectionT * 2d, linestrip.DistanceAt(2));
            TestUtilities.AssertThatDoublesAreEqual(angledSectionT * 3d, linestrip.DistanceAt(3));
            TestUtilities.AssertThatDoublesAreEqual(angledSectionT * 4d, linestrip.DistanceAt(4));
            TestUtilities.AssertThatDoublesAreEqual(angledSectionT * 4d + straightSectionT * 2d, linestrip.DistanceAt(5));
            TestUtilities.AssertThatDoublesAreEqual(angledSectionT * 4d + straightSectionT * 3d, linestrip.DistanceAt(6));
            TestUtilities.AssertThatDoublesAreEqual(angledSectionT * 4d + straightSectionT * 4d, linestrip.DistanceAt(7));
        }

        [Test]
        public void TestPolarAngleAtMethod()
        {
            // simple case
            var vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, 1d)
            };

            var linestrip = new LineStrip2d(vertices);

            TestUtilities.AssertThatDoublesAreEqual(45d, linestrip.PolarAngleAt(0d));
            TestUtilities.AssertThatDoublesAreEqual(45d, linestrip.PolarAngleAt(0.25d));
            TestUtilities.AssertThatDoublesAreEqual(-45d, linestrip.PolarAngleAt(1d / 3d));
            TestUtilities.AssertThatDoublesAreEqual(-45d, linestrip.PolarAngleAt(0.5d));
            TestUtilities.AssertThatDoublesAreEqual(45d, linestrip.PolarAngleAt(0.75d));
            TestUtilities.AssertThatDoublesAreEqual(45d, linestrip.PolarAngleAt(1d));

            // complex 2d case
            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, -1d),
                new Vector2d(2d, -2d),
                new Vector2d(0d, -2d),
                new Vector2d(-1d, -2d),
                new Vector2d(-1d, -1d)
            };

            linestrip = new LineStrip2d(vertices);

            var totalDistance = Math.Sqrt(2) * 4d + 4d;
            var angledSectionT = Math.Sqrt(2) / totalDistance;
            var straightSectionT = 1f / totalDistance;

            TestUtilities.AssertThatDoublesAreEqual(45d, linestrip.PolarAngleAt(0d));
            TestUtilities.AssertThatDoublesAreEqual(-45d, linestrip.PolarAngleAt(angledSectionT));
            TestUtilities.AssertThatDoublesAreEqual(-45d, linestrip.PolarAngleAt(angledSectionT * 2d));
            TestUtilities.AssertThatDoublesAreEqual(-135d, linestrip.PolarAngleAt(angledSectionT * 3d));
            TestUtilities.AssertThatDoublesAreEqual(180d, linestrip.PolarAngleAt(angledSectionT * 4d));
            TestUtilities.AssertThatDoublesAreEqual(180d, linestrip.PolarAngleAt(angledSectionT * 4d + straightSectionT * 2d));
            TestUtilities.AssertThatDoublesAreEqual(90d, linestrip.PolarAngleAt(angledSectionT * 4d + straightSectionT * 3d));
            TestUtilities.AssertThatDoublesAreEqual(90d, linestrip.PolarAngleAt(angledSectionT * 4d + straightSectionT * 4d));
            TestUtilities.AssertThatDoublesAreEqual(90d, linestrip.PolarAngleAt(1d));
        }

        [Test]
        public void TestClosestIndexToMethod()
        {
            // simple 1d case
            var vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, 0d)
            };

            var linestrip = new LineStrip2d(vertices);

            Assert.AreEqual(0, linestrip.ClosestIndexTo(vertices[0]));
            Assert.AreEqual(1, linestrip.ClosestIndexTo(vertices[1]));
            Assert.AreEqual(2, linestrip.ClosestIndexTo(vertices[2]));
            Assert.AreEqual(3, linestrip.ClosestIndexTo(vertices[3]));

            Assert.AreEqual(0, linestrip.ClosestIndexTo(new Vector2d(0d, 1d)));
            Assert.AreEqual(1, linestrip.ClosestIndexTo(new Vector2d(1d, 1d)));
            Assert.AreEqual(2, linestrip.ClosestIndexTo(new Vector2d(2d, 1d)));
            Assert.AreEqual(3, linestrip.ClosestIndexTo(new Vector2d(3d, 1d)));

            Assert.AreEqual(0, linestrip.ClosestIndexTo(new Vector2d(0d, -1d)));
            Assert.AreEqual(1, linestrip.ClosestIndexTo(new Vector2d(1d, -1d)));
            Assert.AreEqual(2, linestrip.ClosestIndexTo(new Vector2d(2d, -1d)));
            Assert.AreEqual(3, linestrip.ClosestIndexTo(new Vector2d(3d, -1d)));

            // equidistant 1d case
            Assert.AreEqual(0, linestrip.ClosestIndexTo(new Vector2d(0.5d, 0d)));
            Assert.AreEqual(1, linestrip.ClosestIndexTo(new Vector2d(1.5d, 0d)));
            Assert.AreEqual(2, linestrip.ClosestIndexTo(new Vector2d(2.5d, 0d)));

            // outlier 1d case
            vertices = new List<Vector2d>
            {
                new Vector2d(-100d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, 0d)
            };

            linestrip = new LineStrip2d(vertices);

            Assert.AreEqual(0, linestrip.ClosestIndexTo(new Vector2d(-50d, 0d)));
            Assert.AreEqual(1, linestrip.ClosestIndexTo(Vector2d.Zero));
            Assert.AreEqual(1, linestrip.ClosestIndexTo(vertices[1]));
            Assert.AreEqual(2, linestrip.ClosestIndexTo(vertices[2]));
            Assert.AreEqual(3, linestrip.ClosestIndexTo(vertices[3]));

            // simple 2d case
            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, 1d)
            };

            linestrip = new LineStrip2d(vertices);

            Assert.AreEqual(0, linestrip.ClosestIndexTo(vertices[0]));
            Assert.AreEqual(1, linestrip.ClosestIndexTo(vertices[1]));
            Assert.AreEqual(2, linestrip.ClosestIndexTo(vertices[2]));
            Assert.AreEqual(3, linestrip.ClosestIndexTo(vertices[3]));

            Assert.AreEqual(0, linestrip.ClosestIndexTo(new Vector2d(0d, -1d)));
            Assert.AreEqual(1, linestrip.ClosestIndexTo(new Vector2d(1d, 2d)));
            Assert.AreEqual(2, linestrip.ClosestIndexTo(new Vector2d(2d, -1d)));
            Assert.AreEqual(3, linestrip.ClosestIndexTo(new Vector2d(4d, 1d)));

            Assert.AreEqual(0, linestrip.ClosestIndexTo(new Vector2d(-1d, -1d)));
            Assert.AreEqual(1, linestrip.ClosestIndexTo(new Vector2d(0d, 2d)));
            Assert.AreEqual(2, linestrip.ClosestIndexTo(new Vector2d(2d, -2d)));
            Assert.AreEqual(3, linestrip.ClosestIndexTo(new Vector2d(5d, 1d)));

            // equidistant 2d case
            Assert.AreEqual(0, linestrip.ClosestIndexTo(new Vector2d(0d, 1d)));
            Assert.AreEqual(1, linestrip.ClosestIndexTo(new Vector2d(2d, 1d)));
            Assert.AreEqual(2, linestrip.ClosestIndexTo(new Vector2d(3d, 0d)));

            // complex 2d case
            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, -1d),
                new Vector2d(2d, -2d),
                new Vector2d(0d, -2d),
                new Vector2d(-1d, -2d),
                new Vector2d(-1d, -1d)
            };

            linestrip = new LineStrip2d(vertices);

            Assert.AreEqual(0, linestrip.ClosestIndexTo(new Vector2d(0d, 0d)));
            Assert.AreEqual(1, linestrip.ClosestIndexTo(new Vector2d(1d, 1d)));
            Assert.AreEqual(2, linestrip.ClosestIndexTo(new Vector2d(2d, 0d)));
            Assert.AreEqual(3, linestrip.ClosestIndexTo(new Vector2d(3d, -1d)));
            Assert.AreEqual(4, linestrip.ClosestIndexTo(new Vector2d(2d, -2d)));
            Assert.AreEqual(5, linestrip.ClosestIndexTo(new Vector2d(0d, -2d)));
            Assert.AreEqual(6, linestrip.ClosestIndexTo(new Vector2d(-1d, -2d)));
            Assert.AreEqual(7, linestrip.ClosestIndexTo(new Vector2d(-1d, -1d)));

            Assert.AreEqual(0, linestrip.ClosestIndexTo(new Vector2d(-0.1d, 0.1d)));
            Assert.AreEqual(1, linestrip.ClosestIndexTo(new Vector2d(1d, 1.1d)));
            Assert.AreEqual(2, linestrip.ClosestIndexTo(new Vector2d(2.1d, 0.1d)));
            Assert.AreEqual(3, linestrip.ClosestIndexTo(new Vector2d(3d, -1.1d)));
            Assert.AreEqual(4, linestrip.ClosestIndexTo(new Vector2d(2.1d, -2.1d)));
            Assert.AreEqual(5, linestrip.ClosestIndexTo(new Vector2d(0.1d, -2.1d)));
            Assert.AreEqual(6, linestrip.ClosestIndexTo(new Vector2d(-1.1d, -2.1d)));
            Assert.AreEqual(7, linestrip.ClosestIndexTo(new Vector2d(-1.1d, -1d)));
        }

        [Test]
        public void TestClosestVertexToMethod()
        {
            // simple 1d case
            var vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, 0d)
            };

            var linestrip = new LineStrip2d(vertices);

            TestUtilities.AssertThatVector2dsAreEqual(vertices[0], linestrip.ClosestPoint(vertices[0]));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[1], linestrip.ClosestPoint(vertices[1]));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[2], linestrip.ClosestPoint(vertices[2]));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[3], linestrip.ClosestPoint(vertices[3]));

            TestUtilities.AssertThatVector2dsAreEqual(vertices[0], linestrip.ClosestPoint(new Vector2d(0d, 1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[1], linestrip.ClosestPoint(new Vector2d(1d, 1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[2], linestrip.ClosestPoint(new Vector2d(2d, 1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[3], linestrip.ClosestPoint(new Vector2d(3d, 1d)));

            TestUtilities.AssertThatVector2dsAreEqual(vertices[0], linestrip.ClosestPoint(new Vector2d(0d, -1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[1], linestrip.ClosestPoint(new Vector2d(1d, -1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[2], linestrip.ClosestPoint(new Vector2d(2d, -1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[3], linestrip.ClosestPoint(new Vector2d(3d, -1d)));

            // equidistant 1d case
            TestUtilities.AssertThatVector2dsAreEqual(vertices[0], linestrip.ClosestPoint(new Vector2d(0.5d, 0d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[1], linestrip.ClosestPoint(new Vector2d(1.5d, 0d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[2], linestrip.ClosestPoint(new Vector2d(2.5d, 0d)));

            // outlier 1d case
            vertices = new List<Vector2d>
            {
                new Vector2d(-100d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, 0d)
            };

            linestrip = new LineStrip2d(vertices);

            TestUtilities.AssertThatVector2dsAreEqual(vertices[0], linestrip.ClosestPoint(new Vector2d(-50d, 0d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[1], linestrip.ClosestPoint(Vector2d.Zero));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[1], linestrip.ClosestPoint(vertices[1]));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[2], linestrip.ClosestPoint(vertices[2]));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[3], linestrip.ClosestPoint(vertices[3]));

            // simple 2d case
            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, 1d)
            };

            linestrip = new LineStrip2d(vertices);

            TestUtilities.AssertThatVector2dsAreEqual(vertices[0], linestrip.ClosestPoint(vertices[0]));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[1], linestrip.ClosestPoint(vertices[1]));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[2], linestrip.ClosestPoint(vertices[2]));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[3], linestrip.ClosestPoint(vertices[3]));

            TestUtilities.AssertThatVector2dsAreEqual(vertices[0], linestrip.ClosestPoint(new Vector2d(0d, -1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[1], linestrip.ClosestPoint(new Vector2d(1d, 2d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[2], linestrip.ClosestPoint(new Vector2d(2d, -1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[3], linestrip.ClosestPoint(new Vector2d(4d, 1d)));

            TestUtilities.AssertThatVector2dsAreEqual(vertices[0], linestrip.ClosestPoint(new Vector2d(-1d, -1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[1], linestrip.ClosestPoint(new Vector2d(0d, 2d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[2], linestrip.ClosestPoint(new Vector2d(2d, -2d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[3], linestrip.ClosestPoint(new Vector2d(5d, 1d)));

            // equidistant 2d case
            TestUtilities.AssertThatVector2dsAreEqual(vertices[0], linestrip.ClosestPoint(new Vector2d(0d, 1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[1], linestrip.ClosestPoint(new Vector2d(2d, 1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[2], linestrip.ClosestPoint(new Vector2d(3d, 0d)));

            // complex 2d case
            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, -1d),
                new Vector2d(2d, -2d),
                new Vector2d(0d, -2d),
                new Vector2d(-1d, -2d),
                new Vector2d(-1d, -1d)
            };

            linestrip = new LineStrip2d(vertices);

            TestUtilities.AssertThatVector2dsAreEqual(vertices[0], linestrip.ClosestPoint(new Vector2d(0d, 0d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[1], linestrip.ClosestPoint(new Vector2d(1d, 1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[2], linestrip.ClosestPoint(new Vector2d(2d, 0d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[3], linestrip.ClosestPoint(new Vector2d(3d, -1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[4], linestrip.ClosestPoint(new Vector2d(2d, -2d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[5], linestrip.ClosestPoint(new Vector2d(0d, -2d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[6], linestrip.ClosestPoint(new Vector2d(-1d, -2d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[7], linestrip.ClosestPoint(new Vector2d(-1d, -1d)));

            TestUtilities.AssertThatVector2dsAreEqual(vertices[0], linestrip.ClosestPoint(new Vector2d(-0.1d, 0.1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[1], linestrip.ClosestPoint(new Vector2d(1d, 1.1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[2], linestrip.ClosestPoint(new Vector2d(2.1d, 0.1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[3], linestrip.ClosestPoint(new Vector2d(3d, -1.1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[4], linestrip.ClosestPoint(new Vector2d(2.1d, -2.1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[5], linestrip.ClosestPoint(new Vector2d(0.1d, -2.1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[6], linestrip.ClosestPoint(new Vector2d(-1.1d, -2.1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[7], linestrip.ClosestPoint(new Vector2d(-1.1d, -1d)));
        }

        [Test]
        public void TestClosestTimeTo()
        {
            // simple 2d case
            var vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d)
            };

            var linestrip = new LineStrip2d(vertices);

            // identity check
            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.ClosestTimeTo(new Vector2d(0d, 0d)));
            TestUtilities.AssertThatDoublesAreEqual(1d, linestrip.ClosestTimeTo(new Vector2d(1d, 0d)));

            // simple potential cases
            TestUtilities.AssertThatDoublesAreEqual(1d, linestrip.ClosestTimeTo(new Vector2d(1d, 1d)));
            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.ClosestTimeTo(new Vector2d(0d, 1d)));
            TestUtilities.AssertThatDoublesAreEqual(1d, linestrip.ClosestTimeTo(new Vector2d(1d, -1d)));
            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.ClosestTimeTo(new Vector2d(0d, -1d)));
            TestUtilities.AssertThatDoublesAreEqual(1d, linestrip.ClosestTimeTo(new Vector2d(2d, 0d)));
            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.ClosestTimeTo(new Vector2d(-2d, 0d)));
            TestUtilities.AssertThatDoublesAreEqual(0.5d, linestrip.ClosestTimeTo(new Vector2d(0.5d, 0d)));
            TestUtilities.AssertThatDoublesAreEqual(0.5d, linestrip.ClosestTimeTo(new Vector2d(0.5d, 1d)));
            TestUtilities.AssertThatDoublesAreEqual(0.5d, linestrip.ClosestTimeTo(new Vector2d(0.5d, -1d)));

            // size zero case
            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(0d, 0d)
            };

            linestrip = new LineStrip2d(vertices);

            // identity check
            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.ClosestTimeTo(new Vector2d(0d, 0d)));
            TestUtilities.AssertThatDoublesAreEqual(1d, linestrip.ClosestTimeTo(new Vector2d(1d, 0d)));

            // complex 2d case
            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, -1d),
                new Vector2d(2d, -2d),
                new Vector2d(0d, -2d),
                new Vector2d(-1d, -2d),
                new Vector2d(-1d, -1d)
            };

            linestrip = new LineStrip2d(vertices);

            var totalDistance = Math.Sqrt(2) * 4d + 4d;
            var angledSectionT = Math.Sqrt(2) / totalDistance;
            var straightSectionT = 1f / totalDistance;

            // idenitity check
            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.ClosestTimeTo(new Vector2d(0d, 0d)));
            TestUtilities.AssertThatDoublesAreEqual(angledSectionT, linestrip.ClosestTimeTo(new Vector2d(1d, 1d)));
            TestUtilities.AssertThatDoublesAreEqual(angledSectionT * 2d, linestrip.ClosestTimeTo(new Vector2d(2d, 0d)));
            TestUtilities.AssertThatDoublesAreEqual(angledSectionT * 3d, linestrip.ClosestTimeTo(new Vector2d(3d, -1d)));
            TestUtilities.AssertThatDoublesAreEqual(angledSectionT * 4d, linestrip.ClosestTimeTo(new Vector2d(2d, -2d)));
            TestUtilities.AssertThatDoublesAreEqual(angledSectionT * 4d + straightSectionT * 2d, linestrip.ClosestTimeTo(new Vector2d(0d, -2d)), Mathd.EpsilonE15);
            TestUtilities.AssertThatDoublesAreEqual(angledSectionT * 4d + straightSectionT * 3d, linestrip.ClosestTimeTo(new Vector2d(-1d, -2d)), Mathd.EpsilonE15);
            TestUtilities.AssertThatDoublesAreEqual(angledSectionT * 4d + straightSectionT * 4d, linestrip.ClosestTimeTo(new Vector2d(-1d, -1d)), Mathd.EpsilonE15);

            // offset vertices check
            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.ClosestTimeTo(new Vector2d(-0.1d, 0d)));
            TestUtilities.AssertThatDoublesAreEqual(angledSectionT, linestrip.ClosestTimeTo(new Vector2d(1d, 1.1d)));
            TestUtilities.AssertThatDoublesAreEqual(angledSectionT * 2d, linestrip.ClosestTimeTo(new Vector2d(2.1d, 0.1d)));
            TestUtilities.AssertThatDoublesAreEqual(angledSectionT * 3d, linestrip.ClosestTimeTo(new Vector2d(3.1d, -1d)));
            TestUtilities.AssertThatDoublesAreEqual(angledSectionT * 4d, linestrip.ClosestTimeTo(new Vector2d(2.1d, -2.1d)));
            TestUtilities.AssertThatDoublesAreEqual(angledSectionT * 4d + straightSectionT * 2d, linestrip.ClosestTimeTo(new Vector2d(0d, -2.1d)), Mathd.EpsilonE15);
            TestUtilities.AssertThatDoublesAreEqual(angledSectionT * 4d + straightSectionT * 3d, linestrip.ClosestTimeTo(new Vector2d(-1d, -2.1d)), Mathd.EpsilonE15);
            TestUtilities.AssertThatDoublesAreEqual(angledSectionT * 4d + straightSectionT * 4d, linestrip.ClosestTimeTo(new Vector2d(-1.1d, -1d)), Mathd.EpsilonE15);
        }

        [Test]
        public void TestClosestPointTo()
        {
            // simple 2d case
            var vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d)
            };

            var linestrip = new LineStrip2d(vertices);

            // identity check
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0d, 0d), linestrip.ClosestPointAlong(new Vector2d(0d, 0d)));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1d, 0d), linestrip.ClosestPointAlong(new Vector2d(1d, 0d)));

            // simple potential cases
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1d, 0d), linestrip.ClosestPointAlong(new Vector2d(1d, 1d)));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0d, 0d), linestrip.ClosestPointAlong(new Vector2d(0d, 1d)));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1d, 0d), linestrip.ClosestPointAlong(new Vector2d(1d, -1d)));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0d, 0d), linestrip.ClosestPointAlong(new Vector2d(0d, -1d)));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(1d, 0d), linestrip.ClosestPointAlong(new Vector2d(2d, 0d)));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0d, 0d), linestrip.ClosestPointAlong(new Vector2d(-2d, 0d)));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5d, 0d), linestrip.ClosestPointAlong(new Vector2d(0.5d, 0d)));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5d, 0d), linestrip.ClosestPointAlong(new Vector2d(0.5d, 1d)));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0.5d, 0d), linestrip.ClosestPointAlong(new Vector2d(0.5d, -1d)));

            // size zero case
            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(0d, 0d)
            };

            linestrip = new LineStrip2d(vertices);

            // identity check
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0d, 0d), linestrip.ClosestPointAlong(new Vector2d(0d, 0d)));
            TestUtilities.AssertThatVector2dsAreEqual(new Vector2d(0d, 0d), linestrip.ClosestPointAlong(new Vector2d(1d, 0d)));

            // complex 2d case
            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, -1d),
                new Vector2d(2d, -2d),
                new Vector2d(0d, -2d),
                new Vector2d(-1d, -2d),
                new Vector2d(-1d, -1d)
            };

            linestrip = new LineStrip2d(vertices);

            // identity check
            TestUtilities.AssertThatVector2dsAreEqual(vertices[0], linestrip.ClosestPointAlong(vertices[0]));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[1], linestrip.ClosestPointAlong(vertices[1]));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[2], linestrip.ClosestPointAlong(vertices[2]));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[3], linestrip.ClosestPointAlong(vertices[3]));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[4], linestrip.ClosestPointAlong(vertices[4]));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[5], linestrip.ClosestPointAlong(vertices[5]));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[6], linestrip.ClosestPointAlong(vertices[6]));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[7], linestrip.ClosestPointAlong(vertices[7]));

            // offset vertices check
            TestUtilities.AssertThatVector2dsAreEqual(vertices[0], linestrip.ClosestPointAlong(vertices[0] + new Vector2d(-0.1d, 0d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[1], linestrip.ClosestPointAlong(vertices[1] + new Vector2d(0.0d, 0.1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[2], linestrip.ClosestPointAlong(vertices[2] + new Vector2d(0.1d, 0.1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[3], linestrip.ClosestPointAlong(vertices[3] + new Vector2d(0.1d, 0d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[4], linestrip.ClosestPointAlong(vertices[4] + new Vector2d(0.1d, -0.1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[5], linestrip.ClosestPointAlong(vertices[5] + new Vector2d(0d, -0.1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[6], linestrip.ClosestPointAlong(vertices[6] + new Vector2d(0d, -0.1d)));
            TestUtilities.AssertThatVector2dsAreEqual(vertices[7], linestrip.ClosestPointAlong(vertices[7] + new Vector2d(-0.1d, 0d)));
        }

        [Test]
        public void TestLeastDistanceTo()
        {
            // simple 2d case
            var vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d)
            };

            var linestrip = new LineStrip2d(vertices);

            // identity check
            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.LeastDistanceTo(new Vector2d(0d, 0d)));
            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.LeastDistanceTo(new Vector2d(1d, 0d)));

            // simple potential cases
            TestUtilities.AssertThatDoublesAreEqual(1d, linestrip.LeastDistanceTo(new Vector2d(1d, 1d)));
            TestUtilities.AssertThatDoublesAreEqual(1d, linestrip.LeastDistanceTo(new Vector2d(0d, 1d)));
            TestUtilities.AssertThatDoublesAreEqual(1d, linestrip.LeastDistanceTo(new Vector2d(1d, -1d)));
            TestUtilities.AssertThatDoublesAreEqual(1d, linestrip.LeastDistanceTo(new Vector2d(0d, -1d)));
            TestUtilities.AssertThatDoublesAreEqual(1d, linestrip.LeastDistanceTo(new Vector2d(2d, 0d)));
            TestUtilities.AssertThatDoublesAreEqual(2d, linestrip.LeastDistanceTo(new Vector2d(-2d, 0d)));
            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.LeastDistanceTo(new Vector2d(0.5d, 0d)));
            TestUtilities.AssertThatDoublesAreEqual(1d, linestrip.LeastDistanceTo(new Vector2d(0.5d, 1d)));
            TestUtilities.AssertThatDoublesAreEqual(1d, linestrip.LeastDistanceTo(new Vector2d(0.5d, -1d)));

            // size zero case
            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(0d, 0d)
            };

            linestrip = new LineStrip2d(vertices);

            // identity check
            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.LeastDistanceTo(new Vector2d(0d, 0d)));
            TestUtilities.AssertThatDoublesAreEqual(1d, linestrip.LeastDistanceTo(new Vector2d(1d, 0d)));

            // complex 2d case
            vertices = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(2d, 0d),
                new Vector2d(3d, -1d),
                new Vector2d(2d, -2d),
                new Vector2d(0d, -2d),
                new Vector2d(-1d, -2d),
                new Vector2d(-1d, -1d)
            };

            linestrip = new LineStrip2d(vertices);

            // identity check
            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.LeastDistanceTo(vertices[0]));
            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.LeastDistanceTo(vertices[1]));
            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.LeastDistanceTo(vertices[2]));
            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.LeastDistanceTo(vertices[3]));
            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.LeastDistanceTo(vertices[4]));
            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.LeastDistanceTo(vertices[5]));
            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.LeastDistanceTo(vertices[6]));
            TestUtilities.AssertThatDoublesAreEqual(0d, linestrip.LeastDistanceTo(vertices[7]));

            // offset vertices check
            TestUtilities.AssertThatDoublesAreEqual(0.1d, linestrip.LeastDistanceTo(vertices[0] + new Vector2d(-0.1d, 0d)));
            TestUtilities.AssertThatDoublesAreEqual(0.1d, linestrip.LeastDistanceTo(vertices[1] + new Vector2d(0.0d, 0.1d)), Mathd.EpsilonE16);
            TestUtilities.AssertThatDoublesAreEqual(Math.Sqrt(0.02d), linestrip.LeastDistanceTo(vertices[2] + new Vector2d(0.1d, 0.1d)), Mathd.EpsilonE16);
            TestUtilities.AssertThatDoublesAreEqual(0.1d, linestrip.LeastDistanceTo(vertices[3] + new Vector2d(0.1d, 0d)), Mathd.EpsilonE16);
            TestUtilities.AssertThatDoublesAreEqual(Math.Sqrt(0.02d), linestrip.LeastDistanceTo(vertices[4] + new Vector2d(0.1d, -0.1d)), Mathd.EpsilonE15);
            TestUtilities.AssertThatDoublesAreEqual(0.1d, linestrip.LeastDistanceTo(vertices[5] + new Vector2d(0d, -0.1d)), Mathd.EpsilonE16);
            TestUtilities.AssertThatDoublesAreEqual(0.1d, linestrip.LeastDistanceTo(vertices[6] + new Vector2d(0d, -0.1d)), Mathd.EpsilonE16);
            TestUtilities.AssertThatDoublesAreEqual(0.1d, linestrip.LeastDistanceTo(vertices[7] + new Vector2d(-0.1d, 0d)), Mathd.EpsilonE16);
        }

        [Test]
        public void TestSegmentsCopyMethod()
        {
            // simple 2d case
            var vertices = new List<Vector2d>
            {
                Vector2d.Zero,
                Vector2d.One
            };

            var linestrip = new LineStrip2d(vertices);
            var segments = linestrip.SegmentsCopy();

            Assert.AreEqual(1, segments.Length);

            vertices = new List<Vector2d>
            {
                Vector2d.Zero,
                Vector2d.One,
                Vector2d.One * 2
            };

            linestrip = new LineStrip2d(vertices);
            segments = linestrip.SegmentsCopy();

            Assert.AreEqual(2, segments.Length);

            vertices = new List<Vector2d>
            {
                Vector2d.Zero,
                Vector2d.One,
                Vector2d.One * 2,
                Vector2d.One * 3
            };

            linestrip = new LineStrip2d(vertices);
            segments = linestrip.SegmentsCopy();

            Assert.AreEqual(3, segments.Length);

            segments = new []
            {
                new LineSegment2d(new Vector2d(0d, 0d), new Vector2d(0d, 1d)),
                new LineSegment2d(new Vector2d(0d, 1d), new Vector2d(1d, 1d)),
                new LineSegment2d(new Vector2d(1d, 1d), new Vector2d(1d, 0d)),
                new LineSegment2d(new Vector2d(1d, 0d), new Vector2d(0d, 0d)),
            };

            linestrip = new LineStrip2d(segments);
            var segmentsCopy = linestrip.SegmentsCopy();

            // assert that the segments array is not the same object as the copy returned
            Assert.IsFalse(ReferenceEquals(segments, segmentsCopy));
        }

        [Test]
        public void TestPointsCopyMethod()
        {
            var points = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(0d, 1d)
            };

            var linestrip = new LineStrip2d(points);
            var pointsCopy = linestrip.PointsCopy();

            // assert that the points array is not the same object as the copy returned
            Assert.IsFalse(ReferenceEquals(points, pointsCopy));
        }

        [Test]
        public void TestToClockwiseMethod()
        {
            var points = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(0d, 1d)
            };

            var linestrip = new LineStrip2d(points);

            Assert.IsFalse(linestrip.Clockwise);

            // assert the clockwiseness change
            linestrip = linestrip.ToClockwise(true);
            Assert.IsTrue(linestrip.Clockwise);

            // assert the reversal of point order
            TestUtilities.AssertThatVector2dsAreEqual(points[0], linestrip[3]);
            TestUtilities.AssertThatVector2dsAreEqual(points[1], linestrip[2]);
            TestUtilities.AssertThatVector2dsAreEqual(points[2], linestrip[1]);
            TestUtilities.AssertThatVector2dsAreEqual(points[3], linestrip[0]);

            points = new List<Vector2d>
            {
                new Vector2d(0d, 1d),
                new Vector2d(1d, 1d),
                new Vector2d(1d, 0d),
                new Vector2d(0d, 0d),
            };

            linestrip = new LineStrip2d(points);

            Assert.IsTrue(linestrip.Clockwise);

            // assert the clockwiseness change
            linestrip = linestrip.ToClockwise(false);
            Assert.IsFalse(linestrip.Clockwise);

            // assert the reversal of point order
            TestUtilities.AssertThatVector2dsAreEqual(points[0], linestrip[3]);
            TestUtilities.AssertThatVector2dsAreEqual(points[1], linestrip[2]);
            TestUtilities.AssertThatVector2dsAreEqual(points[2], linestrip[1]);
            TestUtilities.AssertThatVector2dsAreEqual(points[3], linestrip[0]);
        }

        [Test]
        public void TestTryJoinMethod()
        {
            var aPoints = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
            };

            var a = new LineStrip2d(aPoints);

            var bPoints = new List<Vector2d>
            {
                new Vector2d(1d, 0d),
                new Vector2d(2d, 0d),
            };

            var b = new LineStrip2d(bPoints);

            LineStrip2d c;
            Assert.IsTrue(a.TryJoin(b, out c));
            Assert.AreEqual(3, c.Count);
            TestUtilities.AssertThatVector2dsAreEqual(aPoints[0], c[0]);
            TestUtilities.AssertThatVector2dsAreEqual(aPoints[1], c[1]);
            TestUtilities.AssertThatVector2dsAreEqual(bPoints[1], c[2]);
            Assert.IsFalse(b.TryJoin(a, out c));

            aPoints = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
            };

            a = new LineStrip2d(aPoints);

            bPoints = new List<Vector2d>
            {
                new Vector2d(2d, 0d),
                new Vector2d(3d, 0d),
            };

            b = new LineStrip2d(bPoints);

            Assert.IsFalse(a.TryJoin(b, out c));
            Assert.IsFalse(b.TryJoin(a, out c));
        }

        [Test]
        public void TestCombineMethodSimpleCase()
        {
            var aPoints = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
            };

            var a = new LineStrip2d(aPoints);

            var bPoints = new List<Vector2d>
            {
                new Vector2d(1d, 0d),
                new Vector2d(2d, 0d),
            };

            var b = new LineStrip2d(bPoints);

            var strips = new List<LineStrip2d> {a, b};

            var combined = LineStrip2d.Combine(strips);
            Assert.IsNotNull(combined);
            Assert.IsNotEmpty(combined);
            Assert.AreEqual(1, combined.Count);

            var c = combined[0];
            Assert.AreEqual(3, c.Count);
            TestUtilities.AssertThatVector2dsAreEqual(aPoints[0], c[0]);
            TestUtilities.AssertThatVector2dsAreEqual(aPoints[1], c[1]);
            TestUtilities.AssertThatVector2dsAreEqual(bPoints[1], c[2]);

            // reverse case
            strips = new List<LineStrip2d> { b, a };

            combined = LineStrip2d.Combine(strips);
            Assert.IsNotNull(combined);
            Assert.IsNotEmpty(combined);
            Assert.AreEqual(1, combined.Count);

            c = combined[0];
            Assert.AreEqual(3, c.Count);
            TestUtilities.AssertThatVector2dsAreEqual(aPoints[0], c[0]);
            TestUtilities.AssertThatVector2dsAreEqual(aPoints[1], c[1]);
            TestUtilities.AssertThatVector2dsAreEqual(bPoints[1], c[2]);
        }

        [Test]
        public void TestCombineMethodSimpleDisjointCase()
        {
            var aPoints = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
            };

            var a = new LineStrip2d(aPoints);

            var bPoints = new List<Vector2d>
            {
                new Vector2d(1d, 0d),
                new Vector2d(2d, 0d),
            };

            var b = new LineStrip2d(bPoints);

            var cPoints = new List<Vector2d>
            {
                new Vector2d(0d, 1d),
                new Vector2d(1d, 1d),
            };

            var c = new LineStrip2d(cPoints);

            var dPoints = new List<Vector2d>
            {
                new Vector2d(1d, 1d),
                new Vector2d(2d, 1d),
            };

            var d = new LineStrip2d(dPoints);

            var strips = new List<LineStrip2d> { a, b, c, d };

            var combined = LineStrip2d.Combine(strips);
            Assert.IsNotNull(combined);
            Assert.IsNotEmpty(combined);
            Assert.AreEqual(2, combined.Count);

            var e = combined[0];
            Assert.AreEqual(3, e.Count);
            TestUtilities.AssertThatVector2dsAreEqual(aPoints[0], e[0]);
            TestUtilities.AssertThatVector2dsAreEqual(aPoints[1], e[1]);
            TestUtilities.AssertThatVector2dsAreEqual(bPoints[1], e[2]);

            var f = combined[1];
            Assert.AreEqual(3, f.Count);
            TestUtilities.AssertThatVector2dsAreEqual(cPoints[0], f[0]);
            TestUtilities.AssertThatVector2dsAreEqual(cPoints[1], f[1]);
            TestUtilities.AssertThatVector2dsAreEqual(dPoints[1], f[2]);

            // reverse case
            strips = new List<LineStrip2d> { c, d, a, b };

            combined = LineStrip2d.Combine(strips);
            Assert.IsNotNull(combined);
            Assert.IsNotEmpty(combined);
            Assert.AreEqual(2, combined.Count);

            e = combined[0];
            Assert.AreEqual(3, e.Count);
            TestUtilities.AssertThatVector2dsAreEqual(cPoints[0], e[0]);
            TestUtilities.AssertThatVector2dsAreEqual(cPoints[1], e[1]);
            TestUtilities.AssertThatVector2dsAreEqual(dPoints[1], e[2]);

            f = combined[1];
            Assert.AreEqual(3, f.Count);
            TestUtilities.AssertThatVector2dsAreEqual(aPoints[0], f[0]);
            TestUtilities.AssertThatVector2dsAreEqual(aPoints[1], f[1]);
            TestUtilities.AssertThatVector2dsAreEqual(bPoints[1], f[2]);

            // reverse case again
            strips = new List<LineStrip2d> { d, c, a, b };

            combined = LineStrip2d.Combine(strips);
            Assert.IsNotNull(combined);
            Assert.IsNotEmpty(combined);
            Assert.AreEqual(2, combined.Count);

            e = combined[0];
            Assert.AreEqual(3, e.Count);
            TestUtilities.AssertThatVector2dsAreEqual(cPoints[0], e[0]);
            TestUtilities.AssertThatVector2dsAreEqual(cPoints[1], e[1]);
            TestUtilities.AssertThatVector2dsAreEqual(dPoints[1], e[2]);

            f = combined[1];
            Assert.AreEqual(3, f.Count);
            TestUtilities.AssertThatVector2dsAreEqual(aPoints[0], f[0]);
            TestUtilities.AssertThatVector2dsAreEqual(aPoints[1], f[1]);
            TestUtilities.AssertThatVector2dsAreEqual(bPoints[1], f[2]);

            // reverse case again
            strips = new List<LineStrip2d> { d, c, b, a };

            combined = LineStrip2d.Combine(strips);
            Assert.IsNotNull(combined);
            Assert.IsNotEmpty(combined);
            Assert.AreEqual(2, combined.Count);

            e = combined[0];
            Assert.AreEqual(3, e.Count);
            TestUtilities.AssertThatVector2dsAreEqual(cPoints[0], e[0]);
            TestUtilities.AssertThatVector2dsAreEqual(cPoints[1], e[1]);
            TestUtilities.AssertThatVector2dsAreEqual(dPoints[1], e[2]);

            f = combined[1];
            Assert.AreEqual(3, f.Count);
            TestUtilities.AssertThatVector2dsAreEqual(aPoints[0], f[0]);
            TestUtilities.AssertThatVector2dsAreEqual(aPoints[1], f[1]);
            TestUtilities.AssertThatVector2dsAreEqual(bPoints[1], f[2]);

            // random order case 1
            strips = new List<LineStrip2d> { d, b, a, c };

            combined = LineStrip2d.Combine(strips);
            Assert.IsNotNull(combined);
            Assert.IsNotEmpty(combined);
            Assert.AreEqual(2, combined.Count);

            e = combined[0];
            Assert.AreEqual(3, e.Count);
            TestUtilities.AssertThatVector2dsAreEqual(aPoints[0], e[0]);
            TestUtilities.AssertThatVector2dsAreEqual(aPoints[1], e[1]);
            TestUtilities.AssertThatVector2dsAreEqual(bPoints[1], e[2]);

            f = combined[1];
            Assert.AreEqual(3, f.Count);
            TestUtilities.AssertThatVector2dsAreEqual(cPoints[0], f[0]);
            TestUtilities.AssertThatVector2dsAreEqual(cPoints[1], f[1]);
            TestUtilities.AssertThatVector2dsAreEqual(dPoints[1], f[2]);

            // random order case 2
            strips = new List<LineStrip2d> { c, a, d, b };

            combined = LineStrip2d.Combine(strips);
            Assert.IsNotNull(combined);
            Assert.IsNotEmpty(combined);
            Assert.AreEqual(2, combined.Count);

            e = combined[0];
            Assert.AreEqual(3, e.Count);
            TestUtilities.AssertThatVector2dsAreEqual(cPoints[0], e[0]);
            TestUtilities.AssertThatVector2dsAreEqual(cPoints[1], e[1]);
            TestUtilities.AssertThatVector2dsAreEqual(dPoints[1], e[2]);

            f = combined[1];
            Assert.AreEqual(3, f.Count);
            TestUtilities.AssertThatVector2dsAreEqual(aPoints[0], f[0]);
            TestUtilities.AssertThatVector2dsAreEqual(aPoints[1], f[1]);
            TestUtilities.AssertThatVector2dsAreEqual(bPoints[1], f[2]);
        }

        [Test]
        public void TestEqualsMethod()
        {
            var aPoints = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
            };

            var a = new LineStrip2d(aPoints);

            LineStrip2d b = null;

            Assert.IsFalse(a.Equals(b));
        }
    }
}