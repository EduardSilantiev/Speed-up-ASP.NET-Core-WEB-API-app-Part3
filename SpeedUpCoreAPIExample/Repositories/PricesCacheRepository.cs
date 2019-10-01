using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using SpeedUpCoreAPIExample.Interfaces;
using SpeedUpCoreAPIExample.Models;
using SpeedUpCoreAPIExample.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeedUpCoreAPIExample.Repositories
{
    public class PricesCacheRepository : DistributedCacheRepository<IEnumerable<Price>>, IPricesCacheRepository
    {
        private const string KeyPrefix = "Prices: ";
        private readonly PricesSettings _settings;

        public PricesCacheRepository(IDistributedCache distributedCache, IOptions<PricesSettings> settings)
                      : base(distributedCache, KeyPrefix)
        {
            _settings = settings.Value;
        }

        public override async Task<IEnumerable<Price>> GetOrSetValueAsync(string key, Func<Task<IEnumerable<Price>>> valueDelegate,
                                                                            DistributedCacheEntryOptions options = null)
        {
            //call the base class' method with the clarified caching options
            return await base.GetOrSetValueAsync(key, valueDelegate, options);
        }

        protected override DistributedCacheEntryOptions GetDefaultOptions()
        {
            //use default caching options for the class if they are not defined in options parameter
            return new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_settings.CachingExpirationPeriod)
            };
        }
    }
}
