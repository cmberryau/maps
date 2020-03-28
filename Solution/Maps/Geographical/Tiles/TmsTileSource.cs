using System;
using System.Collections.Generic;
using log4net;

namespace Maps.Geographical.Tiles
{
    /// <summary>
    /// The default TMS tile source
    /// </summary>
    public sealed class TmsTileSource : ITileSource
    {
        /// <inheritdoc />
        public int MinZoomLevel => TmsTile.MinZoom;

        /// <inheritdoc />
        public int MaxZoomLevel => TmsTile.MaxZoom;

        /// <inheritdoc />
        public int DefaultZoomLevel => TmsTile.DefaultZoom;

        private const int DefaultMaxTiles = 4;

        /// <inheritdoc />
        public int[] AvailableZoomLevels
        {
            get;
        }

        private static readonly ILog Log = LogManager.GetLogger(typeof(TmsTileSource));

        /// <inheritdoc />
        public TmsTileSource()
        {
            AvailableZoomLevels = new int[TmsTile.MaxZoom - TmsTile.MinZoom + 1];

            for (var i = 0; i < AvailableZoomLevels.Length; ++i)
            {
                AvailableZoomLevels[i] = TmsTile.MinZoom + i;
            }
        }

        /// <inheritdoc />
        public int Zoom(GeodeticBox2d box)
        {
            return TmsTile.ZoomFor(box, DefaultMaxTiles);
        }

        /// <inheritdoc />
        public int Zoom(GeodeticBox2d box, int maxTiles)
        {
            return TmsTile.ZoomFor(box, maxTiles);
        }

        /// <inheritdoc />
        public double Scale(int zoomLevel)
        {
            return 1d / Math.Max(1d, Math.Pow(2d, zoomLevel));
        }

        /// <inheritdoc />
        public long Count(GeodeticBox2d box)
        {
            return TmsTileRange.Create(box, DefaultMaxTiles).TileCount;
        }

        /// <inheritdoc />
        public long Count(GeodeticBox2d box, int zoomLevel)
        {
            return TmsTileRange.Range(box, zoomLevel).TileCount;
        }

        /// <inheritdoc />
        public Tile Get(Geodetic2d coordinate)
        {
            return TmsTile.Create(coordinate, DefaultZoomLevel);
        }

        /// <inheritdoc />
        public Tile GetForZoom(Geodetic2d coordinate, int zoomLevel)
        {
            return TmsTile.Create(coordinate, zoomLevel);
        }

        /// <inheritdoc />
        public IList<Tile> Get(GeodeticBox2d box, bool padding = false)
        {
            return TmsTileRange.Tiles(TmsTileRange.Create(box, DefaultMaxTiles, padding));
        }

        /// <inheritdoc />
        public IList<Tile> Get(GeodeticBox2d box, int maxTiles, bool padding = false)
        {
            return TmsTileRange.Tiles(TmsTileRange.Create(box, maxTiles, padding));
        }

        /// <inheritdoc />
        public IList<Tile> GetForZoom(GeodeticBox2d box, int zoom, bool padding = false)
        {
            return TmsTileRange.Tiles(TmsTileRange.Range(box, zoom, padding));
        }

        /// <inheritdoc />
        public IList<Tile> GetPadding(GeodeticBox2d box, int zoomLevel)
        {
            return TmsTileRange.Padding(TmsTileRange.Range(box, zoomLevel));
        }
    }
}