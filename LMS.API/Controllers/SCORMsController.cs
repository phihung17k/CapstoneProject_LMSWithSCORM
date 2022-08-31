using LMS.API.Permission;
using LMS.Core.Application;
using LMS.Core.Enum;
using LMS.Core.Models.RequestModels;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static LMS.Core.Common.PermissionConstants;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScormsController : ControllerBase
    {
        private readonly ISCORMService service;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserCourseService _userCourseService;

        public ScormsController(ISCORMService service, ICurrentUserService currentUserService, 
            IUserCourseService userCourseService)
        {
            this.service = service;
            _currentUserService = currentUserService;
            _userCourseService = userCourseService;
        }

        [HttpPost("upload/section")]
        [ProducesResponseType(typeof(SCORMViewModel), 200)]
        [PermissionAuthorize(Subject.AddLearningResource)]
        public async Task<IActionResult> UploadSCORM(int sectionId, IFormFile resource)
        {
            var result = await service.UploadSCORMInSection(sectionId, resource);
            return Ok(result);
        }

        [HttpPost("upload/topic")]
        [ProducesResponseType(typeof(TopicSCORMWithoutCoreViewModel), 200)]
        [PermissionAuthorize(Course.AddLearningResource)]
        public async Task<IActionResult> UploadSCORMInTopic(int topicId, IFormFile resource)
        {
            await _userCourseService.CheckTopicAccessibility(topicId, _currentUserService.UserId,
                ActionMethods.ManageLearningResource, isTeacher: true);

            var result = await service.UploadSCORMInTopic(topicId, resource);
            return Ok(result);
        }

        //[HttpPost("topic/moving/resources")]
        //[ProducesResponseType(typeof(TopicSCORMWithoutCoreViewModel), 200)]
        //[PermissionAuthorize(Course.AddLearningResource)]
        //public async Task<IActionResult> MoveSCORMsToTopic(int topicId, int resourceId)
        //{
        //    await _userCourseService.CheckTopicAccessibility(topicId, _currentUserService.UserId,
        //        ActionMethods.ManageLearningResource, isTeacher: true);

        //    var result = await service.MoveSCORMToTopic(topicId, resourceId);
        //    return Ok(result);
        //}

        [HttpDelete("topic/{topicSCORMId}")]
        [ProducesResponseType(200)]
        [PermissionAuthorize(Course.DeleteLearningResource)]
        public async Task<IActionResult> DeleteSCORMInTopic(int topicSCORMId)
        {
            await _userCourseService.CheckTopicResourceAccessibility(topicSCORMId, _currentUserService.UserId,
               TopicResourceType.SCORM, ActionMethods.ManageLearningResource, isTeacher: true);

            await service.DeleteSCORMInTopic(topicSCORMId);
            return NoContent();
        }

        [HttpDelete("section/{scormId}")]
        [ProducesResponseType(200)]
        [PermissionAuthorize(Subject.DeleteLearningResource)]
        public async Task<IActionResult> DeleteSCORMInSection(int scormId)
        {
            await service.DeleteSCORMInSection(scormId);
            return NoContent();
        }

        [HttpGet("topic/view-content/{topicSCORMId}")]
        [ProducesResponseType(typeof(SCORMViewContentModel), 200)]
        [PermissionAuthorize(Course.ViewContentOfLearningResources)]
        public async Task<ActionResult> ViewContent(int topicSCORMId)
        {
            //student, teacher and manager can access 
            await _userCourseService.CheckTopicResourceAccessibility(topicSCORMId,
                _currentUserService.UserId, TopicResourceType.SCORM, ActionMethods.Nothing,
                isStudent: true, isTeacher: true, isManager: true);

            var result = await service.ViewContent(topicSCORMId);
            return Ok(result);
        }

        [HttpPut("topic-scorm/{topicSCORMId}")]
        [ProducesResponseType(typeof(TopicSCORMWithoutCoreViewModel), 200)]
        [PermissionAuthorize(Course.UpdateLearningResource)]
        public async Task<IActionResult> UpdateSCORMInTopic(int topicSCORMId,
            [FromBody] TopicScormUpdateRequestModel requestModel)
        {
            //Only teacher can access
            await _userCourseService.CheckTopicResourceAccessibility(topicSCORMId, _currentUserService.UserId,
               TopicResourceType.SCORM, ActionMethods.ManageLearningResource, isTeacher: true);

            var result = await service.UpdateSCORMInTopic(topicSCORMId, requestModel);
            return Ok(result);
        }
    }
}
