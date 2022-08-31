using LMS.Core.Entity;
using LMS.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IRepositories
{
    public interface IUserSubjectRepository : IBaseRepository<UserSubject, int>
    {
        Task SynchronizeBulk(List<UserSubject> listOfUserSubject);
    }
}
