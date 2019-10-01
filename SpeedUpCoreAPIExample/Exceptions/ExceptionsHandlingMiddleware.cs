using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SpeedUpCoreAPIExample.Exceptions
{
    public class ExceptionsHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionsHandlingMiddleware> _logger;
        private readonly ICorsService _corsService;
        private readonly CorsOptions _corsOptions;

        public ExceptionsHandlingMiddleware(RequestDelegate next, ILogger<ExceptionsHandlingMiddleware> logger,
                                            ICorsService corsService, IOptions<CorsOptions> corsOptions)
        {
            _next = next;
            _logger = logger;
            _corsService = corsService;
            _corsOptions = corsOptions.Value;
        }

        /// <summary>
        /// Execute Middleware
        /// </summary>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (HttpException ex)
            {
                await HandleHttpExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                await HandleUnhandledExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Handle HTTP exceptions
        /// </summary>
        /// <param name="context">Current HttpContext.</param>
        /// <param name="exception">Custom HTTP exception.</param>
        private async Task HandleHttpExceptionAsync(HttpContext context, HttpException exception)
        {
            _logger.LogError(exception, exception.MessageDetail ?? exception.Message);

            if (!context.Response.HasStarted)
            {
                int statusCode = exception.StatusCode;
                string message = exception.Message;

                context.Response.Clear();

                //repopulate Response header with CORS policy
                _corsService.ApplyResult(_corsService.EvaluatePolicy(context, _corsOptions.GetPolicy("Default")), context.Response);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                var result = new ExceptionMessage(message).ToString();
                await context.Response.WriteAsync(result);
            }
        }

        private async Task HandleUnhandledExceptionAsync(HttpContext context, Exception exception)
        {

            _logger.LogError(exception, exception.Message);

            if (!context.Response.HasStarted)
            {
                int statusCode = (int)HttpStatusCode.InternalServerError; // 500
                string message = string.Empty;
#if DEBUG
                message = exception.Message;
#else
                message = "An unhandled exception has occurred";
#endif
                context.Response.Clear();

                //repopulate Response header with CORS policy
                _corsService.ApplyResult(_corsService.EvaluatePolicy(context, _corsOptions.GetPolicy("Default")), context.Response);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                var result = new ExceptionMessage(message).ToString();
                await context.Response.WriteAsync(result);
            }
        }
    }
}
