using Hangfire;
using LMS.Infrastructure.IServices;
using System.Threading.Tasks;

namespace LMS.API.Jobs
{
    public class SyncJob
    {
        private readonly ICourseService courseService;
        private readonly IUserService userService;
        private readonly ISubjectService subjectService;
        private readonly ITMSService tmsService;

        public SyncJob(ICourseService courseService, IUserService userService, ISubjectService subjectService,
            ITMSService tmsService)
        {
            this.courseService = courseService;
            this.userService = userService;
            this.subjectService = subjectService;
            this.tmsService = tmsService;
        }



        #region Job Scheduler  
        [AutomaticRetry(Attempts = 0)]
        public async Task JobAsync()
        {
            await tmsService.VerifyAuthentication();
            await userService.SyncUser();
            await subjectService.SyncSubject();
            await courseService.SyncCourse();
        }
        #endregion
    }
}
