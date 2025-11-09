using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Algorithms.Refactor
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    public class SemaphoreCache<TKey, TValue> :IDisposable where TKey : notnull // Dispose pattern added with null checks
    {
        private readonly ConcurrentDictionary<TKey, CacheItem<TValue>> _cache = new();
        private readonly ConcurrentDictionary<TKey, SemaphoreSlim> _key = new();//Per-key semaphore
        private readonly TimeSpan _defaultTtl;
        private readonly SemaphoreSlim _semaphore = new(1, 1);
        private bool _disposed;

        public SemaphoreCache(TimeSpan? defaultTtl = null)
        {
            _defaultTtl = defaultTtl ?? TimeSpan.FromMinutes(5);
        }

        // Dispose pattern implementation
        public void Dispose()
        {
            if(_disposed)
            {
                return;
            }

            _disposed = true;
            _cache.Clear();

            foreach(var semaphore in _key.Values)
            {
                semaphore.Dispose();
            }

            _key.Clear();
        }

        public async Task<TValue> GetOrAddAsync(
            TKey key,
            Func<Task<TValue>> valueFactory,
            TimeSpan? ttl = null,
            CancellationToken cancellationToken = default) // Added cancellationToken parameter
        {
            if (_cache.TryGetValue(key, out var existingItem) && !existingItem.IsExpired)
            {
                return existingItem.Value;
            }

            var semaphore = _key.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
            await _semaphore.WaitAsync(cancellationToken);
            try
            {
                if (_cache.TryGetValue(key, out existingItem) && !existingItem.IsExpired)
                {
                    return existingItem.Value;
                }

                var value = await valueFactory();
                var expiration = DateTime.UtcNow + (ttl ?? _defaultTtl);

                _cache[key] = new CacheItem<TValue>(value, expiration);
                return value;
            }
            finally
            {
                _semaphore.Release();

                if(_semaphore.CurrentCount == 1)
                {
                    _key.TryRemove(key, out _);
                }
            }
        }

        public void Invalidate(TKey key)
        {
            _cache.TryRemove(key, out _);
        }

        private class CacheItem<T>
        {
            public T Value { get; }
            public DateTime Expiration { get; }

            public CacheItem(T value, DateTime expiration)
            {
                Value = value;
                Expiration = expiration;
            }

            public bool IsExpired => DateTime.UtcNow > Expiration;
        }
    }

}
