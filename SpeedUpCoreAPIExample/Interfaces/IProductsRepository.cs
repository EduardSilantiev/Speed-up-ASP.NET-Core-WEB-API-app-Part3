using SpeedUpCoreAPIExample.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedUpCoreAPIExample.Interfaces
{
    public interface IProductsRepository
    {
        IQueryable<Product> GetAllProductsAsync();
        Task<Product> GetProductAsync(int productId);
        IQueryable<Product> FindProductsAsync(string sku);
        Task<Product> DeleteProductAsync(int productId);
    }
}