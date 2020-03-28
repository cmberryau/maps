using System;
using System.Collections.Concurrent;

namespace Maps
{
    /// <summary>
    /// Responsible for the implementation of pooling object instances
    /// </summary>
    public sealed class Pool<T> : IPool<T> where T : class, IPoolable
    {
        private readonly ConcurrentQueue<T> _readyInstances;
        private readonly Func<T> _createFunc;

        /// <summary>
        /// Initializes a new instance of PoolImpl
        /// </summary>
        /// <param name="createFunc">The creation function for instances</param>
        /// <param name="minInstances">The minimum number of instances</param>
        public Pool(Func<T> createFunc, int minInstances = 0)
        {
            if (createFunc == null)
            {
                throw new ArgumentNullException(nameof(createFunc));
            }

            if (minInstances < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minInstances));
            }

            _readyInstances = CreateQueue(createFunc, minInstances);
            _createFunc = createFunc;
        }

        /// <inheritdoc />
        public T Borrow()
        {
            if (!_readyInstances.TryDequeue(out T instance))
            {
                instance = _createFunc();
                if (instance == null)
                {
                    throw new InvalidOperationException("Create function returned null");
                }

                instance.OnAddedToPool();
            }

            instance.OnTakenFromPool();
            return instance;
        }

        /// <inheritdoc />
        public void Return(T instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            
            _readyInstances.Enqueue(instance);
            instance.OnReturnedToPool();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            while (_readyInstances.TryDequeue(out T instance))
            {
                if (instance != null)
                {
                    instance.Dispose();
                }
            }
        }

        private static ConcurrentQueue<T> CreateQueue(Func<T> createFunc, int count)
        {
            var queue = new ConcurrentQueue<T>();

            for (var i = 0; i < count; ++i)
            {
                var instance = createFunc();
                if (instance == null)
                {
                    throw new InvalidOperationException("Create function returned null");
                }

                instance.OnAddedToPool();
                queue.Enqueue(instance);
            }

            return queue;
        }
    }
}