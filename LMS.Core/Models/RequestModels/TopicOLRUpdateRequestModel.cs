using System.ComponentModel.DataAnnotations;

namespace LMS.Core.Models.RequestModels
{
    public class TopicOLRUpdateRequestModel
    {
        [Range(0, 1)]
        public float CompletionThreshold { get; set; }

        public string OtherLearningResourceName { get; set; }
    }
}
