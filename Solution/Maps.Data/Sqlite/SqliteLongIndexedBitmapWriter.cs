using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Mono.Data.Sqlite;

namespace Maps.Data.Sqlite
{
    /// <summary>
    /// A specialized long indexed writer for the System.Drawing.Bitmap class
    /// </summary>
    internal class SqliteLongIndexedBitmapWriter : IIndexedWriter<long, Bitmap>
    {
        // ReSharper disable once StaticMemberInGenericType
        private static readonly ImageFormat DefaultImageFormat = ImageFormat.Png;

        private readonly IIndexedWriter<long, byte[]> _impl;
        private readonly ImageFormat _imageFormat;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of SqliteLongIndexedBitmapWriter
        /// </summary>
        /// <param name="connection">The sqlite connection to use</param>
        /// <param name="table">The name of the table</param>
        /// <param name="index">The name of the tables index</param>
        /// <param name="data">The name of the content column</param>
        /// <param name="imageFormat">The image format to save to</param>
        public SqliteLongIndexedBitmapWriter(SqliteConnection connection, string table,
            string index, string data, ImageFormat imageFormat = null)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            if (table == null)
            {
                throw new ArgumentNullException(nameof(table));
            }

            if (index == null)
            {
                throw new ArgumentNullException(nameof(index));
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(index));
            }

            if (imageFormat == null)
            {
                imageFormat = DefaultImageFormat;
            }

            _impl = new SqliteIndexedWriter<long, byte[]>(connection, table, index, 
                data);
            _imageFormat = imageFormat;
        }

        /// <inheritdoc />
        public void Write(long index, Bitmap data)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SqliteLongIndexedBitmapWriter));
            }

            using (var stream = new MemoryStream())
            {
                data.Save(stream, _imageFormat);
                _impl.Write(index, stream.ToArray());
            }
        }

        /// <inheritdoc />
        public void Write(IList<long> indices, IList<Bitmap> data)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SqliteLongIndexedBitmapWriter));
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            for (var i = 0; i < data.Count; i++)
            {
                Write(indices[i], data[i]);
            }
        }

        /// <inheritdoc />
        public void WriteMeta(string key, byte[] data)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SqliteLongIndexedBitmapWriter));
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            _impl.WriteMeta(key, data);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SqliteLongIndexedBitmapWriter));
            }

            _impl.Dispose();
            _disposed = true;
        }

        /// <inheritdoc />
        public void Flush()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SqliteLongIndexedBitmapWriter));
            }

            _impl.Flush();
        }
    }
}