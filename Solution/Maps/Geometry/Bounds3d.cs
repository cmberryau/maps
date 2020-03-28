using System;
using System.Collections.Generic;

namespace Maps.Geometry
{
    /// <summary>
    /// 3 dimensional axis aligned bounding box with double precision
    /// </summary>
    public struct Bounds3d
    {
        /// <summary>
        /// Bounds at the origin with zero size
        /// </summary>
        public static readonly Bounds3d Zero = new Bounds3d(Vector3d.Zero, Vector3d.Zero);

        /// <summary>
        /// Bounds at the origin with unit size
        /// </summary>
        public static readonly Bounds3d One = new Bounds3d(Vector3d.Zero, Vector3d.One);

        /// <summary>
        /// The maximum point of the bounding box
        /// </summary>
        public Vector3d Max => Centre + Extents;

        /// <summary>
        /// The minimum point of the bounding box
        /// </summary>
        public Vector3d Min => Centre - Extents;

        /// <summary>
        /// The centre of the bounding box
        /// </summary>
        public readonly Vector3d Centre;

        /// <summary>
        /// The extents of the bounding box
        /// </summary>
        public readonly Vector3d Extents;

        /// <summary>
        /// Initializes a new instance of Bounds3d
        /// </summary>
        /// <param name="centre">The centre of the bounds</param>
        /// <param name="size">The size of the bounds</param>
        public Bounds3d(Vector3d centre, Vector3d size)
        {
            Centre = centre;
            Extents = size * 0.5;
        }

        /// <summary>
        /// Initializes a new instance of Bounds3d
        /// </summary>
        /// <param name="points">The points to create the bounds from</param>
        public Bounds3d(IList<Vector3d> points)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            var max = Vector3d.MinValue;
            var min = Vector3d.MaxValue;

            for (var i = 0; i < points.Count; i++)
            {
                max = Vector3d.Max(points[i], max);
                min = Vector3d.Min(points[i], min);
            }

            Centre = Vector3d.Midpoint(max, min);
            Extents = (max - min) * 0.5;
        }

        /// <summary>
        /// Evaluates if the bounds contains the subject point
        /// </summary>
        /// <param name="bounds">The bounds to evaluate</param>
        /// <param name="subject">The subject point to evaluate</param>
        public static bool Contains(Bounds3d bounds, Vector3d subject)
        {
            return subject.x <= bounds.Max.x && 
                   subject.y <= bounds.Max.y && 
                   subject.z <= bounds.Max.z &&
                   subject.x >= bounds.Min.x && 
                   subject.y >= bounds.Min.y && 
                   subject.z >= bounds.Min.z;
        }

        /// <summary>
        /// Evaluates if the bounds contains the subject point
        /// </summary>
        /// <param name="bounds">The bounds to evaluate</param>
        /// <param name="subject">The subject point to evaluate</param>
        /// <param name="error">The magnitude of allowed error</param>
        public static bool Contains(Bounds3d bounds, Vector3d subject,
            double error)
        {
            return Contains(new Bounds3d(bounds.Centre, bounds.Extents * 2 +
                new Vector3d(error) * 0.5), subject);
        }

        /// <summary>
        /// Evaluates if the bounds contains the subject bounds
        /// </summary>
        /// <param name="bounds">The bounds to evaluate</param>
        /// <param name="subject">The subject bounds</param>
        public static bool Contains(Bounds3d bounds, Bounds3d subject)
        {
            return Contains(bounds, subject.Max) &&
                   Contains(bounds, subject.Min);
        }

        /// <summary>
        /// Evaluates if the bounds contains the subject bounds
        /// </summary>
        /// <param name="bounds">The bounds to evaluate</param>
        /// <param name="subject">The subject bounds</param>
        /// <param name="error">The magnitude of allowed error</param>
        public static bool Contains(Bounds3d bounds, Bounds3d subject, 
            double error)
        {
            return Contains(bounds, subject.Max, error) &&
                   Contains(bounds, subject.Min, error);
        }

        /// <summary>
        /// Evaluates if the bounds intersects the given bounds
        /// </summary>
        /// <param name="bounds">The bounds to evaluate</param>
        /// <param name="subject">The subject bounds to evaluate</param>
        public static bool Intersects(Bounds3d bounds, Bounds3d subject)
        {
            return bounds.Min.x <= subject.Max.x &&
                   bounds.Max.x >= subject.Min.x &&
                   bounds.Min.y <= subject.Max.y &&
                   bounds.Max.y >= subject.Min.y &&
                   bounds.Min.z <= subject.Max.z &&
                   bounds.Max.z >= subject.Min.z;
        }

        /// <summary>
        /// Returns the addition of the two bounds, a bounds which contains both
        /// given bounds
        /// </summary>
        public static Bounds3d operator +(Bounds3d lhs, Bounds3d rhs)
        {
            var max = Vector3d.Max(lhs.Max, rhs.Max);
            var min = Vector3d.Min(lhs.Min, rhs.Min);

            var centre = Vector3d.Midpoint(max, min);
            var size = Vector3d.ComponentDistance(max, min);

            return new Bounds3d(centre, size);
        }

        /// <summary>
        /// Returns the addition of a bounds and a point, a bounds which contains
        /// both the original bounds and the point 
        /// </summary>
        public static Bounds3d operator +(Bounds3d lhs, Vector3d p)
        {
            var max = Vector3d.Max(lhs.Max, p);
            var min = Vector3d.Min(lhs.Min, p);

            var centre = Vector3d.Midpoint(max, min);
            var size = Vector3d.ComponentDistance(max, min);

            return new Bounds3d(centre, size);
        }
    }
}