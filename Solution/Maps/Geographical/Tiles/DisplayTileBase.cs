using System;
using Maps.Geographical.Features;
using System.Collections.Generic;
using log4net;
using Maps.Appearance;
using Maps.Rendering;

namespace Maps.Geographical.Tiles
{
    /// <summary>
    /// Responsible for allowing control display of a single map tile, which involves 
    /// managing the appearances and renderables for the tile and pushing them down 
    /// to implementing classes
    /// </summary>
    public abstract class DisplayTileBase : IDisplayTile
    {
        /// <inheritdoc />
        public bool Active
        {
            get => _active;
            set
            {
                if (value && !_active)
                {
                    _active = true;
                    OnBecameActive();
                }
                else if (!value && _active)
                {
                    _active = false;
                    OnBecameInactive();
                }
            }
        }

        /// <inheritdoc />
        public bool HasFeatures
        {
            get
            {
                lock (_featuresLock)
                {
                    return _features != null;
                }
            }
        }

        /// <inheritdoc />
        public IMapAppearance Appearance
        {
            get => _activeAppearance;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(Appearance));
                }

                if (!value.Equals(_activeAppearance))
                {
                    //Log.Info($"Tile {this} changed appearance");
                    SetAppearance(value);
                }
            }
        }

        /// <summary>
        /// Transform of the display tile
        /// </summary>
        protected readonly Transformd Transform;

        private static readonly ILog Log = LogManager.GetLogger(typeof(DisplayTileBase));

        // core
        private bool _active;
        private readonly Tile _tile;

        // positioning of the tile
        private readonly Transformd _anchor;
        private readonly double _scale;
        private readonly Vector3d _position;

        // features and appearance
        private IList<Feature> _features;
        private readonly object _featuresLock;

        private IMapAppearance _activeAppearance;
        private readonly object _activeAppearanceLock;
        private readonly IDictionary<IMapAppearance, IList<Renderable>> _renderables;

        /// <summary>
        /// Initializes a new DisplayTileBase instance
        /// </summary>
        /// <param name="appearance">The initial appearance</param>
        /// <param name="tile">The tile</param>
        /// <param name="scale">The scale</param>
        /// <param name="parent">The parent transform</param>
        /// <param name="anchor">The anchor transform</param>
        protected DisplayTileBase(IMapAppearance appearance, Tile tile, double scale,
            Transformd parent, Transformd anchor)
        {
            if (appearance == null)
            {
                throw new ArgumentNullException(nameof(appearance));
            }

            if (tile == null)
            {
                throw new ArgumentNullException(nameof(tile));
            }

            if (anchor == null)
            {
                throw new ArgumentNullException(nameof(anchor));
            }

            if (parent == null)
            {
                 throw new ArgumentNullException(nameof(parent));
            }

            var a = appearance.Projection.Forward(tile.Box.A);
            var b = appearance.Projection.Forward(tile.Box.B);

            _position = Vector3d.Midpoint(a, b);
            Transform = new Transformd(Vector3d.Relative(anchor.Position, _position, 
                scale));
            Transform.SetParent(parent);

            _renderables = new Dictionary<IMapAppearance, IList<Renderable>>();
            _featuresLock = new object();
            _activeAppearanceLock = new object();

            _activeAppearance = appearance;
            _scale = scale;
            _anchor = anchor;
            _tile = tile;
        }

        /// <inheritdoc />
        public void SetFeatures(IList<Feature> features)
        {
            if (features == null)
            {
                throw new ArgumentNullException(nameof(features));
            }

            if (_features == null)
            {
                lock (_featuresLock)
                {
                    _features = features;
                }

                lock (_activeAppearanceLock)
                {
                    // tile is active and we have an appearance
                    if (_activeAppearance != null)
                    {
                        // create the renderables
                        _renderables[_activeAppearance] = Renderables(_features,
                            _activeAppearance, _position, _scale, _tile);

                        // notify clients that new renderables are ready
                        if (_renderables[_activeAppearance].Count > 0)
                        {
                            OnRenderablesReady(_renderables[_activeAppearance],
                                _activeAppearance);
                        }
                    }
                }
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return _tile.ToString();
        }

        /// <inheritdoc />
        public void OnUpdate()
        {
            UpdateTransform();
        }

        /// <summary>
        /// Called when the display tile becomes active
        /// </summary>
        protected abstract void OnBecameActive();

        /// <summary>
        /// Called when the display tile becomes inactive
        /// </summary>
        protected abstract void OnBecameInactive();

        /// <summary>
        /// Called when the active appearance has changed
        /// </summary>
        /// <param name="appearance">The new appearance</param>
        protected abstract void OnMapAppearanceChanged(IMapAppearance appearance);

        /// <summary>
        /// Called when renderables are ready
        /// </summary>
        /// <param name="renderables">The renderable that are ready</param>
        /// <param name="appearance">The map appearance</param>
        protected abstract void OnRenderablesReady(IList<Renderable> renderables,
            IMapAppearance appearance);

        /// <summary>
        /// Called when the transform of the display tile should change
        /// </summary>
        /// <param name="transform">The new transform the tile should have</param>
        protected abstract void OnShouldAssumeTransform(Transformd transform);

        private void SetAppearance(IMapAppearance appearance)
        {
            lock (_activeAppearanceLock)
            {
                _activeAppearance = appearance;
            }

            // no renderables yet for this appearance
            if (!_renderables.ContainsKey(appearance) || _renderables.ContainsKey(
                appearance) && _renderables[appearance] == null)
            {
                // active appearance and tile is active, create renderables
                lock (_featuresLock)
                {
                    // features exist to create renderables, go for it!
                    if (_features != null)
                    {
                        _renderables[appearance] = Renderables(_features, appearance,
                            _position, _scale, _tile);

                        if (_renderables[appearance].Count > 0)
                        {
                            OnRenderablesReady(_renderables[appearance], appearance);
                        }

                        return;
                    }
                }
            }
            else
            {
                OnMapAppearanceChanged(appearance);
            }

            // no features or not active, save null entries
            _renderables[appearance] = null;
        }

        private static IList<Renderable> Renderables(IList<Feature> features,
            IMapAppearance appearance, Vector3d position, double scale, Tile tile)
        {
            //Log.Info($"{tile} - create renderables for {features.Count} features");

            var renderables = new List<Renderable>();

            // add the tile renderable
            var tileRenderables = tile.Area.RenderablesFor(appearance, position, scale);
            for (var i = 0; i < tileRenderables.Count; i++)
            {
                renderables.Add(tileRenderables[i]);
            }

            var featureCount = features.Count;
            // get renderables for each feature
            for (var i = 0; i < featureCount; i++)
            {
                try
                {
                    var renderablesFor = features[i].RenderablesFor(appearance, 
                        position, scale);

                    for (var k = 0; k < renderablesFor.Count; k++)
                    {
                        renderables.Add(renderablesFor[k]);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }

            return renderables;
        }

        private void UpdateTransform()
        {
            Transform.LocalPosition = Vector3d.Relative(_anchor.Position, _position, 
                _scale);
            OnShouldAssumeTransform(Transform);
        }
    }
}