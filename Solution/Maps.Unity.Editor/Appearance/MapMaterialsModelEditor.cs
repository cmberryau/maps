using System;
using System.Reflection;
using Maps.Unity.Appearance;
using UnityEditor;
using UnityEngine;

namespace Maps.Unity.Editor.Appearance
{
    /// <summary>
    /// Custom editor for the Maps.Unity.Appearance.MapMaterialsModel class
    /// </summary>
    [CustomEditor(typeof(MaterialsModel))]
    public sealed class MapMaterialsModelEditor : UnityEditor.Editor
    {
        private MaterialsModel _target;

        /// <inheritdoc />
        public override void OnInspectorGUI()
        {
            _target.Base2dMaterial = EditorGUILayout.ObjectField("2D Element Material:",
                _target.Base2dMaterial, typeof(Material), false) as Material;

            _target.Base3dMaterial = EditorGUILayout.ObjectField("3D Element Material:",
                _target.Base3dMaterial, typeof(Material), false) as Material;

            _target.BaseUIMaterial = EditorGUILayout.ObjectField("UI Element Material:",
                _target.BaseUIMaterial, typeof(Material), false) as Material;
        }

        [Obfuscation(Feature = "renaming", Exclude = true)]
        private void OnEnable()
        {
            if (target == null)
            {
                throw new NullReferenceException(nameof(target));
            }

            if (target is MaterialsModel)
            {
                _target = (MaterialsModel)target;
            }
            else
            {
                throw new InvalidOperationException(
                    $"{nameof(MapMaterialsModelEditor)} shouldn\'t be used for" +
                    " other classes");
            }
        }
    }
}