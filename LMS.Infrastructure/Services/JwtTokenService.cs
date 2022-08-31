using LMS.Core.Common;
using LMS.Core.Entity;
using LMS.Core.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace LMS.Infrastructure.Services
{
    public interface IJwtTokenService
    {
        AccessTokenInfomationModel CreateToken(User user, List<Permission> permissions);
        Guid GetUserIdFromToken(string jwtToken);
    }
    public class JwtTokenService : IJwtTokenService
    {
        private IConfiguration _configuration;
        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public AccessTokenInfomationModel CreateToken(User user, List<Permission> permissions)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName.ToString())
            };

            foreach (var permission in permissions)
            {
                string claimValue = $"{permission.Category}.{permission.Code}";
                claims.Add(new Claim(PermissionConstants.ClaimType, claimValue));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTime expireTime = DateTime.Now.AddDays(1);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: expireTime,
                signingCredentials: creds
            );

            return new AccessTokenInfomationModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireTime = expireTime
            };
        }

        public Guid GetUserIdFromToken(string jwtToken)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken = handler.ReadJwtToken(jwtToken);
            List<Claim> claims = jwtSecurityToken.Claims.ToList();
            string userId = claims.FirstOrDefault(x => x.Type.Equals("UserId")).Value;
            return Guid.Parse(userId);
        }
    }
}
