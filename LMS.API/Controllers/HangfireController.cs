using Hangfire;
using LMS.API.Jobs;
using LMS.Infrastructure.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangfireController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly CourseJob _courseJob;

        public HangfireController(ICourseService courseService, CourseJob courseJob)
        {
            _courseService = courseService;
            _courseJob = courseJob;
        }

        [HttpGet("sendmail/test")]
        public async Task<IActionResult> ActivateHangfireTest([FromQuery] List<int> courseId)
        {
            await _courseService.ActivateHangfireTest(courseId);
            return Ok();
        }

        [HttpGet]
        [Route("active/notify-course-recurring")]
        public IActionResult ActiveCourseRecurringBackgroundJob()
        {
            RecurringJob.AddOrUpdate("check-course-daily",
                        () => _courseJob.CheckCourseDaily(),
                        "0 0 0 * * ?",
                        timeZone: TimeZoneInfo.Local);
            return Ok();
        }

        [HttpGet]
        [Route("active/notify-course")]
        public IActionResult ActiveCourseBackgroundJob()
        {
            BackgroundJob.Enqueue(() => _courseJob.CheckCourseDaily());
            return Ok();
        }
    }
}