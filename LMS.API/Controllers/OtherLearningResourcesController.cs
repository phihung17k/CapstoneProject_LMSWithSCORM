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
    public class OtherLearningResourcesController : ControllerBase
    {
        private readonly IOtherLearningResourceService service;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserCourseService _userCourseService;

        public OtherLearningResourcesController(IOtherLearningResourceService service, 
            ICurrentUserService currentUserService, IUserCourseService userCourseService)
        {
            this.service = service;
            _currentUserService = currentUserService;
            _userCourseService = userCourseService;
        }

        [HttpPost("upload/section")]
        [ProducesResponseType(typeof(OtherLearningResourceViewModel), 200)]
        [PermissionAuthorize(Subject.AddLearningResource)]
        public async Task<IActionResult> UploadOtherLearningResourceInSection(int sectionId, IFormFile resource)
        {
            var result = await service.UploadOtherLearningResourceInSection(sectionId, resource);
            return Ok(result);
        }

        [HttpPost("upload/topic")]
        [ProducesResponseType(typeof(TopicOLRWithoutTrackingViewModel), 200)]
        [PermissionAuthorize(Course.AddLearningResource)]
        public async Task<IActionResult> UploadOtherLearningResourceInTopic(int topicId, IFormFile resource)
        {
            await _userCourseService.CheckTopicAccessibility(topicId, _currentUserService.UserId, 
               ActionMethods.ManageLearningResource, isTeacher: true);

            var result = await service.UploadOtherLearningResourceInTopic(topicId, resource);
            return Ok(result);
        }

        //[HttpPost("move/topic")]
        //[ProducesResponseType(typeof(TopicOLRWithoutTrackingViewModel), 200)]
        //[PermissionAuthorize(Course.AddLearningResource)]
        //public async Task<IActionResult> MoveOtherLearningResourceToTopic(int topicId, int resourceId)
        //{
        //    await _userCourseService.CheckTopicAccessibility(topicId, _currentUserService.UserId,
        //        ActionMethods.ManageLearningResource, isTeacher: true);

        //    var result = await service.MoveOtherLearningResourceToTopic(topicId, resourceId);
        //    return Ok(result);
        //}

        [HttpDelete("topic/{topicOLRId}")]
        [ProducesResponseType(200)]
        [PermissionAuthorize(Course.DeleteLearningResource)]
        public async Task<IActionResult> DeleteOtherLearningResourceInTopic(int topicOLRId)
        {
            await _userCourseService.CheckTopicResourceAccessibility(topicOLRId, _currentUserService.UserId,
                TopicResourceType.OtherLearningResource, ActionMethods.ManageLearningResource, isTeacher: true);

            await service.DeleteOtherLearningResourceInTopic(topicOLRId);
            return NoContent();
        }

        [HttpDelete("section/{OLRId}")]
        [ProducesResponseType(200)]
        [PermissionAuthorize(Subject.DeleteLearningResource)]
        public async Task<IActionResult> DeleteOtherLearningResourceInSubject(int OLRId)
        {
            await service.DeleteOtherLearningResourceInSection(OLRId);
            return NoContent();
        }

        [HttpGet("view-content/{topicOtherLearningResourceId}")]
        [ProducesResponseType(typeof(OtherLearningResourceViewContentModel), 200)]
        [PermissionAuthorize(Course.ViewContentOfLearningResources)]
        public async Task<IActionResult> ViewContent(int topicOtherLearningResourceId)
        {
            //student, teacher and manager can access 
            //view content so that nothing constraint for this action if course is in progress
            await _userCourseService.CheckTopicResourceAccessibility(topicOtherLearningResourceId, 
                _currentUserService.UserId, TopicResourceType.OtherLearningResource, ActionMethods.Nothing,
                isStudent: true, isTeacher: true, isManager: true);

            var result = await service.ViewContent(topicOtherLearningResourceId);
            return Ok(result);
        }

        [HttpPut("topic-other-learning-resource/{topicOtherLearningResourceId}")]
        [ProducesResponseType(typeof(TopicOLRWithoutTrackingViewModel), 200)]
        [PermissionAuthorize(Course.UpdateLearningResource)]
        public async Task<IActionResult> UpdateOtherLearningResourceInTopic(int topicOtherLearningResourceId, 
            [FromBody] TopicOLRUpdateRequestModel requestModel)
        {
            //Only teacher can access
            await _userCourseService.CheckTopicResourceAccessibility(topicOtherLearningResourceId, 
                _currentUserService.UserId, TopicResourceType.OtherLearningResource, 
                ActionMethods.ManageLearningResource, isTeacher: true);

            var result = await service.UpdateOtherLearningResourceInTopic(topicOtherLearningResourceId, requestModel);
            return Ok(result);
        }
    }
}
