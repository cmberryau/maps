using System;
using System.Collections.Generic;
using System.IO;
using Maps.Geographical;
using Maps.Geographical.Features;
using Maps.IO.Features;
using NUnit.Framework;

namespace Maps.Tests.IO.Features
{
    /// <summary>
    /// A series of tests for the BinaryArea class
    /// </summary>
    [TestFixture]
    internal sealed class BinaryAreaTests : BinaryFeatureTests
    {
        /// <summary>
        /// A sample area with a small guid
        /// </summary>
        internal static Area SmallGuidArea
        {
            get
            {
                return new Area(SmallGuid, "", Simple, AreaCategory.Unknown, Simple.Area);
            }
        }

        /// <summary>
        /// A sample area with a small guid and inner coordinates
        /// </summary>
        internal static Area SmallGuidAreaInnerCoordinates
        {
            get
            {
                return new Area(SmallGuid, "", SimpleWithHole, AreaCategory.Unknown,
                    SimpleWithHole.Area);
            }
        }

        /// <summary>
        /// A sample area with a small guid and split inner coordinates
        /// </summary>
        internal static Area SmallGuidAreaInnerCoordinatesSplit
        {
            get
            {
                return new Area(SmallGuid, "", SimpleWithTwoHoles, AreaCategory.Unknown,
                    SimpleWithTwoHoles.Area);
            }
        }

        /// <summary>
        /// A sample area with a big guid
        /// </summary>
        internal static Area BigGuidArea
        {
            get
            {
                return new Area(BigGuid, "", Simple, AreaCategory.Unknown, Simple.Area);
            }
        }

        /// <summary>
        /// A sample area with a big guid and inner coordinates
        /// </summary>
        internal static Area BigGuidAreaInnerCoordinates
        {
            get
            {
                return new Area(BigGuid, "", SimpleWithHole, AreaCategory.Unknown,
                    SimpleWithHole.Area);
            }
        }

        /// <summary>
        /// A sample area with a big guid and split inner coordinates
        /// </summary>
        internal static Area BigGuidAreaInnerCoordinatesSplit
        {
            get
            {
                return new Area(BigGuid, "", SimpleWithTwoHoles, AreaCategory.Unknown,
                    SimpleWithTwoHoles.Area);
            }
        }

        private static readonly Geodetic2d[] OuterCoordinates = 
        {
            new Geodetic2d(10, 10),
            new Geodetic2d(10, 0),
            new Geodetic2d(0, 0),
            new Geodetic2d(0, 10)
        };

        private static readonly Geodetic2d[] SingleHoleCoordinates = 
        {
            new Geodetic2d(5, 5),
            new Geodetic2d(5, 0),
            new Geodetic2d(0, 0),
            new Geodetic2d(0, 5)
        };

        private static readonly Geodetic2d[][] MultipleHoleCoordinates = 
        {
            new[]
            {
                new Geodetic2d(1, 1),
                new Geodetic2d(1, 0),
                new Geodetic2d(0, 0),
                new Geodetic2d(0, 1)
            },
            new[]
            {
                new Geodetic2d(2, 2),
                new Geodetic2d(2, 0),
                new Geodetic2d(0, 0),
                new Geodetic2d(0, 2)
            },
        };

        private static readonly GeodeticPolygon2d FirstHole = new GeodeticPolygon2d((IList<Geodetic2d>)SingleHoleCoordinates);
        private static readonly GeodeticPolygon2d SecondHole = new GeodeticPolygon2d((IList<Geodetic2d>)MultipleHoleCoordinates[0]);
        private static readonly GeodeticPolygon2d ThirdHole = new GeodeticPolygon2d((IList<Geodetic2d>)MultipleHoleCoordinates[1]);

        private static readonly GeodeticPolygon2d Simple = new GeodeticPolygon2d((IList<Geodetic2d>)OuterCoordinates);
        private static readonly GeodeticPolygon2d SimpleWithHole = new GeodeticPolygon2d(OuterCoordinates, new List<GeodeticPolygon2d>
        {
            FirstHole
        });
        private static readonly GeodeticPolygon2d SimpleWithTwoHoles = new GeodeticPolygon2d(OuterCoordinates, new List<GeodeticPolygon2d>
        {
            SecondHole, ThirdHole
        });

        /// <summary>
        /// Tests the constructor of the BinaryArea
        /// </summary>
        [Test]
        public override void TestConstructor()
        {
            var area = new Area(SmallGuid, "", Simple, AreaCategory.Unknown, 
                Simple.Area);
            var binaryArea = new BinaryArea(area, null);

            Assert.IsNotNull(binaryArea);

            area = new Area(SmallGuid, "", SimpleWithHole, AreaCategory.Unknown,
                SimpleWithHole.Area);
            binaryArea = new BinaryArea(area, null);

            Assert.IsNotNull(binaryArea);

            area = new Area(SmallGuid, "", SimpleWithTwoHoles, AreaCategory.Unknown,
                SimpleWithTwoHoles.Area);
            binaryArea = new BinaryArea(area, null);

            Assert.IsNotNull(binaryArea);
        }

        /// <summary>
        /// Tests the constructor of the BinaryArea when given invalid parameters
        /// </summary>
        [Test]
        public override void TestConstructorInvalidParameters()
        {
            Area area = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                var binaryArea = new BinaryArea(area, null);
            });
        }

        /// <summary>
        /// Tests the serialization of the BinaryArea when using a small guid
        /// </summary>
        [Test]
        public override void TestSerializationSmallGuid()
        {
            File.Delete(FullPath);

            var expectedArea = new Area(SmallGuid, "", Simple, AreaCategory.Unknown,
                Simple.Area);

            using (var file = File.Create(FullPath))
            {
                new BinaryArea(expectedArea, null).Serialize(file);
            }

            using (var file = File.OpenRead(FullPath))
            {
                // ensure we can deserialize to a BinaryFeature
                var binaryFeature = BinaryFeature.Deserialize(file);
                Assert.IsNotNull(binaryFeature);

                // ensure it can be casted to a BinaryArea
                var binaryArea = binaryFeature as BinaryArea;
                Assert.IsNotNull(binaryArea);

                // ensure that we get a Feature instance back from ToFeature
                var actualFeature = binaryFeature.ToFeature(null);
                Assert.IsNotNull(actualFeature);

                // ensure it can be casted to an Area
                var actualArea = actualFeature as Area;
                Assert.IsNotNull(actualArea);

                // ensure all details of the Area instance
                TestUtilities.AssertThatAreasAreEqual(expectedArea, actualArea);

                // confirm file size is what we expect
                Assert.AreEqual(59L, file.Length);
            }

            File.Delete(FullPath);
        }

        /// <summary>
        /// Tests the serialization of the BinaryArea when using a small guid
        /// </summary>
        [Test]
        public void TestSerializationSmallGuidInnerCoordinates()
        {
            File.Delete(FullPath);

            var expectedArea = new Area(SmallGuid, "", SimpleWithHole,
                AreaCategory.Unknown, SimpleWithHole.Area);

            using (var file = File.Create(FullPath))
            {
                new BinaryArea(expectedArea, null).Serialize(file);
            }

            using (var file = File.OpenRead(FullPath))
            {
                // ensure we can deserialize to a BinaryFeature
                var binaryFeature = BinaryFeature.Deserialize(file);
                Assert.IsNotNull(binaryFeature);

                // ensure it can be casted to a BinaryArea
                var binaryArea = binaryFeature as BinaryArea;
                Assert.IsNotNull(binaryArea);

                // ensure that we get a Feature instance back from ToFeature
                var actualFeature = binaryFeature.ToFeature(null);
                Assert.IsNotNull(actualFeature);

                // ensure it can be casted to an Area
                var actualArea = actualFeature as Area;
                Assert.IsNotNull(actualArea);

                // ensure all details of the Area instance
                TestUtilities.AssertThatAreasAreEqual(expectedArea, actualArea);

                // confirm file size is what we expect
                Assert.AreEqual(103L, file.Length);
            }

            File.Delete(FullPath);
        }

        /// <summary>
        /// Tests the serialization of the BinaryArea when using a small guid
        /// </summary>
        [Test]
        public void TestSerializationSmallGuidInnerSplitCoordinates()
        {
            File.Delete(FullPath);

            var expectedArea = new Area(SmallGuid, "", SimpleWithTwoHoles, 
                AreaCategory.Unknown, SimpleWithTwoHoles.Area);

            using (var file = File.Create(FullPath))
            {
                new BinaryArea(expectedArea, null).Serialize(file);
            }

            using (var file = File.OpenRead(FullPath))
            {
                // ensure we can deserialized to a BinaryFeature
                var binaryFeature = BinaryFeature.Deserialize(file);
                Assert.IsNotNull(binaryFeature);

                // ensure it can be casted to a BinaryArea
                var binaryArea = binaryFeature as BinaryArea;
                Assert.IsNotNull(binaryArea);

                // ensure that we get a Feature instance back from ToFeature
                var actualFeature = binaryFeature.ToFeature(null);
                Assert.IsNotNull(actualFeature);

                // ensure it can be casted to an Area
                var actualArea = actualFeature as Area;
                Assert.IsNotNull(actualArea);

                // ensure all details of the Area instance
                TestUtilities.AssertThatAreasAreEqual(expectedArea, actualArea);

                // confirm file size is what we expect
                Assert.AreEqual(150L, file.Length);
            }

            File.Delete(FullPath);
        }

        /// <summary>
        /// Tests the serialization of the BinaryArea when given a large guid
        /// </summary>
        [Test]
        public override void TestSerializationFullSizedGuid()
        {
            File.Delete(FullPath);

            var expectedArea = new Area(BigGuid, "", Simple, AreaCategory.Unknown,
                Simple.Area);

            using (var file = File.Create(FullPath))
            {
                new BinaryArea(expectedArea, null).Serialize(file);
            }

            using (var file = File.OpenRead(FullPath))
            {
                // ensure we can deserialized to a BinaryFeature
                var binaryFeature = BinaryFeature.Deserialize(file);
                Assert.IsNotNull(binaryFeature);

                // ensure it can be casted to a BinaryArea
                var binaryArea = binaryFeature as BinaryArea;
                Assert.IsNotNull(binaryArea);

                // ensure that we get a Feature instance back from ToFeature
                var actualFeature = binaryFeature.ToFeature(null);
                Assert.IsNotNull(actualFeature);

                // ensure it can be casted to an Area
                var actualArea = actualFeature as Area;
                Assert.IsNotNull(actualArea);

                // ensure all details of the Area instance
                TestUtilities.AssertThatAreasAreEqual(expectedArea, actualArea);

                // confirm file size is what we expect
                Assert.AreEqual(78L, file.Length);
            }

            File.Delete(FullPath);
        }
    }
}