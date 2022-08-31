using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IRepositories
{
    public interface ICourseRepository : IBaseRepository<Course, int>
    {
        Task SynchronizeBulk(List<Course> listOfCourse);
    }
}
