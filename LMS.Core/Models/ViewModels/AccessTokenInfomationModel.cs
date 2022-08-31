using System;

namespace LMS.Core.Models.ViewModels
{
    public class AccessTokenInfomationModel
    {
        public string AccessToken { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
