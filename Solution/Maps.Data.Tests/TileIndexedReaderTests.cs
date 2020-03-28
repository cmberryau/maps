using System;
using Maps.Geographical;
using Maps.Geographical.Tiles;
using NUnit.Framework;

namespace Maps.Data.Tests
{
    /// <summary>
    /// A series of tests for TileIndexedReader
    /// </summary>
    internal abstract class TileIndexedReaderTests
    {
        /// <summary>
        /// Creates an IIndexedReader for creating a TileIndexedReader
        /// </summary>
        protected abstract IDbReader<long, byte[]> IndexedReader();

        /// <summary>
        /// Creates a ITiledDataConnection
        /// </summary>
        protected abstract IDbConnection<long, byte[]> IndexedDataConnection();

        /// <summary>
        /// Tests the constructor when given valid parameters
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            throw new NotImplementedException();

            //using (var dataConnection = IndexedDataConnection())
            //{
            //    dataConnection.Clear();
            //    dataConnection.Connect();

            //    using (var tileReader = new TileIndexedByteReader(IndexedReader())) { }

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
            //    IIndexedReader<long, byte[]> reader = null;

            //    dataConnection.Clear();
            //    dataConnection.Connect();

            //    Assert.Throws<ArgumentNullException>(() => {
            //        using (var tileReader = new TileIndexedByteReader(reader)) { }
            //    });

            //    dataConnection.Clear();
            //}
        }

        /// <summary>
        /// Tests the reaction to reading an empty db
        /// </summary>
        [Test]
        public void TestReadMethodEmptyDb()
        {
            throw new NotImplementedException();

            //using (var dataConnection = IndexedDataConnection())
            //{
            //    dataConnection.Clear();
            //    dataConnection.Connect();

            //    using (var tileReader = new TileIndexedByteReader(IndexedReader()))
            //    {
            //        var tileSource = new TmsTileSource();
            //        var tile = tileSource.GetForZoom(Geodetic2d.Meridian, 
            //            tileSource.MinZoomLevel);
            //        var data = tileReader.Read(tile);

            //        Assert.IsNull(data);
            //    }

            //    dataConnection.Clear();
            //}
        }
    }
}