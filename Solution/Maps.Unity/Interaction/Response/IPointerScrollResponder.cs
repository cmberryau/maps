using UnityEngine;

namespace Maps.Unity.Interaction.Response
{
    /// <summary>
    /// Simple interface for objects giving response to pointer scroll input
    /// </summary>
    public interface IPointerScrollResponder
    {
        /// <summary>
        /// Called on a mouse wheel scroll or two finger vertical drag
        /// </summary>
        /// <param name="screenPosition">The screen position</param>
        /// <param name="worldPosition">The world position</param>
        /// <param name="scrollDelta">The scroll delta</param>
        void RecievedPointerScroll(Vector2 screenPosition, Vector3 worldPosition,
            Vector2 scrollDelta);
    }
}