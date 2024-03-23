using Asp.Versioning.ApiExplorer;

namespace Sample.Api
{
    /// <summary>
    /// The <see cref="IApplicationBuilder"/> extensions.
    /// </summary>
    internal static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Add swagger middleware with support for versioning.
        /// </summary>
        /// <param name="app">application builder</param>
        /// <param name="services">service provider</param>
        /// <returns></returns>
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

        /// <summary>
        /// Adds a CORS middleware to web application pipeline to allow cross domain requests.
        /// </summary>
        /// <param name="app">The IApplicationBuilder passed to your Configure method</param>
        /// <param name="config">The application configuration</param>
        /// <returns>The original app parameter</returns>
        public static IApplicationBuilder UseCors(this IApplicationBuilder app, IConfiguration config)
        {
            string configAllowedOrigins = config.GetValue<string>("AllowedOrigins", string.Empty);
            var allowedOrigins = configAllowedOrigins.Split(";", StringSplitOptions.RemoveEmptyEntries);

            return app.UseCors(policy =>
            {
                policy.WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        }
    }
}
