using System;
using Maps.Geographical.Features;
using Maps.Geographical.Projection;

namespace Maps.Geometry.Tessellation
{
    /// <summary>
    /// Responsible for tessellating areas
    /// </summary>
    public class AreaTessellator
    {
        private readonly IPolygonTessellator _tessellator;

        /// <summary>
        /// Initializes a new instance of AreaTessellator
        /// </summary>
        /// <param name="tessellator">The geometric tessellator to use</param>
        public AreaTessellator(IPolygonTessellator tessellator)
        {
            if (tessellator == null)
            {
                throw new ArgumentNullException(nameof(tessellator));
            }

            _tessellator = tessellator;
        }

        /// <summary>
        /// Tessellates an area
        /// </summary>
        /// <param name="area">The area to tessellate</param>
        /// <param name="projection">The projection to project the area</param>
        public Mesh Tessellate(Area area, IProjection projection)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            var projectedPoints = projection.Forward(area.Polygon);
            return _tessellator.Tessellate(projectedPoints);
        }
    }
}