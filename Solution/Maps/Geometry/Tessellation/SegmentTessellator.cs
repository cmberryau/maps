using System;
using Maps.Geographical.Features;
using Maps.Geographical.Projection;

namespace Maps.Geometry.Tessellation
{
    /// <summary>
    /// Responsible for tessellating segments
    /// </summary>
    public class SegmentTessellator
    {
        private readonly ILineTessellator _tessellator;

        /// <summary>
        /// Initializes a new instance of SegmentTessellator
        /// </summary>
        /// <param name="tessellator">The geometric tessellator to use</param>
        public SegmentTessellator(ILineTessellator tessellator)
        {
            if (tessellator == null)
            {
                throw new ArgumentNullException(nameof(tessellator));
            }

            _tessellator = tessellator;
        }

        /// <summary>
        /// Tessellates a given segment
        /// </summary>
        /// <param name="segment">The segment to tessellate</param>
        /// <param name="projection">The projection for projecting the segment</param>
        public Mesh Tessellate(Segment segment, IProjection projection)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            var projectedPoints = projection.Forward(segment.LineStrip);
            return _tessellator.Tessellate(projectedPoints);
        }
    }
}