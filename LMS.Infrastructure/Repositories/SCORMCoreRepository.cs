using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class SCORMCoreRepository : BaseRepository<SCORMCore, int>, ISCORMCoreRepository
    {
        public SCORMCoreRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
