using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Maps.Data.Geographical.Features;
using Maps.Data.Sqlite;
using Maps.Geographical.Features;
using Maps.Geographical.Places;
using Maps.IO;

namespace Maps.Data
{
    /// <summary>
    /// A provider for testing, all data is cleared on dispose
    /// </summary>
    public class MapsTestProvider : IProvider
    {
        /// <inheritdoc />
        public bool PlacesSupported => false;

        /// <inheritdoc />
        public bool FeaturesSupported => true;

        /// <inheritdoc />
        public IPlaceProvider PlaceProvider => throw new NotSupportedException();

        /// <inheritdoc />
        public IFeatureProvider FeatureProvider => new MapsFeatureProvider(_featureConnection, 
            new SideData(new List<ITable>
        {
            new DbTable<string>(_stringConnection),
            new DbTable<Bitmap>(_bitmapConnection)
        }));

        private readonly IDbConnection<long, byte[]> _featureConnection;
        private readonly IDbConnection<long, string> _stringConnection;
        private readonly IDbConnection<long, Bitmap> _bitmapConnection;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of MapsTestProvider
        /// </summary>
        public MapsTestProvider()
        {
            var uri = AppDomain.CurrentDomain.BaseDirectory +
            Path.DirectorySeparatorChar + Path.DirectorySeparatorChar + "Data" +
            Path.DirectorySeparatorChar;

            if (!Directory.Exists(uri))
            {
                Directory.CreateDirectory(uri);
            }

            _featureConnection = new SqliteDbConnection<long, byte[]>(uri + "Features.sqlite3", "tiles");
            _stringConnection = new SqliteDbConnection<long, string>(uri + "Strings.sqlite3", "strings");
            _bitmapConnection = new SqliteDbConnection<long, Bitmap>(uri + "Images.sqlite3", "images");

            _featureConnection.Clear();
            _stringConnection.Clear();
            _bitmapConnection.Clear();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapsTestProvider));
            }

            _disposed = true;
        }
    }
}