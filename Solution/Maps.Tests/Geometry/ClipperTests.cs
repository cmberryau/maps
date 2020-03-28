using System.Collections.Generic;
using ClipperLib;
using Maps.Geographical;
using Maps.Geographical.Projection;
using Maps.Geographical.Tiles;
using Maps.Geometry;
using NUnit.Framework;
using TriangleNet.Geometry;
using TriangleNet.Meshing;

namespace Maps.Tests.Geometry
{
    /// <summary>
    /// Series of integration tests for ClipperLib
    /// </summary>
    [TestFixture]
    internal sealed class ClipperTests
    {
        /// <summary>
        /// Tests that points are maintained
        /// </summary>
        [Test]
        public void TestPointsMaintained()
        {
            var scaleFactor = 1e14;
            var extents = 180;
            var inverseScaleFactor = 1 / scaleFactor;
            var translation = Vector2d.Zero;

            var subjectPointsCW = new[]
            {
                new Vector2d(-extents, -extents) + translation,
                new Vector2d(-extents, extents) + translation,
                new Vector2d(extents, extents) + translation,
                new Vector2d(extents, -extents) + translation,
            };

            var subjectPointsCCW = new[]
            {
                new Vector2d(extents, extents) + translation,
                new Vector2d(extents, -extents) + translation,
                new Vector2d(-extents, -extents) + translation,
                new Vector2d(-extents, extents) + translation,
            };

            var subject = new List<IntPoint>();
            foreach (var point in subjectPointsCW)
            {
                var x = (long)(point.x * scaleFactor);
                var y = (long)(point.y * scaleFactor);

                subject.Add(new IntPoint(x, y));
            }

            var clipPoints = new[]
            {
                new Vector2d(-1, -1),
                new Vector2d(-1, 1),
                new Vector2d(1, 1),
                new Vector2d(1, -1),
            };
            var clip = new List<IntPoint>();
            foreach (var point in clipPoints)
            {
                var x = (long)(point.x * scaleFactor);
                var y = (long)(point.y * scaleFactor);

                clip.Add(new IntPoint(x, y));
            }

            var clipper = new Clipper();

            clipper.AddPath(subject, PolyType.ptSubject, true);
            clipper.AddPath(clip, PolyType.ptClip, true);

            var solution = new List<List<IntPoint>>();
            clipper.Execute(ClipType.ctIntersection, solution);

            Assert.IsNotNull(solution);
            Assert.IsNotEmpty(solution);

            var solutionPoints = new List<Vector2d>();

            foreach (var path in solution)
            {
                foreach (var point in path)
                {
                    var x = point.X * inverseScaleFactor;
                    var y = point.Y * inverseScaleFactor;

                    var doublePoint = new Vector2d(x, y);
                    solutionPoints.Add(doublePoint);
                }
            }

            Assert.IsNotNull(solutionPoints);
            Assert.IsNotEmpty(solutionPoints);
            Assert.IsTrue(solutionPoints.Count == 4);
        }

        /// <summary>
        /// Tests ClipperLib against some real-world data
        /// 
        /// See: http://www.openstreetmap.org/way/32552534
        /// </summary>
        [Test]
        public static void RealWorldDataTest()
        {
            var scaleFactor = 1e14;
            var inverseScaleFactor = 1 / scaleFactor;

            var subjectPoints = new[]
            {
                new Vector2d(11.408787d, 48.7686987d),
                new Vector2d(11.4089052d, 48.7689006d),
                new Vector2d(11.4094868d, 48.7688431d),
                new Vector2d(11.4094999d, 48.7687067d),
                new Vector2d(11.4105879d, 48.7683295d),
                new Vector2d(11.4106355d, 48.768677d),
                new Vector2d(11.4111265d, 48.7686495d),
                new Vector2d(11.4111016d, 48.7682779d),
                new Vector2d(11.4116518d, 48.7680558d),
                new Vector2d(11.4122734d, 48.7680723d),
                new Vector2d(11.4123695d, 48.7679218d),
                new Vector2d(11.4120174d, 48.7677008d),
                new Vector2d(11.4118154d, 48.7676645d),
                new Vector2d(11.4116006d, 48.7676815d),
                new Vector2d(11.4111066d, 48.7678797d),
                new Vector2d(11.4100399d, 48.7682969d),
                new Vector2d(11.4092696d, 48.7685698d),
            };

            var subject = new List<IntPoint>();
            foreach (var point in subjectPoints)
            {
                var x = (long)(point.x * scaleFactor);
                var y = (long)(point.y * scaleFactor);

                subject.Add(new IntPoint(x, y));
            }

            var tileSource = new TmsTileSource();
            var tile = tileSource.GetForZoom(new Geodetic2d(48.7688622013463d, 
                11.4065551757813d), 16);

            var clipPoints = new[]
            {
                new Vector2d(tile.Box[0].Longitude, tile.Box[0].Latitude),
                new Vector2d(tile.Box[1].Longitude, tile.Box[1].Latitude),
                new Vector2d(tile.Box[2].Longitude, tile.Box[2].Latitude),
                new Vector2d(tile.Box[3].Longitude, tile.Box[3].Latitude),
            };

            var clip = new List<IntPoint>();
            foreach (var point in clipPoints)
            {
                var x = (long)(point.x * scaleFactor);
                var y = (long)(point.y * scaleFactor);

                clip.Add(new IntPoint(x, y));
            }

            var clipper = new Clipper();
            clipper.AddPath(subject, PolyType.ptSubject, true);
            clipper.AddPath(clip, PolyType.ptClip, true);

            var solution = new List<List<IntPoint>>();
            clipper.Execute(ClipType.ctIntersection, solution);

            Assert.IsNotNull(solution);
            Assert.IsNotEmpty(solution);

            Geodetic2d[] solutionPoints = null;
            var i = -1;

            foreach (var path in solution)
            {
                solutionPoints = new Geodetic2d[path.Count];

                foreach (var point in path)
                {
                    var x = point.X * inverseScaleFactor;
                    var y = point.Y * inverseScaleFactor;

                    var doublePoint = new Geodetic2d(y, x);
                    solutionPoints[++i] = doublePoint;
                }
            }

            var projection = new WebMercatorProjection(10d);
            var projectedPoints = projection.Forward(solutionPoints);

            var verts = new List<Vertex>();
            verts.Add(new Vertex(projectedPoints[0].x, projectedPoints[0].y, 1));
            var poly = new Polygon();

            for (i = 1; i < projectedPoints.Count; ++i)
            {
                verts.Add(new Vertex(projectedPoints[i].x, projectedPoints[i].y, 1));
                var segment = new Segment(verts[i - 1], verts[i], 0);
                poly.Add(segment, 0);
            }

            poly.Add(new Segment(verts[verts.Count - 1], verts[0]), 0);

            var constraintOptions = new ConstraintOptions
            {
                ConformingDelaunay = false,
            };

            var qualityOptions = new QualityOptions()
            {

            };

            var mesher = new GenericMesher();
            Assert.DoesNotThrow(() => mesher.Triangulate(poly, constraintOptions, 
                qualityOptions));
        }

        private static Box2d CreateDefaultBox(Vector2d translation)
        {
            var offset = Vector2d.One * 0.5;
            var a = Vector2d.Zero + translation - offset;
            var b = Vector2d.One + translation - offset;
            return new Box2d(a, b);
        }
    }
}
