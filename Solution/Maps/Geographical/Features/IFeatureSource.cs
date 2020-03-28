using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// Interface for a source of map features
    /// </summary>
    public interface IFeatureSource : IDisposable, ICloneable
    {
        /// <summary>
        /// The meta of the source
        /// </summary>
        SourceMeta Meta
        {
            get;
        }

        /// <summary>
        /// Returns features for the given coordinate box
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        IList<Feature> Get(GeodeticBox2d box);

        /// <summary>
        /// Returns features for the given coordinate box asyncronously
        /// </summary>
        /// <param name="box">The coordinate box to evaluate</param>
        /// <exception cref="Exception">Thrown when connection to API
        /// fails or on recieving malformed data from API</exception>
        Task<IList<Feature>> GetAsync(GeodeticBox2d box);
    }
}
