using System;
using System.Collections.Generic;
using Maps.Geometry.Simplification;

namespace Maps.Geographical.Simplification
{
    /// <summary>
    /// Provides base functionality for geodetic simplifiers
    /// </summary>
    public abstract class GeodeticSimplifier2d : IGeodeticSimplifier2d
    {
        /// <inheritdoc />
        public IList<Geodetic2d> Simplify(IList<Geodetic2d> coordinates)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            return Simplify(coordinates, null);
        }

        /// <inheritdoc />
        public abstract IList<Geodetic2d> Simplify(IList<Geodetic2d> coordinates,
            IList<bool> keep);

        /// <inheritdoc />
        public GeodeticLineStrip2d Simplify(GeodeticLineStrip2d linestrip)
        {
            if (linestrip == null)
            {
                throw new ArgumentNullException(nameof(linestrip));
            }

            return Simplify(linestrip, null);
        }

        /// <inheritdoc />
        public abstract GeodeticLineStrip2d Simplify(GeodeticLineStrip2d linestrip,
            IList<bool> keep);

        /// <inheritdoc />
        public GeodeticPolygon2d Simplify(GeodeticPolygon2d polygon)
        {
            if (polygon == null)
            {
                throw new ArgumentNullException(nameof(polygon));
            }

            return Simplify(polygon, null, null);
        }

        /// <inheritdoc />
        public GeodeticPolygon2d Simplify(GeodeticPolygon2d polygon, 
            IList<IList<bool>> keep)
        {
            if (polygon == null)
            {
                throw new ArgumentNullException(nameof(polygon));
            }

            if (keep == null)
            {
                throw new ArgumentNullException(nameof(keep));
            }

            return Simplify(polygon, keep, null);
        }

        /// <inheritdoc />
        public GeodeticPolygon2d Simplify(GeodeticPolygon2d polygon,
            GeodeticPolygon2d preserveEdges)
        {
            if (polygon == null)
            {
                throw new ArgumentNullException(nameof(polygon));
            }

            if (preserveEdges == null)
            {
                throw new ArgumentNullException(nameof(preserveEdges));
            }

            return Simplify(polygon, null, preserveEdges);
        }

        /// <inheritdoc />
        public abstract GeodeticPolygon2d Simplify(GeodeticPolygon2d polygon,
            IList<IList<bool>> keep, GeodeticPolygon2d preserveEdges);
    }
}