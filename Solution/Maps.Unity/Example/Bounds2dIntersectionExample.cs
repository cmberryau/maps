using Maps.Geometry;
using Maps.Unity.Extensions;
using UnityEngine;

#pragma warning disable 1591

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnassignedField.Global

namespace Maps.Unity.Example
{
    /// <summary>
    /// Visual example for Bounds2d clipping methods
    /// </summary>
    public class Bounds2dIntersectionExample : MonoBehaviour
    {
        public double A_X = 0;
        public double A_Y = 0;

        public double B_X = 1;
        public double B_Y = 1;

        public double P0_X = -2;
        public double P0_Y = -2;

        public double P1_X = 2;
        public double P1_Y = 2;

        private void Update()
        {
            var translation = new Vector2d(transform.localPosition.x,
                transform.localPosition.y);

            var a = new Vector2d(A_X, A_Y) + translation;
            var b = new Vector2d(B_X, B_Y) + translation;
            var bounds = new Bounds2d(Vector2d.Midpoint(a, b),
                Vector2d.Abs(b - a));

            bounds.Draw(Color.gray);

            var p0 = new Vector2d(P0_X, P0_Y);
            var p1 = new Vector2d(P1_X, P1_Y);

            var segment = new LineSegment2d(p0, p1);

            segment.Draw(Color.blue);

            var intersectionSegment = Bounds2d.Intersection(bounds, segment);

            if (intersectionSegment != null)
            {
                intersectionSegment.Draw(Color.green);
            }
        }
    }
}