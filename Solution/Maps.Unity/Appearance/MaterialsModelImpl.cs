using System;
using System.Collections.Generic;
using Maps.Appearance;
using UnityEngine;

namespace Maps.Unity.Appearance
{
    /// <summary>
    /// Responsible for the implementation of providing materials for rendering maps 
    /// in Unity3d
    /// </summary>
    internal sealed class MaterialsModelImpl : IMaterialsModel
    {
        private const int Base2dQueue = 2000;
        private const int Base3dQueue = 2500;
        private const int BaseUIQueue = 3000;

        private readonly IDictionary<MeshAppearance, Material> _map;
        private readonly MaterialGenerator _generator;

        /// <summary>
        /// Initializes a new instance of MapMaterialsModelImpl
        /// </summary>
        /// <param name="appearances">The tiled map appearances</param>
        /// <param name="base2d">The material to use for 2d objects</param>
        /// <param name="base3d">The material to use for 3d objects</param>
        /// <param name="baseui">The material to use for ui objects</param>
        public MaterialsModelImpl(IList<MeshAppearance> appearances,
            Material base2d, Material base3d, Material baseui)
        {
            if (appearances == null)
            {
                throw new ArgumentNullException(nameof(appearances));
            }

            if (base2d == null)
            {
                throw new ArgumentNullException(nameof(base2d));
            }

            base2d.renderQueue = Base2dQueue;
            base3d.renderQueue = Base3dQueue;
            baseui.renderQueue = BaseUIQueue;

            _generator = new MaterialGenerator(base2d, base3d, baseui);
            _map = GenerateMaterials(appearances, _generator);

            Debug.Log($"Total of {_generator.Count} materials generated");
        }

        /// <inheritdoc />
        public Material MaterialFor(MeshAppearance appearance)
        {
            if (appearance == null)
            {
                throw new ArgumentNullException(nameof(appearance));
            }

            var lastCount = _generator.Count;

            if (!_map.ContainsKey(appearance))
            {
                _map[appearance] = _generator.Generate(appearance);

                if (_generator.Count != lastCount)
                {
                    Debug.Log($"Additional material generated, {_generator.Count} in " +
                              "total");
                }
            }

            return _map[appearance];
        }

        private static IDictionary<MeshAppearance, Material> GenerateMaterials(
            IList<MeshAppearance> appearances, MaterialGenerator generator)
        {
            var map = new Dictionary<MeshAppearance, Material>();
            var appearancesCount = appearances.Count;

            for (var i = 0; i < appearancesCount; ++i)
            {
                if (!map.ContainsKey(appearances[i]))
                {
                    var material = generator.Generate(appearances[i]);
                    map.Add(appearances[i], material);
                }
            }

            return map;
        }
    }
}