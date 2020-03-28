using System;
using System.Collections.Generic;
using Maps.Geometry;
using Maps.Rendering;

namespace Maps.Geographical.Projection
{
    /// <summary>
    /// Base functionality for projecting geographical elements
    /// </summary>
    public abstract class Projection : IProjection
    {
        /// <inheritdoc />
        public abstract Vector3d Extents
        {
            get;
        }

        /// <inheritdoc />
        public abstract bool Intersection(Ray3d ray, out Vector3d point);

        /// <inheritdoc />
        public abstract bool Intersection(Ray3d ray, out Geodetic3d coordinate);

        /// <inheritdoc />
        public virtual bool VisibleArea(CameraFrustum frustum, out GeodeticBox2d box)
        {
            if (frustum == null)
            {
                throw new ArgumentNullException(nameof(frustum));
            }

            var topLeft = new Ray3d(frustum.ViewPoint, frustum.FarTopLeft - 
                frustum.ViewPoint);

            if (!Intersection(topLeft, out Geodetic3d a))
            {
                box = GeodeticBox2d.Zero;
                return false;
            }

            var topRight = new Ray3d(frustum.ViewPoint, frustum.FarTopRight -
                frustum.ViewPoint);

            if (!Intersection(topRight, out Geodetic3d b))
            {
                box = GeodeticBox2d.Zero;
                return false;
            }

            var bottomRight = new Ray3d(frustum.ViewPoint, frustum.FarBottomRight -
                frustum.ViewPoint);

            if (!Intersection(bottomRight, out Geodetic3d c))
            {
                box = GeodeticBox2d.Zero;
                return false;
            }

            var bottomLeft = new Ray3d(frustum.ViewPoint, frustum.FarBottomLeft -
                frustum.ViewPoint);

            if (!Intersection(bottomLeft, out Geodetic3d d))
            {
                box = GeodeticBox2d.Zero;
                return false;
            }

            box = GeodeticBox2d.Encompass(new[]
                {
                    a,
                    b,
                    c,
                    d
                }
            );

            return true;
        }

        /// <inheritdoc />
        public abstract Vector3d Forward(Geodetic2d coordinate);

        /// <inheritdoc />
        public abstract Vector3d Forward(Geodetic3d coordinate);

        /// <inheritdoc />
        public abstract Geodetic3d Reverse(Vector3d point);

        /// <inheritdoc />
        public IList<Vector3d> Forward(IReadOnlyList<Geodetic2d> coordinates)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            var coordinatesCount = coordinates.Count;
            var points = new Vector3d[coordinatesCount];

            for (var i = 0; i < coordinatesCount; ++i)
            {
                points[i] = Forward(coordinates[i]);
            }

            return points;
        }

        /// <inheritdoc />
        public IList<Vector3d> Forward(IReadOnlyList<Geodetic3d> coordinates)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException(nameof(coordinates));
            }

            var coordinatesCount = coordinates.Count;
            var points = new Vector3d[coordinatesCount];

            for (var i = 0; i < coordinatesCount; ++i)
            {
                points[i] = Forward(coordinates[i]);
            }

            return points;
        }

        /// <inheritdoc />
        public IList<Geodetic3d> Reverse(IReadOnlyList<Vector3d> points)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            var pointsCount = points.Count;
            var coordinates = new Geodetic3d[pointsCount];

            for (var i = 0; i < pointsCount; ++i)
            {
                coordinates[i] = Reverse(points[i]);
            }

            return coordinates;
        }

        /// <inheritdoc />
        public virtual IList<Vector3d> Forward(GeodeticBox2d box)
        {
            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            return new []
            {
                Forward(box[0]),
                Forward(box[1]),
                Forward(box[2]),
                Forward(box[3]),
            };
        }

        /// <inheritdoc />
        public virtual IList<Vector3d> Forward(GeodeticLineStrip2d strip)
        {
            if (strip == null)
            {
                throw new ArgumentNullException(nameof(strip));
            }

            var count = strip.Count;
            var result = new Vector3d[count];

            for (var i = 0; i < count; i++)
            {
                result[i] = Forward(strip[i]);
            }

            return result;
        }

        /// <inheritdoc />
        public virtual IList<IList<Vector3d>> Forward(GeodeticPolygon2d polygon)
        {
            if (polygon == null)
            {
                throw new ArgumentNullException(nameof(polygon));
            }

            var result = new Vector3d[polygon.HoleCount + 1][];
            result[0] = new Vector3d[polygon.Count];

            for (var i = 0; i < polygon.Count; ++i)
            {
                result[0][i] = Forward(polygon[i]);
            }

            for (var i = 0; i < polygon.HoleCount; ++i)
            {
                result[i + 1] = new Vector3d[polygon.Hole(i).Count];

                for (var j = 0; j < polygon.Hole(i).Count; ++j)
                {
                    result[i + 1][j] = Forward(polygon.Hole(i)[j]);
                }
            }

            return result;
        }
    }
}