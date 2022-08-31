using Newtonsoft.Json;
using System.Collections.Generic;

namespace LMS.Core.Models.TMSResponseModel
{
    public class CourseResponseModel
    {
        [JsonProperty("data")]
        public List<CourseModel> Data { get; set; }

        [JsonProperty("paging")]
        public PagingTMSResponseModel Paging { get; set; }
    }
}
