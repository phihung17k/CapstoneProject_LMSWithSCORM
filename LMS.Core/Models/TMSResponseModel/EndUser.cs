using Newtonsoft.Json;

namespace LMS.Core.Models.TMSResponseModel
{
    public class EndUser
    {
        [JsonProperty("str_Staff_Id")]
        public string StaffId { get; set; }
    }
}
