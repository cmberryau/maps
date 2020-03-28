using System;
using System.Collections.Generic;

namespace Maps.Data.OpenStreetMap.Translation
{
    /// <summary>
    /// Interface for feature translators
    /// </summary>
    public interface IFeatureTranslator
    {
        /// <summary>
        /// The matching tags for feature inclusion
        /// </summary>
        IList<Tuple<string, ISet<string>>> Tags
        {
            get;
        }

        /// <summary>
        /// The number of tags for feature inclusion
        /// </summary>
        int TagsCount
        {
            get;
        }
    }
}