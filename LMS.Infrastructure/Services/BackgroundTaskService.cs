using Hangfire;

namespace LMS.Infrastructure.Services
{
    public interface IBackgroundTaskService
    {
    }

    public class BackgroundTaskService : IBackgroundTaskService
    {
        private IBackgroundJobClient _backgroundJobClient;
        public BackgroundTaskService(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }
    }
}
