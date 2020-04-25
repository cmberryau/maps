using System.IO;
using Maps.IO.Collections;
using Maps.IO.Features;
using Maps.IO.Places;
using Maps.Tests.IO.Features;
using Maps.Tests.IO.Places;
using NUnit.Framework;

namespace Maps.Tests.IO.Collections
{
    /// <summary>
    /// A series of tests for the BinaryFeatureCollection class
    /// </summary>
    [TestFixture]
    internal sealed class BinaryFeatureCollectionTests
    {
        /// <summary>
        /// Some sample features
        /// </summary>
        internal static BinaryFeature[] SampleFeatures => new BinaryFeature[]
        {
            new BinaryPlace(BinaryPlaceTests.SmallGuidPlace, null),
            new BinarySegment(BinarySegmentTests.SmallGuidSegment, null),
            new BinaryArea(BinaryAreaTests.SmallGuidArea, null),
            new BinaryArea(BinaryAreaTests.SmallGuidAreaInnerCoordinates, null),
            new BinaryArea(BinaryAreaTests.SmallGuidAreaInnerCoordinatesSplit, null),
            new BinaryPlace(BinaryPlaceTests.BigGuidPlace, null),
            new BinarySegment(BinarySegmentTests.BigGuidSegment, null),
            new BinaryArea(BinaryAreaTests.BigGuidArea, null),
            new BinaryArea(BinaryAreaTests.BigGuidAreaInnerCoordinates, null),
            new BinaryArea(BinaryAreaTests.BigGuidAreaInnerCoordinatesSplit, null),
        };

        private static string FullPath => TestUtilities.WorkingDirectory + 
            "BinaryFeatureCollection.bin";

        /// <summary>
        /// Tests the constructor for the BinaryFeatureCollection class, creating 
        /// a tile with segments using a tile and segments parameter
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            Assert.NotNull(new BinaryFeatureCollection(SampleFeatures));
        }

        /// <summary>
        /// Tests that BinaryFeatureCollection can be written to disk and read back
        /// </summary>
        [Test]
        public void TestWriteRead()
        {
            File.Delete(FullPath);

            var expectedFeatures = SampleFeatures;

            using (var file = File.Create(FullPath))
            {
                new BinaryFeatureCollection(expectedFeatures).Serialize(file);
            }

            using (var file = File.OpenRead(FullPath))
            {
                // ensure we can deserialize to a BinaryFeatureCollection
                var actualCollection = BinaryFeatureCollection.Deserialize(file);
                Assert.IsNotNull(actualCollection);

                // confirm file size is what we expect
                Assert.AreEqual(835L, file.Length);
            }

            File.Delete(FullPath);
        }
    }
}