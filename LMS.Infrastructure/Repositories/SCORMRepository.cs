using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class SCORMRepository : BaseRepository<SCORM, int>, ISCORMRepository
    {
        public SCORMRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
