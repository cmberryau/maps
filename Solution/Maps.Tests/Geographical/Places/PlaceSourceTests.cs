using System;
using System.Collections.Generic;
using Maps.Geographical;
using Maps.Geographical.Places;
using NUnit.Framework;

namespace Maps.Tests.Geographical.Places
{
    /// <summary>
    /// An series of minimum tests for IPlaceSource implementers
    /// </summary>
    [TestFixture]
    public abstract class PlaceSourceTests
    {
        /// <summary>
        /// Creates a fresh IPlaceSource to be used for testing
        /// </summary>
        protected abstract IPlaceSource CreateSource();

        private const double DefaultBoxQueryRange = 2000d;

        /// <summary>
        /// Tests the constructor for the IPlaceSource
        /// </summary>
        [Test]
        public abstract void TestConstructor();

        /// <summary>
        /// Tests the ability of the IPlaceSource to query potentially
        /// queryable categories given a coordinate
        /// </summary>
        [Test]
        public void TestGetCategoriesMethod()
        {
            var source = CreateSource();
            var categories = source.GetCategories(TestUtilities.Ingolstadt, DefaultBoxQueryRange);

            ValidateCategories(categories);
        }

        /// <summary>
        /// Tests the ability of the IPlaceSource to query potentially
        /// queryable categories in an asyncronous manner given a coordinate
        /// </summary>
        [Test]
        public void TestGetCategoriesAsyncMethod()
        {
            var source = CreateSource();
            var categoriesAsync = source.GetCategoriesAsync(TestUtilities.Ingolstadt, 
                DefaultBoxQueryRange);
            categoriesAsync.Wait();
            var categories = categoriesAsync.Result;

            ValidateCategories(categories);
        }

        /// <summary>
        /// Tests the ability of the IPlaceSource to query potentially
        /// queryable categories given a coordinate box
        /// </summary>
        [Test]
        public void TestBoxGetCategoriesMethod()
        {
            var source = CreateSource();

            if (!source.SupportsBoxQueries)
            {
                return;
            }

            var categories = source.GetCategories(TestUtilities.BigIngolstadtBox);

            ValidateCategories(categories);
        }

        /// <summary>
        /// Tests the ability of the IPlaceSource to query potentially
        /// queryable categories in an asyncronous manner given a coordinate box
        /// </summary>
        [Test]
        public void TestBoxGetCategoriesAsyncMethod()
        {
            var source = CreateSource();

            if (!source.SupportsBoxQueries)
            {
                return;
            }

            var categoriesAsync = source.GetCategoriesAsync(TestUtilities.BigIngolstadtBox);
            categoriesAsync.Wait();
            var categories = categoriesAsync.Result;

            ValidateCategories(categories);
        }

        /// <summary>
        /// Tests the ability of the IPlaceSource to query places
        /// </summary>
        [Test]
        public void TestCoordinateGetMethod()
        {
            var source = CreateSource();
            var queryString = "parking";

            // ensure that a valid query string returns results
            var places = source.Get(TestUtilities.Ingolstadt, queryString, DefaultBoxQueryRange);

            ValidateParkingViaStringResults(places);

            // ensure that a valid category returns results
            places = source.Get(TestUtilities.Ingolstadt,
                new PlaceCategory(RootPlaceCategory.Parking), DefaultBoxQueryRange);

            ValidateParkingViaCategoryResults(places);

            var categoriesList = new List<PlaceCategory>
            {
                new PlaceCategory(RootPlaceCategory.Accomodation),
                new PlaceCategory(RootPlaceCategory.Parking)
            };

            // ensure that a valid category list returns results
            places = source.Get(TestUtilities.Ingolstadt, categoriesList, DefaultBoxQueryRange);

            ValidateParkingAccomodationViaCategoryResults(places);
        }

        /// <summary>
        /// Tests the ability of the IPlaceSource to query places
        /// in an asyncronous manner given a coordinate
        /// </summary>
        [Test]
        public void TestCoordinateGetAsyncMethod()
        {
            var source = CreateSource();
            var queryString = "parking";

            // ensure that a valid query string returns results
            var placesAsync = source.GetAsync(TestUtilities.Ingolstadt,
                queryString, DefaultBoxQueryRange);

            placesAsync.Wait();
            var places = placesAsync.Result;

            ValidateParkingViaStringResults(places);

            // ensure that a valid category returns results
            placesAsync = source.GetAsync(TestUtilities.Ingolstadt,
                new PlaceCategory(RootPlaceCategory.Parking), DefaultBoxQueryRange);
            places = placesAsync.Result;

            ValidateParkingViaCategoryResults(places);

            var categoriesList = new List<PlaceCategory>
            {
                new PlaceCategory(RootPlaceCategory.Accomodation),
                new PlaceCategory(RootPlaceCategory.Parking)
            };

            // ensure that a valid category list returns results
            placesAsync = source.GetAsync(TestUtilities.Ingolstadt, categoriesList, 
                DefaultBoxQueryRange);
            places = placesAsync.Result;

            ValidateParkingAccomodationViaCategoryResults(places);
        }

        /// <summary>
        /// Tests the ability of the IPlaceSource to query places given a coordinate box
        /// </summary>
        [Test]
        public void TestBoxGetMethod()
        {
            var source = CreateSource();

            if (!source.SupportsBoxQueries)
            {
                return;
            }

            var queryString = "parking";

            // ensure that a valid query string returns results
            var places = source.Get(TestUtilities.BigIngolstadtBox, queryString);

            ValidateParkingViaStringResults(places);

            // ensure that a valid category returns results
            places = source.Get(TestUtilities.BigIngolstadtBox, 
                new PlaceCategory(RootPlaceCategory.Parking));

            ValidateParkingViaCategoryResults(places);

            var categoriesList = new List<PlaceCategory>
            {
                new PlaceCategory(RootPlaceCategory.Accomodation),
                new PlaceCategory(RootPlaceCategory.Parking)
            };

            // ensure that a valid category list returns results
            places = source.Get(TestUtilities.BigIngolstadtBox, categoriesList);

            ValidateParkingAccomodationViaCategoryResults(places);
        }

        /// <summary>
        /// Tests the ability of the IPlaceSource to query places
        /// in an asyncronous manner given a coordinate box
        /// </summary>
        [Test]
        public void TestBoxGetAsyncMethod()
        {
            var source = CreateSource();

            if (!source.SupportsBoxQueries)
            {
                return;
            }

            var queryString = "parking";

            // ensure that a valid query string returns results
            var placesAsync = source.GetAsync(TestUtilities.BigIngolstadtBox, queryString);

            placesAsync.Wait();
            var places = placesAsync.Result;

            ValidateParkingViaStringResults(places);

            // ensure that a valid category returns results
            placesAsync = source.GetAsync(TestUtilities.BigIngolstadtBox,
                new PlaceCategory(RootPlaceCategory.Parking));

            placesAsync.Wait();
            places = placesAsync.Result;

            ValidateParkingViaCategoryResults(places);

            var categoriesList = new List<PlaceCategory>
            {
                new PlaceCategory(RootPlaceCategory.Accomodation),
                new PlaceCategory(RootPlaceCategory.Parking)
            };

            // ensure that a valid category list returns results
            placesAsync = source.GetAsync(TestUtilities.BigIngolstadtBox, categoriesList);

            placesAsync.Wait();
            places = placesAsync.Result;

            ValidateParkingAccomodationViaCategoryResults(places);
        }

        /// <summary>
        /// Tests the response of the IPlaceSource to queries which should return empty
        /// </summary>
        [Test]
        public void TestCoordinateGetMethodEmpty()
        {
            var source = CreateSource();
            var queryString = "parking";

            // ensure that a query that *hopefully* returns nothing doesn't throw
            var places = source.Get(Geodetic2d.NorthPole, queryString, DefaultBoxQueryRange);

            Assert.That(places.Count == 0);

            // ensure that a query that *hopefully* returns nothing doesn't throw
            places = source.Get(TestUtilities.Ingolstadt,
                new PlaceCategory(RootPlaceCategory.Invalid), DefaultBoxQueryRange);

            Assert.That(places.Count == 0);
        }

        /// <summary>
        /// Tests the response of the IPlaceSource to empty queries
        /// in an asyncronous manner
        /// </summary>
        [Test]
        public void TestCoordinateGetAsyncMethodEmpty()
        {
            var source = CreateSource();
            var queryString = "parking";

            // ensure that a query that *hopefully* returns nothing doesn't throw
            var placesAsync = source.GetAsync(Geodetic2d.NorthPole, queryString, DefaultBoxQueryRange);

            placesAsync.Wait();
            var places = placesAsync.Result;

            Assert.That(places.Count == 0);

            // ensure that a query that *hopefully* returns nothing doesn't throw
            placesAsync = source.GetAsync(TestUtilities.Ingolstadt,
                new PlaceCategory(RootPlaceCategory.Invalid), DefaultBoxQueryRange);

            placesAsync.Wait();
            places = placesAsync.Result;

            Assert.That(places.Count == 0);
        }

        /// <summary>
        /// Tests the response of the IPlaceSource to queries which should return empty
        /// </summary>
        [Test]
        public void TestBoxGetMethodEmpty()
        {
            var source = CreateSource();

            if (!source.SupportsBoxQueries)
            {
                return;
            }

            var queryString = "parking";

            // ensure that a query that *hopefully* returns nothing doesn't throw
            var places = source.Get(new GeodeticBox2d(Geodetic2d.NorthPole,
                DefaultBoxQueryRange), queryString);

            Assert.That(places.Count == 0);

            // ensure that a query that *hopefully* returns nothing doesn't throw
            places = source.Get(TestUtilities.Ingolstadt,
                new PlaceCategory(RootPlaceCategory.Invalid), DefaultBoxQueryRange);

            Assert.That(places.Count == 0);
        }

        /// <summary>
        /// Tests the response of the IPlaceSource to empty queries
        /// in an asyncronous manner
        /// </summary>
        [Test]
        public void TestBoxGetAsyncMethodEmpty()
        {
            var source = CreateSource();

            if (!source.SupportsBoxQueries)
            {
                return;
            }

            var queryString = "parking";

            // ensure that a query that *hopefully* returns nothing doesn't throw
            var placesAsync = source.GetAsync(new GeodeticBox2d(Geodetic2d.NorthPole, 
                DefaultBoxQueryRange), queryString);

            placesAsync.Wait();
            var places = placesAsync.Result;

            Assert.That(places.Count == 0);

            // ensure that a query that *hopefully* returns nothing doesn't throw
            placesAsync = source.GetAsync(TestUtilities.Ingolstadt,
                new PlaceCategory(RootPlaceCategory.Invalid), DefaultBoxQueryRange);

            placesAsync.Wait();
            places = placesAsync.Result;

            Assert.That(places.Count == 0);
        }

        /// <summary>
        /// Tests the response of the IPlaceSource to invalid queries
        /// </summary>
        [Test]
        public void TestCoordinateGetMethodInvalid()
        {
            var source = CreateSource();
            string queryString = null;

            // ensure that a null query string throws
            Assert.Throws<ArgumentException>(
                () => source.Get(TestUtilities.Ingolstadt, queryString, DefaultBoxQueryRange));

            queryString = "";

            // ensure that an empty query string throws
            Assert.Throws<ArgumentException>(
                () => source.Get(TestUtilities.Ingolstadt, queryString, DefaultBoxQueryRange));

            PlaceCategory queryCategory = null;

            // ensure that a null category throws
            Assert.Throws<ArgumentNullException>(() =>source.Get(TestUtilities.Ingolstadt,
                queryCategory, DefaultBoxQueryRange));

            List<PlaceCategory> queryCategories = null;

            // ensure that a null list of categories throws
            Assert.Throws<ArgumentNullException>(() => source.Get(TestUtilities.Ingolstadt,
                queryCategories, DefaultBoxQueryRange));
        }

        /// <summary>
        /// Tests the response of the IPlaceSource to invalid queries
        /// in an asyncronous manner
        /// </summary>
        [Test]
        public void TestCoordinateGetAsyncMethodInvalid()
        {
            var source = CreateSource();
            string queryString = null;

            // ensure that a null query string throws
            Assert.Throws<ArgumentException>(
                () => source.GetAsync(TestUtilities.Ingolstadt, queryString, DefaultBoxQueryRange));

            queryString = "";

            // ensure that an empty query string throws
            Assert.Throws<ArgumentException>(
                () => source.GetAsync(TestUtilities.Ingolstadt, queryString, DefaultBoxQueryRange));

            PlaceCategory queryCategory = null;

            // ensure that a null category throws
            Assert.Throws<ArgumentNullException>(() => source.GetAsync(TestUtilities.Ingolstadt, 
                queryCategory, DefaultBoxQueryRange));

            List<PlaceCategory> queryCategories = null;

            // ensure that a null list of categories throws
            Assert.Throws<ArgumentNullException>(() => source.GetAsync(TestUtilities.Ingolstadt, 
                queryCategories, DefaultBoxQueryRange));
        }

        /// <summary>
        /// Tests the response of the IPlaceSource to invalid queries
        /// </summary>
        [Test]
        public void TestBoxGetMethodInvalid()
        {
            var source = CreateSource();

            if (!source.SupportsBoxQueries)
            {
                return;
            }

            string queryString = null;

            // ensure that a null query string throws
            Assert.Throws<ArgumentException>(
                () => source.Get(TestUtilities.BigIngolstadtBox, queryString));

            queryString = "";

            // ensure that an empty query string throws
            Assert.Throws<ArgumentException>(
                () => source.Get(TestUtilities.BigIngolstadtBox, queryString));

            PlaceCategory queryCategory = null;

            // ensure that a null category throws
            Assert.Throws<ArgumentNullException>(() => source.Get(TestUtilities.BigIngolstadtBox,
                queryCategory));

            List<PlaceCategory> queryCategories = null;

            // ensure that a null list of categories throws
            Assert.Throws<ArgumentNullException>(() => source.Get(TestUtilities.BigIngolstadtBox,
                queryCategories));
        }

        /// <summary>
        /// Tests the response of the IPlaceSource to invalid queries
        /// in an asyncronous manner
        /// </summary>
        [Test]
        public void TestBoxGetAsyncMethodInvalid()
        {
            var source = CreateSource();

            if (!source.SupportsBoxQueries)
            {
                return;
            }

            string queryString = null;

            // ensure that a null query string throws
            Assert.Throws<ArgumentException>(
                () => source.GetAsync(TestUtilities.BigIngolstadtBox, queryString));

            queryString = "";

            // ensure that an empty query string throws
            Assert.Throws<ArgumentException>(
                () => source.GetAsync(TestUtilities.BigIngolstadtBox, queryString));

            PlaceCategory queryCategory = null;

            // ensure that a null category throws
            Assert.Throws<ArgumentNullException>(() => source.GetAsync(TestUtilities.BigIngolstadtBox,
                queryCategory));

            List<PlaceCategory> queryCategories = null;

            // ensure that a null list of categories throws
            Assert.Throws<ArgumentNullException>(() => source.GetAsync(TestUtilities.BigIngolstadtBox,
                queryCategories));
        }

        private static void ValidateCategories(IList<PlaceCategory> categories)
        {
            // check for known categories
            var containsParking = false;
            var containsFoodAndDrink = false;
            var containsEntertainment = false;

            foreach (var category in categories)
            {
                if (category.Root == RootPlaceCategory.Parking)
                {
                    containsParking = true;
                }
                else if (category.Root == RootPlaceCategory.FoodAndDrink)
                {
                    containsFoodAndDrink = true;
                }
                else if (category.Root == RootPlaceCategory.Entertainment)
                {
                    containsEntertainment = true;
                }
            }

            Assert.True(containsParking);
            Assert.True(containsFoodAndDrink);
            Assert.True(containsEntertainment);
        }

        private static void ValidateParkingAccomodationViaCategoryResults(IList<Place> places)
        {
            // ensure there is something
            Assert.IsTrue(places.Count > 0);

            // ensure that it has parking and accomodation category results
            Assert.IsTrue(HasAll(places, new List<RootPlaceCategory>
            {
                RootPlaceCategory.Accomodation,
                RootPlaceCategory.Parking
            }, false));

            // ensure there is a known result
            Assert.IsTrue(HasAll(places, new List<string>
            {
                "Münster",
                "Altstadthotel"
            }, false));
        }

        private static void ValidateParkingViaCategoryResults(IList<Place> places)
        {
            // ensure there is something
            Assert.IsTrue(places.Count > 0);

            // ensure that it only has parking category results
            Assert.IsTrue(HasAll(places, new List<RootPlaceCategory>
            {
                RootPlaceCategory.Parking
            }, true));

            // ensure there is a known result
            Assert.IsTrue(HasAll(places, new List<string>
            {
                "Münster"
            }, false));
        }

        private static void ValidateParkingViaStringResults(IList<Place> places)
        {
            // ensure there is something
            Assert.IsTrue(places.Count > 0);

            // ensure that it has parking category results
            Assert.IsTrue(HasAll(places, new List<RootPlaceCategory>
            {
                RootPlaceCategory.Parking
            }, false));

            // ensure it has a known result
            Assert.IsTrue(HasAll(places, new List<string>
            {
                "Münster"
            }, false));
        }

        private static bool HasOne(IList<Place> results, IList<RootPlaceCategory> categories,
            bool exclusive)
        {
            var hasOne = false;

            foreach (var place in results)
            {
                if (categories.Contains(place.Category.Root))
                {
                    hasOne = true;
                }
                else
                {
                    if (exclusive)
                    {
                        return false;
                    }
                }
            }

            return hasOne;
        }

        private static bool HasAll(IList<Place> results, IList<RootPlaceCategory> categories,
            bool exclusive)
        {
            var checklist = new Dictionary<RootPlaceCategory, bool>(categories.Count);

            foreach (var category in categories)
            {
                checklist[category] = false;
            }

            foreach (var place in results)
            {
                if (categories.Contains(place.Category.Root))
                {
                    checklist[place.Category.Root] = true;
                }
                else
                {
                    if (exclusive)
                    {
                        return false;
                    }
                }
            }

            foreach (var check in checklist.Keys)
            {
                if (!checklist[check])
                {
                    return false;
                }
            }

            return true;
        }

        private static bool HasOne(IList<Place> results, IList<string> names, bool exclusive)
        {
            var hasOne = false;

            foreach (var place in results)
            {
                if (names.Contains(place.Name))
                {
                    hasOne = true;
                }
                else
                {
                    if (exclusive)
                    {
                        return false;
                    }
                }
            }

            return hasOne;
        }

        private static bool HasAll(IList<Place> results, IList<string> names, bool exclusive)
        {
            var checklist = new Dictionary<string, bool>(names.Count);

            foreach (var category in names)
            {
                checklist[category] = false;
            }

            foreach (var place in results)
            {
                if (names.Contains(place.Name))
                {
                    checklist[place.Name] = true;
                }
                else
                {
                    if (exclusive)
                    {
                        return false;
                    }
                }
            }

            foreach (var check in checklist.Keys)
            {
                if (!checklist[check])
                {
                    return false;
                }
            }

            return true;
        }
    }
}