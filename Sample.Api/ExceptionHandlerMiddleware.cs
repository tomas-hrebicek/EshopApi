using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Sample.Api
{
    /// <summary>
    /// Provides global exception handling middleware.
    /// </summary>
    public sealed class SampleApiExceptionHandlerMiddleware : ExceptionHandlerMiddleware
    {
        public SampleApiExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
            : base(next, logger)
        { }

        protected override HttpStatusCode GetStatusCode(Exception exception)
        {
            return base.GetStatusCode(exception);
        }
    }

    /// <summary>
    /// Provides global exception handling middleware.
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        private static ActionContext CreateActionContext(HttpContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            RouteData routeData = context.GetRouteData();
            ActionDescriptor actionDescriptor = new ActionDescriptor();
            return new ActionContext(context, routeData, actionDescriptor);
        }

        private JsonResult CreateResult(Exception exception)
        {
            return new JsonResult((ApiError)exception)
            {
                StatusCode = GetStatusCodeInternal(exception)
            };
        }

        private int GetStatusCodeInternal(Exception exception)
        {
            try
            {
                return (int)GetStatusCode(exception);
            }
            catch
            {
                return (int)HttpStatusCode.InternalServerError;
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, exception?.Message); ;
            
            IActionResult result = CreateResult(exception);
            ActionContext actionContext = CreateActionContext(context);

            await result.ExecuteResultAsync(actionContext);
        }

        protected virtual HttpStatusCode GetStatusCode(Exception exception)
        {
            return HttpStatusCode.InternalServerError;
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
    }
}