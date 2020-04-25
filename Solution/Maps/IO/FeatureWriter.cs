using System;
using System.Collections.Generic;
using System.IO;
using Maps.Geographical.Features;
using Maps.IO.Collections;
using Maps.IO.Features;

namespace Maps.IO
{
    /// <summary>
    /// Represents a writer that can write Features to a stream
    /// </summary>
    /// <remarks>Written streams are not interchangeable with FeatureWriter or 
    /// FeatureReader.</remarks>
    public class FeatureWriter : IDisposable
    {
        private readonly Stream _destination;
        private readonly ISideData _sideData;
        private bool _disposed;

        /// <summary>
        ///Initializes a new instance of FeatureStreamWriter
        /// </summary>
        /// <param name="destination">The destination stream</param>
        /// <param name="sideData">The side data target</param>
        /// <exception cref="ArgumentNullException">Thrown if any argument is null</exception>
        public FeatureWriter(Stream destination, ISideData sideData = null)
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

            _destination = destination;
            _sideData = sideData;
        }

        /// <summary>
        /// Writes features
        /// </summary>
        /// <param name="features">The features to write</param>
        /// <exception cref="ArgumentNullException">Thrown if any argument is null</exception>
        public void Write(IList<Feature> features)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(FeatureWriter));
            }

            if (features == null)
            {
                throw new ArgumentNullException(nameof(features));
            }

            if (!_destination.CanWrite)
            {
                throw new ObjectDisposedException("Stream has been closed " +
                    "unexpectedly");
            }

            // collect features into binary feature array
            var binaryFeatures = new BinaryFeature[features.Count];
            for (var i = 0; i < features.Count; i++)
            {
                if (features[i] == null)
                {
                    throw new ArgumentException("Contains null element at " +
                        $"index {i}", nameof(features));
                }

                binaryFeatures[i] = features[i].ToBinary(_sideData);
            }

            // serialize from a BinaryFeatureCollection
            new BinaryFeatureCollection(binaryFeatures).Serialize(_destination);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(FeatureWriter));
            }

            if (_destination != null)
            {
                _destination.Close();
            }
            
            _disposed = true;
        }
    }
}