using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class UserQuizRepository : BaseRepository<UserQuiz, int>, IUserQuizRepository
    {
        public UserQuizRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}