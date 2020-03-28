using System;
using System.IO;
using Maps.Geographical.Features;
using Maps.IO;
using Maps.Tests.IO.Features;
using NUnit.Framework;

namespace Maps.Tests.IO
{
    /// <summary>
    /// Series of tests for the FeatureWriter class
    /// </summary>
    [TestFixture]
    internal sealed class FeatureWriterTests
    {
        private static string FullPath => TestUtilities.WorkingDirectory +
            "FeatureBlockWriter.bin";

        /// <summary>
        /// Tests the constructor
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new FeatureWriter(memoryStream, null))
                {
                    Assert.IsNotNull(writer);
                }
            }
        }

        /// <summary>
        /// Tests the constructor when given invalid parameters
        /// </summary>
        [Test]
        public void TestConstructorInvalidParameters()
        {
            Stream nullStream = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                new FeatureWriter(nullStream, null);
            });

            Stream closedStream = new MemoryStream();
            closedStream.Close();

            Assert.Throws<ArgumentException>(() =>
            {
                new FeatureWriter(closedStream, null);
            });
        }

        /// <summary>
        /// Tests the ability to write an array of features
        /// </summary>
        [Test]
        public void TestWriteMethod()
        {
            File.Delete(FullPath);

            var expectedFeatures = SingleFeatureWriterTests.SampleFeatures;

            using (var file = File.Create(FullPath))
            {
                using (var writer = new FeatureWriter(file, null))
                {
                    writer.Write(expectedFeatures);
                }
            }

            using (var file = File.OpenRead(FullPath))
            {
                using (var reader = new FeatureReader(file,
                    null))
                {
                    var count = 0;
                    while (reader.Read())
                    {
                        count++;
                    }

                    // validate that we only had a single read
                    Assert.AreEqual(1, count);

                    // validate the feature array
                    Assert.IsNotNull(reader.Current);
                    Assert.AreEqual(expectedFeatures.Length, reader.Current.Count);

                    for (var i = 0; i < expectedFeatures.Length; i++)
                    {
                        var expectedType = expectedFeatures[i].GetType();
                        var actualType = reader.Current[i].GetType();

                        Assert.AreEqual(expectedType, actualType);
                    }
                }    
            }

            File.Delete(FullPath);
        }

        /// <summary>
        /// Tests the ability to write an array of features when given
        /// invalid parameters
        /// </summary>
        [Test]
        public void TestWriteMethodInvalidParameters()
        {
            // null array invalid case
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new FeatureWriter(memoryStream, null))
                {
                    Feature[] features = null;
                    Assert.Throws<ArgumentNullException>(() =>
                    {
                        writer.Write(features);
                    });
                }
            }

            // null element in array case
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new FeatureWriter(memoryStream, null))
                {
                    var features = new[]
                    {
                        BinaryAreaTests.SmallGuidArea,
                        null
                    };

                    Assert.Throws<ArgumentException>(() =>
                    {
                        writer.Write(features);
                    });
                }
            }
        }

        /// <summary>
        /// Tests the ability to write an array of features when given
        /// invalid parameters
        /// </summary>
        [Test]
        public void TestWriteMethodClosedStream()
        {
            // null array invalid case
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new FeatureWriter(memoryStream, null))
                {
                    memoryStream.Close();
                    Assert.Throws<ObjectDisposedException>(() =>
                    {
                        writer.Write(SingleFeatureWriterTests.SampleFeatures);
                    });
                }
            }
        }
    }
}