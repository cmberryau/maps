using System;
using System.Collections.Generic;
using System.Linq;
using Maps.Geographical;
using Maps.Geographical.Features;
using Maps.Geographical.Filtering;

namespace Maps.Data.Compilation
{
    /// <summary>
    /// Represents a single tiled feature compilation task
    /// </summary>
    public class TiledFeatureTask
    {
        private readonly IFeatureSource _master;
        private readonly ITiledFeatureTarget _target;
        private readonly ITiledFeatureSource _source;
        private readonly IList<ZoomLevelTask> _levels;
        private readonly GeodeticBox2d _area;

        /// <summary>
        /// Initializes a new instance TiledFeatureCompilationTask
        /// </summary>
        /// <param name="master">The master source to read features from</param>
        /// <param name="target">The target to write features to</param>
        /// <param name="source">The target as a source</param>
        /// <param name="area">The area to read features from</param>
        /// <param name="levels">The individual zoom level tasks</param>
        /// <exception cref="ArgumentNullException">Thrown if any argument or argument
        /// list element is null</exception>
        public TiledFeatureTask(IFeatureSource master, ITiledFeatureTarget target,
            ITiledFeatureSource source, GeodeticBox2d area, IList<ZoomLevelTask> levels)
        {
            if (master == null)
            {
                throw new ArgumentNullException(nameof(master));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (levels == null)
            {
                throw new ArgumentNullException(nameof(levels));
            }

            // resolve the min and max zooms
            var minZoom = int.MaxValue;
            var maxZoom = int.MinValue;
            for (var i = 0; i < levels.Count; ++i)
            {
                if (levels[i] == null)
                {
                    throw new ArgumentException($"Contains null element at {i}", 
                        nameof(levels));
                }

                if (levels[i].ZoomLevel > maxZoom)
                {
                    maxZoom = levels[i].ZoomLevel;
                }

                if (levels[i].ZoomLevel < minZoom)
                {
                    minZoom = levels[i].ZoomLevel;
                }
            }

            // validate the min and max zooms
            var tileSource = target.TileSource;
            if (minZoom < tileSource.MinZoomLevel)
            {
                throw new ArgumentOutOfRangeException(nameof(minZoom));
            }

            if (maxZoom > tileSource.MaxZoomLevel)
            {
                throw new ArgumentOutOfRangeException(nameof(maxZoom));
            }

            // sort and hold the zoom levels
            _levels = levels.OrderByDescending(x => x.ZoomLevel).ToList();
            _master = master;
            _target = target;
            _source = source;
            _area = area;
        }

        /// <inheritdoc />
        public void Start(int taskCount)
        {
            var levelCount = _levels.Count;
            var filters = new FeatureFilter<Feature>[levelCount];

            /* first we create the cascading filters, to ensure that every zoom level
             * contains data required by every zoom level above it */
            filters[levelCount - 1] = _levels[levelCount - 1].Filter;
            for (var i = levelCount - 2; i > -1; --i)
            {
                filters[i] = _levels[i].Filter | filters[i + 1];
            }

            /* we compile the first zoom level directly from the master database,
             * following levels come from the target being written to */
            _levels[0].Compile(_master, _target, _area, filters[0], taskCount);

            var levels = new int[levelCount];
            levels[0] = _levels[0].ZoomLevel;

            for (var i = 1; i < _levels.Count; ++i)
            {
                _levels[i].Compile(_source, _target, _area, filters[i], taskCount);
                /* after a level is compiled, the level below is cleaned to remove
                 * the unused data on that level that was only held for levels above */
                _levels[i - 1].Clean(_source, _target, _area, taskCount);
                levels[i] = _levels[i].ZoomLevel;
            }

            // finally we write the meta information to the target
            _target.Meta = new TiledSourceMeta(_area, levels);
        }
    }
}
