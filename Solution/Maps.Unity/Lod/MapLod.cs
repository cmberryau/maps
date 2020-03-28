 using System;
 using System.Collections.Generic;
 using Maps.Appearance;
 using Maps.Geographical.Tiles;
 using Maps.Lod;
 using Maps.Unity.Interaction.Input;
 using Maps.Unity.Rendering;
 using UnityEngine;

namespace Maps.Unity.Lod
{
    /// <summary>
    /// Responsible for managing a single map lod in Unity3d
    /// </summary>
    public sealed class MapLod : MonoBehaviour, IMapLod, IDisposable
    {
        /// <inheritdoc />
        public double Scale => _impl.Scale;

        /// <inheritdoc />
        public double InverseScale => _impl.InverseScale;

        private MapLodImpl _impl;

        /// <summary>
        /// Initializes the MapLod instance
        /// </summary>
        /// <param name="scale">The scale of the lod</param>
        /// <param name="parent">The parent transform</param>
        /// <param name="anchor">The initial anchor</param>
        /// <param name="factory">The translator factory for the lod</param>
        /// <param name="inputHandler">The input handler</param>
        public void Initialize(double scale, Transformd parent, Transformd anchor, 
            TranslatorFactory factory, InputHandler inputHandler)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            if (anchor == null)
            {
                throw new ArgumentNullException(nameof(anchor));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (inputHandler == null)
            {
                throw new ArgumentNullException(nameof(inputHandler));
            }

            _impl = new MapLodImpl(gameObject, scale, parent, anchor, factory, inputHandler);
        }

        /// <inheritdoc />
        public IDisplayTile DisplayTile(Tile tile, IMapAppearance appearance)
        {
            return _impl.DisplayTile(tile, appearance);
        }

        /// <inheritdoc />
        public IList<IDisplayTile> DisplayTiles(IList<Tile> tiles, IMapAppearance appearance)
        {
            return _impl.DisplayTiles(tiles, appearance);
        }

        /// <inheritdoc />
        public void Show()
        {
            _impl.Show();
        }

        /// <inheritdoc />
        public void Hide()
        {
            _impl.Hide();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _impl.Dispose();
        }
    }
}