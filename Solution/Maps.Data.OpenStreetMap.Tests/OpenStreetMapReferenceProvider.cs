using System.Reflection;
using Npgsql;

namespace Maps.Data.OpenStreetMap.Tests
{
    /// <summary>
    /// The reference OpenStreetMap provider, using PostgreSQL
    /// </summary>
    public class OpenStreetMapReferenceProvider : OpenStreetMapProvider
    {
        /// <summary>
        /// The default connection string used by the reference provider
        /// </summary>
        public static readonly string DefaultConnectionString = 
            "Server=localhost;" +
            "Port=5432;" +
            "Database=osm;" +
            "User Id=osmuser;" +
            "Password=osmuserpassword;" +
            $"ApplicationName={Assembly.GetExecutingAssembly()}";

        /// <inheritdoc />
        public OpenStreetMapReferenceProvider() : base(new NpgsqlConnection(DefaultConnectionString)){}
    }
}
