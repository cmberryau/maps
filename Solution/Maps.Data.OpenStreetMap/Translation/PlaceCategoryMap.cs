using Maps.Geographical.Places;

namespace Maps.Data.OpenStreetMap.Translation
{
    /// <summary>
    /// Provides mappings between OpenStreetMap tags and RootPlaceCategory
    /// </summary>
    internal sealed class PlaceCategoryMap : TagsMap<RootPlaceCategory>
    {
        /// <summary>
        /// Initializes a new instance of PlacesCategoriesMap
        /// </summary>
        public PlaceCategoryMap() : base(RootPlaceCategory.Invalid)
        {
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
            CreateCityTagsMap();
            CreateBoroughTagsMap();
            CreateSuburbTagsMap();
            CreateQuarterTagsMap();
            CreateNeighbourhoodTagsMap();
            CreateTownTagsMap();
            CreateVillageTagsMap();
            CreateHamletTagsMap();
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

        private void CreateCityTagsMap()
        {
            AddTagForCategory(RootPlaceCategory.City, "place", "city");
        }

        private void CreateBoroughTagsMap()
        {
            AddTagForCategory(RootPlaceCategory.Borough, "place", "borough");
        }

        private void CreateSuburbTagsMap()
        {
            AddTagForCategory(RootPlaceCategory.Suburb, "place", "suburb");
        }

        private void CreateQuarterTagsMap()
        {
            AddTagForCategory(RootPlaceCategory.Quarter, "place", "quarter");
        }

        private void CreateNeighbourhoodTagsMap()
        {
            AddTagForCategory(RootPlaceCategory.Neighbourhood, "place", "neighbourhood");
        }

        private void CreateTownTagsMap()
        {
            AddTagForCategory(RootPlaceCategory.Town, "place", "town");
        }

        private void CreateVillageTagsMap()
        {
            AddTagForCategory(RootPlaceCategory.Village, "place", "village");
        }

        private void CreateHamletTagsMap()
        {
            AddTagForCategory(RootPlaceCategory.Hamlet, "place", "hamlet");
        }
    }
}
