using System.Collections.Generic;
using UnityEngine;

namespace Maps.Unity.Interaction.Response
{
    /// <summary>
    /// Interface to provide input response to keyboard events
    /// </summary>
    public interface IKeyboardPressResponder
    {
        /// <summary>
        /// The keys that this responder is listening for
        /// </summary>
        HashSet<KeyCode> Keys
        {
            get;
        }

        /// <summary>
        /// Called once the user has pressed a key
        /// </summary>
        /// <param name="key">The key that was pressed</param>
        void RecievedKeyboardPress(KeyCode key);
    }
}