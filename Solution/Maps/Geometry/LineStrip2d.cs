using System;
using System.Collections;
using System.Collections.Generic;
using Maps.Extensions;
using Maps.Geographical;

namespace Maps.Geometry
{
    /// <summary>
    /// 2 dimensional immutable line strip with double precision
    /// </summary>
    public sealed class LineStrip2d : IReadOnlyList<Vector2d>
    {
        /// <summary>
        /// Indexer access to points
        /// </summary>
        public Vector2d this[int index] => _points[index];

        /// <summary>
        /// Number of points in the linestrip
        /// </summary>
        public int Count => _points.Length;

        /// <summary>
        /// Number of segments in the linestrip
        /// </summary>
        public int SegmentCount
        {
            get
            {
                if (_segments == null)
                {
                    _segments = CreateSegments();
                }

                return _segments.Length;
            }
        }

        /// <summary>
        /// The total distance of the linestrip
        /// </summary>
        public double Distance
        {
            get
            {
                if (!_distancesResolved)
                {
                    ResolveDistances();
                }

                return _distance;
            }
        }

        /// <summary>
        /// The bounds of the linestrip
        /// </summary>
        public Bounds2d Bounds
        {
            get
            {
                if (!_boundsResolved)
                {
                    ResolveBounds();
                }

                return _bounds;
            }
        }

        /// <summary>
        /// Is the linestrip clockwise? (when treated as a closed strip)
        /// </summary>
        public bool Clockwise
        {
            get
            {
                if (!_clockwiseResolved)
                {
                    ResolveClockwise();
                }

                return _clockwise;
            }
        }

        /// <summary>
        /// Is the linestrip convex? (when treated as a closed strip)
        /// </summary>
        public bool Convex
        {
            get
            {
                if (!_convexResolved)
                {
                    ResolveConvex();
                }

                return _convex;
            }
        }

        /// <summary>
        /// Is the linestrip closed?
        /// </summary>
        public bool Closed => _points[0] == _points[_points.Length - 1];

        private const double JoinThreshold = Mathd.EpsilonE14;

        private readonly Vector2d[] _points;
        private LineSegment2d[] _segments;

        private bool _convex;
        private bool _convexResolved;

        private bool _clockwise;
        private bool _clockwiseResolved;

        private Bounds2d _bounds;
        private bool _boundsResolved;

        private double _distance;
        private double[] _distanceAt;
        private bool _distancesResolved;

        private double[] _polarAngleAt;
        private bool _polarAnglesResolved;

        /// <summary>
        /// Initializes a new instance of LineStrip2d from a list of points
        /// </summary>
        /// <param name="points">The list of points</param>
        public LineStrip2d(IList<Vector2d> points) : this(points, false){}

        /// <summary>
        /// Initializes a new instance of LineStrip2d from a list of points
        /// </summary>
        /// <param name="points">The list of points</param>
        /// <param name="close">Should the linestrip be forced closed?</param>
        internal LineStrip2d(IList<Vector2d> points, bool close) : this()
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            if (points.Count < 2)
            {
                throw new ArgumentException("Must provide more than one",
                    nameof(points));
            }

            // not closed and should be closed
            var willBeClosed = points[0] != points[points.Count - 1] && close;
            var pointsCount = willBeClosed ? points.Count + 1 : points.Count;

            // copy over all points
            _points = new Vector2d[pointsCount];

            for (var i = 0; i < points.Count; i++)
            {
                _points[i] = points[i];
            }

            // if its to be closed, close it
            if (willBeClosed)
            {
                _points[pointsCount - 1] = points[0];
            }
        }

        /// <summary>
        /// Initializes a new instance of LineStrip2d from line segments
        /// </summary>
        /// <param name="segments">The array of segments</param>
        public LineStrip2d(LineSegment2d[] segments) : this()
        {
            if (segments == null)
            {
                throw new ArgumentNullException(nameof(segments));
            }

            if (segments.Length <= 0)
            {
                throw new ArgumentException("Must provide at least one" +
                                            " segment", nameof(segments));
            }

            // segments count + 1 for the final point
            _points = new Vector2d[segments.Length + 1];

            for (var i = 0; i < segments.Length; i++)
            {
                if (segments[i] == null)
                {
                    throw new ArgumentNullException("Contains null element at" +
                                                    $"index {i}", nameof(segments));
                }

                // copy over the first point of each segment
                _points[i] = segments[i].P0;
            }

            // the final point is the second point  of the final segment
            _points[_points.Length - 1] = segments[segments.Length - 1].P1;

            // copy over the segments
            _segments = new LineSegment2d[segments.Length];
            Array.Copy(segments, 0, _segments, 0, segments.Length);
        }

        /// <summary>
        /// Initializes a new instance of LineStrip2d from coordinates
        /// </summary>
        /// <param name="coordinates">The coordinates</param>
        public LineStrip2d(IList<Geodetic2d> coordinates) : this(coordinates, false){}

        /// <summary>
        /// Initializes a new instance of LineStrip2d from coordinates
        /// </summary>
        /// <param name="coordinates">The coordinates</param>
        /// <param name="close">Should the linestrip be forced closed?</param>
        internal LineStrip2d(IReadOnlyList<Geodetic2d> coordinates, bool close) : this()
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            // not closed and should be closed
            var willBeClosed = coordinates[0] != coordinates[coordinates.Count - 1] && close;
            var pointsCount = willBeClosed ? coordinates.Count + 1 : coordinates.Count;

            // copy over all points
            _points = new Vector2d[pointsCount];

            for (var i = 0; i < coordinates.Count; i++)
            {
                _points[i] = new Vector2d(coordinates[i]);
            }

            // if its to be closed, close it
            if (willBeClosed)
            {
                _points[pointsCount - 1] = new Vector2d(coordinates[0]);
            }
        }

        /// <summary>
        /// Initializes a new instance of LineStrip2d from coordinates
        /// </summary>
        /// <param name="coordinates">The coordinates</param>
        /// <param name="close">Should the linestrip be forced closed?</param>
        internal LineStrip2d(IList<Geodetic2d> coordinates, bool close) : this()
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            // not closed and should be closed
            var willBeClosed = coordinates[0] != coordinates[coordinates.Count - 1] && close;
            var pointsCount = willBeClosed ? coordinates.Count + 1 : coordinates.Count;

            // copy over all points
            _points = new Vector2d[pointsCount];

            for (var i = 0; i < coordinates.Count; i++)
            {
                _points[i] = new Vector2d(coordinates[i]);
            }

            // if its to be closed, close it
            if (willBeClosed)
            {
                _points[pointsCount - 1] = new Vector2d(coordinates[0]);
            }
        }

        /// <summary>
        /// Evaluates the point along the line at the given time
        /// </summary>
        /// <param name="t">The time to evaluate</param>
        public Vector2d PointAlongAt(double t)
        {
            if (t >= 1d)
            {
                return _points[_points.Length - 1];
            }

            if (t <= 0)
            {
                return _points[0];
            }

            if (!_distancesResolved)
            {
                ResolveDistances();
            }

            // total distance along the line we should travel
            var distanceToTravel = t * _distance;
            var distanceSoFar = 0d;

            int i;
            for (i = 0; i < _distanceAt.Length; ++i)
            {
                if (distanceToTravel == _distanceAt[i])
                {
                    return _points[i];
                }

                if (distanceToTravel > _distanceAt[i] &&
                    distanceToTravel < _distanceAt[i + 1])
                {
                    break;
                }

                distanceSoFar = _distanceAt[i + 1];
            }

            var distBetweenPoints = Vector2d.Distance(_points[i], _points[i + 1]);
            var remainingDistance = distanceToTravel - distanceSoFar;
            var tBetweenPoints = remainingDistance / distBetweenPoints;

            return Vector2d.Lerp(_points[i], _points[i + 1], 
                tBetweenPoints);
        }

        /// <summary>
        /// Returns the next point at the time given
        /// </summary>
        /// <param name="t">The time to evaluate</param>
        public Vector2d NextPointAt(double t)
        {
            if (t >= 1d)
            {
                return _points[_points.Length - 1];
            }

            if (t <= 0)
            {
                return _points[0];
            }

            if (!_distancesResolved)
            {
                ResolveDistances();
            }

            // total distance along the line we should travel
            var distanceToTravel = t * _distance;

            int i;
            for (i = 0; i < _distanceAt.Length; ++i)
            {
                // if it lands between two points
                if (distanceToTravel >= _distanceAt[i] &&
                    distanceToTravel < _distanceAt[i + 1])
                {
                    return _points[i + 1];
                }
            }

            throw new InvalidOperationException("Unexpectedly reached end " +
                                                "of method.");
        }

        /// <summary>
        /// Returns the distance at the given index
        /// </summary>
        /// <param name="index">The index to evaluate</param>
        public double DistanceAt(int index)
        {
            if (!_distancesResolved)
            {
                ResolveDistances();
            }

            if (index > _distanceAt.Length - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return _distanceAt[index];
        }

        /// <summary>
        /// Returns the polar angle of the line at the given time
        /// </summary>
        /// <param name="t">The time to evaluate</param>
        public double PolarAngleAt(double t)
        {
            if (!_polarAnglesResolved)
            {
                ResolveAngles();
            }

            if (t >= 1d)
            {
                return _polarAngleAt[_points.Length - 1];
            }

            if (t <= 0)
            {
                return _polarAngleAt[0];
            }

            return _polarAngleAt[ClosestIndexTo(t)];
        }

        /// <summary>
        /// Returns the closest index to the given point in the case of two
        /// or more indices being equidistant it will return the first of
        /// the indices
        /// </summary>
        /// <param name="p">The point to evaluate</param>
        public int ClosestIndexTo(Vector2d p)
        {
            var min = double.MaxValue;
            var minIndex = 0;

            for (var i = 0; i < _points.Length; ++i)
            {
                var dist = Vector2d.Distance(p, _points[i]);

                if (dist < min)
                {
                    min = dist;
                    minIndex = i;
                }
            }

            return minIndex;
        }

        /// <summary>
        /// Returns the closest point to the given point in the case of two
        /// or more points being equidistant it will return the first of 
        /// the points
        /// </summary>
        /// <param name="p">The point to evaluate</param>
        public Vector2d ClosestPoint(Vector2d p)
        {
            return _points[ClosestIndexTo(p)];
        }

        /// <summary>
        /// Returns the closest time along the line to the given point
        /// </summary>
        /// <param name="p">The point to evaluate</param>
        public double ClosestTimeTo(Vector2d p)
        {
            // minimum distance from p to ab
            var minDist = double.MaxValue;
            // minimum p time projected between a and b
            double minT = 0d;
            // index of minimum distance
            var minIndex = 0;

            for (var i = 0; i < _points.Length - 1; ++i)
            {
                var a = _points[i];
                var b = _points[i + 1];

                var ab = b - a;
                var ap = p - a;

                var sqrMagnitude = ab.SqrMagnitude;
                var t = 0d;
                double dist;

                // if ab size is zero
                if (sqrMagnitude == 0d)
                {
                    dist = ap.Magnitude;
                }
                else
                {
                    // p's time projected between a and b
                    t = Vector2d.Dot(ap, ab) / sqrMagnitude;

                    // projected behind a
                    if (t <= 0d)
                    {
                        dist = ap.Magnitude;
                    }
                    // projected infront of b
                    else if (t >= 1d)
                    {
                        dist = Vector2d.Distance(b, p);
                    }
                    // projected inbetween a and b
                    else
                    {
                        var projectedPoint = a + t * ab;
                        dist = Vector2d.Distance(p, projectedPoint);
                    }
                }

                // if the minimum distance is updated, update the 
                // tracking variables
                if (dist < minDist)
                {
                    minDist = dist;
                    minIndex = i;
                    minT = Mathd.Clamp01(t);
                }
            }

            // result is distance at (minimum index + distance to next * t)
            // / Distance
            return (DistanceAt(minIndex) +
                   (DistanceAt(minIndex + 1) - 
                   DistanceAt(minIndex)) * minT) / Distance;
        }

        /// <summary>
        /// Returns the closest point along the line to the given point
        /// </summary>
        /// <param name="p">The point to evaluate</param>
        public Vector2d ClosestPointAlong(Vector2d p)
        {
            // minimum distance from p to linestrip
            var minDist = double.MaxValue;
            // point with minimum distance from p to linestrip
            Vector2d minPoint = Vector2d.NaN;

            for (var i = 0; i < _points.Length - 1; ++i)
            {
                var a = _points[i];
                var b = _points[i + 1];

                var ab = b - a;
                var ap = p - a;

                var sqrMagnitude = ab.SqrMagnitude;
                Vector2d point;
                double dist;

                // if ab size is zero
                if (sqrMagnitude == 0d)
                {
                    dist = ap.Magnitude;
                    point = a;
                }
                else
                {
                    // p's time projected between a and b
                    var t = Vector2d.Dot(ap, ab) / sqrMagnitude;

                    // projected behind a
                    if (t <= 0d)
                    {
                        dist = ap.Magnitude;
                        point = a;
                    }
                    // projected infront of b
                    else if (t >= 1d)
                    {
                        dist = Vector2d.Distance(b, p);
                        point = b;
                    }
                    // projected inbetween a and b
                    else
                    {
                        point = a + t * ab;
                        dist = Vector2d.Distance(p, point);
                    }
                }

                // if the minimum distance is updated, 
                // update the tracking variables
                if (dist < minDist)
                {
                    minDist = dist;
                    minPoint = point;
                }
            }

            // ensure minimum point has been reassigned, should have
            if (Vector2d.IsNaN(minPoint))
            {
                throw new InvalidOperationException("Minimum point is NaN.");
            }

            return minPoint;
        }

        /// <summary>
        /// Returns the least distance from the linestrip to the given point
        /// </summary>
        /// <param name="p">The point to evaluate</param>
        public double LeastDistanceTo(Vector2d p)
        {
            // minimum distance from p to linestrip
            var minDist = double.MaxValue;

            for (var i = 0; i < _points.Length - 1; ++i)
            {
                var a = _points[i];
                var b = _points[i + 1];

                var ab = b - a;
                var ap = p - a;

                var sqrmag = ab.SqrMagnitude;
                double dist;

                // if ab size is zero
                if (sqrmag == 0d)
                {
                    dist = ap.Magnitude;
                }
                else
                {
                    // p's time projected between a and b
                    var t = Vector2d.Dot(ap, ab) / sqrmag;

                    // projected behind a
                    if (t <= 0d)
                    {
                        dist = ap.Magnitude;
                    }
                    // projected infront of b
                    else if (t >= 1d)
                    {
                        dist = Vector2d.Distance(b, p);
                    }
                    // projected inbetween a and b
                    else
                    {
                        var point = a + t * ab;
                        dist = Vector2d.Distance(p, point);
                    }
                }

                // if the minimum distance is updated, 
                // update the tracking variables
                if (dist < minDist)
                {
                    minDist = dist;
                }
            }

            return minDist;
        }

        /// <summary>
        /// Returns a copy of the line segments that make up the linestrip
        /// </summary>
        public LineSegment2d[] SegmentsCopy()
        {
            // if anything has changed, resolve them
            if (!_polarAnglesResolved)
            {
                ResolveAngles();
            }

            if (!_boundsResolved)
            {
                ResolveBounds();
            }

            if (!_distancesResolved)
            {
                ResolveDistances();
            }

            // if we don't have segments, create them
            if (_segments == null)
            {
                _segments = CreateSegments();
            }

            var segmentsCopy = new LineSegment2d[_segments.Length];
            Array.Copy(_segments, 0, segmentsCopy, 0, _segments.Length);

            return segmentsCopy;
        }

        /// <summary>
        /// Returns a copy of the points that make up the linestrip
        /// </summary>
        public Vector2d[] PointsCopy()
        {
            var verticesCopy = new Vector2d[_points.Length];
            Array.Copy(_points, 0, verticesCopy, 0, _points.Length);

            return verticesCopy;
        }

        /// <summary>
        /// Returns a linestrip with the given clockwiseness. A copy of 
        /// itself if the parameter is different to the original 
        /// clockwiseness, otherwise the same linestrip is returned. The
        /// linestrip is treated as a closed strip during this method.
        /// </summary>
        /// <param name="clockwise">true == clockwise, false == counter</param>
        public LineStrip2d ToClockwise(bool clockwise)
        {
            // just return itself its already clockwise
            if (Clockwise == clockwise)
            {
                return this;
            }

            // reverse the points and return a new linestrip
            var points = new Vector2d[_points.Length];

            for (var i = 0; i < points.Length; i++)
            {
                points[i] = _points[points.Length - 1 - i];
            }

            return new LineStrip2d(points);
        }

        /// <summary>
        /// Attempts to join the linestrip to the given linestrip, will join if the 
        /// host's final point is close enough to the subject's first point
        /// </summary>
        /// <param name="subject">The subject linestrip to attempt to join</param>
        /// <param name="joined">The resulting linestrip</param>
        /// <returns>True on successfully joined, false otherwise</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="subject"/> 
        /// is null</exception>
        public bool TryJoin(LineStrip2d subject, out LineStrip2d joined)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            joined = null;
            if (Equals(subject))
            {
                return false;
            }

            var result = false;

            // case a, sequential combine
            if (Vector2d.Distance(subject[0], this[Count - 1]) <= JoinThreshold)
            {
                // create the copy the points
                var points = new Vector2d[subject.Count + Count - 1];
                Array.Copy(_points, 0, points, 0, Count);
                Array.Copy(subject._points, 1, points, Count, subject.Count - 1);

                joined = new LineStrip2d(points);
                result = true;
            }
            // case b, facing each other
            else if (Vector2d.Distance(subject[subject.Count - 1], this[Count - 1]) 
                <= JoinThreshold)
            {
                var points = new Vector2d[subject.Count + Count - 1];
                Array.Copy(_points, 0, points, 0, Count);

                // reverse subject
                for (var i = 0; i < subject.Count - 1; ++i)
                {
                    points[Count + i] = subject[subject.Count - (2 + i)];
                }

                joined = new LineStrip2d(points);
                result = true;
            }
            // case c, facing away from each other
            else if (Vector2d.Distance(subject[0], this[0]) <= JoinThreshold)
            {
                var points = new Vector2d[subject.Count + Count - 1];
                Array.Copy(_points, 0, points, subject.Count - 1, Count);

                // reverse subject
                for (var i = 0; i < subject.Count - 1; ++i)
                {
                    points[i] = subject[subject.Count - (1 + i)];
                }

                joined = new LineStrip2d(points);
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Attempts to combine the given linestrips
        /// </summary>
        /// <param name="linestrips">The linestrips to combine</param>
        /// <returns>A list of potentially combined linestrips</returns>
        /// <exception cref="ArgumentNullException">Thrown if 
        /// <paramref name="linestrips"/> is null</exception>
        /// <exception cref="ArgumentException">Thrown if any element of 
        /// <paramref name="linestrips"/> is null </exception>
        public static IList<LineStrip2d> Combine(IList<LineStrip2d> linestrips)
        {
            if (linestrips == null)
            {
                throw new ArgumentNullException(nameof(linestrips));
            }

            linestrips.AssertNoNullEntries();

            if (linestrips.Count < 2)
            {
                return linestrips;
            }

            var stripCount = linestrips.Count;
            var hasJoined = new bool[stripCount];
            LineStrip2d joinedStrip = null;
            var joinCreated = false;

            // work through strips, looking for an initial join
            for (var i = 0; i < stripCount && !joinCreated; ++i)
            {
                for (var j = 0; j < stripCount && !joinCreated; ++j)
                {
                    // see if we can create a join
                    if (linestrips[i].TryJoin(linestrips[j], out joinedStrip))
                    {
                        // mark the joined strips
                        hasJoined[i] = true;
                        hasJoined[j] = true;
                        // mark to exit the loop
                        joinCreated = true;
                    }
                }
            }

            // if we got a join, go ahead and try to extend the strip
            if (joinCreated)
            {
                do
                {
                    joinCreated = false;
                    // work through non joined strips now and see if we can join them
                    for (var i = 0; i < stripCount; ++i)
                    {
                        if (!hasJoined[i])
                        {
                            LineStrip2d tempJoin;

                            // try a join on either end
                            if (joinedStrip.TryJoin(linestrips[i], out tempJoin) ||
                                linestrips[i].TryJoin(joinedStrip, out tempJoin))
                            {
                                hasJoined[i] = true;
                                joinCreated = true;
                                joinedStrip = tempJoin;
                            }
                        }
                    }
                    // if we created a join, go again.. we might be able to join more
                } while (joinCreated); // todo : add joined count early exit

                IList<LineStrip2d> result = new List<LineStrip2d>();

                // collect the unjoined strips
                for (var i = 0; i < stripCount; ++i) // todo : apply early exit with joined count
                {
                    if (!hasJoined[i])
                    {
                        result.Add(linestrips[i]);
                    }
                }

                // add our joined and return
                result.Add(joinedStrip);

                // try and join the unjoined strips
                if (result.Count > 0)
                {
                    result = Combine(result); // bug : goes into one recursion without need
                }

                return result;
            }

            return linestrips;
        }

        /// <summary>
        /// Returns the linestrip, relative to the given point
        /// </summary>
        /// <param name="point">The point to become relative to</param>
        /// <returns>A linestrip relative to the given point</returns>
        public LineStrip2d Relative(Vector2d point)
        {
            var relativePoints = new Vector2d[Count];

            for (var i = 0; i < Count; ++i)
            {
                relativePoints[i] = _points[i] - point;
            }

            return new LineStrip2d(relativePoints);
        }

        /// <summary>
        /// Returns the linestrip, reversing relativity to a given point
        /// </summary>
        /// <param name="point">The point to reverse relativity to</param>
        /// <returns>A linestrip relative to the given point</returns>
        public LineStrip2d Absolute(Vector2d point)
        {
            var absolutePoints = new Vector2d[Count];

            for (var i = 0; i < Count; ++i)
            {
                absolutePoints[i] = _points[i] + point;
            }

            return new LineStrip2d(absolutePoints);
        }

        /// <summary>
        /// Returns the segment at the given index
        /// </summary>
        /// <param name="index">The index of the segment to return</param>
        public LineSegment2d Segment(int index)
        {
            if (_segments == null)
            {
                _segments = CreateSegments();
            }

            return _segments[index];
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"N = {Count}, d = {Distance}";
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if(!(obj is LineStrip2d))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (LineStrip2d)obj;
            if (Count != other.Count)
            {
                return false;
            }

            var pointsCount = _points.Length;
            for (var i = 0; i < pointsCount; ++i)
            {
                if (_points[i] != other._points[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hash = _points[0].GetHashCode();

            unchecked
            {
                var pointsCount = _points.Length;
                for (var i = 1; i < pointsCount; ++i)
                {
                    hash = (hash * 397) ^ _points[i].GetHashCode();
                }

                return hash;
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public IEnumerator<Vector2d> GetEnumerator()
        {
            for (var i = 0; i < _points.Length; ++i)
            {
                yield return _points[i];
            }
        }

        private LineStrip2d(Vector2d[] points, bool clockwise) 
            : this(false, false, false, true)
        {
            // copy over all points
            _points = new Vector2d[points.Length];
            Array.Copy(points, 0, _points, 0, points.Length);

            _clockwise = clockwise;
            _clockwiseResolved = true;
        }

        private LineStrip2d(bool boundsResolved = false, 
                            bool polarAnglesResolved = false,
                            bool distancesResolved = false,
                            bool clockwiseResolved = false)
        {
            _boundsResolved = boundsResolved;
            _polarAnglesResolved = polarAnglesResolved;
            _distancesResolved = distancesResolved;
            _clockwiseResolved = clockwiseResolved;
        }

        private LineSegment2d[] CreateSegments()
        {
            var segments = new LineSegment2d[_points.Length - 1];

            for (var i = 1; i < _points.Length; i++)
            {
                segments[i - 1] = new LineSegment2d(_points[i - 1], _points[i]);
            }

            return segments;
        }

        private int ClosestIndexTo(double t)
        {
            if (!_distancesResolved)
            {
                ResolveDistances();
            }

            var distanceToTravel = t * _distance;

            int i;
            for (i = 0; i < _distanceAt.Length; i++)
            {
                // almost never lands on this with 0 & 1 prechecks
                if (distanceToTravel == _distanceAt[i])
                {
                    break;
                }

                if (distanceToTravel > _distanceAt[i] &&
                    distanceToTravel < _distanceAt[i + 1])
                {
                    break;
                }
            }

            return i;
        }

        private void ResolveBounds()
        {
            var boundsMin = Vector2d.MaxValue;
            var boundsMax = Vector2d.MinValue;

            for (var i = 0; i < _points.Length; ++i)
            {
                boundsMax = Vector2d.Max(boundsMax, _points[i]);
                boundsMin = Vector2d.Min(boundsMin, _points[i]);
            }

            _bounds = new Bounds2d(Vector2d.Midpoint(boundsMax, 
                boundsMin), Vector2d.Abs(boundsMax - boundsMin));

            _boundsResolved = true;
        }

        private void ResolveAngles()
        {
            if (_polarAngleAt == null)
            {
                _polarAngleAt = new double[_points.Length];
            }

            for (var i = 1; i < _points.Length; ++i)
            {
                _polarAngleAt[i - 1] = (_points[i] - _points[i - 1]).PolarAngle();
            }

            // append the the final angle on, just repeat 2nd last
            _polarAngleAt[_points.Length - 1] = 
                _polarAngleAt[_points.Length - 2];

            _polarAnglesResolved = true;
        }

        private void ResolveDistances()
        {
            if (_distanceAt == null)
            {
                _distanceAt = new double[_points.Length];
            }

            _distance = 0d;
            _distanceAt[0] = 0d;

            for (var i = 1; i < _points.Length; ++i)
            {
                _distance += Vector2d.Distance(_points[i - 1], _points[i]);
                _distanceAt[i] = _distance;
            }

            _distancesResolved = true;
        }

        private void ResolveClockwise()
        {
            _clockwise = Vector2d.Clockwise(_points);
            _clockwiseResolved = true;
        }

        private void ResolveConvex()
        {
            _convex = Vector2d.Convex(_points);
            _convexResolved = true;
        }
    }
}
