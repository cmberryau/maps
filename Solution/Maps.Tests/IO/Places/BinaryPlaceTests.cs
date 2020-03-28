using System;
using System.IO;
using Maps.Geographical;
using Maps.Geographical.Places;
using Maps.IO.Features;
using Maps.IO.Places;
using Maps.Tests.IO.Features;
using NUnit.Framework;

namespace Maps.Tests.IO.Places
{
    /// <summary>
    /// A series of tests for the BinaryPlace class
    /// </summary>
    [TestFixture]
    internal sealed class BinaryPlaceTests : BinaryFeatureTests
    {
        /// <summary>
        /// A sample place with a small guid
        /// </summary>
        internal static Place SmallGuidPlace
        {
            get
            {
                return new Place(SmallGuid, Name, Coordinate, Category);
            }
        }

        /// <summary>
        ///  A sample place with a big guid
        /// </summary>
        internal static Place BigGuidPlace
        {
            get
            {
                return new Place(BigGuid, Name, Coordinate, Category);
            }
        }

        private static readonly Geodetic2d Coordinate = Geodetic2d.NorthPole;
        private static readonly PlaceCategory Category = new PlaceCategory(RootPlaceCategory.Invalid);
        private static readonly string Name = "";

        /// <summary>
        /// Tests the constructor of the BinaryPlace
        /// </summary>
        [Test]
        public override void TestConstructor()
        {
            var binaryPlace = new BinaryPlace(SmallGuidPlace, null);

            Assert.IsNotNull(binaryPlace);
        }

        /// <summary>
        /// Tests the constructor of the BinaryPlace when given invalid parameters
        /// </summary>
        [Test]
        public override void TestConstructorInvalidParameters()
        {
            Place place = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                var binaryPlace = new BinaryPlace(place, null);
            });
        }

        /// <summary>
        /// Tests the serialization of the BinaryPlace when using a small guid
        /// </summary>
        [Test]
        public override void TestSerializationSmallGuid()
        {
            File.Delete(FullPath);

            var expectedPlace = SmallGuidPlace;

            using (var file = File.Create(FullPath))
            {
                new BinaryPlace(expectedPlace, null).Serialize(file);
            }

            using (var file = File.OpenRead(FullPath))
            {
                // ensure we can deserialize to a BinaryFeature
                var binaryFeature = BinaryFeature.Deserialize(file);
                Assert.IsNotNull(binaryFeature);

                // ensure it can be casted to a BinaryPlace
                var binaryPlace = binaryFeature as BinaryPlace;
                Assert.IsNotNull(binaryPlace);

                // ensure that we get a Feature instance back from ToFeature
                var actualFeature = binaryFeature.ToFeature(null);
                Assert.IsNotNull(actualFeature);

                // ensure it can be casted to a Place
                var actualPlace = actualFeature as Place;
                Assert.IsNotNull(actualPlace);

                // ensure all details of the Place instance
                Assert.AreEqual(expectedPlace.Guid, actualPlace.Guid);
                Assert.AreEqual(expectedPlace.Name, actualPlace.Name);
                Assert.AreEqual(expectedPlace.Category.Root, actualPlace.Category.Root);
                //Assert.AreEqual(expectedPlace .Category, actualPlace.Category);
                TestUtilities.AssertThatGeodetic2dsAreEqual(
                    expectedPlace.Coordinate, actualPlace.Coordinate);

                // confirm file size is what we expect
                Assert.AreEqual(15L, file.Length);
            }

            File.Delete(FullPath);
        }

        /// <summary>
        /// Tests the serialization of the BinaryPlace when given a large guid
        /// </summary>
        [Test]
        public override void TestSerializationFullSizedGuid()
        {
            File.Delete(FullPath);

            var expectedPlace = BigGuidPlace;

            using (var file = File.Create(FullPath))
            {
                new BinaryPlace(expectedPlace, null).Serialize(file);
            }

            using (var file = File.OpenRead(FullPath))
            {
                // ensure we can deserialize to a BinaryFeature
                var binaryFeature = BinaryFeature.Deserialize(file);
                Assert.IsNotNull(binaryFeature);

                // ensure it can be casted to a BinaryPlace
                var binaryPlace = binaryFeature as BinaryPlace;
                Assert.IsNotNull(binaryPlace);

                // ensure that we get a Feature instance back from ToFeature
                var actualFeature = binaryFeature.ToFeature(null);
                Assert.IsNotNull(actualFeature);

                // ensure it can be casted to a Place
                var actualPlace = actualFeature as Place;
                Assert.IsNotNull(actualPlace);

                // ensure all details of the Place instance
                Assert.AreEqual(expectedPlace.Guid, actualPlace.Guid);
                Assert.AreEqual(expectedPlace.Name, actualPlace.Name);
                Assert.AreEqual(expectedPlace.Category.Root, actualPlace.Category.Root);
                //Assert.AreEqual(expectedPlace .Category, actualPlace.Category);
                TestUtilities.AssertThatGeodetic2dsAreEqual(
                    expectedPlace.Coordinate, actualPlace.Coordinate);

                // confirm file size is what we expect
                Assert.AreEqual(34L, file.Length);
            }

            File.Delete(FullPath);
        }
    }
}