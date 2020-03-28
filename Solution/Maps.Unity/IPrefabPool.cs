using Maps.Unity.UI;

namespace Maps.Unity
{
    /// <summary>
    /// Read-only Interface to wrap up the prefab pool
    /// </summary>
    public interface IPrefabPool
    {
        /// <summary>
        /// The icon pool
        /// </summary>
        IPool<PoolableIcon> IconPool
        {
            get;
        }

        /// <summary>
        /// The icon pool
        /// </summary>
        IPool<PoolableLabel> LabelPool
        {
            get;
        }

        /// <summary>
        /// The sprite pool
        /// </summary>
        IPool<PoolableSprite> SpritePool
        {
            get;
        }
    }
}