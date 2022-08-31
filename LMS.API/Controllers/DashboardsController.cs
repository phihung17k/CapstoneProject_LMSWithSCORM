using LMS.API.Permission;
using LMS.Core.Application;
using LMS.Core.Models.RequestModels.DashboardRequestModel;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static LMS.Core.Common.PermissionConstants;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        private readonly ICurrentUserService _currentUserService;

        public DashboardsController(IDashboardService dashboardService, ICurrentUserService currentUserService)
        {
            _dashboardService = dashboardService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        [Route("course/progressStatus/all")]
        [ProducesResponseType(typeof(CourseProgressStatusRatioViewModel), 200)]
        [PermissionAuthorize(Dashboard.ViewAllCoursesProgressChart)]
        public async Task<IActionResult> GetAllCourseProgressRatio([FromQuery] CourseProgressRatioRequestModel requestModel)
        {
            var result = await _dashboardService.GetCourseProgressRatio(requestModel);
            return Ok(result);
        }

        [HttpGet]
        [Route("course/progressStatus/assigned")]
        [ProducesResponseType(typeof(CourseProgressStatusRatioViewModel), 200)]
        [PermissionAuthorize(Dashboard.ViewAssignedCoursesProgressChart)]
        public async Task<IActionResult> GetAssignedCourseProgressStatusRatio([FromQuery] CourseProgressRatioRequestModel requestModel)
        {
            var result = await _dashboardService.GetCourseProgressRatio(requestModel, _currentUserService.UserId);
            return Ok(result);
        }

        [HttpGet]
        [Route("attendee/learningProgress/allCourse")]
        [ProducesResponseType(typeof(AttendeeLearningProgressRatioViewModel), 200)]
        [PermissionAuthorize(Dashboard.ViewAttendeesLearningProgressChartInAllCourses)]
        public async Task<IActionResult> GetAttendeesLearningProgressRatioInAllCourses([FromQuery] AttendeeLearningProgressRatioRequestModel requestModel)
        {
            var result = await _dashboardService.GetAttendeesLearningProgressRatio(requestModel);
            return Ok(result);
        }

        [HttpGet]
        [Route("attendee/learningProgress/assignedCourse")]
        [ProducesResponseType(typeof(AttendeeLearningProgressRatioViewModel), 200)]
        [PermissionAuthorize(Dashboard.ViewAttendeesLearningProgressChartInAssignedCourses)]
        public async Task<IActionResult> GetAttendeesLearningStatusRatioInAssignedCourses([FromQuery] AttendeeLearningProgressRatioRequestModel requestModel)
        {
            var result = await _dashboardService.GetAttendeesLearningProgressRatio(requestModel, _currentUserService.UserId);
            return Ok(result);
        }

        [HttpGet]
        [Route("course/ownLearningProgress/assigned")]
        [ProducesResponseType(typeof(OwnLearningProgressRatioByCourseViewModel), 200)]
        [PermissionAuthorize(Dashboard.ViewOwnLearningProgressChartInAssignedCourses)]
        public async Task<IActionResult> GetOwnLearningProgressByCourse()
        {
            var result = await _dashboardService.GetOwnLearningProgressByCourse();
            return Ok(result);
        }

        [HttpGet]
        [Route("role/analytics")]
        [ProducesResponseType(typeof(TotalRoleRatioViewModel), 200)]
        [PermissionAuthorize(Dashboard.ViewUserRoleAnalytics)]
        public async Task<IActionResult> GetTotalRoleRatio()
        {
            var result = await _dashboardService.GetTotalRoleRatio();
            return Ok(result);
        }
    }
}
