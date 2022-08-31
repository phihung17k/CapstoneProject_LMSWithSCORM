using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class SurveyOptionRepository : BaseRepository<SurveyOption, int>, ISurveyOptionRepository
    {
        public SurveyOptionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
