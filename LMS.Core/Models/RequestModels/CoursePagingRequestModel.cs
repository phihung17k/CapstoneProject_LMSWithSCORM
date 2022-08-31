using LMS.Core.Enum;
using LMS.Core.Models.Common.RequestModels;
using System;

namespace LMS.Core.Models.RequestModels
{
    public class CoursePagingRequestModel : PagingRequestModel
    {
        public int? SubjectId { get; set; }
        public string CourseName { get; set; }

        public CourseType? CourseType { get; set; }

        public DateTimeOffset? StartTime { get; set; }

        public DateTimeOffset? EndTime { get; set; }
        public CourseProgressStatus? CourseProgressStatus { get; set; }
        public LearningStatusWithoutUndefined? LearningStatus { get; set; }
        public ActionType? ActionType { get; set; }

        public SortOrder? StartTimeSort { get; set; }

        public SortOrder? EndTimeSort { get; set; }
    }

    public class AttendeePagingRequestModel : PagingRequestModel
    {
    }
}
