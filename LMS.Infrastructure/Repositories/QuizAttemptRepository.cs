using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class QuizAttemptRepository : BaseRepository<QuizAttempt, int>, IQuizAttemptRepository
    {
        public QuizAttemptRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}