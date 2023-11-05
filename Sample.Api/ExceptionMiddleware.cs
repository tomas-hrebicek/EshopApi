using System.Net;

namespace Sample.Api
{
    /// <summary>
    /// Provides global exception handling.
    /// </summary>
    public sealed class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ApiError response = new ApiError()
            {
                Message = exception.Message,
                Data = exception.Data
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
