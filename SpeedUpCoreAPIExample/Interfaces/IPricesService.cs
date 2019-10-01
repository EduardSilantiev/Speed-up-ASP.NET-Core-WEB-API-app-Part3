using SpeedUpCoreAPIExample.ViewModels;
using System.Threading.Tasks;

namespace SpeedUpCoreAPIExample.Interfaces
{
    public interface IPricesService
    {
        Task<PricesPageViewModel> GetPricesAsync(int productId, int pageIndex, int pageSize);
        Task<bool> IsPriceCachedAsync(int productId);
        Task RemovePriceAsync(int productId);
        Task PreparePricesAsync(int productId);
    }
}