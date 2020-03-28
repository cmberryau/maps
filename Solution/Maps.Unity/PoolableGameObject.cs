using UnityEngine;

namespace Maps.Unity
{
    /// <summary>
    /// Represents a game object which is IPoolable
    /// </summary>
    public class PoolableGameObject : MonoBehaviour, IPoolable
    {
        private PoolableGameObjectImpl _impl;

        /// <summary>
        /// Initializes an instance of PoolableUIElement
        /// </summary>
        /// <param name="pool">The transform of the pool</param>
        public void Initialize(Transform pool)
        {
            _impl = new PoolableGameObjectImpl(transform, pool);
        }

        /// <inheritdoc />
        public virtual void OnAddedToPool()
        {
            _impl.OnAddedToPool();
        }

        /// <inheritdoc />
        public virtual void OnReturnedToPool()
        {
            _impl.OnReturnedToPool();
        }

        /// <inheritdoc />
        public virtual void OnTakenFromPool()
        {
            _impl.OnTakenFromPool();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _impl.Dispose();
        }
    }
}