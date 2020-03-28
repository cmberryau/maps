using System;
using System.Reflection;
using Maps.Unity.UI;
using UnityEditor;
using UnityEngine;

namespace Maps.Unity.Editor.Pooling
{
    /// <summary>
    /// Custom editor for the Maps.Unity.UI.UIPrefabModel class
    /// </summary>
    [CustomEditor(typeof(PrefabModel))]
    public class UIPrefabModelEditor : UnityEditor.Editor
    {
        private PrefabModel _target;

        /// <inheritdoc />
        public override void OnInspectorGUI()
        {
            _target.IconPrefab = EditorGUILayout.ObjectField("Icon Prefab:",
                _target.IconPrefab, typeof(Transform), false) as Transform;

            _target.LabelPrefab = EditorGUILayout.ObjectField("Label Prefab:",
                _target.LabelPrefab, typeof(Transform), false) as Transform;

            _target.SpritePrefab = EditorGUILayout.ObjectField("Sprite Prefab:",
                _target.SpritePrefab, typeof(Transform), false) as Transform;
        }

        [Obfuscation(Feature = "renaming", Exclude = true)]
        private void OnEnable()
        {
            if (target == null)
            {
                throw new NullReferenceException(nameof(target));
            }

            if (target is PrefabModel)
            {
                _target = (PrefabModel)target;
            }
            else
            {
                throw new InvalidOperationException($"{nameof(UIPrefabModelEditor)} " +
                    "shouldn't be used for other classes");
            }
        }
    }
}