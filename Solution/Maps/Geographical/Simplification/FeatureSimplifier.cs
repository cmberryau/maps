using System;
using System.Collections.Generic;
using Maps.Extensions;
using Maps.Geographical.Features;
using Maps.Geographical.Tiles;
using Maps.Geometry.Simplification;

namespace Maps.Geographical.Simplification
{
    /// <summary>
    /// Responsible for simplifying map features
    /// </summary>
    public class FeatureSimplifier
    {
        private readonly IGeodeticSimplifier2d _simplifier;

        /// <summary>
        /// Initializes new instance of FeatureSimplifier
        /// </summary>
        /// <param name="simplifier">The geodetic simplifier to use</param>
        public FeatureSimplifier(IGeodeticSimplifier2d simplifier)
        {
            if (simplifier == null)
            {
                throw new ArgumentNullException(nameof(simplifier));
            }

            _simplifier = simplifier;
        }

        /// <summary>
        /// Simplifies the given features
        /// </summary>
        /// <param name="features">The features to simplify</param>
        /// <param name="tile">The tile the features belong to</param>
        /// <returns>The simplified features</returns>
        public IList<Feature> Simplify(IList<Feature> features, Tile tile)
        {
            if (features == null)
            {
                throw new ArgumentNullException(nameof(features));
            }

            if (tile == null)
            {
                throw new ArgumentNullException(nameof(tile));
            }

            features.AssertNoNullEntries();

            var featureCount = features.Count;
            var simplifiedFeatures = new Feature[featureCount];
            for (var i = 0; i < featureCount; ++i)
            {
                simplifiedFeatures[i] = features[i].Simplify(_simplifier, features, tile.Box.Polygon);
            }

            return simplifiedFeatures;
        }
    }
}