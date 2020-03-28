using System;
using System.Collections.Generic;
using Maps.Geometry;
using Maps.Rendering;

namespace Maps.Geographical.Projection
{
    /// <summary>
    /// Interface for projecting geographical elements
    /// </summary>
    public interface IProjection
    {
        /// <summary>
        /// The extents of the projection
        /// </summary>
        Vector3d Extents
        {
            get;
        }

        /// <summary>
        /// Evaluates the intersection between a ray and the projection surface
        /// </summary>
        /// <param name="ray">The ray to evaluate against</param>
        /// <param name="point">The output intersection point</param>
        /// <returns>True if a intersection occured, false otherwise</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="ray"/> 
        /// is null</exception>
        bool Intersection(Ray3d ray, out Vector3d point);

        /// <summary>
        /// Evaluates the intersection between a ray and the projection surface
        /// </summary>
        /// <param name="ray">The ray to evaluate against</param>
        /// <param name="coordinate">The output intersection coordinate</param>
        /// <returns>True if a intersection occured, false otherwise</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="ray"/> 
        /// is null</exception>
        bool Intersection(Ray3d ray, out Geodetic3d coordinate);

        /// <summary>
        /// Evaluates the visible of a given camera frustum on the projection surface
        /// </summary>
        /// <param name="frustum">The camera frustum to evaluate against</param>
        /// <param name="box">The output area</param>
        /// <returns>True if any area is visible, false otherwise</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="frustum"/> 
        /// is null</exception>
        bool VisibleArea(CameraFrustum frustum, out GeodeticBox2d box);

        /// <summary>
        /// Projects a coordinate to a 3d point
        /// </summary>
        /// <param name="coordinate">The coordinate to project</param>
        Vector3d Forward(Geodetic2d coordinate);

        /// <summary>
        /// Projects a coordinate to a 3d point
        /// </summary>
        /// <param name="coordinate">The coordinate to project</param>
        Vector3d Forward(Geodetic3d coordinate);

        /// <summary>
        /// Projects a point to a 3d coordinate
        /// </summary>
        /// <param name="point">The point to project</param>
        Geodetic3d Reverse(Vector3d point);

        /// <summary>
        /// Projects a list of 3d coordinates to 3d points
        /// </summary>
        /// <param name="coordinates">The coordinates to project</param>
        /// <exception cref="ArgumentNullException">Thrown if 
        /// <paramref name="coordinates"/> is null</exception>
        IList<Vector3d> Forward(IReadOnlyList<Geodetic2d> coordinates);

        /// <summary>
        /// Projects a list of 3d coordinates to 3d points
        /// </summary>
        /// <param name="coordinates">The coordinates to project</param>
        /// <exception cref="ArgumentNullException">Thrown if 
        /// <paramref name="coordinates"/> is null</exception>
        IList<Vector3d> Forward(IReadOnlyList<Geodetic3d> coordinates);

        /// <summary>
        /// Projects a list of 3d points to 3d coordinates
        /// </summary>
        /// <param name="points">The points to project</param>
        /// <exception cref="ArgumentNullException">Thrown if 
        /// <paramref name="points"/> is null</exception>
        IList<Geodetic3d> Reverse(IReadOnlyList<Vector3d> points);

        IList<Vector3d> Forward(GeodeticBox2d box);

        IList<Vector3d> Forward(GeodeticLineStrip2d strip);

        IList<IList<Vector3d>> Forward(GeodeticPolygon2d polygon);
    }
}