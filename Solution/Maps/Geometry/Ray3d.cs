using System;

namespace Maps.Geometry
{
    /// <summary>
    /// 3 dimensional ray with double precision
    /// </summary>
    public class Ray3d
    {
        /// <summary>
        /// The origin of the ray
        /// </summary>
        public readonly Vector3d Origin;

        /// <summary>
        /// The direction of the ray
        /// </summary>
        public readonly Vector3d Direction;

        /// <summary>
        /// Initializes a new isntance of Ray3d 
        /// </summary>
        /// <param name="origin">The origin of the ray</param>
        /// <param name="direction">The direction of the ray</param>
        public Ray3d(Vector3d origin, Vector3d direction)
        {
            Origin = origin;
            Direction = direction;
        }

        /// <summary>
        /// Evaluates the intersection with a given plane
        /// See: https://en.wikipedia.org/wiki/Line%E2%80%93plane_intersection#Algebraic_form
        /// </summary>
        /// <param name="plane">The plane to evaluate</param>
        /// <param name="point">The intersection point</param>
        public bool Intersection(Plane3d plane, out Vector3d point)
        {
            if (plane == null)
            {
                throw new ArgumentNullException(nameof(plane));
            }

            double t;
            var intersects = Line3d.Intersection(Origin, Direction, plane, out t);

            if (double.IsNaN(t))
            {
                point = Vector3d.NaN;
            }
            else
            {
                // if t is behind the start of the ray, no intersection
                if (t < 0)
                {
                    point = Vector3d.NaN;
                    intersects = false;
                }
                else
                {
                    point = Origin + Direction * t;
                }
            }

            return intersects;
        }

        /// <summary>
        /// Evaluates the intersection with the given ellipsoid
        /// </summary>
        /// <param name="ellipsoid">The ellipsoid to evaluate</param>
        /// <param name="point0">The first intersection point</param>
        /// <param name="point1">The second intersection point</param>
        public bool Intersection(Ellipsoid ellipsoid, out Vector3d point0,
            out Vector3d point1)
        {
            if (ellipsoid == null)
            {
                throw new ArgumentNullException(nameof(ellipsoid));
            }

            double t0, t1;

            var intersects = Line3d.Intersection(Origin, Direction, ellipsoid,
                out t0, out t1);

            if (!intersects)
            {
                point0 = Vector3d.NaN;
                point1 = Vector3d.NaN;

                return intersects;
            }

            point0 = Origin + Direction * t0;
            point1 = Origin + Direction * t1;

            return intersects;
        }

        /// <summary>
        /// Evaluates the closest point on the ellipsoid to the ray
        /// Good explaination: http://math.stackexchange.com/questions/895385/point-on-an-ellipsoid-closest-to-line
        /// </summary>
        /// <param name="ellipsoid">The ellipsoid to evaluate</param>
        public Vector3d Closest(Ellipsoid ellipsoid)
        {
            if (ellipsoid == null)
            {
                throw new ArgumentNullException(nameof(ellipsoid));
            }

            return Line3d.Closest(Origin, Direction, ellipsoid);
        }
    }
}