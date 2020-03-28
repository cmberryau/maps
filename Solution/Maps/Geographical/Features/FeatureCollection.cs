using System;
using System.Collections.Generic;
using Maps.Extensions;
using Maps.Geographical.Places;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// Responsible for holding features at the first concrete level
    /// </summary>
    public sealed class FeatureCollection : IFeatureCollection, IFeatureVisitor
    {
        /// <inheritdoc />
        public int PlaceCount => _places.Count;

        /// <inheritdoc />
        public int SegmentCount => _segments.Count;

        /// <inheritdoc />
        public int AreaCount => _areas.Count;

        /// <inheritdoc />
        public IList<Place> Places => _places;

        /// <inheritdoc />
        public IList<Segment> Segments => _segments;

        /// <inheritdoc />
        public IList<Area> Areas => _areas;

        /// <inheritdoc />
        public IList<Guid> UniquePlaceGuids => _uniquePlaceGuids;

        /// <inheritdoc />
        public IList<Guid> UniqueSegmentGuids => _uniqueSegmentGuids;

        /// <inheritdoc />
        public IList<Guid> UniqueAreaGuids => _uniqueAreaGuids;

        /// <inheritdoc />
        public IList<PlaceCategory> UniquePlaceCategories => _uniquePlaceCategories;

        /// <inheritdoc />
        public IList<SegmentCategory> UniqueSegmentCategories => _uniqueSegmentCategories;

        /// <inheritdoc />
        public IList<AreaCategory> UniqueAreaCategories => _uniqueAreaCategories;

        private readonly IList<Place> _places;
        private readonly IList<Segment> _segments;
        private readonly IList<Area> _areas;
        private readonly IList<Guid> _uniquePlaceGuids;
        private readonly IList<Guid> _uniqueSegmentGuids;
        private readonly IList<Guid> _uniqueAreaGuids;
        private readonly IDictionary<Guid, IList<Place>> _placeGuidMap;
        private readonly IDictionary<Guid, IList<Segment>> _segmentGuidMap;
        private readonly IDictionary<Guid, IList<Area>> _areaGuidMap;
        private readonly IList<PlaceCategory> _uniquePlaceCategories;
        private readonly IList<SegmentCategory> _uniqueSegmentCategories;
        private readonly IList<AreaCategory> _uniqueAreaCategories;
        private readonly IDictionary<PlaceCategory, IList<Place>> _placeCategoryMap;
        private readonly IDictionary<SegmentCategory, IList<Segment>> _segmentCategoryMap;
        private readonly IDictionary<AreaCategory, IList<Area>> _areaCategoryMap;

        /// <summary>
        /// Initializes a new instance of FeatureCollection
        /// </summary>
        /// <param name="features">The initial features to add</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="features"/>
        /// is null </exception>
        /// <exception cref="ArgumentException">Thrown if any element of 
        /// <paramref name="features"/> is null</exception>
        public FeatureCollection(IList<Feature> features)
        {
            if (features == null)
            {
                throw new ArgumentNullException(nameof(features));
            }

            features.AssertNoNullEntries();

            _uniquePlaceGuids = new List<Guid>();
            _uniqueSegmentGuids = new List<Guid>();
            _uniqueAreaGuids = new List<Guid>();

            _places = new List<Place>();
            _segments = new List<Segment>();
            _areas = new List<Area>();

            _placeGuidMap = new Dictionary<Guid, IList<Place>>();
            _segmentGuidMap = new Dictionary<Guid, IList<Segment>>();
            _areaGuidMap = new Dictionary<Guid, IList<Area>>();

            _uniquePlaceCategories = new List<PlaceCategory>();
            _uniqueSegmentCategories = new List<SegmentCategory>();
            _uniqueAreaCategories = new List<AreaCategory>();

            _placeCategoryMap = new Dictionary<PlaceCategory, IList<Place>>();
            _segmentCategoryMap = new Dictionary<SegmentCategory, IList<Segment>>();
            _areaCategoryMap = new Dictionary<AreaCategory, IList<Area>>(); 

            var featuresCount = features.Count;
            for (var i = 0; i < featuresCount; ++i)
            {
                Add(features[i]);
            }
        }

        /// <summary>
        /// Adds a feature to the collection
        /// </summary>
        /// <param name="feature">The feature to add</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="feature"/> 
        /// is null</exception>
        public void Add(Feature feature)
        {
            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature));
            }

            feature.Accept(this);
        }

        /// <inheritdoc />
        public bool TryGetPlaces(Guid guid, out IList<Place> places)
        {
            return _placeGuidMap.TryGetValue(guid, out places);
        }

        /// <inheritdoc />
        public bool TryGetSegments(Guid guid, out IList<Segment> segments)
        {
            return _segmentGuidMap.TryGetValue(guid, out segments);
        }

        /// <inheritdoc />
        public bool TryGetAreas(Guid guid, out IList<Area> areas)
        {
            return _areaGuidMap.TryGetValue(guid, out areas);
        }

        /// <inheritdoc />
        public bool TryGetPlaces(PlaceCategory category, out IList<Place> places)
        {
            return _placeCategoryMap.TryGetValue(category, out places);
        }

        /// <inheritdoc />
        public bool TryGetSegments(SegmentCategory category, out IList<Segment> segments)
        {
            return _segmentCategoryMap.TryGetValue(category, out segments);
        }

        /// <inheritdoc />
        public bool TryGetAreas(AreaCategory category, out IList<Area> areas)
        {
            return _areaCategoryMap.TryGetValue(category, out areas);
        }

        /// <inheritdoc />
        public void Visit(Place place)
        {
            if (place == null)
            {
                throw new ArgumentNullException(nameof(place));
            }

            _places.Add(place);

            var guid = place.Guid;
            if (!_placeGuidMap.ContainsKey(guid))
            {
                _placeGuidMap.Add(guid, new List<Place>());
                _uniquePlaceGuids.Add(guid);
            }

            var category = place.Category;
            if (!_placeCategoryMap.ContainsKey(category))
            {
                _placeCategoryMap.Add(category, new List<Place>());
                _uniquePlaceCategories.Add(category);
            }

            _placeGuidMap[guid].Add(place);
            _placeCategoryMap[category].Add(place);
        }

        /// <inheritdoc />
        public void Visit(Segment segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            _segments.Add(segment);

            var guid = segment.Guid;
            if (!_segmentGuidMap.ContainsKey(guid))
            {
                _segmentGuidMap.Add(guid, new List<Segment>());
                _uniqueSegmentGuids.Add(guid);
            }

            var category = segment.Category;
            if (!_segmentCategoryMap.ContainsKey(category))
            {
                _segmentCategoryMap.Add(category, new List<Segment>());
                _uniqueSegmentCategories.Add(category);
            }

            _segmentGuidMap[guid].Add(segment);
            _segmentCategoryMap[category].Add(segment);
        }

        /// <inheritdoc />
        public void Visit(Area area)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            _areas.Add(area);

            var guid = area.Guid;
            if (!_areaGuidMap.ContainsKey(guid))
            {
                _areaGuidMap.Add(guid, new List<Area>());
                _uniqueAreaGuids.Add(guid);
            }

            var category = area.Category;
            if (!_areaCategoryMap.ContainsKey(category))
            {
                _areaCategoryMap.Add(category, new List<Area>());
                _uniqueAreaCategories.Add(category);
            }

            _areaGuidMap[guid].Add(area);
            _areaCategoryMap[category].Add(area);
        }
    }
}