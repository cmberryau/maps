using System;

namespace Maps
{
    /// <summary>
    /// Interface for pools of object instances
    /// </summary>
    /// <typeparam name="T">The pooled type</typeparam>
    public interface IPool<T> : IDisposable
    {
        /// <summary>
        /// Borrows an object instance
        /// </summary>
        T Borrow();

        /// <summary>
        /// Returns an instance back to the pool
        /// </summary>
        void Return(T instance);
    }
}