using LMS.Core.Enum;
using Newtonsoft.Json;

namespace LMS.Core.Models.RequestModels.QuizRequestModel
{
    public class RestrictionModel
    {
        [JsonProperty("topicResourceId")]
        public int TopicResourceId { get; set; }
        [JsonProperty("type")]
        public RestrictionResourceType Type { get; set; }
    }
}
