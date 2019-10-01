using Microsoft.EntityFrameworkCore;
using SpeedUpCoreAPIExample.Contexts;
using SpeedUpCoreAPIExample.Interfaces;
using SpeedUpCoreAPIExample.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedUpCoreAPIExample.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly DefaultContext _context;

        public ProductsRepository(DefaultContext context)
        {
            _context = context;
        }

        public IQueryable<Product> GetAllProductsAsync()
        {
            return _context.Products.AsNoTracking();
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            return await _context.Products.AsNoTracking().Where(p => p.ProductId == productId).FirstOrDefaultAsync();
        }

        public IQueryable<Product> FindProductsAsync(string sku)
        {
            return _context.Products.AsNoTracking().FromSql("[dbo].GetProductsBySKU @sku = {0}", sku);
        }

        public async Task<Product> DeleteProductAsync(int productId)
        {
            Product product = await GetProductAsync(productId);

            if (product != null)
            {
                _context.Products.Remove(product);

                await _context.SaveChangesAsync();
            }

            return product;
        }
    }
}