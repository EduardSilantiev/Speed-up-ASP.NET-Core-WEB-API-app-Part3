using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace SpeedUpCoreAPIExample.Exceptions
{
    public class VersioningErrorResponseProvider : DefaultErrorResponseProvider
    {
        public override IActionResult CreateResponse(ErrorResponseContext context)
        {
            string message = string.Empty;
            switch (context.ErrorCode)
            {
                case "ApiVersionUnspecified":
                    message = "An API version is required, but was not specified.";

                    break;

                case "UnsupportedApiVersion":
                    message = "The specified API version is not supported.";

                    break;

                case "InvalidApiVersion":
                    message = "An API version was specified, but it is invalid.";

                    break;

                case "AmbiguousApiVersion":
                    message = "An API version was specified multiple times with different values.";

                    break;

                default:
                    message = context.ErrorCode;

                    break;
            }

            throw new HttpException(System.Net.HttpStatusCode.BadRequest, message, context.MessageDetail);
        }
    }
}
