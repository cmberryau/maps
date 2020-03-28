using System;
using Maps.Extensions;
using NUnit.Framework;

namespace Maps.Tests.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    internal sealed class GuidExtensionsTests
    {
        /// <summary>
        /// Tests the ability to convert a Guid to a pair of longs and back
        /// </summary>
        [Test]
        public void TestTwinLongPairConversion()
        {
            var guid = Guid.NewGuid();

            var a = guid.ToLong();
            var b = guid.ToLong(8);

            var reguid = GuidExtensions.ToGuid(a, b);

            Assert.AreEqual(guid, reguid);
        }
    }
}