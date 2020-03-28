using System;
using System.Collections.Generic;
using Maps.Geographical.Places;

namespace Maps.Geographical.Features
{
    /// <summary>
    /// An interface for first concrete level feature collections
    /// </summary>
    public interface IFeatureCollection
    {
        /// <summary>
        /// The number of places held by the collection
        /// </summary>
        int PlaceCount
        {
            get;
        }

        /// <summary>
        /// The number of segments held by the collection
        /// </summary>
        int SegmentCount
        {
            get;
        }

        /// <summary>
        /// The number of areas held by the collection
        /// </summary>
        int AreaCount
        {
            get;
        }

        /// <summary>
        /// The list of places
        /// </summary>
        IList<Place> Places
        {
            get;
        }

        /// <summary>
        /// The list of segments
        /// </summary>
        IList<Segment> Segments
        {
            get;
        }

        /// <summary>
        /// The list of areas
        /// </summary>
        IList<Area> Areas
        {
            get;
        }

        /// <summary>
        /// The list of unique place guids
        /// </summary>
        IList<Guid> UniquePlaceGuids
        {
            get;
        }

        /// <summary>
        /// The list of unique segment guids
        /// </summary>
        IList<Guid> UniqueSegmentGuids
        {
            get;
        }

        /// <summary>
        /// The list of unique area guids
        /// </summary>
        IList<Guid> UniqueAreaGuids
        {
            get;
        }

        /// <summary>
        /// The list of unique place categories
        /// </summary>
        IList<PlaceCategory> UniquePlaceCategories
        {
            get;
        }

        /// <summary>
        /// The list of unique segment categories
        /// </summary>
        IList<SegmentCategory> UniqueSegmentCategories
        {
            get;
        }

        /// <summary>
        /// The list of unique area categories
        /// </summary>
        IList<AreaCategory> UniqueAreaCategories
        {
            get;
        }

        /// <summary>
        /// Tries to get places given a guid
        /// </summary>
        /// <param name="guid">The guid to look for</param>
        /// <param name="places">The output places list</param>
        /// <returns>True if the collection has the places, false otherwise</returns>
        bool TryGetPlaces(Guid guid, out IList<Place> places);

        /// <summary>
        /// Tries to get segments given a guid
        /// </summary>
        /// <param name="guid">The guid to look for</param>
        /// <param name="segments">The output segments list</param>
        /// <returns>True if the collection has the segments, false otherwise</returns>
        bool TryGetSegments(Guid guid, out IList<Segment> segments);

        /// <summary>
        /// Tries to get areas given a guid
        /// </summary>
        /// <param name="guid">The guid to look for</param>
        /// <param name="areas">The output areas list</param>
        /// <returns>True if the collection has the areas, false otherwise</returns>
        bool TryGetAreas(Guid guid, out IList<Area> areas);

        /// <summary>
        /// Tries to get places given a category
        /// </summary>
        /// <param name="category">The category to look for</param>
        /// <param name="places">The output places list</param>
        /// <returns>True if the collection has the places, false otherwise</returns>
        bool TryGetPlaces(PlaceCategory category, out IList<Place> places);

        /// <summary>
        /// Tries to get segments given a category
        /// </summary>
        /// <param name="category">The category to look for</param>
        /// <param name="segments">The output segments list</param>
        /// <returns>True if the collection has the segments, false otherwise</returns>
        bool TryGetSegments(SegmentCategory category, out IList<Segment> segments);

        /// <summary>
        /// Tries to get areas given a category
        /// </summary>
        /// <param name="category">The category to look for</param>
        /// <param name="areas">The output areas list</param>
        /// <returns>True if the collection has the areas, false otherwise</returns>
        bool TryGetAreas(AreaCategory category, out IList<Area> areas);
    }
}