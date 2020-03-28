using System;
using System.Collections.Generic;
using Maps.Geographical;

namespace Maps.Data.OpenStreetMap
{
    /// <summary>
    /// Interface for OpenStreetMap geometry sources
    /// </summary>
    internal interface IOsmGeoSource : IDisposable, ICloneable
    {
        /// <summary>
        /// Returns the nodes for the given ids
        /// </summary>
        /// <param name="ids">The ids to fetch</param>
        IList<Node> GetNodes(IList<long> ids);

        /// <summary>
        /// Returns the nodes in a given box
        /// </summary>
        /// <param name="box">The box to get nodes in</param>
        /// <param name="tags">The tags to match against</param>
        IList<Node> GetNodes(GeodeticBox2d box, IList<Tuple<string, IList<string>>> tags = null);

        /// <summary>
        /// Returns the ways for the given ids
        /// </summary>
        /// <param name="ids">The ids to fetch</param>
        IList<Way> GetWays(IList<long> ids);

        /// <summary>
        /// Returns the ways in a given box
        /// </summary>
        /// <param name="box">The box to get ways in</param>
        /// <param name="tags">The tags to match against</param>
        IList<Way> GetWays(GeodeticBox2d box, IList<Tuple<string, IList<string>>> tags = null);

        /// <summary>
        /// Returns the relations for the given ids
        /// </summary>
        /// <param name="ids">The ids to fetch</param>
        IList<Relation> GetRelations(IList<long> ids);

        /// <summary>
        /// Returns the relations in a given box
        /// </summary>
        /// <param name="box">The box to get relations in</param>
        /// <param name="tags">The tags to match against</param>
        IList<Relation> GetRelations(GeodeticBox2d box, IList<Tuple<string, IList<string>>> tags = null);
    }
}