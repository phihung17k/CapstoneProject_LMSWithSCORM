using LMS.API.Permission;
using LMS.Core.Models.RequestModels.UserSurveyRequestModel;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static LMS.Core.Common.PermissionConstants;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSurveysController : ControllerBase
    {
        private readonly IUserSurveyService service;

        public UserSurveysController(IUserSurveyService service)
        {
            this.service = service;
        }

        [HttpPost("submitSurvey")]
        [ProducesResponseType(typeof(SubmitSurveyViewModel), 200)]
        [PermissionAuthorize(Course.DoAndEditSurvey)]
        public async Task<ActionResult> SubmitSurvey(UserSurveyCreateRequestModel userSurveyRequestModel)
        {
            var result = await service.SubmitSurvey(userSurveyRequestModel);
            return Ok(result);
        }

        [HttpGet("get-filled-survey/{userSurveyId}")]
        [ProducesResponseType(typeof(UserSurveyViewModel), 200)]
        [PermissionAuthorize(Course.DoAndEditSurvey, Course.ViewDetailOfSurveyResult)]
        public ActionResult<UserSurveyViewModel> GetFilledSurvey(int userSurveyId)
        {
            var result = service.GetFilledSurvey(userSurveyId);
            return Ok(result);
        }

        [HttpPut("update-survey-of-student")]
        [ProducesResponseType(typeof(UserSurveyViewModel), 200)]
        [PermissionAuthorize(Course.DoAndEditSurvey)]
        public async Task<ActionResult> UpdateSurveyOfStudent(UserSurveyUpdateRequestModel userSurveyRequestModel)
        {
            var result = await service.UpdateSurveyOfStudent(userSurveyRequestModel);
            return Ok(result);
        }
    }
}
