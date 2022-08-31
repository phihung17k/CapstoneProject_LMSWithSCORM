using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repositories
{
    public class CourseRepository : BaseRepository<Course, int>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task SynchronizeBulk(List<Course> listOfCourse)
        {
            await applicationDbContext.Courses.BulkInsertAsync(listOfCourse, options =>
            {
                options.InsertIfNotExists = true;
                options.ColumnPrimaryKeyExpression = u => u.Id;
                options.AutoMapOutputDirection = false;
            });
            await applicationDbContext.Courses.BulkUpdateAsync(listOfCourse, options =>
            {
                options.IgnoreOnUpdateExpression = c => c.Description;
            });
        }
    }
}
