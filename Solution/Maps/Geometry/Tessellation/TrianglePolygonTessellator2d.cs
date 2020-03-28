using System;
using System.Collections.Generic;
using TriangleNet;
using TriangleNet.Geometry;
using TriangleNet.Meshing;

namespace Maps.Geometry.Tessellation
{
    /// <summary>
    /// Tessellates input polygons into triangle meshes
    /// </summary>
    internal class TrianglePolygonTessellator2d : IPolygonTessellator
    {
        private readonly ConstraintOptions _constraints;
        private readonly QualityOptions _quality;
        private readonly int[] _triIndices;
        private readonly double _height;

        /// <summary>
        /// Initializes a new instance of TriangleTessellator2d
        /// </summary>
        /// <param name="clockwise">Generate clockwise meshes?</param>
        /// <param name="height">The optional height parameter for meshes</param>
        public TrianglePolygonTessellator2d(bool clockwise, double height = 0d)
        {
            _constraints = new ConstraintOptions
            {
                ConformingDelaunay = false,
                SegmentSplitting = 0,
            };

            _quality = new QualityOptions
            {
                //MinimumAngle = 20,
            };

            _triIndices = new int[3];

            if (clockwise)
            {
                _triIndices[0] = 0;
                _triIndices[1] = 2;
                _triIndices[2] = 1;
            }
            else
            {
                _triIndices[0] = 0;
                _triIndices[1] = 1;
                _triIndices[2] = 2;
            }

            _height = height;
        }

        /// <inheritdoc />
        public Mesh Tessellate(IList<Vector3d> points)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            if (points.Count < 3)
            {
                throw new ArgumentException("Must have at least three points",
                    nameof(points));
            }

            return DelaunayTessellate(points);
        }

        /// <inheritdoc />
        public Mesh Tessellate(IList<IList<Vector3d>> points)
        {
            if (points.Count > 1)
            {
                var holes = new List<IList<Vector3d>>();
                for (var i = 0; i < points.Count - 1; ++i)
                {
                    holes.Add(points[i + 1]);
                }

                return DelaunayTessellate(points[0], holes);
            }

            return DelaunayTessellate(points[0]);
        }

        /// <inheritdoc />
        public Mesh Tessellate(IList<Vector3d> points, IList<IList<Vector3d>> holes)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            if (points.Count < 3)
            {
                throw new ArgumentException("Must have at least three points", nameof(points));
            }

            if (holes == null)
            {
                throw new ArgumentNullException(nameof(holes));
            }

            return DelaunayTessellate(points, holes);
        }

        private Mesh DelaunayTessellate(IList<Vector3d> points)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            if (points.Count < 3)
            {
                throw new ArgumentException("Must have at least three points",
                    nameof(points));
            }

            var pointsCount = points.Count;
            var verts = new Vertex[pointsCount];
            verts[0] = new Vertex(points[0].x, points[0].y, 1);
            var poly = new Polygon();

            // create polygon segments
            for (var i = 1; i < pointsCount; ++i)
            {
                verts[i] = new Vertex(points[i].x, points[i].y, 1);
                poly.Add(new Segment(verts[i - 1], verts[i], 0), 0);
            }

            // if the points aren't closed, close with a final segment
            if (points[0] != points[pointsCount - 1])
            {
                poly.Add(new Segment(verts[pointsCount - 1], verts[0]), 0);
            }

            // for multithreading, we need to create per-thread instances
            var pool = new TrianglePool();
            var predicates = new RobustPredicates();
            var config = new Configuration
            {
                Predicates = () => predicates,
                TrianglePool = () => pool.Restart()
            };

            var mesher = new GenericMesher(config);
            var inputMesh = mesher.Triangulate(poly, _constraints, _quality);

            var outputVerts = new Vector3d[inputMesh.Vertices.Count];
            var outputNorms = new Vector3d[inputMesh.Vertices.Count];

            var j = -1;
            foreach (var vertex in inputMesh.Vertices)
            {
                outputVerts[++j] = new Vector3d(vertex.X, vertex.Y, -_height);
                outputNorms[j] = Vector3d.Back;
            }

            var outputIndices = new int[inputMesh.Triangles.Count * 3];

            var k = -1;
            foreach (var triangle in inputMesh.Triangles)
            {
                outputIndices[++k] = triangle.GetVertexID(_triIndices[0]);
                outputIndices[++k] = triangle.GetVertexID(_triIndices[1]);
                outputIndices[++k] = triangle.GetVertexID(_triIndices[2]);
            }

            pool.Clear();

            if (_height > 0d)
            {
                var sidesVerticesCount = pointsCount * 4;
                var sidesIndicesCount = (pointsCount - 1) * 6;

                Array.Resize(ref outputVerts, outputVerts.Length + sidesVerticesCount);
                Array.Resize(ref outputNorms, outputNorms.Length + sidesVerticesCount);
                Array.Resize(ref outputIndices, outputIndices.Length + sidesIndicesCount);

                var cw = Vector2d.Clockwise(points);

                Vector3d norm;
                var point = points[0];
                outputVerts[++j] = new Vector3d(point.x, point.y, 0d);
                outputVerts[++j] = new Vector3d(point.x, point.y, -_height);

                for (var i = 1; i < pointsCount - 1; ++i)
                {
                    point = points[i];

                    outputVerts[++j] = new Vector3d(point.x, point.y, 0d);
                    outputVerts[++j] = new Vector3d(point.x, point.y, -_height);

                    norm = Vector3d.Cross(outputVerts[j] - outputVerts[j - 2],
                        outputVerts[j - 1] - outputVerts[j - 2]);

                    outputNorms[j] = norm;
                    outputNorms[j - 1] = norm;
                    outputNorms[j - 2] = norm;
                    outputNorms[j - 3] = norm;

                    if (cw)
                    {
                        outputIndices[++k] = j;
                        outputIndices[++k] = j - 2;
                        outputIndices[++k] = j - 1;

                        outputIndices[++k] = j - 1;
                        outputIndices[++k] = j - 2;
                        outputIndices[++k] = j - 3;
                    }
                    else
                    {
                        outputIndices[++k] = j;
                        outputIndices[++k] = j - 1;
                        outputIndices[++k] = j - 2;

                        outputIndices[++k] = j - 1;
                        outputIndices[++k] = j - 3;
                        outputIndices[++k] = j - 2;
                    }

                    outputVerts[++j] = new Vector3d(point.x, point.y, 0d);
                    outputVerts[++j] = new Vector3d(point.x, point.y, -_height);
                }

                point = points[pointsCount - 1];

                outputVerts[++j] = new Vector3d(point.x, point.y, 0d);
                outputVerts[++j] = new Vector3d(point.x, point.y, -_height);

                norm = Vector3d.Cross(outputVerts[j] - outputVerts[j - 2],
                    outputVerts[j - 1] - outputVerts[j - 2]);

                outputNorms[j] = norm;
                outputNorms[j - 1] = norm;
                outputNorms[j - 2] = norm;
                outputNorms[j - 3] = norm;

                if (cw)
                {
                    outputIndices[++k] = j;
                    outputIndices[++k] = j - 2;
                    outputIndices[++k] = j - 1;

                    outputIndices[++k] = j - 1;
                    outputIndices[++k] = j - 2;
                    outputIndices[++k] = j - 3;
                }
                else
                {
                    outputIndices[++k] = j;
                    outputIndices[++k] = j - 1;
                    outputIndices[++k] = j - 2;

                    outputIndices[++k] = j - 1;
                    outputIndices[++k] = j - 3;
                    outputIndices[++k] = j - 2;
                }
            }

            var outputMesh = new Mesh(Topology.Triangles);

            outputMesh.SetVertices(outputVerts);
            outputMesh.SetNormals(outputNorms);
            outputMesh.SetIndices(outputIndices);

            return outputMesh;
        }

        private Mesh DelaunayTessellate(IList<Vector3d> points, 
            IList<IList<Vector3d>> holes)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            if (points.Count < 3)
            {
                throw new ArgumentException("Must have at least three points",
                    nameof(points));
            }

            var pointsCount = points.Count;
            var verts = new Vertex[pointsCount];
            verts[0] = new Vertex(points[0].x, points[0].y, 1);
            var poly = new Polygon();

            // create polygon segments
            for (var i = 1; i < pointsCount; ++i)
            {
                verts[i] = new Vertex(points[i].x, points[i].y, 1);
                poly.Add(new Segment(verts[i - 1], verts[i], 0), 0);
            }

            // if the points aren't closed, close with a final segment
            if (points[0] != points[pointsCount - 1])
            {
                poly.Add(new Segment(verts[pointsCount - 1], verts[0]), 0);
            }

            // for multithreading, we need to create per-thread instances
            var pool = new TrianglePool();
            var predicates = new RobustPredicates();
            var config = new Configuration
            {
                Predicates = () => predicates,
                TrianglePool = () => pool.Restart()
            };

            var mesher = new GenericMesher(config);

            AddHoles(poly, holes, mesher);

            var inputMesh = mesher.Triangulate(poly, _constraints, _quality);
            var outputVerts = new Vector3d[inputMesh.Vertices.Count];
            var outputNorms = new Vector3d[inputMesh.Vertices.Count];

            var j = -1;
            foreach (var vertex in inputMesh.Vertices)
            {
                outputVerts[++j] = new Vector3d(vertex.X, vertex.Y, -_height);
                outputNorms[j] = Vector3d.Back;
            }

            var outputIndices = new int[inputMesh.Triangles.Count * 3];

            var k = -1;
            foreach (var triangle in inputMesh.Triangles)
            {
                outputIndices[++k] = triangle.GetVertexID(_triIndices[0]);
                outputIndices[++k] = triangle.GetVertexID(_triIndices[1]);
                outputIndices[++k] = triangle.GetVertexID(_triIndices[2]);
            }

            pool.Clear();

            if (_height > 0d)
            {
                var sidesVerticesCount = (pointsCount - 1) * 4;
                var sidesIndicesCount = (pointsCount - 1) * 6;

                for (var i = 0; i < holes.Count; ++i)
                {
                    sidesVerticesCount += (holes[i].Count - 1) * 4;
                    sidesIndicesCount += (holes[i].Count - 1) * 6;
                }

                Array.Resize(ref outputVerts, outputVerts.Length + sidesVerticesCount);
                Array.Resize(ref outputNorms, outputNorms.Length + sidesVerticesCount);
                Array.Resize(ref outputIndices, outputIndices.Length + sidesIndicesCount);

                var selectedPoints = points;
                for (var i = 0; i < holes.Count + 1; ++i)
                {
                    Vector3d norm;
                    var cw = Vector2d.Clockwise(selectedPoints);

                    if (i > 0)
                    {
                        cw = !cw;
                    }

                    var point = selectedPoints[0];

                    outputVerts[++j] = new Vector3d(point.x, point.y, 0d);
                    outputVerts[++j] = new Vector3d(point.x, point.y, -_height);

                    pointsCount = selectedPoints.Count;
                    for (var l = 1; l < pointsCount - 1; ++l)
                    {
                        point = selectedPoints[l];

                        outputVerts[++j] = new Vector3d(point.x, point.y, 0d);
                        outputVerts[++j] = new Vector3d(point.x, point.y, -_height);

                        norm = Vector3d.Cross(outputVerts[j] - outputVerts[j - 2],
                            outputVerts[j - 1] - outputVerts[j - 2]);

                        outputNorms[j] = norm;
                        outputNorms[j - 1] = norm;
                        outputNorms[j - 2] = norm;
                        outputNorms[j - 3] = norm;

                        if (cw)
                        {
                            outputIndices[++k] = j;
                            outputIndices[++k] = j - 2;
                            outputIndices[++k] = j - 1;

                            outputIndices[++k] = j - 1;
                            outputIndices[++k] = j - 2;
                            outputIndices[++k] = j - 3;
                        }
                        else
                        {
                            outputIndices[++k] = j;
                            outputIndices[++k] = j - 1;
                            outputIndices[++k] = j - 2;

                            outputIndices[++k] = j - 1;
                            outputIndices[++k] = j - 3;
                            outputIndices[++k] = j - 2;
                        }

                        outputVerts[++j] = new Vector3d(point.x, point.y, 0d);
                        outputVerts[++j] = new Vector3d(point.x, point.y, -_height);
                    }

                    point = selectedPoints[pointsCount - 1];

                    outputVerts[++j] = new Vector3d(point.x, point.y, 0d);
                    outputVerts[++j] = new Vector3d(point.x, point.y, -_height);

                    norm = Vector3d.Cross(outputVerts[j] - outputVerts[j - 2],
                        outputVerts[j - 1] - outputVerts[j - 2]);

                    outputNorms[j] = norm;
                    outputNorms[j - 1] = norm;
                    outputNorms[j - 2] = norm;
                    outputNorms[j - 3] = norm;

                    if (cw)
                    {
                        outputIndices[++k] = j;
                        outputIndices[++k] = j - 2;
                        outputIndices[++k] = j - 1;

                        outputIndices[++k] = j - 1;
                        outputIndices[++k] = j - 2;
                        outputIndices[++k] = j - 3;
                    }
                    else
                    {
                        outputIndices[++k] = j;
                        outputIndices[++k] = j - 1;
                        outputIndices[++k] = j - 2;

                        outputIndices[++k] = j - 1;
                        outputIndices[++k] = j - 3;
                        outputIndices[++k] = j - 2;
                    }

                    if (i < holes.Count)
                    {
                        selectedPoints = holes[i];
                    }
                }
            }

            var outputMesh = new Mesh(Topology.Triangles);

            outputMesh.SetVertices(outputVerts);
            outputMesh.SetNormals(outputNorms);
            outputMesh.SetIndices(outputIndices);

            return outputMesh;
        }

        private void AddHoles(IPolygon poly, IList<IList<Vector3d>> holes, GenericMesher mesher)
        {
            var holesCount = holes.Count;

            for (var i = 0; i < holesCount; ++i)
            {
                var pointsCount = holes[i].Count;
                var verts = new Vertex[pointsCount];
                verts[0] = new Vertex(holes[i][0].x, holes[i][0].y, 2);

                var holePoly = new Polygon();

                // create the hole polygon segments
                for (var j = 1; j < pointsCount; ++j)
                {
                    verts[j] = new Vertex(holes[i][j].x, holes[i][j].y, 2);

                    var segment = new Segment(verts[j - 1], verts[j], 2);
                    poly.Add(segment, 0);
                    holePoly.Add(segment, 0);
                }

                // if the hole points aren't closed, close with a final segment
                if (holes[i][0] != holes[i][pointsCount - 1])
                {
                    var segment = new Segment(verts[pointsCount - 1], verts[0], 2);
                    poly.Add(segment, 0);
                    holePoly.Add(segment, 0);
                }

                // we have to create a mesh to get a point inside the hole
                var holeMesh = mesher.Triangulate(holePoly, _constraints,
                    _quality);

                var outputTriangles = holeMesh.Triangles;

                foreach (var tri in outputTriangles)
                {
                    var v0 = tri.GetVertex(0);
                    var v1 = tri.GetVertex(1);
                    var v2 = tri.GetVertex(2);

                    poly.Holes.Add(new Point((v0.X + v1.X + v2.X) / 3,
                        (v0.Y + v1.Y + v2.Y) / 3));

                    break;
                }
            }
        }
    }
}