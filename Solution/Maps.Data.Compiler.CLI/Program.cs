using System.Collections.Generic;
using System.Reflection;
using Maps.Data.Compilation;
using Maps.Data.OpenStreetMap;
using Maps.Geographical.Features;
using Maps.Geographical.Filtering;
using Maps.Geographical.Places;
using Maps.Geographical.Simplification;
using Maps.Tests;
using log4net;
using Npgsql;

namespace Maps.Data.Compiler.CLI
{
    /// <summary>
    /// The entrypoint class for Maps.Data.Compiler.CLI
    /// </summary>
    public class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        private static readonly string PostgreOsmConnectionString =
            "Server=localhost;" +
            "Port=5432;" +
            "Database=osm;" +
            "User Id=osmuser;" +
            "Password=osmuserpassword;" +
            $"ApplicationName={Assembly.GetExecutingAssembly()}";

        /// <summary>
        /// The entrypoint method for Maps.Data.Compiler.CLI, compiles a map
        /// </summary>
        public static int Main(string[] args)
        {
            using (var pgconn = new NpgsqlConnection(PostgreOsmConnectionString))
            {
                using (var provider = new OpenStreetMapProvider(pgconn))
                {
                    using (var master = provider.FeatureProvider.FeatureSource())
                    {
                        using (var maps = new MapsTestProvider())
                        {
                            using (var features = maps.FeatureProvider)
                            {
                                using (var target = features.TiledFeatureTarget())
                                {
                                    var box = TestUtilities.BigIngolstadtBox;
                                    var simplificationFactor = 2d;

                                    // 16
                                    var placeFilter16 = new PlaceCategoryFilter(
                                        new List<PlaceCategory>
                                        {
                                            new PlaceCategory(RootPlaceCategory.City),
                                            new PlaceCategory(RootPlaceCategory.Village),
                                            new PlaceCategory(RootPlaceCategory.Town),
                                            new PlaceCategory(RootPlaceCategory.Suburb),
                                            new PlaceCategory(RootPlaceCategory.Neighbourhood),
                                            new PlaceCategory(RootPlaceCategory.Borough),
                                            new PlaceCategory(RootPlaceCategory.Hamlet),
                                            new PlaceCategory(RootPlaceCategory.Quarter),
                                            new PlaceCategory(RootPlaceCategory.Parking),
                                            new PlaceCategory(RootPlaceCategory.Petrol),
                                        });

                                    var segmentFilter16 = new SegmentCategoryFilter(
                                        new List<SegmentCategory>
                                        {
                                            new SegmentCategory(RootSegmentCategory.Freeway),
                                            new SegmentCategory(RootSegmentCategory.FreewayLink),
                                            new SegmentCategory(RootSegmentCategory.MajorHighway),
                                            new SegmentCategory(RootSegmentCategory.MajorHighwayLink),
                                            new SegmentCategory(RootSegmentCategory.MinorHighway),
                                            new SegmentCategory(RootSegmentCategory.MinorHighwayLink),
                                            new SegmentCategory(RootSegmentCategory.PrimaryStreet),
                                            new SegmentCategory(RootSegmentCategory.PrimaryStreetLink),
                                            new SegmentCategory(RootSegmentCategory.SecondaryStreet),
                                            new SegmentCategory(RootSegmentCategory.SecondaryStreetLink),
                                            new SegmentCategory(RootSegmentCategory.ResidentialStreet),
                                            new SegmentCategory(RootSegmentCategory.UnclassifiedStreet),
                                        });

                                    var areaFilter16 = new AreaGeodeticAreaFilter(500d) &
                                        new AreaCategoryFilter(new List<AreaCategory>
                                        {
                                            new AreaCategory(RootAreaCategory.Water),
                                            new AreaCategory(RootAreaCategory.ResidentialZone),
                                            new AreaCategory(RootAreaCategory.CommercialZone),
                                            new AreaCategory(RootAreaCategory.IndustrialZone),
                                            new AreaCategory(RootAreaCategory.RetailZone),
                                        }) | (new AreaGeodeticAreaFilter(150d) &
                                        new AreaCategoryFilter(new List<AreaCategory>
                                        {
                                            new AreaCategory(RootAreaCategory.Building),
                                        }));

                                    var filter16 = new CompoundFilter(placeFilter16, segmentFilter16, areaFilter16);
                                    var simplifier16 = new FeatureSimplifier(new RamerDouglasPeukerSimplifier(simplificationFactor));

                                    // 15
                                    var placeFilter15 = new PlaceCategoryFilter(
                                        new List<PlaceCategory>
                                        {
                                            new PlaceCategory(RootPlaceCategory.City),
                                            new PlaceCategory(RootPlaceCategory.Village),
                                            new PlaceCategory(RootPlaceCategory.Town),
                                            new PlaceCategory(RootPlaceCategory.Suburb),
                                            new PlaceCategory(RootPlaceCategory.Neighbourhood),
                                            new PlaceCategory(RootPlaceCategory.Borough),
                                            new PlaceCategory(RootPlaceCategory.Hamlet),
                                            new PlaceCategory(RootPlaceCategory.Quarter),
                                            new PlaceCategory(RootPlaceCategory.Parking),
                                            new PlaceCategory(RootPlaceCategory.Petrol),
                                        });
                                    var segmentFilter15 = segmentFilter16;
                                    var areaFilter15 = new AreaGeodeticAreaFilter(10000d) &
                                        new AreaCategoryFilter(new List<AreaCategory>
                                        {
                                            new AreaCategory(RootAreaCategory.Water),
                                            new AreaCategory(RootAreaCategory.ResidentialZone),
                                            new AreaCategory(RootAreaCategory.CommercialZone),
                                            new AreaCategory(RootAreaCategory.IndustrialZone),
                                            new AreaCategory(RootAreaCategory.RetailZone),
                                        }) | (new AreaGeodeticAreaFilter(250d) &
                                        new AreaCategoryFilter(new List<AreaCategory>
                                        {
                                            new AreaCategory(RootAreaCategory.Building),
                                        }));

                                    var filter15 = new CompoundFilter(placeFilter15, segmentFilter15, areaFilter15);
                                    var simplifier15 = new FeatureSimplifier(new RamerDouglasPeukerSimplifier(simplificationFactor * 2d));

                                    // 14
                                    var placeFilter14 = new PlaceCategoryFilter(
                                        new List<PlaceCategory>
                                        {
                                            new PlaceCategory(RootPlaceCategory.City),
                                            new PlaceCategory(RootPlaceCategory.Village),
                                            new PlaceCategory(RootPlaceCategory.Town),
                                            new PlaceCategory(RootPlaceCategory.Suburb),
                                            new PlaceCategory(RootPlaceCategory.Parking),
                                            new PlaceCategory(RootPlaceCategory.Petrol),
                                        });
                                    var segmentFilter14 = new SegmentCategoryFilter(
                                        new List<SegmentCategory>
                                        {
                                            new SegmentCategory(RootSegmentCategory.Freeway),
                                            new SegmentCategory(RootSegmentCategory.FreewayLink),
                                            new SegmentCategory(RootSegmentCategory.MajorHighway),
                                            new SegmentCategory(RootSegmentCategory.MajorHighwayLink),
                                            new SegmentCategory(RootSegmentCategory.MinorHighway),
                                            new SegmentCategory(RootSegmentCategory.MinorHighwayLink),
                                            new SegmentCategory(RootSegmentCategory.PrimaryStreet),
                                            new SegmentCategory(RootSegmentCategory.PrimaryStreetLink),
                                            new SegmentCategory(RootSegmentCategory.SecondaryStreet),
                                            new SegmentCategory(RootSegmentCategory.SecondaryStreetLink),
                                            new SegmentCategory(RootSegmentCategory.ResidentialStreet),
                                            new SegmentCategory(RootSegmentCategory.UnclassifiedStreet),
                                        });
                                    var areaFilter14 = new AreaGeodeticAreaFilter(25000d) &
                                        new AreaCategoryFilter(new List<AreaCategory>
                                        {
                                            new AreaCategory(RootAreaCategory.Water),
                                            new AreaCategory(RootAreaCategory.ResidentialZone),
                                            new AreaCategory(RootAreaCategory.CommercialZone),
                                            new AreaCategory(RootAreaCategory.IndustrialZone),
                                            new AreaCategory(RootAreaCategory.RetailZone),
                                        }) |
                                        (new AreaGeodeticAreaFilter(500d) &
                                        new AreaCategoryFilter(new List<AreaCategory>
                                        {
                                            new AreaCategory(RootAreaCategory.Building),
                                        }));

                                    var filter14 = new CompoundFilter(placeFilter14, segmentFilter14, areaFilter14);
                                    var simplifier14 = new FeatureSimplifier(new RamerDouglasPeukerSimplifier(simplificationFactor * 4d));

                                    // 13
                                    var placeFilter13 = new PlaceCategoryFilter(
                                        new List<PlaceCategory>
                                        {
                                            new PlaceCategory(RootPlaceCategory.City),
                                            new PlaceCategory(RootPlaceCategory.Village),
                                            new PlaceCategory(RootPlaceCategory.Town),
                                            new PlaceCategory(RootPlaceCategory.Petrol),
                                        });
                                    var segmentFilter13 = new SegmentCategoryFilter(
                                        new List<SegmentCategory>
                                        {
                                            new SegmentCategory(RootSegmentCategory.Freeway),
                                            new SegmentCategory(RootSegmentCategory.FreewayLink),
                                            new SegmentCategory(RootSegmentCategory.MajorHighway),
                                            new SegmentCategory(RootSegmentCategory.MajorHighwayLink),
                                            new SegmentCategory(RootSegmentCategory.MinorHighway),
                                            new SegmentCategory(RootSegmentCategory.MinorHighwayLink),
                                            new SegmentCategory(RootSegmentCategory.PrimaryStreet),
                                            new SegmentCategory(RootSegmentCategory.PrimaryStreetLink),
                                            new SegmentCategory(RootSegmentCategory.SecondaryStreet),
                                            new SegmentCategory(RootSegmentCategory.SecondaryStreetLink),
                                            new SegmentCategory(RootSegmentCategory.ResidentialStreet),
                                            new SegmentCategory(RootSegmentCategory.UnclassifiedStreet),
                                        });
                                    var areaFilter13 = new AreaGeodeticAreaFilter(50000d) &
                                        new AreaCategoryFilter(new List<AreaCategory>
                                        {
                                            new AreaCategory(RootAreaCategory.Water),
                                            new AreaCategory(RootAreaCategory.ResidentialZone),
                                            new AreaCategory(RootAreaCategory.CommercialZone),
                                            new AreaCategory(RootAreaCategory.IndustrialZone),
                                            new AreaCategory(RootAreaCategory.RetailZone),
                                        });
                                    var filter13 = new CompoundFilter(placeFilter13, segmentFilter13, areaFilter13);
                                    var simplifier13 = new FeatureSimplifier(new RamerDouglasPeukerSimplifier(simplificationFactor * 8d));

                                    // 12
                                    var placeFilter12 = new PlaceCategoryFilter(
                                        new List<PlaceCategory>
                                        {
                                            new PlaceCategory(RootPlaceCategory.City),
                                            new PlaceCategory(RootPlaceCategory.Village),
                                            new PlaceCategory(RootPlaceCategory.Town),
                                            new PlaceCategory(RootPlaceCategory.Petrol),
                                        });
                                    var segmentFilter12 = new SegmentCategoryFilter(
                                        new List<SegmentCategory>
                                        {
                                            new SegmentCategory(RootSegmentCategory.Freeway),
                                            new SegmentCategory(RootSegmentCategory.FreewayLink),
                                            new SegmentCategory(RootSegmentCategory.MajorHighway),
                                            new SegmentCategory(RootSegmentCategory.MajorHighwayLink),
                                            new SegmentCategory(RootSegmentCategory.MinorHighway),
                                            new SegmentCategory(RootSegmentCategory.MinorHighwayLink),
                                            new SegmentCategory(RootSegmentCategory.PrimaryStreet),
                                            new SegmentCategory(RootSegmentCategory.PrimaryStreetLink),
                                            new SegmentCategory(RootSegmentCategory.SecondaryStreet),
                                            new SegmentCategory(RootSegmentCategory.SecondaryStreetLink),
                                        });
                                    var areaFilter12 = new AreaGeodeticAreaFilter(100000d) &
                                        new AreaCategoryFilter(new List<AreaCategory>
                                        {
                                            new AreaCategory(RootAreaCategory.Water),
                                            new AreaCategory(RootAreaCategory.ResidentialZone),
                                            new AreaCategory(RootAreaCategory.CommercialZone),
                                            new AreaCategory(RootAreaCategory.IndustrialZone),
                                            new AreaCategory(RootAreaCategory.RetailZone),
                                        });
                                    var filter12 = new CompoundFilter(placeFilter12, segmentFilter12, areaFilter12);
                                    var simplifier12 = new FeatureSimplifier(new RamerDouglasPeukerSimplifier(simplificationFactor * 16d));

                                    // 11
                                    var placeFilter11 = new PlaceCategoryFilter(
                                        new List<PlaceCategory>
                                        {
                                            new PlaceCategory(RootPlaceCategory.City),
                                            new PlaceCategory(RootPlaceCategory.Town),
                                        });
                                    var segmentFilter11 = new SegmentCategoryFilter(
                                        new List<SegmentCategory>
                                        {
                                            new SegmentCategory(RootSegmentCategory.Freeway),
                                            new SegmentCategory(RootSegmentCategory.FreewayLink),
                                            new SegmentCategory(RootSegmentCategory.MajorHighway),
                                            new SegmentCategory(RootSegmentCategory.MajorHighwayLink),
                                            new SegmentCategory(RootSegmentCategory.MinorHighway),
                                            new SegmentCategory(RootSegmentCategory.MinorHighwayLink),
                                            new SegmentCategory(RootSegmentCategory.PrimaryStreet),
                                            new SegmentCategory(RootSegmentCategory.PrimaryStreetLink),
                                            new SegmentCategory(RootSegmentCategory.SecondaryStreet),
                                            new SegmentCategory(RootSegmentCategory.SecondaryStreetLink),
                                        });
                                    var areaFilter11 = new AreaGeodeticAreaFilter(100000d) &
                                        new AreaCategoryFilter(new List<AreaCategory>
                                        {
                                            new AreaCategory(RootAreaCategory.Water),
                                            new AreaCategory(RootAreaCategory.ResidentialZone),
                                            new AreaCategory(RootAreaCategory.CommercialZone),
                                            new AreaCategory(RootAreaCategory.IndustrialZone),
                                            new AreaCategory(RootAreaCategory.RetailZone),
                                        });
                                    var filter11 = new CompoundFilter(placeFilter11, segmentFilter11, areaFilter11);
                                    var simplifier11 = new FeatureSimplifier(new RamerDouglasPeukerSimplifier(simplificationFactor * 32d));

                                    // 10
                                    var placeFilter10 = new PlaceCategoryFilter(
                                        new List<PlaceCategory>
                                        {
                                            new PlaceCategory(RootPlaceCategory.City),
                                            new PlaceCategory(RootPlaceCategory.Town),
                                        });
                                        var segmentFilter10 = new SegmentCategoryFilter(
                                        new List<SegmentCategory>
                                        {
                                            new SegmentCategory(RootSegmentCategory.Freeway),
                                            new SegmentCategory(RootSegmentCategory.FreewayLink),
                                            new SegmentCategory(RootSegmentCategory.MajorHighway),
                                            new SegmentCategory(RootSegmentCategory.MajorHighwayLink),
                                            new SegmentCategory(RootSegmentCategory.MinorHighway),
                                            new SegmentCategory(RootSegmentCategory.PrimaryStreet),
                                            new SegmentCategory(RootSegmentCategory.PrimaryStreetLink),
                                        });

                                    var areaFilter10 = new AreaGeodeticAreaFilter(100000d) &
                                        new AreaCategoryFilter(new List<AreaCategory>
                                        {
                                            new AreaCategory(RootAreaCategory.Water),
                                        });
                                    var filter10 = new CompoundFilter(placeFilter10, segmentFilter10, areaFilter10);
                                    var simplifier10 = new FeatureSimplifier(new RamerDouglasPeukerSimplifier(simplificationFactor * 64d));

                                    // all levels
                                    var levels = new[]
                                    {
                                        new ZoomLevelTask(16, filter16, simplifier16),
                                        new ZoomLevelTask(15, filter15, simplifier15),
                                        new ZoomLevelTask(14, filter14, simplifier14),
                                        new ZoomLevelTask(13, filter13, simplifier13),
                                        new ZoomLevelTask(12, filter12, simplifier12),
                                        new ZoomLevelTask(11, filter11, simplifier11),
                                        new ZoomLevelTask(10, filter10, simplifier10),
                                    };

                                    using (var source = features.TiledFeatureSource())
                                    {
                                        var task = new TiledFeatureTask(master, target, source, box, levels);
                                        task.Start(4);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return 0;
        }
    }
}
