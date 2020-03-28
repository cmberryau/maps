using System;
using System.IO;
using Maps.Extensions;
using Maps.Geographical;
using Maps.Geographical.Features;
using Maps.Geographical.Projection;
using Maps.IO;
using Maps.Unity.Extensions;
using UnityEngine;

namespace Maps.Unity.Editor.IO
{
    /// <summary>
    /// Visual test for the FeatureWriter and FeatureReader classes
    /// </summary>
    public class FeatureWriterTest : MonoBehaviour
    {
        private void Start()
        {
            var path = Application.streamingAssetsPath +
                Path.DirectorySeparatorChar + "features.bin";

            {
                var coordinates = new[]
                {
                    GeodeticBox2d.World.A,
                    GeodeticBox2d.World.B
                };

                var segment = new Segment(123L.ToGuid(), "",
                    coordinates, SegmentCategory.Unknown);

                File.Delete(path);
                using (var fileStream = File.OpenWrite(path))
                {
                    using (var writer = new SingleFeatureWriter(fileStream))
                    {
                        writer.Write(segment, null);
                    }
                }
            }

            Feature feature = null;

            using (var fileStream = File.OpenRead(path))
            {
                using (var reader = new SingleFeatureReader(fileStream))
                {
                    while (reader.Read())
                    {
                        feature = reader.Current;
                    }
                }
            }

            if (feature == null)
            {
                throw new InvalidOperationException();
            }

            var projection = new WebMercatorProjection();

            projection.Forward(GeodeticBox2d.World).DrawLines(Color.green, 1000f, true);

            var readSegment = feature as Segment;

            if (readSegment != null)
            {
                projection.Forward(readSegment.LineStrip).DrawLines(Color.red, 1000f, false);
            }
        }
    }
}