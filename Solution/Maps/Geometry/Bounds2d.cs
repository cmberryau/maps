using System;

namespace Maps.Geometry
{
    /// <summary>
    /// 2 dimensional axis aligned bounding box with double precision
    /// </summary>
    public struct Bounds2d
    {
        /// <summary>
        /// Bounds at the origin with zero size
        /// </summary>
        public static readonly Bounds2d Zero = new Bounds2d(Vector2d.Zero, Vector2d.Zero);

        /// <summary>
        /// The maximum point of the bounding box
        /// </summary>
        public Vector2d Max => Centre + Extents;

        /// <summary>
        /// The minimum point of the bounding box
        /// </summary>
        public Vector2d Min => Centre - Extents;

        /// <summary>
        /// The centre of the bounding box
        /// </summary>
        public readonly Vector2d Centre;

        /// <summary>
        /// The extents of the bounding box
        /// </summary>
        public readonly Vector2d Extents;

        /// <summary>
        /// Initializes a new instance of Bounds2d
        /// </summary>
        /// <param name="centre">The centre of the bounds</param>
        /// <param name="size">The size of the bounds</param>
        public Bounds2d(Vector2d centre, Vector2d size)
        {
            Centre = centre;
            Extents = size * 0.5;
        }

        /// <summary>
        /// Evaluates if the bounds contains the subject point
        /// </summary>
        /// <param name="bounds">The bounds to evaluate</param>
        /// <param name="subject">The subject point to evaluate</param>
        public static bool Contains(Bounds2d bounds, Vector2d subject)
        {
            return subject.x <= bounds.Max.x && subject.y <= bounds.Max.y &&
                   subject.x >= bounds.Min.x && subject.y >= bounds.Min.y;
        }

        /// <summary>
        /// Evaluates if the bounds contains the subject point
        /// </summary>
        /// <param name="bounds">The bounds to evaluate</param>
        /// <param name="subject">The subject point to evaluate</param>
        /// <param name="error">The magnitude of allowed error</param>
        public static bool Contains(Bounds2d bounds, Vector2d subject, 
            double error)
        {
            return Contains(new Bounds2d(bounds.Centre, bounds.Extents * 2 +
                new Vector2d(error) * 0.5), subject);
        }

        /// <summary>
        /// Evaluates if the bounds contains the subject bounds
        /// </summary>
        /// <param name="bounds">The bounds to evaluate</param>
        /// <param name="subject">The subject bounds</param>
        public static bool Contains(Bounds2d bounds, Bounds2d subject)
        {
            return Contains(bounds, subject.Max) && 
                   Contains(bounds, subject.Min);
        }

        /// <summary>
        /// Evaluates if the bounds intersects the given bounds
        /// </summary>
        /// <param name="bounds">The bounds to evaluate</param>
        /// <param name="subject">The subject bounds to evaluate</param>
        public static bool Intersects(Bounds2d bounds, Bounds2d subject)
        {
            return bounds.Min.x <= subject.Max.x && 
                   bounds.Max.x >= subject.Min.x &&
                   bounds.Min.y <= subject.Max.y && 
                   bounds.Max.y >= subject.Min.y;
        }

        /// <summary>
        /// Evaluates if the bounds intersects the formed bounds
        /// </summary>
        /// <param name="bounds">The bounds to evaluate</param>
        /// <param name="min">The minimum vertex</param>
        /// <param name="max">The maximuim vertex</param>
        public static bool Intersects(Bounds2d bounds, Vector2d min, Vector2d max)
        {
            return bounds.Min.x <= max.x &&
                   bounds.Max.x >= min.x &&
                   bounds.Min.y <= max.y &&
                   bounds.Max.y >= min.y;
        }

        /// <summary>
        /// Returns the addition of the two bounds
        /// </summary>
        public static Bounds2d operator +(Bounds2d lhs, Bounds2d rhs)
        {
            var max = Vector2d.Max(lhs.Max, rhs.Max);
            var min = Vector2d.Max(lhs.Min, rhs.Min);
            var centre = Vector2d.Midpoint(lhs.Centre, rhs.Centre);
            var size = Vector2d.ComponentDistance(max, min);

            return new Bounds2d(centre, size);
        }

        /// <summary>
        /// Evaluates if the bounds intersects the given line
        /// </summary>
        /// <param name="bounds">The bounds to evaluate</param>
        /// <param name="line">The subject line to evaluate</param>
        public static LineSegment2d Intersection(Bounds2d bounds, Line2d line)
        {
            if (line == null)
            {
                throw new ArgumentNullException(nameof(line));
            }

            double tmax;
            double tmin;

            if (IntersectionInterval(bounds, line, out tmax, out tmin))
            {
                // intersection is tiny, ignore it
                if (Math.Abs(tmax - tmin) < Mathd.Epsilon)
                {
                    return null;
                }

                //  form the segment
                var p0 = line.P0 + tmin * line.Direction;
                var p1 = line.P0 + tmax * line.Direction;

                return new LineSegment2d(p0, p1);
            }

            return null;
        }

        /// <summary>
        /// Evaluates if the bounds intersects the given line segment
        /// </summary>
        /// <param name="bounds">The bounds to evaluate</param>
        /// <param name="segment">The subject segment to evaluate</param>
        public static LineSegment2d Intersection(Bounds2d bounds, 
            LineSegment2d segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            // no intersection possible exit case
            if (!Intersects(bounds, segment.Bounds))
            {
                return null;
            }

            // completely contained exit case
            if (Contains(bounds, segment.Bounds))
            {
                return segment;
            }

            double tmax;
            double tmin;

            // if the line of the segment has an intersecting 
            // interval with the bounds
            if (IntersectionInterval(bounds, segment, out tmin, out tmax))
            {
                // interval does not overlap segment
                if (tmin > 1 || tmax < 0)
                {
                    return null;
                }

                // clamp time values before further evaluation
                var tminClamped = Mathd.Clamp01(tmin);
                var tmaxClamped = Mathd.Clamp01(tmax);

                // intersection is tiny, ignore it
                if (Math.Abs(tmaxClamped - tminClamped) < Mathd.Epsilon)
                {
                    return null;
                }

                // clamp the interval and form the segment
                var p0 = segment.P0 + tminClamped * segment.Direction;
                var p1 = segment.P0 + tmaxClamped * segment.Direction;

                return new LineSegment2d(p0, p1);
            }

            return null;
        }

        /// <summary>
        /// Clips the given linestrip to the given bounds
        /// </summary>
        /// <param name="bounds">The bounds to clip to</param>
        /// <param name="linestrip">The linestrip to clip</param>
        public static LineStrip2d[] Clip(Bounds2d bounds, 
            LineStrip2d linestrip)
        {
            if (linestrip == null)
            {
                throw new ArgumentNullException(nameof(linestrip));
            }

            // early exit if box already contains linestrip
            if (Contains(bounds, linestrip.Bounds))
            {
                return new[]
                {
                    linestrip
                };
            }

            // early exit if box does not intersect with linestrip
            if (!Intersects(bounds, linestrip.Bounds))
            {
                return new LineStrip2d[0];
            }

            // max possible output vertices count = 2 * input count
            var clippedVertices = new Vector2d[linestrip.Count * 2];
            // tracks the start of new linestrips
            var stripStart = new bool[linestrip.Count * 2];

            var i = 1;
            var vertexCount = 0;
            var stripCount = 0;

            var p0 = linestrip[0];
            var p0Contained = Contains(bounds, p0);
            var tmax = 0d;
            var lastIntersected = false;
            var d = Vector2d.Zero;
            var lastInside = p0Contained;

            // first point is inside, first output strip is starting
            if (lastInside)
            {
                stripStart[0] = true;
                stripCount++;
            }

            // evaluate all segments of the linestrip
            for (; i < linestrip.Count; i++)
            {
                var p1 = linestrip[i];
                var p1Contained = Contains(bounds, p1);
                lastIntersected = false;

                if (p0Contained || p1Contained || Intersects(bounds, 
                    Vector2d.Min(p0, p1), Vector2d.Max(p0, p1)))
                {
                    // segment is contained in bounds
                    if (p0Contained && p1Contained)
                    {
                        // completely contained, add p0
                        clippedVertices[vertexCount++] = p0;

                        // we end inside the bounds
                        lastInside = true;
                    }
                    // segment's extended line intersects with bounds
                    else
                    {
                        double tmin;
                        if (IntersectionInterval(bounds, p0, d = p1 - p0,
                            out tmin, out tmax))
                        {
                            // line intersection interval overlaps segment
                            if (tmin < 1 && tmax > 0)
                            {
                                if (!lastInside)
                                {
                                    // mark as a strip start
                                    stripStart[vertexCount] = true;
                                    stripCount++;
                                }

                                // add adjusted p0 to tmin
                                clippedVertices[vertexCount++] = p0 + d *
                                    Mathd.Clamp01(tmin);

                                if (!p1Contained && 
                                    i < linestrip.Count - 1)
                                {
                                    // add adjusted p1 to tmax
                                    clippedVertices[vertexCount++] = p0 + d *
                                        Mathd.Clamp01(tmax);
                                }

                                lastIntersected = true;
                            }

                            // set the inside flag along the path
                            lastInside = p1Contained;
                        }
                    }
                }

                p0 = p1;
                p0Contained = p1Contained;
            }

            // final point is contained
            if (p0Contained)
            {
                clippedVertices[vertexCount++] = p0;
            } 
            // final segment intersected
            else if (lastIntersected)
            {
                clippedVertices[vertexCount++] = linestrip[i - 2] + d *
                    Mathd.Clamp01(tmax);
            }

            // not enough vertices passed the clip phase
            if (vertexCount < 1)
            {
                return new LineStrip2d[0];
            }

            // return generated linestrips
            return GenerateLineStrips(stripCount, vertexCount, stripStart, 
                clippedVertices);
        }

        /// <summary>
        /// Evaluates if the given two Bounds2d instances are equal
        /// </summary>
        public static bool operator ==(Bounds2d lhs, Bounds2d rhs)
        {
            return lhs.Centre == rhs.Centre && lhs.Extents == rhs.Extents;
        }

        /// <summary>
        /// Evaluates if the given two Bounds2d instances are not equal
        /// </summary>
        public static bool operator !=(Bounds2d lhs, Bounds2d rhs)
        {
            return lhs.Centre != rhs.Centre || lhs.Extents != rhs.Extents;
        }

        /// <summary>
        /// Returns a string representation of the Bounds2d instance
        /// </summary>
        public override string ToString()
        {
            return $"Centre:{Centre}, Extents:{Extents}";
        }

        /// <summary>
        /// Evaluates if the given Bounds2d instance equals the
        /// given object
        /// </summary>
        public override bool Equals(object obj)
        {
            if (!(obj is Bounds2d))
            {
                return false;
            }

            var bounds = (Bounds2d) obj;

            return Centre.Equals(bounds.Centre) && 
                   Extents.Equals(bounds.Extents);
        }

        /// <summary>
        /// Returns the hash code for the Bounds2d instance
        /// </summary>
        public override int GetHashCode()
        {
            return Centre.GetHashCode() ^ Extents.GetHashCode() << 2;
        }

        private static bool IntersectionInterval(Bounds2d bounds, Line2d line,
            out double tmin, out double tmax)
        {
            return IntersectionInterval(bounds, line.P0, line.Direction,
                out tmin, out tmax);
        }

        private static bool IntersectionInterval(Bounds2d bounds,
            LineSegment2d segment, out double tmin, out double tmax)
        {
            return IntersectionInterval(bounds, segment.P0, segment.Direction,
                out tmin, out tmax);
        }

        /// <summary>
        /// Generates an array of linestrips
        /// </summary>
        /// <param name="stripCount">The total expected strip count</param>
        /// <param name="vertexCount">The total vertex count</param>
        /// <param name="stripStart">The array tracking strip starts</param>
        /// <param name="clippedVertices">The array of vertices</param>
        private static LineStrip2d[] GenerateLineStrips(int stripCount,
            int vertexCount, bool[] stripStart, Vector2d[] clippedVertices)
        {
            // generate the indices array
            var indices = new int[stripCount + 1];
            for (int i = 0, j = 0; i < vertexCount; i++)
            {
                if (stripStart[i])
                {
                    indices[j++] = i;
                }
            }

            // append the final index
            indices[stripCount] = vertexCount;

            // generate the final strips array
            var finalStrips = new LineStrip2d[stripCount];
            for (var i = 0; i < stripCount; i++)
            {
                // generate the vertices array and create linestrip
                var vertices = new Vector2d[indices[i + 1] - indices[i]];
                Array.Copy(clippedVertices, indices[i], vertices, 0,
                    vertices.Length);
                finalStrips[i] = new LineStrip2d(vertices);
            }

            return finalStrips;
        }

        /// <summary>
        /// Evaluates if there is an intersection, and if so populates
        /// tmin and tmax with the interval of the intersection of the
        /// line across the bounds
        ///
        /// Optimised version of: 
        /// http://www.scratchapixel.com/lessons/3d-basic-rendering/minimal-ray-tracer-rendering-simple-shapes/ray-box-intersection
        /// </summary>
        private static bool IntersectionInterval(Bounds2d bounds, Vector2d p0, 
            Vector2d direction, out double tmin, out double tmax)
        {
            double txmin;
            double txmax;
            double tymin;
            double tymax;

            // if the segment is parallel to the y axis 
            // (i.e no difference in x)
            if (Math.Abs(direction.x) < Mathd.Epsilon)
            {
                txmin = double.MinValue;
                txmax = double.MaxValue;

                // y axis intersect times
                tymin = (bounds.Min.y - p0.y) / direction.y;
                tymax = (bounds.Max.y - p0.y) / direction.y;

                // swap the min/max if flipped for y
                if (tymin > tymax)
                {
                    var swap = tymax;

                    tymax = tymin;
                    tymin = swap;
                }
            }
            else
            {
                // x axis intersect times
                txmin = (bounds.Min.x - p0.x) / direction.x;
                txmax = (bounds.Max.x - p0.x) / direction.x;

                // swap the min/max if flipped for x
                if (txmin > txmax)
                {
                    var swap = txmax;

                    txmax = txmin;
                    txmin = swap;
                }

                // if the segment is parallel to the x axis 
                // (i.e no difference in y)
                if (Math.Abs(direction.y) < Mathd.Epsilon)
                {
                    tymin = double.MinValue;
                    tymax = double.MaxValue;
                }
                else
                {
                    // y axis intersect times
                    tymin = (bounds.Min.y - p0.y) / direction.y;
                    tymax = (bounds.Max.y - p0.y) / direction.y;

                    // swap the min/max if flipped for y
                    if (tymin > tymax)
                    {
                        var swap = tymax;

                        tymax = tymin;
                        tymin = swap;
                    }
                }
            }

            // miss case
            if (txmin > tymax || tymin > txmax)
            {
                tmin = 0;
                tmax = 0;

                return false;
            }

            // otherwise a hit
            tmin = Math.Max(txmin, tymin);
            tmax = Math.Min(txmax, tymax);

            return true;
        }
    }
}