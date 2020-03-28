using System;

namespace Maps
{
    /// <summary>
    /// An interface for objects which are poolable by Maps.Unity.Pooling.Pool
    /// </summary>
    public interface IPoolable : IDisposable
    {
        /// <summary>
        /// Called when the poolable instance is added to the pool
        /// </summary>
        void OnAddedToPool();

        /// <summary>
        /// Called when the poolable instance has been returned to a pool
        /// </summary>
        void OnReturnedToPool();

        /// <summary>
        /// Called when the poolable instance has been taken out of a pool
        /// </summary>
        void OnTakenFromPool();
    }
}