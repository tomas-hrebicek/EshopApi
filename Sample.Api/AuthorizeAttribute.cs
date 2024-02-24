using Sample.Domain.Entities;

namespace Sample.Api
{
    internal sealed class AuthorizeAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
    {
        public AuthorizeAttribute()
            : base()
        { }

        public AuthorizeAttribute(string policy)
            : base(policy)
        { }

        public AuthorizeAttribute(params Role[] roles)
            : base()
        {
            base.Roles = string.Join(",", roles);
        }

        public AuthorizeAttribute(string policy, params Role[] roles)
            : base(policy)
        {
            base.Roles = string.Join(",", roles);
        }
    }
}
