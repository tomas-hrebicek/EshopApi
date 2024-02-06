namespace Sample.Api
{
    /// <summary>
    /// The <see cref="IApplicationBuilder"/> extensions for adding CORS middleware support.
    /// </summary>
    public static class CorsExtension
    {
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
