using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class SyncLogRepository : BaseRepository<SyncLog, int>, ISyncLogRepository
    {
        public SyncLogRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
