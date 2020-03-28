using Maps.Geographical;
using Maps.Geographical.Projection;
using Maps.Geographical.Tiles;
using Maps.Geometry;
using Maps.Unity.Extensions;
using UnityEngine;

namespace Maps.Unity.Editor.Geographical.Projection
{
    /// <summary>
    /// Visual test for various projection methods
    /// </summary>
    public class ProjectionTest : MonoBehaviour
    {
        private void Start()
        {
            TestTileSourceMercator();
            TestTileSourceEllipsoidal();
        }

        private static void TestTileSourceMercator()
        {
            TestTileSource(new WebMercatorProjection());
        }

        private static void TestTileSourceEllipsoidal()
        {
            TestTileSource(new EllipsoidalProjection(Ellipsoid.NormalisedWgs84));
        }

        private static void TestTileSource(IProjection projection)
        {
            var tileSource = new TmsTileSource();
            var tiles = tileSource.GetForZoom(GeodeticBox2d.World, 4);

            foreach (var tile in tiles)
            {
                projection.Forward(tile.Box).DrawLines(Color.red, 1000f, true);
            }
        }
    }
}