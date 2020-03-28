using UnityEngine;
using UnityEngine.EventSystems;

namespace Maps.Unity.Interaction.Response
{
    /// <summary>
    /// Interface to provide input response to pointer click events
    /// </summary>
    public interface IPointerClickResponder
    {
        /// <summary>
        /// Called when the pointer has clicked i.e (a screen tap or mouse click)
        /// </summary>
        /// <param name="screenPosition">The impulse screen position</param>
        /// <param name="worldPosition">The impulse world position</param>
        /// <param name="button">The index of the button pressed</param>
        void RecievedPointerClick(Vector2 screenPosition, Vector3 worldPosition,
            PointerEventData.InputButton button);
    }
}