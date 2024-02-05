using Sample.Domain.Entities;

namespace Sample.Api.Interfaces
{
    public interface ITokenService
    {
        JwtToken CreateToken(User user);
    }
}
