using System;
using Maps.Geographical;
using Maps.Geometry;
using UnityEngine;

namespace Maps.Unity.Extensions
{
    /// <summary>
    /// Maps specific extensions for the UnityEngine.Vector3 class
    /// </summary>
    public static class Vector3Extensions
    {
        /// <summary>
        /// Performs a conversion from UnityEngine.Vector3 to Maps.Vector3d
        /// </summary>
        /// <param name="a">The vector to convert</param>
        public static Vector3d Vector3d(this Vector3 a)
        {
            return new Vector3d(a.x, a.y, a.z);
        }

        /// <summary>
        /// Returns a geodetic surface normal for the given Unity3d world position
        /// </summary>
        /// <param name="unityWorldPosition">The Unity3d world position to evaluate</param>
        /// <param name="ellipsoid">The ellipsoid for evaluate the position on</param>
        public static Vector3 GeodeticSurfaceNormal(this Vector3 unityWorldPosition, Ellipsoid ellipsoid)
        {
            if (ellipsoid == null)
            {
                throw new ArgumentNullException(nameof(ellipsoid));
            }

            return ellipsoid.GeodeticSurfaceNormal(unityWorldPosition.Vector3d().xzy).xzy.Vector3();
        }

        /// <summary>
        /// Returns a geocentrically projected point on the ellipsoids surface
        /// </summary>
        /// <param name="unityWorldPosition">The Unity3d world position to evaluate</param>
        /// <param name="ellipsoid">The ellipsoid for evaluate the position on</param>
        public static Vector3 ScaleToGeocentricSurface(this Vector3 unityWorldPosition, Ellipsoid ellipsoid)
        {
            return ellipsoid.ScaleToGeocentricSurface(unityWorldPosition.Vector3d().xzy).xzy.Vector3();
        }

        /// <summary>
        /// Returns a geocentrically projected point on the ellipsoids surface
        /// </summary>
        /// <param name="unityWorldPosition">The Unity3d world position to evaluate</param>
        /// <param name="ellipsoid">The ellipsoid for evaluate the position on</param>
        public static Vector3 ScaleToGeodeticSurface(this Vector3 unityWorldPosition, Ellipsoid ellipsoid)
        {
            return ellipsoid.ScaleToGeodeticSurface(unityWorldPosition.Vector3d().xzy).xzy.Vector3();
        }

        /// <summary>
        /// Returns a WGS84 conforming 2d coordinate for the given Unity3d
        /// world position.
        /// </summary>
        /// <param name="unityWorldPosition">The Unity3d world position to evaluate</param>
        /// <param name="ellipsoid">The ellipsoid for evaluate the position on</param>
        public static Geodetic2d Wgs84Coordinate2d(this Vector3 unityWorldPosition, Ellipsoid ellipsoid)
        {
            if (ellipsoid == null)
            {
                throw new ArgumentNullException(nameof(ellipsoid));
            }

            return ellipsoid.Wgs84Coordinate2d(unityWorldPosition.Vector3d().xzy);
        }

        /// <summary>
        /// Returns a WGS84 conforming 3d coordinate for the given Unity3d
        /// world position.
        /// </summary>
        /// <param name="unityWorldPosition">The Unity3d world position to evaluate</param>
        /// <param name="ellipsoid">The ellipsoid for evaluate the position on</param>
        public static Geodetic3d Wgs84Coordinate3d(this Vector3 unityWorldPosition, Ellipsoid ellipsoid)
        {
            if (ellipsoid == null)
            {
                throw new ArgumentNullException(nameof(ellipsoid));
            }

            return ellipsoid.Wgs84Coordinate3d(unityWorldPosition.Vector3d().xzy);
        }
    }
}