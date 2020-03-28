using System;
using System.Collections.Generic;
using Maps.Unity.Extensions;
using UnityEngine;

namespace Maps.Unity.Appearance
{
    /// <summary>
    /// Responsible for maintaining mapping between material properties and materials
    /// </summary>
    public class MaterialMap
    {
        /// <summary>
        /// The total number of materials
        /// </summary>
        public int Count
        {
            get;
            private set;
        }

        private readonly IDictionary<Colorf, Material> _map3d;
        private readonly IDictionary<int, IDictionary<Colorf, Material>> _map2d;

        private readonly Material _base2d;
        private readonly Material _base3d;
        private readonly Material _baseui;

        /// <summary>
        /// Initializes a new instance of MaterialMap
        /// </summary>
        /// <param name="base2d">The base material for 2d objects</param>
        /// <param name="base3d">The base material for 3d objects</param>
        /// <param name="baseui">The base material for ui objects</param>
        public MaterialMap(Material base2d, Material base3d, Material baseui)
        {
            if (base2d == null)
            {
                throw new ArgumentNullException(nameof(base2d));
            }

            if (base3d == null)
            {
                throw new ArgumentNullException(nameof(base3d));
            }

            if (baseui == null)
            {
                throw new ArgumentNullException(nameof(baseui));
            }

            _map3d = new Dictionary<Colorf, Material>();
            _map2d = new Dictionary<int, IDictionary<Colorf, Material>>();

            _base2d = base2d;
            _base3d = base3d;
            _baseui = baseui;
            Count = 1;
        }

        /// <summary>
        /// Returns a 2D object material
        /// </summary>
        /// <param name="z">The required z index</param>
        /// <param name="mainColor">The required main color</param>
        public Material Material2D(int z, Colorf mainColor)
        {
            if (!_map2d.ContainsKey(z))
            {
                _map2d[z] = new Dictionary<Colorf, Material>();
            }

            if (!_map2d[z].ContainsKey(mainColor))
            {
                _map2d[z][mainColor] = new Material(_base2d)
                {
                    renderQueue = _base2d.renderQueue + z,
                    color = mainColor.Color(),
                };

                Count++;
            }

            return _map2d[z][mainColor];
        }

        /// <summary>
        /// Returns a 3D object material
        /// </summary>
        /// <param name="mainColor">The required main color</param>
        public Material Material3D(Colorf mainColor)
        {
            if (!_map3d.ContainsKey(mainColor))
            {
                _map3d[mainColor] = new Material(_base3d)
                {
                    color = mainColor.Color(),
                };

                Count++;
            }

            return _map3d[mainColor];
        }

        /// <summary>
        /// Returns a UI object material
        /// </summary>
        public Material MaterialUI()
        {
            return _baseui;
        }
    }
}