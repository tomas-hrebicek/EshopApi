using Sample.Domain.Entities;

namespace Sample.Api.Interfaces
{
    /// <summary>
    /// Includes operations with authorization token.
    /// </summary>
    public interface ITokenService
    {
        JwtToken CreateToken(User user);
    }
}
