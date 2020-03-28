using System;

namespace Maps.Geometry
{
    /// <summary>
    /// 3 dimensional plane with double precision
    /// </summary>
    public class Plane3d
    {
        /// <summary>
        /// A point on the plane
        /// </summary>
        public readonly Vector3d P0;

        /// <summary>
        /// The normal of the plane
        /// </summary>
        public readonly Vector3d Normal;

        /// <summary>
        /// Initializes a new instance of Plane3d
        /// </summary>
        /// <param name="p0">A point on the plane</param>
        /// <param name="normal">The normal of the plane</param>
        public Plane3d(Vector3d p0, Vector3d normal)
        {
            if (normal == Vector3d.Zero)
            {
                throw new ArgumentException("Cannot be a zero vector",
                    nameof(normal));
            }

            P0 = p0;
            Normal = normal;
        }
    }
}