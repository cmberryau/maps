using Maps.Geographical;
using Maps.Geographical.Projection;
using Maps.Unity.Extensions;
using UnityEngine;

namespace Maps.Unity.Editor.Geographical
{
    /// <summary>
    /// Visual test for GeodeticBox2d clipping methods
    /// </summary>
    public class GeodeticBox2dClippingTest : MonoBehaviour
    {
        private void Start()
        {
            //var projection = new EllipsoidalProjection(Ellipsoid.NormalisedWgs84);
            //var projection = new IdentityProjection(1d / 90d);
            var projection = new WebMercatorProjection();

            // draw the outer bounds
            var centre = Geodetic2d.Meridian;
            var outergeobox = new GeodeticBox2d(centre, 10000000d);
            var outerbox = projection.Forward(outergeobox);
            outerbox.DrawLines(Color.blue, 1000f, true);

            // draw the clip bounds
            var geobox = new GeodeticBox2d(centre, 5000000d);
            var box = projection.Forward(geobox);
            box.DrawLines(Color.green, 1000f, true);

            // draw our subject line strip
            var geolinestrip = new GeodeticLineStrip2d(new[]
            {
                outergeobox.A,
                outergeobox.B
            });
            var linestrip = projection.Forward(geolinestrip);
            linestrip.DrawLines(Color.blue, 1000f, false);

            // draw our clipped linestrip
            var clippedgeolinestrips = geobox.Clip(geolinestrip);
            var clippedlinestrip = projection.Forward(clippedgeolinestrips[0]);
            clippedlinestrip.DrawLines(Color.green, 1000f, false);
        }
    }
}