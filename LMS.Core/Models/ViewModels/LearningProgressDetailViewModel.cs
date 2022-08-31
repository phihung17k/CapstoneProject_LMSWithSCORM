using LMS.Core.Enum;
using System;
using System.Collections.Generic;

namespace LMS.Core.Models.ViewModels
{
    public class LearningProgressDetailViewModel
    {
        public int CourseTrackingId { get; set; }
        public DateTimeOffset? DateOfJoin { get; set; }
        public DateTimeOffset? FinishTime { get; set; }
        public float FinalScore { get; set; }
        public LearningStatus LearningStatus { get; set; }
        public List<TopicViewModelWithResource> Topics { get; set; }
    }
}
