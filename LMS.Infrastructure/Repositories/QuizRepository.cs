using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class QuizRepository : BaseRepository<Quiz, int>, IQuizRepository
    {
        public QuizRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
