using System;
using System.Collections.Generic;
using Maps.Extensions;
using Maps.Geographical.Features;
using Maps.Geographical.Places;
using Maps.Geographical.Projection;
using Maps.Rendering;

namespace Maps.Appearance
{
    /// <summary>
    /// Responsible for describing how a map should appear
    /// </summary>
    internal class MapAppearance : IMapAppearance
    {
        /// <inheritdoc />
        public IList<FeatureAppearance> FeatureAppearances
        {
            get;
        }

        /// <inheritdoc />
        public IList<MeshAppearance> MeshApperances
        {
            get;
        }

        /// <inheritdoc />
        public IList<UIRenderableAppearance> UIElementAppearances
        {
            get;
        }

        /// <inheritdoc />
        public IProjection Projection
        {
            get;
        }

        private readonly IList<PlaceAppearanceTarget> _placeTargets;
        private readonly IList<SegmentAppearanceTarget> _segmentTargets;
        private readonly IList<AreaAppearanceTarget> _areaTargets;
        private readonly string _name;

        /// <summary>
        /// Initializes a new instance of MapAppearance
        /// </summary>
        /// <param name="projection">The projection to use</param>
        /// <param name="placeTargets">The place targets</param>
        /// <param name="segmentTargets">The segment targets</param>
        /// <param name="areaTargets">The area targets</param>
        /// <param name="name">The name of the appearance</param>
        public MapAppearance(IProjection projection, IList<PlaceAppearanceTarget> placeTargets,
            IList<SegmentAppearanceTarget> segmentTargets, IList<AreaAppearanceTarget> areaTargets, 
            string name = null)
        {
            if (projection == null)
            {
                throw new ArgumentNullException(nameof(projection));
            }

            if (placeTargets == null)
            {
                throw new ArgumentNullException(nameof(placeTargets));
            }

            if (segmentTargets == null)
            {
                throw new ArgumentNullException(nameof(segmentTargets));
            }

            if (areaTargets == null)
            {
                throw new ArgumentNullException(nameof(areaTargets));
            }

            placeTargets.AssertNoNullEntries();
            segmentTargets.AssertNoNullEntries();
            areaTargets.AssertNoNullEntries();

            Projection = projection;

            _placeTargets = placeTargets;
            _segmentTargets = segmentTargets;
            _areaTargets = areaTargets;

            // resolve all unique feature and renderable appearances
            var featureAppearanceMap = new HashSet<FeatureAppearance>();
            FeatureAppearances = new List<FeatureAppearance>();
            var renderableAppearanceMap = new HashSet<RenderableAppearance>();
            MeshApperances = new List<MeshAppearance>();
            var uiAppearanceMap = new HashSet<RenderableAppearance>();
            UIElementAppearances = new List<UIRenderableAppearance>();

            foreach (var target in placeTargets)
            {
                var appearance = target.Appearance;
                if (featureAppearanceMap.Add(appearance))
                {
                    FeatureAppearances.Add(appearance);

                    var renderableAppearances = appearance.RenderableAppearances;
                    foreach (var renderableAppearance in renderableAppearances)
                    {
                        if (renderableAppearanceMap.Add(renderableAppearance))
                        {
                            MeshApperances.Add(renderableAppearance);
                        }
                    }

                    var uiAppearances = appearance.UIElementAppearances;
                    foreach (var uiAppearance in uiAppearances)
                    {
                        if (uiAppearanceMap.Add(uiAppearance))
                        {
                            UIElementAppearances.Add(uiAppearance);
                        }
                    }
                }
            }

            foreach (var target in segmentTargets)
            {
                var appearance = target.Appearance;
                if (featureAppearanceMap.Add(appearance))
                {
                    FeatureAppearances.Add(appearance);

                    var renderableAppearances = appearance.RenderableAppearances;
                    foreach (var renderableAppearance in renderableAppearances)
                    {
                        if (renderableAppearanceMap.Add(renderableAppearance))
                        {
                            MeshApperances.Add(renderableAppearance);
                        }
                    }

                    var uiAppearances = appearance.UIElementAppearances;
                    foreach (var uiAppearance in uiAppearances)
                    {
                        if (uiAppearanceMap.Add(uiAppearance))
                        {
                            UIElementAppearances.Add(uiAppearance);
                        }
                    }
                }
            }

            foreach (var target in areaTargets)
            {
                var appearance = target.Appearance;
                if (featureAppearanceMap.Add(appearance))
                {
                    FeatureAppearances.Add(appearance);

                    var renderableAppearances = appearance.RenderableAppearances;
                    foreach (var renderableAppearance in renderableAppearances)
                    {
                        if (renderableAppearanceMap.Add(renderableAppearance))
                        {
                            MeshApperances.Add(renderableAppearance);
                        }
                    }

                    var uiAppearances = appearance.UIElementAppearances;
                    foreach (var uiAppearance in uiAppearances)
                    {
                        if (uiAppearanceMap.Add(uiAppearance))
                        {
                            UIElementAppearances.Add(uiAppearance);
                        }
                    }
                }
            }

            _name = name;
        }

        /// <inheritdoc />
        public PlaceAppearance AppearanceFor(Place place)
        {
            if (place == null)
            {
                throw new ArgumentNullException(nameof(place));
            }

            foreach (var tuple in _placeTargets)
            {
                var filter = tuple.Filter;
                if (filter.Filter(place))
                {
                    var appearance = tuple.Appearance;
                    return appearance;
                }
            }

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public SegmentAppearance AppearanceFor(Segment segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            foreach (var tuple in _segmentTargets)
            {
                var filter = tuple.Filter;
                if (filter.Filter(segment))
                {
                    var appearance = tuple.Appearance;
                    return appearance;
                }
            }

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public AreaAppearance AppearanceFor(Area area)
        {
            if (area == null)
            {
                throw new ArgumentNullException(nameof(area));
            }

            foreach (var tuple in _areaTargets)
            {
                var filter = tuple.Filter;
                if (filter.Filter(area))
                {
                    var appearance = tuple.Appearance;
                    return appearance;
                }
            }

            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(_name))
            {
                return _name;
            }

            return base.ToString();
        }
    }
}