using System.Collections.Generic;
using Maps.Geographical;

namespace Maps.Geometry.Simplification
{
    /// <summary>
    /// Interface for implementers of geodetic simplification in 2 dimensions
    /// </summary>
    public interface IGeodeticSimplifier2d
    {
        /// <summary>
        /// Simplifies the given coordinates
        /// </summary>
        /// <param name="coordinates">The coordinates to simplify</param>
        /// <returns>The simplified coordinates</returns>
        IList<Geodetic2d> Simplify(IList<Geodetic2d> coordinates);

        /// <summary>
        /// Simplifies the given coordinates
        /// </summary>
        /// <param name="coordinates">The coordinates to simplify</param>
        /// <param name="keep">A list of booleans indicating which coordinates indices 
        /// must be kept</param>
        /// <returns>The simplified coordinates</returns>
        IList<Geodetic2d> Simplify(IList<Geodetic2d> coordinates, IList<bool> keep);

        /// <summary>
        /// Simplifies the given linestrip
        /// </summary>
        /// <param name="linestrip">The linestrip to simplify</param>
        /// <returns>The simplified linestrip</returns>
        GeodeticLineStrip2d Simplify(GeodeticLineStrip2d linestrip);

        /// <summary>
        /// Simplifies the given linestrip
        /// </summary>
        /// <param name="linestrip">The linestrip to simplify</param>
        /// <param name="keep">A list of booleans indicating which coordinate indices 
        /// must be kept</param>
        /// <returns>The simplified linestrip</returns>
        GeodeticLineStrip2d Simplify(GeodeticLineStrip2d linestrip, IList<bool> keep);

        /// <summary>
        /// Simplifies the given polygon
        /// </summary>
        /// <param name="polygon">The polygon to simplify</param>
        /// <returns>The simplified polygon</returns>
        GeodeticPolygon2d Simplify(GeodeticPolygon2d polygon);

        /// <summary>
        /// Simplifies the given polygon
        /// </summary>
        /// <param name="polygon">The polygon to simplify</param>
        /// <param name="keep">A list of booleans indicating which outer coordinate 
        /// indices must be kept</param>
        /// <returns>The simplified polygon</returns>
        GeodeticPolygon2d Simplify(GeodeticPolygon2d polygon, IList<IList<bool>> keep);

        /// <summary>
        /// Simplifies the given polygon
        /// </summary>
        /// <param name="polygon">The polygon to simplify</param>
        /// <param name="preserveEdges">A polygon describing edges which must be 
        /// preserved</param>
        /// <returns>The simplified polygon</returns>
        GeodeticPolygon2d Simplify(GeodeticPolygon2d polygon, GeodeticPolygon2d preserveEdges);

        /// <summary>
        /// Simplifies the given polygon
        /// </summary>
        /// <param name="polygon">The polygon to simplify</param>
        /// <param name="keep">A list of booleans indicating which outer coordinate 
        /// indices must be kept</param>
        /// <param name="preserveEdges">A polygon describing edges which must be 
        /// preserved</param>
        /// <returns>The simplified polygon</returns>
        GeodeticPolygon2d Simplify(GeodeticPolygon2d polygon, IList<IList<bool>> keep, GeodeticPolygon2d preserveEdges);
    }
}