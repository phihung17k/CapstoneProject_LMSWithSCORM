using LMS.Core.Entity;
using LMS.Core.Models.RequestModels;
using LMS.Infrastructure.Data;

namespace LMS.Infrastructure.IRepositories
{
    public interface ISCORMNavigationRepository : IBaseRepository<SCORMNavigation, int>
    {
        void GetNavigation(ref LMSModel lms);
    }
}
