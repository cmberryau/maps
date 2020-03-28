using System;
using System.IO;
using Maps.IO;
using NUnit.Framework;

namespace Maps.Tests.IO
{
    /// <summary>
    /// Series of tests for the SingleFeatureReader class
    /// </summary>
    [TestFixture]
    internal sealed class SingleFeatureReaderTests
    {
        /// <summary>
        /// Tests the constructor
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var featureReader = new SingleFeatureReader(memoryStream))
                {
                    Assert.IsNotNull(featureReader);
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
                new SingleFeatureReader(nullStream);
            });

            Stream closedStream = new MemoryStream();
            closedStream.Close();

            Assert.Throws<ArgumentException>(() =>
            {
                new SingleFeatureReader(closedStream);
            });
        }

        /// <summary>
        /// Tests the read method when the stream is empty
        /// </summary>
        [Test]
        public void TestReadMethodEmptyStream()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var featureReader = new SingleFeatureReader(memoryStream))
                {
                    Assert.IsFalse(featureReader.Read());
                    Assert.IsNull(featureReader.Current);
                }
            }
        }

        /// <summary>
        /// Tests the read method when the stream has been unexpectedly closed
        /// </summary>
        [Test]
        public void TestReadMethodClosedStream()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var featureReader = new SingleFeatureReader(memoryStream))
                {
                    memoryStream.Close();

                    Assert.Throws<ObjectDisposedException>(() =>
                    {
                        featureReader.Read();
                    });
                }
            }
        }
    }
}