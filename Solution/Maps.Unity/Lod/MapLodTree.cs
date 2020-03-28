using System;
using Maps.Lod;
using Maps.Unity.Extensions;
using Maps.Unity.Interaction.Input;
using Maps.Unity.Rendering;
using UnityEngine;

namespace Maps.Unity.Lod
{
    /// <summary>
    /// Responsible for creating lods and managing their lifecycle
    /// </summary>
    public sealed class MapLodTree : MonoBehaviour, IMapLodTree, IDisposable
    {
        /// <inheritdoc />
        public Transformd Anchor => _impl.Anchor;

        /// <inheritdoc />
        public Transformd Transform => _impl.Transform;

        private MapLodTreeImpl _impl;
        private bool _disposed;

        /// <summary>
        /// Initializes the MapLodTree instance
        /// </summary>
        /// <param name="factory">The translator factory</param>
        /// <param name="inputHandler">The input handler</param>
        public void Initialize(TranslatorFactory factory, InputHandler inputHandler)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapLodTree));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (inputHandler == null)
            {
                throw new ArgumentNullException(nameof(inputHandler));
            }

            // clean up any dangling children
            gameObject.DestroyChildren();
            _impl = new MapLodTreeImpl(gameObject, factory, inputHandler);
        }

        /// <inheritdoc />
        public IMapLod CreateLod(int level, double scale)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapLodTree));
            }

            return _impl.CreateLod(level, scale);
        }

        /// <inheritdoc />
        public void OnUpdate()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapLodTree));
            }

            _impl.OnUpdate();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapLodTree));
            }

            _disposed = true;

            _impl.Dispose();
            gameObject.SafeDestroy();
        }
    }
}