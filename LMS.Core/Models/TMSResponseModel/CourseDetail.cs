using Newtonsoft.Json;
using System.Collections.Generic;

namespace LMS.Core.Models.TMSResponseModel
{
    public class CourseDetail
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("courseId")]
        public int CourseId { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("monitor")]
        public EndUser Monitor { get; set; }

        [JsonProperty("instructors")]
        public List<EndUser> Instructors { get; set; }

        [JsonProperty("subjectDetail")]
        public SubjectDetail SubjectDetail { get; set; }

        [JsonProperty("trainees")]
        public List<Trainee> TraineeList { get; set; }
    }
}
