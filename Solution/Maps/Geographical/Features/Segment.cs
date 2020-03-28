using System;
using System.Collections.Generic;
using Maps.Appearance;
using Maps.Extensions;
using Maps.Geographical.Places;
using Maps.Geometry.Simplification;
using Maps.IO;
using Maps.IO.Features;
using Maps.Rendering;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// Represents a geographical segment feature
    /// </summary>
    public class Segment : Feature
    {
        /// <summary>
        /// The coordinates of the segment feature as a linestrip
        /// </summary>
        public readonly GeodeticLineStrip2d LineStrip;

        /// <summary>
        /// The category of the segment
        /// </summary>
        public readonly SegmentCategory Category;

        /// <summary>
        /// Initializes a new instance of Segment
        /// </summary>
        /// <param name="guid">The guid of the segment</param>
        /// <param name="name">The name of the segment</param>
        /// <param name="coordinates">The coordinates of the segment</param>
        /// <param name="category">The category of the segment</param>
        /// <exception cref="ArgumentNullException">Thrown if coordinates or category 
        /// is null </exception>
        public Segment(Guid guid, string name, IList<Geodetic2d> coordinates, 
            SegmentCategory category) : base(guid, name)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            LineStrip = new GeodeticLineStrip2d(coordinates);
            Category = category;
        }

        /// <summary>
        /// Initializes a new instance of Segment
        /// </summary>
        /// <param name="guid">The guid of the segment</param>
        /// <param name="name">The name of the segment</param>
        /// <param name="linestrip">The coordinates of the segment</param>
        /// <param name="category">The category of the</param>
        /// <exception cref="ArgumentNullException">Thrown if linestrip or category 
        /// is null </exception>
        public Segment(Guid guid, string name, GeodeticLineStrip2d linestrip, 
            SegmentCategory category) : base(guid, name)
        {
            if (linestrip == null)
            {
                throw new ArgumentNullException(nameof(linestrip));
            }

            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            LineStrip = linestrip;
            Category = category;
        }

        /// <summary>
        /// Initializes a new instance of Segment
        /// </summary>
        /// <param name="segment"></param>
        public Segment(Segment segment) : base(segment.Guid, segment.Name)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            LineStrip = segment.LineStrip;
            Category = segment.Category;
        }

        /// <summary>
        /// Evaluates the connections at each coordinate to the given place's coordinate
        /// </summary>
        /// <remarks>Only resolves connections of overlapping coordinates, not
        /// intersections along the length of any feature</remarks>
        /// <param name="place">The place to evaluate against</param>
        /// <returns>Boolean list corresponding to coordinate indices</returns>
        /// <exception cref="ArgumentNullException">Thrown if place is null</exception>
        public IList<bool> ConnectionTo(Place place)
        {
            if (place == null)
            {
                throw new ArgumentNullException(nameof(place));
            }

            return ResolveConnections(LineStrip, place.Coordinate);
        }

        /// <summary>
        /// Evaluates connections at each coordinate to the given segment's coordinates
        /// </summary>
        /// <remarks>Only resolves connections of overlapping coordinates, not
        /// intersections along the length of any feature</remarks>
        /// <param name="segment">The segment to evaluate against</param>
        /// <returns>Boolean list corresponding to coordinate indices</returns>
        /// <exception cref="ArgumentNullException">Thrown if segment is null</exception>
        public IList<bool> ConnectionTo(Segment segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            return ResolveConnections(LineStrip, segment.LineStrip);
        }

        /// <summary>
        /// Evaluates connections at each coordinate to the given areas's outer 
        /// coordinates
        /// </summary>
        /// <remarks>Only resolves connections of overlapping coordinates, not
        /// intersections along the length of any feature</remarks>
        /// <param name="area">The area to evaluate against</param>
        /// <returns>Boolean list corresponding to coordinate indices</returns>
        /// <exception cref="ArgumentNullException">Thrown if area is null</exception>
        public IList<bool> ConnectionTo(Area area)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            return ResolveConnections(LineStrip, area.Polygon);
        }

        /// <summary>
        /// Attempts to combine the given segments
        /// </summary>
        /// <param name="segments">The segments to combine</param>
        /// <returns>A list of potentially combined segments</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="segments"/>
        /// is null</exception>
        /// <exception cref="ArgumentException">Thrown if all elements of 
        /// <paramref name="segments"/> do not all have the same guid, or if any element 
        /// is null</exception>
        /// <remarks>All elements of <paramref name="segments"/> must have the same 
        /// guid</remarks>
        public static IList<Segment> Combine(IList<Segment> segments)
        {
            if (segments == null)
            {
                throw new ArgumentNullException(nameof(segments));
            }

            segments.AssertNoNullEntries();

            if (segments.Count < 2)
            {
                return segments;
            }

            // extract the strips
            var segmentCount = segments.Count;
            var strips = new GeodeticLineStrip2d[segmentCount];
            for (var i = 0; i < segmentCount; ++i)
            {
                strips[i] = segments[i].LineStrip;
            }

            // combine the strips
            var combined = GeodeticLineStrip2d.Combine(strips);
            IList<Segment> result;
            // if we don't get any combination, just return them
            var combinedCount = combined.Count;
            if (combinedCount == segmentCount)
            {
                result = segments;
            }
            else
            {
                // create the segments from the combined strips
                result = new Segment[combinedCount];
                var segment = segments[0];
                var guid = Guid.NewGuid();
                var name = segment.Name;
                var category = segment.Category;
                for (var i = 0; i < combinedCount; ++i)
                {
                    result[i] = new Segment(guid, name, combined[i], category);
                }
            }

            return result;
        }

        /// <summary>
        /// Evaluates if the segment could combine with another segment, does not
        /// guarantee that they will successfully combine geometrically
        /// </summary>
        /// <param name="segment">The segment to evaluate against</param>
        /// <returns>True if could combine</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="segment"/>
        /// is null</exception>
        public virtual bool CanCombine(Segment segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            return segment.Category.Equals(Category) && segment.Name.Equals(Name);
        }

        /// <inheritdoc />
        public override IList<Renderable> RenderablesFor(IMapAppearance appearance)
        {
            if (appearance == null)
            {
                throw new ArgumentNullException(nameof(appearance));
            }

            return appearance.AppearanceFor(this).RenderablesFor(this);
        }

        /// <inheritdoc />
        public override IList<Renderable> RenderablesFor(IMapAppearance appearance, 
            Vector3d anchor, double scale)
        {
            if (appearance == null)
            {
                throw new ArgumentNullException(nameof(appearance));
            }

            return appearance.AppearanceFor(this).RenderablesFor(this, anchor, scale);
        }

        /// <inheritdoc />
        public override void Accept(IFeatureVisitor visitor)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException(nameof(visitor));
            }

            visitor.Visit(this);
        }

        /// <inheritdoc />
        public override TResult Accept<TResult>(IFeatureVisitor<TResult> visitor)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException(nameof(visitor));
            }

            return visitor.Visit(this);
        }

        /// <inheritdoc />
        public override TResult Accept<TResult, T0>(IFeatureVisitor<TResult, T0> visitor,
            T0 param)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException(nameof(visitor));
            }

            if (param == null)
            {
                throw new ArgumentNullException(nameof(param));
            }

            return visitor.Visit(this, param);
        }

        /// <inheritdoc />
        public override TResult Accept<TResult, T0, T1>(IFeatureVisitor<TResult, T0, T1>
            visitor, T0 param, T1 param2)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException(nameof(visitor));
            }

            if (param == null)
            {
                throw new ArgumentNullException(nameof(param));
            }

            if (param2 == null)
            {
                throw new ArgumentNullException(nameof(param2));
            }

            return visitor.Visit(this, param, param2);
        }

        /// <inheritdoc />
        public override bool ConnectedTo(Place place)
        {
            return place.ConnectionTo(this);
        }

        /// <inheritdoc />
        public override IList<bool> ConnectionsFrom(Segment segment)
        {
            return segment.ConnectionTo(this);
        }

        /// <inheritdoc />
        public override IList<IList<bool>> ConnectionsFrom(Area area)
        {
            return area.ConnectionTo(this);
        }

        /// <inheritdoc />
        public override Feature Relative(Geodetic2d coordinate)
        {
            return new Segment(Guid, Name, LineStrip.Relative(coordinate), Category);
        }

        /// <inheritdoc />
        public override Feature Absolute(Geodetic2d coordinate)
        {
            return new Segment(Guid, Name, LineStrip.Absolute(coordinate), Category);
        }

        /// <inheritdoc />
        public override BinaryFeature ToBinary(ISideData sideData = null)
        {
            return new BinarySegment(this, sideData);
        }

        /// <inheritdoc />
        public override IList<Feature> ClipTo(GeodeticBox2d box)
        {
            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            // clip coordinate linestrip to the box
            var clippedLineStrips = box.Clip(LineStrip);

            if (clippedLineStrips.Count > 0)
            {
                // create and fill the result array, copying in all info
                var result = new Feature[clippedLineStrips.Count];

                for (var i = 0; i < result.Length; i++)
                {
                    result[i] = new Segment(Guid, Name, clippedLineStrips[i], Category);
                }

                return result;
            }

            return EmptyFeatures;
        }

        /// <inheritdoc />
        public override Feature Simplify(IGeodeticSimplifier2d simplifier, IList<Feature> neighbours, GeodeticPolygon2d preservedEdges)
        {
            var neighboursCount = neighbours.Count;
            GeodeticLineStrip2d simplified;

            if (neighboursCount > 0)
            {
                var connectionsCount = LineStrip.Count;
                var connections = new bool[connectionsCount];

                for (var i = 0; i < neighboursCount; ++i)
                {
                    var feature = neighbours[i];
                    var nextConnections = feature.ConnectionsFrom(this);

                    // update the connections array
                    for (var j = 0; j < connectionsCount; ++j)
                    {
                        connections[j] |= nextConnections[j];
                    }
                }

                simplified = simplifier.Simplify(LineStrip, connections);
            }
            else
            {
                simplified = simplifier.Simplify(LineStrip);
            }

            return new Segment(Guid, Name, simplified, Category);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is Segment))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (Segment) obj;
            return Category.Equals(other.Category) &&
                   Name.Equals(other.Name) &&
                   LineStrip.Equals(other.LineStrip);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hash = Category.GetHashCode();

            unchecked
            {
                hash = (hash * 397) ^ Name.GetHashCode();
                hash = (hash * 397) ^ LineStrip.GetHashCode();

                return hash;
            }
        }
    }
}
