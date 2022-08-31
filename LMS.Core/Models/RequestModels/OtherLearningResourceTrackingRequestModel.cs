using System;

namespace LMS.Core.Models.RequestModels
{
    public class OtherLearningResourceTrackingRequestModel
    {
        public TimeSpan Duration { get; set; }
        public int TopicOtherLearningResourceId { get; set; }
    }

    public class LearningProgressUpdateRequestModel
    {
        public bool IsCompleted { get; set; }
    }
}