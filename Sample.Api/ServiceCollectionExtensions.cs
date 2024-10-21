using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sample.Api.Interfaces;
using Sample.Api.Security;
using System.Reflection;
using System.Text;

namespace Sample.Api
{
    /// <summary>
    /// The <see cref="IServiceCollection"/> extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services for token authentication
        /// </summary>
        /// <param name="services">service collection</param>
        /// <param name="config">configuration</param>
        /// <returns>service collection</returns>
        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var jwtSection = config.GetSection(JwtOptions.SectionName);
            services.Configure<JwtOptions>(jwtSection);
            services.AddTransient<ITokenService, JwtTokenService>();

            var jwtOptions = jwtSection.Get<JwtOptions>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = JwtTokenService.CreateTokenValidationParameters(jwtOptions);
            });

            return services;
        }

        /// <summary>
        /// Adds services for api versioning
        /// </summary>
        /// <param name="services">service collection</param>
        /// <returns>versioning builder</returns>
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

        /// <summary>
        /// Adds controller endpoint input validation.
        /// </summary>
        /// <param name="services">service collection</param>
        /// <returns>mvc builder</returns>
        public static IMvcBuilder AddValidatedControllers(this IServiceCollection services, Action<MvcOptions> configure)
        {
            return services.AddControllers(configure).ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    return new BadRequestObjectResult(new BadRequestApiError(actionContext));
                };
            });
        }

        /// <summary>
        /// Adds services for generating swagger documentation
        /// </summary>
        /// <param name="services">service collection</param>
        /// <returns>service collection</returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"), false);
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new string[] { }
                    }
                });
            });

            return services.ConfigureOptions<ConfigureSwaggerOptions>();
        }
    }
}
