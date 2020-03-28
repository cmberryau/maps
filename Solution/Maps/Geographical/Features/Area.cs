using System;
using System.Collections.Generic;
using Maps.Appearance;
using Maps.Extensions;
using Maps.Geographical.Places;
using Maps.Geometry;
using Maps.Geometry.Simplification;
using Maps.IO;
using Maps.IO.Features;
using Maps.Rendering;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// Represents geographical area feature
    /// </summary>
    public sealed class Area : Feature
    {
        /// <summary>
        /// The geodetic polygon of the area
        /// </summary>
        public readonly GeodeticPolygon2d Polygon;

        /// <summary>
        /// The original area of the area in square meters
        /// </summary>
        public readonly double OriginalArea;

        /// <summary>
        /// The category of the area
        /// </summary>
        public readonly AreaCategory Category;

        /// <summary>
        /// Initializes a new instance of Area
        /// </summary>
        /// <param name="guid">The guid of the area</param>
        /// <param name="name">The name of the area</param>
        /// <param name="polygon">The polygon of the area</param>
        /// <param name="category">The category of the area</param>
        /// <param name="area">The original area of the area in sqm</param>
        /// <exception cref="ArgumentNullException">Thrown if polygon or category is null
        /// </exception>
        public Area(Guid guid, string name, GeodeticPolygon2d polygon,
            AreaCategory category, double area) : base(guid, name)
        {
            if (polygon == null)
            {
                throw new ArgumentNullException(nameof(polygon));
            }

            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            Polygon = polygon;
            Category = category;
            OriginalArea = area;
        }

        /// <summary>
        /// Evaluates the connections at each outer coordinate to the given place's 
        /// coordinate
        /// </summary>
        /// <remarks>Only resolves connections of overlapping coordinates, not
        /// intersections along the length of any feature</remarks>
        /// <param name="place">The place to evaluate against</param>
        /// <returns>Boolean list corresponding to coordinate indices</returns>
        /// <exception cref="ArgumentNullException">Thrown if place is null</exception>
        public IList<IList<bool>> ConnectionTo(Place place)
        {
            if (place == null)
            {
                throw new ArgumentNullException(nameof(place));
            }

            var connections = new IList<bool>[Polygon.HoleCount + 1];
            connections[0] = ResolveConnections(Polygon, place.Coordinate);

            // add holes
            for (var i = 0; i < Polygon.HoleCount; ++i)
            {
                connections[i + 1] = ResolveConnections(Polygon.Hole(i),
                    place.Coordinate);
            }

            return connections;
        }

        /// <summary>
        /// Evaluates connections at each coordinate to the given segment's coordinates
        /// </summary>
        /// <remarks>Only resolves connections of overlapping coordinates, not
        /// intersections along the length of any feature</remarks>
        /// <param name="segment">The segment to evaluate against</param>
        /// <returns>Boolean list corresponding to coordinate indices</returns>
        /// <exception cref="ArgumentNullException">Thrown if segment is null</exception>
        public IList<IList<bool>> ConnectionTo(Segment segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            var connections = new IList<bool>[Polygon.HoleCount + 1];
            connections[0] = ResolveConnections(Polygon, segment.LineStrip);

            // add holes
            for (var i = 0; i < Polygon.HoleCount; ++i)
            {
                connections[i + 1] = ResolveConnections(Polygon.Hole(i),
                    segment.LineStrip);
            }

            return connections;
        }

        /// <summary>
        /// Evaluates connections at each coordinate to the given area's outer 
        /// coordinates
        /// </summary>
        /// <remarks>Only resolves connections of overlapping coordinates, not
        /// intersections along the length of any feature</remarks>
        /// <param name="area">The area to evaluate against</param>
        /// <returns>Boolean list corresponding to coordinate indices</returns>
        /// <exception cref="ArgumentNullException">Thrown if area is null</exception>
        public IList<IList<bool>> ConnectionTo(Area area)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            var connections = new IList<bool>[Polygon.HoleCount + 1];
            connections[0] = ResolveConnections(Polygon, area.Polygon);

            // add holes
            for (var i = 0; i < Polygon.HoleCount; ++i)
            {
                for (var k = 0; k < area.Polygon.HoleCount; ++k)
                {
                    connections[i + 1] = ResolveConnections(Polygon.Hole(i),
                        area.Polygon.Hole(k));
                }
            }

            return connections;
        }

        /// <summary>
        /// Combines the area with the given areas
        /// </summary>
        /// <param name="areas">The areas to combine with</param>
        /// <returns>A list of combined areas</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="areas"/> 
        /// is null</exception>
        /// <exception cref="ArgumentException">Thrown if all elements of 
        /// <paramref name="areas"/> do not all have the same guid, or if any element 
        /// is null</exception>
        /// <remarks>All elements of <paramref name="areas"/> must have the same 
        /// guid</remarks>
        public static IList<Area> Combine(IList<Area> areas)
        {
            if (areas == null)
            {
                throw new ArgumentNullException(nameof(areas));
            }

            areas.AssertNoNullEntries();

            // extract the polygons
            var areaCount = areas.Count;
            var polygons = new GeodeticPolygon2d[areaCount];
            for (var i = 0; i < areaCount; ++i)
            {
                polygons[i] = areas[i].Polygon;
            }

            var indices = new int[areaCount];
            var combined = GeodeticPolygon2d.Combine(polygons, indices);

            // check if any could combine
            var combinedCount = combined.Count;
            if (combinedCount != areaCount)
            {
                // evaluate the combined geodetic areas
                var geodeticAreas = new double[combinedCount];
                for (var i = 0; i < areaCount; ++i)
                {
                    // determine which combined polygon the original area ended up in
                    var index = indices[i];

                    // determine the factor of the original area is in the combined area (geometric will do)
                    var factor = areas[i].Polygon.Polygon.Area / combined[index].Polygon.Area;

                    // increase the geodetic area by that factor
                    geodeticAreas[index] += areas[i].OriginalArea * factor;
                }

                // create the areas from the combined polygons
                var result = new Area[combinedCount];
                var area = areas[0];
                for (var i = 0; i < combinedCount; ++i)
                {
                    result[i] = new Area(Guid.NewGuid(), area.Name, combined[i],
                        area.Category, geodeticAreas[i]);
                }

                return result;
            }

            return areas;
        }

        /// <summary>
        /// Evaluates if the area could combine with another area, does not
        /// guarantee that they will successfully combine geometrically
        /// </summary>
        /// <param name="area">The area to evaluate against</param>
        /// <returns>True if could combine</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="area"/>
        /// is null</exception>
        public bool CouldCombine(Area area)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            return area.Category.Equals(Category) && Bounds2d.Intersects(
                area.Polygon.Polygon.Bounds, Polygon.Polygon.Bounds);
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
            return new Area(Guid, Name, Polygon.Relative(coordinate), Category, 
                OriginalArea);
        }

        /// <inheritdoc />
        public override Feature Absolute(Geodetic2d coordinate)
        {
            return new Area(Guid, Name, Polygon.Absolute(coordinate), Category,
                OriginalArea);
        }

        /// <inheritdoc />
        public override BinaryFeature ToBinary(ISideData sideData = null)
        {
            return new BinaryArea(this, sideData);
        }

        /// <inheritdoc />
        public override IList<Feature> ClipTo(GeodeticBox2d box)
        {
            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            var clippedPolygons = box.Clip(Polygon);
            IList<Feature> features;

            if (clippedPolygons.Count == 0)
            {
                features = new Feature[0];
            }
            else
            {
                features = new List<Feature>();
                for (var i = 0; i < clippedPolygons.Count; ++i)
                {
                    if (clippedPolygons[i].Polygon.Area > Mathd.EpsilonE10)
                    {
                        features.Add(new Area(Guid, Name, clippedPolygons[i], Category,
                            OriginalArea));
                    }
                }
            }

            return features;
        }

        /// <inheritdoc />
        public override Feature Simplify(IGeodeticSimplifier2d simplifier, IList<Feature> neighbours, GeodeticPolygon2d preservedEdges)
        {
            var neighboursCount = neighbours.Count;
            GeodeticPolygon2d simplified;

            if (neighboursCount > 0)
            {
                var polygon = Polygon;
                var holeCount = polygon.HoleCount;
                var connections = new bool[holeCount + 1][];

                // create the connections array
                connections[0] = new bool[polygon.Count];
                for (var i = 0; i < holeCount; ++i)
                {
                    var hole = polygon.Hole(i);
                    connections[i + 1] = new bool[hole.Count];
                }

                // run through the features
                for (var i = 0; i < neighboursCount; ++i)
                {
                    var feature = neighbours[i];

                    // make sure we don't resolve connections from a feature to itself
                    if (feature.Equals(this))
                    {
                        continue;
                    }

                    var nextConnections = feature.ConnectionsFrom(this);

                    // update the connections array
                    for (var j = 0; j < polygon.Count; ++j)
                    {
                        connections[0][j] |= nextConnections[0][j];
                    }

                    for (var j = 0; j < holeCount; ++j)
                    {
                        var hole = polygon.Hole(j);
                        for (var k = 0; k < hole.Count; ++k)
                        {
                            connections[j + 1][k] |= connections[j + 1][k];
                        }
                    }
                }

                simplified = simplifier.Simplify(Polygon, connections, preservedEdges);
            }
            else
            {
                simplified = simplifier.Simplify(Polygon, preservedEdges);
            }

            return new Area(Guid, Name, simplified, Category, OriginalArea);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is Area))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (Area) obj;

            return Category.Equals(other.Category) && 
                   Name.Equals(other.Name) && 
                   Polygon.Equals(other.Polygon);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hash = Category.GetHashCode();

            unchecked
            {
                hash = (hash * 397) ^ Name.GetHashCode();
                hash = (hash * 397) ^ Polygon.GetHashCode();

                return hash;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Area: {Guid} - '{Name}' - {Polygon}";
        }
    }
}
