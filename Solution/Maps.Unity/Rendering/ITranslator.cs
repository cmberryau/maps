using System.Collections.Generic;
using Maps.Rendering;
using UnityEngine;

namespace Maps.Unity.Rendering
{
    /// <summary>
    /// Interface for classes with the ability to translate Renderable objects
    /// </summary>
    public interface ITranslator
    {
        /// <summary>
        /// Submits a list of renderables, safe to call from non-unity thread
        /// </summary>
        /// <param name="renderables">The list of renderables to translate</param>
        void Submit(IList<Renderable> renderables);

        /// <summary>
        /// Creates and attaches the game objects, ONLY call from the main unity thread
        /// </summary>
        /// <param name="parent">The object to parent objects to (see remarks)</param>
        /// <returns>The fully translated game objects</returns>
        /// <remarks>Not all objects will take the parent as their parent, as they may
        /// need to be parented to another object (canvas etc)</remarks>
        IList<GameObject> Translate(GameObject parent);
    }
}