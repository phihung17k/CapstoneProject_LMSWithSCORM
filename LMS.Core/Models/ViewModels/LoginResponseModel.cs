using LMS.Core.Entity;
using System;
using System.Collections.Generic;

namespace LMS.Core.Models.ViewModels
{
    public class LoginResponseModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExpireTime { get; set; }
        public string RefreshToken { get; set; }

        //string format "[category].[permission code]"
        public List<string> Permissions = new();

        public LoginResponseModel(User user, AccessTokenInfomationModel accessTokenInfo, string refreshToken,
            List<string> permissions)
        {
            Id = user.Id;
            UserName = user.UserName;
            AccessToken = accessTokenInfo.AccessToken;
            ExpireTime = accessTokenInfo.ExpireTime;
            RefreshToken = refreshToken;
            Permissions = permissions;
        }
    }
}
