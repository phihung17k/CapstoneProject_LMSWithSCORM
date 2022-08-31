using LMS.API.Permission;
using LMS.Core.Application;
using LMS.Core.Enum;
using LMS.Core.Models.RequestModels;
using LMS.Core.Models.RequestModels.SectionRequestModel;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static LMS.Core.Common.PermissionConstants;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ITMSService _tmsService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserCourseService _userCourseService;

        public CoursesController(ICourseService courseService, ITMSService tmsService,
            ICurrentUserService currentUserService, IUserCourseService userCourseService)
        {
            _courseService = courseService;
            _tmsService = tmsService;
            _currentUserService = currentUserService;
            _userCourseService = userCourseService;
        }

        [HttpGet]
        [Route("all/search")]
        [ProducesResponseType(typeof(PagingViewModel<CoursePagingViewModel>), 200)]
        [PermissionAuthorize(Course.ViewAllCourses)]
        public async Task<IActionResult> GetAllCourseAsync([FromQuery] CoursePagingRequestModel requestModel)
        {
            var result = await _courseService.SearchCourse(requestModel);
            return Ok(result);
        }

        [HttpGet]
        [Route("assigned/search")]
        [ProducesResponseType(typeof(PagingViewModel<CoursePagingViewModel>), 200)]
        [PermissionAuthorize(Course.ViewAssignedCoursesList, Course.ViewAllCourses)]
        public async Task<IActionResult> GetAssignedCourseAsync([FromQuery] CoursePagingRequestModel requestModel)
        {
            var result = await _courseService.SearchCourse(requestModel, _currentUserService.UserId);
            return Ok(result);
        }

        [HttpGet]
        [Route("sync-course")]
        public async Task<IActionResult> SyncCourse()
        {
            await _tmsService.VerifyAuthentication();
            await _courseService.SyncCourse();
            return Ok();
        }

        [HttpGet("detail/{id}")]
        [ProducesResponseType(typeof(CourseDetailViewModel), 200)]
        [PermissionAuthorize(Course.ViewDetailOfCourse)]
        public async Task<IActionResult> GetCourseDetailAsync(int id)
        {
            //anyone with permisison can access except student access upcoming course
            await _userCourseService.CheckViewingCourseDetail(id, _currentUserService.UserId);

            //await _userCourseService.CheckCourseAccessibility(id, _currentUserService.UserId, ActionMethods.Nothing,
            //    isStudent: true, isTeacher: true, isManager: true);


            var result = await _courseService.GetCourseDetail(id, _currentUserService.UserId);
            return Ok(result);
        }

        [HttpGet("{courseId}/attendees")]
        [ProducesResponseType(typeof(PagingViewModel<AttendeeViewModel>), 200)]
        [PermissionAuthorize(Course.ViewAttendeesList)]
        public async Task<IActionResult> GetAttendeesList(int courseId, [FromQuery] AttendeePagingRequestModel requestModel)
        {
            //anyone with anyone with permisison can access except student
            await _userCourseService.CheckViewingListAttendessInCourse(courseId, _currentUserService.UserId);

            //await _userCourseService.CheckCourseAccessibility(courseId, 
            //    _currentUserService.UserId, ActionMethods.Nothing, isTeacher: true, isManager: true);

            var result = await _courseService.GetAttendeesList(courseId, requestModel);
            return Ok(result);
        }

        [HttpGet("{courseId}/attendees/summary/learningprogress")]
        [ProducesResponseType(typeof(PagingViewModel<AttendeeSummaryViewModel>), 200)]
        [PermissionAuthorize(Course.ViewSummaryOfLearningProcessOfStudent)]
        public async Task<IActionResult> GetAttendeesSummaryProcress(int courseId, [FromQuery] AttendeePagingRequestModel requestModel)
        {
            //teacher, manager can access
            await _userCourseService.CheckCourseAccessibility(courseId,
                _currentUserService.UserId, ActionMethods.Nothing, isTeacher: true, isManager: true);

            var result = await _courseService.GetAttendeesSummaryProcress(courseId, requestModel);
            return Ok(result);
        }

        [HttpGet("attendee/learningprogress/detail/{courseTrackingId}")]
        [ProducesResponseType(typeof(LearningProgressDetailViewModel), 200)]
        [PermissionAuthorize(Course.ViewDetailOfLearningProcessOfStudent)]
        public async Task<IActionResult> ViewAttendeeLearningProgressDetail(int courseTrackingId)
        {
            var result = await _courseService.GetAttendeeLearningProgressDetail(courseTrackingId);
            return Ok(result);
        }

        [HttpGet("markReport/{courseId}")]
        [ProducesResponseType(typeof(CourseMarkReportViewModel), 200)]
        [PermissionAuthorize(Course.ViewStudentMarkReport)]
        public async Task<IActionResult> GetCourseMarkReport(int courseId)
        {
            var result = await _courseService.GetMarkReport(courseId);
            return Ok(result);
        }

        [HttpGet("own/markReport/{courseId}")]
        [ProducesResponseType(typeof(OwnMarkReportViewModel), 200)]
        [PermissionAuthorize(Course.ViewOwnMarkReport)]
        public async Task<IActionResult> GetOwnMarkReport(int courseId)
        {
            var result = await _courseService.GetOwnMarkReport(courseId);
            return Ok(result);
        }

        [HttpGet("gradingInfo/{courseId}")]
        [ProducesResponseType(typeof(List<TopicWithQuizViewModel>), 200)]
        //[PermissionAuthorize(Course.ViewDetailOfLearningProcessOfStudent)]
        public async Task<IActionResult> GetGradingInfo(int courseId)
        {
            var result = await _courseService.GetGradingInfo(courseId);
            return Ok(result);
        }

        [HttpGet("own/grades")]
        [ProducesResponseType(typeof(List<CourseGradeReportViewModel>), 200)]
        [PermissionAuthorize(Course.ViewCoursesGrades)]
        public async Task<IActionResult> GetTackingCoursesGrades()
        {
            var result = await _courseService.GetTackingCoursesGrades();
            return Ok(result);
        }

        [HttpPut("{courseId}/moving/sections")]
        [ProducesResponseType(typeof(CourseDetailViewModel), 200)]
        [PermissionAuthorize(Course.CreateTopic)]
        public async Task<IActionResult> MoveSectionsIntoTopics(int courseId, SectionListMovingRequestModel requestModel)
        {
            var result = await _courseService.MoveSectionsIntoTopics(courseId, requestModel);
            return Ok(result);
        }

        [HttpPut("{courseId}")]
        [ProducesResponseType(typeof(CourseDetailViewModel), 200)]
        [PermissionAuthorize(Course.UpdateCourse)]
        public async Task<IActionResult> UpdateCourse(int courseId, CourseUpdateRequestModel requestModel)
        {
            await _userCourseService.CheckCourseAccessibility(courseId,
                _currentUserService.UserId, ActionMethods.UpdateCourse, isTeacher: true);

            var result = await _courseService.UpdateDescription(courseId, requestModel);
            return Ok(result);
        }

        //update description from subject
        [HttpPut("{courseId}/moving/subject-information")]
        [ProducesResponseType(typeof(CourseDetailViewModel), 200)]
        [PermissionAuthorize(Course.UpdateCourse)]
        public async Task<IActionResult> UpdateCourseFromSubject(int courseId)
        {
            var result = await _courseService.UpdateCourseFromSubject(courseId);
            return Ok(result);
        }
    }
}
