using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LMS.Core.Models.TMSResponseModel
{
    public class SubjectResponseModel
    {
        [JsonProperty("data")]
        public List<SubjectDetail> Data { get; set; }

        [JsonProperty("paging")]
        public PagingTMSResponseModel Paging { get; set; }
    }

    public class SubjectDetail
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("passScore")]
        public int? PassScore { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDelete")]
        public bool IsDeleted { get; set; }

        [JsonProperty("subject_Type")]
        public SubjectTypeString SubjectType { get; set; }

        [JsonProperty("createdDate")]
        public DateTimeOffset? CreatedDate { get; set; }

        [JsonProperty("modifyDate")]
        public DateTimeOffset? ModifyDate { get; set; }
    }

    public class SubjectTypeString
    {
        [JsonProperty("str_Name")]
        public string Name { get; set; }
    }
}
