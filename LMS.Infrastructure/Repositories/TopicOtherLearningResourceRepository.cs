using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class TopicOtherLearningResourceRepository : BaseRepository<TopicOtherLearningResource, int>,
        ITopicOtherLearningResourceRepository
    {
        public TopicOtherLearningResourceRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
