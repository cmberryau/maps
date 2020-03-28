using System.Collections.Generic;
using NUnit.Framework;
using TriangleNet.Geometry;
using TriangleNet.Meshing;

namespace Maps.Tests.Geometry
{
    /// <summary>
    /// Series of integration tests for Triangle.NET
    /// </summary>
    [TestFixture]
    internal sealed class TriangleTests
    {
        /// <summary>
        /// Tests triangulation via Triangle.NET
        /// </summary>
        [Test]
        public void TestPointsTriangulation()
        {
            var points = new[]
            {
                new Vector2d(-0.25, 0.25),
                new Vector2d(0.25, 0.25),
                new Vector2d(0.25, -0.25),
                new Vector2d(0.0, 0.0),
                new Vector2d(-0.25, -0.25),
                new Vector2d(-0.25, 0.25),
            };

            var pointsMap = new HashSet<Vector2d>();

            foreach (var point in points)
            {
                pointsMap.Add(point);
            }

            var verts = new Vertex[points.Length];
            verts[0] = new Vertex(points[0].x, points[1].y, 1);
            var poly = new Polygon();

            for (var i = 1; i < verts.Length; ++i)
            {
                verts[i] = new Vertex(points[i].x, points[i].y, 1);
                var segment = new Segment(verts[i - 1], verts[i], 1);
                poly.Add(segment, 0);
            }

            var constraintOptions = new ConstraintOptions
            {
                ConformingDelaunay = false,
                SegmentSplitting = 2,
            };

            var qualityOptions = new QualityOptions()
            {

            };

            var mesher = new GenericMesher();
            var mesh = mesher.Triangulate(poly, constraintOptions);
            var matched = 0;

            foreach (var vertex in mesh.Vertices)
            {
                var point = new Vector2d(vertex.X, vertex.Y);

                if (pointsMap.Contains(point))
                {
                    ++matched;
                }
            }

            Assert.AreEqual(pointsMap.Count, matched);
        }
    }
}
