using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LMS.Core.Models.TMSResponseModel
{
    public class CourseModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("startDate")]
        public DateTimeOffset StartTime { get; set; }

        [JsonProperty("endDate")]
        public DateTimeOffset EndTime { get; set; }

        //[JsonProperty("numberOfTrainee")]
        //public int NumberOfTrainee { get; set; }

        [JsonProperty("course_Type")]
        public CourseTypeString CourseTypeString { get; set; }

        [JsonProperty("course_Detail")]
        public List<CourseDetail> CourseDetailList { get; set; }
    }

    public class CourseTypeString
    {
        [JsonProperty("str_Name")]
        public string Name { get; set; }
    }
}
