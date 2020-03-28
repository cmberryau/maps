using System;
using Maps.Unity.Extensions;
using UnityEngine;

namespace Maps.Unity
{
    /// <summary>
    /// Responsible for the implentation of a game object which is IPoolable
    /// </summary>
    public class PoolableGameObjectImpl : IPoolable
    {
        private readonly Transform _objectTransform;
        private readonly Transform _poolTransform;

        /// <summary>
        /// Initializes a new instance of PoolableGameObjectImpl
        /// </summary>
        /// <param name="objectTransform">The transform of the object</param>
        /// <param name="poolTransform">The transform of the pool the object 
        /// belongs to</param>
        public PoolableGameObjectImpl(Transform objectTransform, Transform poolTransform)
        {
            if (objectTransform == null)
            {
                throw new ArgumentNullException(nameof(objectTransform));
            }

            if (poolTransform == null)
            {
                throw new ArgumentNullException(nameof(poolTransform));
            }

            _objectTransform = objectTransform;
            _poolTransform = poolTransform;
        }

        /// <inheritdoc />
        public void OnAddedToPool()
        {
            ReturnToPool();
        }

        /// <inheritdoc />
        public void OnReturnedToPool()
        {
            ReturnToPool();
        }

        /// <inheritdoc />
        public void OnTakenFromPool()
        {
            _objectTransform.gameObject.SetActive(true);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _objectTransform.gameObject.SafeDestroy();
        }

        private void ReturnToPool()
        {
            _objectTransform.SetParent(_poolTransform, false);
            _objectTransform.gameObject.SetActive(false);
        }
    }
}