using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class QuestionRepository : BaseRepository<Question, int>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}