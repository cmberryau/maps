using Maps.Geographical.Features;

namespace Maps.Data.OpenStreetMap.Translation
{
    /// <summary>
    /// Provides mappings between OpenStreetMap tags and RootAreaCategory
    /// </summary>
    internal sealed class AreaCategoryMap : TagsMap<RootAreaCategory>
    {   
        /// <summary>
        /// Initializes a new instance of AreaCategoriesMap
        /// </summary>
        public AreaCategoryMap() : base(RootAreaCategory.Invalid)
        {
            CreateParkTagsMap();
            CreateWaterTagsMap();
            CreateForestTagsMap();
            CreateFarmlandTagsMap();
            CreateGrasslandTagsMap();
            CreateResidentialZoneTagsMap();
            CreateCommercialZoneTagsMap();
            CreateRetailZoneTagsMap();
            CreateIndustrialZoneTagsMap();
            CreateMilitaryZoneTagsMap();
            CreateBuildingTagsMap();
        }

        private void CreateParkTagsMap()
        {
            var category = RootAreaCategory.Park;
            AddTagForCategory(category, "leisure", "common");
            AddTagForCategory(category, "leisure", "dog_park");
            AddTagForCategory(category, "leisure", "garden");
            AddTagForCategory(category, "leisure", "golf_course");
            AddTagForCategory(category, "leisure", "nature_reserve");
            AddTagForCategory(category, "leisure", "park");
            AddTagForCategory(category, "leisure", "pitch");
            AddTagForCategory(category, "leisure", "playground");
            AddTagForCategory(category, "leisure", "wildlife_hide");
        }

        private void CreateWaterTagsMap()
        {
            var category = RootAreaCategory.Water;
            AddTagForCategory(category, "natural", "water");
            AddTagForCategory(category, "natural", "wetland");
            AddTagForCategory(category, "natural", "glacier");
            AddTagForCategory(category, "natural", "bay");
            AddTagForCategory(category, "natural", "beach");
            AddTagForCategory(category, "natural", "hot_spring");
            AddTagForCategory(category, "landuse", "reservoir");

            // for multipolygons
            AddTagForCategory(category, "natural", "waterway");
            AddTagForCategory(category, "waterway", "riverbank");
        }

        private void CreateForestTagsMap()
        {
            var category = RootAreaCategory.Forest;
            AddTagForCategory(category, "natural", "wood");
            AddTagForCategory(category, "landuse", "forest");
        }

        private void CreateFarmlandTagsMap()
        {
            var category = RootAreaCategory.Farmland;
            AddTagForCategory(category, "landuse", "farmland");
            AddTagForCategory(category, "landuse", "orchard");
        }

        private void CreateGrasslandTagsMap()
        {
            var category = RootAreaCategory.Grassland;
            AddTagForCategory(category, "landuse", "meadow");
            AddTagForCategory(category, "landuse", "grass");
            AddTagForCategory(category, "natural", "heath");
            AddTagForCategory(category, "natural", "grassland");
            AddTagForCategory(category, "natural", "scrub");
        }

        private void CreateResidentialZoneTagsMap()
        {
            var category = RootAreaCategory.ResidentialZone;
            AddTagForCategory(category, "landuse", "residential");
        }

        private void CreateCommercialZoneTagsMap()
        {
            var category = RootAreaCategory.CommercialZone;
            AddTagForCategory(category, "landuse", "commercial");
        }

        private void CreateRetailZoneTagsMap()
        {
            var category = RootAreaCategory.RetailZone;
            AddTagForCategory(category, "landuse", "retail");
        }

        private void CreateIndustrialZoneTagsMap()
        {
            var category = RootAreaCategory.IndustrialZone;
            AddTagForCategory(category, "landuse", "industrial");
            AddTagForCategory(category, "landuse", "landfill");
            AddTagForCategory(category, "landuse", "port");
            AddTagForCategory(category, "landuse", "quarry");
        }

        private void CreateMilitaryZoneTagsMap()
        {
            var category = RootAreaCategory.MilitaryZone;
            AddTagForCategory(category, "landuse", "military");
        }

        private void CreateBuildingTagsMap()
        {
            var category = RootAreaCategory.Building;
            AddTagWildcardForCategory(category, "building");
        }
    }
}