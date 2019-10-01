namespace SpeedUpCoreAPIExample.ViewModels
{
    public class PageViewModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PageViewModel() {}

        public PageViewModel(int pageIndex, int pageSize, int totalPages, int totalCount)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalCount = totalCount;
        }
    }
}
