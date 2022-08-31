using LMS.Infrastructure.Data;
using LMS.Infrastructure.Exceptions;
using LMS.Infrastructure.IRepositories;
using LMS.Infrastructure.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LMS.API.Permission
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IHttpContextAccessor accessor;
        private readonly IRefreshTokenRepository refreshTokenRepository;


        public PermissionHandler(IHttpContextAccessor accessor,
            ApplicationDbContext dbContext, IRefreshTokenRepository refreshTokenRepository)
        {
            this.accessor = accessor;
            this.refreshTokenRepository = refreshTokenRepository;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == JwtRegisteredClaimNames.Exp))
            {
                string intervalString = context.User.FindFirst(c => c.Type == JwtRegisteredClaimNames.Exp).Value;
                var utcExpiryDate = long.Parse(intervalString);
                DateTime expiryTime = DatetimeUtils.UnixTimeStampToDateTime(utcExpiryDate);
                if (expiryTime <= DateTime.Now)
                {
                    return Task.FromException(
                        new RequestException(HttpStatusCode.Unauthorized, ErrorCodes.ValueNotValid, "Token has expired"));
                }
            }

            string jwtToken = accessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            bool isRevokedToken = refreshTokenRepository.HasRevokedToken(jwtToken);
            if (isRevokedToken)
            {
                return Task.FromException(
                    new RequestException(HttpStatusCode.Unauthorized, ErrorCodes.ValueNotValid, "Token has revoked"));
            }

            if (context.User.HasClaim(c => c.Type == requirement.ClaimType
                                 && requirement.AllowedValues.Contains(c.Value)))
            {
                context.Succeed(requirement);
            }
            else
            {
                return Task.FromException(
                    new RequestException(HttpStatusCode.Unauthorized, "Unauthorized", "Unauthorized request"));
            }

            return Task.CompletedTask;
        }
    }
}
