using Microsoft.IdentityModel.Tokens;
using Sample.Api.Interfaces;
using Sample.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sample.Api.Services
{
    /// <summary>
    /// Provides operations with authorization token.
    /// </summary>
    public class JwtTokenService : ITokenService
    {
        IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private static ClaimsIdentity CreateClaimsIdentity(User user)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            });

            foreach (var role in user.Roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));
            }

            return identity;
        }

        /// <summary>
        /// Creates authorization token for user.
        /// </summary>
        /// <param name="user">an user</param>
        /// <returns>Authorization token for an user</returns>
        public JwtToken CreateToken(User user)
        {
            var jwtConfiguration = _configuration.GetSection(JwtOptions.Key).Get<JwtOptions>();
            TimeSpan tokenValidity = TimeSpan.FromMinutes(jwtConfiguration.TokenValidityMinutes);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtConfiguration.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = CreateClaimsIdentity(user),
                Expires = DateTime.UtcNow + tokenValidity,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return new JwtToken() 
            {
                Token = tokenHandler.WriteToken(securityToken),
                Expiration = securityToken.ValidTo
            };
        }
    }
}
