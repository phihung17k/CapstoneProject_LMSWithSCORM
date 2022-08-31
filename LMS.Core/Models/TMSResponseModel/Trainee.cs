using Newtonsoft.Json;

namespace LMS.Core.Models.TMSResponseModel
{
    public class Trainee
    {
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDelete")]
        public bool IsDelete { get; set; }

        [JsonProperty("trainee")]
        public TraineeDetail TraineeDetail { get; set; }
    }

    public class TraineeDetail
    {
        [JsonProperty("str_Staff_Id")]
        public string StaffId { get; set; }
    }
}
