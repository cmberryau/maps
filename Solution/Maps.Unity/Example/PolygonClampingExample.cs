using Maps.Geometry;
using Maps.Unity.Extensions;
using UnityEngine;

namespace Maps.Unity.Example
{
    /// <summary>
    /// Demonstrates polygon clamping
    /// </summary>
    public class PolygonClampingExample : MonoBehaviour
    {
        private void Update()
        {
            var box = CreateDefaultBox(Vector2d.Zero);
            box.Draw(Color.red);

            var linestrip = CreateBasicConcaveStripPartA(transform.Transformd());

            if (linestrip != null)
            {
                linestrip.DrawLines(Color.cyan, false);

                var clippedLineStrip = box.Clamp(linestrip);

                if (clippedLineStrip != null)
                {
                    clippedLineStrip.DrawLines(Color.green, false);
                }
            }

            linestrip = CreateBasicConcaveStripPartB(transform.Transformd());

            if (linestrip != null)
            {
                linestrip.DrawLines(Color.cyan, false);

                var clippedLineStrip = box.Clamp(linestrip);

                if (clippedLineStrip != null)
                {
                    clippedLineStrip.DrawLines(Color.green, false);
                }
            }
        }

        private static Box2d CreateDefaultBox(Vector2d translation)
        {
            var offset = Vector2d.One * 0.5;
            var a = Vector2d.Zero + translation - offset;
            var b = Vector2d.One + translation - offset;
            return new Box2d(a, b);
        }

        private static LineStrip2d CreateDefaultBoxStrip(Transformd transformd)
        {
            var points = new[]
            {
                (transformd.Matrix * new Vector3d(-0.25, -0.25, 0)).xy,
                (transformd.Matrix * new Vector3d(-0.25, 0.25, 0)).xy,
                (transformd.Matrix * new Vector3d(0.25, 0.25, 0)).xy,
                (transformd.Matrix * new Vector3d(0.25, -0.25, 0)).xy,
                (transformd.Matrix * new Vector3d(-0.25, -0.25, 0)).xy,
            };

            return new LineStrip2d(points);
        }

        private static LineStrip2d CreateBasicConcaveStrip(Transformd transformd)
        {
            var points = new[]
            {
                (transformd.Matrix * new Vector3d(-0.25, 0.25, 0)).xy,
                (transformd.Matrix * new Vector3d(0.25, 0.25, 0)).xy,
                (transformd.Matrix * new Vector3d(0.25, -0.25, 0)).xy,
                (transformd.Matrix * new Vector3d(0.0, 0.0, 0)).xy,
                (transformd.Matrix * new Vector3d(-0.25, -0.25, 0)).xy,
                (transformd.Matrix * new Vector3d(-0.25, 0.25, 0)).xy,
            };

            return new LineStrip2d(points);
        }

        private static LineStrip2d CreateBasicConcaveStripPartA(Transformd transformd)
        {
            var points = new[]
            {
                (transformd.Matrix * new Vector3d(0.25, 0.25, 0)).xy,
                (transformd.Matrix * new Vector3d(0, 0.25, 0)).xy,
                (transformd.Matrix * new Vector3d(0, 0.0, 0)).xy,
                (transformd.Matrix * new Vector3d(0.25, -0.25, 0)).xy,
                (transformd.Matrix * new Vector3d(0.25, 0.25, 0)).xy,
            };

            return new LineStrip2d(points);
        }

        private static LineStrip2d CreateBasicConcaveStripPartB(Transformd transformd)
        {
            var points = new[]
            {
                (transformd.Matrix * new Vector3d(-0.25, 0.25, 0)).xy,
                (transformd.Matrix * new Vector3d(0, 0.25, 0)).xy,
                (transformd.Matrix * new Vector3d(0.0, 0.0, 0)).xy,
                (transformd.Matrix * new Vector3d(-0.25, -0.25, 0)).xy,
                (transformd.Matrix * new Vector3d(-0.25, 0.25, 0)).xy,
            };

            return new LineStrip2d(points);
        }
    }
}