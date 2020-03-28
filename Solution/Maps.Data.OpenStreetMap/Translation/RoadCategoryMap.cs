using Maps.Geographical.Features;

namespace Maps.Data.OpenStreetMap.Translation
{
    /// <summary>
    /// Provides mappings between OpenStreetMap tags and RootSegmentCategory
    /// </summary>
    internal sealed class SegmentCategoryMap : TagsMap<RootSegmentCategory>
    {
        /// <summary>
        /// Initializes a new instance of SegmentCategoriesMap
        /// </summary>
        public SegmentCategoryMap() : base(RootSegmentCategory.Invalid)
        {
            CreateUnknownTagsMap();
            CreateFreewayTagsMap();
            CreateMajorHighwayTagsMap();
            CreateMinorHighwayTagsMap();
            CreatePrimaryStreetTagsMap();
            CreateSecondaryStreetTagsMap();
            CreateResidentialTagsMap();
            CreateUnclassifiedTagsMap();
            CreatePedestrianSharedTagsMap();
            CreateServiceTagsMap();
            CreateFreewayLinkTagsMap();
            CreateMajorHighwayLinkTagsMap();
            CreateMinorHighwayLinkTagsMap();
            CreatePrimaryStreetLinkTagsMap();
            CreateSecondaryStreetLinkTagsMap();
        }

        private void CreateUnknownTagsMap()
        {
            var category = RootSegmentCategory.UnknownStreet;
            AddTagForCategory(category, "highway", "road");
        }

        private void CreateFreewayTagsMap()
        {
            var category = RootSegmentCategory.Freeway;
            AddTagForCategory(category, "highway", "motorway");
        }

        private void CreateMajorHighwayTagsMap()
        {
            var category = RootSegmentCategory.MajorHighway;
            AddTagForCategory(category, "highway", "trunk");
        }

        private void CreateMinorHighwayTagsMap()
        {
            var category = RootSegmentCategory.MinorHighway;
            AddTagForCategory(category, "highway", "primary");
        }

        private void CreatePrimaryStreetTagsMap()
        {
            var category = RootSegmentCategory.PrimaryStreet;
            AddTagForCategory(category, "highway", "secondary");
        }

        private void CreateSecondaryStreetTagsMap()
        {
            var category = RootSegmentCategory.SecondaryStreet;
            AddTagForCategory(category, "highway", "tertiary");
        }

        private void CreateResidentialTagsMap()
        {
            var category = RootSegmentCategory.ResidentialStreet;
            AddTagForCategory(category, "highway", "residential");
        }

        private void CreateUnclassifiedTagsMap()
        {
            var category = RootSegmentCategory.UnclassifiedStreet;
            AddTagForCategory(category, "highway", "unclassified");
        }

        private void CreatePedestrianSharedTagsMap()
        {
            var category = RootSegmentCategory.PedestrianSharedStreet;
            AddTagForCategory(category, "highway", "living_street");
        }

        private void CreateServiceTagsMap()
        {
            var category = RootSegmentCategory.ServiceStreet;
            AddTagForCategory(category, "highway", "service");
        }

        private void CreateFreewayLinkTagsMap()
        {
            var category = RootSegmentCategory.FreewayLink;
            AddTagForCategory(category, "highway", "motorway_link");
        }

        private void CreateMajorHighwayLinkTagsMap()
        {
            var category = RootSegmentCategory.MajorHighwayLink;
            AddTagForCategory(category, "highway", "trunk_link");
        }

        private void CreateMinorHighwayLinkTagsMap()
        {
            var category = RootSegmentCategory.MinorHighwayLink;
            AddTagForCategory(category, "highway", "primary_link");
        }

        private void CreatePrimaryStreetLinkTagsMap()
        {
            var category = RootSegmentCategory.PrimaryStreetLink;
            AddTagForCategory(category, "highway", "secondary_link");
        }

        private void CreateSecondaryStreetLinkTagsMap()
        {
            var category = RootSegmentCategory.SecondaryStreetLink;
            AddTagForCategory(category, "highway", "tertiary_link");
        }
    }
}