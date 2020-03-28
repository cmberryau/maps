using System;
using Maps.Geometry;
using Maps.Unity.Extensions;
using UnityEngine;

namespace Maps.Unity.Editor.Geometry
{
    /// <summary>
    /// Visual example for Box2d clipping methods
    /// </summary>
    public class Box2dClippingExample : MonoBehaviour
    {
        private void Start()
        {
            var translation = new Vector2d(transform.localPosition.x,
                transform.localPosition.y);

            var a = Vector2d.Zero + translation;
            var b = Vector2d.One + translation;
            var box = new Box2d(a, b);

            box.Draw(Color.grey, 1000f);

            var res = 256;
            var iters = 20;
            var rate = 1;

            //var points = new[]
            //{
            //    new Vector2d(0.5, 1.1) + translation,
            //    new Vector2d(1.1, 0.5) + translation,
            //    new Vector2d(0.5, -0.1) + translation,
            //    new Vector2d(-0.1, 0.5) + translation,
            //    new Vector2d(0.5, 1.1) + translation,
            //};

            var points = new Vector2d[res];

            for (var i = 0; i < res; i++)
            {
                var t = (double)i / res;
                var u = t * rate;

                var x = Math.Cos(t * Math.PI * iters) * u;
                var y = Math.Sin(t * Math.PI * iters) * u;

                points[i] = new Vector2d(x, y) + translation;
            }

            var linestrip = new LineStrip2d(points);
            linestrip.DrawLines(Color.blue, false, 1000f);

            var clippedLineStrips = box.Clip(linestrip);

            foreach (var strip in clippedLineStrips)
            {
                strip.DrawLines(Color.green, false, 1000f);
            }
        }
    }
}