using System;
using System.Collections.Generic;
using Maps.Geographical.Tiles;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// Interface for a target of map features using a tiling scheme to partition data
    /// </summary>
    public interface ITiledFeatureTarget : IDisposable
    {
        /// <summary>
        /// The meta information of the target
        /// </summary>
        TiledSourceMeta Meta
        {
            set;
        }

        /// <summary>
        /// Describes the tiling strategy used
        /// </summary>
        ITileSource TileSource
        {
            get;
        }

        /// <summary>
        /// Writes features to the target that belong to the given tile
        /// </summary>
        /// <param name="tile">The tile that the features belong to</param>
        /// <param name="features">The features to write</param>
        void Write(Tile tile, IList<Feature> features);

        /// <summary>
        /// Writes features to the target that belong to the given tile
        /// </summary>
        /// <param name="tiles">The tiles that the features belong to</param>
        /// <param name="features">The features to write</param>
        void Write(IList<Tile> tiles, IList<IList<Feature>> features);

        /// <summary>
        /// Flushes the target
        /// </summary>
        void Flush();
    }
}