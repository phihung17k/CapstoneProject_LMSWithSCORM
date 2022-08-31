using Hangfire;
using LMS.API.Jobs;
using LMS.Infrastructure.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private readonly ICourseService courseService;
        private readonly IUserService userService;
        private readonly ISubjectService subjectService;
        private readonly ITMSService tmsService;

        public SyncController(ICourseService courseService, IUserService userService, 
            ISubjectService subjectService, ITMSService tmsService)
        {
            this.courseService = courseService;
            this.userService = userService;
            this.subjectService = subjectService;
            this.tmsService = tmsService;
        }

        [HttpGet("sync-data-tms")]
        public IActionResult SyncAll()
        {
            SyncJob jobScheduler = new SyncJob(courseService, userService, subjectService, tmsService);
            BackgroundJob.Enqueue(() => jobScheduler.JobAsync());
            return Ok();
        }
    }
}
