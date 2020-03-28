using System.IO;
using Maps.Geographical;
using Maps.IO.Geographical;
using NUnit.Framework;
using ProtoBuf;

namespace Maps.Tests.IO
{
    /// <summary>
    ///  A series of tests for the BinaryCoordinate class
    /// </summary>
    [TestFixture]
    internal sealed class BinaryCoordinateTests
    {
        private static string FullPath => TestUtilities.WorkingDirectory + "BinaryCoordinate.bin";

        /// <summary>
        /// Tests the BinaryCoordinate constructor with double params
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var latitude = 48.7641175932571;
            var longitude = 11.4209832498872;

            var BinaryCoordinate = new BinaryCoordinate(latitude, longitude);

            TestUtilities.AssertThatDoublesAreEqual(latitude, BinaryCoordinate.Latitude);
            TestUtilities.AssertThatDoublesAreEqual(longitude, BinaryCoordinate.Longitude);
        }

        /// <summary>
        /// Tests the BinaryCoordinate constructor with a Geodetic2d param
        /// </summary>
        [Test]
        public void TestConstructorGeodetic2dParameter()
        {
            var latitude = 48.7641175932571;
            var longitude = 11.4209832498872;

            var BinaryCoordinate = new BinaryCoordinate(new Geodetic2d(latitude, longitude));

            TestUtilities.AssertThatDoublesAreEqual(latitude, BinaryCoordinate.Latitude);
            TestUtilities.AssertThatDoublesAreEqual(longitude, BinaryCoordinate.Longitude);
        }

        /// <summary>
        /// Tests that the BinaryCoordinate can be written to disk and read back
        /// </summary>
        [Test]
        public void TestWriteRead()
        {
            File.Delete(FullPath);

            var latitude = BinaryCoordinate.MaxValue;
            var longitude = BinaryCoordinate.MinValue;

            using (var file = File.Create(FullPath))
            {
                var BinaryCoordinate = new BinaryCoordinate(latitude, longitude);

                Serializer.Serialize(file, BinaryCoordinate);
            }

            using (var file = File.OpenRead(FullPath))
            {
                var BinaryCoordinate = Serializer.Deserialize<BinaryCoordinate>(file);

                Assert.AreEqual(latitude, BinaryCoordinate.Latitude);
                Assert.AreEqual(longitude, BinaryCoordinate.Longitude);
            }
        }

        /// <summary>
        /// Tests the ability to store potential coordinate double
        /// precision floating point values accurately
        /// </summary>
        [Test]
        public void TestCoordinateDoubleFloatStorage()
        {
            File.Delete(FullPath);

            var latitude = 48.7641175932571;
            var longitude = 11.4209832498872;

            using (var file = File.Create(FullPath))
            {
                var BinaryCoordinate = new BinaryCoordinate(latitude, longitude);

                Serializer.Serialize(file, BinaryCoordinate);
            }

            using (var file = File.OpenRead(FullPath))
            {
                var BinaryCoordinate = Serializer.Deserialize<BinaryCoordinate>(file);

                TestUtilities.AssertThatDoublesAreEqual(latitude, BinaryCoordinate.Latitude);
                TestUtilities.AssertThatDoublesAreEqual(longitude, BinaryCoordinate.Longitude);
            }

            var resolution = 512;
            var latincre = 180 / (double)resolution;
            var lonincre = 90 / (double)resolution;

            latitude = -180;
            longitude = -90;

            for (var i = -resolution; i < resolution + 1; i++)
            {
                File.Delete(FullPath);

                using (var file = File.Create(FullPath))
                {
                    var BinaryCoordinate = new BinaryCoordinate(latitude, longitude);

                    Serializer.Serialize(file, BinaryCoordinate);
                }

                using (var file = File.OpenRead(FullPath))
                {
                    var BinaryCoordinate = Serializer.Deserialize<BinaryCoordinate>(file);

                    TestUtilities.AssertThatDoublesAreEqual(latitude, BinaryCoordinate.Latitude);
                    TestUtilities.AssertThatDoublesAreEqual(longitude, BinaryCoordinate.Longitude);
                }

                latitude += latincre;
                longitude += lonincre;
            }

            Assert.AreEqual(180 + latincre, latitude);
            Assert.AreEqual(90 + lonincre, longitude);
        }

        /// <summary>
        /// Tests the byte size variance for small magnitude values
        /// </summary>
        [Test]
        public void TestSizeVarianceSmall()
        {
            File.Delete(FullPath);

            var latitude = 1;
            var longitude = 2;

            using (var file = File.Create(FullPath))
            {
                var BinaryCoordinate = new BinaryCoordinate(latitude, longitude);

                Serializer.Serialize(file, BinaryCoordinate);
            }

            using (var file = File.OpenRead(FullPath))
            {
                Assert.AreEqual(18, file.Length);

                var BinaryCoordinate = Serializer.Deserialize<BinaryCoordinate>(file);

                Assert.AreEqual(latitude, BinaryCoordinate.Latitude);
                Assert.AreEqual(longitude, BinaryCoordinate.Longitude);
            }
        }

        /// <summary>
        /// Tests the byte size variance for max magnitude values
        /// </summary>
        [Test]
        public void TestSizeVarianceLarge()
        {
            var latitude = BinaryCoordinate.MaxValue;
            var longitude = BinaryCoordinate.MinValue;

            File.Delete(FullPath);

            using (var file = File.Create(FullPath))
            {
                var BinaryCoordinate = new BinaryCoordinate(latitude, longitude);

                Serializer.Serialize(file, BinaryCoordinate);
            }

            using (var file = File.OpenRead(FullPath))
            {
                Assert.AreEqual(18, file.Length);
            }
        }

        /// <summary>
        /// Tests the byte size variance for tiny magnitude floating point numbers
        /// in the context of geographical coordinates
        /// </summary>
        [Test]
        public void TestSizeVarianceCoordinateTiny()
        {
            File.Delete(FullPath);

            var latitude = 0.000000000000001d;
            var longitude = -0.000000000000001d;

            using (var file = File.Create(FullPath))
            {
                var BinaryCoordinate = new BinaryCoordinate(latitude, longitude);

                Serializer.Serialize(file, BinaryCoordinate);
            }

            using (var file = File.OpenRead(FullPath))
            {
                Assert.AreEqual(18, file.Length);

                var BinaryCoordinate = Serializer.Deserialize<BinaryCoordinate>(file);

                TestUtilities.AssertThatDoublesAreEqual(latitude, BinaryCoordinate.Latitude);
                TestUtilities.AssertThatDoublesAreEqual(longitude, BinaryCoordinate.Longitude);
            }
        }

        /// <summary>
        /// Tests the byte size variance for medium magnitude floating point numbers
        /// in the context of geographical coordinates
        /// </summary>
        [Test]
        public void TestSizeVarianceCoordinateMedium()
        {
            File.Delete(FullPath);

            var latitude = 1d;
            var longitude = -0.5d;

            using (var file = File.Create(FullPath))
            {
                var BinaryCoordinate = new BinaryCoordinate(latitude, longitude);

                Serializer.Serialize(file, BinaryCoordinate);
            }

            using (var file = File.OpenRead(FullPath))
            {
                Assert.AreEqual(18, file.Length);

                var BinaryCoordinate = Serializer.Deserialize<BinaryCoordinate>(file);

                TestUtilities.AssertThatDoublesAreEqual(latitude, BinaryCoordinate.Latitude);
                TestUtilities.AssertThatDoublesAreEqual(longitude, BinaryCoordinate.Longitude);
            }
        }

        /// <summary>
        /// Tests the byte size variance for max magnitude floating point numbers
        /// in the context of geographical coordinates
        /// </summary>
        [Test]
        public void TestSizeVarianceCoordinateMax()
        {
            File.Delete(FullPath);

            var latitude = 180.0000d;
            var longitude = -90.0000d;

            using (var file = File.Create(FullPath))
            {
                var BinaryCoordinate = new BinaryCoordinate(latitude, longitude);

                Serializer.Serialize(file, BinaryCoordinate);
            }

            using (var file = File.OpenRead(FullPath))
            {
                Assert.AreEqual(18, file.Length);

                var BinaryCoordinate = Serializer.Deserialize<BinaryCoordinate>(file);

                TestUtilities.AssertThatDoublesAreEqual(latitude, BinaryCoordinate.Latitude);
                TestUtilities.AssertThatDoublesAreEqual(longitude, BinaryCoordinate.Longitude);
            }
        }
    }
}