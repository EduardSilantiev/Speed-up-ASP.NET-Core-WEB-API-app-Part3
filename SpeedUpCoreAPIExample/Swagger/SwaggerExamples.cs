using SpeedUpCoreAPIExample.Exceptions;
using SpeedUpCoreAPIExample.ViewModels;
using Swashbuckle.AspNetCore.Examples;
using System.Collections.Generic;

namespace SpeedUpCoreAPIExample.Swagger
{
    public class ProductExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new ProductViewModel(1, "aaa", "Product1");
        }
    }

    public class ProductsExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new ProductsPageViewModel()
            {
                PageIndex = 1,
                PageSize = 20,
                TotalPages = 1,
                TotalCount = 3,
                Items = new List<ProductViewModel>()
                {
                    new ProductViewModel(1, "aaa", "Product1"),
                    new ProductViewModel(2, "aab", "Product2"),
                    new ProductViewModel(3, "abc", "Product3")
                }
            };
        }
    }

    public class PricesExamples : IExamplesProvider
    {
        public object GetExamples()
        {
            return new PricesPageViewModel()
            {
                PageIndex = 1,
                PageSize = 20,
                TotalPages = 1,
                TotalCount = 3,
                Items = new List<PriceViewModel>()
                { 
                    new PriceViewModel(100, "Bosch"),
                    new PriceViewModel(125, "LG"),
                    new PriceViewModel(130, "Garmin")
                }
            };
        }
    }

    public class ProductNotFoundExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new ExceptionMessage("Product not found");
        }
    }

    public class InternalServerErrorExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new ExceptionMessage("An unhandled exception has occurred");
        }
    }

}
