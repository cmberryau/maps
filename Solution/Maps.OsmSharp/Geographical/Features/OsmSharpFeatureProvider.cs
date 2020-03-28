using System;
using Maps.Geographical.Features;
using OsmSharp.Osm.Data;

namespace Maps.OsmSharp.Geographical.Features
{
    /// <summary>
    /// The entry point to OsmSharp's feature source and target
    /// </summary>
    public class OsmSharpFeatureProvider : IFeatureProvider
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
                    throw new ObjectDisposedException(nameof(OsmSharpFeatureProvider));
                }

                return OfflineSupportLevel.Full;
            }
        }

        /// <summary>
        /// Is the feature provider readonly?
        /// </summary>
        public bool ReadOnly
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(OsmSharpFeatureProvider));
                }

                return true;
            }
        }

        /// <summary>
        /// Is the feature provider tiled?
        /// </summary>
        public bool Tiled
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(OsmSharpFeatureProvider));
                }

                return false;
            }
        }

        /// <summary>
        /// The feature source
        /// </summary>
        public IFeatureSource FeatureSource()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OsmSharpFeatureProvider));
            }

            return new OsmSharpFeatureSource(_source);
        }

        /// <summary>
        /// The tiled feature source, if available
        /// </summary>
        /// <exception cref="System.NotSupportedException">Thrown when
        /// the IFeatureProvider is marked as non Tiled</exception>
        public ITiledFeatureSource TiledFeatureSource()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// The feature target, if available
        /// </summary>
        /// <exception cref="System.NotSupportedException">Thrown when
        /// the IFeatureProvider is marked as ReadOnly</exception>
        /// <exception cref="System.NotSupportedException">Thrown when
        /// the IFeatureProvider is marked as Tiled</exception>
        public IFeatureTarget FeatureTarget()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// The feature target
        /// </summary>
        /// <exception cref="System.NotSupportedException">Thrown when
        /// the IFeatureProvider is marked as ReadOnly</exception>
        public ITiledFeatureTarget TiledFeatureTarget()
        {
            throw new NotSupportedException();
        }

        private readonly IDataSourceReadOnly _source;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of OsmSharpFeatureProvider
        /// </summary>
        /// <param name="source">The source to use</param>
        public OsmSharpFeatureProvider(IDataSourceReadOnly source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            _source = source;
        }

        
        /// <summary>
        /// Disposes of all resources held by the OsmSharpFeatureProvider instance
        /// </summary>
        public void Dispose()
        {
            _disposed = true;            
        }
    }
}