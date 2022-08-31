using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Repositories
{
    public class SubjectRepository : BaseRepository<Subject, int>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task SynchronizeBulk(List<Subject> listOfSubject)
        {
            await applicationDbContext.Subjects.BulkInsertAsync(listOfSubject, options =>
            {
                options.InsertIfNotExists = true;
                options.ColumnPrimaryKeyExpression = u => u.Id;
                options.AutoMapOutputDirection = false;
            });
            await applicationDbContext.Subjects.BulkUpdateAsync(listOfSubject);
        }
    }
}
