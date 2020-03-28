using System;
using Maps.Geometry;
using Maps.Unity.Extensions;
using UnityEngine;

namespace Maps.Unity.Example
{
    /// <summary>
    /// Visual example for Box2d clamping methods
    /// </summary>
    public class Box2dClampingExample : MonoBehaviour
    {
        private void Start()
        {
            var translation = new Vector2d(transform.localPosition.x, 
                transform.localPosition.y);

            var a = Vector2d.Zero + translation;
            var b = Vector2d.One + translation;
            var box = new Box2d(a, b);

            box.Draw(Color.grey, 1000f);

            var res = 10;
            var iters = 4;
            var rate = 1;

            Vector2d[] points = new Vector2d[res];

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

            var clampedLineStrip = box.Clamp(linestrip);
            clampedLineStrip.DrawLines(Color.green, false, 1000f);
        }
    }
}