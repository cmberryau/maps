using Maps.Geographical.Features;
using Maps.Geographical.Places;
using Maps.OsmSharp.Geographical.Features;
using Maps.OsmSharp.Geographical.Places;
using OsmSharp.Data.MySQL.Osm;
using OsmSharp.Osm.Data;

namespace Maps.OsmSharp.Tests
{
    /// <summary>
    /// A reference provider using MySQL, OpenStreetMap and OsmSharp
    /// </summary>
    public class OsmSharpReferenceProvider : IProvider
    {
        /// <inheritdoc />
        public bool PlacesSupported => true;

        /// <inheritdoc />
        public bool FeaturesSupported => true;

        private IDataSourceReadOnly DataSource = new MySQLDataSource("cmb-dt", "maps", 
            "mapsuser", "mapsuserpassword", true);

        /// <inheritdoc />
        public IPlaceProvider PlaceProvider => new OsmSharpPlaceProvider(DataSource);

        /// <inheritdoc />
        public IFeatureProvider FeatureProvider => new OsmSharpFeatureProvider(DataSource);

        /// <inheritdoc />
        public void Dispose()
        {
            // no resources held
        }
    }
}