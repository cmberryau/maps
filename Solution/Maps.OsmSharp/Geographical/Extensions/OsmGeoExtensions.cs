using System;
using Maps.Extensions;
using Maps.Geographical.Places;
using Maps.OsmSharp.Geographical.Places;
using OsmSharp.Osm;

namespace Maps.OsmSharp.Geographical.Extensions
{
    /// <summary>
    /// Provides extensions for OsmSharp's OsmGeo class
    /// </summary>
    internal static class OsmGeoExtensions
    {
        /// <summary>
        /// Returns the name of the given OsmGeo
        /// </summary>
        /// <param name="geo">The OsmGeo to evaluate</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        internal static Guid Guid(this OsmGeo geo)
        {
            if (geo == null)
            {
                throw new ArgumentNullException(nameof(geo));
            }

            if (!geo.Id.HasValue)
            {
                throw new ArgumentException("Has no Id value", nameof(geo));
            }

            return geo.Id.Value.ToGuid();
        }

        /// <summary>
        /// Returns the name of the given OsmGeo
        /// </summary>
        /// <param name="geo">The OsmGeo to evaluate</param>
        /// <exception cref="ArgumentNullException"></exception>
        internal static string Name(this OsmGeo geo)
        {
            if (geo == null)
            {
                throw new ArgumentNullException(nameof(geo));
            }

            var value = string.Empty;

            if (geo.Tags == null)
            {
                return value;
            }

            if (!geo.Tags.ContainsKey("name"))
            {
                return value;
            }

            if (!string.IsNullOrEmpty(geo.Tags["name"]))
            {
                value = geo.Tags["name"];
            }

            return value;
        }

        /// <summary>
        /// Returns the PlaceCategory for the given OsmGeo
        /// </summary>
        /// <param name="geo">The geo to evaluate</param>
        /// <param name="categoriesMap">The categories map to use</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        internal static PlaceCategory Category(this OsmGeo geo, 
            CategoriesMap categoriesMap)
        {
            if (geo == null)
            {
                throw new ArgumentNullException(nameof(geo));
            }

            if (geo.Tags == null || geo.Tags.Count <= 0)
            {
                throw new ArgumentException("Contains no tags", nameof(geo));
            }

            var rootCategory = categoriesMap.CategoryFor(geo.Tags);

            return new PlaceCategory(rootCategory);
        }
    }
}