using System;
using Maps.Geographical;
using Maps.Geographical.Features;
using NUnit.Framework;

namespace Maps.Tests.Geographical.Features
{
    /// <summary>
    /// Series of tests for the segment class
    /// </summary>
    [TestFixture]
    internal sealed class SegmentTests
    {
        /// <summary>
        /// Tests the ConnectionsTo method
        /// </summary>
        [Test]
        public void TestConnectionsToMethod()
        {
            var c = Geodetic2d.Meridian;

            var coords = new[]
            {
                c,
                Geodetic2d.Offset(c, 10, (double) CardinalDirection.North),
            };

            var a = new Segment(Guid.Empty, "", coords, SegmentCategory.Unknown);

            coords = new[]
            {
                c,
                Geodetic2d.Offset(c, 10, (double) CardinalDirection.South),
            };

            var b = new Segment(Guid.Empty, "", coords, SegmentCategory.Unknown);

            var connections = a.ConnectionTo(b);

            Assert.IsNotNull(connections);
            Assert.IsNotEmpty(connections);
            Assert.AreEqual(a.LineStrip.Count, connections.Count);
            Assert.AreEqual(b.LineStrip.Count, connections.Count);

            Assert.IsTrue(connections[0]);
            Assert.IsFalse(connections[1]);

            coords = new[]
            {
                c,
                Geodetic2d.Offset(c, 10, (double) CardinalDirection.North),
            };

            a = new Segment(Guid.Empty, "", coords, SegmentCategory.Unknown);

            coords = new[]
            {
                c,
                Geodetic2d.Offset(c, 10, (double) CardinalDirection.North),
            };

            b = new Segment(Guid.Empty, "", coords, SegmentCategory.Unknown);

            connections = a.ConnectionTo(b);

            Assert.IsNotNull(connections);
            Assert.IsNotEmpty(connections);
            Assert.AreEqual(a.LineStrip.Count, connections.Count);
            Assert.AreEqual(b.LineStrip.Count, connections.Count);

            Assert.IsTrue(connections[0]);
            Assert.IsTrue(connections[1]);
        }
    }
}