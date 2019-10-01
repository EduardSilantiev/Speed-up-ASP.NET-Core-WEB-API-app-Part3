using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SpeedUpCoreAPIExample.Interfaces;
using System;
using System.Threading.Tasks;

namespace SpeedUpCoreAPIExample.Repositories
{
    /// <summary>
    /// Generic DistributedCache repository class
    /// </summary>
    public abstract class DistributedCacheRepository<T> : IDistributedCacheRepository<T> where T : class
    {
        private readonly IDistributedCache _distributedCache;
        private readonly string _keyPrefix;

        protected DistributedCacheRepository(IDistributedCache distributedCache, string keyPrefix)
        {
            _distributedCache = distributedCache;
            _keyPrefix = keyPrefix;
        }

        /// <summary>
        /// Get a value from the cache, or by calling an asynchronous delegate and then caching the value
        /// </summary>
        public virtual async Task<T> GetOrSetValueAsync(string key, Func<Task<T>> valueDelegate, DistributedCacheEntryOptions options)
        {
            var value = await GetValueAsync(key);
            if (value == null)
            {
                //not in cache, get a value, calling delegate
                value = await valueDelegate();

                if (value != null)
                    await SetValueAsync(key, value, options ?? GetDefaultOptions());
            }

            return value;
        }

        public async Task<bool> IsValueCachedAsync(string key)
        {
            var value = await _distributedCache.GetStringAsync(_keyPrefix + key);

            return value != null;
        }

        public async Task<T> GetValueAsync(string key)
        {
            var value = await _distributedCache.GetStringAsync(_keyPrefix + key);

            return value != null ? JsonConvert.DeserializeObject<T>(value) : null;
        }

        public virtual async Task SetValueAsync(string key, T value, DistributedCacheEntryOptions options)
        {
            await _distributedCache.SetStringAsync(_keyPrefix + key, JsonConvert.SerializeObject(value), options ?? GetDefaultOptions());
        }

        public async Task RemoveValueAsync(string key)
        {
            await _distributedCache.RemoveAsync(_keyPrefix + key);
        }

        protected abstract DistributedCacheEntryOptions GetDefaultOptions();
    }
}
