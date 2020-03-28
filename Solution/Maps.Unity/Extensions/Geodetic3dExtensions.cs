using System;
using Maps.Geographical;
using Maps.Geometry;
using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Unity3d specific extensions for the Maps.Geographic.Geodetic3d class
    /// </summary>
    public static class Geodetic3dExtensions
    {
        /// <summary>
        /// Returns the Unity3d world position for a given 3d geodetic coordinate
        /// Note: conversion is lossy, consider using High + Low conversion
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate the position for</param>
        /// <param name="ellipsoid">The ellipsoid the coordinate is evaluated from</param>
        public static Vector3 WorldPosition(this Geodetic3d coordinate, Ellipsoid ellipsoid)
        {
            if (ellipsoid == null)
            {
                throw new ArgumentNullException(nameof(ellipsoid));
            }

            return ellipsoid.Wgs84Position3d(coordinate).xzy.Vector3();
        }

        /// <summary>
        /// Returns the Unity3d world position for a given 2d geodetic coordinate 
        /// in a High + Low format
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate the position for</param>
        /// <param name="ellipsoid">The ellipsoid the coordinate is evaluated from</param>
        /// <param name="high">The high portion of the Unity3d world position</param>
        /// <param name="low">The low portion of the Unity3d world position</param>
        public static void WorldPosition(this Geodetic3d coordinate, Ellipsoid ellipsoid,
                                         out Vector3 high, out Vector3 low)
        {
            if (ellipsoid == null)
            {
                throw new ArgumentNullException(nameof(ellipsoid));
            }

            var world = ellipsoid.Wgs84Position3d(coordinate).xzy;

            high = world.High.Vector3();
            low = world.Low.Vector3();
        }
    }
}