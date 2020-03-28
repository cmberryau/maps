using Maps.Geographical.Features;
using System;
using Maps.Geographical.Places;

namespace Maps.Geographical.Filtering
{
    /// <summary>
    /// Responsible for filtering features with concrete type filters
    /// </summary>
    public class CompoundFilter : FeatureFilter<Feature>, IFeatureVisitor<bool>
    {
        private readonly FeatureFilter<Place> _placeFilter;
        private readonly FeatureFilter<Segment> _segmentFilter;
        private readonly FeatureFilter<Area> _areaFilter;

        /// <summary>
        /// Initializes a new instance of CompoundFilter
        /// </summary>
        /// <param name="placeFilter">The place filter</param>
        /// <param name="segmentFilter">The segment filter</param>
        /// <param name="areaFilter">The area filter</param>
        /// <exception cref="ArgumentNullException">Thrown if any argument is null
        /// </exception>
        public CompoundFilter(FeatureFilter<Place> placeFilter, 
            FeatureFilter<Segment> segmentFilter, FeatureFilter<Area> areaFilter)
        {
            if (placeFilter == null)
            {
                throw new ArgumentNullException(nameof(placeFilter));
            }

            if (segmentFilter == null)
            {
                throw new ArgumentNullException(nameof(segmentFilter));
            }

            if (areaFilter == null)
            {
                throw new ArgumentNullException(nameof(areaFilter));
            }

            _placeFilter = placeFilter;
            _segmentFilter = segmentFilter;
            _areaFilter = areaFilter;
        }

        /// <inheritdoc />
        public override bool Filter(Feature feature)
        {
            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature));
            }

            return feature.Accept(this);
        }

        /// <inheritdoc />
        public bool Visit(Place place)
        {
            if (place == null)
            {
                throw new ArgumentNullException(nameof(place));
            }

            return Filter(place);
        }

        /// <inheritdoc />
        public bool Visit(Segment segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            return Filter(segment);
        }

        /// <inheritdoc />
        public bool Visit(Area area)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            return Filter(area);
        }

        private bool Filter(Place place)
        {
            return _placeFilter.Filter(place);
        }

        private bool Filter(Segment segment)
        {
            return _segmentFilter.Filter(segment);
        }

        private bool Filter(Area area)
        {
            return _areaFilter.Filter(area);
        }
    }
}