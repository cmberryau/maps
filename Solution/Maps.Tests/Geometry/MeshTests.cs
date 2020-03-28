using Maps.Geometry;
using NUnit.Framework;

namespace Maps.Tests.Geometry
{
    /// <summary>
    /// Series of tests for the Mesh class
    /// </summary>
    [TestFixture]
    internal sealed class MeshTests
    {
        private static readonly Vector3d[] Vertices = {
            new Vector3d(0),
            new Vector3d(1),
            new Vector3d(2),
            new Vector3d(3),
        };

        private static readonly Vector3d[] Normals = {
            new Vector3d(0),
            new Vector3d(1),
            new Vector3d(2),
            new Vector3d(3),
        };

        private static readonly Vector4d[][] UVs = {
            new[]
            {
                new Vector4d(0),
                new Vector4d(1),
                new Vector4d(2),
                new Vector4d(3),
            },
        };

        private static readonly int[] TriangleIndices = {
            0, 1, 2
        };

        private static readonly int[] QuadIndices = {
            0, 1, 2, 3
        };

        private static readonly int[] LinesIndices = {
            0, 1
        };

        private static readonly int[] LineStripIndices = {
            0, 1, 2
        };

        private static readonly int[] PointIndices = {
            0
        };

        /// <summary>
        /// Tests the constructor without any additional parameters
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var mesh = new Mesh(Topology.Points);
            Assert.AreEqual(Topology.Points, mesh.Topology);

            mesh = new Mesh(Topology.Triangles);
            Assert.AreEqual(Topology.Triangles, mesh.Topology);

            mesh = new Mesh(Topology.Quads);
            Assert.AreEqual(Topology.Quads, mesh.Topology);

            mesh = new Mesh(Topology.Lines);
            Assert.AreEqual(Topology.Lines, mesh.Topology);

            mesh = new Mesh(Topology.LineStrip);
            Assert.AreEqual(Topology.LineStrip, mesh.Topology);
        }

        /// <summary>
        /// Tests the method for appending meshes
        /// </summary>
        [Test]
        public void TestAppendMethod()
        {
            var meshA = new Mesh(Topology.Points);
            meshA.SetVertices(Vertices);
            meshA.SetIndices(PointIndices);

            Assert.AreEqual(Topology.Points, meshA.Topology);
            Assert.AreEqual(Vertices.Length, meshA.Vertices.Length);
            Assert.AreEqual(PointIndices.Length, meshA.Indices.Length);

            var meshB = new Mesh(Topology.Points);
            meshB.SetVertices(Vertices);
            meshB.SetIndices(PointIndices);

            Assert.AreEqual(Topology.Points, meshB.Topology);
            Assert.AreEqual(Vertices.Length, meshB.Vertices.Length);
            Assert.AreEqual(PointIndices.Length, meshB.Indices.Length);

            meshA.Append(meshB);

            Assert.AreEqual(Topology.Points, meshA.Topology);
            Assert.AreEqual(Vertices.Length * 2, meshA.Vertices.Length);
            Assert.AreEqual(PointIndices.Length * 2, meshA.Indices.Length);
        }

        /// <summary>
        /// Tests the method for setting mesh vertices
        /// </summary>
        [Test]
        public void TestSetVerticesMethod()
        {
            var mesh = new Mesh(Topology.Points);
            mesh.SetVertices(Vertices);

            Assert.AreEqual(Topology.Points, mesh.Topology);
            Assert.AreEqual(Vertices.Length, mesh.Vertices.Length);

            mesh = new Mesh(Topology.Triangles);
            mesh.SetVertices(Vertices);

            Assert.AreEqual(Topology.Triangles, mesh.Topology);
            Assert.AreEqual(Vertices.Length, mesh.Vertices.Length);

            mesh = new Mesh(Topology.Quads);
            mesh.SetVertices(Vertices);

            Assert.AreEqual(Topology.Quads, mesh.Topology);
            Assert.AreEqual(Vertices.Length, mesh.Vertices.Length);

            mesh = new Mesh(Topology.Lines);
            mesh.SetVertices(Vertices);

            Assert.AreEqual(Topology.Lines, mesh.Topology);
            Assert.AreEqual(Vertices.Length, mesh.Vertices.Length);

            mesh = new Mesh(Topology.LineStrip);
            mesh.SetVertices(Vertices);

            Assert.AreEqual(Topology.LineStrip, mesh.Topology);
            Assert.AreEqual(Vertices.Length, mesh.Vertices.Length);
        }

        /// <summary>
        /// Tests the method for setting mesh indices
        /// </summary>
        [Test]
        public void TestSetIndicesMethod()
        {
            var mesh = new Mesh(Topology.Points);
            mesh.SetIndices(PointIndices);

            Assert.AreEqual(Topology.Points, mesh.Topology);
            Assert.AreEqual(PointIndices.Length, mesh.Indices.Length);

            mesh = new Mesh(Topology.Triangles);
            mesh.SetIndices(TriangleIndices);

            Assert.AreEqual(Topology.Triangles, mesh.Topology);
            Assert.AreEqual(TriangleIndices.Length, mesh.Indices.Length);

            mesh = new Mesh(Topology.Quads);
            mesh.SetIndices(QuadIndices);

            Assert.AreEqual(Topology.Quads, mesh.Topology);
            Assert.AreEqual(QuadIndices.Length, mesh.Indices.Length);

            mesh = new Mesh(Topology.Lines);
            mesh.SetIndices(LinesIndices);

            Assert.AreEqual(Topology.Lines, mesh.Topology);
            Assert.AreEqual(LinesIndices.Length, mesh.Indices.Length);

            mesh = new Mesh(Topology.LineStrip);
            mesh.SetIndices(LineStripIndices);

            Assert.AreEqual(Topology.LineStrip, mesh.Topology);
            Assert.AreEqual(LineStripIndices.Length, mesh.Indices.Length);
        }

        /// <summary>
        /// Tests the method for setting mesh Normals
        /// </summary>
        [Test]
        public void TestSetNormalsMethod()
        {
            var mesh = new Mesh(Topology.Points);
            mesh.SetNormals(Normals);

            Assert.AreEqual(Topology.Points, mesh.Topology);
            Assert.AreEqual(Normals.Length, mesh.Normals.Length);

            mesh = new Mesh(Topology.Triangles);
            mesh.SetNormals(Normals);

            Assert.AreEqual(Topology.Triangles, mesh.Topology);
            Assert.AreEqual(Normals.Length, mesh.Normals.Length);

            mesh = new Mesh(Topology.Quads);
            mesh.SetNormals(Normals);

            Assert.AreEqual(Topology.Quads, mesh.Topology);
            Assert.AreEqual(Normals.Length, mesh.Normals.Length);

            mesh = new Mesh(Topology.Lines);
            mesh.SetNormals(Normals);

            Assert.AreEqual(Topology.Lines, mesh.Topology);
            Assert.AreEqual(Normals.Length, mesh.Normals.Length);

            mesh = new Mesh(Topology.LineStrip);
            mesh.SetNormals(Normals);

            Assert.AreEqual(Topology.LineStrip, mesh.Topology);
            Assert.AreEqual(Normals.Length, mesh.Normals.Length);
        }

        /// <summary>
        /// Tests the method for setting mesh UVs
        /// </summary>
        [Test]
        public void TestSetUVsMethod()
        {
            var mesh = new Mesh(Topology.Points);
            mesh.SetUVs(UVs);

            Assert.AreEqual(Topology.Points, mesh.Topology);
            Assert.AreEqual(UVs.Length, mesh.UVs.Length);

            mesh = new Mesh(Topology.Triangles);
            mesh.SetUVs(UVs);

            Assert.AreEqual(Topology.Triangles, mesh.Topology);
            Assert.AreEqual(UVs.Length, mesh.UVs.Length);

            mesh = new Mesh(Topology.Quads);
            mesh.SetUVs(UVs);

            Assert.AreEqual(Topology.Quads, mesh.Topology);
            Assert.AreEqual(UVs.Length, mesh.UVs.Length);

            mesh = new Mesh(Topology.Lines);
            mesh.SetUVs(UVs);

            Assert.AreEqual(Topology.Lines, mesh.Topology);
            Assert.AreEqual(UVs.Length, mesh.UVs.Length);

            mesh = new Mesh(Topology.LineStrip);
            mesh.SetUVs(UVs);

            Assert.AreEqual(Topology.LineStrip, mesh.Topology);
            Assert.AreEqual(UVs.Length, mesh.UVs.Length);
        }
    }
}