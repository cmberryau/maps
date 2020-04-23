using System;
using System.Collections.Generic;
using Maps.Collections;

namespace Maps.Geographical.Simplification
{
    /// <summary>
    /// Responsible for simplifying geometry using the Ramer-Douglas-Peuker algorithm
    /// </summary>
    /// <remarks>Slightly optimised version of: https://github.com/podolsir/osmosis-simplifyways/blob/78b99b47d5734a84f705397597be4886449d4e0b/src/main/de/vwistuttgart/openstreetmap/osmosis/simplifyways/v0_6/impl/DouglasPeuckerSimplifier.java</remarks>
    public class RamerDouglasPeukerSimplifier : GeodeticSimplifier2d
    {
        /// <summary>
        /// The minimum epsilon, 10cm
        /// </summary>
        public const double EpsilonMinimum = Mathd.EpsilonE1;

        private const int PolygonMinimum = 4;
        private const int LinestripMinimum = 2;
        private const int CoordinateMinimum = 1;
        private double EdgePreservationThreshold = Mathd.EpsilonE5;
        private readonly double _epsilon;

        /// <summary>
        /// Initializes a new instance of RamerDouglasPeuker
        /// </summary>
        /// <param name="epsilonMeters">The epsilon value in meters</param>
        /// <remarks>epsilonMeters will always be at minimum EpsilonMinimum</remarks>
        public RamerDouglasPeukerSimplifier(double epsilonMeters = EpsilonMinimum)
        {
            _epsilon = Math.Max(EpsilonMinimum, epsilonMeters);
        }

        /// <inheritdoc />
        public override IList<Geodetic2d> Simplify(IList<Geodetic2d> coordinates,
            IList<bool> keep)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            return DoSimplify(new ReadOnlyList<Geodetic2d>(coordinates),
                CoordinateMinimum, keep);
        }

        /// <inheritdoc />
        public override GeodeticLineStrip2d Simplify(GeodeticLineStrip2d linestrip,
            IList<bool> keep)
        {
            if (linestrip == null)
            {
                throw new ArgumentNullException(nameof(linestrip));
            }

            // linestrips are really simple, just account for closed ones
            var minimum = LinestripMinimum;
            if (linestrip.Closed && linestrip.Count > 3)
            {
                minimum = Math.Min(LinestripMinimum + 3, linestrip.Count);
            }

            var coords = DoSimplify(linestrip, minimum, keep);
            return new GeodeticLineStrip2d(coords);
        }

        /// <inheritdoc />
        public override GeodeticPolygon2d Simplify(GeodeticPolygon2d polygon, 
            IList<IList<bool>> keep, GeodeticPolygon2d preserveEdges)
        {
            if (polygon == null)
            {
                throw new ArgumentNullException(nameof(polygon));
            }

            // if we've been given edges to preserve, we need to resolve that before
            // and add the indices to the keep list
            if (preserveEdges != null)
            {
                var preservePolygon = preserveEdges.Polygon;

                if (!preservePolygon.Convex)
                {
                    throw new ArgumentException("Must be convex",
                        nameof(preserveEdges));
                }

                if (preservePolygon.HoleCount > 0)
                {
                    throw new ArgumentException("Cannot contain holes",
                        nameof(preserveEdges));
                }

                // iterate through every coordinate of the polygon, check if it lies on
                // any edge of the preserved edges polygon and if so, mark it to be kept
                var preserveLineStrip = preservePolygon.OuterLineStrip;

                for (var i = 0; i < polygon.Count; ++i)
                {
                    var point = polygon[i].Point;
                    // cartesian distance will do
                    var distance = preserveLineStrip.LeastDistanceTo(point);

                    if (distance <= EdgePreservationThreshold)
                    {
                        keep[0][i] = true;
                    }
                }

                // do the same for holes, they are just as important
                for (var i = 0; i < polygon.HoleCount; ++i)
                {
                    var hole = polygon.Hole(i).Polygon;

                    for (var k = 0; k < hole.Count; ++k)
                    {
                        var point = hole[k];
                        // cartesian distance will do
                        var distance = preserveLineStrip.LeastDistanceTo(point);

                        if (distance <= EdgePreservationThreshold)
                        {
                            keep[i + 1][k] = true;
                        }
                    }
                }
            }

            var simplifiedOuter = DoSimplify(polygon, PolygonMinimum, keep?[0]);
            var simplifiedInners = new List<GeodeticPolygon2d>();

            // polygons holes are individually simplified
            for (var i = 0; i < polygon.HoleCount; ++i)
            {
                var simplifiedInner = DoSimplify(polygon.Hole(i), PolygonMinimum, 
                    keep[i + 1]);
                simplifiedInners.Add(new GeodeticPolygon2d(simplifiedInner));
            }

            return new GeodeticPolygon2d(simplifiedOuter, simplifiedInners);
        }

        private IList<Geodetic2d> DoSimplify(IReadOnlyList<Geodetic2d> coordinates,
            int minimum, IList<bool> keep)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            var coordinateCount = coordinates.Count;
            var discard = new bool[coordinateCount];
            var order = new List<int>();
            RamerDouglasPeuker(coordinates, discard, order, 0, coordinateCount);
            var result = new List<Geodetic2d>();

            // determine if we're going to overdiscard
            var overDiscardCount = (coordinateCount - order.Count - minimum) * -1;
            if (overDiscardCount > 0)
            {
                for (var i = 0; i < overDiscardCount; ++i)
                {
                    discard[order[order.Count - (i + 1)]] = false;
                }
            }

            for (var i = 0; i < coordinateCount; ++i)
            {
                if (!discard[i] || (keep?[i] ?? false))
                {
                    result.Add(coordinates[i]);
                }
            }

            if (result.Count > 2)
            {
                discard = new bool[result.Count];
                var postDiscardCount = 0;

                // simplification post-pass
                for (var i = 1; i < result.Count - 1; ++i)
                {
                    if (Vector2d.Distance(result[i - 1].Point, result[i].Point) < Mathd.Epsilon)
                    {
                        discard[i] = true;
                        postDiscardCount++;
                    }
                }

                if (postDiscardCount > 0)
                {
                    var finalResult = new List<Geodetic2d>();

                    for (var i = 0; i < result.Count; ++i)
                    {
                        if (!discard[i])
                        {
                            finalResult.Add(result[i]);
                        }
                    }

                    result = finalResult;
                }
            }

            return result;
        }

        private void RamerDouglasPeuker(IReadOnlyList<Geodetic2d> coordinates, 
            IList<bool> discard, IList<int> order, int start, int end)
        {
            var dmax = 0d;
            var maxIndex = 0;
            var segment = new GeodeticLineSegment2d(coordinates[start], 
                coordinates[end - 1]);

            for (var i = start + 1; i < end - 1; ++i)
            {
                var coord = coordinates[i];
                var d = segment.Distance(coord);

                if (d > dmax)
                {
                    dmax = d;
                    maxIndex = i;
                }
            }

            if (dmax >= _epsilon)
            {
                RamerDouglasPeuker(coordinates, discard, order, start, maxIndex);
                RamerDouglasPeuker(coordinates, discard, order, maxIndex, end);
            }
            else
            {
                for (var i = start + 1; i < end - 1; ++i)
                {
                    discard[i] = true;
                    order.Add(i);
                }
            }
        }
    }
}