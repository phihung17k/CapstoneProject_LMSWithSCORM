using LMS.API.Permission;
using LMS.Core.Models.RequestModels;
using LMS.Core.Models.ViewModels;
using LMS.Infrastructure.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static LMS.Core.Common.PermissionConstants;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingOtherLearningResoursesController : ControllerBase
    {
        private readonly ITrackingOtherLearningResourseService _trackingOLRService;

        public TrackingOtherLearningResoursesController(ITrackingOtherLearningResourseService trackingOLRService)
        {
            _trackingOLRService = trackingOLRService;
        }
        [HttpPut("update/learningProgress/{OtherLearningResourceTrackingId}")]
        [ProducesResponseType(typeof(OtherLearningResourceUpdateProgressViewModel), 200)]
        [PermissionAuthorize(Course.ViewContentOfLearningResources)]
        public async Task<IActionResult> UpdateLearningProgess(int OtherLearningResourceTrackingId, [FromBody] LearningProgressUpdateRequestModel requestModel)
        {
            var result = await _trackingOLRService.UpdateLearningProgress(OtherLearningResourceTrackingId, requestModel);
            return Ok(result);
        }
    }
}
