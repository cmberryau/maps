using System;
using System.Collections.Generic;

namespace Maps.Geometry
{
    /// <summary>
    /// 2 dimensional axis aligned box with double precision
    /// </summary>
    public sealed class Box2d
    {
        /// <summary>
        /// Point A of the Box2d
        /// </summary>
        public readonly Vector2d A;

        /// <summary>
        /// Point B of the Box2d
        /// </summary>
        public readonly Vector2d B;

        /// <summary>
        /// The bounds of the box
        /// </summary>
        public readonly Bounds2d Bounds;

        /// <summary>
        /// The centre of the box
        /// </summary>
        public readonly Vector2d Centre;

        /// <summary>
        /// The area of the box
        /// </summary>
        public double Area => Vector2d.Distance(this[0], this[1]) * 
            Vector2d.Distance(this[1], this[2]);

        /// <summary>
        /// Index accessor for corners, always clockwise
        /// </summary>
        public Vector2d this[int index] => _corners[index];

        private readonly Vector2d[] _corners;
        private readonly Line2d[] _clipLines;

        /// <summary>
        /// Creates a new box
        /// </summary>
        /// <param name="a">Point a of the box</param>
        /// <param name="b">Point b of the box</param>
        public Box2d(Vector2d a, Vector2d b)
        {
            // set all the default information
            A = a;
            B = b;
            Centre = Vector2d.Midpoint(A, B);
            Bounds = new Bounds2d(Centre, Vector2d.Abs(B - A));

            // create corners, forcing clockwiseness
            if (A.y > B.y)
            {
                if (A.x > B.x)
                {
                    _corners = new []
                    {
                        A, new Vector2d(A.x, B.y),
                        B, new Vector2d(B.x, A.y)
                    };
                }
                else
                {
                    _corners = new []
                    {
                        A, new Vector2d(B.x, A.y),
                        B, new Vector2d(A.x, B.y)
                    };
                }
            }
            else
            {
                if (A.x > B.x)
                {
                    _corners = new []
                    {
                        A, new Vector2d(B.x, A.y),
                        B, new Vector2d(A.x, B.y)
                    };
                }
                else
                {
                    _corners = new []
                    {
                        A, new Vector2d(A.x, B.y),
                        B, new Vector2d(B.x, A.y)
                    };
                }
            }

            // create clip lines
            _clipLines = new[]
            {
                new Line2d(_corners[0], _corners[1] - _corners[0]),
                new Line2d(_corners[1], _corners[2] - _corners[1]),
                new Line2d(_corners[2], _corners[3] - _corners[2]),
                new Line2d(_corners[3], _corners[0] - _corners[3]),
            };
        }

        /// <summary>
        /// Clips the given polygon to the box using ClipperLib
        /// </summary>
        /// <param name="subject">The polygon to clip</param>
        /// <returns>A list of clipped polygons, empty if none passed</returns>
        /// <exception cref="ArgumentNullException">Thrown if the subject is null
        /// </exception>
        /// <exception cref="InvalidOperationException">Thrown if clipper lib returns 
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

            var clipPoly = new Polygon2d(_corners);
            return clipPoly.Clip(subject);
        }

        /// <summary>
        /// Clips the given linestrip to the box using ClipperLib
        /// </summary>
        /// <param name="subject">The linestrip to clip</param>
        /// <returns>A list of clipped linestrips, empty if none passed</returns>
        /// <exception cref="ArgumentNullException">Thrown if the subject is null
        /// </exception>
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

            var clipPoly = new Polygon2d(_corners);
            return clipPoly.Clip(subject);
        }

        /// <summary>
        /// Clamps the given LineStrip2d to the box
        /// </summary>
        /// <param name="linestrip">The line strip instance to clip</param>
        /// <returns>Returns the clamped LineStrip2d, or null on no intersection
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when linestrip is null
        /// </exception>
        public LineStrip2d Clamp(LineStrip2d linestrip)
        {
            if (linestrip == null)
            {
                throw new ArgumentNullException(nameof(linestrip));
            }

            // early exit if box already contains linestrip
            if (Bounds2d.Contains(Bounds, linestrip.Bounds))
            {
                return linestrip;
            }

            // early exit if box does not intersect with linestrip
            if (!Bounds2d.Intersects(Bounds, linestrip.Bounds))
            {
                return null;
            }

            var clampedVertices = SutherlandHodgman(linestrip.PointsCopy(),
                _clipLines);

            if (clampedVertices != null)
            {
                return new LineStrip2d(clampedVertices);
            }

            return null;
        }

        /// <summary>
        /// Evaluates if the Box2d instance contains the given point
        /// </summary>
        /// <param name="point">The point to evaluate</param>
        public bool Contains(Vector2d point)
        {
            return Bounds2d.Contains(Bounds, point);
        }

        /// <summary>
        /// Evaluates if the Box2d instance contains the given point
        /// </summary>
        /// <param name="point">The point to evaluate</param>
        /// <param name="error">The magnitude of allowed error</param>
        public bool Contains(Vector2d point, double error)
        {
            return Bounds2d.Contains(Bounds, point, error);
        }

        /// <summary>
        /// Evaluates if the Box2d instance contains the given segment
        /// </summary>
        /// <param name="segment">The segment to evaluate</param>
        public bool Contains(LineSegment2d segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            return Bounds2d.Contains(Bounds, segment.Bounds);
        }

        /// <summary>
        /// Evaluates if the Box2d contains the given box
        /// </summary>
        /// <param name="box">The box to evaluate against</param>
        /// <returns>True if contained, false if not</returns>
        public bool Contains(Box2d box)
        {
            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            return Bounds2d.Contains(Bounds, box._corners[0]) && 
                   Bounds2d.Contains(Bounds, box._corners[3]);
        }

        /// <summary>
        /// Evaluates if the Box2d contains the given box
        /// </summary>
        /// <param name="box">The box to evaluate against</param>
        /// <param name="error">The allowed error</param>
        /// <returns>True if contained, false if not</returns>
        public bool Contains(Box2d box, double error)
        {
            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            return Bounds2d.Contains(Bounds, box._corners[0], error) &&
                   Bounds2d.Contains(Bounds, box._corners[3], error);
        }

        /// <summary>
        /// Performs the Sutherland-Hodgman clipping algorithm; expects a clockwise 
        /// order of clip lines that form a convex shape. Works for both 
        /// linestrips and polygons.
        /// </summary>
        /// <param name="vertices">The subject vertices</param>
        /// <param name="clipLines">The clip lines</param>
        private static Vector2d[] SutherlandHodgman(Vector2d[] vertices, 
            Line2d[] clipLines)
        {
            if (vertices.Length < 2)
            {
                throw new ArgumentException("Must contain more than one vertex",
                    nameof(vertices));
            }

            // determine if the vertices form a polygon
            var closed = vertices[0] == vertices[vertices.Length - 1];

            if (closed)
            {
                if (vertices.Length < 3)
                {
                    throw new ArgumentException("Must contain more than two vertices",
                        nameof(vertices));
                }
            }

            // create a copy of the vertices array to work with
            var inputVertices = new Vector2d[closed ? vertices.Length - 1: vertices.Length];
            Array.Copy(vertices, 0, inputVertices, 0, inputVertices.Length);
            var lastk = inputVertices.Length;
            var offset = closed ? 0 : 1;

            // iterate through clip lines
            for (int i = 0, k = 0; i < clipLines.Length; i++, k = 0)
            {
                // we will only ever need at max our last output count * 2
                var outputVertices = new Vector2d[lastk * 2];
                var vertex = Vector2d.Zero;

                if (closed)
                {
                    vertex = inputVertices[lastk - 1];
                }

                // iterate through vertices
                for (var j = 0; j < lastk - offset; j++)
                {
                    Vector2d nextVertex;

                    if (!closed)
                    {
                        nextVertex = inputVertices[j + 1];
                        vertex = inputVertices[j];
                    }
                    else
                    {
                        nextVertex = inputVertices[j];
                    }

                    // evaluate vertex sides (right == inside, left == outside
                    // centre == colinear)
                    var side = clipLines[i].Side(vertex);
                    var nextSide = clipLines[i].Side(nextVertex);

                    // if the current vertex is inside
                    if (side == RelativePosition.Right)
                    {
                        outputVertices[k++] = vertex;

                        // if the next vertex is outside
                        if (nextSide == RelativePosition.Left)
                        {
                            // add if intersecting
                            Vector2d intersection;
                            if (Intersection(vertex, nextVertex, clipLines[i], 
                                out intersection))
                            {
                                outputVertices[k++] = intersection;
                            }
                            else
                            {
                                throw new InvalidOperationException("Should " +
                                    "intersect");
                            }
                        }
                        // next is lying on the clip line
                        else if (nextSide == RelativePosition.Centre)
                        {
                            outputVertices[k++] = nextVertex;
                        }
                    }
                    // the current vertex is outside
                    else if (side == RelativePosition.Left)
                    {
                        // the next vertex is inside
                        if (nextSide == RelativePosition.Right)
                        {
                            // add if intersecting
                            Vector2d intersection;
                            if (Intersection(vertex, nextVertex, clipLines[i],
                                out intersection))
                            {
                                outputVertices[k++] = intersection;
                            }
                            else
                            {
                                throw new InvalidOperationException("Should " +
                                    "intersect");
                            }
                        }
                    }
                    // the current vertex lies on the clip line
                    else
                    {
                        // next vertex is inside or on the clip line
                        if (nextSide != RelativePosition.Left)
                        {
                            // add current
                            outputVertices[k++] = vertex;
                        }
                    }

                    if (closed)
                    {
                        vertex = nextVertex;
                    }
                }

                // no vertices passed clip stage, return null
                if (k <= 0)
                {
                    return null;
                }

                // open strips must be finalised
                if (!closed)
                {
                    // finalise the last vertex
                    vertex = inputVertices[lastk - 1];

                    // if the last vertex is inside or colinear
                    if (clipLines[i].Side(vertex) != RelativePosition.Left)
                    {
                        // add it to the output
                        outputVertices[k++] = vertex;
                    }
                }

                // our next input array is our current output array
                inputVertices = outputVertices;
                lastk = k;
            }

            // correctly size our final output
            var finalVertices = new Vector2d[closed ? lastk + 1 : lastk];
            Array.Copy(inputVertices, 0, finalVertices, 0, lastk);

            if (closed)
            {
                finalVertices[finalVertices.Length - 1] = finalVertices[0];
            }

            return finalVertices;
        }

        /// <summary>
        /// Evaluates the intersection of the segment between the two given points
        /// and the subject clipping line
        /// </summary>
        /// <param name="a">Starting point of the segment</param>
        /// <param name="b">End point of the segment</param>
        /// <param name="clipLine">The subject clipping line</param>
        /// <param name="intersectionVertex">The resulting intersection vertex</param>
        /// <returns>True if there is an intersection, false if not</returns>
        private static bool Intersection(Vector2d a, Vector2d b, 
            Line2d clipLine, out Vector2d intersectionVertex)
        {
            // find intersection and add
            var segment = new LineSegment2d(a, b);
            var intersection = clipLine.Intersection(segment);

            // if we intersect, we add - otherwise just move to next
            if (intersection.Type == LineIntersection.Intersection.SinglePoint)
            {
                intersectionVertex = intersection.A;
                return true;
            }

            // colinear intersections not supported yet
            if (intersection.Type == LineIntersection.Intersection.Colinear)
            {
                throw new NotSupportedException("Colinear intersections " +
                                                "shouldn't reach here");
            }

            // no intersection
            intersectionVertex = Vector2d.Zero;
            return false;
        }
    }
}
