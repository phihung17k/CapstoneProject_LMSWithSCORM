using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IRepositories
{
    public interface IUserCourseRepository : IBaseRepository<UserCourse, int>
    {
        Task SynchronizeBulk(List<UserCourse> listOfUserCourse);
    }
}
