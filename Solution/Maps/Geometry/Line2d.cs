using System;
using log4net;

namespace Maps.Geometry
{
    /// <summary>
    /// 2 dimensional line with double precision
    /// </summary>
    public class Line2d
    {
        /// <summary>
        /// A point on the line
        /// </summary>
        public readonly Vector2d P0;

        /// <summary>
        /// The direction of the line
        /// </summary>
        public readonly Vector2d Direction;

        private static readonly ILog Log = LogManager.GetLogger(typeof(Line2d));

        /// <summary>
        /// Initializes a new instance of Line2d
        /// </summary>
        /// <param name="p0">A point on the line</param>
        /// <param name="direction">A direction vector for the line</param>
        public Line2d(Vector2d p0, Vector2d direction)
        {
            P0 = p0;
            Direction = direction;
        }

        /// <summary>
        /// Initializes a new instance of Line2d from the given line segment, essentially
        /// extending it infinitely in the direction it is currently in
        /// </summary>
        /// <param name="segment">The segment to create the Line2d instance from</param>
        public Line2d(LineSegment2d segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            P0 = segment.P0;
            Direction = segment.Direction;
        }

        /// <summary>
        /// Evaluates the relative position of the point to the line, will return
        /// RelativePosition.Centre when the point is colinear to the line
        /// </summary>
        /// <param name="point">The point to evaluate</param>
        public RelativePosition Side(Vector2d point)
        {
            var pxp = Vector2d.Cross(Direction, point - P0);

            if (pxp > Mathd.Epsilon)
            {
                return RelativePosition.Left;
            }

            if (pxp < -Mathd.Epsilon)
            {
                return RelativePosition.Right;
            }

            return RelativePosition.Centre;
        }

        /// <summary>
        /// Evaluates the intersection between the line and the given line segment
        /// 
        /// Uses cross product to resolve parallel case and to simplify 
        /// p + t * d = p0 + k * s where s = (p1 - p0)
        /// 
        /// Uses modified version of cross product simplification mentioned here:
        /// http://stackoverflow.com/questions/4030565/line-and-line-segment-intersection
        /// 
        /// Only solving for k due to t period being infinitely valid
        /// </summary>
        /// <param name="subject">The line segment to evaluate</param>
        public LineIntersection Intersection(LineSegment2d subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            return Intersection(subject, out double t, out double k);
        }

        /// <summary>
        /// Evaluates the intersection between the line and the given line segment
        /// 
        /// Uses cross product to resolve parallel case and to simplify 
        /// p + t * d = p0 + k * s where s = (p1 - p0)
        /// 
        /// Uses modified version of cross product simplification mentioned here:
        /// http://stackoverflow.com/questions/4030565/line-and-line-segment-intersection
        /// 
        /// Only solving for k due to t period being infinitely valid
        /// </summary>
        /// <param name="subject">The line segment to evaluate</param>
        /// <param name="t">The time along the line</param>
        /// <param name="k">The time along the subject segment</param>
        public LineIntersection Intersection(LineSegment2d subject, out double t, out double k)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            // if line and subject are parallel or subject could be a point
            if (Math.Abs(Vector2d.Cross(Direction, subject.Direction)) < Mathd.Epsilon)
            {
                // check for seperation between line and subject
                if (Math.Abs(Vector2d.Cross(Direction, subject.P0 - P0)) > 0d)
                {
                    // no intersection, time for both is NaN
                    t = double.NaN;
                    k = double.NaN;

                    // parallel and not intersecting
                    return LineIntersection.None;
                }

                // check if subject is a point which lies on the line
                if (subject.SqrMagnitude < Mathd.Epsilon)
                {
                    // time for subject is 0 because it has to length
                    k = 0d;

                    // determine line time using subject point, avoid div by 0
                    if (Math.Abs(Direction.x) > Mathd.Epsilon)
                    {
                        t = subject.P0.x - P0.x / Direction.x;
                    }
                    else if (Math.Abs(Direction.y) > Mathd.Epsilon)
                    {
                        t = subject.P0.y - P0.y / Direction.y;
                    }
                    else
                    {
                        // should not occur unless line has direction with 0 magnitude
                        t = 0d;
                        Log.Warn("Time along line returned as 0");
                    }

                    // no seperation, subject is point and lies on line, single intersection
                    return new LineIntersection(subject.P0);
                }

                // no seperation, subject is not point and lies on line, colinear intersection
                t = 0d;
                k = 0d;
                return new LineIntersection(subject.P0, subject.P1);
            }

            // determine k in p + d * t = p0 + k * (p1 - p0) by crossing both sides by d
            k = Vector2d.Cross(P0 - subject.P0, Direction) / Vector2d.Cross(subject.P1 - 
                subject.P0, Direction);

            // determine intersection using k
            var intersection = subject.P0 + k * subject.Direction;

            // k must fall within 0->1 or there is no intersection with subject
            if (k >= 0 && k <= 1)
            {
                // determine t using intersection
                if (Math.Abs(Direction.x) > Mathd.Epsilon)
                {
                    t = intersection.x - P0.x / Direction.x;
                }
                else if (Math.Abs(Direction.y) > Mathd.Epsilon)
                {
                    t = intersection.y - P0.y / Direction.y;
                }
                else
                {
                    // should not occur unless line has direction with 0 magnitude
                    t = 0d;
                    Log.Warn("Time along line returned as 0");
                }

                return new LineIntersection(intersection);
            }

            // no intersection, time for both is NaN
            t = double.NaN;
            k = double.NaN;

            // line and subject are skew and do not intersect
            return LineIntersection.None;
        }

        /// <summary>
        /// Evaluates the intersection between the line and the given line segment
        /// 
        /// Uses cross product to resolve parallel case and to simplify 
        /// p0 + d0 * t = p1 + d1 * k
        /// 
        /// Uses modified version of cross product simplification mentioned here:
        /// http://stackoverflow.com/questions/4030565/line-and-line-segment-intersection
        /// 
        /// Only solving for k due to t period being infinitely valid
        /// </summary>
        /// <param name="subject">The line segment to evaluate</param>
        public LineIntersection Intersection(Line2d subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            return Intersection(subject, out double t, out double k);
        }

        /// <summary>
        /// Evaluates the intersection between the line and the given line segment
        /// 
        /// Uses cross product to resolve parallel case and to simplify 
        /// p0 + d0 * t = p1 + d1 * k
        /// 
        /// Uses modified version of cross product simplification mentioned here:
        /// http://stackoverflow.com/questions/4030565/line-and-line-segment-intersection
        /// 
        /// Only solving for k due to t period being infinitely valid
        /// </summary>
        /// <param name="subject">The line segment to evaluate</param>
        /// <param name="t">The time along the line</param>
        /// <param name="k">The time along the subject line</param>
        public LineIntersection Intersection(Line2d subject, out double t, out double k)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            // if line and subject are parallel
            if (Math.Abs(Vector2d.Cross(Direction, subject.Direction)) < Mathd.Epsilon)
            {
                // check for seperation between line and subject
                if (Math.Abs(Vector2d.Cross(Direction, subject.P0 - P0)) > 0d)
                {
                    t = double.NaN;
                    k = double.NaN;

                    // parallel with no intersection
                    return LineIntersection.None;
                }

                // no seperation, subject lies on line, colinear intersection
                t = 0d;
                k = 0d;
                return new LineIntersection(subject.P0, P0);
            }

            // determine k in p0 + d0 * t = p1 + d1 * k by crossing both sides by d0
            k = Vector2d.Cross(P0 - subject.P0, Direction) / Vector2d.Cross(
                subject.Direction, Direction);

            // determine intersection using k
            var intersection = subject.P0 + k * subject.Direction;

            // determine t using intersection
            if (Math.Abs(Direction.x) > Mathd.Epsilon)
            {
                t = intersection.x - P0.x / Direction.x;
            }
            else if (Math.Abs(Direction.y) > Mathd.Epsilon)
            {
                t = intersection.y - P0.y / Direction.y;
            }
            else
            {
                // should not occur unless line has direction with 0 magnitude
                t = 0d;
                Log.Warn("Time along line returned as 0");
            }

            // line and subject are skew and intersect
            return new LineIntersection(intersection);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"p0({P0}) + t * d({Direction})";
        }
    }
}