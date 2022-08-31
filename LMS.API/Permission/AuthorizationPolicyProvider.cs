using LMS.Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace LMS.API.Permission
{
    public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _options;
        private readonly ConcurrentDictionary<string, AuthorizationPolicy> _policies;

        public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
            _options = options.Value;
            _policies = new ConcurrentDictionary<string, AuthorizationPolicy>();
        }

        public override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            // Check static policies first
            //var policy = await base.GetPolicyAsync(policyName);
            _policies.TryGetValue(policyName, out var policy);

            if (policy == null)
            {
                var permissionCodes = policyName.Substring(PermissionAuthorizeAttribute.PolicyPrefix.Length).Split(",");

                policy = new AuthorizationPolicyBuilder()
                    .AddRequirements(new PermissionRequirement(PermissionConstants.ClaimType, permissionCodes))
                    .Build();

                // Add policy to the AuthorizationOptions, so we don't have to re-create it each time
                //_options.AddPolicy(policyName, policy);
                _policies.TryAdd(policyName, policy);
            }

            return Task.FromResult(policy);
        }
    }
}
