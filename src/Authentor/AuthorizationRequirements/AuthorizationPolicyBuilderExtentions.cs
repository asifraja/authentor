using Microsoft.AspNetCore.Authorization;

namespace Authentor.AuthorizationRequirements
{
    public static class AuthorizationPolicyBuilderExtentions
    {
        public static AuthorizationPolicyBuilder RequireCustomClaim(this AuthorizationPolicyBuilder builder, string claimType)
        {
            builder.AddRequirements(new CustomRequirementClaim(claimType));
            return builder;
        }
    }
}
