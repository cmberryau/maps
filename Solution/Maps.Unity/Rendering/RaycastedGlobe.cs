using System;
using Maps.Geometry;
using Maps.Unity.Extensions;
using UnityEngine;

namespace Maps.Unity.Rendering
{
    /// <summary>
    /// Creates a raycasted globe
    /// </summary>
    public sealed class RaycastedGlobe : MonoBehaviour, IDisposable
    {
        /// <summary>
        /// The material used for raycasting the globe
        /// </summary>
        public Material RaycastedGlobeMaterial;

        private const string OneOverRadiusSquaredProperty = "_OneOverRadiiSquared";
        private const string OneOverPIProperty = "_OneOverPI";
        private const string OneOver2PIProperty = "_OneOver2PI";

        private int _oneOverRadiusSquaredPropertyIndex;
        private int _oneOverPIPropertyIndex;
        private int _oneOver2PIPropertyIndex;

        private Ellipsoid _ellipsoid;
        private GameObject _globeObject;
#pragma warning disable 414
        private bool _disposed;
#pragma warning restore 414

        /// <summary>
        /// Initializes the RaycastedGlobe instance
        /// </summary>
        public void Initialize()
        {
            if (_globeObject == null)
            {
                if (RaycastedGlobeMaterial == null)
                {
                    throw new NullReferenceException(nameof(RaycastedGlobeMaterial) + " must be set.");
                }

                //_ellipsoid = new Ellipsoid(Ellipsoid.NormalisedWgs84Radii * 10d);
                _ellipsoid = Ellipsoid.UnitSphere;

                // set the material properties for the ellipsoid
                SetGlobeMaterialProperties(_ellipsoid);

                // create the globe game object
                _globeObject = CreateGlobe(_ellipsoid);
            }

            _disposed = false;
        }

        /// <summary>
        /// Disposes all resources held by the RaycastedGlobe instance
        /// </summary>
        public void Dispose()
        {
            if (_globeObject != null)
            {
                if (Application.isPlaying)
                {
                    Destroy(_globeObject);
                    _globeObject = null;
                }
            }

            _disposed = true;
        }

        private void SetGlobeMaterialProperties(Ellipsoid ellipsoid)
        {
            if (RaycastedGlobeMaterial.HasProperty(OneOverPIProperty))
            {
                _oneOverPIPropertyIndex = Shader.PropertyToID(OneOverPIProperty);
                RaycastedGlobeMaterial.SetFloat(_oneOverPIPropertyIndex,
                                                (float) (1d / Math.PI));
            }
            else
            {
                throw new InvalidOperationException(nameof(RaycastedGlobeMaterial) + 
                    " is missing property: " + OneOverPIProperty);
            }

            if (RaycastedGlobeMaterial.HasProperty(OneOver2PIProperty))
            {
                _oneOver2PIPropertyIndex = Shader.PropertyToID(OneOver2PIProperty);
                RaycastedGlobeMaterial.SetFloat(_oneOver2PIPropertyIndex,
                                                (float) (1d / (2 * Math.PI)));
            }
            else
            {
                throw new InvalidOperationException(nameof(RaycastedGlobeMaterial) +
                    " is missing property: " + OneOver2PIProperty);
            }

            if (RaycastedGlobeMaterial.HasProperty(OneOverRadiusSquaredProperty))
            {
                _oneOverRadiusSquaredPropertyIndex = Shader.PropertyToID(OneOverRadiusSquaredProperty);
                // yz must be flipped to conform with Unity3d
                RaycastedGlobeMaterial.SetVector(_oneOverRadiusSquaredPropertyIndex, 
                    ellipsoid.OneOverRadiiSquared.xzy.Vector3());
            }
            else
            {
                throw new InvalidOperationException(nameof(RaycastedGlobeMaterial) +
                    " is missing property: " + OneOverRadiusSquaredProperty);
            }
        }

        private GameObject CreateGlobe(Ellipsoid ellipsoid)
        {
            var globe = new GameObject("Globe");

            var globeRenderer = globe.AddMeshRenderer();
            globeRenderer.material = RaycastedGlobeMaterial;

            var filter = globe.AddComponent<MeshFilter>();
            if (filter.sharedMesh == null)
            {
                filter.sharedMesh = new UnityEngine.Mesh();
            }

            var mesh = filter.sharedMesh;

            var vertices = new Vector3[8];

            vertices[0] = new Vector3((float)-ellipsoid.Radii.x, 
                (float)ellipsoid.Radii.y, (float)-ellipsoid.Radii.z);
            vertices[1] = new Vector3((float)-ellipsoid.Radii.x, 
                (float)ellipsoid.Radii.y, (float)ellipsoid.Radii.z);
            vertices[2] = new Vector3((float)ellipsoid.Radii.x, 
                (float)ellipsoid.Radii.y, (float)ellipsoid.Radii.z);
            vertices[3] = new Vector3((float)ellipsoid.Radii.x, 
                (float)ellipsoid.Radii.y, (float)-ellipsoid.Radii.z);

            vertices[4] = new Vector3((float)-ellipsoid.Radii.x, 
                (float)-ellipsoid.Radii.y, (float)-ellipsoid.Radii.z);
            vertices[5] = new Vector3((float)-ellipsoid.Radii.x, 
                (float)-ellipsoid.Radii.y, (float)ellipsoid.Radii.z);
            vertices[6] = new Vector3((float)ellipsoid.Radii.x, 
                (float)-ellipsoid.Radii.y, (float)ellipsoid.Radii.z);
            vertices[7] = new Vector3((float)ellipsoid.Radii.x, 
                (float)-ellipsoid.Radii.y, (float)-ellipsoid.Radii.z);

            var indices = new int[6 * 2 * 3]; // 6 sides, 2 triangles per side, 3 indices per tri

            // +y
            indices[0] = 0;
            indices[1] = 2;
            indices[2] = 1;

            indices[3] = 0;
            indices[4] = 3;
            indices[5] = 2;

            // -z
            indices[6] = 0;
            indices[7] = 4;
            indices[8] = 3;

            indices[9] = 3;
            indices[10] = 4;
            indices[11] = 7;

            // +z
            indices[12] = 1;
            indices[13] = 2;
            indices[14] = 5;

            indices[15] = 2;
            indices[16] = 6;
            indices[17] = 5;

            // -x
            indices[18] = 5;
            indices[19] = 0;
            indices[20] = 1;

            indices[21] = 5;
            indices[22] = 4;
            indices[23] = 0;

            // +x
            indices[24] = 2;
            indices[25] = 3;
            indices[26] = 6;

            indices[27] = 6;
            indices[28] = 3;
            indices[29] = 7;

            // -y
            indices[30] = 4;
            indices[31] = 5;
            indices[32] = 6;

            indices[33] = 6;
            indices[34] = 7;
            indices[35] = 4;

            mesh.vertices = vertices;
            mesh.SetIndices(indices, MeshTopology.Triangles, 0);

            return globe;
        }

        /// <summary>
        /// Called before the first frame if enabled, only ever called once
        /// </summary>
        private void Start()
        {
            Initialize();
        }

        /// <summary>
        /// Called when first attached to an object and when the reset 
        /// command is used in the Inspector
        /// </summary>
        private void Reset()
        {
            Dispose();
            Initialize();
        }

        /// <summary>
        /// Called when the monobehaviour is enabled, is also called when
        /// Unity3d's editor recompiles code
        /// </summary>
        private void OnEnable()
        {
            Initialize();
        }

        /// <summary>
        /// Called when the monobehaviour is disabled
        /// </summary>
        private void OnDisable()
        {
            Dispose();
        }
    }
}
