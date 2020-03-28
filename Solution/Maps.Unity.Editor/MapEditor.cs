//using System;
//using System.Reflection;
//using UnityEditor;
//using UnityEngine;

//namespace Maps.Unity.Editor
//{
//    /// <summary>
//    /// Custom editor for the Maps.Unity.Map class
//    /// </summary>
//    [CustomEditor(typeof(Map))]
//    public sealed class MapEditor : UnityEditor.Editor
//    {
//        private Map _target;

//        /// <inheritdoc />
//        public override void OnInspectorGUI()
//        {
//            // track any changes in the inspector
//            EditorGUI.BeginChangeCheck();

//            _target.InitialLatitude = EditorGUILayout.Slider("Initial Latitude",
//                (float)_target.InitialLatitude, -90f, 90f);
//            _target.InitialLongitude = EditorGUILayout.Slider("Initial Longitude",
//                (float)_target.InitialLongitude, -180f, 180f);
//            _target.InitialHeight = EditorGUILayout.Slider("Initial Height",
//                (float)_target.InitialHeight, 0, 10000f);
//            _target.InitialHeading = EditorGUILayout.Slider("Initial Heading",
//                (float)_target.InitialHeading, 0, 360f);

//            // if any changes made, the camera should update
//            if (EditorGUI.EndChangeCheck())
//            {
//                if (_target != null && Application.isPlaying)
//                {
//                    _target.Reset();
//                }
//            }
//        }

//        [Obfuscation(Feature = "renaming", Exclude = true)]
//        private void OnEnable()
//        {
//            if (target == null)
//            {
//                throw new NullReferenceException(nameof(target));
//            }

//            if (target is Map)
//            {
//                _target = (Map)target;
//            }
//            else
//            {
//                throw new InvalidOperationException($"{nameof(MapEditor)} " +
//                    "shouldn't be used for other classes");
//            }
//        }
//    }
//}