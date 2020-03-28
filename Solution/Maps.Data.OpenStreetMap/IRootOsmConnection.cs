using System;

namespace Maps.Data.OpenStreetMap
{
    /// <summary>
    /// Root connection serving up specific datatype connections
    /// </summary>
    internal interface IRootOsmConnection : IDisposable
    {
        /// <summary>
        /// OpenStreetMap geometry connection
        /// </summary>
        IOsmGeoConnection GeosConnection
        {
            get;
        }
    }
}