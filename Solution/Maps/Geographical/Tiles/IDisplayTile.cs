using System.Collections.Generic;
using Maps.Appearance;
using Maps.Geographical.Features;

namespace Maps.Geographical.Tiles
{
    /// <summary>
    /// Interface for objects controlling display of a single map tile
    /// </summary>
    public interface IDisplayTile
    {
        /// <summary>
        /// Is the display tile active? If active, it's displayed
        /// </summary>
        bool Active
        {
            get;
            set;
        }

        /// <summary>
        /// Has the display had it's features set yet?
        /// </summary>
        bool HasFeatures
        {
            get;
        }

        /// <summary>
        /// The appearance of the display tile
        /// </summary>
        IMapAppearance Appearance
        {
            get;
            set;
        }

        /// <summary>
        /// Sets the features for the display tile
        /// </summary>
        /// <param name="features">The features to set</param>
        void SetFeatures(IList<Feature> features);

        /// <summary>
        /// Notifies the display tile that there has been an update
        /// </summary>
        void OnUpdate();
    }
}