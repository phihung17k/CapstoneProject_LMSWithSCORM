using Microsoft.AspNetCore.Authorization;

namespace LMS.API.Permission
{
    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        internal const string PolicyPrefix = "PERMISSION:";

        public PermissionAuthorizeAttribute(params string[] permissions)
        {
            base.Policy = $"{PolicyPrefix}{string.Join(",", permissions)}";
        }
    }
}
