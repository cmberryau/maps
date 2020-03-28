using System;
using System.Collections.Generic;
using System.Drawing;
using log4net;
using Maps.Geographical.Features;
using Maps.Geographical.Places;

namespace Maps.Data.OpenStreetMap.Translation
{
    /// <summary>
    /// Responsible for translating Nodes into places
    /// </summary>
    internal class PlaceTranslator : FeatureTranslator, INodeTranslator
    {
        /// <summary>
        /// The default place translator
        /// </summary>
        public static PlaceTranslator Default
        {
            get
            {
                var tags = new Dictionary<string, ISet<string>>();

                // place tags
                var place = tags["place"] = new HashSet<string>();

                // populated settlements, urban
                place.Add("city");
                place.Add("borough");
                place.Add("suburb");
                place.Add("quarter");
                place.Add("neighbourhood");

                // populated settlements, urban and rural
                place.Add("town");
                place.Add("village");
                place.Add("hamlet");

                // amenity tags
                var amenity = tags["amenity"] = new HashSet<string>();

                // petrol
                amenity.Add("fuel");

                // petrol
                amenity.Add("parking");

                return new PlaceTranslator(tags);
            }
        }

        private static readonly ILog Log = LogManager.GetLogger(typeof(PlaceTranslator));
        private readonly PlaceCategoryMap _categoryMap;
        private readonly Bitmap _petrolBitmap;
        private readonly Bitmap _parkingBitmap;

        /// <summary>
        /// Creates a new instance of PlaceTranslator
        /// </summary>
        /// <param name="tags">Tags that will result in a place</param>
        public PlaceTranslator(IDictionary<string, ISet<string>> tags) : base(tags)
        {
            _categoryMap = new PlaceCategoryMap();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            var parkingFile = assembly.GetManifestResourceStream(
                "Maps.Data.OpenStreetMap.Resources.Place_Parking33p.png");
            var petrolFile = assembly.GetManifestResourceStream(
                "Maps.Data.OpenStreetMap.Resources.Place_Petrol33p.png");

            _parkingBitmap = new Bitmap(Image.FromStream(parkingFile));
            _petrolBitmap = new Bitmap(Image.FromStream(petrolFile));
        }

        /// <inheritdoc />
        public bool TryTranslate(Node node, out Feature feature)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            var result = TagsMatch(node);
            feature = null;

            if (result)
            {
                var rootCategory = _categoryMap.Map(node.Tags);

                if (rootCategory != RootPlaceCategory.Invalid)
                {
                    var category = new PlaceCategory(rootCategory);

                    if (rootCategory == RootPlaceCategory.Parking)
                    {
                        var place = new Place(node.Guid, node.Name, node.Coordinate,
                            category, _parkingBitmap);
                        feature = place;
                    }
                    else if (rootCategory == RootPlaceCategory.Petrol)
                    {
                        var place = new Place(node.Guid, node.Name, node.Coordinate,
                            category, _petrolBitmap);
                        feature = place;
                    }
                    else
                    {
                        var place = new Place(node.Guid, node.Name, node.Coordinate,
                            category);
                        feature = place;
                    }
                }
                else
                {
                    Log.Info($"Root category is invalid for: {node}, rejecting");
                }
            }

            return result;
        }
    }
}