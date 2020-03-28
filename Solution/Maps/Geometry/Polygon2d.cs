using System;
using System.Collections;
using System.Collections.Generic;
using ClipperLib;
using Maps.Extensions;
using Maps.Geographical;

namespace Maps.Geometry
{
    /// <summary>
    /// Represents a 2 dimensional immutable polygon
    /// </summary>
    public sealed class Polygon2d : IReadOnlyList<Vector2d>
    {
        /// <inheritdoc />
        public Vector2d this[int index] => _outer[index];

        /// <inheritdoc />
        public int Count => _outer.Count;

        /// <summary>
        /// The outer polygon segment count
        /// </summary>
        public int SegmentCount => _outer.SegmentCount;

        /// <summary>
        /// The number of holes the polygon has
        /// </summary>
        public int HoleCount => _holes.Length;

        /// <summary>
        /// The area of the polygon
        /// </summary>
        public double Area
        {
            get
            {
                var a = _outer[0].x * _outer[1].y - _outer[1].x * _outer[0].y;
                int n, n1;
                for (var i = 1; i < _outer.Count - 2; ++i)
                {
                    n = i;
                    n1 = i + 1;
                    a += _outer[n].x * _outer[n1].y - _outer[n1].x * _outer[n].y;
                }

                var count = _outer.Count;

                n = count - 2;
                n1 = count - 1;
                a += _outer[n].x * _outer[n1].y - _outer[n1].x * _outer[n].y;
                n = count - 1;
                n1 = 0;
                a += _outer[n].x * _outer[n1].y - _outer[n1].x * _outer[n].y;

                return Math.Abs(a) * 0.5d;
            }
        }

        /// <summary>
        /// The bounds of the polygon
        /// </summary>
        public Bounds2d Bounds => _outer.Bounds;

        /// <summary>
        /// Are the polygons points oriented clockwise?
        /// </summary>
        public bool Clockwise => _outer.Clockwise;

        /// <summary>
        /// Is the polygon convex?
        /// </summary>
        public bool Convex => _outer.Convex;

        /// <summary>
        /// The linestrip of the outer points
        /// </summary>
        public LineStrip2d OuterLineStrip => _outer;

        private const double ClipperScaleFactor = 1e14d;
        private const double ClipperInverseScaleFactor = 1 / ClipperScaleFactor;

        private readonly LineStrip2d _outer;
        private readonly Polygon2d[] _holes;

        /// <summary>
        /// Initializes a new instance of Polygon2d
        /// </summary>
        /// <param name="points">The points of the polygon</param>
        /// <exception cref="ArgumentNullException">Thrown when points is 
        /// null</exception>
        /// <exception cref="ArgumentException">Thrown when insufficient points 
        /// given</exception>
        public Polygon2d(IList<Vector2d> points)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            if (points.Count < 3)
            {
                throw new ArgumentException("Requires at least 3 outer points",
                    nameof(points));
            }

            _outer = new LineStrip2d(points, true);
            _holes = new Polygon2d[0];
        }

        /// <summary>
        /// Initializes a new instance of Polygon2d
        /// </summary>
        /// <param name="coordinates">The coordinates of the polygon</param>
        /// <exception cref="ArgumentNullException">Thrown if coordinates is null 
        /// </exception>
        /// <exception cref="ArgumentException">Thrown if insufficient coordinates given 
        /// </exception>
        internal Polygon2d(IReadOnlyList<Geodetic2d> coordinates)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            if (coordinates.Count < 3)
            {
                throw new ArgumentException("Requires at least 3 outer points",
                    nameof(coordinates));
            }

            _outer = new LineStrip2d(coordinates, true);
            _holes = new Polygon2d[0];
        }

        /// <summary>
        /// Initializes a new instance of Polygon2d
        /// </summary>
        /// <param name="coordinates">The coordinates of the polygon</param>
        /// <exception cref="ArgumentNullException">Thrown if coordinates is null 
        /// </exception>
        /// <exception cref="ArgumentException">Thrown if insufficient coordinates given 
        /// </exception>
        internal Polygon2d(IList<Geodetic2d> coordinates)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            if (coordinates.Count < 3)
            {
                throw new ArgumentException("Requires at least 3 outer points",
                    nameof(coordinates));
            }

            _outer = new LineStrip2d(coordinates, true);
            _holes = new Polygon2d[0];
        }

        /// <summary>
        /// Initializes a new instance of Polygon2d
        /// </summary>
        /// <param name="points">The points of the polygon</param>
        /// <param name="holes">The holes of the poylgon</param>
        /// <exception cref="ArgumentNullException">Thrown when points or holes is 
        /// null</exception>
        /// <exception cref="ArgumentException">Thrown when insufficient points 
        /// given or holes contains a null element</exception>
        public Polygon2d(IList<Vector2d> points, IList<Polygon2d> holes)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            if (holes == null)
            {
                throw new ArgumentNullException(nameof(holes));
            }

            if (points.Count < 3)
            {
                throw new ArgumentException("Requires at least 3 outer points",
                    nameof(points));
            }

            var holesCount = holes.Count;
            _holes = new Polygon2d[holesCount];

            for (var i = 0; i < holesCount; ++i)
            {
                if (holes[i] == null)
                {
                    throw new ArgumentException("Contains null element at " +
                        $"index {i}", nameof(holes));
                }

                _holes[i] = holes[i];
            }

            _outer = new LineStrip2d(points, true);
        }

        /// <summary>
        /// Initializes a new instance of Polygon2d
        /// </summary>
        /// <param name="outer">The outer part of the polygon</param>
        /// <param name="holes">The holes of the polygon</param>
        /// <exception cref="ArgumentNullException">Thrown if outer or holes is null
        /// </exception>
        /// <exception cref="ArgumentException">Thrown if holes contains null element
        /// </exception>
        public Polygon2d(Polygon2d outer, IList<Polygon2d> holes)
        {
            if (outer == null)
            {
                throw new ArgumentNullException(nameof(outer));
            }

            if (holes == null)
            {
                throw new ArgumentNullException(nameof(holes));
            }

            var holesCount = holes.Count;
            _holes = new Polygon2d[holesCount];

            for (var i = 0; i < holesCount; ++i)
            {
                if (holes[i] == null)
                {
                    throw new ArgumentException($"Contains null element at index {i}", nameof(holes));
                }

                _holes[i] = holes[i];
            }

            _outer = outer._outer;
        }

        /// <summary>
        /// Initializes a new instance of Polygon2d
        /// </summary>
        /// <param name="coordinates">The coordinates of the polygon</param>
        /// <param name="holes">The holes of the polygon</param>
        /// <exception cref="ArgumentNullException">Thrown if coordinates or holes is 
        /// null</exception>
        /// <exception cref="ArgumentException">Thrown if insufficient coordinates given 
        /// or holes contains a null element</exception>
        public Polygon2d(IList<Geodetic2d> coordinates, IList<Polygon2d> holes)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            if (holes == null)
            {
                throw new ArgumentNullException(nameof(holes));
            }

            if (coordinates.Count < 3)
            {
                throw new ArgumentException("Requires at least 3 outer points",
                    nameof(coordinates));
            }

            var holesCount = holes.Count;
            _holes = new Polygon2d[holesCount];

            for (var i = 0; i < holesCount; ++i)
            {
                if (holes[i] == null)
                {
                    throw new ArgumentException("Contains null element at " +
                        $"index {i}", nameof(holes));
                }

                _holes[i] = holes[i];
            }

            _outer = new LineStrip2d(coordinates, true);
        }

        /// <summary>
        /// Returns the hole at the given index
        /// </summary>
        /// <param name="index">The index of the hole to return</param>
        public Polygon2d Hole(int index)
        {
            return _holes[index];
        }

        /// <summary>
        /// Returns the segment at the given index
        /// </summary>
        /// <param name="index">The index of the segment to return</param>
        public LineSegment2d Segment(int index)
        {
            return _outer.Segment(index);
        }

        /// <summary>
        /// Clips the given subject polygon to the polygon
        /// </summary>
        /// <param name="subject">The subject polygon to clip</param>
        /// <returns>A list of clipped polygons, empty if the polygon did not pass 
        /// clipping</returns>
        /// <exception cref="ArgumentNullException">Thrown if the subject is null
        /// </exception>
        /// <exception cref="InvalidOperationException">Thrown if ClipperLib returns 
        /// unexpected or invalid results</exception>
        public IList<Polygon2d> Clip(Polygon2d subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            if (!Bounds2d.Intersects(Bounds, subject.Bounds))
            {
                return new List<Polygon2d>();
            }

            if (Bounds2d.Contains(Bounds, subject.Bounds))
            {
                return new List<Polygon2d>
                {
                    subject
                };
            }

            // first we clip the outer component to the box
            var subjectPoints = IntPoints(subject);
            var clipPoints = IntPoints(this);

            var clipper = new Clipper();

            clipper.AddPath(subjectPoints, PolyType.ptSubject, true);
            clipper.AddPath(clipPoints, PolyType.ptClip, true);

            var solution = new PolyTree();

            clipper.Execute(ClipType.ctIntersection, solution);

            if (solution == null)
            {
                throw new InvalidOperationException("ClipperLib returned null solution");
            }

            var clippedOuters = Polygons(solution);

            if (clippedOuters.Count > 0 && subject.HoleCount > 0)
            {
                // clip each original hole to each of the clipped outers
                for (var i = 0; i < clippedOuters.Count; ++i)
                {
                    var clippedHoles = new List<Polygon2d>();

                    for (var j = 0; j < subject.HoleCount; ++j)
                    {
                        // recursive clipping done here
                        clippedHoles.AddRange(clippedOuters[i].Clip(subject.Hole(j)));
                    }

                    // add the holes to the clipped outer
                    if (clippedHoles.Count > 0)
                    {
                        clippedOuters[i] = new Polygon2d(clippedOuters[i],
                            clippedHoles);
                    }
                }
            }

            return clippedOuters;
        }

        /// <summary>
        /// Clips a linestrip to the polygon
        /// </summary>
        /// <param name="subject">The subject linestrip to clip</param>
        /// <returns>A list of clipped linestrips or empty if the linestrip did 
        /// not pass clipping</returns>
        /// <exception cref="ArgumentNullException">Thrown if the subject is null
        /// </exception>
        /// <exception cref="InvalidOperationException">Thrown if ClipperLib returns 
        /// unexpected or invalid results</exception>
        public IList<LineStrip2d> Clip(LineStrip2d subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            if (!Bounds2d.Intersects(Bounds, subject.Bounds))
            {
                return new List<LineStrip2d>();
            }

            if (Bounds2d.Contains(Bounds, subject.Bounds))
            {
                return new List<LineStrip2d>
                {
                    subject
                };
            }

            var subjectPoints = IntPoints(subject);
            var clipPoints = IntPoints(this);
            var clipper = new Clipper();

            clipper.AddPath(subjectPoints, PolyType.ptSubject, false);
            clipper.AddPath(clipPoints, PolyType.ptClip, true);

            var solution = new PolyTree();

            clipper.Execute(ClipType.ctIntersection, solution);

            if (solution == null)
            {
                throw new InvalidOperationException("ClipperLib returned null solution");
            }

            var result = LineStrips(solution);

            return result;
        }

        /// <summary>
        /// Attemmpts to join the polygon to the given polygon
        /// </summary>
        /// <param name="subject">The subject polygon to attempt to join</param>
        /// <param name="joined">The resulting polygon</param>
        /// <returns>True on successfully joined, false otherwise</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="subject"/> 
        /// is null</exception>
        public bool TryJoin(Polygon2d subject, out Polygon2d joined)
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

            var hostPoints = IntPoints(this);
            var subjectPoints = IntPoints(subject);

            var clipper = new Clipper();
            clipper.AddPath(subjectPoints, PolyType.ptSubject, true);
            clipper.AddPath(hostPoints, PolyType.ptClip, true);
            var solution = new PolyTree();

            clipper.Execute(ClipType.ctUnion, solution);
            var polygons = Polygons(solution);

            if (polygons.Count == 0)
            {
                throw new InvalidOperationException("ClipperLib solution resulted" +
                                                    "in no polygons");
            }

            // if we got a single polygon
            if (polygons.Count == 1)
            {
                // handle holes
                if (HoleCount < 1 && subject.HoleCount < 1)
                {
                    joined = polygons[0];
                }
                else
                {
                    if (HoleCount > 0 && subject.HoleCount > 0)
                    {
                        var holes = new Polygon2d[HoleCount + subject.HoleCount];
                        var k = -1;
                        for (var i = 0; i < HoleCount; ++i)
                        {
                            holes[++k] = Hole(i);
                        }
                        for (var i = 0; i < subject.HoleCount; ++i)
                        {
                            holes[++k] = subject.Hole(i);
                        }

                        joined = new Polygon2d(polygons[0], Combine(holes));
                    }
                    else if (HoleCount > 0)
                    {
                        joined = new Polygon2d(polygons[0], _holes);
                    }
                    else if (subject.HoleCount > 0)
                    {
                        // todo : optimise with direct access to polygon holes as ilist
                        var holes = new Polygon2d[subject.HoleCount];
                        for (var i = 0; i < subject.HoleCount; ++i)
                        {
                            holes[i] = subject.Hole(i);
                        }
                        joined = new Polygon2d(polygons[0], holes);
                    }
                }

                result = true;
            }

            return result;
        }

        /// <summary>
        /// Attempts to combine the given polygons
        /// </summary>
        /// <param name="polygons">The polygons to combine</param>
        /// <param name="indices">The optional list of indices to track where the
        /// original polygons end up</param>
        /// <returns>A list of potentially combined polygons</returns>
        /// <exception cref="ArgumentNullException">Thrown if 
        /// <paramref name="polygons"/> is null</exception>
        /// <exception cref="ArgumentException">Thrown if any element of 
        /// <paramref name="polygons"/> is null </exception>
        public static IList<Polygon2d> Combine(IList<Polygon2d> polygons, IList<int> indices = null)
        {
            if (polygons == null)
            {
                throw new ArgumentNullException(nameof(polygons));
            }

            polygons.AssertNoNullEntries();
            var polygonCount = polygons.Count;

            if (polygonCount < 2)
            {
                return polygons;
            }

            var index = 0;
            var joined = new bool[polygonCount];
            var joinedPolygon = Join(polygons, joined, indices, index);
            var result = new List<Polygon2d>();

            while (joinedPolygon != null)
            {
                bool joinCreated;

                do
                {
                    joinCreated = false;

                    // work through non joined polygons now and see if we can join them
                    for (var i = 0; i < polygonCount; ++i)
                    {
                        if (!joined[i])
                        {
                            Polygon2d tempJoin;

                            if (joinedPolygon.TryJoin(polygons[i], out tempJoin))
                            {
                                joined[i] = true;
                                joinCreated = true;
                                joinedPolygon = tempJoin;

                                if (indices != null)
                                {
                                    indices[i] = index;
                                }
                            }
                        }
                    }
                    // if we created a join, go again.. we might be able to join more
                } while (joinCreated); // todo : add joined count early exit

                result.Add(joinedPolygon);
                joinedPolygon = Join(polygons, joined, indices, ++index);
            }

            --index;

            for (var i = 0; i < polygonCount; ++i)
            {
                if (!joined[i])
                {
                    result.Add(polygons[i]);

                    if (indices != null)
                    {
                        indices[i] = ++index;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the polygon, relative to the given point
        /// </summary>
        /// <param name="point">The point to become relative to</param>
        /// <returns>A polygon relative to the given point</returns>
        public Polygon2d Relative(Vector2d point)
        {
            var relativePoints = new Vector2d[Count];

            for (var i = 0; i < Count; ++i)
            {
                relativePoints[i] = _outer[i] - point;
            }

            var relativeHoles = new Polygon2d[HoleCount];

            for (var i = 0; i < HoleCount; ++i)
            {
                relativeHoles[i] = _holes[i].Relative(point);
            }

            return new Polygon2d(relativePoints, relativeHoles);
        }

        /// <summary>
        /// Returns the polygon, reversing relativity to a given point
        /// </summary>
        /// <param name="point">The point to reverse relativity to</param>
        /// <returns>A polygon relative to the given point</returns>
        public Polygon2d Absolute(Vector2d point)
        {
            var absolutePoints = new Vector2d[Count];

            for (var i = 0; i < Count; ++i)
            {
                absolutePoints[i] = _outer[i] + point;
            }

            var absoluteHoles = new Polygon2d[HoleCount];

            for (var i = 0; i < HoleCount; ++i)
            {
                absoluteHoles[i] = _holes[i].Absolute(point);
            }

            return new Polygon2d(absolutePoints, absoluteHoles);
        }

        /// <summary>
        /// Evaluates if the polygon intersects with a subject polygon
        /// </summary>
        /// <param name="subject">The subject polygon to test against</param>
        /// <returns>Returns true if the subject is intersecting, false otherwise</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="subject"/> is null</exception>
        public bool Intersects(Polygon2d subject)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            if (!Bounds2d.Intersects(Bounds, subject.Bounds))
            {
                return false;
            }

            // first we clip the outer component to the box
            var subjectPoints = IntPoints(subject);
            var clipPoints = IntPoints(this);

            var clipper = new Clipper();

            clipper.AddPath(subjectPoints, PolyType.ptSubject, true);
            clipper.AddPath(clipPoints, PolyType.ptClip, true);

            var solution = new PolyTree();

            clipper.Execute(ClipType.ctIntersection, solution);

            if (solution == null)
            {
                throw new InvalidOperationException("ClipperLib returned null solution");
            }

            var clippedOuters = Polygons(solution);
            return clippedOuters.Count == 1;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is Polygon2d))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (Polygon2d)obj;

            if (Count != other.Count || HoleCount != other.HoleCount)
            {
                return false;
            }

            var outerCount = _outer.Count;
            for (var i = 0; i < outerCount; ++i)
            {
                if (_outer[i] != other._outer[i])
                {
                    return false;
                }
            }

            for (var i = 0; i < HoleCount; ++i)
            {
                if (!Hole(i).Equals(other.Hole(i)))
                {
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hash = _outer[0].GetHashCode();

            unchecked
            {
                var outerCount = _outer.Count;
                for (var i = 1; i < outerCount; ++i)
                {
                    hash = (hash * 397) ^ _outer[i].GetHashCode();
                }

                for (var i = 0; i < HoleCount; ++i)
                {
                    hash = (hash * 397) ^ _holes[i].GetHashCode();
                }

                return hash;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"N: {Count}, Holes: {HoleCount}";
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public IEnumerator<Vector2d> GetEnumerator()
        {
            for (var i = 0; i < _outer.Count; ++i)
            {
                yield return _outer[i];
            }
        }

        private static Polygon2d Join(IList<Polygon2d> polygons, IList<bool> joined,
            IList<int> indices = null, int index = 0)
        {
            var joinCreated = false;
            Polygon2d joinedPolygon = null;
            var polygonCount = polygons.Count;

            // work through polygons, looking for an initial join
            for (var i = 0; i < polygonCount && !joinCreated; ++i)
            {
                if (!joined[i])
                {
                    for (var j = 0; j < polygonCount && !joinCreated; ++j)
                    {
                        if (!joined[j])
                        {
                            // see if we can create a join
                            if (i != j && polygons[i].TryJoin(polygons[j], out joinedPolygon))
                            {
                                // mark the joined polygons
                                joined[i] = true;
                                joined[j] = true;

                                if (indices != null)
                                {
                                    indices[i] = index;
                                    indices[j] = index;
                                }

                                // mark to exit the loop
                                joinCreated = true;
                            }
                        }
                    }
                }

            }

            return joinedPolygon;
        }

        private static IList<Vector2d> Vector2ds(IList<IntPoint> points)
        {
            var vectors = new Vector2d[points.Count];

            for (var i = 0; i < points.Count; ++i)
            {
                vectors[i] = new Vector2d(points[i].X * ClipperInverseScaleFactor,
                    points[i].Y * ClipperInverseScaleFactor);
            }

            return vectors;
        }

        private static List<IntPoint> IntPoints(IReadOnlyList<Vector2d> vectors)
        {
            var points = new List<IntPoint>(vectors.Count);

            for (var i = 0; i < vectors.Count; ++i)
            {
                points.Add(new IntPoint((long)(vectors[i].x * ClipperScaleFactor),
                    (long)(vectors[i].y * ClipperScaleFactor)));
            }

            return points;
        }

        private static IList<Polygon2d> Polygons(PolyTree solution)
        {
            var polygons = new List<Polygon2d>();

            var nodes = solution.Childs;
            for (var i = 0; i < nodes.Count; ++i)
            {
                var contour = nodes[i].Contour;
                if (contour.Count > 1)
                {
                    var points = Vector2ds(contour);
                    polygons.Add(new Polygon2d(points));
                }
            }

            return polygons;
        }

        private static IList<LineStrip2d> LineStrips(PolyTree solution)
        {
            var result = new List<LineStrip2d>();
            var nodes = solution.Childs;

            for (var i = 0; i < nodes.Count; ++i)
            {
                var contour = nodes[i].Contour;
                if (contour.Count > 1)
                {
                    var points = Vector2ds(contour);
                    result.Add(new LineStrip2d(points));
                }
            }

            return result;
        }
    }
}