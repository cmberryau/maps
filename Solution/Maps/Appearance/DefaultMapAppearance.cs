using System;
using System.Collections.Generic;
using Maps.Appearance.Properties;
using Maps.Geographical.Features;
using Maps.Geographical.Filtering;
using Maps.Geographical.Places;
using Maps.Geographical.Projection;

namespace Maps.Appearance
{
    /// <summary>
    /// The default map appearance model
    /// </summary>
    public static class DefaultMapAppearance
    {
        private static readonly IProjection Projection = new WebMercatorProjection(10d);
        private const float SegmentColorFactor = 0.65f;
        private const float SegmentOutlineColorFactor = 0.85f;
        private const double SegmentOutlineSizeFactor = 1.5d;
        private const string DefaultNightName = "DefaultNight";
        private const string DefaultDayName = "DefaultDay";

        /// <summary>
        /// Creates a new default map appearance for a given lod
        /// </summary>
        /// <param name="lod">The lod this appearance applies to</param>
        /// <param name="day">Should we apply the day appearance set?</param>
        public static IMapAppearance Create(int lod, bool day)
        {
            var projection = Projection;

            var placeTargets = PlaceTargets(projection, lod, day);
            var segmentTargets = SegmentTargets(projection, lod, day);
            var areaTargets = AreaTargets(projection, lod, day);

            return new MapAppearance(projection, placeTargets, segmentTargets,
                areaTargets, day ? DefaultDayName : DefaultNightName);
        }

        private static IList<PlaceAppearanceTarget> PlaceTargets(IProjection projection, 
            int lod, bool day)
        {
            return new List<PlaceAppearanceTarget>
            {
                CityTarget(projection, lod, day),
                TownTarget(projection, lod, day),
                SuburbTarget(projection, lod, day),
                PetrolTarget(projection, lod, day),
                ParkingTarget(projection, lod, day),
            };
        }

        private static IList<SegmentAppearanceTarget> SegmentTargets(
            IProjection projection, int lod, bool day)
        {
            return new List<SegmentAppearanceTarget>
            {
                FreewayTarget(projection, lod, day),
                FreewayLinkTarget(projection, lod, day),
                HighwayTarget(projection, lod, day),
                HighwayLinkTarget(projection, lod, day),
                MinorHighwayTarget(projection, lod, day),
                MinorHighwayLinkTarget(projection, lod, day),
                PrimaryStreetTarget(projection, lod, day),
                PrimaryStreetLinkTarget(projection, lod, day),
                SecondaryStreetTarget(projection, lod, day),
                SecondaryStreetLinkTarget(projection, lod, day),
                ResidentialStreetTarget(projection, lod, day),
                HighwayTarget(projection, lod, day),
                OtherStreetTarget(projection, lod, day),
                UnknownStreetTarget(projection, lod, day),
            };
        }

        private static IList<AreaAppearanceTarget> AreaTargets(IProjection projection, 
            int lod, bool day)
        {
            return new List<AreaAppearanceTarget>
            {
                WaterTarget(projection, lod, day),
                ParksForestFarmAndGrassTarget(projection, lod, day),
                ResidentialTarget(projection, lod, day),
                RetailTarget(projection, lod, day),
                IndustrialTarget(projection, lod, day),
                BuildingTarget(projection, lod, day),
                CommercialTarget(projection, lod, day),
                MilitaryTarget(projection, lod, day),
                UnknownTarget(projection, lod, day),
                TileTarget(projection, lod, day),
            };
        }

        private static PlaceAppearanceTarget CityTarget(IProjection projection, int lod,
            bool day)
        {
            var filter = new PlaceCategoryFilter(new List<PlaceCategory>
            {
                new PlaceCategory(RootPlaceCategory.City),
            });

            var color = day ? Colorf.White : Colorf.Grey;
            var outlineColor = day ? Colorf.Black : Colorf.Black;

            var properties = new List<Property>
            {
                new Int32Property("label_z", 3),
                new SingleProperty("label_padding", 1.5f),
                new BoolProperty("label", true),
                new ColorProperty("label_font_color", color),
                new BoolProperty("label_font_bold", true),
                new SingleProperty("label_font_size", 30f),
                new BoolProperty("label_font_outline", true),
                new ColorProperty("label_font_outline_color", outlineColor),
            };

            var appearance = new PlaceAppearance(properties, projection);
            var target = new PlaceAppearanceTarget(appearance, filter);

            return target;
        }

        private static PlaceAppearanceTarget TownTarget(IProjection projection, int lod,
            bool day)
        {
            var filter = new PlaceCategoryFilter(new List<PlaceCategory>
            {
                new PlaceCategory(RootPlaceCategory.Town),
                new PlaceCategory(RootPlaceCategory.Hamlet),
                new PlaceCategory(RootPlaceCategory.Village),
            });

            var color = day ? Colorf.White : Colorf.Grey;
            var outlineColor = day ? Colorf.Black : Colorf.Black;

            var properties = new List<Property>
            {
                new Int32Property("label_z", 2),
                new SingleProperty("label_padding", 1.5f),
                new BoolProperty("label", true),
                new ColorProperty("label_font_color", color),
                new BoolProperty("label_font_bold", true),
                new SingleProperty("label_font_size", 20f),
                new BoolProperty("label_font_outline", true),
                new ColorProperty("label_font_outline_color", outlineColor),
            };

            var appearance = new PlaceAppearance(properties, projection);
            var target = new PlaceAppearanceTarget(appearance, filter);

            return target;
        }

        private static PlaceAppearanceTarget SuburbTarget(IProjection projection, 
            int lod, bool day)
        {
            var filter = new PlaceCategoryFilter(new List<PlaceCategory>
            {
                new PlaceCategory(RootPlaceCategory.Suburb),
                new PlaceCategory(RootPlaceCategory.Neighbourhood),
                new PlaceCategory(RootPlaceCategory.Borough),
            });

            var color = day ? Colorf.White : Colorf.Grey;
            var outlineColor = day ? Colorf.Black : Colorf.Black;

            var properties = new List<Property>
            {
                new Int32Property("label_z", 1),
                new SingleProperty("label_padding", 1.5f),
                new BoolProperty("label", true),
                new ColorProperty("label_font_color", color),
                new BoolProperty("label_font_bold", true),
                new SingleProperty("label_font_size", 20f),
                new BoolProperty("label_font_outline", true),
                new ColorProperty("label_font_outline_color", outlineColor),
            };

            var appearance = new PlaceAppearance(properties, projection);
            var target = new PlaceAppearanceTarget(appearance, filter);

            return target;
        }

        private static PlaceAppearanceTarget PetrolTarget(IProjection projection, 
            int lod, bool day)
        {
            var filter = new PlaceCategoryFilter(new List<PlaceCategory>
            {
                new PlaceCategory(RootPlaceCategory.Petrol),
            });

            var color = day ? Colorf.White : Colorf.White * 0.75f;

            var properties = new List<Property>
            {
                new Int32Property("icon_z", 1),
                new BoolProperty("icon", true),
                new ColorProperty("icon_background_color", color),
            };

            var appearance = new PlaceAppearance(properties, projection);
            var target = new PlaceAppearanceTarget(appearance, filter);

            return target;
        }

        private static PlaceAppearanceTarget ParkingTarget(IProjection projection, 
            int lod, bool day)
        {
            var filter = new PlaceCategoryFilter(new List<PlaceCategory>
            {
                new PlaceCategory(RootPlaceCategory.Parking),
            });

            var color = day ? Colorf.White : Colorf.White * 0.75f;

            var properties = new List<Property>
            {
                new Int32Property("icon_z", 1),
                new BoolProperty("icon", true),
                new ColorProperty("icon_background_color", color),
            };

            var appearance = new PlaceAppearance(properties, projection);
            var target = new PlaceAppearanceTarget(appearance, filter);

            return target;
        }

        private static SegmentAppearanceTarget FreewayTarget(IProjection projection, 
            int lod, bool day)
        {
            var width = BaseSegmentWidth(lod) * 2d;

            var filter = new SegmentCategoryFilter(new List<SegmentCategory>
            {
                new SegmentCategory(RootSegmentCategory.Freeway),
            });

            var color = day ? Colorf.Red : Colorf.Red * 0.5f;

            var properties = new List<Property>
            {
                new Int32Property("z", 13),
                new ColorProperty("main", color * SegmentColorFactor),
                new DoubleProperty("width", width),
                new BoolProperty("outline", true),
                new ColorProperty("outline_color", color * SegmentOutlineColorFactor),
                new DoubleProperty("outline_width", width * SegmentOutlineSizeFactor),
                new BoolProperty("label", true),
                new SingleProperty("label_padding", 1.5f),
            };

            var appearance = new SegmentAppearance(properties, projection);
            var target = new SegmentAppearanceTarget(appearance, filter);

            return target;
        }

        private static SegmentAppearanceTarget FreewayLinkTarget(IProjection projection, 
            int lod, bool day)
        {
            var width = BaseSegmentWidth(lod);

            var filter = new SegmentCategoryFilter(new List<SegmentCategory>
            {
                new SegmentCategory(RootSegmentCategory.FreewayLink),
            });

            var color = day ? Colorf.Red : Colorf.Red * 0.5f;

            var properties = new List<Property>
            {
                new Int32Property("z", 13),
                new ColorProperty("main", color * SegmentColorFactor),
                new DoubleProperty("width", width),
                new BoolProperty("outline", true),
                new ColorProperty("outline_color", color * SegmentOutlineColorFactor),
                new DoubleProperty("outline_width", width * SegmentOutlineSizeFactor),
            };

            var appearance = new SegmentAppearance(properties, projection);
            var target = new SegmentAppearanceTarget(appearance, filter);

            return target;
        }

        private static SegmentAppearanceTarget HighwayTarget(IProjection projection, 
            int lod, bool day)
        {
            var width = BaseSegmentWidth(lod) * 1.75d;

            var filter = new SegmentCategoryFilter(new List<SegmentCategory>
            {
                new SegmentCategory(RootSegmentCategory.MajorHighway),
            });

            var color = day ? Colorf.Green : Colorf.Green * 0.5f;

            var properties = new List<Property>
            {
                new Int32Property("z", 12),
                new ColorProperty("main", color * SegmentColorFactor),
                new DoubleProperty("width", width),
                new BoolProperty("outline", true),
                new ColorProperty("outline_color", color * SegmentOutlineColorFactor),
                new DoubleProperty("outline_width", width * SegmentOutlineSizeFactor),
                new BoolProperty("label", true),
                new SingleProperty("label_padding", 1.5f),
            };

            var appearance = new SegmentAppearance(properties, projection);
            var target = new SegmentAppearanceTarget(appearance, filter);

            return target;
        }

        private static SegmentAppearanceTarget HighwayLinkTarget(IProjection projection,
            int lod, bool day)
        {
            var width = BaseSegmentWidth(lod);

            var filter = new SegmentCategoryFilter(new List<SegmentCategory>
            {
                new SegmentCategory(RootSegmentCategory.MajorHighwayLink),
            });

            var color = day ? Colorf.Green : Colorf.Green * 0.5f;

            var properties = new List<Property>
            {
                new Int32Property("z", 12),
                new ColorProperty("main", color * SegmentColorFactor),
                new DoubleProperty("width", width),
                new BoolProperty("outline", true),
                new ColorProperty("outline_color", color * SegmentOutlineColorFactor),
                new DoubleProperty("outline_width", width * SegmentOutlineSizeFactor),
            };

            var appearance = new SegmentAppearance(properties, projection);
            var target = new SegmentAppearanceTarget(appearance, filter);

            return target;
        }

        private static SegmentAppearanceTarget MinorHighwayTarget(IProjection projection,
            int lod, bool day)
        {
            var width = BaseSegmentWidth(lod) * 1.75d;

            var filter = new SegmentCategoryFilter(new List<SegmentCategory>
            {
                new SegmentCategory(RootSegmentCategory.MinorHighway),
            });

            var color = day ? Colorf.Green : Colorf.Green * 0.5f;
            var properties = new List<Property>
            {
                new Int32Property("z", 12),
                new ColorProperty("main", color * SegmentColorFactor),
                new DoubleProperty("width", width),
                new BoolProperty("outline", true),
                new ColorProperty("outline_color", color * SegmentOutlineColorFactor),
                new DoubleProperty("outline_width", width * SegmentOutlineSizeFactor),
                new BoolProperty("label", true),
                new SingleProperty("label_padding", 1.5f),
            };

            var appearance = new SegmentAppearance(properties, projection);
            var target = new SegmentAppearanceTarget(appearance, filter);

            return target;
        }

        private static SegmentAppearanceTarget MinorHighwayLinkTarget(
            IProjection projection, int lod, bool day)
        {
            var width = BaseSegmentWidth(lod);

            var filter = new SegmentCategoryFilter(new List<SegmentCategory>
            {
                new SegmentCategory(RootSegmentCategory.MinorHighwayLink),
            });

            var color = day ? Colorf.Green : Colorf.Green * 0.5f;
            var properties = new List<Property>
            {
                new Int32Property("z", 12),
                new ColorProperty("main", color * SegmentColorFactor),
                new DoubleProperty("width", width),
                new BoolProperty("outline", true),
                new ColorProperty("outline_color", color * SegmentOutlineColorFactor),
                new DoubleProperty("outline_width", width * SegmentOutlineSizeFactor),
            };

            var appearance = new SegmentAppearance(properties, projection);
            var target = new SegmentAppearanceTarget(appearance, filter);

            return target;
        }

        private static SegmentAppearanceTarget PrimaryStreetTarget(
            IProjection projection, int lod, bool day)
        {
            var width = BaseSegmentWidth(lod) * 1.5d;
            var filter = new SegmentCategoryFilter(new List<SegmentCategory>
            {
                new SegmentCategory(RootSegmentCategory.PrimaryStreet),
            });

            var label = lod > 11;
            var color = day ? Colorf.Yellow : Colorf.Yellow * 0.5f;
            var properties = new List<Property>
            {
                new Int32Property("z", 11),
                new ColorProperty("main", color * SegmentColorFactor),
                new DoubleProperty("width", width),
                new BoolProperty("outline", true),
                new ColorProperty("outline_color", color * SegmentOutlineColorFactor),
                new DoubleProperty("outline_width", width * SegmentOutlineSizeFactor),
                new BoolProperty("label", label),
                new SingleProperty("label_padding", 1.5f),
            };

            var appearance = new SegmentAppearance(properties, projection);
            var target = new SegmentAppearanceTarget(appearance, filter);

            return target;
        }

        private static SegmentAppearanceTarget PrimaryStreetLinkTarget(
            IProjection projection, int lod, bool day)
        {
            var width = BaseSegmentWidth(lod);
            var filter = new SegmentCategoryFilter(new List<SegmentCategory>
            {
                new SegmentCategory(RootSegmentCategory.PrimaryStreetLink),
            });

            var color = day ? Colorf.Yellow : Colorf.Yellow * 0.5f;
            var properties = new List<Property>
            {
                new Int32Property("z", 11),
                new ColorProperty("main", color * SegmentColorFactor),
                new DoubleProperty("width", width),
                new BoolProperty("outline", true),
                new ColorProperty("outline_color", color * SegmentOutlineColorFactor),
                new DoubleProperty("outline_width", width * SegmentOutlineSizeFactor),
            };

            var appearance = new SegmentAppearance(properties, projection);
            var target = new SegmentAppearanceTarget(appearance, filter);

            return target;
        }

        private static SegmentAppearanceTarget SecondaryStreetTarget(
            IProjection projection, int lod, bool day)
        {
            var width = BaseSegmentWidth(lod) * 1.25d;
            var filter = new SegmentCategoryFilter(new List<SegmentCategory>
            {
                new SegmentCategory(RootSegmentCategory.SecondaryStreet),
            });

            var label = lod > 13;
            var color = day ? Colorf.White : Colorf.White * 0.5f;
            var properties = new List<Property>
            {
                new Int32Property("z", 10),
                new ColorProperty("main", color * SegmentColorFactor),
                new DoubleProperty("width", width),
                new BoolProperty("outline", true),
                new ColorProperty("outline_color", color * SegmentOutlineColorFactor),
                new DoubleProperty("outline_width", width * SegmentOutlineSizeFactor),
                new BoolProperty("label", label),
                new SingleProperty("label_padding", 1.5f),
            };

            var appearance = new SegmentAppearance(properties, projection);
            var target = new SegmentAppearanceTarget(appearance, filter);

            return target;
        }

        private static SegmentAppearanceTarget SecondaryStreetLinkTarget(
            IProjection projection, int lod, bool day)
        {
            var width = BaseSegmentWidth(lod) * 1.25d;
            var filter = new SegmentCategoryFilter(new List<SegmentCategory>
            {
                new SegmentCategory(RootSegmentCategory.SecondaryStreetLink),
            });

            var color = day ? Colorf.White : new Colorf(0.137f, 0.184f, 0.24f);
            var properties = new List<Property>
            {
                new Int32Property("z", 10),
                new ColorProperty("main", color * SegmentColorFactor),
                new DoubleProperty("width", width),
                new BoolProperty("outline", true),
                new ColorProperty("outline_color", color * SegmentOutlineColorFactor),
                new DoubleProperty("outline_width", width * SegmentOutlineSizeFactor),
            };

            var appearance = new SegmentAppearance(properties, projection);
            var target = new SegmentAppearanceTarget(appearance, filter);

            return target;
        }

        private static SegmentAppearanceTarget ResidentialStreetTarget(
            IProjection projection, int lod, bool day)
        {
            var width = BaseSegmentWidth(lod);
            var filter = new SegmentCategoryFilter(new List<SegmentCategory>
            {
                new SegmentCategory(RootSegmentCategory.ResidentialStreet),
            });

            var label = lod > 14;
            var color = day ? Colorf.Grey : new Colorf(0.137f, 0.184f, 0.24f);
            var properties = new List<Property>
            {
                new Int32Property("z", 8),
                new ColorProperty("main", color * SegmentColorFactor),
                new BoolProperty("outline", true),
                new DoubleProperty("width", width),
                new ColorProperty("outline_color", color * SegmentOutlineColorFactor),
                new DoubleProperty("outline_width", width * SegmentOutlineSizeFactor),
                new BoolProperty("label", label),
                new SingleProperty("label_padding", 1.5f),
                new SingleProperty("label_minimum_segment_length", 100f),
            };

            var appearance = new SegmentAppearance(properties, projection);
            var target = new SegmentAppearanceTarget(appearance, filter);

            return target;
        }

        private static SegmentAppearanceTarget OtherStreetTarget(IProjection projection,
            int lod, bool day)
        {
            var width = BaseSegmentWidth(lod);
            var filter = new SegmentCategoryFilter(new List<SegmentCategory>
            {
                new SegmentCategory(RootSegmentCategory.UnclassifiedStreet),
                new SegmentCategory(RootSegmentCategory.PedestrianSharedStreet),
                new SegmentCategory(RootSegmentCategory.ServiceStreet),
            });

            var label = lod > 14;
            var color = day ? Colorf.Grey : new Colorf(0.137f, 0.184f, 0.24f);
            var properties = new List<Property>
            {
                new Int32Property("z", 8),
                new ColorProperty("main", color * SegmentColorFactor),
                new DoubleProperty("width", width),
                new BoolProperty("outline", true),
                new ColorProperty("outline_color", color * SegmentOutlineColorFactor),
                new DoubleProperty("outline_width", width * SegmentOutlineSizeFactor),
                new BoolProperty("label", label),
                new SingleProperty("label_padding", 1.5f),
                new SingleProperty("label_minimum_segment_length", 100f),
            };

            var appearance = new SegmentAppearance(properties, projection);
            var target = new SegmentAppearanceTarget(appearance, filter);

            return target;
        }

        private static SegmentAppearanceTarget UnknownStreetTarget(
            IProjection projection, int lod, bool day)
        {
            var width = BaseSegmentWidth(lod);
            var filter = new SegmentCategoryFilter(new List<SegmentCategory>
            {
                new SegmentCategory(RootSegmentCategory.UnknownStreet),
            });

            var label = lod > 14;
            var color = Colorf.Magenta;
            var properties = new List<Property>
            {
                new Int32Property("z", 7),
                new ColorProperty("main", color * SegmentColorFactor),
                new DoubleProperty("width", width),
                new BoolProperty("outline", true),
                new ColorProperty("outline_color", color * SegmentOutlineColorFactor),
                new DoubleProperty("outline_width", width * SegmentOutlineSizeFactor),
                new BoolProperty("label", label),
                new SingleProperty("label_padding", 1.5f),
            };

            var appearance = new SegmentAppearance(properties, projection);
            var target = new SegmentAppearanceTarget(appearance, filter);

            return target;
        }

        private static AreaAppearanceTarget WaterTarget(IProjection projection, int lod,
            bool day)
        {
            var filter = new AreaCategoryFilter(new List<AreaCategory>
            {
                new AreaCategory(RootAreaCategory.Water),
            });

            var color = day ? Colorf.Blue : Colorf.Blue * 0.5f;
            var properties = new List<Property>
            {
                new Int32Property("z", 6),
                new ColorProperty("main", color),
            };

            var appearance = new AreaAppearance(properties, projection);
            var target = new AreaAppearanceTarget(appearance, filter);

            return target;
        }

        private static AreaAppearanceTarget ParksForestFarmAndGrassTarget(
            IProjection projection, int lod, bool day)
        {
            var filter = new AreaCategoryFilter(new List<AreaCategory>
            {
                new AreaCategory(RootAreaCategory.Park),
                new AreaCategory(RootAreaCategory.Forest),
                new AreaCategory(RootAreaCategory.Farmland),
                new AreaCategory(RootAreaCategory.Grassland),
            });

            var color = day ? Colorf.Green * 0.5f : Colorf.Green * 0.75f;
            var properties = new List<Property>
            {
                new Int32Property("z", 5),
                new ColorProperty("main", color),
            };

            var appearance = new AreaAppearance(properties, projection);
            var target = new AreaAppearanceTarget(appearance, filter);

            return target;
        }

        private static AreaAppearanceTarget ResidentialTarget(IProjection projection, 
            int lod, bool day)
        {
            var filter = new AreaCategoryFilter(new List<AreaCategory>
            {
                new AreaCategory(RootAreaCategory.ResidentialZone),
            });

            var color = day ? Colorf.Grey : Colorf.Grey * 0.5f;
            var properties = new List<Property>
            {
                new Int32Property("z", 3),
                new ColorProperty("main", color),
            };

            var appearance = new AreaAppearance(properties, projection);
            var target = new AreaAppearanceTarget(appearance, filter);

            return target;
        }

        private static AreaAppearanceTarget RetailTarget(IProjection projection, 
            int lod, bool day)
        {
            var filter = new AreaCategoryFilter(new List<AreaCategory>
            {
                new AreaCategory(RootAreaCategory.RetailZone),
            });

            var color = day ? Colorf.Grey : Colorf.Grey * 0.5f;
            var properties = new List<Property>
            {
                new Int32Property("z", 3),
                new ColorProperty("main", color),
            };

            var appearance = new AreaAppearance(properties, projection);
            var target = new AreaAppearanceTarget(appearance, filter);

            return target;
        }

        private static AreaAppearanceTarget IndustrialTarget(IProjection projection, 
            int lod, bool day)
        {
            var filter = new AreaCategoryFilter(new List<AreaCategory>
            {
                new AreaCategory(RootAreaCategory.IndustrialZone),
            });

            var color = day ? Colorf.Grey : Colorf.Grey * 0.5f;
            var properties = new List<Property>
            {
                new Int32Property("z", 2),
                new ColorProperty("main", color),
            };

            var appearance = new AreaAppearance(properties, projection);
            var target = new AreaAppearanceTarget(appearance, filter);

            return target;
        }

        private static AreaAppearanceTarget BuildingTarget(IProjection projection,
            int lod, bool day)
        {
            var filter = new AreaCategoryFilter(new List<AreaCategory>
            {
                new AreaCategory(RootAreaCategory.Building),
            });

            var color = day ? Colorf.White * 0.7f : Colorf.White * 0.2f;
            var properties = new List<Property>
            {
                new Int32Property("z", 4),
                new ColorProperty("main", color),
                new DoubleProperty("height", 0.000005d),
            };

            var appearance = new AreaAppearance(properties, projection);
            var target = new AreaAppearanceTarget(appearance, filter);

            return target;
        }

        private static AreaAppearanceTarget CommercialTarget(IProjection projection,
            int lod, bool day)
        {
            var filter = new AreaCategoryFilter(new List<AreaCategory>
            {
                new AreaCategory(RootAreaCategory.CommercialZone),
            });

            var color = day ? Colorf.Grey : Colorf.Grey * 0.5f;
            var properties = new List<Property>
            {
                new Int32Property("z", 2),
                new ColorProperty("main", color),
            };

            var appearance = new AreaAppearance(properties, projection);
            var target = new AreaAppearanceTarget(appearance, filter);

            return target;
        }

        private static AreaAppearanceTarget MilitaryTarget(IProjection projection, 
            int lod, bool day)
        {
            var filter = new AreaCategoryFilter(new List<AreaCategory>
            {
                new AreaCategory(RootAreaCategory.MilitaryZone),
            });

            var color = day ? Colorf.Red : Colorf.Red * 0.5f;
            var properties = new List<Property>
            {
                new Int32Property("z", 1),
                new ColorProperty("main", Colorf.Red),
            };

            var appearance = new AreaAppearance(properties, projection);
            var target = new AreaAppearanceTarget(appearance, filter);

            return target;
        }

        private static AreaAppearanceTarget UnknownTarget(IProjection projection, 
            int lod, bool day)
        {
            var filter = new AreaCategoryFilter(new List<AreaCategory>
            {
                new AreaCategory(RootAreaCategory.Invalid),
            });

            var properties = new List<Property>
            {
                new Int32Property("z", 0),
                new ColorProperty("main", Colorf.Magenta),
            };

            var appearance = new AreaAppearance(properties, projection);
            var target = new AreaAppearanceTarget(appearance, filter);

            return target;
        }

        private static AreaAppearanceTarget TileTarget(IProjection projection, 
            int lod, bool day)
        {
            var filter = new AreaCategoryFilter(new List<AreaCategory>
            {
                new AreaCategory(RootAreaCategory.Tile),
            });

            var color = day ? Colorf.White * 0.25f : new Colorf(0.137f, 0.184f, 0.24f) * 0.25f;
            var properties = new List<Property>
            {
                new Int32Property("z", -1),
                new ColorProperty("main", color),
            };

            var appearance = new AreaAppearance(properties, projection);
            var target = new AreaAppearanceTarget(appearance, filter);

            return target;
        }

        private static double BaseSegmentWidth(int lod)
        {
            double lineWidth;

            switch (lod)
            {
                case 16:
                    lineWidth = Mathd.EpsilonE6;
                    break;
                case 15:
                    lineWidth = Mathd.EpsilonE6 * 1.25d;
                    break;
                case 14:
                    lineWidth = Mathd.EpsilonE6 * 1.75d;
                    break;
                case 13:
                    lineWidth = Mathd.EpsilonE6 * 2.25d;
                    break;
                case 12:
                    lineWidth = Mathd.EpsilonE6 * 4.0d;
                    break;
                case 11:
                    lineWidth = Mathd.EpsilonE6 * 7.0d;
                    break;
                case 10:
                    lineWidth = Mathd.EpsilonE6 * 10d;
                    break;
                default:
                    throw new NotImplementedException();
            }

            return lineWidth;
        }
    }
}