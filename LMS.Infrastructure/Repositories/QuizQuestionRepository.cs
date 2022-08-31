using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class QuizQuestionRepository : BaseRepository<QuizQuestion, int>, IQuizQuestionRepository
    {
        public QuizQuestionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
