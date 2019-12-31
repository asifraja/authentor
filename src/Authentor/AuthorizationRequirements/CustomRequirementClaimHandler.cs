using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace Authentor.AuthorizationRequirements
{
    public class CustomRequirementClaimHandler : AuthorizationHandler<CustomRequirementClaim>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequirementClaim requirement)
        {
            var hasClaim = context.User.Claims.Any(x => x.Type == requirement.ClaimType);
            if(hasClaim)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
