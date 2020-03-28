using System;
using System.Collections.Generic;
using Maps.Appearance;
using Maps.Geographical.Features;
using Maps.Geographical.Tiles;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Maps.Unity.Geographical.Tiles
{
    /// <summary>
    /// Responsible for allowing control display of a single map tile in Unity3d
    /// </summary>
    [RequireComponent(typeof(BoxCollider))]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class DisplayTile : MonoBehaviour, IDisplayTile, IDragHandler, 
        IPointerClickHandler, IScrollHandler
    {
        /// <inheritdoc />
        public bool Active
        {
            get => _impl.Active;
            set => _impl.Active = value;
        }

        /// <inheritdoc />
        public bool HasFeatures
        {
            get => _impl.HasFeatures;
        }

        /// <inheritdoc />
        public IMapAppearance Appearance
        {
            get => _impl.Appearance;
            set => _impl.Appearance = value;
        }

        private DisplayTileImpl _impl;

        /// <summary>
        /// Initializes the DisplayTile instance
        /// </summary>
        /// <param name="impl">The display tile implementation</param>
        internal void Initialize(DisplayTileImpl impl)
        {
            if (impl == null)
            {
                throw new ArgumentNullException(nameof(impl));
            }

            var boxCollider = GetComponent<BoxCollider>();
            if (boxCollider == null)
            {
                throw new NullReferenceException($"Could not find a {nameof(BoxCollider)} " +
                    $", {nameof(DisplayTile)} requires an attached {nameof(BoxCollider)}");
            }

            // todo: remove collider sizing hack
            boxCollider.size = new Vector3(10f, 10f, 0.1f);

            impl.Initialize(gameObject);
            _impl = impl;
        }

        /// <inheritdoc />
        public void SetFeatures(IList<Feature> features)
        {
            if (features == null)
            {
                throw new ArgumentNullException(nameof(features));
            }

            _impl.SetFeatures(features);
        }

        /// <inheritdoc />
        public void OnUpdate()
        {
            _impl.OnUpdate();
        }

        /// <inheritdoc />
        public void OnDrag(PointerEventData eventData)
        {
            if (eventData == null)
            {
                throw new ArgumentNullException(nameof(eventData));
            }

            _impl.OnDrag(eventData);
        }

        /// <inheritdoc />
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData == null)
            {
                throw new ArgumentNullException(nameof(eventData));
            }

            _impl.OnPointerClick(eventData);
        }

        /// <inheritdoc />
        public void OnScroll(PointerEventData eventData)
        {
            if (eventData == null)
            {
                throw new ArgumentNullException(nameof(eventData));
            }

            _impl.OnScroll(eventData);
        }
    }
}