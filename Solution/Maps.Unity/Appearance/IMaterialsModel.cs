using Maps.Appearance;
using UnityEngine;

namespace Maps.Unity.Appearance
{
    /// <summary>
    /// Interface to provide access to materials used for map rendering in Unity3D
    /// </summary>
    public interface IMaterialsModel
    {
        /// <summary>
        /// Returns the material for the given appearance
        /// </summary>
        /// <param name="appearance">The appearance to evaluate</param>
        Material MaterialFor(MeshAppearance appearance);
    }
}