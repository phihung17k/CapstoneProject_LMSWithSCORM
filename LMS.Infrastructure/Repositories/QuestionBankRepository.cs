using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class QuestionBankRepository : BaseRepository<QuestionBank, int>, IQuestionBankRepository
    {
        public QuestionBankRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}