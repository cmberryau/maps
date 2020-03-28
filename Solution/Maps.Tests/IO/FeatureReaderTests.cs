using System;
using System.IO;
using Maps.IO;
using NUnit.Framework;

namespace Maps.Tests.IO
{
    /// <summary>
    /// Series of tests for the FeatureRader class
    /// </summary>
    [TestFixture]
    internal sealed class FeatureReaderTests
    {
        /// <summary>
        /// Tests the constructor
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var reader = new FeatureReader(memoryStream,
                    null))
                {
                    Assert.IsNotNull(reader);
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
                new FeatureReader(nullStream, null);
            });

            Stream closedStream = new MemoryStream();
            closedStream.Close();

            Assert.Throws<ArgumentException>(() =>
            {
                new FeatureReader(closedStream, null);
            });
        }
    }
}