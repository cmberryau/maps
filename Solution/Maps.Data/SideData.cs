using System;
using System.Collections.Generic;
using Maps.IO;

namespace Maps.Data
{
    /// <summary>
    /// Responsible for side data
    /// </summary>
    public class SideData : ISideData
    {
        private readonly IDictionary<Type, ITable> _tables;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of SideDataTarget
        /// </summary>
        /// <param name="tables">The tables to use</param>
        public SideData(IList<ITable> tables)
        {
            _tables = new Dictionary<Type, ITable>();

            foreach (var table in tables)
            {
                if (!_tables.ContainsKey(table.Type))
                {
                    _tables.Add(table.Type, table);
                }
                else
                {
                    throw new ArgumentException("Multiple tables for same type provided", nameof(tables));
                }
            }
        }

        /// <inheritdoc />
        public bool TryGetTable<TValue>(out ITable<TValue> table)
        {
            var type = typeof(TValue);
            if (_tables.TryGetValue(type, out var baseTable))
            {
                if (baseTable is ITable<TValue> castedTable)
                {
                    table = castedTable;
                    return true;
                }
            }

            table = null;
            return false;
        }

        /// <inheritdoc />
        public virtual void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SideData));
            }

            Flush();

            foreach (var table in _tables)
            {
                table.Value.Dispose();
            }

            _disposed = true;
        }

        /// <inheritdoc />
        public virtual void Flush()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SideData));
            }

            foreach (var table in _tables)
            {
                table.Value.Flush();
            }
        }
    }
}