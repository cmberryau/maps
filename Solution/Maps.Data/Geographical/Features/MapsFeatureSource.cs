using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Maps.Geographical.Features;
using Maps.Geographical.Tiles;
using Maps.IO;

namespace Maps.Data.Geographical.Features
{
    /// <summary>
    /// Our own feature source
    /// </summary>
    public class MapsFeatureSource : ITiledFeatureSource
    {
        /// <inheritdoc />
        public TiledSourceMeta Meta
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapsFeatureSource));
                }

                var bytes = _metaReader.Read(MapsFeatureProvider.MetaRow);
                if (bytes != null)
                {
                    using (var stream = new MemoryStream(bytes))
                    {
                        if (TiledSourceMeta.TryDeserialize(stream, out var meta))
                        {
                            return meta;
                        }
                    }
                }

                return null;
            }
        }

        /// <inheritdoc />
        public ITileSource TileSource
        {
            get;
        }

        /// <inheritdoc />
        public ISideData SideData
        {
            get;
        }

        private readonly IDbReader<long, byte[]> _featureReader;
        private readonly IDbReader<string, byte[]> _metaReader;
        private readonly object _readerLock;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of MapsFeatureSource
        /// </summary>
        /// <param name="featureReader">The tile reader to use</param>
        /// <param name="metaReader">The meta reader to use</param>
        /// <param name="sideData">The side data source</param>
        internal MapsFeatureSource(IDbReader<long, byte[]> featureReader,
            IDbReader<string, byte[]> metaReader, ISideData sideData)
        {
            if (featureReader == null)
            {
                throw new ArgumentNullException(nameof(featureReader));
            }

            if (metaReader == null)
            {
                throw new ArgumentNullException(nameof(metaReader));
            }

            if (sideData == null)
            {
                throw new ArgumentNullException(nameof(sideData));
            }

            TileSource = new TmsTileSource();
            _readerLock = new object();

            _featureReader = featureReader;
            _metaReader = metaReader;
            SideData = sideData;
        }

        /// <inheritdoc />
        public IList<Feature> Get(Tile tile)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapsFeatureSource));
            }

            if (tile == null)
            {
                throw new ArgumentNullException(nameof(tile));
            }

            byte[] bytes;

            lock (_readerLock)
            {
                // read byte array
                bytes = _featureReader.Read(tile.Id);
            }

            // only open up stream if not null and not zero sized
            if (bytes == null || bytes.Length < 1)
            {
                // if nothing was read, return empty
                return new Feature[0];
            }

            // open up a stream from the byte array
            using (var stream = new MemoryStream(bytes))
            {
                using (var reader = new FeatureReader(stream, SideData))
                {
                    lock (_readerLock)
                    {
                        // read the block, return the first result - should be no more
                        while (reader.Read())
                        {
                            return reader.Current;
                        }
                    }
                }
            }

            return new Feature[0];
        }

        /// <inheritdoc />
        public IList<IList<Feature>> Get(IList<Tile> tiles)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapsFeatureSource));
            }

            if (tiles == null)
            {
                throw new ArgumentNullException(nameof(tiles));
            }

            var tileIds = new long[tiles.Count];
            for (var i = 0; i < tiles.Count; ++i)
            {
                if (tiles[i] == null)
                {
                    throw new ArgumentException($"Contains null element at index {i}", nameof(tiles));
                }

                tileIds[i] = tiles[i].Id;
            }

            IList<byte[]> byteArrays;

            // read the byte arrays
            lock (_readerLock)
            {
                byteArrays = _featureReader.Read(tileIds);
            }

            // if no byte arrays returned, return empty features lists list
            if (byteArrays == null || byteArrays.Count < 1)
            {
                throw new InvalidOperationException($"{nameof(IDbReader<long, byte[]>)} returned null or empty array");
            }

            // fill the feature arrays
            IList<IList<Feature>> featuresArrays = new Feature[byteArrays.Count][];
            for (var i = 0; i < byteArrays.Count; ++i)
            {
                // if byte array is null or zero sized, skip stream phase
                if (byteArrays[i] == null || byteArrays[i].Length < 1)
                {
                    featuresArrays[i] = new Feature[0];
                }
                else
                {
                    // open up a stream from the byte array
                    using (var stream = new MemoryStream(byteArrays[i]))
                    {
                        using (var blockReader = new FeatureReader(stream, SideData))
                        {
                            var count = 0;

                            lock (_readerLock)
                            {
                                // read the block
                                while (blockReader.Read())
                                {
                                    // should be no more than one read per stream
                                    if (++count > 1)
                                    {
                                        throw new InvalidOperationException(
                                            "Byte array contains malformed data");
                                    }

                                    featuresArrays[i] = blockReader.Current ?? new Feature[0];
                                }
                            }
                        }
                    }
                }
            }

            return featuresArrays;
        }

        /// <inheritdoc />
        public Task<IList<Feature>> GetAsync(Tile tile)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapsFeatureSource));
            }

            if (tile == null)
            {
                throw new ArgumentNullException(nameof(tile));
            }

            return Task<IList<Feature>>.Factory.StartNew(() => Get(tile));
        }

        /// <inheritdoc />
        public Task<IList<IList<Feature>>> GetAsync(IList<Tile> tiles)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapsFeatureSource));
            }

            if (tiles == null)
            {
                throw new ArgumentNullException(nameof(tiles));
            }

            for (var i = 0; i < tiles.Count; i++)
            {
                if (tiles[i] == null)
                {
                    throw new ArgumentException($"Contains null element at index {i}", nameof(tiles));
                }
            }

            return Task<IList<IList<Feature>>>.Factory.StartNew(() => Get(tiles));
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapsFeatureSource));
            }

            _featureReader.Dispose();
            _metaReader.Dispose();

            _disposed = true;
        }

        /// <inheritdoc />
        public object Clone()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapsFeatureSource));
            }

            throw new NotImplementedException();
        }
    }
}