using System;
using System.Collections.Generic;
using System.Text;

namespace Maps.Geometry
{
    /// <summary>
    /// 2 dimensional line segment with double precision
    /// </summary>
    public sealed class LineSegment2d
    {
        /// <summary>
        /// First point of the line segment
        /// </summary>
        public readonly Vector2d P0;

        /// <summary>
        /// Second point of the line segment
        /// </summary>
        public readonly Vector2d P1;

        /// <summary>
        /// The direction of the line segment
        /// </summary>
        public readonly Vector2d Direction;

        /// <summary>
        /// The bounds of the line segment
        /// </summary>
        public readonly Bounds2d Bounds;

        /// <summary>
        /// Squared magnitude of the line segment
        /// </summary>
        public double SqrMagnitude => Direction.SqrMagnitude;

        /// <summary>
        /// The length of the line segment
        /// </summary>
        public double Length => Direction.Magnitude;

        /// <summary>
        /// Initializes a new instance of LineSegment2d
        /// </summary>
        /// <param name="p0">Point a of the line segment</param>
        /// <param name="p1">Point b of the line segment</param>
        public LineSegment2d(Vector2d p0, Vector2d p1)

        {
            P1 = p1;
            P0 = p0;
            Direction = P1 - P0;

            var max = Vector2d.Max(P0, P1);
            var min = Vector2d.Min(P0, P1);

            Bounds = new Bounds2d(Vector2d.Midpoint(max, min), Vector2d.Abs(max - min));
        }

        /// <summary>
        /// Evaluates the intersection between the given two line segments
        /// 
        /// Modified algorithm from:
        /// http://geomalgorithms.com/a05-_intersect-1.html#intersect2d_2Segments()
        /// </summary>
        /// <param name="b">The segment to evaluate against</param>
        public LineIntersection Intersection(LineSegment2d b)
        {
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            // early exit if no intersection between the bounds
            if (!Bounds2d.Intersects(Bounds, b.Bounds))
            {
                return LineIntersection.None;
            }

            var u = P1 - P0;
            var v = b.P1 - b.P0;
            var w = P0 - b.P0;
            var d = Vector2d.Cross(u, v);

            // test if vectors u, v are parallel (includes either being a point)
            if (Math.Abs(d) < Mathd.Epsilon)
            {
                // segments not colinear, parallel with no intersection
                if (Math.Abs(Vector2d.Cross(u, w)) > Mathd.Epsilon ||
                    Math.Abs(Vector2d.Cross(v, w)) > Mathd.Epsilon)
                {
                    return LineIntersection.None;
                }

                // they are colinear or degenerate
                var aMag = u.SqrMagnitude;
                var bMag = v.SqrMagnitude;

                // both are degenerate
                if (aMag < Mathd.Epsilon && bMag < Mathd.Epsilon)
                {
                    // distinct points
                    if (P0 != b.P0)
                    {
                        return LineIntersection.None;
                    }

                    // both points on top of each other
                    return new LineIntersection(P0);
                }

                // a segment a is a point
                if (aMag < Mathd.Epsilon)
                {
                    // if point is in segment b
                    if (ContainsColinearPoint(P0))
                    {
                        return new LineIntersection(P0);
                    }

                    // otherwise, disjoint
                    return LineIntersection.None;
                }

                // b segment b is a point
                if (bMag < Mathd.Epsilon)
                {
                    // if point is in segment a
                    if (ContainsColinearPoint(b.P0))
                    {
                        return new LineIntersection(b.P0);
                    }

                    // otherwise disjoint
                    return LineIntersection.None;
                }

                // they are colinear, find any overlap
                // endpoints of a in equation for b
                double t0, t1;
                var w2 = P1 - b.P0;
                if (Math.Abs(v.x) > 0)
                {
                    t0 = w.x / v.x;
                    t1 = w2.x / v.x;
                }
                else
                {
                    t0 = w.y / v.y;
                    t1 = w2.y / v.y;
                }

                // t0 should be smaller than t1, swap otherwise
                if (t0 > t1)
                {
                    var t = t0; t0 = t1; t1 = t;
                }

                // t0, t1 should fall within 0->1, otherwise disjoint
                if (t0 > 1 || t1 < 0)
                {
                    return LineIntersection.None;
                }

                // clamp between 0->1
                t0 = Mathd.Clamp01(t0);
                t1 = Mathd.Clamp01(t1);

                // intersection is a point
                if (Math.Abs(t1 - t0) < Mathd.Epsilon)
                {
                    return new LineIntersection(b.P0 + t0 * v);
                }

                // a, b overlap as a segment
                return new LineIntersection(b.P0 + t0 * v, b.P0 + t1 * v);
            }

            // the segments are skew and may intersect
            var si = Vector2d.Cross(v, w) / d;

            // intersect param for a
            if (si < 0 || si > 1)
            {
                // no intersection
                return LineIntersection.None;
            }

            // intersect param for b
            var ti = Vector2d.Cross(u, w) / d;
            if (ti < 0 || ti > 1)
            {
                // no intersection
                return LineIntersection.None;
            }

            // a intersect point
            return new LineIntersection(P0 + si * u);
        }

        /// <summary>
        /// Tests if the point colinear to the given segment is contained within it
        /// </summary>
        private bool ContainsColinearPoint(Vector2d a)
        {
            // if segment is not vertical, test x
            if (Math.Abs(P1.x - P0.x) > 0)
            {
                if ((a.x >= P0.x && a.x <= P1.x) ||
                    (a.x <= P0.x && a.x >= P1.x))
                {
                    return true;
                }
            }
            // segment is vertical, test y
            else
            { 
                if ((a.y >= P0.y && a.y <= P1.y) ||
                    (a.y <= P0.y && a.y >= P1.y))
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            //return $"p0({P0}) + t * (p1({P1}) - p0({P0}))";
            return $"p0({P0}) -> p1({P1})";
        }

        /// <summary>
        /// Outputs a list of segments to a plottable string
        /// </summary>
        /// <param name="segments"></param>
        /// <returns></returns>
        public static string ToPlottableString(IList<LineSegment2d> segments)
        {
            var sb = new StringBuilder();

            sb.Append("[");

            foreach (var segment in segments)
            {
                sb.Append($"[{segment.P0.x}, {segment.P0.y}], ");
            }

            sb.Append($"[{segments[segments.Count - 1].P1.x}, {segments[segments.Count - 1].P1.y}]]");

            return sb.ToString();
        }
    }
}
