using System.Collections.Generic;
using Maps.Appearance;
using Maps.Geographical.Tiles;

namespace Maps.Lod
{
    /// <summary>
    /// Interface for objects with the responsibility of managing a single map lod
    /// </summary>
    public interface IMapLod
    {
        /// <summary>
        /// The scale of the lod
        /// </summary>
        double Scale
        {
            get;
        }

        /// <summary>
        /// The inverse scale of the lod
        /// </summary>
        double InverseScale
        {
            get;
        }

        /// <summary>
        /// Returns the display tile for a given tile
        /// </summary>
        /// <param name="tile">The tile</param>
        /// <param name="appearance">The map appearance</param>
        IDisplayTile DisplayTile(Tile tile, IMapAppearance appearance);

        /// <summary>
        /// Returns the display tiles for the given tiles
        /// </summary>
        /// <param name="tiles">The tiles</param>
        /// <param name="appearance">The map appearance</param>
        IList<IDisplayTile> DisplayTiles(IList<Tile> tiles, IMapAppearance appearance);

        /// <summary>
        /// Shows the lod
        /// </summary>
        void Show();

        /// <summary>
        /// Hides the lod
        /// </summary>
        void Hide();
    }
}