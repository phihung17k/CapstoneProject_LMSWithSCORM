using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;

namespace LMS.API.Permission
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string ClaimType { get; }
        public IEnumerable<string> AllowedValues { get; }

        public PermissionRequirement(string claimType, IEnumerable<string> allowedValues)
        {
            ClaimType = claimType ?? throw new ArgumentNullException(claimType, nameof(ClaimType));
            AllowedValues = allowedValues ?? throw new ArgumentNullException(allowedValues?.ToString(), nameof(AllowedValues));
        }
    }
}
