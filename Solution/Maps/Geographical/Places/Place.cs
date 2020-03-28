using System;
using System.Collections.Generic;
using System.Drawing;
using Maps.Appearance;
using Maps.Geographical.Features;
using Maps.Geometry.Simplification;
using Maps.IO;
using Maps.IO.Features;
using Maps.IO.Places;
using Maps.Rendering;

namespace Maps.Geographical.Places
{
    /// <summary>
    /// Represents real world Place or Point of Interest
    /// </summary>
    public sealed class Place : Feature
    {
        /// <summary>
        /// The coordinate of the Place
        /// </summary>
        public readonly Geodetic2d Coordinate;

        /// <summary>
        /// The category of the Place
        /// </summary>
        public readonly PlaceCategory Category;

        /// <summary>
        /// The icon of the place
        /// </summary>
        public readonly Bitmap Icon;

        /// <summary>
        /// Initialises a new instance of Place
        /// </summary>
        /// <param name="guid">The guid of the place</param>
        /// <param name="name">The name of the place</param>
        /// <param name="coordinate">The coordinate of the place</param>
        /// <param name="category">The category the place belongs to</param>
        /// <param name="icon">The icon of the place</param>
        /// <exception cref="ArgumentNullException">Thrown if category is null 
        /// </exception>
        public Place(Guid guid, string name, Geodetic2d coordinate, 
            PlaceCategory category, Bitmap icon = null) : base(guid, name)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            Icon = icon;
            Coordinate = coordinate;
            Category = category;
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

            return appearance.AppearanceFor(this).RenderablesFor(this, anchor, 
                scale);
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

        /// <summary>
        /// Evaluates if the place connects to the given place
        /// </summary>
        /// <param name="place">The place to evaluate against</param>
        /// <returns>True if connected, false otherwise</returns>
        /// <exception cref="ArgumentNullException">Thrown if place is null</exception>
        public bool ConnectionTo(Place place)
        {
            if (place == null)
            {
                throw new ArgumentNullException(nameof(place));
            }

            return Coordinate == place.Coordinate;
        }

        /// <summary>
        /// Evaluates if the place connects to the given segment
        /// </summary>
        /// <param name="segment">The segment to evaluate against</param>
        /// <returns>True if connected, false otherwise</returns>
        /// <exception cref="ArgumentNullException">Thrown if segment is null</exception>
        public bool ConnectionTo(Segment segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            return ResolveConnection(Coordinate, segment.LineStrip);
        }

        /// <summary>
        /// Evaluates if the place connects to the given area
        /// </summary>
        /// <param name="area">The area to evaluate against</param>
        /// <returns>True if connected, false otherwise</returns>
        /// <exception cref="ArgumentNullException">Thrown if area is null</exception>
        public bool ConnectionTo(Area area)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            return ResolveConnection(Coordinate, area.Polygon);
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
            return new Place(Guid, Name, Coordinate - coordinate, Category, Icon);
        }

        /// <inheritdoc />
        public override Feature Absolute(Geodetic2d coordinate)
        {
            return new Place(Guid, Name, Coordinate + coordinate, Category, Icon);
        }

        /// <inheritdoc />
        public override BinaryFeature ToBinary(ISideData sideData = null)
        {
            return new BinaryPlace(this, sideData);
        }

        /// <inheritdoc />
        public override IList<Feature> ClipTo(GeodeticBox2d box)
        {
            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            // simple for place, if the coordinate is contained or not
            if (box.Contains(Coordinate))
            {
                return new Feature[]
                {
                    this
                };
            }

            return EmptyFeatures;
        }

        /// <inheritdoc />
        public override Feature Simplify(IGeodeticSimplifier2d simplifier, IList<Feature> neighbours, GeodeticPolygon2d preservedEdges)
        {
            return this;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Name + " @ " + Coordinate + " (" + Category + ")";
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is Place))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (Place) obj;
            return Category.Equals(other.Category) && 
                   Name.Equals(other.Name) && 
                   Coordinate.Equals(other.Coordinate);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hash = Category.GetHashCode();

            unchecked
            {
                hash = (hash * 397) ^ Name.GetHashCode();
                hash = (hash * 397) ^ Coordinate.GetHashCode();

                return hash;
            }
        }
    }
}