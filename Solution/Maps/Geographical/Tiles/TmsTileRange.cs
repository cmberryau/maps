using System;
using System.Collections.Generic;

namespace Maps.Geographical.Tiles
{
    /// <summary>
    /// Represents a range of tiles which comply with
    /// GoogleMaps, HERE and OpenStreetMap TMS y-flipped coordinates
    ///
    /// See Maps.Geographical.TmsTile
    /// </summary>
    internal sealed class TmsTileRange
    {
        /// <summary>
        /// The minimum x index
        /// </summary>
        public readonly int XMin;

        /// <summary>
        /// The minimum y index
        /// </summary>
        public readonly int YMin;

        /// <summary>
        /// The maximum x index
        /// </summary>
        public readonly int XMax;

        /// <summary>
        /// The maximum y index
        /// </summary>
        public readonly int YMax;

        /// <summary>
        /// The tile count for the tilerange
        /// </summary>
        public readonly long TileCount;

        /// <summary>
        /// The zoom level
        /// </summary>
        public readonly int Zoom;

        /// <summary>
        /// Creates a new tile range
        /// </summary>
        /// <param name="xmin">The minimum x index</param>
        /// <param name="ymin">The minimum y index</param>
        /// <param name="xmax">The maximum x index</param>
        /// <param name="ymax">The maximum y index</param>
        /// <param name="zoom">The zoom level</param>
        public TmsTileRange(int xmin, int ymin, int xmax, int ymax, int zoom)
        {
            if (zoom < TmsTile.MinZoom || zoom > TmsTile.MaxZoom)
            {
                throw new ArgumentOutOfRangeException(nameof(zoom), "Zoom " +
                    $"must be between {TmsTile.MinZoom}, {TmsTile.MaxZoom}, " +
                    $"was {zoom}");
            }

            var maxIndex = TmsTile.MaxXyIndex(zoom);

            if (xmin < 0 || xmin > maxIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(xmin));
            }

            if (xmax < 0 || xmax > maxIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(xmax));
            }

            if (ymin < 0 || ymin > maxIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(ymin));
            }

            if (ymax < 0 || ymax > maxIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(ymax));
            }

            XMin = xmin;
            XMax = xmax;
            YMin = ymin;
            YMax = ymax;
            Zoom = zoom;
            TileCount = (Math.Abs(XMax - XMin) + 1) * 
                        (Math.Abs(YMax - YMin) + 1);
        }

        /// <summary>
        /// Creates a new tile range from the given box and zoom level
        /// </summary>
        /// <param name="box">The geographical box to create the tile range around</param>
        /// <param name="zoom">The zoom which to create the tile range at</param>
        /// <param name="padding">Optional padding (1 layer of tiles)</param>
        public static TmsTileRange Range(GeodeticBox2d box, int zoom, 
            bool padding = false)
        {
            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            if (zoom < TmsTile.MinZoom || zoom > TmsTile.MaxZoom)
            {
                throw new ArgumentOutOfRangeException(nameof(zoom), "Zoom " +
                    $"must be between {TmsTile.MinZoom}, {TmsTile.MaxZoom}, " +
                    $"was {zoom}");
            }

            // clamp the box bounds to TMS bounds
            box = GeodeticBox2d.ClampAbs(box, TmsTile.LatitudeLimit, 
                TmsTile.LongitudeLimit);

            var xmin = TmsTile.LongitudeToTileX(box.MinimumLongitude, zoom);
            var xmax = TmsTile.LongitudeToTileX(box.MaximumLongitude, zoom);

            // flipped y index
            var ymin = TmsTile.LatitudeToTileY(box.MaximumLatitude, zoom);
            var ymax = TmsTile.LatitudeToTileY(box.MinimumLatitude, zoom);

            // expand by 1 if padding was requested
            if (padding)
            {
                --xmin;
                ++xmax;
                --ymin;
                ++ymax;
            }

            return new TmsTileRange(xmin, ymin, xmax, ymax, zoom);
        }

        /// <summary>
        /// Creates a new tile range from the given box and maximum tile count
        /// </summary>
        /// <param name="box">The geographical box to create the tile range around</param>
        /// <param name="maxTiles">The maximum number of tiles in the range</param>
        /// <param name="padding">Optional padding (1 layer of tiles)</param>
        public static TmsTileRange Create(GeodeticBox2d box, int maxTiles, 
            bool padding = false)
        {
            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            return Range(box, TmsTile.ZoomFor(box, maxTiles), padding);
        }

        /// <summary>
        /// Returns a new IList of Tiles represented contained in the TmsTileRange
        /// </summary>
        /// <param name="range">The TmsTileRange to create the IList from</param>
        /// <param name="padding">Optional padding (1 layer of tiles)</param>
        public static IList<Tile> Tiles(TmsTileRange range, bool padding = false)
        {
            if (range == null)
            {
                throw new ArgumentNullException(nameof(range));
            }

            var xmin = range.XMin;
            var xmax = range.XMax;
            var ymin = range.YMin;
            var ymax = range.YMax;

            // expand by 1 if padding was requested
            if (padding)
            {
                --xmin;
                ++xmax;
                --ymin;
                ++ymax;
            }

            var tiles = new TmsTile[range.TileCount];
            for (int x = xmin, i = 0; x < xmax + 1; ++x)
            {
                for (var y = ymin; y < ymax + 1; ++y)
                {
                    var tile = new TmsTile(x, y, range.Zoom);
                    tiles[i++] = tile;
                }
            }

            return tiles;
        }

        /// <summary>
        /// Returns a list of tiles which are the padding tiles for the given range
        /// </summary>
        /// <param name="range">The range of tiles to evaluate padding for</param>
        public static IList<Tile> Padding(TmsTileRange range)
        {
            if (range == null)
            {
                throw new ArgumentNullException(nameof(range));
            }

            var xmin = range.XMin - 1;
            var xmax = range.XMax + 1;
            var ymin = range.YMin - 1;
            var ymax = range.YMax + 1;

            var tiles = new TmsTile[(xmax - xmin) * 2 + (ymax - ymin) * 2];
            var i = -1;

            for (var x = xmin; x < xmax + 1; ++x)
            {
                tiles[++i] = new TmsTile(x, ymin, range.Zoom);
                tiles[++i] = new TmsTile(x, ymax, range.Zoom);
            }

            for (var y = ymin + 1; y < ymax; ++y)
            {
                tiles[++i] = new TmsTile(xmin, y, range.Zoom);
                tiles[++i] = new TmsTile(xmax, y, range.Zoom);
            }

            return tiles;
        }
    }
}