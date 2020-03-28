using System;
using System.Collections.Generic;
using System.IO;
using Maps.Geographical.Features;
using Maps.IO.Collections;

namespace Maps.IO
{
    /// <summary>
    /// Represents a reader that can read Features from a stream
    /// </summary>
    public class FeatureReader : IDisposable
    {
        /// <summary>
        /// The current block of features
        /// </summary>
        public IList<Feature> Current
        {
            get;
            private set;
        }

        private readonly Stream _sourceStream;
        private readonly ISideData _sideData;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of FeatureReader
        /// </summary>
        /// <param name="source">The source stream</param>
        /// <param name="sideData">The strings dictionary</param>
        /// <exception cref="ArgumentNullException">Thrown if any argument is null</exception>
        /// <exception cref="ArgumentException">Thrown if source cannot be read</exception>
        public FeatureReader(Stream source, ISideData sideData)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (sideData == null)
            {
                throw new ArgumentNullException(nameof(sideData));
            }

            if (!source.CanRead)
            {
                throw new ArgumentException("Cannot read from stream",
                    nameof(source));
            }

            _sourceStream = source;
            _sideData = sideData;
        }

        /// <summary>
        /// Reads the next block of features
        /// </summary>
        public bool Read()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(FeatureReader));
            }

            if (!_sourceStream.CanRead)
            {
                throw new ObjectDisposedException("Stream has been closed " +
                    "unexpectedly");
            }

            // deserialize from the source stream
            var binaryCollection = BinaryFeatureCollection.Deserialize(_sourceStream);

            // if we got something, fill up the Current property
            if (binaryCollection != null)
            {
                Current = new Feature[binaryCollection.Count];

                for (var i = 0; i < binaryCollection.Count; i++)
                {
                    Current[i] = binaryCollection[i].ToFeature(_sideData);
                }

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
                throw new ObjectDisposedException(nameof(FeatureReader));
            }

            if (_sourceStream != null)
            {
                _sourceStream.Close();
            }
                        
            _disposed = true;
        }
    }
}