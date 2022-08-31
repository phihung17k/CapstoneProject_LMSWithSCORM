using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repositories
{
    public class UserCourseRepository : BaseRepository<UserCourse, int>, IUserCourseRepository
    {
        public UserCourseRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task SynchronizeBulk(List<UserCourse> listOfUserCourse)
        {
            await applicationDbContext.UserCourses.BulkInsertAsync(listOfUserCourse, options =>
            {
                options.InsertIfNotExists = true;
                options.AutoMapOutputDirection = false;
            });
            await applicationDbContext.UserCourses.BulkUpdateAsync(listOfUserCourse);
        }
    }
}
