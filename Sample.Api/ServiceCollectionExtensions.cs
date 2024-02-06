using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Sample.Api
{
    public static class ServiceCollectionExtensions
    {
        public static IApiVersioningBuilder AddVersioning(this IServiceCollection services)
        {
            return services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }

        public static IMvcBuilder AddValidatedControllers(this IServiceCollection services)
        {
            return services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    return new BadRequestObjectResult(new BadRequestApiError(actionContext));
                };
            });
        }
    }
}
