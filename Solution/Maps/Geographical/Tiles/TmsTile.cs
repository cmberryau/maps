using System;
using System.Collections.Generic;

namespace Maps.Geographical.Tiles
{
    /// <summary>
    /// Represents a geographical tile which complies with
    /// GoogleMaps, HERE and OpenStreetMap TMS y-flipped coordinates
    /// 
    /// Tiles start in the top left to the bottom right. The x and y index
    /// or it's lat/long conversion does not refer to the centre of the tile
    /// but to the top left of the tile.
    /// 
    /// http://wiki.openstreetmap.org/wiki/Slippy_map_tilenames#Lon..2Flat._to_tile_numbers
    /// http://www.maptiler.org/google-maps-coordinates-tile-bounds-projection/
    /// </summary>
    internal sealed class TmsTile : Tile
    {
        /// <inheritdoc />
        public override IList<Tile> SubTiles
        {
            get
            {
                if (Zoom >= MaxZoom)
                {
                    return new Tile[0];
                }

                var tiles = new Tile[4];
                var next = Zoom + 1;
                var x2 = x * 2;
                var y2 = y * 2;

                tiles[0] = new TmsTile(x2, y2, next);
                tiles[1] = new TmsTile(x2 + 1, y2, next);
                tiles[2] = new TmsTile(x2, y2 + 1, next);
                tiles[3] = new TmsTile(x2 + 1, y2 + 1, next);

                return tiles;
            }
        }

        /// <inheritdoc />
        public override IList<Tile> SuperTiles
        {
            get
            {
                if (Zoom <= MinZoom)
                {
                    return new Tile[0];
                }

                var tiles = new Tile[1];
                var next = Zoom - 1;
                var x2 = x / 2;
                var y2 = y / 2;

                tiles[0] = new TmsTile(x2, y2, next);

                return tiles;
            }
        }

        /// <summary>
        /// Max absolute longitude coverage by TMS (full coverage)
        /// </summary>
        public const double LongitudeLimit = 180;

        /// <summary>
        /// atan(sinh(pi)), the max absolute latitude coverage by TMS
        /// </summary>
        public const double LatitudeLimit = 85.0511287798066;

        /// <summary>
        /// The typical minimum zoom of TMS
        /// </summary>
        public const int MinZoom = 0;

        /// <summary>
        /// The typical maximum zoom of TMS
        /// </summary>
        public const int MaxZoom = 21;

        /// <summary>
        /// The default zoom level of TMS
        /// </summary>
        public const int DefaultZoom = 16;

        /// <summary>
        /// The x index of the tile
        /// </summary>
        public readonly int x;

        /// <summary>
        /// The y index of the tile
        /// </summary>
        public readonly int y;

        /// <summary>
        /// The zoom level of the Tile
        /// </summary>
        public readonly int Zoom;

        private static readonly long MaxId = ((long)Math.Pow(2, MaxZoom)
            - 1 << 32) + ((long)Math.Pow(2, MaxZoom) - 1 << 8) + MaxZoom;
        private const int ZoomMask = 0xFF;
        private const int IndexMask = 0xFFFFFF;

        /// <summary>
        /// Creates a new TMS Tile
        /// </summary>
        /// <param name="x">The x index of the tile</param>
        /// <param name="y">The y index of the tile</param>
        /// <param name="zoom">The zoom level of the tile</param>
        public TmsTile(int x, int y, int zoom) : base(TileId(x, y, zoom))
        {
            if (zoom < MinZoom || zoom > MaxZoom)
            {
                throw new ArgumentOutOfRangeException(nameof(zoom), "Zoom " +
                    $"must be between {MinZoom}, {MaxZoom}, was {zoom}");
            }

            var maxIndex = MaxXyIndex(zoom);

            if (x < 0 || x > maxIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(x));
            }

            if (y < 0 || y > maxIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(y));
            }

            this.x = x;
            this.y = y;
            Zoom = zoom;

            Box = new GeodeticBox2d(Coordinate(x, y, zoom), Coordinate(x + 1,
                y + 1, zoom));
        }

        /// <summary>
        /// Creates a new TMS tile
        /// </summary>
        /// <param name="id">The id to create the tile from</param>
        public TmsTile(long id) : base(id)
        {
            if (Id < 0 || Id > MaxId)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Must be " +
                    $"between 0 and {MaxId}, was {id}");
            }

            Zoom = ZoomFromId(id);

            if (Zoom < MinZoom || Zoom > MaxZoom)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "Zoom " +
                    $"must be between {MinZoom}, {MaxZoom}, was {Zoom}");
            }

            x = XFromId(id);

            var maxIndex = MaxXyIndex(Zoom);

            if (x > maxIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            y = YFromId(id);

            if (y > maxIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            Box = new GeodeticBox2d(Coordinate(x, y, Zoom), Coordinate(x + 1,
                y + 1, Zoom));
        }

        /// <summary>
        /// Creates a Tile
        /// </summary>
        /// <param name="geodetic2d">The coordinate to create the tile at</param>
        /// <param name="zoom">The zoom to create the tile at</param>
        public static TmsTile Create(Geodetic2d geodetic2d, int zoom)
        {
            if (zoom < MinZoom || zoom > MaxZoom)
            {
                throw new ArgumentOutOfRangeException(nameof(zoom), "Zoom " +
                    $"must be between {MinZoom}, {MaxZoom}, was {zoom}");
            }

            int x;
            int y;

            CoordinateToTileXy(geodetic2d, zoom, out x, out y);

            return new TmsTile(x, y, zoom);
        }

        /// <summary>
        /// Returns a tile index in the x axis
        /// </summary>
        /// <param name="longitude">The longitude of the tile index</param>
        /// <param name="zoom">The zoom which the tile index is at</param>
        public static int LongitudeToTileX(double longitude, int zoom)
        {
            if (zoom < MinZoom || zoom > MaxZoom)
            {
                throw new ArgumentOutOfRangeException(nameof(zoom), "Zoom " +
                    $"must be between {MinZoom}, {MaxZoom}, was {zoom}");
            }

            longitude = Mathd.Clamp(longitude, -LongitudeLimit, LongitudeLimit);

            var x = (int) ((longitude + 180) / 360 * Math.Pow(2, zoom));

            // fix for +180 longitude input
            if (Mathd.EpsilonEquals(longitude, 180))
            {
                x--;
            }

            return x;
        }

        /// <summary>
        /// Returns a tile index in the y axis
        /// </summary>
        /// <param name="latitude">The latitude of the tile index</param>
        /// <param name="zoom">The zoom which the tile index is at</param>
        public static int LatitudeToTileY(double latitude, int zoom)
        {
            if (zoom < MinZoom || zoom > MaxZoom)
            {
                throw new ArgumentOutOfRangeException(nameof(zoom), "Zoom " +
                    $"must be between {MinZoom}, {MaxZoom}, was {zoom}");
            }

            latitude = Mathd.Clamp(latitude, -LatitudeLimit, LatitudeLimit);

            var latRads = latitude * Mathd.Deg2Rad;
            return (int)((1 - Math.Log(Math.Tan(latRads) + 1 / 
                Math.Cos(latRads)) / Math.PI) / 2 * Math.Pow(2, zoom));
        }

        /// <summary>
        /// Returns the longitude of a tile index
        /// </summary>
        /// <param name="x">The tile index in the x axis</param>
        /// <param name="zoom">The tile zoom</param>
        public static double TileXToLongitude(int x, int zoom)
        {
            if (zoom < MinZoom || zoom > MaxZoom)
            {
                throw new ArgumentOutOfRangeException(nameof(zoom), "Zoom " +
                    $"must be between {MinZoom}, {MaxZoom}, was {zoom}");
            }

            if (x < 0 || x > MaxXyIndex(zoom))
            {
                throw new ArgumentOutOfRangeException(nameof(x));
            }

            return x / Math.Pow(2, zoom) * 360 - 180;
        }

        /// <summary>
        /// Returns the latitude of a tile index
        /// </summary>
        /// <param name="y">The tile index in the y axis</param>
        /// <param name="zoom">The tile zoom</param>
        public static double TileYToLatitude(int y, int zoom)
        {
            if (zoom < MinZoom || zoom > MaxZoom)
            {
                throw new ArgumentOutOfRangeException(nameof(zoom), "Zoom " +
                    $"must be between {MinZoom}, {MaxZoom}, was {zoom}");
            }

            if (y < 0 || y > MaxXyIndex(y))
            {
                throw new ArgumentOutOfRangeException(nameof(y));
            }

            return Mathd.Rad2Deg * Math.Atan(Math.Sinh(Math.PI * 
                (1 - 2 * y / Math.Pow(2, zoom))));
        }

        /// <summary>
        /// Evaluates the maximum index allowed along the xy axes
        /// </summary>
        /// <param name="zoom">The zoom level to evaluate for</param>
        public static int MaxXyIndex(int zoom)
        {
            return (int)Math.Pow(2, zoom) - 1;
        }

        /// <summary>
        /// Evaluates the zoom for the given box and desired maximum tiles
        /// to cover it
        /// </summary>  
        /// <param name="box">The box to evaluate</param>
        /// <param name="maxTiles">The maximum number of tiles</param>
        public static int ZoomFor(GeodeticBox2d box, int maxTiles)
        {
            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }
            
            // clamp the box bounds to TMS bounds
            box = GeodeticBox2d.ClampAbs(box, LatitudeLimit, LongitudeLimit);

            // obtain the x and y deltas
            var minLong = box.MinimumLongitude;
            var maxLong = box.MaximumLongitude;

            var xmin = (minLong + 180d) / 360d;
            var xmax = (maxLong + 180d) / 360d;

            // y is flipped
            var latRads = box.MaximumLatitude * Mathd.Deg2Rad;
            var ymin = 1 - Math.Log(Math.Tan(latRads) + 1 / Math.Cos(latRads)) / Math.PI;

            latRads = box.MinimumLatitude * Mathd.Deg2Rad;
            var ymax = 1 - Math.Log(Math.Tan(latRads) + 1 / Math.Cos(latRads)) / Math.PI;

            var xd = xmax - xmin;
            var yd = ymax - ymin;

            // obtain the tile count on the x axis by taking the ratio of xd/yd
            // and factoring that against the square box side
            var xtiles = (int)Math.Max(1, xd / yd * Math.Sqrt(maxTiles));

            // 360 * xdelta / longDelta is the inverse of the x index calculations
            // adjusted for index and coordinate delta, must be clamped
            return (int)Mathd.Clamp((int)Math.Log(Math.Ceiling(360d * xtiles /
                (maxLong - minLong)), 2), MinZoom, MaxZoom);
        }

        private static void CoordinateToTileXy(Geodetic2d coordinate, int zoom, 
            out int x, out int y)
        {
            var longitude = Mathd.Clamp(coordinate.Longitude,
                -LongitudeLimit, LongitudeLimit);
            var latitude = Mathd.Clamp(coordinate.Latitude,
                -LatitudeLimit, LatitudeLimit);

            var n = Math.Pow(2, zoom);
            x = (int)((longitude + 180) / 360 * n);
            var latRads = latitude * Mathd.Deg2Rad;
            y = (int)((1 - Math.Log(Math.Tan(latRads) + 1 /
                Math.Cos(latRads)) / Math.PI) / 2 * n);

            // fix for +180 longitude input
            if (Mathd.EpsilonEquals(coordinate.Longitude, 180))
            {
                x--;
            }
        }

        private static Geodetic2d Coordinate(int x, int y, int zoom)
        {
            var n = Math.Pow(2, zoom);
            return new Geodetic2d(Mathd.Rad2Deg * Math.Atan(Math.Sinh(Math.PI -
                2 * Math.PI * y / n)), x / n * 360 - 180);
        }

        private static long TileId(int x, int y, int zoom)
        {
            return ((long)x << 32) + ((long)y << 8) + zoom;
        }

        private static int XFromId(long id)
        {
            return (int)((id >> 32) & IndexMask);
        }

        private static int YFromId(long id)
        {
            return (int)((id >> 8) & IndexMask);
        }

        private static int ZoomFromId(long id)
        {
            return (int)id & ZoomMask;
        }
    }
}