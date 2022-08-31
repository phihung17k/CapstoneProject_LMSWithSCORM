using System;
using System.Threading.Tasks;
using LMS.API.Permission;
using LMS.Infrastructure.IServices;
using LMS.Core.Models.RequestModels.SurveyRequestModel;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using static LMS.Core.Common.PermissionConstants;
using LMS.Core.Models.ViewModels;
using LMS.Core.Application;
using LMS.Core.Enum;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserCourseService _userCourseService;

        public SurveysController(ISurveyService surveyService, ICurrentUserService currentUserService,
            IUserCourseService userCourseService)
        {
            _surveyService = surveyService;
            _currentUserService = currentUserService;
            _userCourseService = userCourseService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SurveyViewModel), 201)]
        [PermissionAuthorize(Course.AddSurveyFromTemplate)]
        public async Task<IActionResult> CreateInTopic(SurveyCreateRequestModel surveyCreateRequestModel)
        {
            ////manager can access
            await _userCourseService.CheckTopicAccessibility(surveyCreateRequestModel.TopicId, 
                _currentUserService.UserId, ActionMethods.ManageSurvey, isManager: true);

            var createdSurvey = await _surveyService.CreateSurveyInTopic(surveyCreateRequestModel);
            return Created(new Uri(Request.GetEncodedUrl() + "/" + createdSurvey.Id), createdSurvey);
        }

        [HttpGet("detail/{id}")]
        [ProducesResponseType(typeof(SurveyViewModel), 200)]
        [PermissionAuthorize(Course.DoAndEditSurvey, Course.UpdateSurvey, Course.DeleteSurvey, Course.PreviewSurvey)]
        public async Task<IActionResult> Get(int id)
        {
            var survey = await _surveyService.Get(id);
            return Ok(survey);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [PermissionAuthorize(Course.DeleteSurvey)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            //manager can access
            await _userCourseService.CheckTopicResourceAccessibility(id, _currentUserService.UserId,
               TopicResourceType.Survey, ActionMethods.ManageSurvey, isManager: true);

            await _surveyService.Delete(id);
            return NoContent();
        }

        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(SurveyViewModel), 200)]
        [PermissionAuthorize(Course.UpdateSurvey)]
        public async Task<IActionResult> Update(int id, SurveyUpdateRequestModel surveyUpdateRequestModel)
        {
            //manager can access
            await _userCourseService.CheckTopicResourceAccessibility(id, _currentUserService.UserId,
               TopicResourceType.Survey, ActionMethods.ManageSurvey, isManager: true);

            var updatedSurvey = await _surveyService.Update(id, surveyUpdateRequestModel);
            return Ok(updatedSurvey);
        }

        [HttpGet("aggregation/{surveyId}")]
        [ProducesResponseType(typeof(SurveyAggregationViewModel), 200)]
        [PermissionAuthorize(Course.ViewSummaryOfSurveyResults)]
        public IActionResult GetSurveyResultAggregation(int surveyId)
        {
            //teacher and manager can access
            _userCourseService.CheckTopicResourceAccessibility(surveyId, _currentUserService.UserId,
               TopicResourceType.Survey, ActionMethods.Nothing, isTeacher: true, isManager: true).GetAwaiter().GetResult();

            var result = _surveyService.GetSurveyAggregation(surveyId);
            return Ok(result);
        }

        [HttpGet("search/allCourses")]
        [ProducesResponseType(typeof(PagingViewModel<SurveyManagementViewModel>), 200)]
        [PermissionAuthorize(Course.ViewListOfSurveyResultsInAllCourses)]
        public async Task<IActionResult> GetSurveyListInAllCourses([FromQuery] SurveyPagingRequestModel requestModel)
        {
            var result = await _surveyService.GetSurveyList(requestModel);
            return Ok(result);
        }

        [HttpGet("search/assignedCourses")]
        [ProducesResponseType(typeof(PagingViewModel<SurveyManagementViewModel>), 200)]
        [PermissionAuthorize(Course.ViewListOfSurveyResultsInAllCourses, Course.ViewListOfSurveyResultsInAssignedCourses)]
        public async Task<IActionResult> GetSurveyListInAssignedCourses([FromQuery] SurveyPagingRequestModel requestModel)
        {
            var result = await _surveyService.GetSurveyList(requestModel, _currentUserService.UserId);
            return Ok(result);
        }
    }
}