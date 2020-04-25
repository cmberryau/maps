using System;
using System.IO;
using Maps.Geographical.Features;
using Maps.IO.Features;

namespace Maps.IO
{
    /// <summary>
    /// Represents a reader that can reader Features from a stream
    /// </summary>
    public class SingleFeatureReader : IDisposable
    {
        /// <summary>
        /// The current feature
        /// </summary>
        public Feature Current
        {
            get;
            private set;
        }

        private readonly Stream _sourceStream;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of SingleFeatureReader
        /// </summary>
        /// <param name="source">The source stream</param>
        public SingleFeatureReader(Stream source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.CanRead)
            {
                throw new ArgumentException("Cannot read from stream", 
                    nameof(source));
            }

            _sourceStream = source;
        }

        /// <summary>
        /// Reads a feature from the the stream
        /// </summary>
        public bool Read()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SingleFeatureReader));
            }

            if (!_sourceStream.CanRead)
            {
                throw new ObjectDisposedException("Stream has been closed " +
                    "unexpectedly");
            }

            // deserialize from the source stream
            var binaryFeature = BinaryFeature.Deserialize(_sourceStream, true);

            // if we got something, fill up the Current property
            if (binaryFeature != null)
            {
                Current = binaryFeature.ToFeature();
                return true;
            }

            // return false if we got nothing
            return false;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SingleFeatureReader));
            }

            if (_sourceStream != null)
            {
                _sourceStream.Close();
            }
            
            _disposed = true;
        }
    }
}