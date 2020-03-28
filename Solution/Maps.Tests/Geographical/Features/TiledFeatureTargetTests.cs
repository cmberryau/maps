using System;
using System.Collections.Generic;
using Maps.Extensions;
using Maps.Geographical;
using Maps.Geographical.Features;
using Maps.Geographical.Tiles;
using NUnit.Framework;

namespace Maps.Tests.Geographical.Features
{
    /// <summary>
    /// An series of minimum tests for ITiledFeatureTarget implementers
    /// </summary>
    [TestFixture]
    public abstract class TiledFeatureTargetTests
    {
        /// <summary>
        /// Creates a fresh ITiledFeatureTarget
        /// </summary>
        protected abstract ITiledFeatureTarget CreateTarget();

        /// <summary>
        /// Creates a ITiledFeatureTarget
        /// </summary>
        protected abstract ITiledFeatureSource CreateSource();

        /// <summary>
        /// Creates a reference feature provider to test against
        /// </summary>
        protected abstract IFeatureProvider ReferenceFeatureProvider();

        /// <summary>
        /// Tests the constructor for the ITiledFeatureTarget implementer
        /// </summary>
        [Test]
        public abstract void TestConstructor();

        /// <summary>
        /// Tests the constructor for the ITiledFeatureTarget implementer
        /// </summary>
        [Test]
        public abstract void TestConstructorInvalidParameters();

        /// <summary>
        /// Tests the IFeatureTarget TilingStrategy property
        /// </summary>
        [Test]
        public void TestTilingStrategyProperty()
        {
            using (var target = CreateTarget())
            {
                Assert.DoesNotThrow(() =>
                {
                    var strategy = target.TileSource;
                });
            }
        }

        /// <summary>
        /// Tests the ability of the ITiledFeatureTarget to write features given
        /// a tile
        /// </summary>
        [Test]
        public void TestTileWriteMethod()
        {
            var expectedTile = new TmsTile(0L);
            var expectedCoordiantes = new[]
            {
                expectedTile.Box.Clamp(Geodetic2d.SouthPole),
                expectedTile.Box.Clamp(Geodetic2d.NorthPole)
            };

            using (var target = CreateTarget())
            {
                var segment = new Segment(123L.ToGuid(), "",
                    expectedCoordiantes, SegmentCategory.Unknown);

                target.Write(expectedTile, new[]
                {
                    segment
                });
            }

            using (var source = CreateSource())
            {
                var features = source.Get(expectedTile);

                Assert.IsNotNull(features);
                Assert.AreEqual(1, features.Count);

                var segment = features[0] as Segment;

                Assert.IsNotNull(segment);

                TestUtilities.AssertThatGeodetic2dsAreEqual(
                    expectedCoordiantes[0], segment.LineStrip[0]);
                TestUtilities.AssertThatGeodetic2dsAreEqual(
                    expectedCoordiantes[1], segment.LineStrip[1]);
            }
        }

        /// <summary>
        /// Tests that the tile write method throws an exception when
        /// given a non-empty feature array with a null entry
        /// </summary>
        [Test]
        public void TestTileWriteMethodNullFeature()
        {
            var expectedTile = new TmsTile(0L);
            var expectedCoordiantes = new[]
            {
                expectedTile.Box.Clamp(Geodetic2d.SouthPole),
                expectedTile.Box.Clamp(Geodetic2d.NorthPole)
            };
            var segment = new Segment(123L.ToGuid(), "",
                expectedCoordiantes, SegmentCategory.Unknown);
            var features = new Feature[] { segment, null };

            using (var target = CreateTarget())
            {
                Assert.Throws<ArgumentException>(() => 
                target.Write(expectedTile, features));
            }
        }

        /// <summary>
        /// Tests that the tile write method throws an exception when
        /// given a null tile
        /// </summary>
        [Test]
        public void TestTileWriteMethodNullTile()
        {
            Tile tile = null;

            var expectedCoordiantes = new[]
            {
                Geodetic2d.SouthPole,
                Geodetic2d.NorthPole
            };
            var segment = new Segment(123L.ToGuid(), "",
                expectedCoordiantes, SegmentCategory.Unknown);
            var features = new Feature[] { segment, null };

            using (var target = CreateTarget())
            {
                Assert.Throws<ArgumentNullException>(() =>
                target.Write(tile, features));
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureTarget to write features given
        /// a tile with a null feature array
        /// </summary>
        [Test]
        public void TestTileWriteMethodNullFeatureArray()
        {
            var tile = new TmsTile(0L);

            using (var target = CreateTarget())
            {
                target.Write(tile, null);
            }

            using (var source = CreateSource())
            {
                var features = source.Get(tile);

                Assert.IsNotNull(features);
                Assert.IsEmpty(features);
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureTarget to write features given
        /// a tile with an empty feature array
        /// </summary>
        [Test]
        public void TestTileWriteMethodEmptyFeatureArray()
        {
            var tile = new TmsTile(0L);

            using (var target = CreateTarget())
            {
                target.Write(tile, new List<Feature>());
            }

            using (var source = CreateSource())
            {
                var features = source.Get(tile);

                Assert.IsNotNull(features);
                Assert.IsEmpty(features);
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureTarget to write features given
        /// tiles with a null feature array
        /// </summary>
        [Test]
        public void TestTileWriteMethodMultipleTilesNullFeatureArray()
        {
            var expectedCoordiantes = new[]
            {
                Geodetic2d.SouthPole,
                Geodetic2d.NorthPole
            };

            var segment = new Segment(123L.ToGuid(), "",
                expectedCoordiantes, SegmentCategory.Unknown);

            var expectedTiles = new []
            {
                new TmsTile(0L),
                new TmsTile(1L),
            };

            var expectedFeatures = new Feature[][]
            {
                new []
                {
                    segment
                },
                null
            };

            using (var target = CreateTarget())
            {
                target.Write(expectedTiles, expectedFeatures);
            }

            using (var source = CreateSource())
            {
                var features = source.Get(expectedTiles);

                Assert.IsNotNull(features);
                Assert.IsNotEmpty(features);

                Assert.IsNotNull(features[0]);
                Assert.IsNotEmpty(features[0]);

                Assert.IsNotNull(features[1]);
                Assert.IsEmpty(features[1]);
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureTarget to write features given
        /// tiles with a empty feature array
        /// </summary>
        [Test]
        public void TestTileWriteMethodMultipleTileEmptyFeatureArray()
        {
            var expectedCoordiantes = new[]
            {
                Geodetic2d.SouthPole,
                Geodetic2d.NorthPole
            };

            var segment = new Segment(123L.ToGuid(), "",
                expectedCoordiantes, SegmentCategory.Unknown);

            var expectedTiles = new[]
            {
                new TmsTile(0L),
                new TmsTile(1L),
            };

            var expectedFeatures = new Feature[][]
            {
                new []
                {
                    segment
                },
                new Feature[]
                {
                    
                }, 
            };

            using (var target = CreateTarget())
            {
                target.Write(expectedTiles, expectedFeatures);
            }

            using (var source = CreateSource())
            {
                var features = source.Get(expectedTiles);

                Assert.IsNotNull(features);
                Assert.IsNotEmpty(features);

                Assert.IsNotNull(features[0]);
                Assert.IsNotEmpty(features[0]);

                Assert.IsNotNull(features[1]);
                Assert.IsEmpty(features[1]);
            }
        }

        /// <summary>
        /// Tests the ability of the IFeatureTarget to write features given
        /// multiple tiles and reference data
        /// </summary>
        [Test]
        public void TestTileWriteMethodReferenceData()
        {
            IList<Tile> expectedTiles;
            int[] expectedFeatureCount;

            using (var target = CreateTarget())
            {
                // get the reference feature source
                var referenceFeatureSource = ReferenceFeatureProvider().FeatureSource();
                var box = new GeodeticBox2d(new Geodetic2d(48.8200, 11.3039),
                                            new Geodetic2d(48.7136, 11.5734));
                expectedTiles = target.TileSource.GetForZoom(box, 16, false);
                expectedFeatureCount = new int[expectedTiles.Count];

                // write the tiles and their features to the target
                for (var i = 0; i < expectedTiles.Count; i++)
                {
                    var features = referenceFeatureSource.Get(expectedTiles[i].Box);
                    expectedFeatureCount[i] = features.Count;
                    target.Write(expectedTiles[i], features);
                }
            }

            using (var source = CreateSource())
            {
                // read the tiles and their features back
                for (var i = 0; i < expectedTiles.Count; i++)
                {
                    var features = source.Get(expectedTiles[i]);

                    // it is possible to have more features on a tile after
                    Assert.GreaterOrEqual(features.Count, expectedFeatureCount[i]);
                }
            }
        }
    }
}