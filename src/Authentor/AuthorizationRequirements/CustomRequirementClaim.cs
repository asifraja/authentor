using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;

namespace Authentor.AuthorizationRequirements
{
    public class CustomRequirementClaim : IAuthorizationRequirement
    {
        public CustomRequirementClaim(string claimType)
        {
            ClaimType = claimType;
        }

        public string ClaimType { get; }
    }
}
