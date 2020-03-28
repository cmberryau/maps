using UnityEngine;
using UnityEngine.EventSystems;

namespace Maps.Unity.Interaction.Response
{
    /// <summary>
    /// Simple interface for objects giving response to mouse drag input
    /// </summary>
    public interface IPointerDragResponder
    {
        /// <summary>
        /// Called on a mouse drag
        /// </summary>
        /// <param name="startScreenPosition">The start screen position</param>
        /// <param name="startWorldPosition">The start world position</param>
        /// <param name="endScreenPosition">The final screen position</param>
        /// <param name="endWorldPosition">The final world position</param>
        /// <param name="button">The button dragged</param>
        void RecievedPointerDrag(Vector2 startScreenPosition, 
            Vector3 startWorldPosition, Vector2 endScreenPosition, 
            Vector3 endWorldPosition, PointerEventData.InputButton button);
    }
}