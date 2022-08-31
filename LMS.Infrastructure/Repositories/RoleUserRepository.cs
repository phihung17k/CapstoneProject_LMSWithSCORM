using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class RoleUserRepository : BaseRepository<RoleUser, int>, IRoleUserRepository
    {
        public RoleUserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }


    }
}