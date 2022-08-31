using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class TopicTrackingRepository : BaseRepository<TopicTracking, int>, ITopicTrackingRepository
    {
        public TopicTrackingRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}