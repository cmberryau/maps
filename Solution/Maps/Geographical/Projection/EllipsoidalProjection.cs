using System;
using System.Collections.Generic;
using Maps.Geometry;
using Maps.Rendering;

namespace Maps.Geographical.Projection
{
    /// <summary>
    /// Provides an Ellipsoidal projection, used commonly for projecting
    /// Map data onto a ellipsoidal approximation of the earth
    /// 
    /// See Maps.Geometry.Ellipsoid for more info
    /// </summary>
    public class EllipsoidalProjection : Projection
    {
        /// <inheritdoc />
        public override Vector3d Extents
        {
            get
            {
                var extent = _ellipsoid.Scale * Mathd.RMajor;
                return new Vector3d(extent, extent, extent);
            }
        }

        private readonly Ellipsoid _ellipsoid;

        /// <summary>
        /// Initializes a new instance of EllipsoidalProjection
        /// </summary>
        /// <param name="ellipsoid">The ellipsoid geometry to use for 
        /// projection</param>
        public EllipsoidalProjection(Ellipsoid ellipsoid)
        {
            if (ellipsoid == null)
            {
                throw new ArgumentNullException(nameof(ellipsoid));
            }

            _ellipsoid = ellipsoid;
        }

        /// <inheritdoc />
        public override bool Intersection(Ray3d ray, out Vector3d point)
        {
            if (ray == null)
            {
                throw new ArgumentNullException(nameof(ray));
            }

            Vector3d p1;

            if (!ray.Intersection(_ellipsoid, out point, out p1) 
                || point == Vector3d.NaN || p1 == Vector3d.NaN)
            {
                point = Vector3d.Zero;
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        public override bool Intersection(Ray3d ray, out Geodetic3d coordinate)
        {
            if (ray == null)
            {
                throw new ArgumentNullException(nameof(ray));
            }

            Vector3d intersectionPoint;

            if (!Intersection(ray, out intersectionPoint) ||
                intersectionPoint == Vector3d.NaN)
            {
                coordinate = new Geodetic3d(Geodetic2d.Meridian, 0d);
                return false;
            }

            coordinate = Reverse(intersectionPoint);

            return true;
        }

        /// <inheritdoc />
        public override bool VisibleArea(CameraFrustum frustum, out GeodeticBox2d box)
        {
            throw new NotImplementedException();

            //if (topLeft == null)
            //{
            //    throw new ArgumentNullException(nameof(topLeft));
            //}

            //if (topRight == null)
            //{
            //    throw new ArgumentNullException(nameof(topRight));
            //}

            //if (bottomRight == null)
            //{
            //    throw new ArgumentNullException(nameof(bottomRight));
            //}

            //if (bottomLeft == null)
            //{
            //    throw new ArgumentNullException(nameof(bottomLeft));
            //}

            //// resolve the intersections
            //Geodetic3d topLeftIntersection;
            //Geodetic3d topRightIntersection;
            //Geodetic3d bottomRightIntersection;
            //Geodetic3d bottomLeftIntersection;

            //var origin = topLeft.Origin;

            //var leftPlane = new Plane3d(origin,
            //    Vector3d.Cross(topLeft.Direction, bottomLeft.Direction));
            //var topPlane = new Plane3d(origin,
            //    Vector3d.Cross(topRight.Direction, topLeft.Direction));

            //var rightPlane = new Plane3d(origin,
            //    Vector3d.Cross(bottomRight.Direction, topRight.Direction));
            //var bottomPlane = new Plane3d(origin,
            //    Vector3d.Cross(bottomLeft.Direction, bottomRight.Direction));

            //if (!Intersection(topLeft, out topLeftIntersection))
            //{
            //    // ellipsoid intersection of ray from plane intersection
            //    //aIntersection = Project(ResolveEllipsoidMiss(a, plane));

            //    //var plane = new Plane3d(topLeft.Origin, Vector3d.Cross());
            //    topLeftIntersection = Reverse(_ellipsoid.Closest(leftPlane));
            //}

            //if (!Intersection(topRight, out topRightIntersection))
            //{
            //    // ellipsoid intersection of ray from plane intersection
            //    //bIntersection = Project(ResolveEllipsoidMiss(b, plane));

            //    topRightIntersection = Reverse(_ellipsoid.Closest(rightPlane));
            //}

            //if (!Intersection(bottomRight, out bottomRightIntersection))
            //{
            //    // ellipsoid intersection of ray from plane intersection
            //    //cIntersection = Project(ResolveEllipsoidMiss(c, plane));

            //    bottomRightIntersection = Reverse(bottomRight.Closest(_ellipsoid));
            //}

            //if (!Intersection(bottomLeft, out bottomLeftIntersection))
            //{
            //    // ellipsoid intersection of ray from plane intersection
            //    //dIntersection = Project(ResolveEllipsoidMiss(d, plane));

            //    bottomLeftIntersection = Reverse(bottomLeft.Closest(_ellipsoid));
            //}

            //// pack intersections into an array and encompass them in a box
            //var intersections = new[]
            //{
            //    topLeftIntersection,
            //    topRightIntersection,
            //    bottomRightIntersection,
            //    bottomLeftIntersection,
            //};

            //box = GeodeticBox2d.Encompass(intersections);

            return true;
        }

        /// <inheritdoc />
        public override Vector3d Forward(Geodetic2d coordinate)
        {
            return _ellipsoid.Wgs84Position3d(coordinate).xzy;
        }

        /// <inheritdoc />
        public override Vector3d Forward(Geodetic3d coordinate)
        {
            return _ellipsoid.Wgs84Position3d(coordinate).xzy;
        }

        /// <inheritdoc />
        public override Geodetic3d Reverse(Vector3d point)
        {
            return _ellipsoid.Wgs84Coordinate3d(point.xzy);
        }

        /// <inheritdoc />
        public override IList<Vector3d> Forward(GeodeticBox2d box)
        {
            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            // generate curves over ellipsoid
            var curvePoints = new Vector3d[4][];
            var totalPoints = 0;

            for (var i = 0; i < 3; i++)
            {
                curvePoints[i] = _ellipsoid.CurveBetween(box[i], box[i + 1], 1);
                totalPoints += curvePoints[i].Length;
            }

            curvePoints[3] = _ellipsoid.CurveBetween(box[3], box[0]);
            var finalPoints = new Vector3d[totalPoints + 
                curvePoints[3].Length];

            // copy generated curves into final points array
            for (int i = 0, targetIndex = 0; i < 4; i++)
            {
                Array.Copy(curvePoints[i], 0, finalPoints, targetIndex, 
                    curvePoints[i].Length);
                targetIndex += curvePoints[i].Length;
            }

            // swizzle for y-up
            for (var i = 0; i < finalPoints.Length; i++)
            {
                finalPoints[i] = finalPoints[i].xzy;
            }

            return finalPoints;
        }

        /// <inheritdoc />
        public override IList<Vector3d> Forward(GeodeticLineStrip2d linestrip)
        {
            if (linestrip == null)
            {
                throw new ArgumentNullException(nameof(linestrip));
            }

            var coordinateCount = linestrip.Count;
            var stripPoints = new Vector3d[coordinateCount - 1][];
            var totalPoints = 0;

            // generate curves between each point first
            for (var i = 1; i < coordinateCount; i++)
            {
                stripPoints[i - 1] = _ellipsoid.CurveBetween(linestrip[i - 1],
                    linestrip[i]);
                totalPoints += stripPoints[i - 1].Length;
            }

            // copy generated curves into final array
            var finalPoints = new Vector3d[totalPoints];
            for (int i = 0, targetIndex = 0; i < stripPoints.Length; i++)
            {
                Array.Copy(stripPoints[i], 0, finalPoints, targetIndex,
                    stripPoints[i].Length);
                targetIndex += stripPoints[i].Length;
            }

            // swizzle for y-up
            for (var i = 0; i < finalPoints.Length; i++)
            {
                finalPoints[i] = finalPoints[i].xzy;
            }

            return finalPoints;
        }

        /// <inheritdoc />
        public override IList<IList<Vector3d>> Forward(GeodeticPolygon2d polygon)
        {
            throw new NotImplementedException();
        }

        private Vector3d ResolveEllipsoidMiss(Ray3d ray, Plane3d plane)
        {
            throw new NotImplementedException();

            Vector3d planeIntersection;

            if (!ray.Intersection(plane, out planeIntersection))
            {
                // not even facing the ellipsoid's midplane
                throw new NotImplementedException("Not facing ellipsoid midplane");
            }

            // create a new ray from the plane intersection to the ellipsoid's centre
            var planeRay = new Ray3d(planeIntersection, -planeIntersection);

            Vector3d intersection0;
            Vector3d intersection1;

            if (!planeRay.Intersection(_ellipsoid, out intersection0,
                out intersection1))
            {
                throw new InvalidOperationException("Plane intersection to " +
                    "origin does not intersect with ellipsoid");
            }

            return intersection0;
        }
    }
}