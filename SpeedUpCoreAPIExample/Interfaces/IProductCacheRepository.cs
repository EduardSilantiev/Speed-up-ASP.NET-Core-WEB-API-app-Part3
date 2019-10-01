using Microsoft.Extensions.Caching.Distributed;
using SpeedUpCoreAPIExample.Models;
using System;
using System.Threading.Tasks;

namespace SpeedUpCoreAPIExample.Interfaces
{
    public interface IProductCacheRepository
    {
        Task<Product> GetOrSetValueAsync(string key, Func<Task<Product>> valueDelegate, DistributedCacheEntryOptions options = null);
        Task<bool> IsValueCachedAsync(string key);
        Task RemoveValueAsync(string key);
        Task SetValueAsync(string key, Product value, DistributedCacheEntryOptions options = null);
    }
}
