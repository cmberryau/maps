using Maps.Geographical.Features;
using Maps.OsmSharp.Collections;
using Maps.OsmSharp.Geographical.Extensions;
using OsmSharp.Osm;

namespace Maps.OsmSharp.Geographical.Features
{
    /// <summary>
    /// The default implementation of an OsmGeoTranslator
    /// </summary>
    public class DefaultOsmGeoTranslator : OsmGeoTranslator
    {
        /// <summary>
        /// Returns the feature for the given relation
        /// </summary>
        /// <param name="relation">The relation to evaluate</param>
        /// <param name="collection">The OsmGeo collection to read for dependencies</param>
        /// <param name="feature">The feature to write to</param>
        protected override bool FeatureFor(Relation relation, 
            ReadOnlyOsmGeoCollection collection, out Feature feature)
        {
            var result = false;
            feature = null;

            if (relation.IsMultipolygon() && relation.IsWaterway())
            {
                
            }

            return result;
        }

        /// <summary>
        /// Returns the feature for the given way
        /// </summary>
        /// <param name="way">The way to evaluate</param>
        /// <param name="collection">The OsmGeo collection to read for dependencies</param>
        /// <param name="feature">The feature to write to</param>
        protected override bool FeatureFor(Way way, ReadOnlyOsmGeoCollection collection, 
            out Feature feature)
        {
            var result = false;
            feature = null;

            if (way.IsSegment())
            {
                feature = new Segment(way.Guid(), way.Name(), way.Coordinates(
                    collection.Nodes), SegmentCategory.Unknown);
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Returns the feature for the given node
        /// </summary>
        /// <param name="node">The node to evaluate</param>
        /// <param name="feature">The feature to write to</param>
        protected override bool FeatureFor(Node node, out Feature feature)
        {
            var result = false;
            feature = null;

            return result;
        }
    }
}