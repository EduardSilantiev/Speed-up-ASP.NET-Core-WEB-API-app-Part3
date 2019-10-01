using Microsoft.AspNetCore.Mvc.Filters;
using SpeedUpCoreAPIExample.Exceptions;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedUpCoreAPIExample.Filters
{
    public class ValidateIdAsyncActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ValidateParameter(context, "id");

            await next();
        }

        private void ValidateParameter(ActionExecutingContext context, string paramName)
        {
            string message = $"'{paramName.ToLower()}' must be a positive integer.";

            var param = context.ActionArguments.SingleOrDefault(p => p.Key == paramName);
            if (param.Value == null)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, message, $"'{paramName.ToLower()}' is empty.");
            }

            var id = param.Value as int?;
            if (!id.HasValue || id < 1)
            {
                throw new HttpException(System.Net.HttpStatusCode.BadRequest, message,
                                        param.Value != null ? $"{paramName}: {param.Value}" : null);
            }
        }

    }
}
