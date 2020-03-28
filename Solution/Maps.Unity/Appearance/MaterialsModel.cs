using System;
using System.Collections.Generic;
using System.Reflection;
using Maps.Appearance;
using UnityEngine;

namespace Maps.Unity.Appearance
{
    /// <summary>
    /// Responsible for providing materials for rendering maps in Unity3d
    /// </summary>
    public sealed class MaterialsModel : MonoBehaviour, IMaterialsModel
    {
        /// <summary>
        /// The feature material
        /// </summary>
        public Material Base2dMaterial
        {
            get => _base2d;
            set => _base2d = value;
        }

        /// <summary>
        /// The feature material
        /// </summary>
        public Material Base3dMaterial
        {
            get => _base3d;
            set => _base3d = value;
        }

        /// <summary>
        /// The feature material
        /// </summary>
        public Material BaseUIMaterial
        {
            get => _baseui;
            set => _baseui = value;
        }

        /// <inheritdoc />
        public Material MaterialFor(MeshAppearance appearance)
        {
            return _impl.MaterialFor(appearance);
        }

        [SerializeField, Obfuscation(Feature = "renaming", Exclude = true)]
        private Material _base2d;

        [SerializeField, Obfuscation(Feature = "renaming", Exclude = true)]
        private Material _base3d;

        [SerializeField, Obfuscation(Feature = "renaming", Exclude = true)]
        private Material _baseui;

        private IMaterialsModel _impl;

        /// <summary>
        /// Initializes the MapMaterialsModel instance
        /// </summary>
        /// <param name="appearances">The appearances to initialize from</param>
        public void Initialize(IList<MeshAppearance> appearances)
        {
            if (appearances == null)
            {
                throw new ArgumentNullException(nameof(appearances));
            }

            if (Base2dMaterial == null)
            {
                throw new ArgumentNullException(nameof(Base2dMaterial));
            }

            if (Base3dMaterial == null)
            {
                throw new ArgumentNullException(nameof(Base2dMaterial));
            }

            if (BaseUIMaterial == null)
            {
                throw new ArgumentNullException(nameof(BaseUIMaterial));
            }

            _impl = new MaterialsModelImpl(appearances, _base2d, _base3d, _baseui);
        }
    }
}