using System;
using Maps.Extensions;
using NUnit.Framework;

namespace Maps.Tests.IO.Features
{
    /// <summary>
    /// Series of required tests for BinaryFeature testers
    /// </summary>
    [TestFixture]
    public abstract class BinaryFeatureTests
    {
        /// <summary>
        /// The full disk path to write the BinaryFeature to
        /// </summary>
        protected static readonly string FullPath = TestUtilities.WorkingDirectory + "BinaryFeature.bin";

        /// <summary>
        /// A small sized guid
        /// </summary>
        internal static readonly Guid SmallGuid = 123L.ToGuid();

        /// <summary>
        /// A large sized guid
        /// </summary>
        internal static readonly Guid BigGuid = new Guid("dee7823b-dd67-4b19-b075-df46c6613ec2");

        /// <summary>
        /// Tests the constructor of the BinaryFeature
        /// </summary>
        [Test]
        public abstract void TestConstructor();

        /// <summary>
        /// Tests the constructor of the BinaryFeature when given invalid parameters
        /// </summary>
        [Test]
        public abstract void TestConstructorInvalidParameters();

        /// <summary>
        /// Tests the serialization of the BinaryFeature when using a small guid
        /// </summary>
        [Test]
        public abstract void TestSerializationSmallGuid();

        /// <summary>
        /// Tests the serialization of the BinaryFeature when given a large guid
        /// </summary>
        [Test]
        public abstract void TestSerializationFullSizedGuid();
    }
}