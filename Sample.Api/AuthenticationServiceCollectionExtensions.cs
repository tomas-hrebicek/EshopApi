using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Sample.Api.Interfaces;
using Sample.Api.Services;
using Sample.Application.Interfaces;
using System.Text;

namespace Sample.Api
{
    public static class AuthenticationServiceCollectionExtensions
    {
        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<ITokenService, JwtTokenService>();
            
            var jwtConfiguration = config.GetSection(JwtOptions.Key).Get<JwtOptions>();

            var key = Encoding.ASCII.GetBytes(jwtConfiguration.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = !string.IsNullOrEmpty(jwtConfiguration.Issuer),
                    ValidateAudience = !string.IsNullOrEmpty(jwtConfiguration.Audience),
                    ValidIssuer = jwtConfiguration.Issuer,
                    ValidAudience = jwtConfiguration.Audience
                };
            });

            return services;
        }
    }
}
