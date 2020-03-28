using System.Collections.Generic;
using Maps.Geometry;
using NUnit.Framework;

namespace Maps.Tests.Geometry
{
    /// <summary>
    /// Series of tests for the Polygon2d class
    /// </summary>
    [TestFixture]
    internal sealed class Polygon2dTests
    {
        /// <summary>
        /// Tests the constructor
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var points = new[]
            {
                new Vector2d(0, 0),
                new Vector2d(0, 10),
                new Vector2d(10, 10),
                new Vector2d(10, 0),
            };

            var poly = new Polygon2d(points);
        }

        /// <summary>
        /// Tests the area property in a very simple case
        /// </summary>
        [Test]
        public void TestAreaPropertySimpleCase()
        {
            var points = new []
            {
                new Vector2d(0, 0),
                new Vector2d(0, 10),
                new Vector2d(10, 10),
                new Vector2d(10, 0),
            };

            var poly = new Polygon2d(points);
            Assert.AreEqual(100d, poly.Area);

            points = new[]
            {
                new Vector2d(0, 0),
                new Vector2d(0, 10),
                new Vector2d(10, 10),
                new Vector2d(0, 0),
            };

            poly = new Polygon2d(points);
            Assert.AreEqual(50d, poly.Area);

            points = new[]
            {
                new Vector2d(0, 0),
                new Vector2d(0, 5),
                new Vector2d(5, 5),
                new Vector2d(5, 0),
            };

            poly = new Polygon2d(points);
            Assert.AreEqual(25d, poly.Area);

            points = new[]
            {
                new Vector2d(0, 0),
                new Vector2d(0, 5),
                new Vector2d(5, 5),
                new Vector2d(0, 0),
            };

            poly = new Polygon2d(points);
            Assert.AreEqual(12.5d, poly.Area);
        }

        [Test]
        public void TestTryJoinMethod()
        {
            var aPoints = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(1d, -1d),
                new Vector2d(0d, -1d),
            };

            var a = new Polygon2d(aPoints);

            var bPoints = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(0d, 1d),
            };

            var b = new Polygon2d(bPoints);

            Polygon2d c;
            Assert.IsTrue(a.TryJoin(b, out c));
            Assert.AreEqual(5, c.Count);
            Assert.AreEqual(0, c.HoleCount);

            // reverse case
            Assert.IsTrue(b.TryJoin(a, out c));
            Assert.AreEqual(5, c.Count);
            Assert.AreEqual(0, c.HoleCount);
        }

        [Test]
        public void TestTryJoinMethodWithSingleHole()
        {
            var aPoints = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(1d, -1d),
                new Vector2d(0d, -1d),
            };

            var a = new Polygon2d(aPoints);

            var cPoints = new List<Vector2d>
            {
                new Vector2d(0.25d, 0.25d),
                new Vector2d(0.75d, 0.25d),
                new Vector2d(0.75d, 0.75d),
                new Vector2d(0.25d, 0.75d),
            };

            var c = new Polygon2d(cPoints);

            var bPoints = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(0d, 1d),
            };

            var b = new Polygon2d(bPoints, new List<Polygon2d>
            {
                c
            });

            Polygon2d d;
            Assert.IsTrue(a.TryJoin(b, out d));
            Assert.AreEqual(5, d.Count);
            Assert.AreEqual(1, d.HoleCount);

            // reverse case
            Assert.IsTrue(b.TryJoin(a, out d));
            Assert.AreEqual(5, d.Count);
            Assert.AreEqual(1, d.HoleCount);
        }

        [Test]
        public void TestTryJoinMethodWithTwoHoles()
        {
            var dPoints = new List<Vector2d>
            {
                new Vector2d(0.0d, -0.25d),
                new Vector2d(0.75d, -0.25d),
                new Vector2d(0.75d, -0.75d),
                new Vector2d(0.0d, -0.75d),
            };

            var d = new Polygon2d(dPoints);

            var aPoints = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(1d, -1d),
                new Vector2d(0d, -1d),
            };

            var a = new Polygon2d(aPoints, new List<Polygon2d>
            {
                d
            });

            var cPoints = new List<Vector2d>
            {
                new Vector2d(0.25d, 0.25d),
                new Vector2d(0.75d, 0.25d),
                new Vector2d(0.75d, 0.75d),
                new Vector2d(0.25d, 0.75d),
            };

            var c = new Polygon2d(cPoints);

            var bPoints = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(0d, 1d),
            };

            var b = new Polygon2d(bPoints, new List<Polygon2d>
            {
                c
            });

            Polygon2d e;
            Assert.IsTrue(a.TryJoin(b, out e));
            Assert.AreEqual(5, e.Count);
            Assert.AreEqual(2, e.HoleCount);

            // reverse case
            Assert.IsTrue(b.TryJoin(a, out e));
            Assert.AreEqual(5, e.Count);
            Assert.AreEqual(2, e.HoleCount);
        }

        [Test]
        public void TestTryJoinMethodTwoOpenHolesHole()
        {
            var dPoints = new List<Vector2d>
            {
                new Vector2d(0.25d, 0.0d),
                new Vector2d(0.75d, 0.0d),
                new Vector2d(0.75d, -0.75d),
                new Vector2d(0.25d, -0.75d),
            };

            var d = new Polygon2d(dPoints);

            var aPoints = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(1d, -1d),
                new Vector2d(0d, -1d),
            };

            var a = new Polygon2d(aPoints, new List<Polygon2d>
            {
                d
            });

            var cPoints = new List<Vector2d>
            {
                new Vector2d(0.25d, 0.0d),
                new Vector2d(0.75d, 0.0d),
                new Vector2d(0.75d, 0.75d),
                new Vector2d(0.25d, 0.75d),
            };

            var c = new Polygon2d(cPoints);

            var bPoints = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(0d, 1d),
            };

            var b = new Polygon2d(bPoints, new List<Polygon2d>
            {
                c
            });

            Polygon2d e;
            Assert.IsTrue(a.TryJoin(b, out e));
            Assert.AreEqual(5, e.Count);
            Assert.AreEqual(1, e.HoleCount);

            // reverse case
            Assert.IsTrue(b.TryJoin(a, out e));
            Assert.AreEqual(5, e.Count);
            Assert.AreEqual(1, e.HoleCount);
        }

        [Test]
        public void TestCombineMethodSimpleCase()
        {
            var aPoints = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(1d, -1d),
                new Vector2d(0d, -1d),
            };

            var a = new Polygon2d(aPoints);

            var bPoints = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(0d, 1d),
            };

            var b = new Polygon2d(bPoints);

            var c = Polygon2d.Combine(new List<Polygon2d>
            {
                a,
                b
            });
            Assert.AreEqual(1, c.Count);
            Assert.AreEqual(5, c[0].Count);
            Assert.AreEqual(0, c[0].HoleCount);

            // reverse case
            c = Polygon2d.Combine(new List<Polygon2d>
            {
                a,
                b
            });
            Assert.AreEqual(1, c.Count);
            Assert.AreEqual(5, c[0].Count);
            Assert.AreEqual(0, c[0].HoleCount);
        }

        [Test]
        public void TestCombineMethodDisjointSimpleCase()
        {
            var aPoints = new List<Vector2d>
            {
                new Vector2d(-1d, 0d),
                new Vector2d(-2d, 0d),
                new Vector2d(-2d, -1d),
                new Vector2d(-1d, -1d),
            };

            var a = new Polygon2d(aPoints);

            var bPoints = new List<Vector2d>
            {
                new Vector2d(1d, 0d),
                new Vector2d(2d, 0d),
                new Vector2d(2d, 1d),
                new Vector2d(1d, 1d),
            };

            var b = new Polygon2d(bPoints);

            var c = Polygon2d.Combine(new List<Polygon2d>
            {
                a,
                b
            });
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual(5, c[0].Count);
            Assert.AreEqual(0, c[0].HoleCount);
            Assert.AreEqual(5, c[1].Count);
            Assert.AreEqual(0, c[1].HoleCount);

            // reverse case
            c = Polygon2d.Combine(new List<Polygon2d>
            {
                b,
                a
            });
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual(5, c[0].Count);
            Assert.AreEqual(0, c[0].HoleCount);
            Assert.AreEqual(5, c[1].Count);
            Assert.AreEqual(0, c[1].HoleCount);
        }

        [Test]
        public void TestCombineMethodDisjointTouchingFirstIndexCase()
        {
            var aPoints = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(-1d, 0d),
                new Vector2d(-1d, -1d),
                new Vector2d(0d, -1d),
            };

            var a = new Polygon2d(aPoints);

            var bPoints = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(0d, 1d),
            };

            var b = new Polygon2d(bPoints);

            var c = Polygon2d.Combine(new List<Polygon2d>
            {
                a,
                b
            });
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual(5, c[0].Count);
            Assert.AreEqual(0, c[0].HoleCount);
            Assert.AreEqual(5, c[1].Count);
            Assert.AreEqual(0, c[1].HoleCount);

            // reverse case
            c = Polygon2d.Combine(new List<Polygon2d>
            {
                b,
                a
            });
            Assert.AreEqual(2, c.Count);
            Assert.AreEqual(5, c[0].Count);
            Assert.AreEqual(0, c[0].HoleCount);
            Assert.AreEqual(5, c[1].Count);
            Assert.AreEqual(0, c[1].HoleCount);
        }

        [Test]
        public void TestCombineMethodDisjointWithOneHole()
        {
            var dPoints = new List<Vector2d>
            {
                new Vector2d(-1.25d, -0.25d),
                new Vector2d(-1.75d, -0.25d),
                new Vector2d(-1.75d, -0.75d),
                new Vector2d(-1.25d, -0.75d),
            };

            var d = new Polygon2d(dPoints);

            var aPoints = new List<Vector2d>
            {
                new Vector2d(-1d, 0d),
                new Vector2d(-2d, 0d),
                new Vector2d(-2d, -1d),
                new Vector2d(-1d, -1d),
            };

            var a = new Polygon2d(aPoints, new List<Polygon2d>
            {
                d
            });

            var bPoints = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(0d, 1d),
            };

            var b = new Polygon2d(bPoints);

            var e = Polygon2d.Combine(new List<Polygon2d>
            {
                a,
                b
            });
            Assert.AreEqual(2, e.Count);
            Assert.AreEqual(5, e[0].Count);
            Assert.AreEqual(1, e[0].HoleCount);
            Assert.AreEqual(5, e[0].Hole(0).Count);
            Assert.AreEqual(5, e[1].Count);
            Assert.AreEqual(0, e[1].HoleCount);

            // reverse case
            e = Polygon2d.Combine(new List<Polygon2d>
            {
                b,
                a
            });
            Assert.AreEqual(2, e.Count);
            Assert.AreEqual(5, e[0].Count);
            Assert.AreEqual(0, e[0].HoleCount);
            Assert.AreEqual(5, e[1].Count);
            Assert.AreEqual(1, e[1].HoleCount);
            Assert.AreEqual(5, e[1].Hole(0).Count);
        }

        [Test]
        public void TestCombineMethodDisjointWithTwoHoles()
        {
            var dPoints = new List<Vector2d>
            {
                new Vector2d(-1.25d, -0.25d),
                new Vector2d(-1.75d, -0.25d),
                new Vector2d(-1.75d, -0.75d),
                new Vector2d(-1.25d, -0.75d),
            };

            var d = new Polygon2d(dPoints);

            var aPoints = new List<Vector2d>
            {
                new Vector2d(-1d, 0d),
                new Vector2d(-2d, 0d),
                new Vector2d(-2d, -1d),
                new Vector2d(-1d, -1d),
            };

            var a = new Polygon2d(aPoints, new List<Polygon2d>
            {
                d
            });

            var cPoints = new List<Vector2d>
            {
                new Vector2d(0.25d, 0.25d),
                new Vector2d(0.75d, 0.25d),
                new Vector2d(0.75d, 0.75d),
                new Vector2d(0.25d, 0.75d),
            };

            var c = new Polygon2d(cPoints);

            var bPoints = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(0d, 1d),
            };

            var b = new Polygon2d(bPoints, new List<Polygon2d>
            {
                c
            });

            var e = Polygon2d.Combine(new List<Polygon2d>
            {
                a,
                b
            });
            Assert.AreEqual(2, e.Count);
            Assert.AreEqual(5, e[0].Count);
            Assert.AreEqual(1, e[0].HoleCount);
            Assert.AreEqual(5, e[0].Hole(0).Count);
            Assert.AreEqual(5, e[1].Count);
            Assert.AreEqual(1, e[1].HoleCount);
            Assert.AreEqual(5, e[1].Hole(0).Count);

            // reverse case
            e = Polygon2d.Combine(new List<Polygon2d>
            {
                b,
                a
            });
            Assert.AreEqual(2, e.Count);
            Assert.AreEqual(5, e[0].Count);
            Assert.AreEqual(1, e[0].HoleCount);
            Assert.AreEqual(5, e[0].Hole(0).Count);
            Assert.AreEqual(5, e[1].Count);
            Assert.AreEqual(1, e[1].HoleCount);
            Assert.AreEqual(5, e[1].Hole(0).Count);
        }

        [Test]
        public void TestCombineMethodWithTwoHoles()
        {
            var dPoints = new List<Vector2d>
            {
                new Vector2d(0.0d, -0.25d),
                new Vector2d(0.75d, -0.25d),
                new Vector2d(0.75d, -0.75d),
                new Vector2d(0.0d, -0.75d),
            };

            var d = new Polygon2d(dPoints);

            var aPoints = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(1d, -1d),
                new Vector2d(0d, -1d),
            };

            var a = new Polygon2d(aPoints, new List<Polygon2d>
            {
                d
            });

            var cPoints = new List<Vector2d>
            {
                new Vector2d(0.25d, 0.25d),
                new Vector2d(0.75d, 0.25d),
                new Vector2d(0.75d, 0.75d),
                new Vector2d(0.25d, 0.75d),
            };

            var c = new Polygon2d(cPoints);

            var bPoints = new List<Vector2d>
            {
                new Vector2d(0d, 0d),
                new Vector2d(1d, 0d),
                new Vector2d(1d, 1d),
                new Vector2d(0d, 1d),
            };

            var b = new Polygon2d(bPoints, new List<Polygon2d>
            {
                c
            });

            var e = Polygon2d.Combine(new List<Polygon2d>
            {
                a,
                b
            });
            Assert.AreEqual(1, e.Count);
            Assert.AreEqual(5, e[0].Count);
            Assert.AreEqual(2, e[0].HoleCount);

            // reverse case
            e = Polygon2d.Combine(new List<Polygon2d>
            {
                b,
                a
            });
            Assert.AreEqual(1, e.Count);
            Assert.AreEqual(5, e[0].Count);
            Assert.AreEqual(2, e[0].HoleCount);
        }

        [Test]
        public void TestIntersectionMethod()
        {
            // simple side intersection
            var p = new[]
            {
                new Vector2d(0, 0),
                new Vector2d(0, 10),
                new Vector2d(10, 10),
                new Vector2d(10, 0),
            };

            var a = new Polygon2d(p);

            p = new[]
            {
                new Vector2d(-2.5, 2.5),
                new Vector2d(-2.5, 7.5),
                new Vector2d(2.5, 7.5),
                new Vector2d(2.5, 2.5),
            };

            var b = new Polygon2d(p);

            Assert.IsTrue(a.Intersects(b));

            // simple total containment
            p = new[]
            {
                new Vector2d(0, 0),
                new Vector2d(0, 10),
                new Vector2d(10, 10),
                new Vector2d(10, 0),
            };

            a = new Polygon2d(p);

            p = new[]
            {
                new Vector2d(2.5, 2.5),
                new Vector2d(2.5, 7.5),
                new Vector2d(7.5, 7.5),
                new Vector2d(7.5, 2.5),
            };

            b = new Polygon2d(p);

            Assert.IsTrue(a.Intersects(b));

            // simple disjoint
            p = new[]
            {
                new Vector2d(0, 0),
                new Vector2d(0, 10),
                new Vector2d(10, 10),
                new Vector2d(10, 0),
            };

            a = new Polygon2d(p);

            p = new[]
            {
                new Vector2d(-2.5, -2.5),
                new Vector2d(-2.5, -7.5),
                new Vector2d(-7.5, -7.5),
                new Vector2d(-7.5, -2.5),
            };

            b = new Polygon2d(p);

            Assert.IsFalse(a.Intersects(b));
        }
    }
}