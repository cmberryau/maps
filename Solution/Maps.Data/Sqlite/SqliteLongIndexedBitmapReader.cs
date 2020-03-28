using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Mono.Data.Sqlite;

namespace Maps.Data.Sqlite
{
    /// <summary>
    /// A specialized long indexed reader for the System.Drawing.Bitmap class
    /// </summary>
    internal class SqliteLongIndexedBitmapReader : IIndexedReader<long, Bitmap>
    {
        private readonly IIndexedReader<long, byte[]> _impl;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of SqliteLongIndexedBitmapReader
        /// </summary>
        /// <param name="connection">The sqlite connection to use</param>
        /// <param name="table">The name of the table</param>
        /// <param name="index">The name of the tables index</param>
        /// <param name="data">The name of the content column</param>
        public SqliteLongIndexedBitmapReader(SqliteConnection connection, string table, 
            string index, string data)
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

            _impl = new SqliteIndexedByteArrayReader<long>(connection, table, index, 
                data);
        }

        /// <inheritdoc />
        public Bitmap Read(long index)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SqliteLongIndexedBitmapReader));
            }

            var bytes = _impl.Read(index);
            using (var stream = new MemoryStream(bytes))
            {
                return new Bitmap(stream);
            }
        }

        /// <inheritdoc />
        public IList<Bitmap> Read(IList<long> indices)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SqliteLongIndexedBitmapReader));
            }

            var byteBlocks = _impl.Read(indices);
            var bitmaps = new Bitmap[indices.Count];
            for (var i = 0; i < byteBlocks.Count; i++)
            {
                using (var stream = new MemoryStream(byteBlocks[i]))
                {
                    bitmaps[i] = new Bitmap(stream);
                }
            }

            return bitmaps;
        }

        /// <inheritdoc />
        public byte[] ReadMeta(string key)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SqliteLongIndexedBitmapReader));
            }

            return _impl.ReadMeta(key);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SqliteLongIndexedBitmapReader));
            }

            _impl.Dispose();
            _disposed = true;
        }
    }
}