using System;
using System.Collections.Generic;
using System.IO;
using Maps.Geographical.Features;
using Maps.Geographical.Tiles;
using Maps.IO;

namespace Maps.Data.Geographical.Features
{
    /// <summary>
    /// Our own feature target 
    /// </summary>
    public class MapsFeatureTarget : ITiledFeatureTarget
    {
        /// <inheritdoc />
        public TiledSourceMeta Meta
        {
            set
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapsFeatureTarget));
                }

                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                // write the meta to an array
                using (var stream = new MemoryStream())
                {
                    // serialize the meta instance to the stream
                    value.Serialize(stream);

                    lock (_writerLock)
                    {
                        // write the stream as an array to the db
                        _metaWriter.Write(MapsFeatureProvider.MetaRow, stream.ToArray());
                    }
                }
            }
        }

        /// <inheritdoc />
        public ITileSource TileSource
        {
            get
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(MapsFeatureTarget));
                }

                return _tileSource;
            }
        }

        private readonly IDbWriter<long, byte[]> _featureWriter;
        private readonly IDbWriter<string, byte[]> _metaWriter;
        private readonly ISideData _sideData;
        private readonly ITileSource _tileSource;
        private readonly object _writerLock;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of MapsFeatureTarget
        /// </summary>
        /// <param name="featureWriter">The tile writer to use</param>
        /// <param name="metaWriter">The meta writer to use</param>
        /// <param name="sideData">The side data target</param>
        internal MapsFeatureTarget(IDbWriter<long, byte[]> featureWriter,
            IDbWriter<string, byte[]> metaWriter, ISideData sideData)
        {
            if (featureWriter == null)
            {
                throw new ArgumentNullException(nameof(featureWriter));
            }

            if (metaWriter == null)
            {
                throw new ArgumentNullException(nameof(metaWriter));
            }

            if (sideData == null)
            {
                throw new ArgumentNullException(nameof(sideData));
            }

            _tileSource = new TmsTileSource();
            _writerLock = new object();
            _featureWriter = featureWriter;
            _metaWriter = metaWriter;
            _sideData = sideData;
        }

        /// <inheritdoc />
        public void Write(Tile tile, IList<Feature> features)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapsFeatureTarget));
            }

            if (tile == null)
            {
                throw new ArgumentNullException(nameof(tile));
            }

            // if there are no features for the tile, skip side data
            if (features == null || features.Count < 1)
            {
                lock (_writerLock)
                {
                    _featureWriter.Write(tile.Id, null);
                }
            }
            else
            {
                using (var stream = new MemoryStream())
                {
                    using (var writer = new FeatureWriter(stream, _sideData))
                    {
                        lock (_writerLock)
                        {
                            // write the clipped features to the stream
                            writer.Write(features);
                            // write the stream as a byte array to the indexed writer
                            _featureWriter.Write(tile.Id, stream.ToArray());
                        }
                    }
                }
            }
        }

        /// <inheritdoc />
        public void Write(IList<Tile> tiles, IList<IList<Feature>> features)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapsFeatureTarget));
            }

            if (features == null)
            {
                throw new ArgumentNullException(nameof(features));
            }

            if (tiles == null)
            {
                throw new ArgumentNullException(nameof(tiles));
            }

            if (features.Count != tiles.Count)
            {
                throw new ArgumentException($"Lists {nameof(tiles)} and {nameof(features)} must be of same length");
            }

            lock (_writerLock)
            {
                // create the array of byte arrays to write
                var tileIds = new long[tiles.Count];
                var bytes = new byte[tiles.Count][];
                for (var i = 0; i < tiles.Count; i++)
                {
                    // if there are no features for this given tile, skip side data
                    if (features[i] == null || features[i].Count < 1)
                    {
                        bytes[i] = null;
                    }
                    else
                    {
                        using (var stream = new MemoryStream())
                        {
                            using (var blockWriter = new FeatureWriter(stream, _sideData))
                            {
                                // write the clipped features to the stream
                                blockWriter.Write(features[i]);
                                // set the bytes array for the tile
                                bytes[i] = stream.ToArray();
                            }
                        }
                    }

                    tileIds[i] = tiles[i].Id;
                }
                // write all the tiles with the byte arrays alongside
                _featureWriter.Write(tileIds, bytes);
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapsFeatureTarget));
            }

            Flush();

            lock (_writerLock)
            {
                _featureWriter.Dispose();
                _metaWriter.Dispose();
            }

            _disposed = true;
        }

        /// <inheritdoc />
        public void Flush()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MapsFeatureTarget));
            }

            lock (_writerLock)
            {
                _featureWriter.Flush();
                _metaWriter.Flush();
                _sideData.Flush();
            }
        }
    }
}