using Microsoft.AspNetCore.Mvc;

namespace SpeedUpCoreAPIExample.Filters
{
    public class ValidateIdAttribute : ServiceFilterAttribute
    {
        public ValidateIdAttribute() : base(typeof(ValidateIdAsyncActionFilter))
        {
        }
    }
}