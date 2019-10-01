using SpeedUpCoreAPIExample.Helpers;
using SpeedUpCoreAPIExample.Models;
using System.Collections.Generic;
using System.Linq;

namespace SpeedUpCoreAPIExample.ViewModels
{
    public class ProductsPageViewModel : PageViewModel
    {
        public IList<ProductViewModel> Items;

        public ProductsPageViewModel() {}

        public ProductsPageViewModel(PaginatedList<Product> paginatedList) :
                base(paginatedList.PageIndex, paginatedList.PageSize, paginatedList.TotalPages, paginatedList.TotalCount)
        {
            this.Items = paginatedList.Select(p => new ProductViewModel(p)).ToList();
        }
    }
}
