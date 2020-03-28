using System;
using Maps.Geographical;
using Maps.Geographical.Tiles;
using NUnit.Framework;

namespace Maps.Tests.Geographical.Tiles
{
    /// <summary>
    /// Series of tests for the TmsTile class
    /// </summary>
    [TestFixture]
    internal sealed class TmsTileTests
    {
        /// <summary>
        /// Tests the constructor for TmsTile using x, y and zoom
        /// </summary>
        [Test]
        public void TestConstructorXyz()
        {
            var tile = new TmsTile(0, 0, 0);

            Assert.AreEqual(0, tile.x);
            Assert.AreEqual(0, tile.y);
            Assert.AreEqual(0, tile.Zoom);

            tile = new TmsTile(1, 1, 1);

            Assert.AreEqual(1, tile.x);
            Assert.AreEqual(1, tile.y);
            Assert.AreEqual(1, tile.Zoom);
        }

        /// <summary>
        /// Tests the constructor for TmsTile using tile ids
        /// </summary>
        [Test]
        public void TestConstructorId()
        {
            // create a new tile with id 0
            var tile = new TmsTile(0L);

            Assert.AreEqual(0, tile.x);
            Assert.AreEqual(0, tile.y);
            Assert.AreEqual(0, tile.Zoom);

            // create a new tile with an arbitrary value
            var xindex = 83787;
            var yindex = 23421;
            var zoom = TmsTile.MaxZoom;
            var id = ((long)xindex << 32) + ((long)yindex << 8) + zoom;

            tile = new TmsTile(id);

            Assert.AreEqual(xindex, tile.x);
            Assert.AreEqual(yindex, tile.y);
            Assert.AreEqual(zoom, tile.Zoom);
        }

        /// <summary>
        /// Tests the constructor for TmsTile using x, y and zoom
        /// while providing negative values for each
        /// </summary>
        [Test]
        public void TestConstructorXyzNegativeValuesCase()
        {
            TmsTile tile;

            // assert that invalid arguments throw, negative values
            Assert.Throws<ArgumentOutOfRangeException>(
                () => tile = new TmsTile(-1, 0, 0));

            Assert.Throws<ArgumentOutOfRangeException>(
                () => tile = new TmsTile(-1, -1, 0));

            Assert.Throws<ArgumentOutOfRangeException>(
                () => tile = new TmsTile(-1, -1, -1));

            Assert.Throws<ArgumentOutOfRangeException>(
                () => tile = new TmsTile(0, -1, 0));

            Assert.Throws<ArgumentOutOfRangeException>(
                () => tile = new TmsTile(0, -1, -1));

            Assert.Throws<ArgumentOutOfRangeException>(
                () => tile = new TmsTile(0, 0, -1));

            Assert.Throws<ArgumentOutOfRangeException>(
                () => tile = new TmsTile(-1, 0, -1));
        }

        /// <summary>
        /// Tests the constructor for TmsTile using tile ids while
        /// providing a negative id value
        /// </summary>
        [Test]
        public void TestConstructorIdNegativeValueCase()
        {
            TmsTile tile;

            Assert.Throws<ArgumentOutOfRangeException>(() => tile = new TmsTile(-1L));
        }

        /// <summary>
        /// Tests the constructor for TmsTile using x, y and zoom
        /// while providing invalid values for each
        /// </summary>
        [Test]
        public void TestConstructorXyzInvalidIndicesCase()
        {
            TmsTile tile;

            var zoom = TmsTile.MinZoom;

            // out of range xy indices for given zooms by one 
            Assert.Throws<ArgumentOutOfRangeException>(
                () => tile = new TmsTile(1, 1, zoom));

            Assert.Throws<ArgumentOutOfRangeException>(
                () => tile = new TmsTile(0, 1, zoom));

            Assert.Throws<ArgumentOutOfRangeException>(
                () => tile = new TmsTile(1, 0, zoom));

            zoom = TmsTile.MaxZoom;
            var index = (int) System.Math.Pow(2, zoom);

            Assert.Throws<ArgumentOutOfRangeException>(
                () => tile = new TmsTile(index, index, zoom));

            Assert.Throws<ArgumentOutOfRangeException>(
                () => tile = new TmsTile(0, index, zoom));

            Assert.Throws<ArgumentOutOfRangeException>(
                () => tile = new TmsTile(index, 0, zoom));
        }

        /// <summary>
        /// Tests the constructor for TmsTile using tile ids while
        /// providing a invalid id value
        /// </summary>
        [Test]
        public void TestConstructorIdInvalidValueCase()
        {
            // out of range for id by one
            TmsTile tile;
            var zoom = TmsTile.MaxZoom;
            var index = (int) System.Math.Pow(2, zoom);
            var id = ((long)index << 32) + ((long)index << 8) + zoom;

            Assert.Throws<ArgumentOutOfRangeException>(() => tile = new TmsTile(id));
        }

        /// <summary>
        /// Tests the generated Id for the TmsTile with an arbitrary case
        /// </summary>
        [Test]
        public void TestIdGeneration()
        {
            var zoom = TmsTile.MaxZoom;
            var tile = new TmsTile(83787, 23421, zoom);
            var id = ((long)83787 << 32) + ((long)23421 << 8) + zoom;

            Assert.AreEqual(id, tile.Id);

            var actualZoom = (int)tile.Id & 0xFF;
            var actualX = (int)((tile.Id >> 32) & 0xFFFFFF);
            var actualY = (int)((tile.Id >> 8) & 0xFFFFFF);

            Assert.AreEqual(zoom, actualZoom);
            Assert.AreEqual(83787, actualX);
            Assert.AreEqual(23421, actualY);
        }

        /// <summary>
        /// Tests the generated Id for the TmsTile at the maxmimum case
        /// </summary>
        [Test]
        public void TestIdGenerationMaxCase()
        {
            var zoom = TmsTile.MaxZoom;
            var index = (int) System.Math.Pow(2, zoom) - 1;
            var tile = new TmsTile(index, index, zoom);
            var id = ((long)index << 32) + ((long)index << 8) + zoom;

            Assert.AreEqual(id, tile.Id);

            var actualZoom = tile.Id & 0xFF;
            var actualX = (int)((tile.Id >> 32) & 0xFFFFFF);
            var actualY = (int)((tile.Id >> 8) & 0xFFFFFF);

            Assert.AreEqual(zoom, actualZoom);
            Assert.AreEqual(index, actualX);
            Assert.AreEqual(index, actualY);
        }

        /// <summary>
        /// Tests the generated Id for the TmsTile at the minimum case
        /// </summary>
        [Test]
        public void TestIdGenerationMinCase()
        {
            var zoom = TmsTile.MinZoom;
            var index = 0;
            var tile = new TmsTile(index, index, zoom);
            var id = ((long)index << 32) + ((long)index << 8) + zoom;

            Assert.AreEqual(id, tile.Id);

            var actualZoom = tile.Id & 0xFF;
            var actualX = (int)((tile.Id >> 32) & 0xFFFFFF);
            var actualY = (int)((tile.Id >> 8) & 0xFFFFFF);

            Assert.AreEqual(zoom, actualZoom);
            Assert.AreEqual(index, actualX);
            Assert.AreEqual(index, actualY);
        }

        /// <summary>
        /// Tests the method which evaluates longitude values and outputs 
        /// tile x indices at the maximum zoom case
        /// </summary>
        [Test]
        public void TestLongitudeToTileXMaxZoomCase()
        {
            var zoom = TmsTile.MaxZoom;
            var tilex = TmsTile.LongitudeToTileX(TmsTile.LongitudeLimit, zoom);

            Assert.AreEqual((int)System.Math.Pow(2, zoom) - 1, tilex);

            tilex = TmsTile.LongitudeToTileX(-TmsTile.LongitudeLimit, zoom);

            Assert.AreEqual(0, tilex);
        }

        /// <summary>
        /// Tests the method which evaluates longitude values and outputs 
        /// tile x indices at the minimum zoom case
        /// </summary>
        [Test]
        public void TestLongitudeToTileXMinCase()
        {
            var zoom = TmsTile.MinZoom;
            var tilex = TmsTile.LongitudeToTileX(TmsTile.LongitudeLimit, zoom);

            Assert.AreEqual(0, tilex);

            tilex = TmsTile.LongitudeToTileX(-TmsTile.LongitudeLimit, zoom);

            Assert.AreEqual(0, tilex);
        }

        /// <summary>
        /// Tests the method which evaluates latitude values and outputs 
        /// tile y indices at the maxmimum zoom case
        /// </summary>
        [Test]
        public void TestLatitudeToTileYMaxZoomCase()
        {
            var zoom = TmsTile.MaxZoom;
            var tiley = TmsTile.LatitudeToTileY(TmsTile.LatitudeLimit, zoom);

            Assert.AreEqual(0, tiley);

            tiley = TmsTile.LatitudeToTileY(-TmsTile.LatitudeLimit, zoom);

            Assert.AreEqual((int)System.Math.Pow(2, zoom) - 1, tiley);
        }

        /// <summary>
        /// Tests the method which evaluates latitude values and outputs 
        /// tile y indices at the minimum zoom case
        /// </summary>
        [Test]
        public void TestLatitudeToTileYMinZoomCase()
        {
            var zoom = TmsTile.MinZoom;
            var tiley = TmsTile.LatitudeToTileY(TmsTile.LatitudeLimit, zoom);

            Assert.AreEqual(0, tiley);

            tiley = TmsTile.LatitudeToTileY(-TmsTile.LatitudeLimit, zoom);

            Assert.AreEqual(0, tiley);
        }

        /// <summary>
        /// Tests the ZoomFor method
        /// </summary>
        [Test]
        public void TestZoomForMethod()
        {
            var box = GeodeticBox2d.World;
            var maxExpectedTiles = 4;

            var zoom = TmsTile.ZoomFor(box, maxExpectedTiles);
            var range = TmsTileRange.Range(box, zoom);

            Assert.GreaterOrEqual(maxExpectedTiles, range.TileCount);

            box = GeodeticBox2d.World;
            maxExpectedTiles = 16;

            zoom = TmsTile.ZoomFor(box, maxExpectedTiles);
            range = TmsTileRange.Range(box, zoom);

            Assert.GreaterOrEqual(maxExpectedTiles, range.TileCount);

            box = GeodeticBox2d.World;
            maxExpectedTiles = 64;

            zoom = TmsTile.ZoomFor(box, maxExpectedTiles);
            range = TmsTileRange.Range(box, zoom);

            Assert.GreaterOrEqual(maxExpectedTiles, range.TileCount);

            box = TestUtilities.BigIngolstadtBox;
            maxExpectedTiles = 4;

            zoom = TmsTile.ZoomFor(box, maxExpectedTiles);
            range = TmsTileRange.Range(box, zoom);

            Assert.GreaterOrEqual(maxExpectedTiles, range.TileCount);

            box = TestUtilities.BigIngolstadtBox;
            maxExpectedTiles = 16;

            zoom = TmsTile.ZoomFor(box, maxExpectedTiles);
            range = TmsTileRange.Range(box, zoom);

            Assert.GreaterOrEqual(maxExpectedTiles, range.TileCount);

            box = TestUtilities.BigIngolstadtBox;
            maxExpectedTiles = 64;

            zoom = TmsTile.ZoomFor(box, maxExpectedTiles);
            range = TmsTileRange.Range(box, zoom);

            Assert.GreaterOrEqual(maxExpectedTiles, range.TileCount);
        }

        /// <summary>
        /// Tests for the SubTiles property
        /// </summary>
        [Test]
        public void TestSubTilesProperty()
        {
            Tile tile = TmsTile.Create(Geodetic2d.Meridian, TmsTile.MinZoom);
            var subTiles = tile.SubTiles;

            Assert.IsNotNull(subTiles);
            Assert.IsNotEmpty(subTiles);
            Assert.AreEqual(4, subTiles.Count);

            for (var i = 0; i < subTiles.Count; ++i)
            {
                Assert.IsTrue(tile.Box.Contains(subTiles[i].Box));
            }

            for (var i = 0; i < TmsTile.MaxZoom - 2; ++i)
            {
                tile = subTiles[0];
                subTiles = tile.SubTiles;

                Assert.IsNotNull(subTiles);
                Assert.IsNotEmpty(subTiles);
                Assert.AreEqual(4, subTiles.Count);

                for (var j = 0; j < subTiles.Count; ++j)
                {
                    Assert.IsTrue(tile.Box.Contains(subTiles[j].Box, Mathd.EpsilonE15));
                }
            }
        }
    }
}