using System;
using Maps.Geographical;
using Maps.Geographical.Features;
using Maps.Geographical.Tiles;
using NUnit.Framework;

namespace Maps.Tests.Geographical.Features
{
    [TestFixture]
    public abstract class TiledFeatureSourceTests
    {
        /// <summary>
        /// Creates a ITiledFeatureSource with known reference data
        /// </summary>
        protected abstract ITiledFeatureSource CreateReferenceTiledSource();

        /// <summary>
        /// Creates an empty ITiledFeatureSource
        /// </summary>
        protected abstract ITiledFeatureSource CreateEmptyTiledSource();

        /// <summary>
        /// Tests the constructor for the ITiledFeatureSource implementer
        /// </summary>
        [Test]
        public abstract void TestConstructor();

        /// <summary>
        /// Tests the constructor for the ITiledFeatureSource implementer
        /// </summary>
        [Test]
        public abstract void TestConstructorInvalidParameters();

        /// <summary>
        /// Tests the IFeatureSource TilingStrategy property
        /// </summary>
        [Test]
        public void TestTilingSourceProperty()
        {
            using (var source = CreateEmptyTiledSource())
            {
                Assert.DoesNotThrow(() =>
                {
                    var strategy = source.TileSource;
                });
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureSource to query features given
        /// a tile
        /// </summary>
        [Test]
        public void TestTileGetMethod()
        {
            using (var source = CreateReferenceTiledSource())
            {
                var tile = source.TileSource.Get(TestUtilities.Ingolstadt);
                var features = source.Get(tile);

                Assert.IsNotNull(features);
                Assert.IsNotEmpty(features);

                for (var i = 0; i < features.Count; i++)
                {
                    Assert.IsNotNull(features[i]);
                }
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureSource to query features given
        /// a tile when given invalid parameters
        /// </summary>
        [Test]
        public void TestTileGetMethodInvalidParameters()
        {
            using (var source = CreateReferenceTiledSource())
            {
                Tile tile = null;

                Assert.Throws<ArgumentNullException>(() =>
                {
                    var features = source.Get(tile);
                });
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureSource to query features
        /// asyncronously given a tile
        /// </summary>
        [Test]
        public void TestTileGetAsyncMethod()
        {
            using (var source = CreateReferenceTiledSource())
            {
                var tile = source.TileSource.Get(TestUtilities.Ingolstadt);
                var featuresAsync = source.GetAsync(tile);

                featuresAsync.Wait();

                var features = featuresAsync.Result;

                Assert.IsNotNull(features);
                Assert.IsNotEmpty(features);

                for (var i = 0; i < features.Count; i++)
                {
                    Assert.IsNotNull(features[i]);
                }
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureSource to query features
        /// asyncronously given a tile when given invalid parameters
        /// </summary>
        [Test]
        public void TestTileGetAsyncMethodInvalidParameters()
        {
            using (var source = CreateReferenceTiledSource())
            {
                Tile tile = null;

                Assert.Throws<ArgumentNullException>(() =>
                {
                    var featuresAsync = source.GetAsync(tile);
                });
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureSource to query features given
        /// a tile when empty
        /// </summary>
        [Test]
        public void TestTileGetMethodEmptySource()
        {
            using (var source = CreateEmptyTiledSource())
            {
                var tile = source.TileSource.Get(TestUtilities.Ingolstadt);
                var features = source.Get(tile);

                Assert.IsNotNull(features);
                Assert.IsEmpty(features);
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureSource to query features
        /// asyncronously given a tile when empty
        /// </summary>
        [Test]
        public void TestTileGetAsyncMethodEmptySource()
        {
            using (var source = CreateEmptyTiledSource())
            {
                var tile = source.TileSource.Get(TestUtilities.Ingolstadt);
                var featuresAsync = source.GetAsync(tile);

                featuresAsync.Wait();

                Assert.IsNotNull(featuresAsync.Result);
                Assert.IsEmpty(featuresAsync.Result);
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureSource to query features given
        /// multiple tiles
        /// </summary>
        [Test]
        public void TestMultipleTilesGetMethod()
        {
            using (var source = CreateReferenceTiledSource())
            {
                var tiles = source.TileSource.Get(TestUtilities.BigIngolstadtBox);
                var featureLists = source.Get(tiles);

                Assert.IsNotNull(featureLists);
                Assert.IsNotEmpty(featureLists);

                Assert.AreEqual(tiles.Count, featureLists.Count);

                for (var i = 0; i < featureLists.Count; i++)
                {
                    Assert.IsNotNull(featureLists[i]);

                    for (var j = 0; j < featureLists[i].Count; j++)
                    {
                        Assert.IsNotNull(featureLists[i][j]);
                    }
                }
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureSource to query features given
        /// multiple tiles when given invalid parameters
        /// </summary>
        [Test]
        public void TestMultipleTilesGetMethodInvalidParameters()
        {
            // should throw on null tile array
            using (var source = CreateReferenceTiledSource())
            {
                Tile[] tiles = null;

                Assert.Throws<ArgumentNullException>(() =>
                {
                    var features = source.Get(tiles);
                });
            }

            // should throw on null entry in tile array
            using (var source = CreateReferenceTiledSource())
            {
                var tiles = new Tile[]
                {
                    source.TileSource.Get(Geodetic2d.Meridian),
                    null
                };

                Assert.Throws<ArgumentException>(() =>
                {
                    var features = source.Get(tiles);
                });
            }

            // lets try it at the front of the array
            using (var source = CreateReferenceTiledSource())
            {
                var tiles = new Tile[]
                {
                    null,
                    source.TileSource.Get(Geodetic2d.Meridian),
                };

                Assert.Throws<ArgumentException>(() =>
                {
                    var features = source.Get(tiles);
                });
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureSource to query features
        /// asyncronously given multiple tiles
        /// </summary>
        [Test]
        public void TestMultipleTilesGetAsyncMethod()
        {
            using (var source = CreateReferenceTiledSource())
            {
                var tiles = source.TileSource.Get(TestUtilities.BigIngolstadtBox);
                var featureListsAsync = source.GetAsync(tiles);

                featureListsAsync.Wait();

                var featureLists = featureListsAsync.Result;

                Assert.IsNotNull(featureLists);
                Assert.IsNotEmpty(featureLists);

                Assert.AreEqual(tiles.Count, featureLists.Count);

                for (var i = 0; i < featureLists.Count; i++)
                {
                    Assert.IsNotNull(featureLists[i]);

                    for (var j = 0; j < featureLists[i].Count; j++)
                    {
                        Assert.IsNotNull(featureLists[i][j]);
                    }
                }
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureSource to query features
        /// asyncronously given multiple tiles
        /// </summary>
        [Test]
        public void TestMultipleTilesGetAsyncMethodInvalidParameters()
        {
            // should throw on null tile array
            using (var source = CreateReferenceTiledSource())
            {
                Tile[] tiles = null;

                Assert.Throws<ArgumentNullException>(() =>
                {
                    var featuresAsync = source.GetAsync(tiles);
                });
            }

            // should throw on null entry in tile array
            using (var source = CreateReferenceTiledSource())
            {
                var tiles = new Tile[]
                {
                    new TmsTile(0),
                    null
                };

                Assert.Throws<ArgumentException>(() =>
                {
                    var featuresAsync = source.GetAsync(tiles);
                });
            }

            // lets try it at the front of the array
            using (var source = CreateReferenceTiledSource())
            {
                var tiles = new Tile[]
                {
                    null,
                    new TmsTile(0),
                };

                Assert.Throws<ArgumentException>(() =>
                {
                    var featuresAsync = source.GetAsync(tiles);
                });
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureSource to query features given
        /// multiple tiles when empty
        /// </summary>
        [Test]
        public void TestMultipleTilesGetMethodEmptySource()
        {
            using (var source = CreateEmptyTiledSource())
            {
                var tiles = source.TileSource.Get(TestUtilities.BigIngolstadtBox);
                var featureLists = source.Get(tiles);

                Assert.IsNotNull(featureLists);
                Assert.IsNotEmpty(featureLists);

                for (var i = 0; i < featureLists.Count; i++)
                {
                    Assert.IsNotNull(featureLists[i]);
                    Assert.IsEmpty(featureLists[i]);
                }
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureSource to query features
        /// asyncronously given multiple tiles when empty
        /// </summary>
        [Test]
        public void TestMultipleTilesGetAsyncMethodEmptySource()
        {
            using (var source = CreateEmptyTiledSource())
            {
                var tiles = source.TileSource.Get(TestUtilities.BigIngolstadtBox);
                var featureListsAsync = source.GetAsync(tiles);

                featureListsAsync.Wait();

                var featureLists = featureListsAsync.Result;

                Assert.IsNotNull(featureLists);
                Assert.IsNotEmpty(featureLists);

                for (var i = 0; i < featureLists.Count; i++)
                {
                    Assert.IsNotNull(featureLists[i]);
                    Assert.IsEmpty(featureLists[i]);
                }
            }
        }
    }
}