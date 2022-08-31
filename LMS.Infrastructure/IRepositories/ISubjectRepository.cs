using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IRepositories
{
    public interface ISubjectRepository : IBaseRepository<Subject, int>
    {
        Task SynchronizeBulk(List<Subject> listOfSubject);
    }
}
