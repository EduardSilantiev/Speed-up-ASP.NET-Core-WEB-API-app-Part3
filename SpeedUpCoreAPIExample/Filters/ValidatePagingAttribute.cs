using Microsoft.AspNetCore.Mvc;

namespace SpeedUpCoreAPIExample.Filters
{
    public class ValidatePagingAttribute : ServiceFilterAttribute
    {
        public ValidatePagingAttribute() : base(typeof(ValidatePagingAsyncActionFilter))
        {
        }
    }
}
