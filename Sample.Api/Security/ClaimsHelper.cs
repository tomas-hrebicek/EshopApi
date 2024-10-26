using Sample.Application;
using Sample.Domain.Entities;
using System.Security.Claims;

namespace Sample.Api.Security
{
    internal static class ClaimsHelper
    {
        internal static IEnumerable<Role> GetRoles(ClaimsPrincipal claims)
        {
            ArgumentNullException.ThrowIfNull(claims);

            List<Role> roles = new List<Role>();

            foreach (var roleData in claims.FindAll(ClaimTypes.Role) ?? Array.Empty<Claim>())
            {
                if (Enum.TryParse(roleData.Value, true, out Role role))
                {
                    roles.Add(role);
                }
            }

            return roles.Distinct();
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

        internal static bool HasRole(ClaimsPrincipal claims, Role role)
        {
            ArgumentNullException.ThrowIfNull(claims);

            return claims.IsInRole(role.ToString());
        }
    }
}