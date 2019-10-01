using SpeedUpCoreAPIExample.ViewModels;
using System.Threading.Tasks;

namespace SpeedUpCoreAPIExample.Interfaces
{
    public interface IProductsService
    {
        Task<ProductsPageViewModel> GetAllProductsAsync(int pageIndex, int pageSize);
        Task<ProductViewModel> GetProductAsync(int productId);
        Task<ProductsPageViewModel> FindProductsAsync(string sku, int pageIndex, int pageSize);
        Task<ProductViewModel> DeleteProductAsync(int productId);
    }
}