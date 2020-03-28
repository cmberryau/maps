using System;
using System.Collections.Generic;
using Maps.Rendering;
using Maps.Unity.Appearance;
using Maps.Unity.Extensions;
using Maps.Unity.UI;
using UnityEngine;
using Mesh = Maps.Geometry.Mesh;

namespace Maps.Unity.Rendering
{
    /// <summary>
    /// Responsible for translation of Renderable types
    /// </summary>
    public class Translator : ITranslator, IRenderableVisitor
    {
        private readonly IMaterialsModel _materialsModel;
        private readonly UIElementFactory _uiFactory;
        private readonly IPrefabPool _prefabPool;
        private readonly Transformd _parent;

        // mesh renderable related
        private readonly IList<Material> _materials;
        private readonly IDictionary<Material, Mesh> _meshMap;

        // point renderable related
        private readonly IList<UIRenderable> _pointRenderables;

        // prepared arrays
        private readonly IDictionary<Material, MeshTopology> _topologies;
        private readonly IDictionary<Material, IList<List<Vector3>>> _vertices;
        private readonly IDictionary<Material, IList<int[]>> _indices;
        private readonly IDictionary<Material, IList<List<Vector3>>> _normals;
        private readonly IDictionary<Material, IList<List<List<Vector4>>>> _uvs;

        /// <summary>
        /// Initializes a new instance of of Translator
        /// </summary>
        /// <param name="materialsModel">The materials model to use</param>
        /// <param name="uiFactory">The ui element factory</param>
        /// <param name="prefabPool">The prefab pool</param>
        /// <param name="parent">The anchor</param>
        public Translator(IMaterialsModel materialsModel, UIElementFactory uiFactory,
            IPrefabPool prefabPool, Transformd parent)
        {
            if (materialsModel == null)
            {
                throw new ArgumentNullException(nameof(materialsModel));
            }

            if (uiFactory == null)
            {
                throw new ArgumentNullException(nameof(uiFactory));
            }

            if (prefabPool == null)
            {
                throw new ArgumentNullException(nameof(prefabPool));
            }

            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            _materials = new List<Material>();
            _meshMap = new Dictionary<Material, Mesh>();
            _pointRenderables = new List<UIRenderable>();

            _topologies = new Dictionary<Material, MeshTopology>();
            _vertices = new Dictionary<Material, IList<List<Vector3>>>();
            _indices = new Dictionary<Material, IList<int[]>>();
            _normals = new Dictionary<Material, IList<List<Vector3>>>();
            _uvs = new Dictionary<Material, IList<List<List<Vector4>>>>();

            _materialsModel = materialsModel;
            _uiFactory = uiFactory;
            _prefabPool = prefabPool;
            _parent = parent;
        }

        /// <inheritdoc />
        public void Submit(IList<Renderable> renderables)
        {
            if (renderables == null)
            {
                throw new ArgumentNullException(nameof(renderables));
            }

            var renderableCount = renderables.Count;
            for (var i = 0; i < renderableCount; ++i)
            {
                renderables[i].Accept(this);
            }

            // work through materials
            var materialCount = _materials.Count;
            for (var i = 0; i < materialCount; ++i)
            {
                var material = _materials[i];
                var mesh = _meshMap[material];
                IList<Mesh> meshes;

                // determine if we need to split meshes up
                if (_meshMap[material].VertexCount > MeshExtensions.MaxVertices)
                {
                    meshes = _meshMap[material].Split(MeshExtensions.MaxVertices);
                }
                else
                {
                    meshes = new List<Mesh>
                    {
                        _meshMap[material]
                    };
                }

                // set the topologyt for the mesh
                _topologies.Add(material, mesh.Topology.UnityTopology());

                // setup all the different unity required mesh data
                for (var j = 0; j < meshes.Count; ++j)
                {
                    mesh = meshes[j];

                    // create the lists for the mesh data
                    _vertices.Add(material, new List<List<Vector3>>());
                    _indices.Add(material, new List<int[]>());
                    _normals.Add(material, new List<List<Vector3>>());
                    _uvs.Add(material, new List<List<List<Vector4>>>());

                    // convert the mesh data from our format to theirs
                    _vertices[material].Add(mesh.Vertices.Vector3());
                    _indices[material].Add(mesh.Indices);
                    _normals[material].Add(mesh.Normals.Vector3());
                    _uvs[material].Add(new List<List<Vector4>>());
                    for (var k = 0; k < mesh.UVs.Length; ++k)
                    {
                        _uvs[material][k].Add(mesh.UVs[k].Vector4());
                    }
                }
            }

            // meshes are no longer required
            _meshMap.Clear();
        }

        /// <inheritdoc />
        public IList<GameObject> Translate(GameObject parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            var gameObjects = new List<GameObject>();
            var materialCount = _materials.Count;
            for (var i = 0; i < materialCount; ++i)
            {
                // create the initial game object
                var gameObject = new GameObject($"{nameof(MeshRenderable)}_");
                gameObject.transform.SetParent(parent.transform, false);
                gameObject.layer = parent.layer;

                // attach a mesh renderer to it
                var meshRenderer = gameObject.GetComponent<MeshRenderer>();
                if (meshRenderer == null)
                {
                    meshRenderer = gameObject.AddMeshRenderer();
                }
                else
                {
                    throw new InvalidOperationException($"{nameof(gameObject)} " +
                        $"already has {nameof(MeshRenderer)} component");
                }

                // set the material
                var material = _materials[i];
                meshRenderer.sharedMaterial = material;

                // set the mesh filter
                var meshFilter = gameObject.GetComponent<MeshFilter>();
                if (meshFilter == null)
                {
                    meshFilter = gameObject.AddComponent<MeshFilter>();
                }
                else
                {
                    throw new InvalidOperationException($"{nameof(gameObject)} " +
                        $"already has {nameof(MeshFilter)} component");
                }

                // create and set the mesh
                meshFilter.mesh = new UnityEngine.Mesh();
                SetMesh(material, 0, meshFilter.mesh);

                // if we have split up meshes
                for (var j = 1; j < _vertices[material].Count; ++j)
                {
                    // create the sub game object
                    var subGameObject = new GameObject($"{nameof(MeshRenderable)}_");
                    subGameObject.transform.SetParent(gameObject.transform, false);
                    subGameObject.layer = parent.layer;

                    // mesh renderer
                    var subMeshRenderer = subGameObject.GetComponent<MeshRenderer>();
                    if (subMeshRenderer == null)
                    {
                        subMeshRenderer = subGameObject.AddMeshRenderer();
                    }
                    else
                    {
                        throw new InvalidOperationException(
                            $"{nameof(subGameObject)} already has " +
                            $"{nameof(MeshRenderer)} component");
                    }

                    // set the material
                    subMeshRenderer.sharedMaterial = material;

                    // create the filter
                    var subMeshFilter = subGameObject.GetComponent<MeshFilter>();
                    if (subMeshFilter == null)
                    {
                        subMeshFilter = subGameObject.AddComponent<MeshFilter>();
                    }
                    else
                    {
                        throw new InvalidOperationException(
                            $"{nameof(subGameObject)} already has " +
                            $"{nameof(MeshFilter)} component");
                    }

                    // create and set the mesh
                    subMeshFilter.mesh = new UnityEngine.Mesh();
                    SetMesh(material, j, subMeshFilter.mesh);
                }

                gameObjects.Add(gameObject);
            }

            // clear up everything related to meshes
            _materials.Clear();
            _topologies.Clear();
            _vertices.Clear();
            _indices.Clear();
            _normals.Clear();
            _uvs.Clear();

            // move onto point renderables
            var pointRenderableCount = _pointRenderables.Count;
            if (pointRenderableCount > 0)
            {
                // point renderables require only one game object
                var pointObject = new GameObject($"{nameof(UIRenderable)}_");
                pointObject.transform.SetParent(parent.transform, false);
                pointObject.layer = parent.layer;

                // add the ui element renderer to the object
                var renderer = pointObject.GetComponent<UIElementRenderer>();
                if (renderer == null)
                {
                    renderer = pointObject.AddComponent<UIElementRenderer>();
                    renderer.Initialize(_prefabPool, _parent);
                }

                // add the point renderables to the ui element renderer
                for (var i = 0; i < pointRenderableCount; ++i)
                {
                    renderer.Add(_uiFactory.Create(_pointRenderables[i]));
                }

                gameObjects.Add(pointObject);
            }

            _pointRenderables.Clear();

            return gameObjects;
        }

        /// <inheritdoc />
        public void Visit(MeshRenderable renderable)
        {
            if (renderable == null)
            {
                throw new ArgumentNullException(nameof(renderable));
            }

            // we combine all material sharing meshes together
            var material = _materialsModel.MaterialFor(renderable.Appearance);

            if (!_meshMap.ContainsKey(material))
            {
                _materials.Add(material);
                _meshMap.Add(material, renderable.Mesh);
            }
            else
            {
                _meshMap[material].Append(renderable.Mesh);
            }
        }

        /// <inheritdoc />
        public void Visit(UIRenderable renderable)
        {
            if (renderable == null)
            {
                throw new ArgumentNullException(nameof(renderable));
            }

            _pointRenderables.Add(renderable);
        }

        private void SetMesh(Material material, int index, UnityEngine.Mesh mesh)
        {
            mesh.SetVertices(_vertices[material][index]);
            mesh.SetNormals(_normals[material][index]);

            for (var i = 0; i < _uvs[material][index].Count; ++i)
            {
                mesh.SetUVs(i, _uvs[material][index][i]);
            }

            mesh.SetIndices(_indices[material][index], _topologies[material], 0);
        }
    }
}