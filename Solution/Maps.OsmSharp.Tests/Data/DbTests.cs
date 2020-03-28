using Maps.Tests;
using NUnit.Framework;

namespace Maps.OsmSharp.Tests.Data
{
    /// <summary>
    /// Series of database related tests for the OsmSharp library
    /// </summary>
    [TestFixture]
    public class DbTests
    {
        /// <summary>
        /// Tests the db's ability to read a box
        /// </summary>
        [Test]
        public void TestIngoslstadtBoxQuery()
        {
            var provider = new OsmSharpReferenceProvider();
            var featureProvider = provider.FeatureProvider;
            var featureSource = featureProvider.FeatureSource();

            var features = featureSource.Get(TestUtilities.BigIngolstadtBox);

            Assert.NotNull(features);
            Assert.IsNotEmpty(features);
        }
    }
}