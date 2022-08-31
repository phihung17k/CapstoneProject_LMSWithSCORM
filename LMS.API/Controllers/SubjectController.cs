using LMS.API.Permission;
using LMS.Core.Application;
using LMS.Core.Models.RequestModels;
using LMS.Core.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static LMS.Core.Common.PermissionConstants;
using LMS.Infrastructure.IServices;
using System.Collections.Generic;
using LMS.Core.Models.RequestModels.SubjectRequestModel;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ITMSService _tmsService;

        public SubjectController(ISubjectService subjectService, ICurrentUserService currentUserService,
            ITMSService tmsService)
        {
            _subjectService = subjectService;
            _currentUserService = currentUserService;
            _tmsService = tmsService;
        }

        [HttpGet("all/search")]
        [ProducesResponseType(typeof(PagingViewModel<SubjectViewModel>), 200)]
        [PermissionAuthorize(Subject.ViewSubjectsList)]
        public async Task<IActionResult> GetAllSubjects([FromQuery] SubjectPagingRequestModel subjectPagingRequestModel)
        {
            var subjects = await _subjectService.GetSubjectList(subjectPagingRequestModel);
            return Ok(subjects);
        }

        [HttpGet("assigned/search")]
        [ProducesResponseType(typeof(PagingViewModel<SubjectViewModel>), 200)]
        [PermissionAuthorize(Subject.ViewAssignedSubjectsList, Subject.ViewSubjectsList)]
        public async Task<IActionResult> GetAssignedSubjects([FromQuery] SubjectPagingRequestModel subjectPagingRequestModel)
        {
            var subjects = await _subjectService.GetSubjectList(subjectPagingRequestModel, _currentUserService.UserId);
            return Ok(subjects);
        }

        [HttpGet]
        [Route("detail/{subjectId}")]
        [ProducesResponseType(typeof(SubjectViewModel), 200)]
        [PermissionAuthorize(Subject.ViewDetailOfSubject)]
        public async Task<IActionResult> GetById(int subjectId)
        {
            var role = await _subjectService.GetDetailSubject(subjectId);
            return Ok(role);
        }

        [HttpGet("detail/{subjectId}/list-sections/course/{courseId}")]
        [ProducesResponseType(typeof(SubjectWithSectionsViewModel), 200)]
        [PermissionAuthorize(Subject.ViewLearningResourcesList)]
        public async Task<ActionResult> GetSectionsAndResourcesInCourse(int subjectId, int courseId)
        {
            var result = await _subjectService.GetSectionsAndResourcesInCourse(subjectId, courseId);
            return Ok(result);
        }

        [HttpGet("detail/{subjectId}/list-sections/topic/{topicId}")]
        [ProducesResponseType(typeof(SubjectWithSectionsViewModel), 200)]
        [PermissionAuthorize(Subject.ViewLearningResourcesList)]
        public async Task<ActionResult> GetSectionsAndResourcesInTopic(int subjectId, int topicId)
        {
            var result = await _subjectService.GetSectionsAndResourcesInTopic(subjectId, topicId);
            return Ok(result);
        }

        [HttpPut("{subjectId}")]
        [ProducesResponseType(typeof(SubjectViewModelWithoutSection), 200)]
        [PermissionAuthorize(Subject.UpdateSubject)]
        public async Task<IActionResult> UpdateSubject(int subjectId, [FromBody] SubjectUpdateRequestModel requestModel)
        {
            var result = await _subjectService.UpdateDescription(subjectId, requestModel);
            return Ok(result);
        }

        [HttpGet("sync-subject")]
        public async Task<IActionResult> SyncSubject()
        {
            await _tmsService.VerifyAuthentication();
            await _subjectService.SyncSubject();
            return Ok();
        }
    }
}
