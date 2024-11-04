// File: CongestionTaxApp/Middleware/RequestLoggingMiddleware.cs
using CongestionTaxApp.Common;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CongestionTaxApp.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggingService _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILoggingService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            // Log the request
            _logger.LogInformation("Handling request: {context.Request.Method} {context.Request.Path}");

            await _next(context); // Call the next middleware in the pipeline

            stopwatch.Stop();

            // Log the response
            _logger.LogInformation($"Finished handling request. Status Code: {context.Response.StatusCode}. Time taken: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}