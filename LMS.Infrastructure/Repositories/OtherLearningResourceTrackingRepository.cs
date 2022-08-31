using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class OtherLearningResourceTrackingRepository : BaseRepository<OtherLearningResourceTracking, int>,
        IOtherLearningResourceTrackingRepository
    {
        public OtherLearningResourceTrackingRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
