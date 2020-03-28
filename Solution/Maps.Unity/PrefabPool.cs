using System;
using Maps.Unity.Extensions;
using Maps.Unity.UI;
using UnityEngine;

namespace Maps.Unity
{
    /// <summary>
    /// Responsible for holding a pool of prefabs
    /// </summary>
    public class PrefabPool : MonoBehaviour, IPrefabPool, IDisposable
    {
        /// <inheritdoc />
        public IPool<PoolableIcon> IconPool => _impl.IconPool;

        /// <inheritdoc />
        public IPool<PoolableLabel> LabelPool => _impl.LabelPool;

        /// <inheritdoc />
        public IPool<PoolableSprite> SpritePool => ((IPrefabPool) _impl).SpritePool;

        private PrefabPoolImpl _impl;

        /// <summary>
        /// Initializes a new UIPrefabPool instance
        /// </summary>
        public void Initialize(PrefabModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            gameObject.DestroyChildren();
            _impl = new PrefabPoolImpl(model, transform);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _impl.Dispose();
            gameObject.SafeDestroy();
        }
    }
}