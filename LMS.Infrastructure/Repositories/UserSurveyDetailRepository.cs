using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class UserSurveyDetailRepository : BaseRepository<UserSurveyDetail, int>, IUserSurveyDetailRepository
    {
        public UserSurveyDetailRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
