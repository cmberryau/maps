using System;
using Maps.Geometry;
using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Extensions for the Maps.Geometry.Topology enum
    /// </summary>
    public static class TopologyExtensions
    {
        /// <summary>
        /// Returns the UnityEngine.MeshTopology for the given topology
        /// </summary>
        /// <param name="topology">The topology to evaluate</param>
        public static MeshTopology UnityTopology(this Topology topology)
        {
            switch(topology)
            {
                case Topology.Triangles:
                    return MeshTopology.Triangles;

                case Topology.Quads:
                    return MeshTopology.Quads;

                case Topology.Lines:
                    return MeshTopology.Lines;

                case Topology.LineStrip:
                    return MeshTopology.LineStrip;

                case Topology.Points:
                    return MeshTopology.Points;

                default:
                    throw new ArgumentOutOfRangeException(nameof(topology),
                        topology, null);
            }
        }
    }
}