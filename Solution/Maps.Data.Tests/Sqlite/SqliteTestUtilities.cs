using System.IO;
using Maps.Tests;

namespace Maps.Data.Tests.Sqlite
{
    /// <summary>
    /// Some shared properties between Maps.Data.Tests.Sqlite tests
    /// </summary>
    public static class SqliteTestUtilities
    {
        /// <summary>
        /// The directory to place reference databases
        /// </summary>
        public static string ReferenceDbDirectory => TestUtilities.ReferenceFilesDirectory +
            "Data" + Path.DirectorySeparatorChar;

        /// <summary>
        /// The directory to place temporary databases
        /// </summary>
        public static string TempDbDirectory => TestUtilities.WorkingDirectory +
            "Data" + Path.DirectorySeparatorChar;

        /// <summary>
        /// The full path to the temporary test db
        /// </summary>
        public static string TempTestDbFullPath => TempDbDirectory +
            "Test.sqlite3";
    }
}