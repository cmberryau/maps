using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Maps.Appearance;
using Maps.Geographical.Tiles;
using Maps.Rendering;
using Maps.Unity.Extensions;
using Maps.Unity.Interaction.Input;
using Maps.Unity.Rendering;
using Maps.Unity.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Maps.Unity.Geographical.Tiles
{
    /// <summary>
    /// Responsible for the DisplayTile implementation
    /// </summary>
    internal sealed class DisplayTileImpl : DisplayTileBase, IDragHandler, 
        IPointerClickHandler, IScrollHandler
    {
        private GameObject _gameObject;
        private IMapAppearance _lastActiveMapAppearance;
        private readonly object _lastMapAppearanceLock;
        private readonly IDictionary<IMapAppearance, GameObject> _roots;
        private readonly TranslatorFactory _translatorFactory;
        private readonly InputHandler _inputHandler;

        /// <summary>
        /// Initializes a new instance of DisplayTileImpl
        /// </summary>
        /// <param name="appearance">The initial appearance</param>
        /// <param name="tile">The tile</param>
        /// <param name="scale">The scale</param>
        /// <param name="parent">The parent transform</param>
        /// <param name="anchor">The anchor transform</param>
        /// <param name="factory">The translator factory</param>
        /// <param name="inputHandler">The input handler</param>
        public DisplayTileImpl(IMapAppearance appearance, Tile tile, double scale, 
            Transformd anchor, Transformd parent, TranslatorFactory factory, 
            InputHandler inputHandler) : base(appearance, tile, scale, parent, anchor)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (inputHandler == null)
            {
                throw new ArgumentNullException(nameof(inputHandler));
            }

            _roots = new ConcurrentDictionary<IMapAppearance, GameObject>();
            _lastMapAppearanceLock = new object();
            _translatorFactory = factory;
            _inputHandler = inputHandler;
        }

        /// <summary>
        /// Initializes the DisplayTileImpl instance
        /// </summary>
        /// <param name="gameObject">The game object containing the tile</param>
        public void Initialize(GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            _gameObject = gameObject;
            _gameObject.SetActive(Active);

            // assume the transform from above
            OnShouldAssumeTransform(Transform);
        }

        /// <inheritdoc />
        public void OnDrag(PointerEventData eventData)
        {
            if (eventData == null)
            {
                throw new ArgumentNullException(nameof(eventData));
            }

            _inputHandler.OnPointerDrag(eventData);
        }

        /// <inheritdoc />
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData == null)
            {
                throw new ArgumentNullException(nameof(eventData));
            }

            _inputHandler.OnPointerClick(eventData);
        }

        /// <inheritdoc />
        public void OnScroll(PointerEventData eventData)
        {
            if (eventData == null)
            {
                throw new ArgumentNullException(nameof(eventData));
            }

            _inputHandler.OnPointerScroll(eventData);
        }

        /// <inheritdoc />
        protected override void OnBecameActive()
        {
            if (_gameObject != null)
            {
                _gameObject.SetActive(true);
            }
        }

        /// <inheritdoc />
        protected override void OnBecameInactive()
        {
            if (_gameObject != null)
            {
                _gameObject.SetActive(false);
            }
        }

        /// <inheritdoc />
        protected override void OnMapAppearanceChanged(IMapAppearance appearance)
        {
            if (appearance == null)
            {
                throw new ArgumentNullException(nameof(appearance));
            }

            // create a new appearance root object if we don't have one already
            if (!_roots.ContainsKey(appearance))
            {
                Coroutines.Queue(() =>
                {
                    _roots[appearance] = CreateAppearanceRoot(appearance);
                });
            }

            // if it exists, disable the last active appearance root object
            IMapAppearance lastActive;

            lock (_lastMapAppearanceLock)
            {
                lastActive = _lastActiveMapAppearance;
                _lastActiveMapAppearance = appearance;
            }

            // set the previous appearance root to false
            Coroutines.Queue(() =>
            {
                if (lastActive != null)
                {
                    _roots[lastActive].SetActive(false);
                }

                // set the new appearance root to active
                _roots[appearance].SetActive(true);
            });
        }

        /// <inheritdoc />
        protected override void OnRenderablesReady(IList<Renderable> renderables,
            IMapAppearance appearance)
        {
            if (renderables == null)
            {
                throw new ArgumentNullException(nameof(renderables));
            }

            if (appearance == null)
            {
                throw new ArgumentNullException(nameof(appearance));
            }

            // create a translator
            var translator = _translatorFactory.Create(Transform);
            // translate the renderables
            translator.Submit(renderables);

            // create a new appearance root object if we don't have one already
            if (!_roots.ContainsKey(appearance))
            {
                Coroutines.Queue(() =>
                {
                    _roots[appearance] = CreateAppearanceRoot(appearance);
                    translator.Translate(_roots[appearance]);
                });
            }

            // if it exists, disable the last active appearance root object
            IMapAppearance lastActive;
            lock (_lastMapAppearanceLock)
            {
                lastActive = _lastActiveMapAppearance;
                _lastActiveMapAppearance = appearance;
            }

            // set the previous appearance root to false
            Coroutines.Queue(() =>
            {
                if (lastActive != null)
                {
                    _roots[lastActive].SetActive(false);
                }

                // set the new appearance root to active
                _roots[appearance].SetActive(true);
            });
        }

        /// <inheritdoc />
        protected override void OnShouldAssumeTransform(Transformd transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            if (_gameObject != null)
            {
                _gameObject.transform.localPosition = transform.LocalPosition.Vector3();
                _gameObject.transform.localRotation = transform.LocalRotation.Quaternion();
                _gameObject.transform.localScale = transform.LocalScale.Vector3();
            }
        }

        private GameObject CreateAppearanceRoot(IMapAppearance appearance)
        {
            var rootGameObject = new GameObject($"{appearance}_");
            
            rootGameObject.transform.SetParent(_gameObject.transform, false);
            rootGameObject.layer = _gameObject.layer;
            rootGameObject.SetActive(false);

            return rootGameObject;
        }
    }
}