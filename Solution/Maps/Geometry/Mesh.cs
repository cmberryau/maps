using System;
using System.Collections.Generic;
using System.Linq;

namespace Maps.Geometry
{
    /// <summary>
    /// 3 dimensional Mesh with double precision elements
    /// </summary>
    public sealed class Mesh
    {
        /// <summary>
        /// The topology of the mesh
        /// </summary>
        public readonly Topology Topology;

        /// <summary>
        /// The empty mesh
        /// </summary>
        public static readonly Mesh Empty = new Mesh();

        /// <summary>
        /// The bounds of the mesh
        /// </summary>
        public Bounds3d Bounds
        {
            get;
            private set;
        }

        /// <summary>
        /// The vertices of the mesh
        /// </summary>
        public Vector3d[] Vertices
        {
            get => _vertices;
            private set => _vertices = value;
        }

        /// <summary>
        /// The number of vertices in the mesh
        /// </summary>
        public int VertexCount => _vertices.Length;

        /// <summary>
        /// The indicies of the mesh
        /// </summary>
        public int[] Indices
        {
            get => _indices;
            private set => _indices = value;
        }

        /// <summary>
        /// The number of indices in the mesh
        /// </summary>
        public int IndexCount => _indices.Length;

        /// <summary>
        /// The normals of the mesh
        /// </summary>
        public Vector3d[] Normals
        {
            get => _normals;
            private set => _normals = value;
        }

        /// <summary>
        /// The number of normals in the mesh
        /// </summary>
        public int NormalsCount => _normals.Length;

        /// <summary>
        /// The UVs of the mesh
        /// </summary>
        public Vector4d[][] UVs
        {
            get => _uvs;
            private set => _uvs = value;
        }

        private static readonly Vector3d[] EmptyVectors = new Vector3d[0];
        private static readonly int[] EmptyInts = new int[0];
        private static readonly Vector4d[][] EmptyUVs = new Vector4d[0][];

        private Vector3d[] _vertices;
        private int[] _indices;
        private Vector3d[] _normals;
        private Vector4d[][] _uvs;

        /// <summary>
        /// Creates a new mesh
        /// </summary>
        /// <param name="topology">The topology of the new mesh</param>
        public Mesh(Topology topology)
        {
            Topology = topology;

            _vertices = EmptyVectors;
            _indices = EmptyInts;
            _normals = EmptyVectors;
            _uvs = EmptyUVs;
        }

        private Mesh()
        {
            Topology = Topology.Points;

            _vertices = EmptyVectors;
            _indices = EmptyInts;
            _normals = EmptyVectors;
            _uvs = EmptyUVs;
        }

        /// <summary>
        /// Appends the subject mesh
        /// </summary>
        /// <param name="mesh">The subject mesh to append</param>
        public void Append(Mesh mesh)
        {
            if (mesh == null)
            {
                throw new ArgumentNullException(nameof(mesh));
            }

            if (mesh.Topology != Topology)
            {
                throw new ArgumentException("Both meshes have the same " +
                    "topology", nameof(mesh));
            }

            // append uv channels first, highest chance of failure
            if (_uvs.Length > 0)
            {
                if (mesh._uvs.Length <= 0)
                {
                    throw new ArgumentException("Does not contain uvs", 
                        nameof(mesh));
                }

                if (_uvs.Length != mesh._uvs.Length)
                {
                    throw new ArgumentException("Mismatching UV channel " +
                        "counts", nameof(mesh));
                }
                
                // append each individual uv channel
                for (var i = 0; i < _uvs.Length; i++)
                {
                    if (mesh._uvs[i].Length <= 0)
                    {
                        throw new ArgumentException("Contains empty UV " +
                            $"channel at index {i}", nameof(mesh));
                    }

                    var originalUVsLength = _uvs[i].Length;
                    Array.Resize(ref _uvs[i], _uvs[i].Length + 
                        mesh._uvs[i].Length);
                    Array.Copy(mesh._uvs[i], 0, _uvs[i], originalUVsLength, 
                        mesh._uvs[i].Length);
                }
            }

            // normals next
            if (_normals.Length > 0)
            {
                if (mesh._normals.Length <= 0)
                {
                    throw new ArgumentException("Does not contain normals", 
                        nameof(mesh));
                }

                var originalNormalsLength = _normals.Length;
                Array.Resize(ref _normals, _normals.Length + 
                    mesh._normals.Length);
                Array.Copy(mesh._normals, 0, _normals, originalNormalsLength, 
                    mesh._normals.Length);
            }

            // vertices
            var originalVerticesLength = _vertices.Length;
            Array.Resize(ref _vertices, _vertices.Length + 
                mesh._vertices.Length);
            Array.Copy(mesh._vertices, 0, _vertices, originalVerticesLength, 
                mesh._vertices.Length);

            // indices
            var originalIndicesLength = _indices.Length;
            Array.Resize(ref _indices, _indices.Length + mesh._indices.Length);
            for (var i = 0; i < mesh._indices.Length; i++)
            {
                _indices[i + originalIndicesLength] = mesh._indices[i] + 
                    originalVerticesLength;
            }

            // finally, the bounds
            Bounds += mesh.Bounds;
        }

        /// <summary>
        /// Sets the vertices of the mesh
        /// </summary>
        /// <param name="vertices">The vertices to set</param>
        public void SetVertices(Vector3d[] vertices)
        {
            if (vertices == null)
            {
                throw new ArgumentNullException(nameof(vertices));
            }

            ValidateElementArrayLength(vertices.Length);
            // calculate the bounds from the array of vertices
            Bounds = new Bounds3d(vertices);
            Vertices = vertices;
        }

        /// <summary>
        /// Sets the vertices of the mesh
        /// </summary>
        /// <param name="vertices">The vertices to set</param>
        public void SetVertices(IList<Vector3d> vertices)
        {
            if (vertices == null)
            {
                throw new ArgumentNullException(nameof(vertices));
            }

            SetVertices(vertices.ToArray());
        }

        /// <summary>
        /// Sets the vertices of the mesh
        /// </summary>
        /// <param name="vertices">The vertices to set</param>
        public void SetVertices(Vector2d[] vertices)
        {
            if (vertices == null)
            {
                throw new ArgumentNullException(nameof(vertices));
            }

            var vertexCount = vertices.Length;
            ValidateElementArrayLength(vertexCount);
            Vertices = new Vector3d[vertexCount];

            for (var i = 0; i < vertexCount; ++i)
            {
                Vertices[i] = new Vector3d(vertices[i]);
            }

            Bounds = new Bounds3d(Vertices);
        }

        /// <summary>
        /// Sets the vertices of the mesh
        /// </summary>
        /// <param name="vertices">The vertices to set</param>
        public void SetVertices(IList<Vector2d> vertices)
        {
            if (vertices == null)
            {
                throw new ArgumentNullException(nameof(vertices));
            }

            SetVertices(vertices.ToArray());
        }

        /// <summary>
        /// Sets the indices of the mesh
        /// </summary>
        /// <param name="indices">The indices to set</param>
        public void SetIndices(int[] indices)
        {
            if (indices == null)
            {
                throw new ArgumentNullException(nameof(indices));
            }

            ValidateIndices(indices);
            Indices = indices;
        }

        /// <summary>
        /// Sets the indices of the mesh
        /// </summary>
        /// <param name="indices">The indices to set</param>
        public void SetIndices(IList<int> indices)
        {
            if (indices == null)
            {
                throw new ArgumentNullException(nameof(indices));
            }

            SetIndices(indices.ToArray());
        }

        /// <summary>
        /// Sets the normals of the mesh
        /// </summary>
        /// <param name="normals">The normals to set</param>
        public void SetNormals(Vector3d[] normals)
        {
            if (normals == null)
            {
                throw new ArgumentNullException(nameof(normals));
            }

            ValidateElementArrayLength(normals.Length);
            Normals = normals;
        }

        /// <summary>
        /// Sets the normals of the mesh
        /// </summary>
        /// <param name="normals">The normals to set</param>
        public void SetNormals(IList<Vector3d> normals)
        {
            if (normals == null)
            {
                throw new ArgumentNullException(nameof(normals));
            }

            SetNormals(normals.ToArray());
        }

        /// <summary>
        /// Sets the normals of the mesh
        /// </summary>
        /// <param name="normals">The normals to set</param>
        public void SetNormals(Vector2d[] normals)
        {
            var normalCount = normals.Length;
            ValidateElementArrayLength(normalCount);
            Normals = new Vector3d[normalCount];

            for (var i = 0; i < normalCount; ++i)
            {
                Normals[i] = new Vector3d(normals[i]);
            }
        }

        /// <summary>
        /// Sets the normals of the mesh
        /// </summary>
        /// <param name="normals">The normals to set</param>
        public void SetNormals(IList<Vector2d> normals)
        {
            SetNormals(normals.ToArray());
        }

        /// <summary>
        /// Sets the uvs of the mesh
        /// </summary>
        /// <param name="uvs">The uvs to set</param>
        public void SetUVs(Vector4d[][] uvs)
        {
            if (uvs == null)
            {
                throw new ArgumentNullException(nameof(uvs));
            }

            // check each uv channel
            for (var i = 0; i < uvs.Length; i++)
            {
                if (uvs[i] == null)
                {
                    throw new ArgumentException("Contains null element at " +
                        $"{i}", nameof(uvs));
                }

                if (uvs[i].Length <= 0)
                {
                    throw new ArgumentException("Contains empty element at " +
                        $"{i}", nameof(uvs));
                }

                ValidateElementArrayLength(uvs[i].Length);
            }

            UVs = uvs;
        }


        /// <summary>
        /// Sets the uvs of the mesh
        /// </summary>
        /// <param name="uvs">The uvs to set</param>
        public void SetUVs(IList<IList<Vector4d>> uvs)
        {
            if (uvs == null)
            {
                throw new ArgumentNullException(nameof(uvs));
            }

            var arrayUvs = new Vector4d[uvs.Count][];
            for (var i = 0; i < uvs.Count; ++i)
            {
                arrayUvs[i] = uvs[i].ToArray();
            }

            UVs = arrayUvs;
        }

        /// <summary>
        /// Evaluates a mesh relative to a given point
        /// </summary>
        /// <param name="point">The point to evaluate against</param>
        /// <param name="scale">The scale to apply during the evaluation</param>
        /// <returns>A mesh relative to the given point</returns>
        public Mesh Relative(Vector3d point, double scale)
        {
            var count = _vertices.Length;
            var vertices = new Vector3d[count];

            for (var i = 0; i < count; ++i)
            {
                vertices[i] = (_vertices[i] - point) * scale;
            }

            var mesh = new Mesh(Topology);

            mesh.SetVertices(vertices);

            if (_normals.Length > 0)
            {
                mesh.SetNormals(_normals);
            }

            if (_uvs.Length > 0)
            {
                mesh.SetUVs(_uvs);
            }
                        
            mesh.SetIndices(_indices);

            return mesh;
        }

        private void ValidateElementArrayLength(int length)
        {
            // validate that element length is correct for given topology
            switch (Topology)
            {
                case Topology.Triangles:
                    if (length < 3)
                    {
                        throw new ArgumentException("Triangle toplogy " +
                            "requires 3 or more elements");
                    }
                    break;
                case Topology.Quads:
                    if (length < 4)
                    {
                        throw new ArgumentException("Quad toplogy requires " +
                            "4 or more elements");
                    }
                    break;
                case Topology.Lines:
                    if (length < 2)
                    {
                        throw new ArgumentException("Lines toplogy requires " +
                            "2 or more elements");
                    }
                    break;
                case Topology.LineStrip:
                    if (length < 2)
                    {
                        throw new ArgumentException("LineStrip toplogy " +
                            "requires 2 or more vertices");
                    }
                    break;
                case Topology.Points:
                    if (length < 1)
                    {
                        throw new ArgumentException("LineStrip toplogy " +
                            "requires 1 or more vertices");
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // validate that element arrays have same lengths
            if (_vertices.Length > 0)
            {
                if (length != _vertices.Length)
                {
                    throw new ArgumentException("Mismatch of array size " +
                        "with Vertices");
                }
            }

            if (_normals.Length > 0)
            {
                if (length != _normals.Length)
                {
                    throw new ArgumentException("Mismatch of array size " +
                        "with Normals");
                }
            }

            if (_uvs.Length > 0)
            {
                for (var i = 0; i < _uvs.Length; i++)
                {
                    if (length != _uvs[i].Length)
                    {
                        throw new ArgumentException("Mismatch of array size " +
                            "with UVs");
                    }
                }
            }
        }

        private void ValidateIndices(int[] indices)
        {
            // validate index counts against topology types
            switch (Topology)
            {
                case Topology.Triangles:
                    if (indices.Length % 3 != 0)
                    {
                        throw new ArgumentException("Triangle topology " + 
                            "requires indices to be divisible by 3");
                    }

                    break;
                case Topology.Quads:
                    if (indices.Length % 4 != 0)
                    {
                        throw new ArgumentException("Quad topology requires " + 
                            "indices to be divisible by 4");
                    }

                    break;
                case Topology.Lines:
                    if (indices.Length % 2 != 0)
                    {
                        throw new ArgumentException("Lines topology " +
                            "requires indices to be divisible by 2");
                    }

                    break;
                case Topology.LineStrip:
                    if (indices.Length < 2)
                    {
                        throw new ArgumentException("LineStrip topology " + 
                            "requires index count to be above 1");
                    }

                    break;
                case Topology.Points:
                    if (indices.Length < 1)
                    {
                        throw new ArgumentException("Points topology " + 
                            "requires index count to be above 0");
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // validate that none are below 0
            for (var i = 0; i < indices.Length; i++)
            {
                if (indices[i] < 0)
                {
                    throw new ArgumentException($"Negative index at {i}");
                }
            }
        }

        /// <summary>
        /// Splits the mesh into a number of parts
        /// </summary>
        /// <param name="maxVertexCount">The maximum number of vertices per part</param>
        /// <returns>A list of split meshes</returns>
        public IList<Mesh> Split(int maxVertexCount)
        {
            // just return this mesh if we're already under the max
            if (maxVertexCount <= 0 || VertexCount <= maxVertexCount)
            {
                return new List<Mesh>
                {
                    this
                };
            }

            var indices = new List<int>();
            var vertices = new List<Vector3d>();
            var normals = new List<Vector3d>();

            var meshes = new List<Mesh>();
            var indexSet = new HashSet<int>();
            var indexCount = IndexCount;

            // work through indices, until we hit the max vertex count
            var uniqueIndexCount = -1;
            var totalIndicesAdded = 0;
            var i = 0;
            for (; i < indexCount; ++i)
            {
                // only increment on unique vertex adds
                if (indexSet.Add(_indices[i]))
                {
                    // reached the max vertex count
                    if (++uniqueIndexCount == maxVertexCount)
                    {
                        // determine the index difference for the topology
                        int indexDifference;
                        switch (Topology)
                        {
                            case Topology.Triangles:
                                indexDifference = i % 3;
                                break;
                            case Topology.Quads:
                                indexDifference = i % 4;
                                break;
                            case Topology.Lines:
                                indexDifference = i % 2;
                                break;
                            case Topology.LineStrip:
                                indexDifference = 0;
                                break;
                            case Topology.Points:
                                indexDifference = 0;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        i -= indexDifference;

                        // determine mapping from old indices to new
                        var indexMap = new Dictionary<int, int>();
                        for (int j = totalIndicesAdded, k = -1; j < i; ++j)
                        {
                            if (!indexMap.ContainsKey(_indices[j]))
                            {
                                // add the index mapping
                                indexMap[_indices[j]] = ++k;

                                // add the vertex, normal and uvs
                                vertices.Add(_vertices[_indices[j]]);
                                normals.Add(_normals[_indices[j]]);
                            }

                            indices.Add(indexMap[_indices[j]]);
                        }

                        // create the mesh
                        meshes.Add(new Mesh(Topology));
                        meshes[meshes.Count - 1].SetVertices(vertices);
                        meshes[meshes.Count - 1].SetNormals(normals);
                        meshes[meshes.Count - 1].SetIndices(indices);

                        totalIndicesAdded += indices.Count;

                        // reset everything
                        uniqueIndexCount = 0;
                        indexSet.Clear();
                        vertices = new List<Vector3d>();
                        normals = new List<Vector3d>();
                        indices = new List<int>();
                    }
                }
            }

            if (uniqueIndexCount > 0)
            {
                // determine the index difference for the topology
                int indexDifference;
                switch (Topology)
                {
                    case Topology.Triangles:
                        indexDifference = i % 3;
                        break;
                    case Topology.Quads:
                        indexDifference = i % 4;
                        break;
                    case Topology.Lines:
                        indexDifference = i % 2;
                        break;
                    case Topology.LineStrip:
                        indexDifference = 0;
                        break;
                    case Topology.Points:
                        indexDifference = 0;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                i -= indexDifference;

                // determine mapping from old indices to new
                var indexMap = new Dictionary<int, int>();
                for (int j = totalIndicesAdded, k = -1; j < i; ++j)
                {
                    if (!indexMap.ContainsKey(_indices[j]))
                    {
                        // add the index mapping
                        indexMap[_indices[j]] = ++k;

                        // add the vertex, normal and uvs
                        vertices.Add(_vertices[_indices[j]]);
                        normals.Add(_normals[_indices[j]]);
                    }

                    indices.Add(indexMap[_indices[j]]);
                }

                // create the mesh
                meshes.Add(new Mesh(Topology));
                meshes[meshes.Count - 1].SetVertices(vertices);
                meshes[meshes.Count - 1].SetNormals(normals);
                meshes[meshes.Count - 1].SetIndices(indices);
            }

            return meshes;
        }
    }
}