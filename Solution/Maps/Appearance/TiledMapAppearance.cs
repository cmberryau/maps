using System;
using System.Collections.Generic;
using Maps.Geographical.Features;
using Maps.Rendering;

namespace Maps.Appearance
{
    /// <summary>
    /// Responsible for providing information on how to display a tiled map
    /// </summary>
    public class TiledMapAppearance
    {
        /// <summary>
        /// Index accessor for map appearances at each lod
        /// </summary>
        /// <param name="index">The lod index</param>
        public IMapAppearance this[int index] => _lods[index];

        /// <summary>
        /// The default day map appearance
        /// </summary>
        public static TiledMapAppearance DefaultDay
        {
            get
            {
                var lods = new Dictionary<int, IMapAppearance>();

                for (var i = 10; i < 17; ++i)
                {
                    lods[i] = DefaultMapAppearance.Create(i, true);
                }

                return new TiledMapAppearance(lods);
            }
        }

        /// <summary>
        /// The default night map appearance
        /// </summary>
        public static TiledMapAppearance DefaultNight
        {
            get
            {
                var lods = new Dictionary<int, IMapAppearance>();

                for (var i = 10; i < 17; ++i)
                {
                    lods[i] = DefaultMapAppearance.Create(i, false);
                }

                return new TiledMapAppearance(lods);
            }
        }

        /// <summary>
        /// All unique feature appearances
        /// </summary>
        public IList<FeatureAppearance> FeatureAppearances
        {
            get;
        }

        /// <summary>
        /// All unique mesh appearances
        /// </summary>
        public IList<MeshAppearance> MeshAppearances
        {
            get;
        }

        /// <summary>
        /// All unique ui element appearances
        /// </summary>
        public IList<UIRenderableAppearance> UIElementAppearances
        {
            get;
        }

        private IDictionary<int, IMapAppearance> _lods;

        /// <summary>
        /// Initializes a new instance of MapAppearanceBase
        /// </summary>
        /// <param name="lods">The map appearances per lod</param>
        public TiledMapAppearance(IDictionary<int, IMapAppearance> lods)
        {
            if (lods == null)
            {
                throw new ArgumentNullException(nameof(lods));
            }

            _lods = lods;

            // resolve all unique feature and renderable appearances for all lods
            var featureAppearanceMap = new HashSet<FeatureAppearance>();
            FeatureAppearances = new List<FeatureAppearance>();
            var renderableAppearanceMap = new HashSet<RenderableAppearance>();
            MeshAppearances = new List<MeshAppearance>();
            var uiAppearanceMap = new HashSet<UIRenderableAppearance>();
            UIElementAppearances = new List<UIRenderableAppearance>();

            // build the unique appearance lists for all lods
            foreach (var lod in lods)
            {
                if (lod.Value == null)
                {
                    throw new ArgumentException("Contains a null value at key " +
                                                $"{lod.Key}");
                }

                foreach (var featureAppearance in lod.Value.FeatureAppearances)
                {
                    if (featureAppearanceMap.Add(featureAppearance))
                    {
                        FeatureAppearances.Add(featureAppearance);
                    }
                }

                foreach (var renderableAppearance in lod.Value.MeshApperances)
                {
                    if (renderableAppearanceMap.Add(renderableAppearance))
                    {
                        MeshAppearances.Add(renderableAppearance);
                    }
                }

                foreach (var uiAppearance in lod.Value.UIElementAppearances)
                {
                    if (uiAppearanceMap.Add(uiAppearance))
                    {
                        UIElementAppearances.Add(uiAppearance);
                    }
                }
            }
        }
    }
}