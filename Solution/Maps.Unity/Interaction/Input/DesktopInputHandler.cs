using System;
using System.Collections.Generic;
using Maps.Unity.Interaction.Response;
using UnityEngine;

namespace Maps.Unity.Interaction.Input
{
    /// <summary>
    /// Handles desktop input and calls appropriate responders
    /// </summary>
    public class DesktopInputHandler : InputHandler
    {
        private const float KeyPressThreshold = 0.100f; // 100ms

        private readonly IDictionary<KeyCode, IList<IKeyboardPressResponder>> _keyboardPressResponders;
        private readonly IDictionary<KeyCode, float> _lastKeyDownTime;
        private readonly IList<KeyCode> _keysWatchList;

        /// <summary>
        /// Initializes a new instance of DesktopInputHandler
        /// </summary>
        public DesktopInputHandler()
        {
            _keyboardPressResponders = new Dictionary<KeyCode, IList<IKeyboardPressResponder>>();
            _lastKeyDownTime = new Dictionary<KeyCode, float>();
            _keysWatchList = new List<KeyCode>();
        }

        /// <inheritdoc />
        public override void AddResponder(object obj)
        {
            base.AddResponder(obj);

            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            // check if it's responding to keyboard presses
            if (obj is IKeyboardPressResponder)
            {
                var keyboardPressResponder = (IKeyboardPressResponder) obj;

                foreach (var key in keyboardPressResponder.Keys)
                {
                    if (!_keyboardPressResponders.ContainsKey(key))
                    {
                        _keyboardPressResponders.Add(key, new List<IKeyboardPressResponder>());
                        _keysWatchList.Add(key);
                    }

                    // todo: get rid of list contains check
                    if (!_keyboardPressResponders[key].Contains(keyboardPressResponder))
                    {
                        _keyboardPressResponders[key].Add(keyboardPressResponder);
                    }
                }
            }
        }

        /// <summary>
        /// Should be called by the holding MonoBehaviour to update the InputHandler
        /// </summary>
        public override void Update()
        {
            var time = Time.time;
            var watchedKeysCount = _keysWatchList.Count;
            // run through the keys we have in our watchlist
            for (var i = 0; i < watchedKeysCount; ++i)
            {
                var key = _keysWatchList[i];
                var press = false;

                // check if key is down
                if (UnityEngine.Input.GetKeyDown(key))
                {
                    // record the time it was down
                    _lastKeyDownTime[key] = time;
                }
                // check if key went up
                else if (UnityEngine.Input.GetKeyUp(key))
                {
                    // check if it's a press
                    if (_lastKeyDownTime.TryGetValue(key, out float lastDownTime))
                    {
                        if (time - lastDownTime < KeyPressThreshold)
                        {
                            press = true;
                        }
                    }
                }

                // if the key was pressed and we have responders, notify them
                if (press && _keyboardPressResponders.TryGetValue(key, 
                    out IList<IKeyboardPressResponder> responders))
                {
                    var respondersCount = responders.Count;
                    for (var j = 0; j < respondersCount; ++j)
                    {
                        responders[j].RecievedKeyboardPress(key);
                    }
                }
            }
        }
    }
}