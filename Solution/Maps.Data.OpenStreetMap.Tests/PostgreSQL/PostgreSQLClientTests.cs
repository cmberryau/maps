using Maps.Data.OpenStreetMap.PostgreSQL;
using Npgsql;
using NUnit.Framework;

namespace Maps.Data.OpenStreetMap.Tests.PostgreSQL
{
    [TestFixture]
    public class PostgreSQLClientTests
    {
        [Test]
        public void TestCloneMethod()
        {
            var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString);

            var client = new PostgreSQLClient(connection);
            var clonedClient = client.Clone() as PostgreSQLClient;

            Assert.IsNotNull(clonedClient);
        }
    }
}