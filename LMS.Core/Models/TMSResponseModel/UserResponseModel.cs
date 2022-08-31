using Newtonsoft.Json;
using System.Collections.Generic;

namespace LMS.Core.Models.TMSResponseModel
{
    public class UserResponseModel
    {
        [JsonProperty("data")]
        public List<UserDetailModel> Data { get; set; }

        [JsonProperty("paging")]
        public PagingTMSResponseModel Paging { get; set; }
    }
}
