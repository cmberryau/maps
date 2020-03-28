using System;
using Maps.Geographical;

namespace Maps.Geometry
{
    /// <summary>
    /// Represents an ellipsoid with it's centre at the origin
    /// </summary>
    public sealed class Ellipsoid
    {
        /// <summary>
        /// The earth's ellipsoid as defined by the WGS84 standard
        /// 
        /// Note: WGS84 defines it's coordinates as Z UP, you may need to flip
        /// Z and Y coordinates if the target engine is defined as Y up
        /// </summary>
        public static readonly Ellipsoid Wgs84 =
            new Ellipsoid(new Vector3d(Mathd.RMajor, Mathd.RMajor, Mathd.RMinor));

        /// <summary>
        /// The normalised earth's ellipsoid as defined by the WGS84 standard
        /// 
        /// Note: WGS84 defines it's coordinates as Z UP, you may need to flip
        /// Z and Y coordinates if the target engine is defined as Y up
        /// </summary>
        public static readonly Ellipsoid NormalisedWgs84 = 
            new Ellipsoid(new Vector3d(1d, 1d, Mathd.RMinor / Mathd.RMajor));

        /// <summary>
        /// The normalised earth's ellipsoidal radii as defined by the WGS84 standard
        /// 
        /// Note: WGS84 defines it's coordinates as Z UP, you may need to flip
        /// Z and Y coordinates if the target engine is defined as Y up
        /// </summary>
        public static readonly Vector3d NormalisedWgs84Radii =
            new Vector3d(1d, 1d, Mathd.RMinor / Mathd.RMajor);

        /// <summary>
        /// Unit sphere for testing
        /// </summary>
        public static readonly Ellipsoid UnitSphere =
            new Ellipsoid(new Vector3d(1d, 1d, 1d));

        /// <summary>
        /// The radii of the Ellipsoid
        /// </summary>
        public readonly Vector3d Radii;

        /// <summary>
        /// Precomputed square of the Ellipsoid's radii
        /// </summary>
        public readonly Vector3d RadiiSquared;

        /// <summary>
        /// Precomputed radii^4
        /// </summary>
        public readonly Vector3d RadiiToTheFourth;

        /// <summary>
        /// Precomputed one over the Ellipsoid's radii
        /// </summary>
        public readonly Vector3d OneOverRadii;

        /// <summary>
        /// Precomputed one over the square of the Ellipsoid's radii
        /// </summary>
        public readonly Vector3d OneOverRadiiSquared;

        /// <summary>
        /// The scale of this Ellipsoid to meters (if an Earth ellipsoid)
        /// </summary>
        public readonly double Scale;

        /// <summary>
        /// Initializes a new instance of Ellipsoid
        /// </summary>
        /// <param name="radii">The radii of the new Ellipsoid</param>
        public Ellipsoid(Vector3d radii)
        {
            Radii = radii;
            RadiiSquared = Vector3d.Pow(radii, 2d);
            RadiiToTheFourth = Vector3d.Pow(radii, 4d);
            OneOverRadii = 1 / Radii;
            OneOverRadiiSquared = 1 / RadiiSquared;            

            // abitrary scaled earth ellipsoid defined by x == y && x / z = RMajor / RMinor
            if (radii.x == radii.y &&
                (radii.x / radii.z == Mathd.RMajor / Mathd.RMinor))
            {
                Scale = radii.x / Mathd.RMajor;
            }
            else
            {
                Scale = 1d;
            }
        }

        /// <summary>
        /// Returns a geodetic surface normal for the given 3d WGS84 position
        /// 
        /// Note: WGS84 defines it's coordinates as Z UP, for a position coming
        /// from a Y up engine, please ensure to swap Y and Z components, and likewise
        /// for the returned normal.
        /// 
        /// See "3d Engine Design for Virtual Globes" page 26
        /// </summary>
        /// <param name="wgsPosition3d">The 3d WGS84 conforming position to evaluate from</param>
        /// <returns></returns>
        public Vector3d GeodeticSurfaceNormal(Vector3d wgsPosition3d)
        {
            return Vector3d.ComponentMultiply(wgsPosition3d, OneOverRadiiSquared).Normalised;
        }

        /// <summary>
        /// Returns a geodetic surface normal for the given geodetic 2d coordinate
        /// 
        /// Note: WGS84 defines it's coordinates as Z UP, for a Y UP target engine
        /// please ensure to swap the Y and Z components for the returned normal.
        /// 
        /// See "3d Engine Design for Virtual Globes" page 23
        /// </summary>
        /// <param name="wgs84Coordinate2d">The 2d WGS84 conforming geodetic coordinate to evaluate</param>
        public static Vector3d GeodeticSurfaceNormal(Geodetic2d wgs84Coordinate2d)
        {
            var cosLatitude = Math.Cos(wgs84Coordinate2d.Latitude * Mathd.Deg2Rad);

            return new Vector3d(cosLatitude * Math.Cos(wgs84Coordinate2d.Longitude * Mathd.Deg2Rad),
                cosLatitude * Math.Sin(wgs84Coordinate2d.Longitude * Mathd.Deg2Rad),
                Math.Sin(wgs84Coordinate2d.Latitude * Mathd.Deg2Rad));
        }

        /// <summary>
        /// Returns a geodetic surface normal for the given geodetic 3d coordinate
        /// 
        /// Note: WGS84 defines it's coordinates as Z UP, for a Y UP target engine
        /// please ensure to swap the Y and Z components for the returned normal.
        /// 
        /// See "3d Engine Design for Virtual Globes" page 23
        /// </summary>
        /// <param name="wgs84Coordinate3d">The 2d WGS84 conforming geodetic coordinate to evaluate</param>
        /// <returns></returns>
        public static Vector3d GeodeticSurfaceNormal(Geodetic3d wgs84Coordinate3d)
        {
            var cosLatitude = Math.Cos(wgs84Coordinate3d.Latitude * Mathd.Deg2Rad);

            return new Vector3d(cosLatitude * Math.Cos(wgs84Coordinate3d.Longitude * Mathd.Deg2Rad),
                cosLatitude * Math.Sin(wgs84Coordinate3d.Longitude * Mathd.Deg2Rad),
                Math.Sin(wgs84Coordinate3d.Latitude * Mathd.Deg2Rad));
        }

        /// <summary>
        /// Returns a geocentrically projected WGS84 3d position on the ellipsoids surface
        /// 
        /// Note: WGS84 defines it's coordinates as Z UP, for a position coming
        /// from a Y up engine, please ensure to swap Y and Z components, and likewise
        /// for the returned position.
        /// 
        /// See "3d Engine Design for Virtual Globes" page 27
        /// </summary>
        /// <param name="wgs84Position3d">The 3d WGS84 conforming position to evaluate from</param>
        /// <returns></returns>
        public Vector3d ScaleToGeocentricSurface(Vector3d wgs84Position3d)
        {
            var beta = 1d / Math.Sqrt(Vector3d.Dot(Vector3d.Pow(wgs84Position3d, 2d),
                OneOverRadiiSquared));

            return beta * wgs84Position3d;
        }

        /// <summary>
        /// Returns a geodetically projected WGS84 3d position on the ellipsoids surface
        /// 
        /// Note: WGS84 defines it's coordinates as Z UP, for a position coming
        /// from a Y up engine, please ensure to swap Y and Z components, and likewise
        /// for the returned position.
        /// 
        /// See "3d Engine Design for Virtual Globes" page 27-34
        /// </summary>
        /// <param name="wgs84Position3d">The 3d WGS84 conforming position to evaluate from</param>
        /// <returns></returns>
        public Vector3d ScaleToGeodeticSurface(Vector3d wgs84Position3d)
        {
            var beta = 1d / Math.Sqrt(Vector3d.Dot(Vector3d.Pow(wgs84Position3d, 2d),
                OneOverRadiiSquared));

            var norm = new Vector3d(beta * wgs84Position3d.x * OneOverRadiiSquared.x,
                beta * wgs84Position3d.y * OneOverRadiiSquared.y,
                beta * wgs84Position3d.z * OneOverRadiiSquared.z).Magnitude;

            var alpha = (1d - beta) * (wgs84Position3d.Magnitude / norm);

            var x2 = wgs84Position3d.x * wgs84Position3d.x;
            var y2 = wgs84Position3d.y * wgs84Position3d.y;
            var z2 = wgs84Position3d.z * wgs84Position3d.z;

            var da = 0d;
            var db = 0d;
            var dc = 0d;

            var s = 0d;
            var dSdA = 1d;

            do
            {
                alpha -= s / dSdA;

                da = 1.0 + alpha * OneOverRadiiSquared.x;
                db = 1.0 + alpha * OneOverRadiiSquared.y;
                dc = 1.0 + alpha * OneOverRadiiSquared.z;

                var da2 = da * da;
                var db2 = db * db;
                var dc2 = dc * dc;

                var da3 = da * da2;
                var db3 = db * db2;
                var dc3 = dc * dc2;

                s = x2 / (RadiiSquared.x * da2) +
                    y2 / (RadiiSquared.y * db2) +
                    z2 / (RadiiSquared.z * dc2) - 1.0;

                dSdA = -2.0 *
                       (x2 / (RadiiToTheFourth.x * da3) +
                        y2 / (RadiiToTheFourth.y * db3) +
                        z2 / (RadiiToTheFourth.z * dc3));

            } while (Math.Abs(s) > Mathd.EpsilonE10);

            return new Vector3d(wgs84Position3d.x / da,
                wgs84Position3d.y / db,
                wgs84Position3d.z / dc);
        }

        /// <summary>
        /// Returns a WGS84 3d position for the given geodetic 2d coordinate,
        /// point will always rest on ellipsoid surface due to being 2d coordinate
        /// 
        /// Note: WGS84 defines it's coordinates as Z UP, for a Y UP target engine
        /// please ensure to swap the Y and Z components for the returned position.
        /// 
        /// See "3d Engine Design for Virtual Globes" page 24
        /// </summary>
        /// <param name="wgs84Coordinate2d">The 2d WGS84 conforming geodetic coordinate to evaluate</param>
        /// <returns></returns>
        public Vector3d Wgs84Position3d(Geodetic2d wgs84Coordinate2d)
        {
            var norm = GeodeticSurfaceNormal(wgs84Coordinate2d);
            var k = Vector3d.ComponentMultiply(RadiiSquared, norm);
            var gamma = Math.Sqrt(Vector3d.Dot(norm, k));

            return k / gamma;
        }

        /// <summary>
        /// Returns a WGS84 3d position for the given geodetic 3d coordinate
        /// 
        /// Note: WGS84 defines it's coordinates as Z UP, for a Y UP target engine
        /// please ensure to swap the Y and Z components for the returned position.
        /// 
        /// See "3d Engine Design for Virtual Globes" page 24
        /// </summary>
        /// <param name="wgs84Coordinate3d">The 3d WGS84 conforming geodetic coordinate to evaluate</param>
        /// <returns></returns>
        public Vector3d Wgs84Position3d(Geodetic3d wgs84Coordinate3d)
        {
            var norm = GeodeticSurfaceNormal(wgs84Coordinate3d);
            var k = Vector3d.ComponentMultiply(RadiiSquared, norm);
            var gamma = Math.Sqrt(Vector3d.Dot(norm, k));

            var surfacePoint = k / gamma;

            return surfacePoint + wgs84Coordinate3d.Height * Scale * norm;
        }

        /// <summary>
        /// Returns a WGS84 conforming 2d coordinate for the given WGS84 conforming
        /// geodetic coordinate.
        /// 
        /// Note: WGS84 defines it's coordinates as Z UP, for a position coming
        /// from a Y up engine, please ensure to swap Y and Z components.
        /// 
        /// See "3d Engine Design for Virtual Globes" page 26
        /// </summary>
        /// <param name="wgs84Position3d">The 3d WGS84 conforming position to evaluate from</param>
        /// <returns></returns>
        public Geodetic2d Wgs84Coordinate2d(Vector3d wgs84Position3d)
        {
            // using the definition of a surface normal on an ellipsoid
            var norm = GeodeticSurfaceNormal(wgs84Position3d);

            return new Geodetic2d(Math.Asin(norm.z / norm.Magnitude) * Mathd.Rad2Deg,
                Math.Atan2(norm.y, norm.x) * Mathd.Rad2Deg);
        }

        /// <summary>
        /// Returns a WGS84 geodetic 3d coordinate for the given WGS84 conforming
        /// position.
        /// 
        /// Note: WGS84 defines it's coordinates as Z UP, for a position coming
        /// from a Y up engine, please ensure to swap Y and Z components.
        /// 
        /// See "3d Engine Design for Virtual Globes" pages 27-34
        /// </summary>
        /// <param name="wgs84Position3d">The 3d WGS84 conforming position to evaluate from</param>
        public Geodetic3d Wgs84Coordinate3d(Vector3d wgs84Position3d)
        {
            // geodetically projected point
            var geodeticPoint = ScaleToGeodeticSurface(wgs84Position3d);

            // height vector from geodetically projected point to point passed in
            var heightVector = wgs84Position3d - geodeticPoint;

            // signed height above the surface of the ellipsoid
            var height = Math.Sign(Vector3d.Dot(heightVector, wgs84Position3d)) *
                         heightVector.Magnitude;

            return new Geodetic3d(Wgs84Coordinate2d(geodeticPoint), height * 1d / Scale);
        }

        /// <summary>
        /// Returns an array of WGS84 conforming 3d coordinates that represent a 
        /// curve on the ellipsoid between the two given points
        /// 
        /// Note: WGS84 defines it's coordinates as Z UP, for positions coming
        /// from a Y up engine, please ensure to swap Y and Z components and the
        /// same for the returned positions.
        /// </summary>
        /// <param name="a">The start of the curve</param>
        /// <param name="b">The end of the curve</param>
        /// <param name="granularityDegrees">The angle between each point 
        /// on the curve</param>
        /// <returns></returns>
        public Vector3d[] CurveBetween(Vector3d a, Vector3d b, 
            double granularityDegrees)
        {
            // get the angle between the two vectors
            var angleBetween = Vector3d.Angle(a, b);

            // early exit cases
            if (angleBetween < Mathd.Epsilon)
            {
                return new[] { ScaleToGeocentricSurface(a) };
            }

            if (granularityDegrees >= angleBetween)
            {
                return new[]
                {
                    ScaleToGeocentricSurface(a),
                    ScaleToGeocentricSurface(b)
                };
            }

            // force granularity  to be positive
            granularityDegrees = Math.Abs(granularityDegrees);

            // check for small values to avoid number overflows
            if (granularityDegrees < Mathd.EpsilonE4)
            {
                granularityDegrees = Mathd.EpsilonE4;
            }

            // get a vector perpendicular to the plane the two point vectors lie on
            var planeAxis = Vector3d.Cross(a, b);

            // two point vectors are anti-parallel, not supported yet
            if (planeAxis == Vector3d.Zero)
            {
                throw new NotSupportedException("Provided start points are anti-parallel");
            }

            // get the number of points to be added
            var pointsCount = Math.Max((int)(angleBetween / granularityDegrees), 0);
            // define the angle between each point
            var anglePerPoint = angleBetween / pointsCount;
            // create the resulting curve points array
            var curvePoints = new Vector3d[pointsCount + 1];

            curvePoints[0] = a;
            curvePoints[pointsCount] = b;

            // TODO: possible SIMD optimisation
            // run through each point, rotating around the plane normal
            for (var i = 1; i < pointsCount; ++i)
            {
                curvePoints[i] = ScaleToGeocentricSurface(a.Rotate(planeAxis, anglePerPoint * i));
            }

            return curvePoints;
        }

        /// <summary>
        /// Returns an array of WGS84 conforming 3d coordinates that represent a 
        /// curve on the ellipsoid between the two given geodetic 3d coordinates
        /// 
        /// Note: WGS84 defines it's coordinates as Z UP, please ensure to swap Y and Z 
        /// components for the returned positions.
        /// </summary>
        /// <param name="a">The start of the curve</param>
        /// <param name="b">The end of the curve</param>
        public Vector3d[] CurveBetween(Geodetic2d a, Geodetic2d b)
        {
            // TODO: determine granularity automagically based on distance
            var granularityDegrees = 1;
            return CurveBetween(Wgs84Position3d(a), Wgs84Position3d(b), granularityDegrees);
        }

        /// <summary>
        /// Returns an array of WGS84 conforming 3d coordinates that represent a 
        /// curve on the ellipsoid between the two given geodetic 3d coordinates
        /// 
        /// Note: WGS84 defines it's coordinates as Z UP, please ensure to swap Y and Z 
        /// components for the returned positions.
        /// </summary>
        /// <param name="a">The start of the curve</param>
        /// <param name="b">The end of the curve</param>
        /// <param name="granularityDegrees">The angle between each point on the curve</param>
        /// <returns></returns>
        public Vector3d[] CurveBetween(Geodetic2d a, Geodetic2d b, double granularityDegrees)
        {
            return CurveBetween(Wgs84Position3d(a), Wgs84Position3d(b), granularityDegrees);
        }

        /// <summary>
        /// Returns an array of WGS84 conforming 3d coordinates that represent a 
        /// curve on the ellipsoid between the two given geodetic 3d coordinates
        /// 
        /// Note: WGS84 defines it's coordinates as Z UP, please ensure to swap Y and Z 
        /// components for the returned positions.
        /// 
        /// Note: Even if you provide Geodetic3d positions that are above or below the 
        /// ellipsoid, the curve will always lie on the surface of the ellipsoid.
        /// </summary>
        /// <param name="a">The start of the curve</param>
        /// <param name="b">The end of the curve</param>
        /// <param name="granularityDegrees">The angle between each point on the curve</param>
        /// <returns></returns>
        public Vector3d[] CurveBetween(Geodetic3d a, Geodetic3d b, double granularityDegrees)
        {
            return CurveBetween(Wgs84Position3d(a), Wgs84Position3d(b), granularityDegrees);
        }

        /// <summary>
        /// Returns the point closest to the given plane, similar to Wgs84Position3d
        /// </summary>
        /// <param name="plane">The plane to evaluate</param>
        public Vector3d Closest(Plane3d plane)
        {
            if (plane == null)
            {
                throw new ArgumentNullException(nameof(plane));
            }

            var n = -plane.Normal.Normalised;
            var k = Vector3d.ComponentMultiply(n, RadiiSquared);

            return k / Math.Sqrt(Vector3d.Dot(k, n));
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"r = ({Radii})";
        }
    }
}