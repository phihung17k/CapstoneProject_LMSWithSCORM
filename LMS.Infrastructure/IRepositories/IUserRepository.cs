using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IRepositories
{
    public interface IUserRepository : IBaseRepository<User, Guid>
    {
        List<Permission> GetPermissionsById(Guid id);

        Task SynchronizeBulk(List<User> listOfUser);

        Task AssignRoleForInsertedUser(DateTimeOffset startTime, DateTimeOffset endTime, Guid creatorId, List<int> roleIds);
    }
}
