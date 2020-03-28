using System;
using System.Collections.Generic;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// Interface for a target of map features
    /// </summary>
    public interface IFeatureTarget : IDisposable
    {
        /// <summary>
        /// Writes features to the target
        /// </summary>
        /// <param name="features">The features to write</param>
        void Write(IList<Feature> features);

        /// <summary>
        /// Writes features to the target
        /// </summary>
        /// <param name="features">The features to write</param>
        void Write(IList<IList<Feature>> features);
    }
}