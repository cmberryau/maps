namespace Maps.Geometry
{
    /// <summary>
    /// Contains information about line intersections
    /// </summary>
    public struct LineIntersection
    {
        /// <summary>
        /// A decription of the different types of potential line intersections
        /// </summary>
        public enum Intersection
        {
            /// <summary>
            /// Represents no intersection
            /// </summary>
            NoIntersection,

            /// <summary>
            /// Represents a single point of intersection
            /// </summary>
            SinglePoint,

            /// <summary>
            /// Represents a colinear intersection
            /// </summary>
            Colinear,
        }

        /// <summary>
        /// Initializes a new instance of IntersectionInfo for the case
        /// where there is no intersection
        /// </summary>
        public static LineIntersection None => new LineIntersection(Intersection.NoIntersection);

        /// <summary>
        /// Was there an intersection?
        /// </summary>
        public bool Intersected => Type != Intersection.NoIntersection;

        /// <summary>
        /// The type of intersection
        /// </summary>
        public readonly Intersection Type;

        /// <summary>
        /// The first point of intersection
        /// </summary>
        public readonly Vector2d A;

        /// <summary>
        /// The second point of intersection
        /// </summary>
        public readonly Vector2d B;

        private LineIntersection(Intersection type)
        {
            Type = Intersection.NoIntersection;
            A = Vector2d.Zero;
            B = Vector2d.Zero;
        }

        /// <summary>
        /// Initializes a new instance of IntersectionInfo
        /// </summary>
        /// <param name="a">The intersection point</param>
        public LineIntersection(Vector2d a)
        {
            Type = Intersection.SinglePoint;
            A = a;
            B = Vector2d.Zero;
        }

        /// <summary>
        /// Initializes a new instance of IntersectionInfo
        /// </summary>
        /// <param name="a">The first intersection point</param>
        /// <param name="b">The second intersection point</param>
        public LineIntersection(Vector2d a, Vector2d b)
        {
            Type = Intersection.Colinear;
            A = a;
            B = b;
        }

        /// <summary>
        /// Implicitly defines a bool check against the IntersectionInfo struct
        /// </summary>
        /// <param name="info">The IntersectionInfo struct to evaluate</param>
        public static implicit operator bool(LineIntersection info)
        {
            return info.Intersected;
        }
    }
}