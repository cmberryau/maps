using System.Collections.Generic;
using System.Text;

namespace Maps.Data
{
    /// <summary>
    /// Tools for working with dbs
    /// </summary>
    public static class DbTools
    {
        /// <summary>
        /// Constructs an id list from the given list of longs
        /// </summary>
        /// <param name="ids">The list of longs to construct from</param>
        /// <param name="from">The starting index</param>
        /// <param name="to">The final index</param>
        public static string ConstructIdList(IList<long> ids, int from, int to)
        {
            var sb = new StringBuilder();
            var commaSnippet = ",";
            var idsCount = ids.Count;

            if (idsCount > 0 && idsCount > from)
            {
                sb.Append(ids[from].ToString());

                for (var i = from + 1; i < to; ++i)
                {
                    var idString = ids[i].ToString();

                    sb.Append(commaSnippet);
                    sb.Append(idString);
                }
            }

            return sb.ToString();
        }
    }
}
