using System;
using System.Collections.Generic;
using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Extensions for the Maps.Geometry.Mesh class specific to Unity3d
    /// </summary>
    public static class MeshExtensions
    {
        /// <summary>
        /// The number of maximum vertices a mesh can have in Unity3d
        /// </summary>
        public const int MaxVertices = 65000;

        /// <summary>
        /// Performs a conversion from Maps.Geometry.Mesh to 
        /// UnityEngine.Mesh with encoding for Maps' high precision shaders
        /// </summary>
        /// <param name="mesh">The mesh to convert</param>
        public static Mesh UnityMesh(this Geometry.Mesh mesh)
        {
            if (mesh == null)
            {
                throw new ArgumentNullException(nameof(mesh));
            }

            // if the mesh has more than 65000 vertices, it cannot pass
            if (mesh.VertexCount > MaxVertices)
            {
                throw new ArgumentException($"Must have no more than {MaxVertices} vertices ({mesh.VertexCount})");
            }

            // high vertices go into vertex, low vertices go into tangent
            var highVertices = new Vector3[mesh.Vertices.Length];
            for (var i = 0; i < highVertices.Length; ++i)
            {
                highVertices[i] = mesh.Vertices[i].Vector3();
            }

            // normals go into normal, with no lowhigh pair
            var normals = new Vector3[mesh.Normals.Length];
            for (var i = 0; i < normals.Length; ++i)
            {
                normals[i] = mesh.Normals[i].Vector3();
            }
            
            // create the unity mesh
            var unityMesh = new Mesh
            {
                vertices = highVertices,
                normals = normals,
            };

            // set the indices & topo
            unityMesh.SetIndices(mesh.Indices, mesh.Topology.UnityTopology(), 0);
            // uv channel count is clamped 0-4
            var uvChannelCount = Math.Min(4, mesh.UVs.Length);
            // uvs go into uv
            var uvs = new List<List<Vector4>>(uvChannelCount);

            for (var i = 0; i < uvChannelCount; ++i)
            {
                uvs.Add(new List<Vector4>(mesh.UVs[i].Length));

                for (var j = 0; j < uvs[i].Count; ++j)
                {
                    uvs[i].Add(mesh.UVs[i][j].Vector4());
                }

                unityMesh.SetUVs(i, uvs[i]);
            }

            // set the unity mesh bounds
            unityMesh.bounds = mesh.Bounds.Bounds(); 

            return unityMesh;
        }
    }
}