using System.Threading.Tasks;
using LMS.API.Permission;
using LMS.Infrastructure.IServices;
using LMS.Core.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using static LMS.Core.Common.PermissionConstants;
using LMS.Core.Models.ViewModels;
using LMS.Core.Application;
using LMS.Core.Enum;
using LMS.Core.Models.RequestModels.LearningResourceRequestModel;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserCourseService _userCourseService;

        public TopicsController(ITopicService topicService, ICurrentUserService currentUserService,
            IUserCourseService userCourseService)
        {
            _topicService = topicService;
            _currentUserService = currentUserService;
            _userCourseService = userCourseService;
        }
        [HttpPost]
        [ProducesResponseType(typeof(TopicViewModelWithoutResource), 201)]
        [PermissionAuthorize(Course.CreateTopic)]
        public async Task<IActionResult> Create(TopicCreateRequestModel topicRequestModel)
        {
            //check action type
            //teacher, manager can access
            await _userCourseService.CheckCourseAccessibility(topicRequestModel.CourseId, _currentUserService.UserId, 
                ActionMethods.ManageTopic, isTeacher: true, isManager: true);

            var createdTopic = await _topicService.CreateTopic(topicRequestModel);
            return Created("", createdTopic);
        }
        [HttpPut("update/{id}")]
        [ProducesResponseType(typeof(TopicViewModelWithoutResource), 200)]
        [PermissionAuthorize(Course.UpdateTopic)]
        public async Task<IActionResult> Update(int id, TopicUpdateRequestModel topicRequestModel)
        {
            //teacher, manager can access
            await _userCourseService.CheckTopicAccessibility(id, _currentUserService.UserId,
                ActionMethods.ManageTopic, isTeacher: true, isManager: true);

            var updatedTopic = await _topicService.Update(id, topicRequestModel);
            return Ok(updatedTopic);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [PermissionAuthorize(Course.DeleteTopic)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            //teacher, manager can access
            await _userCourseService.CheckTopicAccessibility(id, _currentUserService.UserId,
                ActionMethods.ManageTopic, isTeacher: true, isManager: true);

            await _topicService.Delete(id);
            return Ok();
        }

        [HttpPut("{topicId}/moving/resources")]
        [ProducesResponseType(typeof(TopicViewModelWithResource), 200)]
        [PermissionAuthorize(Course.AddLearningResource)]
        public async Task<IActionResult> MoveLearningResourcesToTopic(int topicId,
            LearningResourceListMovingRequestModel requestModel)
        {
            await _userCourseService.CheckTopicAccessibility(topicId, _currentUserService.UserId,
                ActionMethods.ManageLearningResource, isTeacher: true);

            var result = await _topicService.MoveLearningResourcesToTopic(topicId, requestModel);
            return Ok(result);
        }
    }
}