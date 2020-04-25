using System;
using System.IO;
using Maps.Geographical;
using Maps.Geographical.Features;
using Maps.IO.Features;
using NUnit.Framework;

namespace Maps.Tests.IO.Features
{
    /// <summary>
    /// A series of tests for the BinarySegment class
    /// </summary>
    [TestFixture]
    internal sealed class BinarySegmentTests : BinaryFeatureTests
    {
        /// <summary>
        /// Returns a segment with a small guid
        /// </summary>
        internal static Segment SmallGuidSegment
        {
            get
            {
                return new Segment(SmallGuid, Name,
                    Coordinates, SegmentCategory.Unknown);
            }
        }

        /// <summary>
        /// Returns a segment with a big guid
        /// </summary>
        internal static Segment BigGuidSegment
        {
            get
            {
                return new Segment(BigGuid, Name,
                    Coordinates, SegmentCategory.Unknown);
            }
        }

        private static readonly Geodetic2d[] Coordinates = new[]
        {
            Geodetic2d.SouthPole,
            Geodetic2d.NorthPole
        };

        private static readonly string Name = "";

        /// <summary>
        /// Tests the creation of a BinarySegment instance
        /// </summary>
        [Test]
        public override void TestConstructor()
        {
            var binarySegment = new BinarySegment(SmallGuidSegment, null);

            Assert.IsNotNull(binarySegment);
        }

        /// <summary>
        /// Tests the creation of a BinarySegment instance when given invalid
        /// parameters
        /// </summary>
        [Test]
        public override void TestConstructorInvalidParameters()
        {
            Segment segment = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                var binarySegment = new BinarySegment(segment, null);
            });
        }

        /// <summary>
        /// Tests that the BinarySegment can be serialized to disk and 
        /// deserialized back with a small sized guid
        /// </summary>
        [Test]
        public override void TestSerializationSmallGuid()
        {
            File.Delete(FullPath);

            var expectedSegment = SmallGuidSegment;

            using (var file = File.Create(FullPath))
            {
                new BinarySegment(expectedSegment, null).Serialize(file);
            }

            using (var file = File.OpenRead(FullPath))
            {
                // ensure we can deserialize to a BinaryFeature
                var binaryFeature = BinaryFeature.Deserialize(file);
                Assert.IsNotNull(binaryFeature);

                // ensure it can be casted to a BinarySegment
                var binarySegment = binaryFeature as BinarySegment;
                Assert.IsNotNull(binarySegment);

                // ensure we get a Feature instance back from ToFeature
                var actualFeature = binaryFeature.ToFeature(null);
                Assert.IsNotNull(actualFeature);

                // ensure it can be casted to a Segment
                var actualSegment = actualFeature as Segment;
                Assert.IsNotNull(actualSegment);

                // ensure all details of the Segment instance
                Assert.AreEqual(expectedSegment.Guid, actualSegment.Guid);
                Assert.AreEqual(expectedSegment.Name, actualSegment.Name);
                Assert.AreEqual(expectedSegment.LineStrip.Count,
                    actualSegment.LineStrip.Count);

                for (var i = 0; i < Coordinates.Length; i++)
                {
                    TestUtilities.AssertThatGeodetic2dsAreEqual(
                        expectedSegment.LineStrip[i],
                        actualSegment.LineStrip[i]);
                }

                // confirm file size is what we expect
                Assert.AreEqual(30L, file.Length);
            }

            File.Delete(FullPath);
        }

        /// <summary>
        /// Tests that the BinarySegment can be serialized to disk and 
        /// deserialized back with a full sized guid
        /// </summary>
        [Test]
        public override void TestSerializationFullSizedGuid()
        {
            File.Delete(FullPath);

            var expectedSegment = BigGuidSegment;

            using (var file = File.Create(FullPath))
            {
                new BinarySegment(expectedSegment, null).Serialize(file);
            }

            using (var file = File.OpenRead(FullPath))
            {
                // ensure we can deserialize to a BinaryFeature
                var binaryFeature = BinaryFeature.Deserialize(file);
                Assert.IsNotNull(binaryFeature);

                // ensure it can be casted to a BinarySegment
                var binarySegment = binaryFeature as BinarySegment;
                Assert.IsNotNull(binarySegment);

                // ensure we get a Feature instance back from ToFeature
                var actualFeature = binaryFeature.ToFeature(null);
                Assert.IsNotNull(actualFeature);

                // ensure it can be casted to a Segment
                var actualSegment = actualFeature as Segment;
                Assert.IsNotNull(actualSegment);

                // ensure all details of the Segment instance
                Assert.AreEqual(expectedSegment.Guid, actualSegment.Guid);
                Assert.AreEqual(expectedSegment.Name, actualSegment.Name);
                Assert.AreEqual(expectedSegment.LineStrip.Count,
                    actualSegment.LineStrip.Count);

                for (var i = 0; i < Coordinates.Length; i++)
                {
                    TestUtilities.AssertThatGeodetic2dsAreEqual(
                        expectedSegment.LineStrip[i], 
                        actualSegment.LineStrip[i]);
                }

                // confirm file size is what we expect
                Assert.AreEqual(49L, file.Length);
            }

            File.Delete(FullPath);
        }
    }
}