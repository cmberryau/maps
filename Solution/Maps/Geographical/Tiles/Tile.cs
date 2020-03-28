using System;
using System.Collections.Generic;
using Maps.Extensions;
using Maps.Geographical.Features;

namespace Maps.Geographical.Tiles
{
    /// <summary>
    /// Represents a generic geographical tile using a fixed tiling scheme (constant 
    /// longitude and latitude)
    /// </summary>
    public abstract class Tile
    {
        /// <summary>
        /// The id of this tile, unique across all lods
        /// </summary>
        public readonly long Id;

        /// <summary>
        /// The box that this Tile represents
        /// </summary>
        public GeodeticBox2d Box
        {
            get;
            protected set;
        }

        /// <summary>
        /// The area covered by the tile
        /// </summary>
        public Area Area
        {
            get
            {
                var poly = new GeodeticPolygon2d((IList<Geodetic2d>)new[]
                {
                    Box[0],
                    Box[1],
                    Box[2],
                    Box[3],
                });

                return new Area(Id.ToGuid(), ToString(), poly, new AreaCategory(
                    RootAreaCategory.Tile), poly.Area);
            }
        }

        /// <summary>
        /// The tiles contained by this tile on the next detail level
        /// </summary>
        public abstract IList<Tile> SubTiles
        {
            get;
        }

        /// <summary>
        /// The tiles containing by this tile on the previous detail level
        /// </summary>
        public abstract IList<Tile> SuperTiles
        {
            get;
        }

        /// <summary>
        /// Initializes the components of the Tile class
        /// </summary>
        /// <param name="id">The unique id of the tile</param>
        protected Tile(long id)
        {
            Id = id;
        }

        /// <summary>
        /// Clips the given features to the tile
        /// </summary>
        /// <param name="features">The features to clip</param>
        public IList<Feature> Clip(IList<Feature> features)
        {
            if (features == null)
            {
                throw new ArgumentNullException(nameof(features));
            }

            return Box.Clip(features);
        }

        /// <summary>
        /// Returns the features, relative to the centre of the tile
        /// </summary>
        /// <param name="features">The features to make relative</param>
        public IList<Feature> Relative(IList<Feature> features)
        {
            if (features == null)
            {
                throw new ArgumentNullException(nameof(features));
            }

            var featureCount = features.Count;
            for (var i = 0; i < featureCount; ++i)
            {
                features[i] = features[i].Relative(Box.Centre);
            }

            return features;
        }

        /// <summary>
        /// Returns the features, reversing relativity to the tile
        /// </summary>
        /// <param name="features">The features to make absolute</param>
        public IList<Feature> Absolute(IList<Feature> features)
        {
            if (features == null)
            {
                throw new ArgumentNullException(nameof(features));
            }

            var featureCount = features.Count;
            for (var i = 0; i < featureCount; ++i)
            {
                features[i] = features[i].Absolute(Box.Centre);
            }

            return features;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"0x{Id:x16}";
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (!(obj is Tile))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return ((Tile)obj).Id == Id;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}