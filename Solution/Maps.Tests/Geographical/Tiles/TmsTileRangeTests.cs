using Maps.Geographical.Tiles;
using NUnit.Framework;

namespace Maps.Tests.Geographical.Tiles
{
    /// <summary>
    /// Series of tests for the TmsTileRange class
    /// </summary>
    [TestFixture]
    internal sealed class TmsTileRangeTests
    {
        /// <summary>
        /// The typical minimum zoom for TMS
        /// </summary>
        private const int TypicalMinZoom = 0;

        /// <summary>
        /// The typical maximum zoom for TMS
        /// </summary>
        private const int TypicalMaxZoom = 21;

        /// <summary>
        /// Tests the count of tiles on each level
        /// </summary>
        [Test]
        public void TestZoomLevelCounts()
        {
            // create top level tile for further creation
            var tileRange = TmsTileRange.Range(
                TestUtilities.BigIngolstadtBox, TypicalMinZoom);

            Assert.AreEqual(0, tileRange.Zoom);
            Assert.AreEqual(0, tileRange.XMin);
            Assert.AreEqual(0, tileRange.XMax);
            Assert.AreEqual(0, tileRange.YMax);
            Assert.AreEqual(0, tileRange.YMin);

            var tiles = TmsTileRange.Tiles(tileRange);

            Assert.AreEqual(1, tiles.Count);

            var rootTile = tiles[0];

            for (var i = TypicalMinZoom + 1; i < TypicalMaxZoom; i++)
            {
                tileRange = TmsTileRange.Range(rootTile.Box, i);

                Assert.AreEqual(i, tileRange.Zoom);
                Assert.AreEqual(0, tileRange.XMin);
                Assert.AreEqual(System.Math.Pow(2, i) - 1, tileRange.XMax);
                Assert.AreEqual(0, tileRange.YMin);
                Assert.AreEqual(System.Math.Pow(2, i) - 1, tileRange.YMax);
            }
        }

        [Test]
        public void TestPaddingForMethod()
        {
            var tileRange = TmsTileRange.Range(
                TestUtilities.BigIngolstadtBox, TypicalMaxZoom);

            var padding = TmsTileRange.Padding(tileRange);
        }
    }
}