using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class OptionRepository : BaseRepository<Option, int>, IOptionRepository
    {
        public OptionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}