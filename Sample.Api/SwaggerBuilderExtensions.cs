using Asp.Versioning.ApiExplorer;

namespace Sample.Api
{
    public static class SwaggerBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerVersionedUI(this IApplicationBuilder app, IServiceProvider services)
        {
            app.UseSwagger();
            return app.UseSwaggerUI(
                    options =>
                    {
                        var provider = services.GetService<IApiVersionDescriptionProvider>();
                        // build a swagger endpoint for each discovered API version
                        foreach (var description in provider.ApiVersionDescriptions)
                        {
                            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
                        }
                    });
        }
    }
}
