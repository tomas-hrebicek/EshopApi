using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sample.Api.Interfaces;
using Sample.Application;
using Sample.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sample.Api.Security
{
    /// <summary>
    /// Provides operations with authorization token.
    /// </summary>
    public class JwtTokenService : ITokenService
    {
        private readonly JwtOptions _configuration;

        public JwtTokenService(IOptions<JwtOptions> configuration)
        {
            _configuration = configuration.Value;
        }

        internal static TokenValidationParameters CreateTokenValidationParameters(JwtOptions options)
        {
            return new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.Secret)),
                ValidateIssuer = !string.IsNullOrEmpty(options.Issuer),
                ValidateAudience = !string.IsNullOrEmpty(options.Audience),
                ValidIssuer = options.Issuer,
                ValidAudience = options.Audience,
            };
        }

        /// <summary>
        /// Creates authorization token for user.
        /// </summary>
        /// <param name="account">an account</param>
        /// <returns>Authorization token for an user</returns>
        public Token CreateToken(Account account)
        {
            TimeSpan tokenValidity = TimeSpan.FromMinutes(_configuration.TokenValidityMinutes);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = ClaimsHelper.ToClaimsIdentity(account),
                Expires = DateTime.UtcNow + tokenValidity,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return new Token()
            {
                Data = tokenHandler.WriteToken(securityToken),
                Expiration = securityToken.ValidTo
            };
        }

        /// <summary>
        /// Decode token and returns account
        /// </summary>
        /// <param name="token">token data</param>
        /// <returns>Claims principal includes in token</returns>
        public ClaimsPrincipal DecodeToken(string token, out SecurityToken validatedToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = CreateTokenValidationParameters(_configuration);
            return tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
        }
    }
}
