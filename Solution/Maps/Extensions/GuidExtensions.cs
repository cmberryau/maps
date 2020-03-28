using System;
using System.Collections.Generic;

namespace Maps.Extensions
{
    /// <summary>
    /// Provides a set of  extensions for the Guid class
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// Converts the first 8 bytes of a Guid to a long
        /// </summary>
        /// <param name="guid">The Guid to convert</param>
        /// <param name="offset">The byte offset in the Guid</param>
        public static long ToLong(this Guid guid, int offset = 0)
        {
            var ba = guid.ToByteArray();

            return (long)ba[0 + offset] | ((long)ba[1 + offset] << 8) |
                   ((long)ba[2 + offset] << 16) | ((long)ba[3 + offset] << 24) |
                   ((long)ba[4 + offset] << 32) | ((long)ba[5 + offset] << 40) |
                   ((long)ba[6 + offset] << 48) | ((long)ba[7 + offset] << 56);
        }

        /// <summary>
        /// Converts a long to a guid (note, only getting 8 bytes of info)
        /// </summary>
        /// <param name="value">The long to convert</param>
        public static Guid ToGuid(this long value)
        {
            return new Guid(new[]
            {
                (byte)value, (byte)(value >> 8),
                (byte)(value >> 16), (byte)(value >> 24),
                (byte)(value >> 32), (byte)(value >> 40),
                (byte)(value >> 48), (byte)(value >> 56),
                (byte)0, (byte)0, (byte)0, (byte)0,
                (byte)0, (byte)0, (byte)0, (byte)0
            });
        }

        /// <summary>
        /// Converts a list of longs to a list of guids
        /// </summary>
        /// <param name="values">The longs to convert to guids</param>
        /// <returns>A list of converted guids</returns>
        public static IList<Guid> ToGuids(this IList<long> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            var valuesCount = values.Count;
            var result = new Guid[valuesCount];

            for (var i = 0; i < valuesCount; ++i)
            {
                result[i] = values[i].ToGuid();
            }

            return result;
        }

        /// <summary>
        /// Converts a pair of longs to a Guid
        /// </summary>
        /// <param name="a">The long for the first 8 bytes</param>
        /// <param name="b">The long for the second 8 bytes</param>
        public static Guid ToGuid(this long a, long b)
        {
            return new Guid(new[]
            {
                (byte)a, (byte)(a >> 8),
                (byte)(a >> 16), (byte)(a >> 24),
                (byte)(a >> 32), (byte)(a >> 40),
                (byte)(a >> 48), (byte)(a >> 56),
                (byte)b, (byte)(b >> 8),
                (byte)(b >> 16), (byte)(b >> 24),
                (byte)(b >> 32), (byte)(b >> 40),
                (byte)(b >> 48), (byte)(b >> 56)
            });
        }
    }
}