using System;

namespace Maps.Data.OpenStreetMap
{
    /// <summary>
    /// Interface for classes respsonible for providing a OpenStreetMap geometry connection
    /// </summary>
    internal interface IOsmGeoConnection : IDbConnection, IDisposable
    {
        /// <summary>
        /// The geometry source
        /// </summary>
        IOsmGeoSource GeoSource
        {
            get;
        }
    }
}