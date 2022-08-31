using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class TopicSCORMRepository : BaseRepository<TopicSCORM, int>, ITopicSCORMRepository
    {
        public TopicSCORMRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
