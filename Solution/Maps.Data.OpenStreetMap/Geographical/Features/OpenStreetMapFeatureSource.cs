using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maps.Data.OpenStreetMap.Translation;
using Maps.Geographical;
using Maps.Geographical.Features;

namespace Maps.Data.OpenStreetMap.Geographical.Features
{
    /// <summary>
    /// Feature source for OpenStreetMap
    /// </summary>
    public class OpenStreetMapFeatureSource : IFeatureSource
    {
        /// <inheritdoc />
        public SourceMeta Meta => throw new NotImplementedException();

        private readonly IOsmGeoSource _geoSource;
        private readonly IOsmGeoTranslator _translator;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of OpenStreetMapFeatureSource
        /// </summary>
        internal OpenStreetMapFeatureSource(IOsmGeoSource geoSource)
        {
            if (geoSource == null)
            {
                throw new ArgumentNullException(nameof(geoSource));
            }

            _translator = OsmGeoTranslator.Default;
            _geoSource = geoSource;
        }

        private OpenStreetMapFeatureSource(IOsmGeoSource geoSource, 
            IOsmGeoTranslator translator)
        {
            _geoSource = geoSource;
            _translator = translator;
        }

        /// <inheritdoc />
        public IList<Feature> Get(GeodeticBox2d box)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OpenStreetMapFeatureSource));
            }

            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            Feature feature;
            var features = new List<Feature>();

            var nodes = _geoSource.GetNodes(box, _translator.NodeTags);
            var nodesCount = nodes.Count;
            for (var i = 0; i < nodesCount; ++i)
            {
                if (_translator.TryTranslate(nodes[i], out feature))
                {
                    features.Add(feature);
                }
            }

            var ways = _geoSource.GetWays(box, _translator.WayTags);
            var waysCount = ways.Count;

            for (var i = 0; i < waysCount; ++i)
            {
                if(_translator.TryTranslate(ways[i], out feature))
                {
                    features.Add(feature);
                }
            }

            var relations = _geoSource.GetRelations(box, _translator.RelationTags);
            var relationsCount = relations.Count;

            for (var i = 0; i < relationsCount; ++i)
            {
                _translator.TryTranslate(relations[i], features);
            }

            return features;
        }

        /// <inheritdoc />
        public Task<IList<Feature>>  
            GetAsync(GeodeticBox2d box)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OpenStreetMapFeatureSource));
            }

            if (box == null)
            {
                throw new ArgumentNullException(nameof(box));
            }

            return Task<IList<Feature>
                >.Factory.StartNew(() => Get(box));
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OpenStreetMapFeatureSource));
            }

            _geoSource.Dispose();
            _disposed = true;
        }

        /// <inheritdoc />
        public object Clone()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OpenStreetMapFeatureSource));
            }

            var clonedGeoSource = _geoSource.Clone();

            if (clonedGeoSource == null)
            {
                throw new InvalidOperationException($"{nameof(IOsmGeoSource)} failed to clone");
            }

            var castedClonedGeoSource = clonedGeoSource as IOsmGeoSource;

            if (castedClonedGeoSource == null)
            {
                throw new InvalidOperationException($"Cloned {nameof(IOsmGeoSource)} not castable to {nameof(IOsmGeoSource)}");
            }

            return new OpenStreetMapFeatureSource(castedClonedGeoSource, _translator);
        }
    }
}