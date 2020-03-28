using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maps.Geographical.Tiles;
using Maps.IO;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// Interface for a source of map features using a tiling scheme to partition data
    /// </summary>
    public interface ITiledFeatureSource : IDisposable, ICloneable
    {
        /// <summary>
        /// The meta of the source
        /// </summary>
        TiledSourceMeta Meta
        {
            get;
        }

        /// <summary>
        /// The tile source used by the feature source
        /// </summary>
        ITileSource TileSource
        {
            get;
        }

        /// <summary>
        /// The side data source used by the feature source
        /// </summary>
        ISideData SideData
        {
            get;
        }

        /// <summary>
        /// Returns features for the given tile
        /// </summary>
        /// <param name="tile">The tile to evaluate</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        IList<Feature> Get(Tile tile);

        /// <summary>
        /// Returns features for the given tiles, split into a list for each tile
        /// </summary>
        /// <param name="tiles">The tiles to evaluate</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        IList<IList<Feature>> Get(IList<Tile> tiles);

        /// <summary>
        /// Returns features for the given tile asyncronously
        /// </summary>
        /// <param name="tile">The tile to evaluate</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        Task<IList<Feature>> GetAsync(Tile tile);

        /// <summary>
        /// Returns features for the given tiles asyncronously, split into a list for 
        /// each tile
        /// </summary>
        /// <param name="tiles">The tiles to evaluate</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        Task<IList<IList<Feature>>> GetAsync(IList<Tile> tiles);    
    }
}