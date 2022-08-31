using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;

namespace LMS.Infrastructure.Repositories
{
    public class PermissionRepository : BaseRepository<Permission, int>, IPermissionRepository
    {
        public PermissionRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}