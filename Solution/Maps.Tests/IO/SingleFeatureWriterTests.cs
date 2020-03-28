using System;
using System.IO;
using Maps.Geographical.Features;
using Maps.Geographical.Places;
using Maps.IO;
using Maps.Tests.IO.Features;
using Maps.Tests.IO.Places;
using NUnit.Framework;

namespace Maps.Tests.IO
{
    /// <summary>
    /// Series of tests for the SingleFeatureStreamWriter class
    /// </summary>
    [TestFixture]
    internal sealed class SingleFeatureWriterTests
    {
        /// <summary>
        /// Some sample features
        /// </summary>
        internal static Feature[] SampleFeatures => new Feature[]
        {
            BinaryPlaceTests.SmallGuidPlace,
            BinarySegmentTests.SmallGuidSegment,
            BinaryAreaTests.SmallGuidArea,
            BinaryAreaTests.SmallGuidAreaInnerCoordinates,
            BinaryAreaTests.SmallGuidAreaInnerCoordinatesSplit,
            BinaryPlaceTests.BigGuidPlace,
            BinarySegmentTests.BigGuidSegment,
            BinaryAreaTests.BigGuidArea,
            BinaryAreaTests.BigGuidAreaInnerCoordinates,
            BinaryAreaTests.BigGuidAreaInnerCoordinatesSplit,
        };

        private static string FullPath => TestUtilities.WorkingDirectory +
            "FeatureWriter.bin";

        /// <summary>
        /// Tests the constructor of FeatureWriter
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var featureWriter = new SingleFeatureWriter(memoryStream))
                {
                    Assert.IsNotNull(featureWriter);
                }
            }   
        }

        /// <summary>
        /// Tests the constructor of FeatureWriter when given invalid
        /// parameters
        /// </summary>
        [Test]
        public void TestConstructorInvalidParameters()
        {
            Stream nullStream = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                var featureWriter = new SingleFeatureWriter(nullStream);
            });

            Stream closedStream = new MemoryStream();
            closedStream.Close();

            Assert.Throws<ArgumentException>(() =>
            {
                var featureWriter = new SingleFeatureWriter(closedStream);
            });
        }

        /// <summary>
        /// Tests the ability to write a single place
        /// </summary>
        [Test]
        public void TestWriteMethodPlace()
        {
            File.Delete(FullPath);

            var expectedPlace = BinaryPlaceTests.SmallGuidPlace;

            using (var file = File.Create(FullPath))
            {
                using (var writer = new SingleFeatureWriter(file))
                {
                    writer.Write(expectedPlace, null);

                    // confirm file size is what we expect
                    Assert.AreEqual(16L, file.Length);
                }
            }

            using (var file = File.OpenRead(FullPath))
            {
                using (var reader = new SingleFeatureReader(file))
                {
                    var count = 0;
                    while (reader.Read())
                    {
                        count++;
                    }

                    // validate that we only had a single read
                    Assert.AreEqual(1, count);

                    // ensure that we get a Feature instance back
                    var actualFeature = reader.Current;
                    Assert.IsNotNull(actualFeature);

                    // ensure it can be casted to a Place
                    var actualPlace = actualFeature as Place;
                    Assert.IsNotNull(actualPlace);

                    // ensure all details of the Place instance
                    Assert.AreEqual(expectedPlace.Guid, actualPlace.Guid);
                    Assert.AreEqual(expectedPlace.Name, actualPlace.Name);
                    Assert.AreEqual(expectedPlace.Category.Root,
                        actualPlace.Category.Root);
                }
            }

            File.Delete(FullPath);
        }

        /// <summary>
        /// Tests the ability to write a single segment
        /// </summary>
        [Test]
        public void TestWriteMethodSegment()
        {
            File.Delete(FullPath);

            var expectedSegment = BinarySegmentTests.SmallGuidSegment;

            using (var file = File.Create(FullPath))
            {
                using (var writer = new SingleFeatureWriter(file))
                {
                    writer.Write(expectedSegment, null);

                    // confirm file size is what we expect
                    Assert.AreEqual(29L, file.Length);
                }
            }

            using (var file = File.OpenRead(FullPath))
            {
                using (var reader = new SingleFeatureReader(file))
                {
                    var count = 0;
                    while (reader.Read())
                    {
                        count++;
                    }

                    // validate that we only had a single read
                    Assert.AreEqual(1, count);

                    // ensure that we get a Feature instance back
                    var actualFeature = reader.Current;
                    Assert.IsNotNull(actualFeature);

                    // ensure it can be casted to a Segment
                    var actualSegment = actualFeature as Segment;
                    Assert.IsNotNull(actualSegment);

                    // ensure all details of the Segment instance
                    Assert.AreEqual(expectedSegment.Guid, actualSegment.Guid);
                    Assert.AreEqual(expectedSegment.Name, actualSegment.Name);
                }
            }

            File.Delete(FullPath);
        }

        /// <summary>
        /// Tests the ability to write a single area
        /// </summary>
        [Test]
        public void TestWriteMethodArea()
        {
            File.Delete(FullPath);

            var expectedArea = BinaryAreaTests.SmallGuidArea;

            using (var file = File.Create(FullPath))
            {
                using (var writer = new SingleFeatureWriter(file))
                {
                    writer.Write(expectedArea, null);

                    // confirm file size is what we expect
                    Assert.AreEqual(49L, file.Length);
                }
            }

            using (var file = File.OpenRead(FullPath))
            {
                using (var reader = new SingleFeatureReader(file))
                {
                    var count = 0;
                    while (reader.Read())
                    {
                        count++;
                    }

                    // validate that we only had a single read
                    Assert.AreEqual(1, count);

                    // ensure that we get a Feature instance back
                    var actualFeature = reader.Current;
                    Assert.IsNotNull(actualFeature);

                    // ensure it can be casted to a Area
                    var actualArea = actualFeature as Area;
                    Assert.IsNotNull(actualArea);

                    // ensure all details of the Area instance
                    Assert.AreEqual(expectedArea.Guid, actualArea.Guid);
                }
            }

            File.Delete(FullPath);
        }

        /// <summary>
        /// Tests the ability to write multiple features
        /// </summary>
        [Test]
        public void TestWriteMethodMultipleFeatures()
        {
            File.Delete(FullPath);

            var expectedFeatures = SampleFeatures;

            // write features seperately
            using (var file = File.Create(FullPath))
            {
                using (var writer = new SingleFeatureWriter(file))
                {
                    foreach (var feature in expectedFeatures)
                    {
                        writer.Write(feature, null);
                    }

                    // confirm file size is what we expect
                    Assert.AreEqual(751L, file.Length);
                }
            }

            using (var file = File.OpenRead(FullPath))
            {
                using (var reader = new SingleFeatureReader(file))
                {
                    var count = 0;
                    while (reader.Read())
                    {
                        // validate that the feature is not null
                        Assert.IsNotNull(reader.Current);

                        // validate that the features are of the correct type
                        var expectedType = expectedFeatures[count].GetType();
                        var actualType = reader.Current.GetType();
                        Assert.AreEqual(expectedType, actualType);

                        count++;
                    }

                    // validate that we only read what we needed to
                    Assert.AreEqual(expectedFeatures.Length, count);
                }
            }

            File.Delete(FullPath);
        }

        /// <summary>
        /// Tests the write method when given invalid parameters
        /// </summary>
        [Test]
        public void TestWriteMethodInvalidParameters()
        {
            File.Delete(FullPath);

            // write features seperately
            using (var file = File.Create(FullPath))
            {
                using (var writer = new SingleFeatureWriter(file))
                {
                    Feature nullFeature = null;
                    Assert.Throws<ArgumentNullException>(() =>
                    {
                        writer.Write(nullFeature, null);
                    });
                }
            }

            File.Delete(FullPath);
        }

        /// <summary>
        /// Tests the write method when the stream has been unexpectedly closed
        /// </summary>
        [Test]
        public void TestWriteMethodClosedStream()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new SingleFeatureWriter(memoryStream))
                {
                    memoryStream.Close();

                    Assert.Throws<ObjectDisposedException>(() =>
                    {
                        writer.Write(BinaryPlaceTests.SmallGuidPlace, null);
                    });
                }
            }
        }
    }
}