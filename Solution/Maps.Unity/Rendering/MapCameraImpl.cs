using System;
using Maps.Geometry;
using Maps.Rendering;
using Maps.Unity.Extensions;
using Maps.Unity.Interaction;
using Maps.Unity.Interaction.Input;
using UnityEngine;
using UnityEngine.UI;

namespace Maps.Unity.Rendering
{
    /// <summary>
    /// Responsible for interfacing between a Map instance and a Unity3d 
    /// camera so Map updates can move the camera in the scene
    /// </summary>
    internal sealed class MapCameraImpl : MapCameraBase
    {
        /// <summary>
        /// The camera used by the MapCameraImpl
        /// </summary>
        public readonly Camera Camera;

        private const float FarMinimum = 2e-6f;
        private const float FarPaddingFactor = 1.1f;
        private const float NearMinimum = 1e-6f;
        private const float NearPaddingFactor = 0.5f;

        private Matrix4x4 _lastProjection;

        /// <summary>
        /// Initializes a new instance of MapCameraController
        /// </summary>
        /// <param name="map">The associated map</param>
        /// <param name="camera">The associated unity camera</param>
        /// <param name="layer">The unity layer the camera should be on</param>
        /// <param name="inputHandler">The input handler</param>
        /// <param name="targetImage">The target ui image</param>
        internal MapCameraImpl(IMap map, Camera camera, int layer, InputHandler inputHandler,
            Image targetImage = null) : base(map, camera.transform.Transformd(), 
                camera.projectionMatrix.Matrix4())
        {
            if (camera == null)
            {
                throw new ArgumentNullException(nameof(camera));
            }

            if (inputHandler == null)
            {
                throw new ArgumentNullException(nameof(inputHandler));
            }

            // setup required camera properties
            Camera = camera;
            Camera.cullingMask = 1 << layer;
            Camera.useOcclusionCulling = false;
            Camera.backgroundColor = Color.black;
            Camera.clearFlags = CameraClearFlags.SolidColor;
            Camera.allowHDR = false;

            UpdateClipPlanes((float)Far, (float)Near);
            ClipPlanesChanged += UpdateClipPlanes;

            // store the current projection
            _lastProjection = Camera.projectionMatrix;

            // is the camera's results projected onto another surface?
            if (camera.targetTexture != null && targetImage != null)
            {
                var forwarder = targetImage.gameObject.AddComponent<TargetImageInputForwarder>();
                forwarder.Initialize(inputHandler, camera);
            }
        }

        /// <summary>
        /// Should be called by the holding instance on MonoBehaviour.Update
        /// </summary>
        internal void Update()
        {
            // check for camera transform changes
            if (Camera.transform.hasChanged)
            {
                Camera.transform.hasChanged = false;
                OnTransformChange(Camera.transform.Transformd());
            }

            // check for camera projection changes
            if (Camera.projectionMatrix != _lastProjection)
            {
                _lastProjection = Camera.projectionMatrix;
                OnProjectionChanged(_lastProjection.Matrix4());
            }
        }

        /// <inheritdoc />
        protected override void OnShouldAssumeTransform(Transformd transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            Camera.transform.localPosition = transform.LocalPosition.Vector3();
            Camera.transform.localRotation = transform.LocalRotation.Quaternion();
            Camera.transform.localScale = transform.LocalScale.Vector3();

            // must mark the transform as NOT changed, to prevent extra calls
            Camera.transform.hasChanged = false;
        }

        /// <inheritdoc />
        protected override void DrawDebugRay(Ray3d ray)
        {
            Debug.DrawLine(ray.Origin.Vector3(), (ray.Origin + 
                ray.Direction.Normalised * 10f).Vector3(), Color.red, 10f);
        }

        private void UpdateClipPlanes(double far, double near)
        {
            // set the far clip
            float z;

            if (far > FarMinimum)
            {
                z = (float)far * FarPaddingFactor;
            }
            else
            {
                z = FarMinimum;
            }
            Camera.farClipPlane = z;

            // set the near clip
            if (near > NearMinimum)
            {
                Camera.nearClipPlane = (float)near * NearPaddingFactor;
                z = (float)near * NearPaddingFactor;
            }
            else
            {
                Camera.nearClipPlane = NearMinimum;
                z = NearMinimum;
            }
            Camera.nearClipPlane = z;
        }
    }
}