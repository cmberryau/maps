using System.Collections.Generic;
using Maps.Geographical;

namespace Maps.Data.OpenStreetMap
{
    /// <summary>
    /// Node feature from OpenStretMap
    /// </summary>
    public class Node : OsmGeo
    {
        /// <summary>
        /// The coordinate of the Node
        /// </summary>
        public readonly Geodetic2d Coordinate;

        /// <inheritdoc />
        protected override OsmGeoType Type => OsmGeoType.Node;

        /// <summary>
        /// Initializes a new instance of Node
        /// </summary>
        /// <param name="id">The id of the node</param>
        /// <param name="tags">The tags of the node</param>
        /// <param name="coordinate">The coordinate of the node</param>
        public Node(long id, IDictionary<string, string> tags, 
            Geodetic2d coordinate) : base(id, tags)
        {
            Coordinate = coordinate;
        }
    }
}