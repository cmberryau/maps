using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Maps.Unity.Extensions;
using UnityEngine;

namespace Maps.Unity.Threading
{
    /// <summary>
    /// Responsible for out-of-unity thread multithreadingesque capabilities
    /// </summary>
    public sealed class Coroutines : MonoBehaviour, IDisposable
    {
        /// <summary>
        /// Is the calling thread the main Unity thread?
        /// </summary>
        internal static bool IsUnityThread => CoroutinesImpl.IsUnityThread;

        private static Coroutines _instance;

        /// <summary>
        /// Initializes Coroutines, call from Unity thread before calling 
        /// Queue
        /// </summary>
        public void Initialize()
        {
            // coroutines instance is being renewed
            if (_instance != null)
            {
                // destroy all queued
                CoroutinesImpl.Clear();
            }

            // mark this thread as unity thread
            CoroutinesImpl.MarkAsUnityThread();

            _instance = this;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            gameObject.SafeDestroy();
        }

        /// <summary>
        /// Queues a routine
        /// </summary>
        /// <param name="routine">The routine to queue</param>
        internal static void Queue(IEnumerator routine)
        {
            CoroutinesImpl.Queue(routine);
        }

        /// <summary>
        /// Queues an action
        /// </summary>
        /// <param name="action">The action to queue</param>
        internal static void Queue(Action action)
        {
            CoroutinesImpl.Queue(action);
        }

        private void Update()
        {
            CoroutinesImpl.Update(this);
        }

        private static class CoroutinesImpl
        {
            internal static bool IsUnityThread => 
                Thread.CurrentThread.Equals(_unityThread);

            private static readonly IList<IEnumerator> Routines;
            private static readonly object RoutinesLock;
            private static Thread _unityThread;

            static CoroutinesImpl()
            {
                Routines = new List<IEnumerator>();
                RoutinesLock = new object();
            }

            internal static void MarkAsUnityThread()
            {
                _unityThread = Thread.CurrentThread;
            }

            internal static void Queue(IEnumerator routine)
            {
                if (routine == null)
                {
                    throw new ArgumentNullException(nameof(routine));
                }

                lock (RoutinesLock)
                {
                    Routines.Add(routine);
                }
            }

            internal static void Queue(Action action)
            {
                if (action == null)
                {
                    throw new ArgumentNullException(nameof(action));
                }

                lock (RoutinesLock)
                {
                    Routines.Add(ActionToCoroutine(action));
                }
            }

            internal static void Update(MonoBehaviour monoBehaviour)
            {
                lock (RoutinesLock)
                {
                    if (Routines.Count > 0)
                    {
                        foreach (var routine in Routines)
                        {
                            monoBehaviour.StartCoroutine(routine);
                        }

                        Routines.Clear();
                    }
                }
            }

            internal static void Clear()
            {
                lock (RoutinesLock)
                {
                    if (Routines.Count > 0)
                    {
                        Routines.Clear();
                    }
                }
            }

            private static IEnumerator ActionToCoroutine(Action action)
            {
                yield return null;
                action();
            }
        }
    }
}