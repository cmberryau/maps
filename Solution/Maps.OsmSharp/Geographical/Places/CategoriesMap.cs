using System;
using System.Collections.Generic;
using Maps.Geographical.Places;
using OsmSharp.Collections.Tags;

namespace Maps.OsmSharp.Geographical.Places
{
    /// <summary>
    /// Provides mappings between OsmSharp tags and the RootPlaceCategory enum
    /// </summary>
    internal sealed class CategoriesMap
    {
        private readonly Dictionary<RootPlaceCategory, Dictionary<string, List<string>>> 
            _categoriesMap;
        private readonly Dictionary<string, Dictionary<string, 
            RootPlaceCategory>> _keysMap;

        /// <summary>
        /// Initializes a new instance of the CategoriesMap class
        /// </summary>
        public CategoriesMap()
        {
            _categoriesMap = new Dictionary<RootPlaceCategory, 
                Dictionary<string, List<string>>>();
            _keysMap = new Dictionary<string, Dictionary<string, RootPlaceCategory>>();

            // create maps for the enums
            CreateFoodAndDrinksTagsMap();
            CreateEntertainmentTagsMap();
            CreateNatureTagsMap();
            CreateShoppingTagsMap();
            CreateTransportTagsMap();
            CreateAccomodationTagsMap();
            CreateServicesTagsMap();
            CreateParkingTagsMap();
            CreatePetrolTagsMap();
            CreateEmergencyTagsMap();
        }

        /// <summary>
        /// Returns a RootPlaceCategory enum for the given tags Collection
        /// </summary>
        /// <param name="tags">The tags collection to evaluate</param>
        public RootPlaceCategory CategoryFor(TagsCollectionBase tags)
        {
            if (tags == null)
            {   
                throw new ArgumentNullException(nameof(tags));
            }

            foreach (var tag in tags)
            {
                if (_keysMap.ContainsKey(tag.Key))
                {
                    if (_keysMap[tag.Key].ContainsKey(tag.Value))
                    {
                        return _keysMap[tag.Key][tag.Value];
                    }
                }
            }

            return RootPlaceCategory.Invalid;
        }

        /// <summary>
        /// Returns a dictionary of tags for the given RootPlaceCategory enum
        /// </summary>
        /// <param name="rootCategory">The RootPlaceCategory enum to evaluate</param>
        public Dictionary<string, List<string>> TagsFor(RootPlaceCategory rootCategory)
        {
            return _categoriesMap[rootCategory];
        }

        private void AddTagForCategory(RootPlaceCategory rootCategory, string key, string value)
        {
            // add the key value pair to the categories map
            if (!_categoriesMap.ContainsKey(rootCategory))
            {
                _categoriesMap[rootCategory] = new Dictionary<string, List<string>>();
            }

            if (!_categoriesMap[rootCategory].ContainsKey(key))
            {
                _categoriesMap[rootCategory][key] = new List<string>();
            }

            _categoriesMap[rootCategory][key].Add(value);

            // add the key value pair to the keys map
            if (!_keysMap.ContainsKey(key))
            {
                _keysMap[key] = new Dictionary<string, RootPlaceCategory>();
            }

            _keysMap[key][value] = rootCategory;
        }

        private void CreateFoodAndDrinksTagsMap()
        {
            AddTagForCategory(RootPlaceCategory.FoodAndDrink, "amenity", "bar");
            AddTagForCategory(RootPlaceCategory.FoodAndDrink, "amenity", "pub");
            AddTagForCategory(RootPlaceCategory.FoodAndDrink, "amenity", "biergarten");
            AddTagForCategory(RootPlaceCategory.FoodAndDrink, "amenity", "cafe");
            AddTagForCategory(RootPlaceCategory.FoodAndDrink, "amenity", "fast_food");
            AddTagForCategory(RootPlaceCategory.FoodAndDrink, "amenity", "food_court");
            AddTagForCategory(RootPlaceCategory.FoodAndDrink, "amenity", "ice_cream");
            AddTagForCategory(RootPlaceCategory.FoodAndDrink, "amenity", "restaurant");
        }

        private void CreateEntertainmentTagsMap()
        {
            AddTagForCategory(RootPlaceCategory.Entertainment, "amenity", "arts_centre");
            AddTagForCategory(RootPlaceCategory.Entertainment, "amenity", "casino");
            AddTagForCategory(RootPlaceCategory.Entertainment, "amenity", "cinema");
            AddTagForCategory(RootPlaceCategory.Entertainment, "amenity", "community_centre");
            AddTagForCategory(RootPlaceCategory.Entertainment, "amenity", "fountain");
            AddTagForCategory(RootPlaceCategory.Entertainment, "amenity", "gambling");
            AddTagForCategory(RootPlaceCategory.Entertainment, "amenity", "nightclub");
            AddTagForCategory(RootPlaceCategory.Entertainment, "amenity", "planetarium");
            AddTagForCategory(RootPlaceCategory.Entertainment, "amenity", "social_centre");
            AddTagForCategory(RootPlaceCategory.Entertainment, "amenity", "studio");
            AddTagForCategory(RootPlaceCategory.Entertainment, "amenity", "theatre");
        }

        private void CreateNatureTagsMap()
        {
            AddTagForCategory(RootPlaceCategory.Nature, "tourism", "alpine_hut");
            AddTagForCategory(RootPlaceCategory.Nature, "tourism", "picnic_site");
            AddTagForCategory(RootPlaceCategory.Nature, "tourism", "wilderness_hut");

            AddTagForCategory(RootPlaceCategory.Nature, "natural", "peak");
            AddTagForCategory(RootPlaceCategory.Nature, "natural", "volcano");
            AddTagForCategory(RootPlaceCategory.Nature, "natural", "cliff");
            AddTagForCategory(RootPlaceCategory.Nature, "natural", "sinkhole");
        }

        private void CreateShoppingTagsMap()
        {
            AddTagForCategory(RootPlaceCategory.Shopping, "shop", "*");
        }

        private void CreateTransportTagsMap()
        {
            AddTagForCategory(RootPlaceCategory.Transport, "amenity", "bus_station");
            AddTagForCategory(RootPlaceCategory.Transport, "public_transport", "bus_station");
            AddTagForCategory(RootPlaceCategory.Transport, "railway", "station");
            AddTagForCategory(RootPlaceCategory.Transport, "aeroway", "aerodrome");
        }

        private void CreateAccomodationTagsMap()
        {
            AddTagForCategory(RootPlaceCategory.Accomodation, "building", "hotel");
            AddTagForCategory(RootPlaceCategory.Accomodation, "building", "motel");

            AddTagForCategory(RootPlaceCategory.Accomodation, "tourism", "hotel");
            AddTagForCategory(RootPlaceCategory.Accomodation, "tourism", "motel");
        }

        private void CreateServicesTagsMap()
        {
            AddTagForCategory(RootPlaceCategory.Services, "amenity", "atm");
            AddTagForCategory(RootPlaceCategory.Services, "amenity", "bank");
            AddTagForCategory(RootPlaceCategory.Services, "amenity", "pharmacy");
            AddTagForCategory(RootPlaceCategory.Services, "amenity", "bureau_de_change");
        }

        private void CreateParkingTagsMap()
        {
            AddTagForCategory(RootPlaceCategory.Parking, "amenity", "parking");
            AddTagForCategory(RootPlaceCategory.Parking, "amenity", "parking_space");
        }

        private void CreatePetrolTagsMap()
        {
            AddTagForCategory(RootPlaceCategory.Petrol, "amenity", "fuel");
        }

        private void CreateEmergencyTagsMap()
        {
            AddTagForCategory(RootPlaceCategory.Emergency, "amenity", "hospital");
            AddTagForCategory(RootPlaceCategory.Emergency, "amenity", "police");
        }
    }
}