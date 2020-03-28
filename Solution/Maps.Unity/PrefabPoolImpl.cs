using System;
using Maps.Unity.UI;
using UnityEngine;

namespace Maps.Unity
{
    /// <summary>
    /// Responsible for the implementation of holding a pool of prefabs
    /// </summary>
    internal sealed class PrefabPoolImpl : IPrefabPool, IDisposable
    {
        /// <inheritdoc />
        public IPool<PoolableIcon> IconPool => _iconPool;

        /// <inheritdoc />
        public IPool<PoolableLabel> LabelPool => _labelPool;

        /// <inheritdoc />
        public IPool<PoolableSprite> SpritePool => _spritePool;

        private readonly IPool<PoolableIcon> _iconPool;
        private readonly IPool<PoolableLabel> _labelPool;
        private readonly IPool<PoolableSprite> _spritePool;

        /// <summary>
        /// Initializes a new instance of PrefabPoolImpl
        /// </summary>
        /// <param name="prefabModel">The prefab model to use</param>
        /// <param name="root">The root transform for all prefab pools</param>
        public PrefabPoolImpl(PrefabModel prefabModel, Transform root)
        {
            if (prefabModel == null)
            {
                throw new ArgumentNullException(nameof(prefabModel));
            }

            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            if (prefabModel.IconPrefab != null)
            {
                _iconPool = new Pool<PoolableIcon>(() => CreateInstance<PoolableIcon>(
                    prefabModel.IconPrefab, root));
            }

            if (prefabModel.LabelPrefab != null)
            {
                _labelPool = new Pool<PoolableLabel>(() => CreateInstance<PoolableLabel>(
                    prefabModel.LabelPrefab, root));
            }

            if (prefabModel.SpritePrefab != null)
            {
                _spritePool = new Pool<PoolableSprite>(() => CreateInstance<PoolableSprite>(
                    prefabModel.SpritePrefab, root));
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _iconPool.Dispose();
            _labelPool.Dispose();
            _spritePool.Dispose();
        }

        private static T CreateInstance<T>(Transform original, Transform parent) 
            where T : PoolableGameObject
        {
            var instance = GameObject.Instantiate(original);
            var poolable = instance.GetComponent<T>();
            poolable.Initialize(parent);

            return poolable;
        }
    }
}