using System;
using System.Reflection;
using UnityEngine;

namespace Maps.Unity.UI
{
    /// <summary>
    /// Responsible for holding information on user interface prefabs
    /// </summary>
    public sealed class PrefabModel : MonoBehaviour
    {
        /// <summary>
        /// The icon prefab
        /// </summary>
        public Transform IconPrefab
        {
            get => _iconPrefab;
            set => _iconPrefab = value;
        }

        /// <summary>
        /// The label prefab
        /// </summary>
        public Transform LabelPrefab
        {
            get => _labelPrefab;
            set => _labelPrefab = value;
        }

        /// <summary>
        /// The sprite prefab
        /// </summary>
        public Transform SpritePrefab
        {
            get => _spritePrefab;
            set => _spritePrefab = value;
        }

        [SerializeField, Obfuscation(Feature = "renaming", Exclude = true)]
        private Transform _iconPrefab;
        [SerializeField, Obfuscation(Feature = "renaming", Exclude = true)]
        private Transform _labelPrefab;
        [SerializeField, Obfuscation(Feature = "renaming", Exclude = true)]
        private Transform _spritePrefab;

        /// <summary>
        /// Initializes the UIPrefabModel instance
        /// </summary>
        public void Initialize()
        {
            if (IconPrefab == null)
            {
                throw new ArgumentNullException(nameof(IconPrefab));
            }

            if (LabelPrefab == null)
            {
                throw new ArgumentNullException(nameof(LabelPrefab));
            }

            if (SpritePrefab == null)
            {
                throw new ArgumentNullException(nameof(SpritePrefab));
            }
        }
    }
}