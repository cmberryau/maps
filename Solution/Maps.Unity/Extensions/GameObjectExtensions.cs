using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Extensions for the UnityEngine.GameObject class
    /// </summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Destroys all children of the game object
        /// </summary>
        /// <param name="gameObject">The game object to to destroy all children from</param>
        public static void DestroyChildren(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            var children = new GameObject[gameObject.transform.childCount];
            for (var i = 0; i < children.Length; i++)
            {
                children[i] = gameObject.transform.GetChild(i).gameObject;
            }

            for (var i = 0; i < children.Length; i++)
            {
                if (Application.isEditor)
                {
                    GameObject.DestroyImmediate(children[i]);
                }
                else
                {
                    GameObject.Destroy(children[i]);
                }
            }

            // detach the children after deletion
            gameObject.transform.DetachChildren();
        }

        /// <summary>
        /// Safely destroys the game object
        /// </summary>
        /// <param name="gameObject">The game object to destroy</param>
        public static void SafeDestroy(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            if (Application.isEditor)
            {
                GameObject.DestroyImmediate(gameObject);
            }
            else
            {
                GameObject.Destroy(gameObject);
            }
        }

        /// <summary>
        /// Adds a mesh renderer to the game object with the default
        /// set of options
        /// </summary>
        /// <param name="gameObject">The game object to add the mesh
        /// renderer to</param>
        /// <returns>The added mesh renderer</returns>
        public static MeshRenderer AddMeshRenderer(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            var meshRenderer = gameObject.AddComponent<MeshRenderer>();

            // default settings for mesh renderers
            meshRenderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
            meshRenderer.receiveShadows = false;
            meshRenderer.lightProbeUsage = LightProbeUsage.Off;
            meshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
            meshRenderer.shadowCastingMode = ShadowCastingMode.Off;

            return meshRenderer;
        }
    }
}