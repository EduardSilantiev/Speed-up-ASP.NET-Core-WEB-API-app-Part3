using Microsoft.Extensions.Options;
using SpeedUpCoreAPIExample.Helpers;
using SpeedUpCoreAPIExample.Interfaces;
using SpeedUpCoreAPIExample.Models;
using SpeedUpCoreAPIExample.Settings;
using SpeedUpCoreAPIExample.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpeedUpCoreAPIExample.Services
{
    public class PricesService : IPricesService
    {
        private readonly IPricesRepository _pricesRepository;
        private readonly IPricesCacheRepository _pricesCacheRepository;
        private readonly PricesSettings _settings;

        public PricesService(IPricesRepository pricesRepository, IPricesCacheRepository pricesCacheRepository,
                             IOptions<PricesSettings> settings)
        {
            _pricesRepository = pricesRepository;
            _pricesCacheRepository = pricesCacheRepository;
            _settings = settings.Value;
        }

        public async Task<PricesPageViewModel> GetPricesAsync(int productId, int pageIndex, int pageSize)
        {
            IEnumerable<Price> prices = await _pricesCacheRepository.GetOrSetValueAsync(productId.ToString(),
                                async () => await _pricesRepository.GetPricesAsync(productId));

            pageSize = pageSize == 0 ? _settings.DefaultPageSize : pageSize;
            return new PricesPageViewModel(new PaginatedList<Price>(prices, pageIndex, pageSize));
        }

        public async Task<bool> IsPriceCachedAsync(int productId)
        {
            return await _pricesCacheRepository.IsValueCachedAsync(productId.ToString());
        }

        public async Task RemovePriceAsync(int productId)
        {
            await _pricesCacheRepository.RemoveValueAsync(productId.ToString());
        }

        public async Task PreparePricesAsync(int productId)
        {
            try
            {
                await _pricesCacheRepository.GetOrSetValueAsync(productId.ToString(),
                                async () => await _pricesRepository.GetPricesAsync(productId));
            }
            catch
            {
            }
        }
    }
}