using System.Collections.Generic;

namespace Maps.Geographical.Tiles
{
    /// <summary>
    /// Provides methods for creating tiles
    /// </summary>
    public interface ITileSource
    {
        /// <summary>
        /// The minimum zoom level for creating tiles, the furthest zoomed
        /// out, the least detailed
        /// </summary>
        int MinZoomLevel
        {
            get;
        }

        /// <summary>
        /// The maximum zoom level for creating tiles, the closest zoomed
        /// in, the most detailed
        /// </summary>
        int MaxZoomLevel
        {
            get;
        }

        /// <summary>
        /// The default zoom level for creating tiles
        /// </summary>
        int DefaultZoomLevel
        {
            get;
        }

        /// <summary>
        /// The available zoom levels at which to create tiles
        /// </summary>
        int[] AvailableZoomLevels
        {
            get;
        }

        /// <summary>
        /// Evaluates the zoom level given the box
        /// </summary>
        /// <param name="box">The box to evaluate</param>
        int Zoom(GeodeticBox2d box);

        /// <summary>
        /// Evaluates the zoom level given the box and maximum number of tiles
        /// </summary>
        /// <param name="box">The box to evaluate</param>
        /// <param name="maxTiles">The maximum number of tiles</param>
        int Zoom(GeodeticBox2d box, int maxTiles);

        /// <summary>
        /// Evaluates the scale for the given zoom level
        /// </summary>
        /// <param name="zoomLevel">The zoom level</param>
        double Scale(int zoomLevel);

        /// <summary>
        /// Evaluates the number of tiles for the given box
        /// </summary>
        /// <param name="box">The box to evaluate</param>
        long Count(GeodeticBox2d box);

        /// <summary>
        /// Evaluates the number of tiles for the given box and zoom level
        /// </summary>
        /// <param name="box">The box to evaluate</param>
        /// <param name="zoomLevel">The zoom level to evaluate</param>
        long Count(GeodeticBox2d box, int zoomLevel);

        /// <summary>
        /// Creates a single tile at the default zoom level
        /// </summary>
        /// <param name="coordinate">The coordinate to create tile at</param>
        Tile Get(Geodetic2d coordinate);

        /// <summary>
        /// Creates a tile at the given zoom level
        /// </summary>
        /// <param name="coordinate">The coordinate to create tile at</param>
        /// <param name="zoomLevel">The zoom level to create the tile</param>
        Tile GetForZoom(Geodetic2d coordinate, int zoomLevel);

        /// <summary>
        /// Creates an automated number of tiles given a box
        /// </summary>
        /// <param name="box">The box to create the tiles from</param>
        /// <param name="padding">Optional padding (1 layer of tiles)</param>
        IList<Tile> Get(GeodeticBox2d box, bool padding = false);

        /// <summary>
        /// Creates a maximum number of tiles that given a box and a max tile count
        /// </summary>
        /// <param name="box">The box to create the tiles from</param>
        /// <param name="maxTiles">The maximum number of tiles</param>
        /// <param name="padding">Optional padding (1 layer of tiles)</param>
        IList<Tile> Get(GeodeticBox2d box, int maxTiles, bool padding);

        /// <summary>
        /// Creates tiles given a box and zoom level
        /// </summary>
        /// <param name="box">The box to create the tiles from</param>
        /// <param name="zoomLevel">The zoom level to create tiles</param>
        /// <param name="padding">Optional included padding tiles (1 layer of tiles)</param>
        IList<Tile> GetForZoom(GeodeticBox2d box, int zoomLevel, bool padding);

        /// <summary>
        /// Creates padding tiles given a box and zoom level
        /// </summary>
        /// <param name="box">The box to create the tiles from</param>
        /// <param name="zoomLevel">The zoom level to create tiles</param>
        IList<Tile> GetPadding(GeodeticBox2d box, int zoomLevel);
    }
}