using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maps.Geographical;
using Maps.Geographical.Combining;
using Maps.Geographical.Features;
using Maps.Geographical.Filtering;
using Maps.Geographical.Simplification;
using Maps.Geographical.Tiles;

namespace Maps.Data.Compilation
{
    /// <summary>
    /// Responsible for the compilation of a single zoom level
    /// </summary>
    public class ZoomLevelTask
    {
        /// <summary>
        /// The zoom level
        /// </summary>
        public readonly int ZoomLevel;

        /// <summary>
        /// The feature filter for this zoom level
        /// </summary>
        public readonly CompoundFilter Filter;

        private readonly FeatureSimplifier _simplifier;

        /// <summary>
        /// Initializes a new instance of ZoomLevelTask
        /// </summary>
        /// <param name="zoomLevel">The zoom level</param>
        /// <param name="filter">The feature filter</param>
        /// <param name="simplifier">The feature simplifier</param>
        /// <exception cref="ArgumentNullException">Thrown if any argument is null
        /// </exception>
        public ZoomLevelTask(int zoomLevel, CompoundFilter filter,
            FeatureSimplifier simplifier)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (simplifier == null)
            {
                throw new ArgumentNullException(nameof(simplifier));
            }

            ZoomLevel = zoomLevel;
            Filter = filter;
            _simplifier = simplifier;
        }

        /// <summary>
        /// Compiles the zoom level from a non-tiled source
        /// </summary>
        /// <param name="master">The master feature source</param>
        /// <param name="target">The write-only feature target</param>
        /// <param name="area">The area we are compiling for</param>
        /// <param name="filter">The overriding filter to use</param>
        /// <param name="taskCount">The number of tasks to use</param>
        /// <exception cref="ArgumentNullException">Thrown if any argument is null</exception>
        public void Compile(IFeatureSource master, ITiledFeatureTarget target,
            GeodeticBox2d area, FeatureFilter<Feature> filter, int taskCount)
        {
            if (master == null)
            {
                throw new ArgumentNullException(nameof(master));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var tilesList = target.TileSource.GetForZoom(area, ZoomLevel, false);
            var tilesCount = tilesList.Count;
            var tileQueue = new ConcurrentQueue<Tile>();

            // place all tiles in a queue
            for (var i = 0; i < tilesCount; ++i)
            {
                tileQueue.Enqueue(tilesList[i]);
            }

            // start all tasks
            var tasks = new Task[taskCount];
            for (var i = 0; i < taskCount; ++i)
            {
                var masterClone = master.Clone();

                if (masterClone == null)
                {
                    throw new InvalidOperationException($"{nameof(IFeatureSource)} failed to clone");
                }

                var castedMasterClone = masterClone as IFeatureSource;

                if (castedMasterClone == null)
                {
                    throw new InvalidOperationException($"Cloned {nameof(IFeatureSource)} failed to cast to {nameof(IFeatureSource)}");
                }

                tasks[i] = Task.Factory.StartNew(() => CompileRunner(tileQueue,
                    castedMasterClone, target, area, filter, _simplifier));
            }

            // wait for all tasks
            Task.WaitAll(tasks);

            // flush the target out
            target.Flush();
        }

        /// <summary>
        /// Compiles the zoom level from a tiled source
        /// </summary>
        /// <param name="source">The tiled feature source</param>
        /// <param name="target">The tiled feature target</param>
        /// <param name="area">The area we are compiling for</param>
        /// <param name="filter">The overriding filter to use</param>
        /// <param name="taskCount">The number of tasks to use</param>
        /// <exception cref="ArgumentNullException">Thrown if any argument is null</exception>
        public void Compile(ITiledFeatureSource source, ITiledFeatureTarget target,
            GeodeticBox2d area, FeatureFilter<Feature> filter, int taskCount)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var tilesList = target.TileSource.GetForZoom(area, ZoomLevel, false);
            var tilesCount = tilesList.Count;
            var tileQueue = new ConcurrentQueue<Tile>();

            // place all tiles in a queue
            for (var i = 0; i < tilesCount; ++i)
            {
                tileQueue.Enqueue(tilesList[i]);
            }

            // start all tasks
            var tasks = new Task[taskCount];
            for (var i = 0; i < taskCount; ++i)
            {
                tasks[i] = Task.Factory.StartNew(() => CompileRunner(tileQueue,
                    source, target, filter, _simplifier));
            }

            // wait for all tasks
            Task.WaitAll(tasks);

            // flush the target out
            target.Flush();
        }

        /// <summary>
        /// Cleans the zoom level task out, removing unwanted features
        /// </summary>
        /// <param name="source">The tiled feature source</param>
        /// <param name="target">The tiled feature target</param>
        /// <param name="area">The area we are cleaning for</param>
        /// <param name="taskCount">The number of tasks to use</param>
        /// <exception cref="ArgumentNullException">Thrown if any argument is null</exception>
        public void Clean(ITiledFeatureSource source, ITiledFeatureTarget target,
            GeodeticBox2d area, int taskCount)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            var tilesList = target.TileSource.GetForZoom(area, ZoomLevel, false);
            var tilesCount = tilesList.Count;
            var tileQueue = new ConcurrentQueue<Tile>();

            // place all tiles in a queue
            for (var i = 0; i < tilesCount; ++i)
            {
                tileQueue.Enqueue(tilesList[i]);
            }

            // start all tasks
            var tasks = new Task[taskCount];
            for (var i = 0; i < taskCount; ++i)
            {
                tasks[i] = Task.Factory.StartNew(() => CleanRunner(tileQueue, source, 
                    target, Filter));
            }

            // wait for all tasks
            Task.WaitAll(tasks);

            // flush the target out
            target.Flush();
        }

        private static void CompileRunner(ConcurrentQueue<Tile> tileQueue,
            IFeatureSource master, ITiledFeatureTarget target, GeodeticBox2d area,
            FeatureFilter<Feature> filter, FeatureSimplifier simplifier)
        {
            var combiner = new FeatureCombiner();

            // keep attempting to get tiles out of the queue
            while (tileQueue.TryDequeue(out var tile))
            {
                var features = master.Get(tile.Box);

                if (features.Count > 0)
                {
                    // filter features
                    var filtered = filter.Filter(features);
                    // clip features to tile
                    var clipped = tile.Clip(filtered);

                    // clip features to area if tile exceeds area
                    if (!area.Contains(tile.Box))
                    {
                        clipped = area.Clip(clipped);
                    }

                    // combine features
                    var combined = combiner.Combine(clipped);
                    // simplify features
                    var simplified = simplifier.Simplify(combined, tile);

                    target.Write(tile, simplified);
                }
            }
        }

        private static void CompileRunner(ConcurrentQueue<Tile> tileQueue,
            ITiledFeatureSource source, ITiledFeatureTarget target,
            FeatureFilter<Feature> filter, FeatureSimplifier simplifier)
        {
            var combiner = new FeatureCombiner();

            // keep attempting to get tiles out of the queue
            while (tileQueue.TryDequeue(out var tile))
            {
                var subTiles = tile.SubTiles;
                var subFeatures = new List<Feature>();

                // gather all features from the subtiles
                for (var j = 0; j < subTiles.Count; ++j)
                {
                    var features = source.Get(subTiles[j]);

                    for (var k = 0; k < features.Count; ++k)
                    {
                        subFeatures.Add(features[k]);
                    }
                }

                if (subFeatures.Count > 0)
                {
                    // filter features
                    var filtered = filter.Filter(subFeatures);
                    // combine features
                    var combined = combiner.Combine(filtered);
                    // simplify features
                    var simplified = simplifier.Simplify(combined, tile);

                    target.Write(tile, simplified);
                }
            }
        }

        private static void CleanRunner(ConcurrentQueue<Tile> tileQueue,
            ITiledFeatureSource source, ITiledFeatureTarget target,
            FeatureFilter<Feature> filter)
        {
            // keep attempting to get tiles out of the queue
            while (tileQueue.TryDequeue(out var tile))
            {
                var features = source.Get(tile);
                var filtered = filter.Filter(features);

                target.Write(tile, filtered);
            }
        }
    }
}