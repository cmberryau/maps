using System;
using Maps.Geometry;

namespace Maps.Geographical.Projection
{
    /// <summary>
    /// Base functionality for projecting geographical elements onto a 2d surface
    /// </summary>
    public abstract class Projection2d : Projection
    {
        private readonly Plane3d _plane;

        /// <summary>
        /// Initializes a new instance of Projection2d
        /// </summary>
        protected Projection2d()
        {
            _plane = new Plane3d(Vector3d.Zero, Vector3d.Back);
        }

        /// <summary>
        /// Initializes a new instance of Projection2d
        /// </summary>
        /// <param name="plane">The plane used for intersection resolution</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="plane"/> 
        /// is null</exception>
        protected Projection2d(Plane3d plane)
        {
            if (plane == null)
            {
                throw new ArgumentNullException(nameof(plane));
            }

            _plane = plane;
        }

        /// <inheritdoc />
        public override bool Intersection(Ray3d ray, out Vector3d point)
        {
            if (ray == null)
            {
                throw new ArgumentNullException(nameof(ray));
            }

            if (!ray.Intersection(_plane, out Vector3d intersectionPoint) ||
                intersectionPoint == Vector3d.NaN)
            {
                point = Vector3d.Zero;
                return false;
            }

            point = intersectionPoint;
            return true;
        }

        /// <inheritdoc />
        public override bool Intersection(Ray3d ray, out Geodetic3d coordinate)
        {
            if (ray == null)
            {
                throw new ArgumentNullException(nameof(ray));
            }

            if (Intersection(ray, out Vector3d point))
            {
                coordinate = Reverse(point);
                return true;
            }

            coordinate = Geodetic3d.NaN;
            return false;
        }
    }
}