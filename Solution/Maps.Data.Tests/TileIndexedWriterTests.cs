using System;
using System.Collections.Generic;
using System.IO;
using Maps.Extensions;
using Maps.Geographical;
using Maps.Geographical.Features;
using Maps.Geographical.Tiles;
using Maps.IO;
//using Maps.OsmSharp.Tests;
using Maps.Tests;
using NUnit.Framework;

namespace Maps.Data.Tests
{
    /// <summary>
    /// A series of tests for the TileIndexedWriter class
    /// </summary>
    [TestFixture]
    internal abstract class TileIndexedWriterTests
    {
        /// <summary>
        /// Creates an IIndexedWriter for creating a TileIndexedWriter
        /// </summary>
        protected abstract IDbWriter<long, byte[]> IndexedWriter();
        
        /// <summary>
        /// Creates a ITiledDataConnection
        /// </summary>
        protected abstract IDbConnection<long, byte[]> IndexedDataConnection();

        private readonly IFeatureProvider _referenceFeatureProvider;

        internal TileIndexedWriterTests()
        {
            //var referenceProvider = new OsmSharpReferenceProvider();
            //_referenceFeatureProvider = referenceProvider.FeatureProvider;

            throw new NotImplementedException("Waiting for compiler implementation for reference dataset");
        }

        /// <summary>
        /// Tests the constructor when given valid parameters
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            throw new NotImplementedException();

            //using (var dataConnection = IndexedDataConnection())
            //{
            //    var writer = IndexedWriter();

            //    dataConnection.Clear();
            //    dataConnection.Connect();

            //    using (var tileWriter = new TileIndexedByteWriter(writer)) { }

            //    dataConnection.Clear();
            //}
        }

        /// <summary>
        /// Tests the constructor when given invalid parameters
        /// </summary>
        [Test]
        public void TestConstructorInvalidParameters()
        {
            throw new NotImplementedException();

            //using (var dataConnection = IndexedDataConnection())
            //{
            //    IIndexedWriter<long, byte[]> writer = null;

            //    dataConnection.Clear();
            //    dataConnection.Connect();

            //    Assert.Throws<ArgumentNullException>(() => {
            //        using (var tileWriter = new TileIndexedByteWriter(writer)) { }
            //    });

            //    dataConnection.Clear();
            //}
        }

        /// <summary>
        /// Tests the ability to write an empty tile
        /// </summary>
        [Test]
        public void TestWriteMethodSingleTileEmpty()
        {
            throw new NotImplementedException();

            //Tile expectedTile;

            //using (var dataConnection = IndexedDataConnection())
            //{
            //    dataConnection.Connect();

            //    using (var tileWriter = dataConnection.CreateTileIndexedWriter())
            //    {
            //        var tileSource = new TmsTileSource();
            //        expectedTile = tileSource.GetForZoom(Geodetic2d.Meridian, 
            //            tileSource.MinZoomLevel);
            //        tileWriter.Write(expectedTile, null);
            //    }
            //}

            //ValidateDeltaSingleTileEmpty();

            //using (var dataConnection = IndexedDataConnection())
            //{
            //    dataConnection.Clear();
            //    dataConnection.Connect();

            //    using (var tileReader = dataConnection.CreateTileIndexedReader())
            //    {
            //        var actualBytes = tileReader.Read(expectedTile);

            //        Assert.IsNull(actualBytes);
            //    }

            //    dataConnection.Clear();
            //}
        }

        /// <summary>
        /// Validates deltas when writing an empty tile
        /// </summary>
        protected abstract void ValidateDeltaSingleTileEmpty();

        /// <summary>
        /// Tests the ability to write a tile and some bytes
        /// </summary>
        [Test]
        public void TestWriteMethodSingleTile()
        {
            throw new NotImplementedException();

            //Tile expectedTile;
            //byte[] expectedBytes;

            //using (var dataConnection = IndexedDataConnection())
            //{
            //    dataConnection.Clear();
            //    dataConnection.Connect();

            //    using (var tileWriter = dataConnection.CreateTileIndexedWriter())
            //    {
            //        var coordinates = new[]
            //        {
            //            Geodetic2d.SouthPole,
            //            Geodetic2d.NorthPole
            //        };

            //        var tileSource = new TmsTileSource();
            //        expectedTile = tileSource.GetForZoom(Geodetic2d.Meridian,
            //                        tileSource.MinZoomLevel);

            //        Feature segment = new Segment(123L.ToGuid(), "",
            //            coordinates, SegmentCategory.Unknown);

            //        using (var memoryStream = new MemoryStream())
            //        {
            //            using (var featureBlockWriter = new FeatureWriter(memoryStream, null))
            //            {
            //                featureBlockWriter.Write(new[] { segment });
            //            }

            //            expectedBytes = memoryStream.ToArray();
            //        }

            //        tileWriter.Write(expectedTile, expectedBytes);
            //    }

            //    // check byte array size
            //    Assert.AreEqual(30, expectedBytes.Length);
            //}

            //ValidateDeltaSingleTile();

            //using (var dataConnection = IndexedDataConnection())
            //{
            //    dataConnection.Connect();

            //    using (var tileReader = dataConnection.CreateTileIndexedReader())
            //    {
            //        var actualBytes = tileReader.Read(expectedTile);

            //        Assert.IsNotNull(actualBytes);
            //        Assert.AreEqual(expectedBytes.Length, actualBytes.Length);

            //        // verify the actual bytes
            //        for (var j = 0; j < actualBytes.Length; j++)
            //        {
            //            var actualByte = actualBytes[j];
            //            var expectedByte = expectedBytes[j];

            //            Assert.AreEqual(expectedByte, actualByte);
            //        }
            //    }

            //    dataConnection.Clear();
            //}
        }

        /// <summary>
        /// Validates deltas when writing a single tile
        /// </summary>
        protected abstract void ValidateDeltaSingleTile();

        /// <summary>
        /// Tests the ability to write a tile and some bytes using a
        /// single tile of real world data
        /// </summary>
        [Test]
        public void TestWriteMethodRealWorldDataSingleTile()
        {
            throw new NotImplementedException();

            //Tile expectedTile;
            //byte[] expectedBytes;

            //using (var dataConnection = IndexedDataConnection())
            //{
            //    dataConnection.Clear();
            //    dataConnection.Connect();

            //    using (var tileWriter = dataConnection.CreateTileIndexedWriter())
            //    {
            //        // use an osmsharp feature source for getting segments for the tile
            //        var featureSource = _referenceFeatureProvider.CreateFeatureSource();
            //        var tileSource = new TmsTileSource();
            //        var box = TestUtilities.BigIngolstadtBox;
            //        var features = featureSource.Get(box);
            //        var nameIds = new long[features.Count];

            //        // use the highest level tile
            //        expectedTile = tileSource.GetForZoom(Geodetic2d.Meridian,
            //            tileSource.MinZoomLevel);

            //        using (var memoryStream = new MemoryStream())
            //        {
            //            using (var blockWriter = new FeatureWriter(memoryStream, null))
            //            {
            //                blockWriter.Write(features);
            //                expectedBytes = memoryStream.ToArray();
            //            }
            //        }

            //        tileWriter.Write(expectedTile, expectedBytes);
            //    }

            //    // check byte array size
            //    Assert.AreEqual(201606L, expectedBytes.Length);
            //}

            //ValidateDeltaRealWorldDataSingleTile();

            //using (var dataConnection = IndexedDataConnection())
            //{
            //    dataConnection.Connect();

            //    using (var tileReader = dataConnection.CreateTileIndexedReader())
            //    {
            //        var actualBytes = tileReader.Read(expectedTile);

            //        Assert.IsNotNull(actualBytes);
            //        Assert.AreEqual(expectedBytes.Length, actualBytes.Length);

            //        // verify the actual bytes
            //        for (var j = 0; j < actualBytes.Length; j++)
            //        {
            //            var actualByte = actualBytes[j];
            //            var expectedByte = expectedBytes[j];

            //            Assert.AreEqual(expectedByte, actualByte);
            //        }
            //    }

            //    dataConnection.Clear();
            //}
        }

        /// <summary>
        /// Validates deltas when using a single tile of real world data
        /// </summary>
        protected abstract void ValidateDeltaRealWorldDataSingleTile();

        /// <summary>
        /// Tests the ability to write a tile and some bytes using real
        /// massive amounts of world data
        /// </summary>
        [Test]
        public void TestWriteMethodMassRealWorldDataMultipleTiles()
        {
            throw new NotImplementedException();

            //IList<Tile> expectedTiles;
            //byte[][] expectedBytes;

            //using (var dataConnection = IndexedDataConnection())
            //{
            //    dataConnection.Clear();
            //    dataConnection.Connect();


            //    using (var tileWriter = dataConnection.CreateTileIndexedWriter())
            //    {
            //        // use an osmsharp feature source for getting segments for the tile
            //        var featureSource = _referenceFeatureProvider.CreateFeatureSource();
            //        var box = new GeodeticBox2d(new Geodetic2d(48.8200, 11.3039),
            //            new Geodetic2d(48.7136, 11.5734));
            //        expectedTiles = new TmsTileSource().GetForZoom(box, 16);
            //        expectedBytes = new byte[expectedTiles.Count][];

            //        var i = 0;
            //        foreach (var tile in expectedTiles)
            //        {
            //            var features = featureSource.Get(tile.Box);
            //            var nameIds = new long[features.Count];

            //            using (var memoryStream = new MemoryStream())
            //            {
            //                using (var blockWriter = new FeatureWriter(memoryStream, null))
            //                {
            //                    blockWriter.Write(features);
            //                    expectedBytes[i] = memoryStream.ToArray();
            //                    tileWriter.Write(tile, expectedBytes[i]);
            //                }
            //            }

            //            i++;
            //        }
            //    }

            //    // check array sizes
            //    Assert.AreEqual(1500, expectedTiles.Count);
            //    Assert.AreEqual(1500, expectedBytes.Length);
            //}

            //// validate the delta
            //ValidateDeltaMassRealWorldDataMultipleTiles();

            //using (var dataConnection = IndexedDataConnection())
            //{
            //    dataConnection.Connect();

            //    using (var tileReader = dataConnection.CreateTileIndexedReader())
            //    {
            //        // verify each tile
            //        for (var i = 0; i < expectedTiles.Count; i++)
            //        {
            //            var actualBytes = tileReader.Read(expectedTiles[i]);

            //            Assert.IsNotNull(actualBytes);
            //            Assert.AreEqual(expectedBytes[i].Length, actualBytes.Length);

            //            // verify the actual bytes
            //            for (var j = 0; j < actualBytes.Length; j++)
            //            {
            //                var actualByte = actualBytes[j];
            //                var expectedByte = expectedBytes[i][j];

            //                Assert.AreEqual(expectedByte, actualByte);
            //            }
            //        }
            //    }

            //    dataConnection.Clear();
            //}
        }

        /// <summary>
        /// Validates deltas when using real massive amounts of world data
        /// </summary>
        protected abstract void ValidateDeltaMassRealWorldDataMultipleTiles();
    }
}