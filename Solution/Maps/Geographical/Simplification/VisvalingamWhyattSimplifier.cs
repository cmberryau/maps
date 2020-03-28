using System.Collections.Generic;

namespace Maps.Geographical.Simplification
{
    /// <summary>
    /// Responsible for simplifying geometry using the Visvalingam-Whyatt algorithm
    /// </summary>
    public class VisvalingamWhyattSimplifier : GeodeticSimplifier2d
    {
        /// <summary>
        /// Initializes a new instance of VisvalingamWhyattSimplifier
        /// </summary>
        public VisvalingamWhyattSimplifier()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public override IList<Geodetic2d> Simplify(IList<Geodetic2d> coordinates, 
            IList<bool> keep)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public override GeodeticLineStrip2d Simplify(GeodeticLineStrip2d linestrip, 
            IList<bool> keep)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public override GeodeticPolygon2d Simplify(GeodeticPolygon2d polygon, 
            IList<IList<bool>> keep, GeodeticPolygon2d preserveEdges)
        {
            throw new System.NotImplementedException();
        }
    }
}