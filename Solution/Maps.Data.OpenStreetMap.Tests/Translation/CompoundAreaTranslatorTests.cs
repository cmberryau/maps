using System.Collections.Generic;
using Maps.Data.OpenStreetMap.PostgreSQL;
using Maps.Data.OpenStreetMap.Translation;
using Maps.Geographical.Features;
using Npgsql;
using NUnit.Framework;

namespace Maps.Data.OpenStreetMap.Tests.Translation
{
    /// <summary>
    /// Series of tests for the CompoundAreaTranslator class
    /// </summary>
    [TestFixture]
    public class CompoundAreaTranslatorTests
    {
        /// <summary>
        /// Test the split relation case
        /// </summary>
        [Test]
        public void TestSplitRelationCase()
        {
            using (var connection = new NpgsqlConnection(
                OpenStreetMapReferenceProvider.DefaultConnectionString))
            {
                using (var geoSource = new PostgreSQLOsmGeoSource(new PostgreSQLClient(connection)))
                {
                    var relations = geoSource.GetRelations(new List<long>
                    {
                        58887,
                        2501754,
                        71460,
                        3417949,
                        4501251,
                        411356,
                        1426817,
                        34344521,
                        1456949,
                    });

                    Assert.IsNotEmpty(relations);

                    var translator = CompoundAreaTranslator.Default;
                    var features = new List<Feature>();

                    foreach (var relation in relations)
                    {
                        translator.TryTranslate(relation, features);
                        Assert.IsNotNull(features);
                    }
                }
            }
        }
    }
}