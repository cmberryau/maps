using System;
using System.Collections.Generic;
using Maps.Lod;
using Maps.Unity.Extensions;
using Maps.Unity.Interaction.Input;
using Maps.Unity.Rendering;
using UnityEngine;

namespace Maps.Unity.Lod
{
    /// <summary>
    /// Represents the lod tree for a map, responsible for creating LOD's in Unity3D
    /// </summary>
    internal sealed class MapLodTreeImpl : MapLodTreeBase, IDisposable
    {
        private readonly GameObject _gameObject;
        private readonly TranslatorFactory _translatorFactory;
        private readonly IList<MapLod> _lods;
        private readonly InputHandler _inputHandler;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of MapLodTreeImpl
        /// </summary>
        /// <param name="gameObject">The game object containing the lodtree</param>
        /// <param name="factory">The translator factory</param>
        /// <param name="inputHandler">The input handler</param>
        public MapLodTreeImpl(GameObject gameObject, TranslatorFactory factory,
            InputHandler inputHandler)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (inputHandler == null)
            {
                throw new ArgumentNullException(nameof(inputHandler));
            }

            _lods = new List<MapLod>();
            _gameObject = gameObject;
            _translatorFactory = factory;
            _inputHandler = inputHandler;
        }

        /// <inheritdoc />
        public override IMapLod CreateLod(int level, double scale)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapLodTreeImpl));
            }

            // create the lod game object
            var lodGameObject = new GameObject($"LOD_{level}_");

            // the new gameobject is parented to the tree gameobject
            lodGameObject.transform.SetParent(_gameObject.transform, false);
            // the new gameobject must take on the same layer as the tree gameobject
            lodGameObject.layer = _gameObject.layer;
            // lod gameobject starts off inactive
            lodGameObject.SetActive(false);

            // add the map lod to the gameobject
            var lod = lodGameObject.AddComponent<MapLod>();
            if (lod == null)
            {
                throw new InvalidOperationException("Could not add a MapLod component");
            }

            // initialize and store the lod
            lod.Initialize(scale, Transform, Anchor, _translatorFactory, _inputHandler);
            _lods.Add(lod);

            return lod;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapLodTreeImpl));
            }

            _disposed = true;

            for (var i = 0; i < _lods.Count; i++)
            {
                _lods[i].Dispose();
            }
        }

        /// <inheritdoc />
        protected override void OnShouldAssumeTransform()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapLodTreeImpl));
            }

            _gameObject.transform.localPosition = Transform.LocalPosition.Vector3();
            _gameObject.transform.localRotation = Transform.LocalRotation.Quaternion();
            _gameObject.transform.localScale = Transform.LocalScale.Vector3();
        }
    }
}