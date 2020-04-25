using System;
using System.IO;
using Maps.Geographical.Features;

namespace Maps.IO
{
    /// <summary>
    /// Represents a writer that can write single features to a stream
    /// </summary>
    public class SingleFeatureWriter : IDisposable
    {
        private readonly Stream _destinationStream;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of SingleFeatureWriter
        /// </summary>
        /// <param name="destination">The destination stream</param>
        /// <exception cref="ArgumentNullException">Thrown if 
        /// <paramref name="destination"/> is null</exception>
        public SingleFeatureWriter(Stream destination)
        {
            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            if (!destination.CanWrite)
            {
                throw new ArgumentException("Cannot write to stream", 
                    nameof(destination));
            }

            _destinationStream = destination;
        }

        /// <summary>
        /// Writes a feature
        /// </summary>
        /// <param name="feature">The feature to write</param>
        /// <param name="sideData">The side data target</param>
        /// <exception cref="ArgumentNullException">Thrown if any argument is null</exception>
        public void Write(Feature feature, ISideData sideData = null)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SingleFeatureWriter));
            }

            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature));
            }

            if (!_destinationStream.CanWrite)
            {
                throw new ObjectDisposedException("Stream has been closed " +
                    "unexpectedly");
            }

            // serialize as an individual feature
            feature.ToBinary(sideData).Serialize(_destinationStream, true);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SingleFeatureWriter));
            }

            if (_destinationStream != null)
            {
                _destinationStream.Close();
            }
            
            _disposed = true;
        }
    }
}