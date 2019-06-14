using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Readify.KnockKnock.Api.Middlewares
{
    public class FactoryRequestResponseLoggingMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public FactoryRequestResponseLoggingMiddleware(ILogger<FactoryRequestResponseLoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var requestPath = context.Request.Path;
            var requestQueryString = context.Request.QueryString;
            _logger.LogInformation("Request: {RequestPath}/{RequestQueryString}",requestPath, requestQueryString);

            var originalBodyStream = context.Response.Body;

            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await next(context);

            _logger.LogInformation(await FormatResponse(context.Response));
            await responseBody.CopyToAsync(originalBodyStream);
        }

        private static async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return $"Response: {response.StatusCode} - {responseBody}";
        }
    }
}
