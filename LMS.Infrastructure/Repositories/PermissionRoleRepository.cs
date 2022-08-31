using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class PermissionRoleRepository : BaseRepository<PermissionRole, int>, IPermissionRoleRepository
    {
        public PermissionRoleRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}