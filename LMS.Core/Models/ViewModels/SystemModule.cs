using Newtonsoft.Json;

namespace LMS.Core.Models.ViewModels
{
    public class SystemModule
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }
    }
}
