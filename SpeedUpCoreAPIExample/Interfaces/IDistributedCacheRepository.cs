using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace SpeedUpCoreAPIExample.Interfaces
{
    public interface IDistributedCacheRepository<T>
    {
        Task<T> GetOrSetValueAsync(string key, Func<Task<T>> valueDelegate, DistributedCacheEntryOptions options);
        Task<bool> IsValueCachedAsync(string key);
        Task<T> GetValueAsync(string key);
        Task SetValueAsync(string key, T value, DistributedCacheEntryOptions options);
        Task RemoveValueAsync(string key);
    }
}
