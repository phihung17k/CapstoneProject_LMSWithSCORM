using System.Collections.Generic;

namespace LMS.Core.Models.ViewModels
{
    public class CourseProgressStatusRatioViewModel
    {
        public List<StatusRatioViewModel> CourseProgressStatusRatio { get; set; }
    }

    public class AttendeeLearningProgressRatioViewModel
    {
        public List<StatusRatioViewModel> AttendeeLearningProgressStatusRatio { get; set; }
    }

    public class OwnLearningProgressRatioByCourseViewModel
    {
        public List<StatusRatioViewModel> OwnLearningProgressRatio { get; set; }
    }

    public class TotalRoleRatioViewModel
    {
        public int TotalRoles { get; set; }
        public List<RoleRatioViewModel> RoleRatio { get; set; }
    }

    public class StatusRatioViewModel
    {
        public string Status { get; set; }
        public int Count { get; set; }
    }

    public class RoleRatioViewModel
    {
        public string Role { get; set; }
        public int UserCount { get; set; }
    }
}
