using Maps.Appearance;

namespace Maps.Unity.UI
{
    /// <summary>
    /// An interface for poolable icon control
    /// </summary>
    public interface IPoolableIcon : IIconAppearance
    {
        /// <summary>
        /// The sprite of the icon
        /// </summary>
        UnityEngine.Sprite Sprite
        {
            get;
            set;
        }

        /// <summary>
        /// The appearance of the icon
        /// </summary>
        IIconAppearance Appearance
        {
            get;
            set;
        }
    }
}