using Maps.Tests;
using Npgsql;
using NUnit.Framework;

namespace Maps.Data.OpenStreetMap.Tests
{
    /// <summary>
    /// A series of tests for the OpenStreetMapProvider class while using PostgreSQL
    /// </summary>
    [TestFixture]
    public class OpenStreetMapProviderPostgreSQLTests
    {
        /// <summary>
        /// Tests the constructor
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            using (var provider = new OpenStreetMapProvider(connection))
            {
                Assert.IsNotNull(provider);
            }
        }

        /// <summary>
        /// Tests the ability to get features
        /// </summary>
        [Test]
        public void TestGetFeatures()
        {
            using (var provider = new OpenStreetMapReferenceProvider())
            {
                using (var featureProvider = provider.FeatureProvider)
                {
                    using (var featureSource = featureProvider.FeatureSource())
                    {
                        var features = featureSource.Get(TestUtilities.BigIngolstadtBox);

                        Assert.IsNotNull(features);
                        Assert.IsNotEmpty(features);
                    }
                }
            }
        }
    }
}
