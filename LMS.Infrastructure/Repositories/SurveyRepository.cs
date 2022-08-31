using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class SurveyRepository : BaseRepository<Survey, int>, ISurveyRepository
    {
        public SurveyRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}