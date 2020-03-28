using System;
using Maps.Unity.Appearance;
using Maps.Unity.UI;

namespace Maps.Unity.Rendering
{
    /// <summary>
    /// Responsible for creating ITranslator instances
    /// </summary>
    public class TranslatorFactory
    {
        private readonly IMaterialsModel _materials;
        private readonly UIElementFactory _uiFactory;
        private readonly IPrefabPool _prefabPool;

        /// <summary>
        /// Creates a new instance of TranslatorFactory
        /// </summary>
        /// <param name="materials">The materials model to use</param>
        /// <param name="uiFactory">The ui element factory</param>
        /// <param name="prefabPool">The prefab pool</param>
        /// <exception cref="ArgumentNullException">Thrown when any argument is null
        /// </exception>
        public TranslatorFactory(IMaterialsModel materials, UIElementFactory uiFactory,
            IPrefabPool prefabPool)
        {
            if (materials == null)
            {
                throw new ArgumentNullException(nameof(materials));
            }

            if (uiFactory == null)
            {
                throw new ArgumentNullException(nameof(uiFactory));
            }

            if (prefabPool == null)
            {
                throw new ArgumentNullException(nameof(prefabPool));
            }

            _materials = materials;
            _uiFactory = uiFactory;
            _prefabPool = prefabPool;
        }

        /// <summary>
        /// Creates a translator
        /// </summary>
        /// <param name="parent">The parent transform</param>
        /// <returns>The created translator</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="parent"/>
        /// is null</exception>
        public ITranslator Create(Transformd parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            return new Translator(_materials, _uiFactory, _prefabPool, parent);
        }
    }
}