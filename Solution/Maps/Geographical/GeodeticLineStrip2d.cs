using System;
using System.Collections;
using System.Collections.Generic;
using Maps.Extensions;
using Maps.Geometry;

namespace Maps.Geographical
{
    /// <summary>
    /// 2 dimensional immutable geodetic line strip
    /// </summary>
    public sealed class GeodeticLineStrip2d : IReadOnlyList<Geodetic2d>
    {
        /// <inheritdoc />
        public Geodetic2d this[int index] => _coordinates[index];

        /// <inheritdoc />
        public int Count => _coordinates.Count;

        /// <summary>
        /// The length of the line strip in meters
        /// </summary>
        public double Length
        {
            get
            {
                var total = Geodetic2d.Distance(_coordinates[0], _coordinates[1]);

                for (var i = 1; i < Count - 1; ++i)
                {
                    total += Geodetic2d.Distance(_coordinates[i], _coordinates[i + 1]);
                }

                return total;
            }
        }

        /// <summary>
        /// Is the linestrip closed?
        /// </summary>
        public bool Closed => LineStrip.Closed;

        /// <summary>
        /// The geometric linestrip
        /// </summary>
        public readonly LineStrip2d LineStrip;

        private readonly IList<Geodetic2d> _coordinates;

        /// <summary>
        /// Initializes a new instance of GeodeticLineStrip2d with coordinates
        /// </summary>
        /// <param name="coordinates">The coordinates to use</param>
        public GeodeticLineStrip2d(IList<Geodetic2d> coordinates) : this(coordinates, 
            false){}

        /// <summary>
        /// Initializes a new instance of GeodeticLineStrip2d with coordinates
        /// </summary>
        /// <param name="coordinates">The coordinates to use</param>
        /// <param name="close">Should the linestrip be forced closed?</param>
        internal GeodeticLineStrip2d(IList<Geodetic2d> coordinates, bool close)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            if (coordinates.Count < 2)
            {
                throw new ArgumentException("Must provide more than one " +
                                            "coordinate", nameof(coordinates));
            }

            // not closed and should be closed
            var willBeClosed = coordinates[0] != coordinates[coordinates.Count - 1] && close;
            var coordiantesCount = willBeClosed ? coordinates.Count + 1 : coordinates.Count;

            // copy over all coordinates
            _coordinates = new Geodetic2d[coordiantesCount];

            for (var i = 0; i < coordinates.Count; i++)
            {
                _coordinates[i] = coordinates[i];
            }

            // if its to be closed, close it
            if (willBeClosed)
            {
                _coordinates[coordiantesCount - 1] = coordinates[0];
            }

            // create the geometric representation
            LineStrip = new LineStrip2d(_coordinates);
        }

        /// <summary>
        /// Initializes a new instance of GeodeticLineStrip2d with a linestrip
        /// </summary>
        /// <param name="linestrip">The geometric linestrip to create from</param>
        public GeodeticLineStrip2d(LineStrip2d linestrip)
        {
            if (linestrip == null)
            {
                throw new ArgumentNullException(nameof(linestrip));
            }

            var vertexCount = linestrip.Count;
            _coordinates = new Geodetic2d[vertexCount];
            for (var i = 0; i < vertexCount; i++)
            {
                _coordinates[i] = new Geodetic2d(linestrip[i]);
            }

            // reference the geometric representation
            LineStrip = linestrip;
        }

        /// <summary>
        /// Tries to join the linestrip to the given linestrip, will join if the host's
        /// final coordinate is close enough to the subject's first coordinate
        /// </summary>
        /// <param name="subject">The subject linestrip to attempt to join</param>
        /// <param name="joined">The resulting linestrip</param>
        /// <returns>True on successfully joined, false otherwise</returns>
        /// <exception cref="ArgumentNullException">Thrown if subject is null</exception>
        public bool TryJoin(GeodeticLineStrip2d subject, out GeodeticLineStrip2d joined)
        {
            if (subject == null)
            {
                throw new ArgumentNullException(nameof(subject));
            }

            var result = false;
            joined = null;
            LineStrip2d joinedLineStrip;
            if (LineStrip.TryJoin(subject.LineStrip, out joinedLineStrip))
            {
                joined = new GeodeticLineStrip2d(joinedLineStrip);
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
        public static IList<GeodeticLineStrip2d> Combine(
            IList<GeodeticLineStrip2d> linestrips)
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

            // extract the geometric linestrips
            var linestripCount = linestrips.Count;
            var geometric = new LineStrip2d[linestripCount];
            for (var i = 0; i < linestripCount; ++i)
            {
                geometric[i] = linestrips[i].LineStrip;
            }

            var combined = LineStrip2d.Combine(geometric);

            // create the geodetic linestrips from the combined geometric linestrips
            var combinedCount = combined.Count;
            var result = new GeodeticLineStrip2d[combinedCount];
            for (var i = 0; i < combinedCount; ++i)
            {
                result[i] = new GeodeticLineStrip2d(combined[i]);
            }

            return result;
        }

        /// <summary>
        /// Returns the linestrip, relative to the given coordinate
        /// </summary>
        /// <param name="coordinate">The coordinate to become relative to</param>
        /// <returns>A linestrip relative to the given coordinate</returns>
        public GeodeticLineStrip2d Relative(Geodetic2d coordinate)
        {
            return new GeodeticLineStrip2d(LineStrip.Relative(coordinate.Point));
        }

        /// <summary>
        /// Returns the linestrip, reversing relativity to a given coordinate
        /// </summary>
        /// <param name="coordinate">The coordinate to reverse relativity to</param>
        /// <returns>A linestrip relative to the given coordinate</returns>
        public GeodeticLineStrip2d Absolute(Geodetic2d coordinate)
        {
            return new GeodeticLineStrip2d(LineStrip.Absolute(coordinate.Point));
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"N = {Count}, d = {Length}";
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if(!(obj is GeodeticLineStrip2d))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (GeodeticLineStrip2d)obj;
            return LineStrip.Equals(other.LineStrip);
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public IEnumerator<Geodetic2d> GetEnumerator()
        {
            for (var i = 0; i < _coordinates.Count; ++i)
            {
                yield return _coordinates[i];
            }
        }
    }
}