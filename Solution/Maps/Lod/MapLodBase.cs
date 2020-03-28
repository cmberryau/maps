using System;
using System.Collections.Generic;
using Maps.Appearance;
using Maps.Geographical.Tiles;

namespace Maps.Lod
{
    /// <summary>
    /// Responsible for managing a single map lod
    /// </summary>
    public abstract class MapLodBase : IMapLod
    {
        /// <inheritdoc />
        public double Scale
        {
            get;
        }

        /// <inheritdoc />
        public double InverseScale
        {
            get;
        }

        /// <summary>
        /// The transform for the lod
        /// </summary>
        protected readonly Transformd Transform;

        /// <summary>
        /// The anchor for the lod
        /// </summary>
        protected readonly Transformd Anchor;

        private readonly IDictionary<Tile, IDisplayTile> _displayTiles;
        private readonly object _displayTilesLock;

        /// <summary>
        /// Initializes a new instance of MapLodBase
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <param name="anchor">The anchor transform</param>
        /// <param name="scale">The scale of the map lod</param>
        protected MapLodBase(Transformd parent, Transformd anchor, double scale)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            if (anchor == null)
            {
                throw new ArgumentNullException(nameof(anchor));
            }

            _displayTiles = new Dictionary<Tile, IDisplayTile>();
            _displayTilesLock = new object();

            Transform = new Transformd();
            Transform.SetParent(parent);
            Anchor = anchor;

            Scale = scale;
            InverseScale = 1d / scale;
        }

        /// <inheritdoc />
        public virtual IList<IDisplayTile> DisplayTiles(IList<Tile> tiles, 
            IMapAppearance appearance)
        {
            if (tiles == null)
            {
                throw new ArgumentNullException(nameof(tiles));
            }

            lock (_displayTilesLock)
            {
                var displayTiles = new List<IDisplayTile>();

                for (var i = 0; i < tiles.Count; ++i)
                {
                    var tile = tiles[i];

                    if (!_displayTiles.TryGetValue(tile, out IDisplayTile displayTile))
                    {
                        displayTile = DisplayTileFor(tile, appearance);
                        _displayTiles.Add(tile, displayTile);
                    }

                    displayTiles.Add(displayTile);
                }

                return displayTiles;
            }
        }

        /// <inheritdoc />
        public virtual IDisplayTile DisplayTile(Tile tile, IMapAppearance appearance)
        {
            if (tile == null)
            {
                throw new ArgumentNullException(nameof(tile));
            }

            lock (_displayTilesLock)
            {
                if (!_displayTiles.TryGetValue(tile, out IDisplayTile displayTile))
                {
                    displayTile = DisplayTileFor(tile, appearance);
                    _displayTiles.Add(tile, displayTile);
                }

                return displayTile;
            }
        }

        /// <inheritdoc />
        public abstract void Show();

        /// <inheritdoc />
        public abstract void Hide();

        /// <summary>
        /// Returns the display tile for the given tile
        /// </summary>
        /// <param name="tile">The tile to create a display tile for</param>
        /// <param name="appearance">The initial map appearance for the tile</param>
        protected abstract IDisplayTile DisplayTileFor(Tile tile, IMapAppearance appearance);
    }
}