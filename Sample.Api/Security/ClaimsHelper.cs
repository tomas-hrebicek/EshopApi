using Sample.Application;
using Sample.Domain.Entities;
using System.Security.Claims;
using System.Security.Principal;

namespace Sample.Api.Security
{
    internal static class ClaimsHelper
    {
        internal static IEnumerable<Right> GetRights(ClaimsPrincipal claims)
        {
            ArgumentNullException.ThrowIfNull(claims);

            List<Right> rights = new List<Right>();

            foreach (var role in claims.FindAll(ClaimTypes.Role) ?? Array.Empty<Claim>())
            {
                if (Enum.TryParse(role.Value, true, out Right right))
                {
                    rights.Add(right);
                }
            }

            return rights.Distinct();
        }

        internal static ClaimsIdentity ToClaimsIdentity(Account account)
        {
            ArgumentNullException.ThrowIfNull(account);

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Sid, account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.UserData, System.Text.Json.JsonSerializer.Serialize(account))
            });

            foreach (var role in account.Roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));
            }

            return identity;
        }

        internal static Account ToAccount(ClaimsPrincipal claims)
        {
            ArgumentNullException.ThrowIfNull(claims);

            string userData = claims.FindFirstValue(ClaimTypes.UserData);
            return string.IsNullOrEmpty(userData) ? null : System.Text.Json.JsonSerializer.Deserialize<Account>(userData);
        }

        internal static int GetUserId(ClaimsPrincipal claims)
        {
            ArgumentNullException.ThrowIfNull(claims);

            string sid = claims.FindFirstValue(ClaimTypes.Sid);
            return int.TryParse(sid, out int result) ? result : default;
        }

        internal static bool HasRight(ClaimsPrincipal claims, Role role)
        {
            ArgumentNullException.ThrowIfNull(claims);

            return claims.IsInRole(role.ToString());
        }
    }
}