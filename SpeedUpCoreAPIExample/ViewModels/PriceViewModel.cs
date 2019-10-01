using SpeedUpCoreAPIExample.Models;

namespace SpeedUpCoreAPIExample.ViewModels
{
    public class PriceViewModel
    {
        public decimal Price { get; set; }
        public string Supplier { get; set; }

        public PriceViewModel() {}

        public PriceViewModel(decimal price, string supplier)
        {
            Price = price;
            Supplier = supplier;
        }

        public PriceViewModel(Price price)
        {
            Price = price.Value;
            Supplier = price.Supplier;
        }
    }
}