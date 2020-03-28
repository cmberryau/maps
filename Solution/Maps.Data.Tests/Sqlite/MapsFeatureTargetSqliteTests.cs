using System.Drawing;
using System.IO;
using Maps.Data.Sqlite;
using Maps.Data.Tests.Geographical.Features;
using NUnit.Framework;

namespace Maps.Data.Tests.Sqlite
{
    /// <summary>
    /// Tests for MapFeatureTarget when using sqlite
    /// </summary>
    [TestFixture]
    internal class MapsFeatureTargetSqliteTests : MapsFeatureTargetTests
    {
        /// <inheritdoc />
        protected override IDbConnection<long, byte[]> FeatureConnection()
        {
            if (!Directory.Exists(SqliteTestUtilities.TempDbDirectory))
            {
                Directory.CreateDirectory(SqliteTestUtilities.TempDbDirectory);
            }

            return new SqliteDbConnection<long, byte[]>(SqliteTestUtilities.TempDbDirectory + "Features.sqlite3", "tiles");
        }

        /// <inheritdoc />
        protected override IDbConnection<long, byte[]> ReferenceFeatureConnection()
        {
            if (!Directory.Exists(SqliteTestUtilities.ReferenceDbDirectory))
            {
                Directory.CreateDirectory(SqliteTestUtilities.ReferenceDbDirectory);
            }

            return new SqliteDbConnection<long, byte[]>(SqliteTestUtilities.ReferenceDbDirectory + "Features.sqlite3", "tiles");
        }

        /// <inheritdoc />
        protected override IDbConnection<long, string> StringConnection()
        {
            if (!Directory.Exists(SqliteTestUtilities.TempDbDirectory))
            {
                Directory.CreateDirectory(SqliteTestUtilities.TempDbDirectory);
            }

            return new SqliteDbConnection<long, string>(SqliteTestUtilities.TempDbDirectory + "Strings.sqlite3", "strings");
        }

        /// <inheritdoc />
        protected override IDbConnection<long, string> ReferenceStringConnection()
        {
            if (!Directory.Exists(SqliteTestUtilities.ReferenceDbDirectory))
            {
                Directory.CreateDirectory(SqliteTestUtilities.ReferenceDbDirectory);
            }

            return new SqliteDbConnection<long, string>(SqliteTestUtilities.ReferenceDbDirectory + "Strings.sqlite3", "strings");
        }

        /// <inheritdoc />
        protected override IDbConnection<long, Bitmap> ImageConnection()
        {
            if (!Directory.Exists(SqliteTestUtilities.TempDbDirectory))
            {
                Directory.CreateDirectory(SqliteTestUtilities.TempDbDirectory);
            }

            return new SqliteDbConnection<long, Bitmap>(SqliteTestUtilities.TempDbDirectory + "Images.sqlite3", "images");
        }

        /// <inheritdoc />
        protected override IDbConnection<long, Bitmap> ReferenceImageConnection()
        {
            if (!Directory.Exists(SqliteTestUtilities.ReferenceDbDirectory))
            {
                Directory.CreateDirectory(SqliteTestUtilities.ReferenceDbDirectory);
            }

            return new SqliteDbConnection<long, Bitmap>(SqliteTestUtilities.ReferenceDbDirectory + "Strings.sqlite3", "images");
        }
    }
}