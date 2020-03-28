using System;
using Maps.Geographical.Places;
using Maps.OsmSharp.Geographical.Places;
using Maps.Tests.Geographical.Places;
using NUnit.Framework;
using OsmSharp.Data.MySQL.Osm;

namespace Maps.OsmSharp.Tests.Geographical.Places
{
    /// <summary>
    /// A series of tests for the OsmSharpPlaceSource class
    /// </summary>
    [TestFixture]
    public sealed class OsmSharpPlaceSourceTests : PlaceSourceTests
    {
        /// <summary>
        /// Creates a fresh OsmSharpPlaceSource to be used for testing
        /// </summary>
        protected override IPlaceSource CreateSource()
        {
            var source = new MySQLDataSource("cmb-dt", "maps", "mapsuser",
                    "mapsuserpassword", true);
            return new OsmSharpPlaceSource(source);
        }

        /// <summary>
        /// Tests the constructor for OsmSharpPlaceSource
        /// </summary>
        [Test]
        public override void TestConstructor()
        {
            var source = new MySQLDataSource("cmb-dt", "maps", "mapsuser",
                    "mapsuserpassword", true);
            var osmSource = new OsmSharpPlaceSource(source);

            Assert.Throws<ArgumentNullException>(() =>
                osmSource = new OsmSharpPlaceSource(null));
        }
    }
}