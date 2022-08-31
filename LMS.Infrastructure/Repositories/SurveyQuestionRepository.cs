using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class SurveyQuestionRepository : BaseRepository<SurveyQuestion, int>, ISurveyQuestionRepository
    {
        public SurveyQuestionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
