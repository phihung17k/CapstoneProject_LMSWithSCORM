using LMS.Infrastructure.IServices;
using LMS.Core.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using LMS.Core.Application;
using LMS.API.Hubs;
using Newtonsoft.Json.Linq;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingScormController : ControllerBase
    {
        private readonly ITrackingScormService _service;
        private readonly IHubContext<NotificationHub> _notiHubContext;
        private readonly ICurrentUserService _currentUserService;

        public TrackingScormController(ITrackingScormService service, IHubContext<NotificationHub> notiHubContext, 
            ICurrentUserService currentUserService)
        {
            _service = service;
            _notiHubContext = notiHubContext;
            _currentUserService = currentUserService;
        }

        [HttpPost("Initialize")]
        [ProducesResponseType(typeof(LMSModel), 200)]
        public async Task<ActionResult<LMSModel>> Initialize([FromBody] LMSModel lms)
        {
            lms = await _service.InitializeSession(lms);
            return Ok(lms);
        }

        [HttpPost("LMSGetValue")]
        [ProducesResponseType(typeof(LMSModel), 200)]
        public ActionResult<LMSModel> LMSGetValue([FromBody] LMSModel lms)
        {
            lms = _service.GetValue(lms);
            return Ok(lms);
        }

        [HttpPut("SetValue")]
        [ProducesResponseType(typeof(LMSModel), 200)]
        public async Task<ActionResult<LMSModel>> SetValue([FromBody] LMSModel lms)
        {
            lms = await _service.SetValue(lms);
            if (lms.IsUpdateCompletionStatus && !lms.isStopTracking)
            {
                var scormCore = lms.scormCore;
                var topicTracking = lms.TopicTracking;
                string result = JObject.FromObject(new 
                { 
                    scormCoreId = scormCore.Id,
                    progressMeasure = scormCore.ProgressMeasure,
                    completionStatus = lms.IsSCORMVersion12 ? null : scormCore.CompletionStatus,
                    lessonStatus = lms.IsSCORMVersion12 ? scormCore.LessonStatus12 : null,
                    topicTracking
                }).ToString();
                //send Scorm core to client
                await _notiHubContext.Clients.User(_currentUserService.UserId.ToString())
                    .SendAsync("UpdateScormCoreStatus", result);
            }
            return Ok(lms);
        }

        [HttpPost("Terminate")]
        [ProducesResponseType(typeof(LMSModel), 200)]
        public async Task<ActionResult<LMSModel>> Terminate([FromBody] LMSModel lms)
        {
            lms = await _service.Terminate(lms);
            return Ok(lms);
        }

        [HttpPost("Commit")]
        [ProducesResponseType(typeof(LMSModel), 200)]
        public ActionResult<LMSModel> Commit([FromBody] LMSModel lms)
        {
            lms = _service.Commit(lms);
            return Ok(lms);
        }
    }
}
