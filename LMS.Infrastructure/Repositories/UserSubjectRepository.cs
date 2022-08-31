using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repositories
{
    public class UserSubjectRepository : BaseRepository<UserSubject, int>, IUserSubjectRepository
    {
        public UserSubjectRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task SynchronizeBulk(List<UserSubject> listOfUserSubject)
        {
            await applicationDbContext.UserSubjects.BulkInsertAsync(listOfUserSubject, options =>
            {
                options.InsertIfNotExists = true;
                options.AutoMapOutputDirection = false;
            });
            await applicationDbContext.UserSubjects.BulkUpdateAsync(listOfUserSubject);
        }
    }
}
