using System;

namespace Maps.Geometry
{
    /// <summary>
    /// 3 dimensional line with double precision
    /// </summary>
    public class Line3d
    {
        /// <summary>
        /// A point on the line
        /// </summary>
        public readonly Vector3d P0;

        /// <summary>
        /// The direction of the line
        /// </summary>
        public readonly Vector3d Direction;

        /// <summary>
        /// Initializes a new instance of Line2d
        /// </summary>
        /// <param name="p0">A point on the line</param>
        /// <param name="direction">A direction vector for the line</param>
        public Line3d(Vector3d p0, Vector3d direction)
        {
            P0 = p0;
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

            // line and plane are parallel
            if (Vector3d.Dot(Direction, plane.Normal) < Mathd.Epsilon)
            {
                // line lies on plane
                if (Vector3d.Dot(P0 - plane.P0, plane.Normal) < Mathd.Epsilon)
                {
                    point = Vector3d.NaN;
                    return true;
                }

                point = Vector3d.NaN;
                return false;
            }

            var t = Vector3d.Dot(plane.P0 - P0, plane.Normal) /
                    Vector3d.Dot(Direction, plane.Normal);

            point = P0 + Direction * t;

            return true;
        }

        /// <summary>
        /// Evaluates the intersection time with a given plane
        /// See: https://en.wikipedia.org/wiki/Line%E2%80%93plane_intersection#Algebraic_form
        /// </summary>
        /// <param name="p0">A point on the line</param>
        /// <param name="direction">The direction of the line</param>
        /// <param name="plane">The plane to evaluate</param>
        /// <param name="time">The intersection time</param>
        internal static bool Intersection(Vector3d p0, Vector3d direction, 
            Plane3d plane, out double time)
        {
            // line and plane are parallel
            if (Math.Abs(Vector3d.Dot(direction, plane.Normal)) < Mathd.Epsilon)
            {
                // line lies on plane
                if (Math.Abs(Vector3d.Dot(p0 - plane.P0, plane.Normal)) < Mathd.Epsilon)
                {
                    time = double.NaN;
                    return true;
                }

                time = double.NaN;
                return false;
            }

            time = Vector3d.Dot(plane.P0 - p0, plane.Normal) /
                   Vector3d.Dot(direction, plane.Normal);

            return true;
        }

        /// <summary>
        /// Evaluates the intersection time with a given ellipsoid
        /// Good explaination of the quadratic equation steps: http://stackoverflow.com/questions/1986378/how-to-set-up-quadratic-equation-for-a-ray-sphere-intersection
        /// </summary>
        /// <param name="p0">A point on the line</param>
        /// <param name="direction">The direction of the line</param>
        /// <param name="ellipsoid">The ellipsoid to evaluate</param>
        /// <param name="time0">The first intersection time</param>
        /// <param name="time1">The second intersection time</param>
        internal static bool Intersection(Vector3d p0, Vector3d direction,
            Ellipsoid ellipsoid, out double time0, out double time1)
        {
            if (ellipsoid == null)
            {
                throw new ArgumentNullException(nameof(ellipsoid));
            }

            var p0Squared = Vector3d.Pow(p0, 2);
            var dirSquared = Vector3d.Pow(direction, 2);

            var a = Vector3d.Dot(dirSquared, ellipsoid.OneOverRadiiSquared);
            var b = 2 * Vector3d.Dot(Vector3d.ComponentMultiply(p0, direction), 
                ellipsoid.OneOverRadiiSquared);
            var c = Vector3d.Dot(p0Squared, ellipsoid.OneOverRadiiSquared) - 1;
            // discriminant
            var d = b * b - 4 * a * c;

            // no collision
            if (d < 0)
            {
                time0 = double.NaN;
                time1 = double.NaN;

                return false;
            }

            double t;

            // one collision
            if (Math.Abs(d) < Mathd.Epsilon)
            {
                t = -0.5 * b / a;

                time0 = t;
                time1 = t;

                return true;
            }

            // two collisions
            t = -0.5 * (b + (b > 0 ? 1 : -1) * Math.Sqrt(d));
            var root1 = t / a;
            var root2 = c / t;

            time0 = Math.Min(root1, root2);
            time1 = Math.Max(root1, root2);

            return true;
        }

        /// <summary>
        /// Evaluates the closest point on the ellipsoid to the line
        /// Good explaination: http://math.stackexchange.com/questions/895385/point-on-an-ellipsoid-closest-to-line
        /// </summary>
        /// <param name="p0">A point on the line</param>
        /// <param name="direction">The direction of the line</param>
        /// <param name="ellipsoid">The ellipsoid to evaluate</param>
        internal static Vector3d Closest(Vector3d p0, Vector3d direction,
            Ellipsoid ellipsoid)
        {
            if (ellipsoid == null)
            {
                throw new ArgumentNullException(nameof(ellipsoid));
            }

            var cpOrigin = -Vector3d.Cross(direction, Vector3d.Cross(direction, p0)) /
                              direction.SqrMagnitude;

            var planeNormal = cpOrigin / cpOrigin.Magnitude;

            var cpOriginSquared = Vector3d.Pow(cpOrigin, 2);

            var planeDistance =
                Math.Sqrt(ellipsoid.RadiiSquared.x * cpOriginSquared.x +
                    ellipsoid.RadiiSquared.y * cpOriginSquared.y +
                    ellipsoid.RadiiSquared.z * cpOriginSquared.z);

            var closest = 1 / planeDistance * 
                new Vector3d(ellipsoid.RadiiSquared.x * cpOrigin.x,
                             ellipsoid.RadiiSquared.y * cpOrigin.y,
                             ellipsoid.RadiiSquared.z * cpOrigin.z);

            return closest;
        }
    }
}