using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using SpeedUpCoreAPIExample.Exceptions;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedUpCoreAPIExample.Filters
{
    // Validating PageIndex and PageSize request parameters ActionFilter. If exist, must be 0 or a positive integer
    public class ValidatePagingAsyncActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ValidateParameter(context, "pageIndex");
            ValidateParameter(context, "pageSize");

            await next();
        }

        private void ValidateParameter(ActionExecutingContext context, string paramName)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Key == paramName);
            if (param.Value != null)
            {
                var id = param.Value as int?;
                if (!id.HasValue || id < 0)
                {
                    string message = $"'{paramName.ToLower()}' must be 0 or a positive integer.";
                    throw new HttpException(System.Net.HttpStatusCode.BadRequest, message,
                                            param.Value != null ? $"{paramName}: {param.Value}" : null);
                }
            }
        }
    }
}
