using Microsoft.Extensions.Caching.Distributed;
using SpeedUpCoreAPIExample.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeedUpCoreAPIExample.Interfaces
{
    public interface IPricesCacheRepository
    {
        Task<IEnumerable<Price>> GetOrSetValueAsync(string key, Func<Task<IEnumerable<Price>>> valueDelegate,
                                                    DistributedCacheEntryOptions options = null);
        Task<bool> IsValueCachedAsync(string key);
        Task RemoveValueAsync(string key);
    }
}
