using LMS.Core.Entity;
using LMS.Core.Models.RequestModels;
using LMS.Infrastructure.Data;
using System.Threading.Tasks;

namespace LMS.Infrastructure.IRepositories
{
    public interface ISCORMInteractionRepository : IBaseRepository<SCORMInteraction, int>
    {
        void GetInteractions(ref LMSModel lms, bool isSCORMVersion12 = false);
        Task<LMSModel> SetInteractions(LMSModel lms, SCORMCore scormCore, bool isSCORMVersion12 = false);
    }
}
