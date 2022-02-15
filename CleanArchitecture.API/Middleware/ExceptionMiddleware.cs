using CleanArchitecture.API.Errors;
using CleanArchitecture.Application.Exceptions;
using System.Net;

namespace CleanArchitecture.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";

                var statusCode = (int)HttpStatusCode.InternalServerError;
                var result = string.Empty;

                switch (ex)
                {
                    case NotFoundException notFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ValidationException validationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        var validationJson = Newtonsoft.Json.JsonConvert.SerializeObject(validationException.Errors);
                        result = Newtonsoft.Json.JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, validationJson));
                        break;
                    case BadRequestException badRequestException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        break;
                }

                if(string.IsNullOrEmpty(result))
                {
                    result = Newtonsoft.Json.JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, ex.StackTrace));
                }
                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsync(result);
            }
        }
    }
}
