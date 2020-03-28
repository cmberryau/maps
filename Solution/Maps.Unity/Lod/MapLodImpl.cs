using System;
using Maps.Appearance;
using Maps.Geographical.Tiles;
using Maps.Lod;
using Maps.Unity.Extensions;
using Maps.Unity.Geographical.Tiles;
using Maps.Unity.Interaction.Input;
using Maps.Unity.Rendering;
using Maps.Unity.Threading;
using UnityEngine;

namespace Maps.Unity.Lod
{
    /// <summary>
    /// Responsible for managing a single map lod in Unity3d
    /// </summary>
    internal sealed class MapLodImpl : MapLodBase, IDisposable
    {
        private readonly GameObject _gameObject;
        private readonly TranslatorFactory _translatorFactory;
        private readonly InputHandler _inputHandler;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of MapLodImpl
        /// </summary>
        /// <param name="gameObject">The lod game object</param>
        /// <param name="scale">The scale of the lod</param>
        /// <param name="parent">The parent transform</param>
        /// <param name="anchor">The anchor transform</param>
        /// <param name="factory">The factory to use</param>
        /// <param name="inputHandler">The input handler</param>
        public MapLodImpl(GameObject gameObject, double scale, Transformd parent, 
            Transformd anchor, TranslatorFactory factory, InputHandler inputHandler) 
            : base(parent, anchor, scale)
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

            // clean up any dangling children
            gameObject.DestroyChildren();

            _gameObject = gameObject;
            _translatorFactory = factory;
            _inputHandler = inputHandler;
        }

        /// <inheritdoc />
        public override void Show()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapLodImpl));
            }

            _gameObject.SetActive(true);
        }

        /// <inheritdoc />
        public override void Hide()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapLodImpl));
            }

            _gameObject.SetActive(false);
        }

        /// <inheritdoc />
        protected override IDisplayTile DisplayTileFor(Tile tile, IMapAppearance appearance)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapLodImpl));
            }

            if (tile == null)
            {
                throw new ArgumentNullException(nameof(tile));
            }

            // create the implementation for the display tile
            var impl = new DisplayTileImpl(appearance, tile, InverseScale, Anchor, 
                Transform, _translatorFactory, _inputHandler);

            // create the actual game object
            Coroutines.Queue(() => CreateDisplayTileGameObject(impl));

            // return the implementation, may not be initialised yet
            return impl;
        }

        private void CreateDisplayTileGameObject(DisplayTileImpl impl)
        {
            // create the game object
            var tileGameObject = new GameObject($"{impl}_");
            tileGameObject.transform.SetParent(_gameObject.transform, false);
            tileGameObject.layer = _gameObject.layer;

            var displayTile = tileGameObject.AddComponent<DisplayTile>();
            if (displayTile == null)
            {
                throw new NullReferenceException($"Could not create {nameof(Geographical.Tiles.DisplayTile)} instance");
            }

            // initialize the display tile component
            displayTile.Initialize(impl);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapLodImpl));
            }

            _disposed = true;

            // clean up any dangling children
            _gameObject.DestroyChildren();
            _gameObject.SafeDestroy();
        }
    }
}