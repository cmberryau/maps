using System;
using System.Reflection;
using Maps.Geographical;
using Maps.Rendering;
using Maps.Unity.Interaction.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Maps.Unity.Rendering
{
    /// <summary>
    /// Responsible for interfacing between a Map instance and a Unity3d 
    /// camera so Map updates can move the camera in the scene
    /// </summary>
    [RequireComponent(typeof(PhysicsRaycaster))]
    [RequireComponent(typeof(Camera))]
    public class MapCamera : MonoBehaviour, IMapCamera
    {
        /// <summary>
        /// The target image
        /// </summary>
        [SerializeField, Obfuscation(Feature = "renaming", Exclude = true)]
        public Image TargetImage;

        /// <inheritdoc />
        public GeodeticBox2d VisibleArea => _impl.VisibleArea;

        /// <inheritdoc />
        public event VisibleAreaChangeHandler VisibleAreaChanged;

        /// <summary>
        /// Called when the clip planes have been changed
        /// </summary>
        public event ClipPlanesChangeHandler ClipPlanesChanged
        {
            add
            {
                _impl.ClipPlanesChanged += value;
            }
            remove
            {
                _impl.ClipPlanesChanged += value;
            }
        }

        /// <summary>
        /// The camera used by the MapCamera
        /// </summary>
        public Camera Camera => _impl.Camera;

        private MapCameraImpl _impl;

        /// <summary>
        /// Initializes the MapCamera
        /// </summary>
        /// <param name="map">The map instance to be associated with</param>
        /// <param name="inputHandler">The input handler</param>
        /// <param name="layer">The unity layer the camera should be on</param>
        public void Initialize(IMap map, InputHandler inputHandler, int layer)
        {
            if (map == null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            if (inputHandler == null)
            {
                throw new ArgumentNullException(nameof(inputHandler));
            }

            var theCamera = GetComponent<Camera>();

            if (theCamera == null)
            {
                throw new NullReferenceException($"Could not find a {nameof(UnityEngine.Camera)} " +
                    $", {nameof(MapCamera)} requires an attached {nameof(UnityEngine.Camera)}");
            }

            var physicsRaycaster = GetComponent<PhysicsRaycaster>();

            if (physicsRaycaster == null)
            {
                throw new NullReferenceException($"Could not find a {nameof(PhysicsRaycaster)} " +
                    $", {nameof(MapCamera)} requires an attached {nameof(PhysicsRaycaster)}");
            }

            // set the bit mask for event layers
            physicsRaycaster.eventMask = 1 << layer;
            _impl = new MapCameraImpl(map, theCamera, layer, inputHandler, TargetImage);
            _impl.VisibleAreaChanged += OnImplVisibleAreaChanged;
        }

        private void Update()
        {
            if (_impl != null)
            {
                _impl.Update();
            }
        }

        private void OnImplVisibleAreaChanged(GeodeticBox2d area, bool lodChange)
        {
            if (VisibleAreaChanged != null)
            {
                VisibleAreaChanged(area, lodChange);
            }
        }
    }
}