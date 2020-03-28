using System;
using Maps.Geographical.Features;
using Maps.IO;

namespace Maps.Data.Geographical.Features
{
    /// <summary>
    /// The feature provider for Maps.Data
    /// </summary>
    public class MapsFeatureProvider : IFeatureProvider
    {
        /// <inheritdoc />
        public OfflineSupportLevel OfflineSupport
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapsFeatureProvider));
                }

                return OfflineSupportLevel.Full;
            }
        }

        /// <inheritdoc />
        public bool ReadOnly
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapsFeatureProvider));
                }

                return _features.ReadOnly;
            }
        }

        /// <inheritdoc />
        public bool Tiled
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapsFeatureProvider));
                }

                return true;
            }
        }

        internal const string MetaRow = "feature_meta";

        private readonly IDbConnection<long, byte[]> _features;
        private readonly ISideData _sideData;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of MapsFeatureProvider
        /// </summary>
        public MapsFeatureProvider(IDbConnection<long, byte[]> features, ISideData sideData)
        {
            _features = features;
            _sideData = sideData;
        }

        /// <inheritdoc />
        public IFeatureSource FeatureSource()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapsFeatureProvider));
            }

            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public ITiledFeatureSource TiledFeatureSource()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapsFeatureProvider));
            }

            return new MapsFeatureSource(_features.Reader(), _features.MetaReader(), _sideData);
        }

        /// <inheritdoc />
        public IFeatureTarget FeatureTarget()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public ITiledFeatureTarget TiledFeatureTarget()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapsFeatureProvider));
            }

            return new MapsFeatureTarget(_features.Writer(), _features.MetaWriter(), _sideData);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapsFeatureProvider));
            }

            _features.Flush();
            _sideData.Flush();

            _disposed = true;
        }
    }
}