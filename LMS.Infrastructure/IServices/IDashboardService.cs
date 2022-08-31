using LMS.Core.Models.RequestModels.DashboardRequestModel;
using LMS.Core.Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IServices
{
    public interface IDashboardService
    {
        public Task<CourseProgressStatusRatioViewModel> GetCourseProgressRatio(CourseProgressRatioRequestModel requestModel, Guid? userId = null);
        public Task<AttendeeLearningProgressRatioViewModel> GetAttendeesLearningProgressRatio(AttendeeLearningProgressRatioRequestModel requestModel, Guid? userId = null);
        public Task<OwnLearningProgressRatioByCourseViewModel> GetOwnLearningProgressByCourse();
        public Task<TotalRoleRatioViewModel> GetTotalRoleRatio();
    }
}
