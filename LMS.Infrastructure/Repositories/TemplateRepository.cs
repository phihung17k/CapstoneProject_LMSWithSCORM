using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class TemplateRepository : BaseRepository<Template, int>, ITemplateRepository
    {
        public TemplateRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}