using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class TopicRepository : BaseRepository<Topic, int>, ITopicRepository
    {
        public TopicRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}