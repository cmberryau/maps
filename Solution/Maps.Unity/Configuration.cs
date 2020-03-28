using System.Collections.Generic;
using System.Drawing;
using Maps.Data;
using Maps.Data.Sqlite;
using Maps.Geographical.Features;
using Maps.IO;
using Maps.Unity.IO;
using UnityEngine;

namespace Maps.Unity
{
    /// <summary>
    /// Responsible for holding configuration information for the Maps framework while 
    /// integrated with Unity3d
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// The overall provider for the Unity3d integration
        /// </summary>
        public static readonly IProvider Provider = CreateSource(DefaultDataUri);

        /// <summary>
        /// The feature provider for the Unity3d integration
        /// </summary>
        public static IFeatureProvider FeatureProvider => Provider.FeatureProvider;

        /// <summary>
        /// Creates a new tiled feature source
        /// </summary>
        public static ITiledFeatureSource CreateTiledFeatureSource()
        {
            return FeatureProvider.TiledFeatureSource();
        }

        /// <summary>
        /// The default streaming assets subfolder name
        /// </summary>
        public const string DefaultFolderName = "Maps";

        /// <summary>
        /// The default data uri
        /// </summary>
        public static string DefaultDataUri => Application.streamingAssetsPath +
                                               IOUtility.DirectorySeparatorChar + DefaultFolderName +
                                               IOUtility.DirectorySeparatorChar;

        private static IProvider CreateSource(string uri)
        {
            var featureConnection = new SqliteDbConnection<long, byte[]>(uri + "Features.sqlite3", "tiles");
            var stringConnection = new SqliteDbConnection<long, string>(uri + "Strings.sqlite3", "strings");
            var bitmapConnection = new SqliteDbConnection<long, Bitmap>(uri + "Images.sqlite3", "images");

            return new MapsProvider(featureConnection, new SideData(new List<ITable>
            {
                new DbTable<string>(stringConnection),
                new DbTable<Bitmap>(bitmapConnection),
            }));
        }
    }
}