using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class OtherLearningResourceRepository : BaseRepository<OtherLearningResource, int>,
        IOtherLearningResourceRepository
    {
        public OtherLearningResourceRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
