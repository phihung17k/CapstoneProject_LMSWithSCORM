using System;
using System.Collections.Generic;
using LMS.Core.Enum;

namespace LMS.Core.Models.ViewModels
{
    public class TopicOtherLearningResourceViewModel
    {
        public int Id { get; set; }
        public int OtherLearningResourceId { get; set; }
        public string OtherLearningResourceName { get; set; }
        public string Title { get; set; }
        public LearningResourceType Type { get; set; }
        public float CompletionThreshold { get; set; }
        public DateTimeOffset CreateTime { get; set; }
        public OtherLearningResourceTrackingViewModel OLRTracking { get; set; }
    }

    public class TopicOLRWithoutTrackingViewModel
    {
        public int TopicId { get; set; }

        public int OtherLearningResourceId { get; set; }

        public int TopicOtherLearningResourceId { get; set; }

        public string OtherLearningResourceName { get; set; }

        public string PathToFile { get; set; }

        public LearningResourceType Type { get; set; }
        public float CompletionThreshold { get; set; }

        public DateTimeOffset CreateTime { get; set; }

        public Guid CreateBy { get; set; }
    }

    public class TopicOLRListViewModel
    {
        public int TopicId { get; set; }
        public List<TopicOLRDetailViewModel> AdditionalTopicOLRDetails { get; set; }
    }

    public class TopicOLRDetailViewModel
    {
        public int OtherLearningResourceId { get; set; }

        public int TopicOtherLearningResourceId { get; set; }

        public string OtherLearningResourceName { get; set; }

        public string PathToFile { get; set; }

        public LearningResourceType Type { get; set; }

        public DateTimeOffset CreateTime { get; set; }

        public Guid CreateBy { get; set; }
    }
}