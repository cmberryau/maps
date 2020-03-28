using System;
using Maps.Geographical.Features;
using Maps.OsmSharp.Geographical.Features;
using Maps.Tests.Geographical.Features;
using NUnit.Framework;
using OsmSharp.Data.MySQL.Osm;

namespace Maps.OsmSharp.Tests.Geographical.Features
{
    /// <summary>
    /// A series of tests for OsmSharpFeatureSource
    /// </summary>
    [TestFixture]
    public sealed class OsmSharpFeatureSourceTests : FeatureSourceTests
    {
        /// <summary>
        /// Creates a fresh OsmSharpFeatureSource with known reference data
        /// </summary>
        protected override IFeatureSource CreateReferenceSource()
        {
            var source = new MySQLDataSource("cmb-dt", "maps", "mapsuser",
                    "mapsuserpassword", true);
            return new OsmSharpFeatureSource(source);
        }

        /// <summary>
        /// Creates an empty OsmSharpFeatureSource
        /// </summary>
        protected override IFeatureSource CreateEmptySource()
        {
            var source = new MySQLDataSource("cmb-dt", "mapsempty", "mapsuser",
                    "mapsuserpassword", true);
            return new OsmSharpFeatureSource(source);
        }

        /// <summary>
        /// Tests the constructor of OsmSharpFeatureSource
        /// </summary>
        [Test]
        public override void TestConstructor()
        {
            using (var source = new MySQLDataSource("cmb-dt", "mapsempty", "mapsuser",
                    "mapsuserpassword", true))
            {
                var osmSource = new OsmSharpFeatureSource(source);
            }
        }

        /// <summary>
        /// Tests the constructor for the OsmSharpFeatureSource
        /// </summary>
        [Test]
        public override void TestConstructorInvalidParameters()
        {
            Assert.Throws<ArgumentNullException>(() => 
                new OsmSharpFeatureSource(null));
        }
    }
}