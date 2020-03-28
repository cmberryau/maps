using System;
using System.Collections.Generic;
using log4net;
using Maps.Appearance;
using Maps.Geographical;
using Maps.Geographical.Features;
using Maps.Geographical.Projection;
using Maps.Geographical.Tiles;
using Maps.Lod;

namespace Maps
{
    /// <summary>
    /// Responsible for the implementation of a map, partitioned by tiles.
    /// </summary>
    public class TiledMap : MapBase, ITiledMap
    {
        /// <inheritdoc />
        public TiledMapAppearance Appearance
        {
            get => _appearance;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Appearance));
                }

                if (!value.Equals(_appearance))
                {
                    OnAppearanceChanged(value);
                    _appearance = value;
                }
            }
        }

        /// <inheritdoc />
        public event MapLodChangedHandler LodChanged;

        /// <inheritdoc />
        public int Lod
        {
            get => _lastLod;
            private set
            {
                // only change lod if its an actual change
                if (_lastLod != value)
                {
                    if (LodChanged != null)
                    {
                        LodChanged(_lastLod, value);
                    }

                    HideLod(_lastLod);
                    ShowLod(value);
                    _lastLod = value;
                }

                UpdatePosition();
                UpdateLodTree();
            }
        }

        /// <inheritdoc />
        public override double Scale => _availableLods[Lod].Scale;

        /// <inheritdoc />
        public override double InverseScale => _availableLods[Lod].InverseScale;

        /// <inheritdoc />
        public override IProjection Projection => _appearance[Lod].Projection;

        private readonly ITiledFeatureSource _source;
        private readonly ITileSource _tileSource;

        private readonly IMapLodTree _lodTree;
        private readonly IDictionary<int, IMapLod> _availableLods;
        private readonly int _minLod;
        private int _lastLod;

        private readonly IDynamicFeatureManager _dynamicManager;

        private TiledMapAppearance _appearance;

        private readonly List<Tile> _sideTilesWaiting;
        private readonly HashSet<Tile> _sideTilesWaitingMap;
        private readonly HashSet<Tile> _coreTilesLoading;
        private HashSet<IDisplayTile> _lastActive;

        private Geodetic3d _lastCoordinateBeforeAreaChange;

        private static readonly ILog Log = LogManager.GetLogger(typeof(TiledMap));

        /// <summary>
        /// Initializes a new instance of TiledMap
        /// </summary>
        /// <param name="appearance">The appearance to use</param>
        /// <param name="coordinate">The initial coordinate</param>
        /// <param name="source">The tiled feature source to use</param>
        /// <param name="lodTree">The lodtree for the map</param>
        /// <param name="dynamicManager">The dynamic feature manager</param>
        public TiledMap(TiledMapAppearance appearance, Geodetic3d coordinate,
            ITiledFeatureSource source, IMapLodTree lodTree,
            IDynamicFeatureManager dynamicManager) : base(coordinate)
        {
            if (appearance == null)
            {
                throw new ArgumentNullException(nameof(appearance));
            }

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (lodTree == null)
            {
                throw new ArgumentNullException(nameof(lodTree));
            }

            if (dynamicManager == null)
            {
                throw new ArgumentNullException(nameof(dynamicManager));
            }

            var meta = source.Meta;
            if (meta == null)
            {
                throw new ArgumentException("Source does not provide a Meta property",
                    nameof(source));
            }

            _lastCoordinateBeforeAreaChange = Geodetic3d.Zero;
            _source = source;
            _tileSource = _source.TileSource;

            // create all the available lods
            _minLod = int.MaxValue;
            _availableLods = new Dictionary<int, IMapLod>();
            for (var i = 0; i < meta.ZoomLevels.Count; ++i)
            {
                var lod = meta.ZoomLevels[i];
                _availableLods.Add(lod, lodTree.CreateLod(lod, _tileSource.Scale(lod)));
                _minLod = Math.Min(_minLod, lod);
            }

            // the initial lod is set to the lowest detail lod
            _lastLod = _minLod;
            _appearance = appearance;

            // set up the transform
            Transform.LocalPosition = _appearance[Lod].Projection.Forward(
                coordinate);
            GroundTransform.LocalPosition = _appearance[Lod].Projection.Forward(
                coordinate.Geodetic2d);

            lodTree.Anchor.SetParent(GroundTransform);
            _lodTree = lodTree;

            _sideTilesWaiting = new List<Tile>();
            _sideTilesWaitingMap = new HashSet<Tile>();
            _coreTilesLoading = new HashSet<Tile>();
            _lastActive = new HashSet<IDisplayTile>();

            // attach the dynamic manager to this map
            _dynamicManager = dynamicManager;
            _dynamicManager.AttachTo(this);

            // show the initial lod
            ShowLod(_lastLod);
        }

        /// <inheritdoc />
        public override void Add(IDynamicFeature feature)
        {
            if (feature == null)
            {
                throw new ArgumentNullException(nameof(feature));
            }

            _dynamicManager.Add(feature);
        }

        /// <summary>
        /// Performs a reverse projection of the world position
        /// </summary>
        /// <param name="worldPosition">The world position to reverse-project</param>
        /// <returns>A reverse projected geodetic coordinate</returns>
        public Geodetic3d Reverse(Vector3d worldPosition)
        {
            var scaled = worldPosition * Scale;
            return new Geodetic3d(Projection.Reverse(scaled + 
                Transform.Position).Geodetic2d, 0d);
        }

        /// <inheritdoc />
        protected override void OnVisibleAreaChanged(GeodeticBox2d area, bool lodChange)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            var nextLod = Lod;

            // only change lods if we're allowed to
            if (lodChange)
            {
                // get the lod level based on the visible area
                nextLod = _tileSource.Zoom(area);

                // if lod is not available, get the closest one
                if (!_availableLods.ContainsKey(nextLod))
                {
                    nextLod = ClosestLod(nextLod);
                }

                if (_lastCoordinateBeforeAreaChange != Geodetic3d.Zero)
                {
                    // moving lower, should be a higher lod
                    if (_lastCoordinateBeforeAreaChange.Height > Coordinate.Height)
                    {
                        if (nextLod < _lastLod)
                        {
                            nextLod = _lastLod;
                        }
                    }
                    // moving higher, should be a lower lod
                    else
                    {
                        if (nextLod > _lastLod)
                        {
                            nextLod = _lastLod;
                        }
                    }
                }
            }

            // load the area
            LoadArea(area, nextLod);

            // todo : remove this hack for keeping last coord
            _lastCoordinateBeforeAreaChange = Coordinate;
        }

        /// <inheritdoc />
        protected override void OnCoordinateChanged(Geodetic3d coordinate)
        {
            base.OnCoordinateChanged(coordinate);

            UpdatePosition();
            UpdateLodTree();
        }

        /// <inheritdoc />
        protected override void OnHeadingChanged(double delta)
        {
            base.OnHeadingChanged(delta);

            _dynamicManager.Transform.Rotate(Vector3d.Forward, delta);
            _lodTree.Transform.Rotate(Vector3d.Forward, delta);
            UpdateLodTree();
        }

        /// <inheritdoc />
        protected override void OnTiltChanged(double delta)
        {
            base.OnTiltChanged(delta);

            _dynamicManager.Transform.Rotate(Vector3d.Right, delta, true);
            _lodTree.Transform.Rotate(Vector3d.Right, delta, true);
            UpdateLodTree();
        }

        private void LoadArea(GeodeticBox2d area, int lod)
        {
            var coreTiles = _tileSource.GetForZoom(area, lod, false);
            var sideTiles = _tileSource.GetPadding(area, lod);

            // clear all the currently waiting side tiles
            _sideTilesWaiting.Clear();
            _sideTilesWaitingMap.Clear();

            // add the new side tiles
            for (var i = 0; i < sideTiles.Count; ++i)
            {
                var sideTile = sideTiles[i];

                if (!DisplayTile(lod, sideTile).HasFeatures)
                {
                    if (_sideTilesWaitingMap.Add(sideTiles[i]))
                    {
                        _sideTilesWaiting.Add(sideTiles[i]);
                    }
                    else
                    {
                        Console.WriteLine("Duplicate side tile");
                    }
                }
            }

            // prepare the list of core tiles to load
            var coreTilesToLoad = new List<Tile>();
            for (var i = 0; i < coreTiles.Count; ++i)
            {
                var coreTile = coreTiles[i];

                // only queue for load if not already loading
                if (!DisplayTile(lod, coreTile).HasFeatures  && 
                    _coreTilesLoading.Add(coreTile))
                {
                    coreTilesToLoad.Add(coreTile);
                }
            }

            // if there are core tiles to load, load them
            if (coreTilesToLoad.Count > 0)
            {
                for (var i = 0; i < coreTilesToLoad.Count; ++i)
                {
                    var coreTile = coreTilesToLoad[i];
                    OnGetCoreTileComplete(lod, coreTile, _source.Get(coreTile));
                }
            }
            // otherwise just set the lod
            else
            {
                Lod = lod;
            }

            OnActiveTilesChanged(lod, coreTiles);
        }

        private void OnGetCoreTileComplete(int lod, Tile tile, IList<Feature> features)
        {
            if (_coreTilesLoading.Remove(tile))
            {
                // call the general tile complete method
                OnTileComplete(lod, tile, features);

                // if all queued core tiles are finished
                if (_coreTilesLoading.Count == 0)
                {
                    // set the lod now that all cores are done
                    Lod = lod;

                    // start loading side tiles
                    for (var i = 0; i < _sideTilesWaiting.Count; ++i)
                    {
                        var sideTile = _sideTilesWaiting[i];
                        OnGetSideTileComplete(lod, sideTile, _source.Get(sideTile));
                    }
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private void OnGetSideTileComplete(int lod, Tile tile, IList<Feature> features)
        {
            if (_sideTilesWaitingMap.Remove(tile))
            {
                // call the general tile complete method
                OnTileComplete(lod, tile, features);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private int ClosestLod(int lod)
        {
            // we always want to move to lower resolution lods
            do
            {
                --lod;

                if (lod <= _minLod)
                {
                    lod = _minLod;
                }

            } while (!_availableLods.ContainsKey(lod));

            return lod;
        }

        private void OnTileComplete(int lod, Tile tile, IList<Feature> features)
        {
            DisplayTile(lod, tile).SetFeatures(features);
        }

        private IDisplayTile DisplayTile(int lod, Tile tile)
        {
            return _availableLods[lod].DisplayTile(tile, _appearance[lod]);
        }

        private void OnActiveTilesChanged(int lod, IList<Tile> tiles)
        {
            // create the set of active display tiles
            var active = new HashSet<IDisplayTile>();
            for (var i = 0; i < tiles.Count; ++i)
            {
                var displayTile = DisplayTile(lod, tiles[i]);

                // if display tile was not previously active, becomes active
                if (!_lastActive.Remove(displayTile))
                {
                    displayTile.Active = true;
                }

                // add display tile to the active set
                active.Add(displayTile);
            }

            // remaining last active display tiles become inactive
            foreach (var last in _lastActive)
            {
                last.Active = false;
            }

            // swap over the active tile sets
            _lastActive = active;
        }

        private void HideLod(int lod)
        {
            _availableLods[lod].Hide();
        }

        private void ShowLod(int lod)
        {
            _availableLods[lod].Show();
        }

        private void UpdatePosition()
        {
            _dynamicManager.Transform.LocalPosition = Vector3d.Relative(
                Transform.LocalPosition, GroundTransform.LocalPosition, InverseScale);
            _lodTree.Transform.LocalPosition = Vector3d.Relative(
                Transform.LocalPosition, GroundTransform.LocalPosition, InverseScale);
        }

        private void UpdateLodTree()
        {
            _lodTree.OnUpdate();

            foreach (var lastActive in _lastActive)
            {
                lastActive.OnUpdate();
            }
        }

        private void OnAppearanceChanged(TiledMapAppearance mapAppearance)
        {
            var lodAppearance = mapAppearance[Lod];

            foreach (var active in _lastActive)
            {
                active.Appearance = lodAppearance;
            }
        }
    }
}