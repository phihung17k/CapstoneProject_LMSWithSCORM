using System;

namespace LMS.Core.Models.ViewModels
{
    public class OtherLearningResourceTrackingViewModel
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        public Guid LearnerId { get; set; }
        public int TopicOtherLearningResourceId { get; set; }
    }

    public class OtherLearningResourceViewContentModel
    {
        public string PathToFile { get; set; }
        public float CompletionThreshold { get; set; }
        public OtherLearningResourceTrackingViewModel OLRTracking { get; set; }
    }

    public class OtherLearningResourceUpdateProgressViewModel : OtherLearningResourceTrackingViewModel
    {
        public TopicTrackingViewModel TopicTracking { get; set; }
    }
}
