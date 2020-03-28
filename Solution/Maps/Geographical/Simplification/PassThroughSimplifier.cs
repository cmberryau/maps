using System.Collections.Generic;
using Maps.Geometry.Simplification;

namespace Maps.Geographical.Simplification
{
    /// <summary>
    /// Performs no simplification
    /// </summary>
    public class PassThroughSimplifier : IGeodeticSimplifier2d
    {
        /// <inheritdoc />
        public IList<Geodetic2d> Simplify(IList<Geodetic2d> coordinates)
        {
            return coordinates;
        }

        /// <inheritdoc />
        public GeodeticLineStrip2d Simplify(GeodeticLineStrip2d linestrip)
        {
            return linestrip;
        }

        /// <inheritdoc />
        public GeodeticPolygon2d Simplify(GeodeticPolygon2d polygon)
        {
            return polygon;
        }

        /// <inheritdoc />
        public IList<Geodetic2d> Simplify(IList<Geodetic2d> coordinates, 
            IList<bool> keep)
        {
            return coordinates;
        }

        /// <inheritdoc />
        public GeodeticLineStrip2d Simplify(GeodeticLineStrip2d linestrip, 
            IList<bool> keep)
        {
            return linestrip;
        }

        /// <inheritdoc />
        public GeodeticPolygon2d Simplify(GeodeticPolygon2d polygon,
            IList<IList<bool>> keep)
        {
            return polygon;
        }

        /// <inheritdoc />
        public GeodeticPolygon2d Simplify(GeodeticPolygon2d polygon, 
            IList<IList<bool>> keep, GeodeticPolygon2d preserveEdges)
        {
            return polygon;
        }

        /// <inheritdoc />
        public GeodeticPolygon2d Simplify(GeodeticPolygon2d polygon,
            GeodeticPolygon2d preserveEdges)
        {
            return polygon;
        }
    }
}