using LMS.Core.Entity;
using LMS.Core.Models.RequestModels;
using LMS.Core.Models.RequestModels.SectionRequestModel;
using LMS.Core.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IServices
{
    public interface ICourseService
    {
        Task<PagingViewModel<CoursePagingViewModel>> SearchCourse(CoursePagingRequestModel requestModel, Guid? userId = null);
        Task<CourseDetailViewModel> GetCourseDetail(int courseId, Guid? userId = null);
        Task SyncCourse();
        Task<PagingViewModel<AttendeeSummaryViewModel>> GetAttendeesSummaryProcress(int courseId, AttendeePagingRequestModel requestModel);
        Task<PagingViewModel<AttendeeViewModel>> GetAttendeesList(int courseId, AttendeePagingRequestModel requestModel);
        Task<LearningProgressDetailViewModel> GetAttendeeLearningProgressDetail(int courseTrackingId);
        Task ActivateHangfireTest(List<int> courseIds);
        List<Course> GetUpcomingCourseInToday();
        List<Course> GetEndCourseInToday();
        Task UpdateLearningProgressWhenCourseEnd(int courseId);
        Task<CourseMarkReportViewModel> GetMarkReport(int courseId);
        Task<OwnMarkReportViewModel> GetOwnMarkReport(int courseId);
        Task<List<TopicWithQuizViewModel>> GetGradingInfo(int courseId);
        Task<List<CourseGradeReportViewModel>> GetTackingCoursesGrades();
        Task<CourseDetailViewModel> MoveSectionsIntoTopics(int courseId, SectionListMovingRequestModel requestModel);
        Task<CourseDetailViewModel> UpdateDescription(int courseId, CourseUpdateRequestModel requestModel);
        Task<CourseDetailViewModel> UpdateCourseFromSubject(int courseId);
        Task<Course> GetCourseWithActivities(int courseId);
    }
}
