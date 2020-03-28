using System;
using Maps.Geographical.Places;
using OsmSharp.Osm.Data;

namespace Maps.OsmSharp.Geographical.Places
{
    /// <summary>
    /// The entry point to OsmSharp's place source
    /// </summary>
    public class OsmSharpPlaceProvider : IPlaceProvider
    {
        /// <summary>
        /// The level of offline support provided
        /// </summary>
        public OfflineSupportLevel OfflineSupport
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(OsmSharpPlaceProvider));
                }

                return OfflineSupportLevel.Full;
            }
        }

        private readonly IDataSourceReadOnly _source;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of OsmSharpPlaceProvider
        /// </summary>
        /// <param name="source">The source to use</param>
        public OsmSharpPlaceProvider(IDataSourceReadOnly source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            _source = source;
        }

        /// <summary>
        /// The place source
        /// </summary>
        public IPlaceSource CreatePlaceSource()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OsmSharpPlaceProvider));
            }

            return new OsmSharpPlaceSource(_source);
        }

        /// <summary>
        ///  Disposes of all resources held by the OsmSharpPlaceProvider instance
        /// </summary>
        public void Dispose()
        {
            _disposed = true;
        }
    }
}