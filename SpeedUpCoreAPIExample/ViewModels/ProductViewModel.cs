using SpeedUpCoreAPIExample.Models;

namespace SpeedUpCoreAPIExample.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }

        public ProductViewModel() {}

        public ProductViewModel(int id, string sku, string name)
        {
            Id = id;
            Sku = sku;
            Name = name;
        }

        public ProductViewModel(Product product)
        {
            Id = product.ProductId;
            Sku = product.Sku;
            Name = product.Name;
        }
    }
}