using System.Collections.Generic;
using Maps.Geographical.Features;
using Maps.Geographical.Places;
using Maps.Geographical.Projection;

namespace Maps.Appearance
{
    /// <summary>
    /// Interface for providing information on how a map should appear
    /// </summary>
    public interface IMapAppearance
    {
        /// <summary>
        /// All unique feature appearances
        /// </summary>
        IList<FeatureAppearance> FeatureAppearances
        {
            get;
        }

        /// <summary>
        /// All unique mesh appearances
        /// </summary>
        IList<MeshAppearance> MeshApperances
        {
            get;
        }

        /// <summary>
        /// All unique ui element appearances
        /// </summary>
        IList<UIRenderableAppearance> UIElementAppearances
        {
            get;
        }

        /// <summary>
        /// The projection for the map
        /// </summary>
        IProjection Projection
        {
            get;
        }

        /// <summary>
        /// Returns the PlaceAppearance for the given place
        /// </summary>
        /// <param name="place">The place to evaluate</param>
        PlaceAppearance AppearanceFor(Place place);

        /// <summary>
        /// Returns the SegmentAppearance for the given segment
        /// </summary>
        /// <param name="segment">The segment to evaluate</param>
        SegmentAppearance AppearanceFor(Segment segment);

        /// <summary>
        /// Returns the AreaAppearance for the given area
        /// </summary>
        /// <param name="area">The area to evaluate</param>
        AreaAppearance AppearanceFor(Area area);
    }
}