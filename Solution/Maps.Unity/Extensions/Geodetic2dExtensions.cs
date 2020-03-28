using System;
using Maps.Geographical;
using Maps.Geometry;
using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Unity3d specific extensions for the Maps.Geographic.Geodetic3d class
    /// </summary>
    public static class Geodetic2dExtensions
    {
        /// <summary>
        /// Returns the Unity3d world position for a given 2d geodetic coordinate
        /// Note: conversion is lossy, consider using High + Low conversion
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate the position for</param>
        /// <param name="ellipsoid">The ellipsoid the coordinate is evaluated from</param>
        public static Vector3 WorldPosition(this Geodetic2d coordinate, Ellipsoid ellipsoid)
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
        public static void WorldPosition(this Geodetic2d coordinate, Ellipsoid ellipsoid,
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

        /// <summary>
        /// Returns the Unity3d world positions for a given array of 
        /// 2d geodetic coordinates in a High + Low format
        /// </summary>
        /// <param name="coordinates">The coordinate to evaluate the position for</param>
        /// <param name="ellipsoid">The ellipsoid the coordinate is evaluated from</param>
        /// <param name="highPositions"></param>
        /// <param name="lowPositions"></param>
        public static void WorldPositions(this Geodetic2d[] coordinates, Ellipsoid ellipsoid,
                                          out Vector3[] highPositions, out Vector3[] lowPositions)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            if (ellipsoid == null)
            {
                throw new ArgumentNullException(nameof(ellipsoid));
            }

            Vector3 high;
            Vector3 low;

            highPositions = new Vector3[coordinates.Length];
            lowPositions = new Vector3[coordinates.Length];

            // TODO: possible SIMD optimisation
            for (var i = 0; i < coordinates.Length; ++i)
            {
                coordinates[i].WorldPosition(ellipsoid, out high, out low);

                highPositions[i] = high;
                lowPositions[i] = low;
            }
        }
    }
}