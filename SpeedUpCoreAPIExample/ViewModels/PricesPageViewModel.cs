using SpeedUpCoreAPIExample.Helpers;
using SpeedUpCoreAPIExample.Models;
using System.Collections.Generic;
using System.Linq;

namespace SpeedUpCoreAPIExample.ViewModels
{
    public class PricesPageViewModel : PageViewModel
    {
        public IList<PriceViewModel> Items;

        public PricesPageViewModel() {}

        public PricesPageViewModel(PaginatedList<Price> paginatedList) :
                base(paginatedList.PageIndex, paginatedList.PageSize, paginatedList.TotalPages, paginatedList.TotalCount)
        {
            this.Items = paginatedList.Select(p => new PriceViewModel(p))
                .OrderBy(p => p.Price)
                .ThenBy(p => p.Supplier)
                .ToList();
        }
    }
}
