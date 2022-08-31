using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class SectionRepository : BaseRepository<Section, int>, ISectionRepository
    {
        public SectionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
