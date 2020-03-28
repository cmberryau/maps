using System;
using Maps.Appearance;
using UnityEngine;

namespace Maps.Unity.Appearance
{
    /// <summary>
    /// Responsible for generating materials based on appearances
    /// </summary>
    public class MaterialGenerator
    {
        /// <summary>
        /// The number of generated materials
        /// </summary>
        public int Count => _materialMap.Count;

        private readonly MaterialMap _materialMap;

        /// <summary>
        /// Initializes a new instance of MaterialGenerator
        /// </summary>
        /// <param name="base2d">The base material for 2d objects</param>
        /// <param name="base3d">The base material for 3d objects</param>
        /// <param name="baseui">The base material for ui objects</param>
        public MaterialGenerator(Material base2d, Material base3d, Material baseui)
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

            _materialMap = new MaterialMap(base2d, base3d, baseui);
        }

        /// <summary>
        /// Generates a material based on an appearance
        /// </summary>
        /// <param name="appearance">The appearance to generate a material for</param>
        /// <returns>The generated material</returns>
        public Material Generate(MeshAppearance appearance)
        {
            if (appearance == null)
            {
                throw new ArgumentNullException(nameof(appearance));
            }

            Material material;

            if (appearance.Flat)
            {
                material = _materialMap.Material2D(appearance.ZIndex, 
                    appearance.MainColor);
            }
            else
            {
                material = _materialMap.Material3D(appearance.MainColor);
            }

            return material;
        }
    }
}