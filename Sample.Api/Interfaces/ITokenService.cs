using Microsoft.IdentityModel.Tokens;
using Sample.Api.Security;
using Sample.Application;
using System.Security.Claims;

namespace Sample.Api.Interfaces
{
    /// <summary>
    /// Includes operations with authorization token.
    /// </summary>
    public interface ITokenService
    {
        Token CreateToken(Account account);
        ClaimsPrincipal DecodeToken(string token, out SecurityToken validatedToken);
    }
}
