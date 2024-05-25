using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace global_exception_midleware.Configurations
{
    internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger = logger;

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "[GlobalExceptionHandler] -> Exception: {Ex}", exception.Message);

            var problemDetails = new
            {
                Code = httpContext.Response.StatusCode,
                Description = exception.Message,
            };

            httpContext.Response.StatusCode = (int)HttpStatusCode.BadGateway;

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
